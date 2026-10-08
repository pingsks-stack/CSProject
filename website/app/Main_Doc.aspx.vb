Imports System.Data
Imports System.Text

' โครงงานของฉัน: การ์ดต่อโครงงานพร้อมแถบความคืบหน้า 5 ขั้น
Partial Class production_version_Main_Doc
    Inherits System.Web.UI.Page

    Protected CardsHtml As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim email As String = Convert.ToString(Session("Email"))
        Dim dt = Shell.Query(
            "SELECT m.DocID, m.Doc_Name, m.Doc_NameEN, m.Doc_Type, m.Submit_TC, m.Class_Year_No, m.Name_teacherPJ, " &
            "(SELECT COUNT(*) FROM Permission_TB t WHERE t.DocID = m.DocID AND t.Urole = '2') AS Teachers, " &
            "(SELECT COUNT(*) FROM SubAdd_AJ_TB s WHERE s.DocID = m.DocID AND s.Status = 'รอการยืนยัน') AS Invites, " &
            "(SELECT COUNT(*) FROM Doc_File f WHERE f.DocID = m.DocID) AS Files, " &
            "(SELECT COUNT(DISTINCT s.Chapter) FROM Doc_Submission s WHERE s.DocID = m.DocID) AS Chapters, " &
            "(SELECT COUNT(*) FROM Doc_List_TB c WHERE c.DocID = m.DocID) AS Codes " &
            "FROM MAS_DOC m WHERE m.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email) ORDER BY m.Doc_dt_Save DESC",
            Shell.P("@Email", email))

        Dim sb As New StringBuilder()
        For Each r As DataRow In dt.Rows
            Dim id As String = Convert.ToString(r("DocID"))
            Dim status As String = Convert.ToString(r("Submit_TC"))
            Dim teachers As Integer = CInt(r("Teachers"))
            Dim files As Integer = CInt(r("Files"))
            Dim codes As Integer = CInt(r("Codes"))
            Dim passed As Boolean = status = "ผ่าน3/3"
            Dim failed As Boolean = status = "ไม่ผ่าน"

            sb.Append("<div class=""panel tw-p-5"">")
            ' หัวการ์ด
            sb.Append("<div class=""tw-flex tw-flex-wrap tw-items-start tw-justify-between tw-gap-3"">")
            sb.Append("<div class=""tw-min-w-0""><div class=""tw-mb-2 tw-flex tw-flex-wrap tw-gap-2"">" & Dash.StatusBadge(status))
            sb.Append("<span class=""badge-soft tw-bg-gold/15 tw-text-gold"">" & H(If(Convert.ToString(r("Doc_Type")) = "", "ยังไม่ระบุประเภท", r("Doc_Type"))) & "</span>")
            If Convert.ToString(r("Class_Year_No")) <> "" Then sb.Append("<span class=""badge-soft tw-bg-accent/15 tw-text-accent"">ปีการศึกษา " & H(r("Class_Year_No")) & "</span>")
            sb.Append("</div><h3 class=""tw-m-0 tw-text-lg tw-font-semibold tw-text-ink"">" & H(r("Doc_Name")) & "</h3>")
            sb.Append("<div class=""tw-text-sm tw-text-muted"">" & H(r("Doc_NameEN")) & "</div></div>")
            ' ปุ่มลัด
            sb.Append("<div class=""tw-flex tw-flex-wrap tw-gap-2"">")
            If passed Then
                sb.Append("<a href=""Infor_Pass_N.aspx?DocID=" & id & """ class=""btn btn-success btn-sm""><i data-lucide=""printer"" class=""tw-mr-1 tw-h-4 tw-w-4""></i>พิมพ์ใบยืนยัน</a>")
            ElseIf failed Then
                sb.Append("<a href=""Sent_PJ.aspx?DocID=" & id & """ class=""btn btn-warning btn-sm""><i data-lucide=""rotate-ccw"" class=""tw-mr-1 tw-h-4 tw-w-4""></i>ส่งโครงงานใหม่</a>")
            End If
            sb.Append("<a href=""Chapters.aspx?DocID=" & id & """ class=""btn btn-secondary btn-sm""><i data-lucide=""files"" class=""tw-mr-1 tw-h-4 tw-w-4""></i>เอกสารรายบท</a>")
            sb.Append("<a href=""CodeEdit.aspx?DocID=" & id & """ class=""btn btn-secondary btn-sm""><i data-lucide=""file-code"" class=""tw-mr-1 tw-h-4 tw-w-4""></i>ซอร์สโค้ด</a>")
            sb.Append("<a href=""MAS_Doc.aspx?DocID=" & id & """ class=""btn btn-primary btn-sm""><i data-lucide=""settings-2"" class=""tw-mr-1 tw-h-4 tw-w-4""></i>จัดการโครงงาน</a>")
            sb.Append("</div></div>")

            ' แถบความคืบหน้า
            sb.Append("<div class=""tw-mt-5 tw-grid tw-gap-3 sm:tw-grid-cols-5"">")
            sb.Append(Stepper("folder-plus", "สร้างโครงงาน", "เรียบร้อย", True))
            sb.Append(Stepper("graduation-cap", "อาจารย์ " & teachers & "/3",
                              If(CInt(r("Invites")) > 0, "รอตอบรับ " & r("Invites") & " คำเชิญ", If(teachers >= 3, "ครบแล้ว", "เพิ่มได้ที่หน้าจัดการ")), teachers >= 3))
            Dim nChapters As Integer = CInt(r("Chapters"))
            sb.Append(Stepper("file-up", "เอกสาร " & nChapters & "/" & Chapters.List.Length & " บท", If(nChapters > 0, "ส่งแล้ว " & nChapters & " บท", If(files > 0, "มีไฟล์เดิม " & files & " ไฟล์", "ยังไม่ส่ง")), nChapters >= Chapters.List.Length))
            sb.Append(Stepper("code-xml", "ซอร์สโค้ด " & codes & " ฟังก์ชัน", If(codes > 0, "เพิ่มแล้ว", "ยังไม่มีโค้ด"), codes > 0))
            sb.Append(Stepper("badge-check", "ผลการพิจารณา", If(passed, "ผ่านครบ 3/3", If(failed, "ไม่ผ่าน", "รออาจารย์พิจารณา")), passed))
            sb.Append("</div></div>")
        Next
        If dt.Rows.Count = 0 Then
            sb.Append("<div class=""panel tw-p-12 tw-text-center""><div class=""tw-mx-auto tw-mb-3 tw-flex tw-h-12 tw-w-12 tw-items-center tw-justify-center tw-rounded-full tw-bg-accent/15 tw-text-accent""><i data-lucide=""folder-plus""></i></div>")
            sb.Append("<div class=""tw-font-semibold tw-text-ink"">ยังไม่มีโครงงาน</div><div class=""tw-mt-1 tw-text-sm tw-text-muted"">เริ่มจากสร้างโครงงานแรกของคุณ</div>")
            sb.Append("<a href=""Insert.aspx"" class=""btn-accent tw-mt-4""><i data-lucide=""file-plus""></i>เพิ่มโครงงาน</a></div>")
        End If
        CardsHtml = sb.ToString()
    End Sub

    Private Function Stepper(icon As String, title As String, note As String, done As Boolean) As String
        Return "<div class=""tw-flex tw-items-center tw-gap-3 tw-rounded-xl tw-border tw-border-line tw-p-3" & If(done, " tw-bg-ok/5", "") & """>" &
               "<span class=""kpi-icon " & If(done, "tw-bg-ok/15 tw-text-ok", "tw-bg-surface-2 tw-text-muted") & """><i data-lucide=""" & If(done, "check", icon) & """></i></span>" &
               "<div class=""tw-min-w-0""><div class=""tw-truncate tw-text-sm tw-font-semibold tw-text-ink"">" & HttpUtility.HtmlEncode(title) & "</div>" &
               "<div class=""tw-truncate tw-text-xs tw-text-muted"">" & HttpUtility.HtmlEncode(note) & "</div></div></div>"
    End Function

    Private Function H(v As Object) As String
        Return HttpUtility.HtmlEncode(Convert.ToString(v))
    End Function

End Class
