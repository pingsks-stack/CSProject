Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

Partial Class production_version_Search_Doc
    Inherits System.Web.UI.Page

    Protected Q, ScopeNote, TypeOptions, StatusOptions, ResultsHtml As String
    Protected ResultCount As Integer

    ' ใช้เมนูตามบทบาทของผู้ใช้
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Select Case Convert.ToString(Session("Urole"))
            Case "3" : MasterPageFile = "~/app/MasterPageAdmin.master"
            Case "2" : MasterPageFile = "~/app/MasterPageTeacher.master"
            Case Else : MasterPageFile = "~/app/MasterPageHome.master"
        End Select
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim role As String = Convert.ToString(Session("Urole"))
        Dim email As String = Convert.ToString(Session("Email"))
        Q = If(Request.QueryString("q"), "").Trim()
        Dim docType As String = If(Request.QueryString("type"), "")
        Dim status As String = If(Request.QueryString("status"), "")

        ' ตัวเลือกประเภท
        Dim sbType As New StringBuilder("<option value="""">ทุกประเภท</option>")
        For Each r As DataRow In Shell.Query("SELECT Doc_Type FROM DocTypeTB ORDER BY CASE WHEN Doc_Type = N'Other' THEN 1 ELSE 0 END, TypeID").Rows
            Dim v As String = Convert.ToString(r("Doc_Type"))
            sbType.Append(Opt(v, v, docType))
        Next
        sbType.Append(Opt("none", "ยังไม่ระบุประเภท", docType))
        TypeOptions = sbType.ToString()
        StatusOptions = Opt("", "ทุกสถานะ", status) & Opt("passed", "ผ่านครบ 3/3", status) &
                        Opt("partial", "ผ่านบางส่วน", status) & Opt("pending", "รอพิจารณา", status)

        ' ขอบเขตที่เห็นได้: แอดมินเห็นทั้งหมด, คนอื่นเห็นคลังโครงงาน (ผ่าน 3/3) + โครงงานที่ตัวเองเกี่ยวข้อง
        Dim conds As New List(Of String)
        Dim ps As New List(Of SqlParameter)
        If role = "3" Then
            ScopeNote = "ค้นหาจากโครงงานทั้งหมดในระบบ"
        Else
            ScopeNote = "ค้นหาจากคลังโครงงานที่ผ่านครบ 3/3 และโครงงานที่คุณเกี่ยวข้อง"
            conds.Add("(m.Submit_TC = N'ผ่าน3/3' OR m.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email))")
        End If
        ps.Add(Shell.P("@Email", email))

        If Q <> "" Then
            conds.Add("(m.Doc_Name LIKE @Q OR m.Doc_NameEN LIKE @Q OR m.Student_Name LIKE @Q OR m.Student_Name2 LIKE @Q OR m.Name_teacherPJ LIKE @Q)")
            ps.Add(Shell.P("@Q", "%" & Q.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]") & "%"))
        End If
        If docType = "none" Then
            conds.Add("ISNULL(m.Doc_Type, N'') = N''")
        ElseIf docType <> "" Then
            conds.Add("m.Doc_Type = @Type")
            ps.Add(Shell.P("@Type", docType))
        End If
        Select Case status
            Case "passed" : conds.Add("m.Submit_TC = N'ผ่าน3/3'")
            Case "partial" : conds.Add("m.Submit_TC LIKE N'ผ่าน%' AND m.Submit_TC <> N'ผ่าน3/3'")
            Case "pending" : conds.Add("(m.Submit_TC IS NULL OR m.Submit_TC NOT LIKE N'ผ่าน%')")
        End Select

        Dim sql As String =
            "SELECT TOP 200 m.DocID, m.Doc_Name, m.Doc_NameEN, m.Student_Name, m.Student_Name2, m.Name_teacherPJ, m.Doc_Type, m.Class_Year_No, m.Submit_TC, " &
            "CASE WHEN EXISTS (SELECT 1 FROM Permission_TB p WHERE p.DocID = m.DocID AND p.Email = @Email) THEN 1 ELSE 0 END AS IsMine " &
            "FROM MAS_DOC m" & If(conds.Count > 0, " WHERE " & String.Join(" AND ", conds), "") & " ORDER BY m.Doc_dt_Save DESC"
        Dim dt = Shell.Query(sql, ps.ToArray())
        ResultCount = dt.Rows.Count

        Dim sb As New StringBuilder()
        For Each r As DataRow In dt.Rows
            Dim id As String = Convert.ToString(r("DocID"))
            Dim url As String
            If role = "3" Then
                url = "Admin_infor_See.aspx?DocID=" & id
            ElseIf CInt(r("IsMine")) = 1 Then
                url = If(role = "2", "Doc_Student_Infor.aspx?DocID=", "MAS_Doc.aspx?DocID=") & id
            Else
                url = "Doc_Infor_See.aspx?DocID=" & id
            End If
            Dim students As String = Convert.ToString(r("Student_Name")) & If(Convert.ToString(r("Student_Name2")).Trim() <> "", ", " & Convert.ToString(r("Student_Name2")).Trim(), "")
            sb.Append("<tr>")
            sb.Append("<td><div class=""tw-font-semibold tw-text-ink"">" & H(r("Doc_Name")) & "</div><div class=""tw-text-xs tw-text-muted"">" & H(r("Doc_NameEN")) & "</div></td>")
            sb.Append("<td>" & HttpUtility.HtmlEncode(students) & "</td>")
            sb.Append("<td class=""tw-text-muted"">" & H(r("Name_teacherPJ")) & "</td>")
            sb.Append("<td class=""tw-text-muted"">" & If(Convert.ToString(r("Doc_Type")) = "", "-", H(r("Doc_Type"))) & "</td>")
            sb.Append("<td class=""tw-whitespace-nowrap tw-text-muted"">" & H(r("Class_Year_No")) & "</td>")
            sb.Append("<td>" & Dash.StatusBadge(r("Submit_TC")) & "</td>")
            sb.Append("<td class=""tw-text-right""><a href=""" & url & """ class=""tw-whitespace-nowrap tw-text-sm tw-font-semibold"">ดูรายละเอียด</a></td>")
            sb.Append("</tr>")
        Next
        If dt.Rows.Count = 0 Then
            sb.Append("<tr><td colspan=""7"" class=""tw-py-12 tw-text-center tw-text-muted"">ไม่พบโครงงานที่ตรงกับเงื่อนไข</td></tr>")
        End If
        ResultsHtml = sb.ToString()
    End Sub

    Private Function H(v As Object) As String
        Return HttpUtility.HtmlEncode(Convert.ToString(v))
    End Function

    Private Function Opt(value As String, text As String, current As String) As String
        Return "<option value=""" & HttpUtility.HtmlAttributeEncode(value) & """" & If(value = current, " selected", "") & ">" & HttpUtility.HtmlEncode(text) & "</option>"
    End Function

End Class
