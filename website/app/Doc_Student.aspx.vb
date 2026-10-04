Imports System.Data
Imports System.Text

' โครงงานที่อาจารย์รับผิดชอบ (การ์ด + ตัวกรองสถานะ)
Partial Class production_version_Doc_Student
    Inherits System.Web.UI.Page

    Protected CardsHtml, FilterHtml As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim email As String = Convert.ToString(Session("Email"))
        Dim show As String = If(Request.QueryString("show"), "")

        Dim dt = Shell.Query(
            "SELECT m.DocID, m.Doc_Name, m.Doc_NameEN, m.Student_Name, m.Student_Name2, m.Doc_Type, m.Submit_TC, m.Class_Year_No, " &
            "p.Urole_TC, p.Submit_TC AS MySubmit, p.Permission_ID, " &
            "(SELECT COUNT(*) FROM Doc_File f WHERE f.DocID = m.DocID) AS Files, " &
            "(SELECT COUNT(*) FROM Doc_List_TB c WHERE c.DocID = m.DocID) AS Codes " &
            "FROM Permission_TB p JOIN MAS_DOC m ON m.DocID = p.DocID " &
            "WHERE p.Email = @Email AND p.Urole = N'2' ORDER BY CASE WHEN p.Submit_TC IS NULL THEN 0 ELSE 1 END, m.Doc_dt_Save DESC",
            Shell.P("@Email", email))

        Dim waiting As Integer = 0, approved As Integer = 0
        For Each r As DataRow In dt.Rows
            If Convert.ToString(r("MySubmit")) = "ผ่าน" Then approved += 1 Else waiting += 1
        Next
        FilterHtml = Chip("", "ทั้งหมด", dt.Rows.Count, show) & Chip("waiting", "รอคุณพิจารณา", waiting, show) & Chip("approved", "คุณกดผ่านแล้ว", approved, show)

        Dim sb As New StringBuilder()
        Dim shown As Integer = 0
        For Each r As DataRow In dt.Rows
            Dim mine As Boolean = Convert.ToString(r("MySubmit")) = "ผ่าน"
            If show = "waiting" AndAlso mine Then Continue For
            If show = "approved" AndAlso Not mine Then Continue For
            shown += 1
            Dim role As String = Convert.ToString(r("Urole_TC"))
            Dim students As String = Convert.ToString(r("Student_Name")).Trim() &
                If(Convert.ToString(r("Student_Name2")).Trim() <> "", ", " & Convert.ToString(r("Student_Name2")).Trim(), "")
            Dim url As String = "Doc_Student_Infor.aspx?DocID=" & r("DocID") & "&uRole=" & HttpUtility.UrlEncode(role)

            sb.Append("<div class=""panel tw-flex tw-flex-col tw-p-5"">")
            sb.Append("<div class=""tw-mb-2 tw-flex tw-flex-wrap tw-gap-2"">" & Dash.StatusBadge(r("Submit_TC")))
            sb.Append("<span class=""badge-soft tw-bg-accent/15 tw-text-accent"">" & H(If(role = "", "อาจารย์", role)) & "</span>")
            sb.Append("<span class=""badge-soft tw-bg-gold/15 tw-text-gold"">" & H(If(Convert.ToString(r("Doc_Type")) = "", "ไม่ระบุประเภท", r("Doc_Type"))) & "</span></div>")
            sb.Append("<h3 class=""tw-m-0 tw-text-base tw-font-semibold tw-text-ink"">" & H(r("Doc_Name")) & "</h3>")
            sb.Append("<div class=""tw-text-sm tw-text-muted"">" & H(r("Doc_NameEN")) & "</div>")
            sb.Append("<div class=""tw-mt-3 tw-flex tw-gap-2 tw-text-sm tw-text-muted""><i data-lucide=""users"" class=""tw-mt-0.5 tw-h-4 tw-w-4 tw-shrink-0""></i><span class=""tw-text-ink"">" & HttpUtility.HtmlEncode(If(students = "", "-", students)) & "</span></div>")
            sb.Append("<div class=""tw-mt-4 tw-flex tw-items-center tw-justify-between tw-gap-3 tw-border-t tw-border-line tw-pt-4"">")
            sb.Append("<div class=""tw-flex tw-flex-wrap tw-items-center tw-gap-4 tw-text-xs tw-text-muted"">")
            sb.Append("<span class=""tw-flex tw-items-center tw-gap-1""><i data-lucide=""file-text"" class=""tw-h-3.5 tw-w-3.5""></i>" & r("Files") & " ไฟล์</span>")
            sb.Append("<a href=""Code.aspx?doc=" & r("DocID") & """ class=""tw-flex tw-items-center tw-gap-1""><i data-lucide=""code-xml"" class=""tw-h-3.5 tw-w-3.5""></i>" & r("Codes") & " ฟังก์ชัน</a>")
            sb.Append(If(mine, "<span class=""tw-flex tw-items-center tw-gap-1 tw-text-ok""><i data-lucide=""check"" class=""tw-h-3.5 tw-w-3.5""></i>คุณกดผ่านแล้ว</span>",
                                "<span class=""tw-flex tw-items-center tw-gap-1 tw-text-warn""><i data-lucide=""clock"" class=""tw-h-3.5 tw-w-3.5""></i>รอคุณพิจารณา</span>"))
            If Convert.ToString(r("Submit_TC")) = "ผ่าน3/3" Then sb.Append("</div><div class=""tw-flex tw-shrink-0 tw-gap-2""><a href=""Pass_AJ.aspx?DocID=" & r("DocID") & "&Permission_ID=" & r("Permission_ID") & """ class=""btn btn-success btn-sm"">พิมพ์ใบยืนยัน</a>") Else sb.Append("</div><div class=""tw-flex tw-shrink-0 tw-gap-2"">")
            sb.Append("<a href=""Chapters.aspx?DocID=" & r("DocID") & """ class=""btn btn-secondary btn-sm"">ตรวจเอกสาร</a><a href=""" & url & """ class=""btn btn-primary btn-sm"">พิจารณา/จัดการ</a></div></div></div>")
        Next
        If shown = 0 Then
            sb.Append("<div class=""panel tw-col-span-full tw-p-12 tw-text-center tw-text-muted"">" &
                      If(dt.Rows.Count = 0, "ยังไม่มีโครงงานที่คุณรับผิดชอบ · คำเชิญจากนิสิตจะอยู่ที่เมนู <a href=""Confirm_NS.aspx"">คำขอรอยืนยัน</a>", "ไม่มีโครงงานในหมวดนี้") & "</div>")
        End If
        CardsHtml = sb.ToString()
    End Sub

    Private Function Chip(value As String, text As String, count As Integer, current As String) As String
        Dim active As Boolean = value = current
        Return "<a href=""Doc_Student.aspx" & If(value = "", "", "?show=" & value) & """ class=""tw-flex tw-items-center tw-gap-2 tw-rounded-full tw-border tw-px-3 tw-py-1.5 tw-text-sm tw-no-underline hover:tw-no-underline " &
               If(active, "tw-border-accent tw-bg-accent/15 tw-text-accent", "tw-border-line tw-text-muted hover:tw-text-ink") & """>" &
               HttpUtility.HtmlEncode(text) & "<span class=""tw-rounded-full tw-bg-surface-2 tw-px-1.5 tw-text-xs"">" & count & "</span></a>"
    End Function

    Private Function H(v As Object) As String
        Return HttpUtility.HtmlEncode(Convert.ToString(v))
    End Function

End Class
