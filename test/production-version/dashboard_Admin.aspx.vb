Imports System.Data
Imports System.Text

Partial Class production_version_dashboard_Admin
    Inherits System.Web.UI.Page

    Protected KpiHtml, LatestHtml, ActivityHtml As String
    Protected MonthLabels, MonthProjects, MonthFiles, TypeLabels, TypeValues As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim projects = Dash.Count("SELECT COUNT(*) FROM MAS_DOC")
        Dim passed = Dash.Count("SELECT COUNT(*) FROM MAS_DOC WHERE Submit_TC = N'ผ่าน3/3'")
        Dim students = Dash.Count("SELECT COUNT(*) FROM Signin_Signup WHERE Urole = N'1'")
        Dim teachers = Dash.Count("SELECT COUNT(*) FROM Signin_Signup WHERE Urole = N'2'")
        Dim files = Dash.Count("SELECT COUNT(*) FROM Doc_File")
        Dim untyped = Dash.Count("SELECT COUNT(*) FROM MAS_DOC WHERE ISNULL(Doc_Type, N'') = N''")

        KpiHtml =
            Dash.Kpi("folder-kanban", "accent", "โครงงานทั้งหมด", projects, "ยังไม่ระบุประเภท " & untyped & " โครงงาน", "spark-projects") &
            Dash.Kpi("users", "info", "ผู้ใช้งาน", students + teachers, "นิสิต " & students & " · อาจารย์ " & teachers) &
            Dash.Kpi("badge-check", "ok", "ผ่านครบ 3/3", passed, If(projects > 0, Math.Round(passed * 100.0 / projects) & "% ของโครงงานทั้งหมด", "-")) &
            Dash.Kpi("files", "warn", "ไฟล์เอกสาร", files, "เฉลี่ย " & If(projects > 0, Math.Round(files / projects, 1), 0) & " ไฟล์ต่อโครงงาน", "spark-files")

        Dim m = Dash.Monthly({"SELECT Doc_dt_Save AS D FROM MAS_DOC",
                              "SELECT Date_Delete AS D FROM Doc_File_Log WHERE Action IN (N'Insert', N'เพิ่ม', N'เพิ่มไฟล์', N'เพิ่มไฟล์ใหม่')"})
        MonthLabels = m.Labels : MonthProjects = m.Series(0) : MonthFiles = m.Series(1)

        Dim t = Dash.Pairs("SELECT ISNULL(NULLIF(Doc_Type, N''), N'ยังไม่ระบุ') AS Name, COUNT(*) AS N FROM MAS_DOC GROUP BY ISNULL(NULLIF(Doc_Type, N''), N'ยังไม่ระบุ') ORDER BY N DESC")
        TypeLabels = t(0) : TypeValues = t(1)

        Dim sb As New StringBuilder()
        For Each r As DataRow In Shell.Query("SELECT TOP 6 DocID, Doc_Name, Student_Name, Doc_Type, Submit_TC, Doc_dt_Save FROM MAS_DOC ORDER BY Doc_dt_Save DESC").Rows
            sb.Append("<tr>")
            sb.Append("<td class=""tw-whitespace-nowrap tw-text-muted"">" & If(IsDBNull(r("Doc_dt_Save")), "-", CDate(r("Doc_dt_Save")).ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))) & "</td>")
            sb.Append("<td><a class=""tw-font-semibold"" href=""Admin_infor_See.aspx?DocID=" & r("DocID") & """>" & HttpUtility.HtmlEncode(Convert.ToString(r("Doc_Name"))) & "</a></td>")
            sb.Append("<td>" & HttpUtility.HtmlEncode(Convert.ToString(r("Student_Name"))) & "</td>")
            sb.Append("<td class=""tw-text-muted"">" & HttpUtility.HtmlEncode(If(Convert.ToString(r("Doc_Type")) = "", "-", Convert.ToString(r("Doc_Type")))) & "</td>")
            sb.Append("<td>" & Dash.StatusBadge(r("Submit_TC")) & "</td>")
            sb.Append("</tr>")
        Next
        LatestHtml = sb.ToString()

        ActivityHtml = Dash.Activity(Dash.ActivitySql("1 = 1", 7))
    End Sub

End Class
