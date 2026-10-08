Imports System.Data
Imports System.Text

Partial Class production_version_dashboard
    Inherits System.Web.UI.Page

    Protected FirstName, KpiHtml, MyProjectsHtml, ActivityHtml, DeadlineHtml As String
    Protected MonthLabels, MonthFiles, MonthEdits, TypeLabels, TypeValues As String

    ' โครงงานที่ผู้ใช้เป็นสมาชิก
    Private Const MyDocs As String = "{DocID} IN (SELECT DocID FROM Permission_TB WHERE Email = @Email)"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim email As String = Convert.ToString(Session("Email"))
        Dim name As String = Convert.ToString(Session("Name")).Trim()
        FirstName = If(name.Contains(" "), name.Split(" "c)(0), name)

        Dim mine = Dash.Count("SELECT COUNT(DISTINCT DocID) FROM Permission_TB WHERE Email = @Email", Shell.P("@Email", email))
        Dim minePassed = Dash.Count("SELECT COUNT(*) FROM MAS_DOC WHERE Submit_TC = 'ผ่าน3/3' AND " & MyDocs.Replace("{DocID}", "DocID"), Shell.P("@Email", email))
        Dim files = Dash.Count("SELECT COUNT(*) FROM Doc_File WHERE " & MyDocs.Replace("{DocID}", "DocID"), Shell.P("@Email", email))
        Dim teachers = Dash.Count("SELECT COUNT(DISTINCT Email) FROM Permission_TB WHERE Urole = '2' AND DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email)", Shell.P("@Email", email))
        Dim invites = Dash.Count("SELECT COUNT(*) FROM SubAdd_AJ_TB WHERE Status = 'รอการยืนยัน' AND " & MyDocs.Replace("{DocID}", "DocID"), Shell.P("@Email", email))
        Dim library = Dash.Count("SELECT COUNT(*) FROM MAS_DOC WHERE Submit_TC = 'ผ่าน3/3'")

        KpiHtml =
            Dash.Kpi("folder-open", "accent", "โครงงานของฉัน", mine, "ผ่านครบ 3/3 แล้ว " & minePassed & " โครงงาน") &
            Dash.Kpi("files", "info", "ไฟล์ในโครงงาน", files, "ไฟล์เอกสารที่อัปโหลดไว้", "spark-files") &
            Dash.Kpi("graduation-cap", "ok", "อาจารย์ที่ดูแล", teachers, If(invites > 0, "รออาจารย์ตอบรับ " & invites & " คำเชิญ", "ไม่มีคำเชิญค้างอยู่")) &
            Dash.Kpi("library", "warn", "คลังโครงงาน", library, "โครงงานที่ผ่านครบ 3/3 ในระบบ")

        Dim m = Dash.Monthly({
            "SELECT Date_Delete AS D FROM Doc_File_Log WHERE " & MyDocs.Replace("{DocID}", "DocID") & " AND Action IN ('Insert', 'เพิ่ม', 'เพิ่มไฟล์', 'เพิ่มไฟล์ใหม่')",
            "SELECT Doc_dt_Save AS D FROM MAS_DOC_LOG WHERE " & MyDocs.Replace("{DocID}", "DocID")}, Shell.P("@Email", email))
        MonthLabels = m.Labels : MonthFiles = m.Series(0) : MonthEdits = m.Series(1)

        Dim t = Dash.Pairs("SELECT COALESCE(NULLIF(Doc_Type, ''), 'ยังไม่ระบุ') AS Name, COUNT(*) AS N FROM MAS_DOC GROUP BY COALESCE(NULLIF(Doc_Type, ''), 'ยังไม่ระบุ') ORDER BY N DESC")
        TypeLabels = t(0) : TypeValues = t(1)

        Dim rows = Shell.Query(
            "SELECT m.DocID, m.Doc_Name, m.Doc_Type, m.Submit_TC, " &
            "(SELECT COUNT(*) FROM Permission_TB t WHERE t.DocID = m.DocID AND t.Urole = '2') AS Teachers " &
            "FROM MAS_DOC m WHERE " & MyDocs.Replace("{DocID}", "m.DocID") & " ORDER BY m.Doc_dt_Save DESC", Shell.P("@Email", email))
        Dim sb As New StringBuilder()
        For Each r As DataRow In rows.Rows
            sb.Append("<tr>")
            sb.Append("<td class=""tw-font-semibold"">" & HttpUtility.HtmlEncode(Convert.ToString(r("Doc_Name"))) & "</td>")
            sb.Append("<td class=""tw-text-muted"">" & HttpUtility.HtmlEncode(If(Convert.ToString(r("Doc_Type")) = "", "ยังไม่ระบุ", Convert.ToString(r("Doc_Type")))) & "</td>")
            sb.Append("<td>" & r("Teachers") & "/3</td>")
            sb.Append("<td>" & Dash.StatusBadge(r("Submit_TC")) & "</td>")
            sb.Append("<td class=""tw-text-right""><a href=""MAS_Doc.aspx?DocID=" & r("DocID") & """ class=""tw-text-sm tw-font-semibold"">จัดการ</a></td>")
            sb.Append("</tr>")
        Next
        If rows.Rows.Count = 0 Then
            sb.Append("<tr><td colspan=""5"" class=""tw-py-10 tw-text-center tw-text-muted"">ยังไม่มีโครงงาน · <a href=""Insert.aspx"" class=""tw-font-semibold"">เพิ่มโครงงานแรก</a></td></tr>")
        End If
        MyProjectsHtml = sb.ToString()

        ActivityHtml = Dash.Activity(Dash.ActivitySql(MyDocs, 7), Shell.P("@Email", email))
        DeadlineHtml = BuildDeadlines(email)
    End Sub

    ' กำหนดส่งของโครงงานที่เป็นสมาชิก: แสดงที่ยังไม่ส่ง (เลยกำหนด/ใกล้ถึง) และที่ส่งแล้ว
    Private Function BuildDeadlines(email As String) As String
        Dim dt = Shell.Query(
            "SELECT m.DocID, m.Doc_Name, d.Chapter, d.Due_Date, d.Note, " &
            "(SELECT MAX(s.Version) FROM Doc_Submission s WHERE s.DocID = m.DocID AND s.Chapter = d.Chapter) AS LastVersion " &
            "FROM MAS_DOC m JOIN Deadline_TB d ON d.Class_Year_No = m.Class_Year_No " &
            "WHERE m.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email) ORDER BY d.Due_Date",
            Shell.P("@Email", email))
        If dt.Rows.Count = 0 Then Return ""

        Dim overdue As Integer = 0
        Dim sb As New StringBuilder()
        For Each r As DataRow In dt.Rows
            Dim due As DateTime = CDate(r("Due_Date"))
            Dim days As Integer = CInt((due.Date - DateTime.Today).TotalDays)
            Dim sent As Boolean = Not IsDBNull(r("LastVersion"))
            Dim tone As String, text As String
            If sent Then
                tone = "ok" : text = "ส่งแล้ว v" & Convert.ToString(r("LastVersion"))
            ElseIf days < 0 Then
                tone = "bad" : text = "เลยกำหนด " & -days & " วัน" : overdue += 1
            ElseIf days <= 7 Then
                tone = "warn" : text = If(days = 0, "ครบกำหนดวันนี้", "อีก " & days & " วัน")
            Else
                tone = "info" : text = "อีก " & days & " วัน"
            End If
            sb.Append("<a href=""Chapters.aspx?DocID=" & r("DocID") & """ class=""tw-flex tw-items-center tw-gap-3 tw-border-b tw-border-line tw-px-5 tw-py-3 tw-no-underline last:tw-border-b-0 hover:tw-bg-surface-2 hover:tw-no-underline"">")
            sb.Append("<span class=""kpi-icon tw-bg-" & tone & "/15 tw-text-" & tone & """><i data-lucide=""" & If(sent, "check", "calendar-clock") & """></i></span>")
            sb.Append("<div class=""tw-min-w-0 tw-flex-1""><div class=""tw-font-semibold tw-text-ink"">" & HttpUtility.HtmlEncode(Convert.ToString(r("Chapter"))) & "</div>")
            sb.Append("<div class=""tw-truncate tw-text-xs tw-text-muted"">กำหนดส่ง " & Chapters.ThaiDate(due) & If(Convert.ToString(r("Note")) <> "", " · " & HttpUtility.HtmlEncode(Convert.ToString(r("Note"))), "") & "</div></div>")
            sb.Append("<span class=""badge-soft tw-bg-" & tone & "/15 tw-text-" & tone & """>" & HttpUtility.HtmlEncode(text) & "</span></a>")
        Next
        Return "<div class=""panel tw-mt-4 tw-overflow-hidden""><div class=""panel-head""><div><h3 class=""panel-title"">กำหนดส่งเอกสาร</h3><p class=""panel-sub"">" &
               If(overdue > 0, "<span class=""tw-text-bad"">มี " & overdue & " บทที่เลยกำหนดแล้ว</span>", "ตามปีการศึกษาของโครงงาน") &
               "</p></div><a href=""Chapters.aspx?DocID=" & dt.Rows(0)("DocID") & """ class=""btn-accent""><i data-lucide=""upload""></i>ส่งเอกสาร</a></div>" &
               "<div class=""tw-grid md:tw-grid-cols-2"">" & sb.ToString() & "</div></div>"
    End Function

End Class
