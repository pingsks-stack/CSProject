Imports System.Data
Imports System.Text

Partial Class production_version_dashboard_Teacher
    Inherits System.Web.UI.Page

    Protected KpiHtml, OverdueHtml, PendingHtml, ActivityHtml As String
    Protected MonthLabels, MonthFiles, MonthEdits, StatusLabels, StatusValues As String

    ' โครงงานที่อาจารย์คนนี้ดูแล
    Private Const MyDocs As String = "{DocID} IN (SELECT DocID FROM Permission_TB WHERE Email = @Email AND Urole = '2')"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim email As String = Convert.ToString(Session("Email"))
        Dim scope As String = MyDocs.Replace("{DocID}", "DocID")

        Dim total = Dash.Count("SELECT COUNT(*) FROM MAS_DOC WHERE " & scope, Shell.P("@Email", email))
        Dim advisor = Dash.Count("SELECT COUNT(*) FROM Permission_TB WHERE Email = @Email AND Urole = '2' AND Urole_TC = 'ที่ปรึกษา'", Shell.P("@Email", email))
        Dim passed = Dash.Count("SELECT COUNT(*) FROM MAS_DOC WHERE Submit_TC = 'ผ่าน3/3' AND " & scope, Shell.P("@Email", email))
        Dim myApproved = Dash.Count("SELECT COUNT(*) FROM Permission_TB WHERE Email = @Email AND Urole = '2' AND Submit_TC = 'ผ่าน'", Shell.P("@Email", email))
        Dim files = Dash.Count("SELECT COUNT(*) FROM Doc_File WHERE " & scope, Shell.P("@Email", email))
        Dim pending = Shell.TeacherPending(email)

        KpiHtml =
            Dash.Kpi("folder-open", "accent", "โครงงานที่ดูแล", total, "เป็นอาจารย์ที่ปรึกษา " & advisor & " โครงงาน") &
            Dash.Kpi("inbox", "warn", "รอการยืนยัน", pending.Rows.Count, "คำเชิญ ขอเปลี่ยนชื่อ และขอเปลี่ยนสมาชิก") &
            Dash.Kpi("badge-check", "ok", "ผ่านครบ 3/3", passed, "คุณกดผ่านไปแล้ว " & myApproved & " โครงงาน") &
            Dash.Kpi("files", "info", "ไฟล์เอกสาร", files, "ในโครงงานที่ดูแล", "spark-files")

        Dim m = Dash.Monthly({
            "SELECT Date_Delete AS D FROM Doc_File_Log WHERE " & scope & " AND Action IN ('Insert', 'เพิ่ม', 'เพิ่มไฟล์', 'เพิ่มไฟล์ใหม่')",
            "SELECT Doc_dt_Save AS D FROM MAS_DOC_LOG WHERE " & scope}, Shell.P("@Email", email))
        MonthLabels = m.Labels : MonthFiles = m.Series(0) : MonthEdits = m.Series(1)

        Dim s = Dash.Pairs(
            "SELECT CASE WHEN Submit_TC = 'ผ่าน3/3' THEN 'ผ่านครบ 3/3' WHEN Submit_TC LIKE 'ผ่าน%' THEN 'ผ่านบางส่วน' " &
            "WHEN Submit_TC LIKE '%ไม่ผ่าน%' THEN 'ไม่ผ่าน' ELSE 'รอพิจารณา' END AS Name, COUNT(*) AS N " &
            "FROM MAS_DOC WHERE " & scope & " GROUP BY CASE WHEN Submit_TC = 'ผ่าน3/3' THEN 'ผ่านครบ 3/3' WHEN Submit_TC LIKE 'ผ่าน%' THEN 'ผ่านบางส่วน' " &
            "WHEN Submit_TC LIKE '%ไม่ผ่าน%' THEN 'ไม่ผ่าน' ELSE 'รอพิจารณา' END", Shell.P("@Email", email))
        StatusLabels = s(0) : StatusValues = s(1)

        Dim sb As New StringBuilder()
        For Each r As DataRow In pending.Rows
            sb.Append("<tr>")
            sb.Append("<td class=""tw-whitespace-nowrap tw-text-muted"">" & HttpUtility.HtmlEncode(Shell.TimeAgo(r("At"))) & "</td>")
            sb.Append("<td class=""tw-font-semibold"">" & HttpUtility.HtmlEncode(Convert.ToString(r("Title"))) & "</td>")
            sb.Append("<td class=""tw-text-muted"">" & HttpUtility.HtmlEncode(Convert.ToString(r("Detail"))) & "</td>")
            sb.Append("<td class=""tw-text-right""><a href=""" & HttpUtility.HtmlAttributeEncode(Convert.ToString(r("Url"))) & """ class=""tw-text-sm tw-font-semibold"">ดำเนินการ</a></td>")
            sb.Append("</tr>")
        Next
        If pending.Rows.Count = 0 Then
            sb.Append("<tr><td colspan=""4"" class=""tw-py-10 tw-text-center tw-text-muted"">ไม่มีรายการรอยืนยัน</td></tr>")
        End If
        PendingHtml = sb.ToString()

        ActivityHtml = Dash.Activity(Dash.ActivitySql(MyDocs, 7), Shell.P("@Email", email))
        OverdueHtml = Chapters.OverduePanel(MyDocs.Replace("{DocID}", "m.DocID"), "Chapters.aspx?DocID=", Shell.P("@Email", email))
    End Sub

End Class
