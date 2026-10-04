Imports System.Configuration
Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Imports System.Web

' ส่วนกลางของ layout: อักษรย่อชื่อ, การแจ้งเตือนบนกระดิ่ง และตัวช่วยดึงข้อมูล
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

    Public Function Query(sql As String, ParamArray params As SqlParameter()) As DataTable
        Dim cs As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Using con As New SqlConnection(cs)
            Using cmd As New SqlCommand(sql, con)
                cmd.Parameters.AddRange(params)
                Dim dt As New DataTable()
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
                Return dt
            End Using
        End Using
    End Function

    Public Function Exec(sql As String, ParamArray params As SqlParameter()) As Integer
        Dim cs As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Using con As New SqlConnection(cs)
            Using cmd As New SqlCommand(sql, con)
                cmd.Parameters.AddRange(params)
                con.Open()
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Public Function P(name As String, value As Object) As SqlParameter
        Return New SqlParameter(name, If(value, DBNull.Value))
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

    ' รายการที่อาจารย์ต้องยืนยัน: คำเชิญ, ขอเปลี่ยนชื่อโครงงาน, ขอเปลี่ยนสมาชิก
    Public Function TeacherPending(email As String) As DataTable
        Return Query(
                "SELECT TOP 10 * FROM ( " &
                "SELECT N'user-plus' AS Icon, N'คำเชิญเป็นอาจารย์' + ISNULL(s.Urole_TC, N'') AS Title, " &
                "N'โครงงาน: ' + ISNULL(m.Doc_Name, N'') AS Detail, s.Date_Send_NS AS At, N'Confirm_NS.aspx' AS Url " &
                "FROM SubAdd_AJ_TB s LEFT JOIN MAS_DOC m ON m.DocID = s.DocID " &
                "WHERE s.Email = @Email AND s.Status = N'รอการยืนยัน' " &
                "UNION ALL " &
                "SELECT N'pencil-line', N'ขอเปลี่ยนชื่อโครงงาน', N'ชื่อใหม่: ' + ISNULL(n.Doc_Name, N''), n.Date_Save, " &
                "N'Doc_Student_Infor.aspx?DocID=' + CAST(n.DocID AS nvarchar(20)) " &
                "FROM Sub_NPJ_TB n " &
                "WHERE n.Action = N'รอการยืนยัน' AND n.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email AND Urole = N'2') " &
                "UNION ALL " &
                "SELECT N'repeat', N'ขอเปลี่ยนสมาชิกโครงงาน', ISNULL(c.Post_Name, N'') + N' → ' + ISNULL(c.Name, N''), c.Date_Send_NS, " &
                "N'Doc_Student_Infor.aspx?DocID=' + CAST(c.DocID AS nvarchar(20)) " &
                "FROM Status_Change_TB c " &
                "WHERE c.Status = N'รอการยืนยัน' AND c.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email AND Urole = N'2') " &
                ") x ORDER BY At DESC ", P("@Email", email))
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
                "SELECT TOP 10 N'message-square' AS Icon, N'ข้อความใหม่จาก ' + ISNULL(t.Post_Name_RS, N'นิสิต') AS Title, " &
                "LEFT(ISNULL(t.Comment_Text, N''), 80) AS Detail, t.Date_Save AS At, N'Chat.aspx?with=' + t.Email_NS AS Url " &
                "FROM Talk_TB t WHERE t.Email_AJ = @Email AND t.Sender_Email <> @Email AND t.Is_Read = 0 ORDER BY t.Talk_ID DESC",
                P("@Email", email)))
            pending = dt.Rows.Count

        ElseIf role = "3" Then
            dt = Query(                "SELECT TOP 8 N'file-plus' AS Icon, N'โครงงานใหม่' AS Title, ISNULL(Doc_Name, N'') + N' · ' + ISNULL(Student_Name, N'') AS Detail, " &
                "Doc_dt_Save AS At, N'Admin_infor_See.aspx?DocID=' + CAST(DocID AS nvarchar(20)) AS Url " &
                "FROM MAS_DOC ORDER BY Doc_dt_Save DESC ")

        Else
            dt = Query(                "SELECT TOP 10 * FROM ( " &
                "SELECT CASE WHEN s.Status = N'ยืนยันแล้ว' THEN N'check-circle' ELSE N'x-circle' END AS Icon, " &
                "ISNULL(s.Name, N'') + CASE WHEN s.Status = N'ยืนยันแล้ว' THEN N' ตอบรับคำเชิญแล้ว' ELSE N' ปฏิเสธคำเชิญ' END AS Title, " &
                "N'บทบาท: ' + ISNULL(s.Urole_TC, N'') AS Detail, s.Date_Confirm AS At, " &
                "N'MAS_Doc.aspx?DocID=' + CAST(s.DocID AS nvarchar(20)) AS Url " &
                "FROM SubAdd_AJ_TB s " &
                "WHERE s.Status <> N'รอการยืนยัน' AND s.Date_Confirm IS NOT NULL " &
                "AND s.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email) " &
                "UNION ALL " &
                "SELECT N'message-circle', N'ความคิดเห็นจาก ' + ISNULL(c.Name, N''), LEFT(ISNULL(c.Comment_Text, N''), 80), c.Date_Save, " &
                "N'MAS_Doc.aspx?DocID=' + CAST(c.DocID AS nvarchar(20)) " &
                "FROM Comment_TB c " &
                "WHERE c.Email <> @Email AND c.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email) " &
                "UNION ALL " &
                "SELECT N'message-square', N'ข้อความใหม่จาก ' + ISNULL(t.Post_Name_RS, N'อาจารย์'), LEFT(ISNULL(t.Comment_Text, N''), 80), t.Date_Save, N'Chat.aspx?with=' + t.Email_AJ " &
                "FROM Talk_TB t " &
                "WHERE t.Email_NS = @Email AND t.Sender_Email <> @Email AND t.Is_Read = 0 " &
                ") x ORDER BY At DESC ", P("@Email", email))
            ' จุดแดงบนกระดิ่งของนิสิต = ข้อความแชทที่ยังไม่อ่าน
            pending = dt.Rows.Cast(Of DataRow)().Count(Function(r) Convert.ToString(r("Url")).StartsWith("Chat.aspx"))
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
            html = "<div class=""tw-px-4 tw-py-8 tw-text-center tw-text-sm tw-text-muted"">โหลดการแจ้งเตือนไม่สำเร็จ</div>"
        End Try
        CType(master.FindControl("litNotifItems"), System.Web.UI.WebControls.Literal).Text = html
        CType(master.FindControl("litNotifDot"), System.Web.UI.WebControls.Literal).Text =
            If(pending > 0,
               "<span class=""tw-absolute -tw-right-0.5 -tw-top-0.5 tw-flex tw-h-4 tw-min-w-[16px] tw-items-center tw-justify-center tw-rounded-full tw-bg-bad tw-px-1 tw-text-[10px] tw-font-bold tw-text-white"">" & pending & "</span>",
               "")
    End Sub

End Module
