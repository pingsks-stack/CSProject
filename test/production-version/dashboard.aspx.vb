Imports System.Data
Imports System.Text

Partial Class production_version_dashboard
    Inherits System.Web.UI.Page

    Protected FirstName, KpiHtml, MyProjectsHtml, ActivityHtml As String
    Protected MonthLabels, MonthFiles, MonthEdits, TypeLabels, TypeValues As String

    ' โครงงานที่ผู้ใช้เป็นสมาชิก
    Private Const MyDocs As String = "{DocID} IN (SELECT DocID FROM Permission_TB WHERE Email = @Email)"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim email As String = Convert.ToString(Session("Email"))
        Dim name As String = Convert.ToString(Session("Name")).Trim()
        FirstName = If(name.Contains(" "), name.Split(" "c)(0), name)

        Dim mine = Dash.Count("SELECT COUNT(DISTINCT DocID) FROM Permission_TB WHERE Email = @Email", Shell.P("@Email", email))
        Dim minePassed = Dash.Count("SELECT COUNT(*) FROM MAS_DOC WHERE Submit_TC = N'ผ่าน3/3' AND " & MyDocs.Replace("{DocID}", "DocID"), Shell.P("@Email", email))
        Dim files = Dash.Count("SELECT COUNT(*) FROM Doc_File WHERE " & MyDocs.Replace("{DocID}", "DocID"), Shell.P("@Email", email))
        Dim teachers = Dash.Count("SELECT COUNT(DISTINCT Email) FROM Permission_TB WHERE Urole = N'2' AND DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email)", Shell.P("@Email", email))
        Dim invites = Dash.Count("SELECT COUNT(*) FROM SubAdd_AJ_TB WHERE Status = N'รอการยืนยัน' AND " & MyDocs.Replace("{DocID}", "DocID"), Shell.P("@Email", email))
        Dim library = Dash.Count("SELECT COUNT(*) FROM MAS_DOC WHERE Submit_TC = N'ผ่าน3/3'")

        KpiHtml =
            Dash.Kpi("folder-open", "accent", "โครงงานของฉัน", mine, "ผ่านครบ 3/3 แล้ว " & minePassed & " โครงงาน") &
            Dash.Kpi("files", "info", "ไฟล์ในโครงงาน", files, "ไฟล์เอกสารที่อัปโหลดไว้", "spark-files") &
            Dash.Kpi("graduation-cap", "ok", "อาจารย์ที่ดูแล", teachers, If(invites > 0, "รออาจารย์ตอบรับ " & invites & " คำเชิญ", "ไม่มีคำเชิญค้างอยู่")) &
            Dash.Kpi("library", "warn", "คลังโครงงาน", library, "โครงงานที่ผ่านครบ 3/3 ในระบบ")

        Dim m = Dash.Monthly({
            "SELECT Date_Delete AS D FROM Doc_File_Log WHERE " & MyDocs.Replace("{DocID}", "DocID") & " AND Action IN (N'Insert', N'เพิ่ม', N'เพิ่มไฟล์', N'เพิ่มไฟล์ใหม่')",
            "SELECT Doc_dt_Save AS D FROM MAS_DOC_LOG WHERE " & MyDocs.Replace("{DocID}", "DocID")}, Shell.P("@Email", email))
        MonthLabels = m.Labels : MonthFiles = m.Series(0) : MonthEdits = m.Series(1)

        Dim t = Dash.Pairs("SELECT ISNULL(NULLIF(Doc_Type, N''), N'ยังไม่ระบุ') AS Name, COUNT(*) AS N FROM MAS_DOC GROUP BY ISNULL(NULLIF(Doc_Type, N''), N'ยังไม่ระบุ') ORDER BY N DESC")
        TypeLabels = t(0) : TypeValues = t(1)

        Dim rows = Shell.Query(
            "SELECT m.DocID, m.Doc_Name, m.Doc_Type, m.Submit_TC, " &
            "(SELECT COUNT(*) FROM Permission_TB t WHERE t.DocID = m.DocID AND t.Urole = N'2') AS Teachers " &
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
    End Sub

End Class
