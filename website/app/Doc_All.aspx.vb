Imports System.Data
Imports Npgsql
Imports System.Text

' คลังโครงงาน (โครงงานที่ผ่าน 3/3) ใช้ได้ทุกบทบาท
Partial Class production_version_Doc_All
    Inherits System.Web.UI.Page

    Protected Q, TypeOptions, YearOptions, ClassOptions, CardsHtml As String
    Protected ResultCount As Integer

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Select Case Convert.ToString(Session("Urole"))
            Case "3" : MasterPageFile = "~/app/MasterPageAdmin.master"
            Case "2" : MasterPageFile = "~/app/MasterPageTeacher.master"
            Case Else : MasterPageFile = "~/app/MasterPageHome.master"
        End Select
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim role As String = Convert.ToString(Session("Urole"))
        Q = If(Request.QueryString("q"), "").Trim()
        Dim docType As String = If(Request.QueryString("type"), "")
        Dim year As String = If(Request.QueryString("year"), "")
        Dim cls As String = If(Request.QueryString("class"), "")

        ' ตัวเลือกกรอง ดึงจากข้อมูลจริง
        Dim sb As New StringBuilder(Opt("", "ทุกประเภท", docType))
        For Each r As DataRow In Shell.Query("SELECT Doc_Type FROM DocTypeTB ORDER BY CASE WHEN Doc_Type = 'Other' THEN 1 ELSE 0 END, TypeID").Rows
            sb.Append(Opt(Convert.ToString(r(0)), Convert.ToString(r(0)), docType))
        Next
        TypeOptions = sb.ToString()
        sb = New StringBuilder(Opt("", "ทุกปีการศึกษา", year))
        For Each r As DataRow In Shell.Query("SELECT DISTINCT Class_Year_No FROM MAS_DOC WHERE Submit_TC = 'ผ่าน3/3' AND COALESCE(Class_Year_No, '') <> '' ORDER BY Class_Year_No DESC").Rows
            sb.Append(Opt(Convert.ToString(r(0)), "ปีการศึกษา " & Convert.ToString(r(0)), year))
        Next
        YearOptions = sb.ToString()
        sb = New StringBuilder(Opt("", "ทุกชั้นปี", cls))
        For i As Integer = 1 To 8
            sb.Append(Opt(i.ToString(), "ชั้นปีที่ " & i, cls))
        Next
        ClassOptions = sb.ToString()

        Dim conds As New List(Of String) From {"m.Submit_TC = 'ผ่าน3/3'"}
        Dim ps As New List(Of NpgsqlParameter)
        If Q <> "" Then
            conds.Add("(m.Doc_Name LIKE @Q OR m.Doc_NameEN LIKE @Q OR m.Student_Name LIKE @Q OR m.Student_Name2 LIKE @Q OR m.Name_teacherPJ LIKE @Q)")
            ps.Add(Shell.P("@Q", "%" & Q.Replace("\", "\\").Replace("%", "\%").Replace("_", "\_") & "%"))
        End If
        If docType <> "" Then conds.Add("m.Doc_Type = @Type") : ps.Add(Shell.P("@Type", docType))
        If year <> "" Then conds.Add("m.Class_Year_No = @Year") : ps.Add(Shell.P("@Year", year))
        If cls <> "" Then conds.Add("m.Class_No = @Class") : ps.Add(Shell.P("@Class", cls))

        Dim dt = Shell.Query(
            "SELECT m.DocID, m.Doc_Name, m.Doc_NameEN, m.Student_Name, m.Student_Name2, m.Name_teacherPJ, m.Doc_Type, m.Class_Year_No, m.Class_No, " &
            "(SELECT COUNT(*) FROM Doc_File f WHERE f.DocID = m.DocID) AS Files, (SELECT COUNT(*) FROM Doc_List_TB c WHERE c.DocID = m.DocID) AS Codes " &
            "FROM MAS_DOC m WHERE " & String.Join(" AND ", conds) & " ORDER BY m.Doc_dt_Save DESC", ps.ToArray())
        ResultCount = dt.Rows.Count

        Dim detail As String = If(role = "3", "Admin_infor_See.aspx?DocID=", If(role = "2", "Doc_Infor_SeeAJ.aspx?DocID=", "Doc_Infor_See.aspx?DocID="))
        sb = New StringBuilder()
        For Each r As DataRow In dt.Rows
            Dim students As String = Convert.ToString(r("Student_Name")).Trim() &
                If(Convert.ToString(r("Student_Name2")).Trim() <> "", ", " & Convert.ToString(r("Student_Name2")).Trim(), "")
            sb.Append("<div class=""panel tw-flex tw-flex-col tw-p-5"">")
            sb.Append("<div class=""tw-mb-3 tw-flex tw-flex-wrap tw-gap-2"">")
            sb.Append("<span class=""badge-soft tw-bg-gold/15 tw-text-gold"">" & H(If(Convert.ToString(r("Doc_Type")) = "", "ไม่ระบุประเภท", r("Doc_Type"))) & "</span>")
            If Convert.ToString(r("Class_Year_No")) <> "" Then sb.Append("<span class=""badge-soft tw-bg-accent/15 tw-text-accent"">ปีการศึกษา " & H(r("Class_Year_No")) & "</span>")
            sb.Append("</div>")
            sb.Append("<h3 class=""tw-m-0 tw-text-base tw-font-semibold tw-leading-snug tw-text-ink"">" & H(r("Doc_Name")) & "</h3>")
            sb.Append("<div class=""tw-mt-0.5 tw-text-sm tw-text-muted"">" & H(r("Doc_NameEN")) & "</div>")
            sb.Append("<div class=""tw-mt-4 tw-space-y-1.5 tw-text-sm"">")
            sb.Append("<div class=""tw-flex tw-gap-2 tw-text-muted""><i data-lucide=""users"" class=""tw-mt-0.5 tw-h-4 tw-w-4 tw-shrink-0""></i><span class=""tw-text-ink"">" & HttpUtility.HtmlEncode(If(students = "", "-", students)) & "</span></div>")
            sb.Append("<div class=""tw-flex tw-gap-2 tw-text-muted""><i data-lucide=""graduation-cap"" class=""tw-mt-0.5 tw-h-4 tw-w-4 tw-shrink-0""></i><span class=""tw-text-ink"">" & H(If(Convert.ToString(r("Name_teacherPJ")) = "", "-", r("Name_teacherPJ"))) & "</span></div>")
            sb.Append("</div>")
            sb.Append("<div class=""tw-mt-auto tw-flex tw-items-center tw-justify-between tw-gap-3 tw-border-t tw-border-line tw-pt-4"" style=""margin-top:1.25rem"">")
            sb.Append("<div class=""tw-flex tw-gap-4 tw-text-xs tw-text-muted""><span class=""tw-flex tw-items-center tw-gap-1""><i data-lucide=""file-text"" class=""tw-h-3.5 tw-w-3.5""></i>" & r("Files") & " ไฟล์</span>")
            sb.Append("<span class=""tw-flex tw-items-center tw-gap-1""><i data-lucide=""code-xml"" class=""tw-h-3.5 tw-w-3.5""></i>" & r("Codes") & " ฟังก์ชัน</span></div>")
            sb.Append("<a href=""" & detail & r("DocID") & """ class=""tw-flex tw-items-center tw-gap-1 tw-text-sm tw-font-semibold"">ดูรายละเอียด <i data-lucide=""arrow-right"" class=""tw-h-4 tw-w-4""></i></a>")
            sb.Append("</div></div>")
        Next
        If dt.Rows.Count = 0 Then
            sb.Append("<div class=""panel tw-col-span-full tw-p-12 tw-text-center tw-text-muted"">ไม่พบโครงงานที่ตรงกับเงื่อนไข</div>")
        End If
        CardsHtml = sb.ToString()
    End Sub

    Private Function H(v As Object) As String
        Return HttpUtility.HtmlEncode(Convert.ToString(v))
    End Function

    Private Function Opt(value As String, text As String, current As String) As String
        Return "<option value=""" & HttpUtility.HtmlAttributeEncode(value) & """" & If(value = current, " selected", "") & ">" & HttpUtility.HtmlEncode(text) & "</option>"
    End Function

End Class
