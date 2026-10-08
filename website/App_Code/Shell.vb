Imports System.Linq
Imports System.Data
Imports System.Text
Imports System.Web
Imports Npgsql

' ส่วนกลางของ layout: อักษรย่อชื่อ, การแจ้งเตือนบนกระดิ่ง และตัวช่วยดึงข้อมูล (PostgreSQL ผ่าน Db)
Public Module Shell

    Public Function Initials(name As String) As String
        Dim parts = (If(name, "")).Trim().Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        If parts.Length = 0 Then Return "?"
        ' ข้ามคำนำหน้าชื่อภาษาไทย
        Dim skip As String() = {"นาย", "นาง", "นางสาว", "อาจารย์", "ดร.", "ผศ.", "รศ.", "ศ."}
        Dim words = parts.Where(Function(p) Not skip.Contains(p)).ToArray()
        If words.Length = 0 Then words = parts
        Dim first As String = words(0).Substring(0, 1)
        Dim second As String = If(words.Length > 1, words(1).Substring(0, 1), "")
        Return HttpUtility.HtmlEncode((first & second).ToUpperInvariant())
    End Function

    Public Function Query(sql As String, ParamArray params As NpgsqlParameter()) As DataTable
        Return PgDb.Query(sql, params)
    End Function

    Public Function Exec(sql As String, ParamArray params As NpgsqlParameter()) As Integer
        Return PgDb.Exec(sql, params)
    End Function

    Public Function P(name As String, value As Object) As NpgsqlParameter
        Return PgDb.Param(name, value)
    End Function

    Public Function TimeAgo(value As Object) As String
        If value Is Nothing OrElse IsDBNull(value) Then Return ""
        Dim span As TimeSpan = DateTime.Now - CDate(value)
        If span.TotalMinutes < 1 Then Return "เมื่อสักครู่"
        If span.TotalHours < 1 Then Return CInt(span.TotalMinutes) & " นาที"
        If span.TotalDays < 1 Then Return CInt(span.TotalHours) & " ชม."
        If span.TotalDays < 30 Then Return CInt(span.TotalDays) & " วัน"
        Return CDate(value).ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
    End Function

    ' รายการที่อาจารย์ต้องยืนยัน: คำเชิญ, ขอเปลี่ยนชื่อโครงงาน, ขอเปลี่ยนสมาชิก, เอกสารรอตรวจ
    Public Function TeacherPending(email As String) As DataTable
        Return Query(
                "SELECT * FROM ( " &
                "SELECT 'user-plus' AS Icon, 'คำเชิญเป็นอาจารย์' || COALESCE(s.Urole_TC, '') AS Title, " &
                "'โครงงาน: ' || COALESCE(m.Doc_Name, '') AS Detail, s.Date_Send_NS AS At, 'Confirm_NS.aspx' AS Url " &
                "FROM SubAdd_AJ_TB s LEFT JOIN MAS_DOC m ON m.DocID = s.DocID " &
                "WHERE s.Email = @Email AND s.Status = 'รอการยืนยัน' " &
                "UNION ALL " &
                "SELECT 'pencil-line', 'ขอเปลี่ยนชื่อโครงงาน', 'ชื่อใหม่: ' || COALESCE(n.Doc_Name, ''), n.Date_Save, " &
                "'Doc_Student_Infor.aspx?DocID=' || n.DocID " &
                "FROM Sub_NPJ_TB n " &
                "WHERE n.Action = 'รอการยืนยัน' AND n.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email AND Urole = '2') " &
                "UNION ALL " &
                "SELECT 'repeat', 'ขอเปลี่ยนสมาชิกโครงงาน', COALESCE(c.Post_Name, '') || ' → ' || COALESCE(c.Name, ''), c.Date_Send_NS, " &
                "'Doc_Student_Infor.aspx?DocID=' || c.DocID " &
                "FROM Status_Change_TB c " &
                "WHERE c.Status = 'รอการยืนยัน' AND c.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email AND Urole = '2') " &
                "UNION ALL " &
                "SELECT 'file-check', 'เอกสารรอตรวจ: ' || s.Chapter || ' v' || s.Version, m.Doc_Name, s.Uploaded_At, " &
                "'Chapters.aspx?DocID=' || s.DocID " &
                "FROM Doc_Submission s JOIN MAS_DOC m ON m.DocID = s.DocID " &
                "WHERE s.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email AND Urole = '2') " &
                "AND s.Version = (SELECT MAX(x.Version) FROM Doc_Submission x WHERE x.DocID = s.DocID AND x.Chapter = s.Chapter) " &
                "AND NOT EXISTS (SELECT 1 FROM Doc_Review r WHERE r.Sub_ID = s.Sub_ID AND r.Reviewer_Email = @Email) " &
                ") x ORDER BY At DESC NULLS LAST LIMIT 10", P("@Email", email))
    End Function

    ' ===== การแจ้งเตือน =====
    ' คืนค่า (จำนวนที่ต้องจัดการ, HTML รายการ) ตามบทบาทของผู้ใช้
    Public Function Notifications(ctx As HttpContext, ByRef pending As Integer) As String
        Dim email As String = Convert.ToString(ctx.Session("Email"))
        Dim role As String = Convert.ToString(ctx.Session("Urole"))
        Dim dt As DataTable
        pending = 0

        If role = "2" Then
            dt = TeacherPending(email)
            ' รวมข้อความแชทจากนิสิตที่ยังไม่ได้อ่าน
            dt.Merge(Query(
                "SELECT 'message-square' AS Icon, 'ข้อความใหม่จาก ' || COALESCE(t.Post_Name_RS, 'นิสิต') AS Title, " &
                "LEFT(COALESCE(t.Comment_Text, ''), 80) AS Detail, t.Date_Save AS At, 'Chat.aspx?with=' || t.Email_NS AS Url " &
                "FROM Talk_TB t WHERE t.Email_AJ = @Email AND t.Sender_Email <> @Email AND t.Is_Read = 0 ORDER BY t.Talk_ID DESC LIMIT 10",
                P("@Email", email)))
            pending = dt.Rows.Count

        ElseIf role = "3" Then
            dt = Query(
                "SELECT 'file-plus' AS Icon, 'โครงงานใหม่' AS Title, COALESCE(Doc_Name, '') || ' · ' || COALESCE(Student_Name, '') AS Detail, " &
                "Doc_dt_Save AS At, 'Admin_infor_See.aspx?DocID=' || DocID AS Url " &
                "FROM MAS_DOC ORDER BY Doc_dt_Save DESC NULLS LAST LIMIT 8")

        Else
            dt = Query(
                "SELECT * FROM ( " &
                "SELECT CASE WHEN s.Status = 'ยืนยันแล้ว' THEN 'check-circle' ELSE 'x-circle' END AS Icon, " &
                "COALESCE(s.Name, '') || CASE WHEN s.Status = 'ยืนยันแล้ว' THEN ' ตอบรับคำเชิญแล้ว' ELSE ' ปฏิเสธคำเชิญ' END AS Title, " &
                "'บทบาท: ' || COALESCE(s.Urole_TC, '') AS Detail, s.Date_Confirm AS At, " &
                "'MAS_Doc.aspx?DocID=' || s.DocID AS Url " &
                "FROM SubAdd_AJ_TB s " &
                "WHERE s.Status <> 'รอการยืนยัน' AND s.Date_Confirm IS NOT NULL " &
                "AND s.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email) " &
                "UNION ALL " &
                "SELECT 'message-circle', 'ความคิดเห็นจาก ' || COALESCE(c.Name, ''), LEFT(COALESCE(c.Comment_Text, ''), 80), c.Date_Save, " &
                "'MAS_Doc.aspx?DocID=' || c.DocID " &
                "FROM Comment_TB c " &
                "WHERE c.Email <> @Email AND c.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email) " &
                "UNION ALL " &
                "SELECT 'message-square', 'ข้อความใหม่จาก ' || COALESCE(t.Post_Name_RS, 'อาจารย์'), LEFT(COALESCE(t.Comment_Text, ''), 80), t.Date_Save, 'Chat.aspx?with=' || t.Email_AJ " &
                "FROM Talk_TB t " &
                "WHERE t.Email_NS = @Email AND t.Sender_Email <> @Email AND t.Is_Read = 0 " &
                "UNION ALL " &
                "SELECT CASE WHEN r.Verdict = 'ผ่าน' THEN 'check-circle' ELSE 'alert-circle' END, " &
                "COALESCE(r.Reviewer_Name, 'อาจารย์') || CASE WHEN r.Verdict = 'ผ่าน' THEN ' ให้ผ่าน ' ELSE ' ขอให้แก้ไข ' END || s.Chapter, " &
                "LEFT(COALESCE(r.Comment, ''), 80), r.Created_At, 'Chapters.aspx?DocID=' || s.DocID " &
                "FROM Doc_Review r JOIN Doc_Submission s ON s.Sub_ID = r.Sub_ID " &
                "WHERE r.Is_Read = 0 AND s.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email) " &
                ") x ORDER BY At DESC NULLS LAST LIMIT 10", P("@Email", email))
            ' จุดแดงบนกระดิ่งของนิสิต = ข้อความแชท/ความเห็นเอกสารที่ยังไม่อ่าน
            pending = dt.Rows.Cast(Of DataRow)().Count(Function(r) Convert.ToString(r("Url")).StartsWith("Chat.aspx") OrElse Convert.ToString(r("Url")).StartsWith("Chapters.aspx"))
        End If

        If dt.Rows.Count = 0 Then
            Return "<div class=""tw-px-4 tw-py-8 tw-text-center tw-text-sm tw-text-muted"">ไม่มีการแจ้งเตือน</div>"
        End If

        Dim sb As New StringBuilder()
        For Each r As DataRow In dt.Rows
            sb.AppendFormat(
                "<a href=""{0}"" class=""tw-flex tw-gap-3 tw-border-b tw-border-line tw-px-4 tw-py-3 tw-no-underline hover:tw-bg-surface-2 hover:tw-no-underline"">" &
                "<span class=""kpi-icon tw-bg-accent/15 tw-text-accent""><i data-lucide=""{1}""></i></span>" &
                "<span class=""tw-min-w-0 tw-flex-1""><span class=""tw-block tw-truncate tw-text-sm tw-font-semibold tw-text-ink"">{2}</span>" &
                "<span class=""tw-block tw-truncate tw-text-xs tw-text-muted"">{3}</span></span>" &
                "<span class=""tw-shrink-0 tw-text-[11px] tw-text-muted"">{4}</span></a>",
                HttpUtility.HtmlAttributeEncode(Convert.ToString(r("Url"))),
                HttpUtility.HtmlAttributeEncode(Convert.ToString(r("Icon"))),
                HttpUtility.HtmlEncode(Convert.ToString(r("Title"))),
                HttpUtility.HtmlEncode(Convert.ToString(r("Detail"))),
                HttpUtility.HtmlEncode(TimeAgo(r("At"))))
        Next
        Return sb.ToString()
    End Function

    ' ใส่อักษรย่อ ชื่อ และการแจ้งเตือนลงใน master
    Public Sub FillShell(master As System.Web.UI.MasterPage)
        Dim ctx = HttpContext.Current
        Dim name As String = Convert.ToString(ctx.Session("Name"))
        Dim ini As String = Initials(name)
        CType(master.FindControl("litInitials"), System.Web.UI.WebControls.Literal).Text = ini
        CType(master.FindControl("litInitials2"), System.Web.UI.WebControls.Literal).Text = ini

        Dim pending As Integer
        Dim html As String
        Try
            html = Notifications(ctx, pending)
        Catch ex As Exception
            System.Diagnostics.Trace.TraceError("Notifications: " & ex.ToString())
            PgDb.LogError("(notifications)", ex)
            html = "<div class=""tw-px-4 tw-py-8 tw-text-center tw-text-sm tw-text-muted"">โหลดการแจ้งเตือนไม่สำเร็จ</div>"
        End Try
        CType(master.FindControl("litNotifItems"), System.Web.UI.WebControls.Literal).Text = html
        CType(master.FindControl("litNotifDot"), System.Web.UI.WebControls.Literal).Text =
            If(pending > 0,
               "<span class=""tw-absolute -tw-right-0.5 -tw-top-0.5 tw-flex tw-h-4 tw-min-w-[16px] tw-items-center tw-justify-center tw-rounded-full tw-bg-bad tw-px-1 tw-text-[10px] tw-font-bold tw-text-white"">" & pending & "</span>",
               "")
    End Sub

End Module
