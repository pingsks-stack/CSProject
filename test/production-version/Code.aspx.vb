Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text

Partial Class production_version_Code
    Inherits System.Web.UI.Page

    Protected Q, ScopeNote, LangOptions, ListHtml, ViewerHtml As String
    Protected ResultCount As Integer
    Protected CanAdd As Boolean

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Select Case Convert.ToString(Session("Urole"))
            Case "3" : MasterPageFile = "~/production-version/MasterPageAdmin.master"
            Case "2" : MasterPageFile = "~/production-version/MasterPageTeacher.master"
            Case Else : MasterPageFile = "~/production-version/MasterPageHome.master"
        End Select
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim role As String = Convert.ToString(Session("Urole"))
        Dim email As String = Convert.ToString(Session("Email"))
        CanAdd = role = "1"
        Q = If(Request.QueryString("q"), "").Trim()
        Dim lang As String = If(Request.QueryString("lang"), "")
        Dim selected As Integer
        Integer.TryParse(Request.QueryString("id"), selected)

        Dim sbLang As New StringBuilder("<option value="""">ทุกภาษา</option>")
        For i As Integer = 0 To CodeLib.Languages.GetLength(0) - 1
            Dim v As String = CodeLib.Languages(i, 0)
            sbLang.Append("<option value=""" & HttpUtility.HtmlAttributeEncode(v) & """" & If(v = lang, " selected", "") & ">" & HttpUtility.HtmlEncode(CodeLib.Languages(i, 1)) & "</option>")
        Next
        LangOptions = sbLang.ToString()

        ' ขอบเขต: แอดมินเห็นทั้งหมด, คนอื่นเห็นโค้ดของโครงงานที่ผ่าน 3/3 และโครงงานที่ตัวเองเกี่ยวข้อง
        Dim conds As New List(Of String)
        Dim ps As New List(Of SqlParameter) From {Shell.P("@Email", email)}
        If role = "3" Then
            ScopeNote = "โค้ดและฟังก์ชันจากทุกโครงงานในระบบ"
        Else
            ScopeNote = "โค้ดจากคลังโครงงานที่ผ่านครบ 3/3 และโครงงานที่คุณเกี่ยวข้อง ใช้เป็นแนวทางในการทำโครงงาน"
            conds.Add("(m.Submit_TC = N'ผ่าน3/3' OR c.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email))")
        End If
        If Q <> "" Then
            conds.Add("(c.Doc_Functions_Code LIKE @Q OR c.Doc_List_Code LIKE @Q OR m.Doc_Name LIKE @Q OR m.Doc_NameEN LIKE @Q)")
            ps.Add(Shell.P("@Q", "%" & Q.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]") & "%"))
        End If
        Dim onlyDoc As Integer
        If Integer.TryParse(Request.QueryString("doc"), onlyDoc) AndAlso onlyDoc > 0 Then
            conds.Add("c.DocID = @Doc")
            ps.Add(Shell.P("@Doc", onlyDoc))
        End If
        If lang <> "" Then
            Dim names = CodeLib.Aliases(lang)
            Dim inList As New List(Of String)
            For i As Integer = 0 To names.Length - 1
                inList.Add("@L" & i)
                ps.Add(Shell.P("@L" & i, names(i)))
            Next
            conds.Add("ISNULL(c.Doc_List_Type, N'') IN (" & String.Join(",", inList) & ")")
        End If

        Dim dt = Shell.Query(
            "SELECT TOP 300 c.Doc_List_ID, c.DocID, c.Doc_List_Type, c.Doc_Functions_Code, c.Doc_List_Code, c.Doc_Date_Save, m.Doc_Name, m.Submit_TC, " &
            "CASE WHEN EXISTS (SELECT 1 FROM Permission_TB p WHERE p.DocID = c.DocID AND p.Email = @Email) THEN 1 ELSE 0 END AS IsMine " &
            "FROM Doc_List_TB c JOIN MAS_DOC m ON m.DocID = c.DocID" &
            If(conds.Count > 0, " WHERE " & String.Join(" AND ", conds), "") & " ORDER BY c.Doc_Date_Save DESC", ps.ToArray())
        ResultCount = dt.Rows.Count

        Dim current As DataRow = Nothing
        For Each r As DataRow In dt.Rows
            If CInt(r("Doc_List_ID")) = selected Then current = r
        Next
        If current Is Nothing AndAlso dt.Rows.Count > 0 Then current = dt.Rows(0)

        ' รายการทางซ้าย
        Dim qs As String = If(onlyDoc > 0, "&doc=" & onlyDoc, "") & If(Q <> "", "&q=" & HttpUtility.UrlEncode(Q), "") & If(lang <> "", "&lang=" & HttpUtility.UrlEncode(lang), "")
        Dim sb As New StringBuilder()
        For Each r As DataRow In dt.Rows
            Dim active As Boolean = current IsNot Nothing AndAlso r Is current
            Dim lines As Integer = Convert.ToString(r("Doc_List_Code")).Split(ControlChars.Lf).Length
            sb.Append("<a href=""Code.aspx?id=" & r("Doc_List_ID") & qs & """ class=""tw-block tw-border-b tw-border-line tw-px-5 tw-py-3 tw-no-underline hover:tw-bg-surface-2 hover:tw-no-underline" & If(active, " tw-bg-accent/10", "") & """>")
            sb.Append("<div class=""tw-flex tw-items-center tw-justify-between tw-gap-2"">")
            sb.Append("<span class=""tw-truncate tw-font-semibold tw-text-ink"">" & H(If(Convert.ToString(r("Doc_Functions_Code")) = "", "(ไม่มีชื่อ)", r("Doc_Functions_Code"))) & "</span>")
            sb.Append("<span class=""badge-soft tw-shrink-0 tw-bg-gold/15 tw-text-gold"">" & H(CodeLib.Display(Convert.ToString(r("Doc_List_Type")))) & "</span></div>")
            sb.Append("<div class=""tw-mt-0.5 tw-truncate tw-text-xs tw-text-muted"">" & H(r("Doc_Name")) & " · " & lines & " บรรทัด</div></a>")
        Next
        If dt.Rows.Count = 0 Then sb.Append("<div class=""tw-px-5 tw-py-12 tw-text-center tw-text-sm tw-text-muted"">ไม่พบโค้ดที่ตรงกับเงื่อนไข</div>")
        ListHtml = sb.ToString()

        ' ช่องแสดงโค้ด
        If current Is Nothing Then
            ViewerHtml = "<div class=""tw-px-5 tw-py-24 tw-text-center tw-text-sm tw-text-muted"">เลือกฟังก์ชันทางซ้ายเพื่อดูโค้ด</div>"
        Else
            Dim docID As String = Convert.ToString(current("DocID"))
            Dim link As String
            If role = "3" Then
                link = "Admin_infor_See.aspx?DocID=" & docID
            ElseIf CInt(current("IsMine")) = 1 Then
                link = If(role = "2", "Doc_Student_Infor.aspx?DocID=", "MAS_Doc.aspx?DocID=") & docID
            Else
                link = "Doc_Infor_See.aspx?DocID=" & docID
            End If
            Dim code As String = Convert.ToString(current("Doc_List_Code")).Replace(vbCrLf, vbLf).Trim(ControlChars.Lf, ControlChars.Cr)
            Dim v As New StringBuilder()
            v.Append("<div class=""panel-head""><div class=""tw-min-w-0"">")
            v.Append("<h3 class=""panel-title tw-truncate"">" & H(current("Doc_Functions_Code")) & "</h3>")
            v.Append("<p class=""panel-sub tw-truncate"">" & H(CodeLib.Display(Convert.ToString(current("Doc_List_Type")))) & " · <a href=""" & link & """>" & H(current("Doc_Name")) & "</a>" &
                     If(IsDBNull(current("Doc_Date_Save")), "", " · " & CDate(current("Doc_Date_Save")).ToString("d MMM yyyy", New System.Globalization.CultureInfo("th-TH"))) & "</p></div>")
            v.Append("<button type=""button"" id=""copy-code"" class=""btn btn-secondary btn-sm tw-shrink-0""><i data-lucide=""copy"" class=""tw-mr-1 tw-h-4 tw-w-4""></i>คัดลอก</button></div>")
            v.Append("<pre class=""code-view""><code class=""language-" & CodeLib.Hljs(Convert.ToString(current("Doc_List_Type"))) & """>" & HttpUtility.HtmlEncode(code) & "</code></pre>")
            v.Append("<textarea id=""raw-code"" style=""display:none"">" & HttpUtility.HtmlEncode(code) & "</textarea>")
            ViewerHtml = v.ToString()
        End If
    End Sub

    Private Function H(v As Object) As String
        Return HttpUtility.HtmlEncode(Convert.ToString(v))
    End Function

End Class
