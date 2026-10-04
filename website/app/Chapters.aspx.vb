Imports System.Data
Imports System.IO
Imports System.Linq
Imports System.Text

' ส่งเอกสารรายบท + ประวัติเวอร์ชัน + ความเห็นอาจารย์ + กำหนดส่ง
Partial Class production_version_Chapters
    Inherits System.Web.UI.Page

    Protected DocName, YearNo, ChaptersHtml, SummaryHtml, BackUrl As String
    Private docID As Integer
    Private role, email As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Select Case Convert.ToString(Session("Urole"))
            Case "3" : MasterPageFile = "~/app/MasterPageAdmin.master"
            Case "2" : MasterPageFile = "~/app/MasterPageTeacher.master"
            Case Else : MasterPageFile = "~/app/MasterPageHome.master"
        End Select
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        role = Convert.ToString(Session("Urole"))
        email = Convert.ToString(Session("Email"))
        If Not Integer.TryParse(Request.QueryString("DocID"), docID) OrElse Not Chapters.CanView(docID, email, role) Then
            Response.Redirect(If(role = "2", "Doc_Student.aspx", If(role = "3", "Admin_infor.aspx", "Main_Doc.aspx")), True)
        End If
        Dim doc = Shell.Query("SELECT Doc_Name, Class_Year_No FROM MAS_DOC WHERE DocID = @D", Shell.P("@D", docID))
        DocName = Convert.ToString(doc.Rows(0)("Doc_Name"))
        YearNo = Convert.ToString(doc.Rows(0)("Class_Year_No"))
        BackUrl = If(role = "2", "Doc_Student.aspx", If(role = "3", "Admin_infor_See.aspx?DocID=" & docID, "Main_Doc.aspx"))

        pnlUpload.Visible = Chapters.CanUpload(docID, email, role)
        pnlReview.Visible = Chapters.CanReview(docID, email, role)
        If pnlUpload.Visible AndAlso Not IsPostBack Then
            For Each ch In Chapters.List
                ddlChapter.Items.Add(ch)
            Next
        End If
        ' นิสิตเปิดดูแล้ว = อ่านความเห็นแล้ว
        If role = "1" AndAlso Chapters.IsMember(docID, email) Then
            Shell.Exec("UPDATE r SET Is_Read = 1 FROM Doc_Review r JOIN Doc_Submission s ON s.Sub_ID = r.Sub_ID WHERE s.DocID = @D AND r.Is_Read = 0", Shell.P("@D", docID))
        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If Not Chapters.CanUpload(docID, email, role) Then Return
        Dim chapter As String = ddlChapter.SelectedValue
        If Not Chapters.List.Contains(chapter) Then Return
        If Not fuFile.HasFile Then Alert("ยังไม่ได้เลือกไฟล์", "กรุณาเลือกไฟล์ที่จะส่ง", "warning") : Return
        Dim name As String = Path.GetFileName(fuFile.FileName)
        If Not UploadSecurity.IsAllowedDocument(name) Then Alert("ไม่รองรับไฟล์นี้", "รองรับ PDF, Word, PowerPoint, Excel, ZIP/RAR และรูปภาพ", "error") : Return
        If fuFile.PostedFile.ContentLength > Chapters.MaxBytes Then Alert("ไฟล์ใหญ่เกินไป", "ไฟล์ต้องไม่เกิน 30 MB", "error") : Return

        Dim stored As String = Guid.NewGuid().ToString("N") & Path.GetExtension(name).ToLowerInvariant()
        fuFile.SaveAs(Path.Combine(Chapters.StorageFolder(docID), stored))
        Dim version As Integer = Dash.Count("SELECT ISNULL(MAX(Version), 0) + 1 FROM Doc_Submission WHERE DocID = @D AND Chapter = @C", Shell.P("@D", docID), Shell.P("@C", chapter))
        Shell.Exec("INSERT INTO Doc_Submission (DocID, Chapter, Version, File_Name, Stored_Name, File_Size, Note, Uploaded_By, Uploaded_Email) " &
                   "VALUES (@D, @C, @V, @F, @S, @Z, @N, @By, @E)",
                   Shell.P("@D", docID), Shell.P("@C", chapter), Shell.P("@V", version), Shell.P("@F", Left(name, 260)), Shell.P("@S", stored),
                   Shell.P("@Z", fuFile.PostedFile.ContentLength), Shell.P("@N", txtNote.Text.Trim()),
                   Shell.P("@By", Convert.ToString(Session("Name"))), Shell.P("@E", email))
        txtNote.Text = ""
        Alert("ส่งเอกสารแล้ว", chapter & " เวอร์ชัน " & version, "success")
    End Sub

    Protected Sub btnReview_Click(sender As Object, e As EventArgs) Handles btnReview.Click
        If Not Chapters.CanReview(docID, email, role) Then Return
        Dim subId As Integer
        If Not Integer.TryParse(hfSubId.Value, subId) Then Return
        Dim verdict As String = Request.Form("verdict")
        If verdict <> "ผ่าน" AndAlso verdict <> "แก้ไข" Then Return
        ' ให้ความเห็นได้เฉพาะเวอร์ชันล่าสุดของบทในโครงงานนี้
        Dim isLatest = Dash.Count(
            "SELECT COUNT(*) FROM Doc_Submission s WHERE s.Sub_ID = @S AND s.DocID = @D AND s.Version = (SELECT MAX(Version) FROM Doc_Submission x WHERE x.DocID = s.DocID AND x.Chapter = s.Chapter)",
            Shell.P("@S", subId), Shell.P("@D", docID))
        If isLatest = 0 Then Alert("ให้ความเห็นไม่ได้", "มีเวอร์ชันใหม่กว่านี้แล้ว", "warning") : Return
        Shell.Exec("INSERT INTO Doc_Review (Sub_ID, Reviewer_Email, Reviewer_Name, Verdict, Comment) VALUES (@S, @E, @N, @V, @C)",
                   Shell.P("@S", subId), Shell.P("@E", email), Shell.P("@N", Convert.ToString(Session("Name"))),
                   Shell.P("@V", verdict), Shell.P("@C", Left(txtComment.Text.Trim(), 1000)))
        txtComment.Text = ""
        hfSubId.Value = ""
        Alert("บันทึกความเห็นแล้ว", If(verdict = "ผ่าน", "ให้ผ่านแล้ว", "แจ้งให้นิสิตแก้ไขแล้ว"), "success")
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        Dim subs = Shell.Query("SELECT Sub_ID, Chapter, Version, File_Name, File_Size, Note, Uploaded_By, Uploaded_At FROM Doc_Submission WHERE DocID = @D ORDER BY Chapter, Version DESC", Shell.P("@D", docID))
        Dim reviews = Shell.Query("SELECT r.Sub_ID, r.Reviewer_Email, r.Reviewer_Name, r.Verdict, r.Comment, r.Created_At FROM Doc_Review r JOIN Doc_Submission s ON s.Sub_ID = r.Sub_ID WHERE s.DocID = @D ORDER BY r.Created_At DESC", Shell.P("@D", docID))
        Dim deadlines = Chapters.Deadlines(docID)
        Dim canReview As Boolean = pnlReview.Visible
        Dim canUpload As Boolean = pnlUpload.Visible
        Dim nPass As Integer = 0, nFix As Integer = 0, nWait As Integer = 0, nLate As Integer = 0

        Dim sb As New StringBuilder()
        For Each ch In Chapters.List
            Dim versions = subs.Rows.Cast(Of DataRow)().Where(Function(r) Convert.ToString(r("Chapter")) = ch).ToList()
            Dim due As DateTime? = Nothing
            If deadlines.ContainsKey(ch) Then due = deadlines(ch)
            Dim latest As DataRow = If(versions.Count > 0, versions(0), Nothing)
            Dim latestReviews = If(latest Is Nothing, New List(Of DataRow)(), reviews.Rows.Cast(Of DataRow)().Where(Function(r) CInt(r("Sub_ID")) = CInt(latest("Sub_ID"))).ToList())

            ' สถานะของบท
            Dim status As String, tone As String
            If latest Is Nothing Then
                If due.HasValue AndAlso due.Value.Date < DateTime.Today Then
                    status = "เลยกำหนด" : tone = "bad" : nLate += 1
                Else
                    status = "ยังไม่ส่ง" : tone = "muted"
                End If
            ElseIf latestReviews.Any(Function(r) Convert.ToString(r("Verdict")) = "แก้ไข") Then
                status = "ต้องแก้ไข" : tone = "warn" : nFix += 1
            ElseIf latestReviews.Count > 0 Then
                status = "ผ่าน (" & latestReviews.Count & ")" : tone = "ok" : nPass += 1
            Else
                status = "รอตรวจ" : tone = "info" : nWait += 1
            End If
            Dim badge As String = If(tone = "muted", "<span class=""badge-soft tw-bg-surface-2 tw-text-muted"">", "<span class=""badge-soft tw-bg-" & tone & "/15 tw-text-" & tone & """>") & H(status) & "</span>"

            sb.Append("<div class=""panel"">")
            sb.Append("<div class=""panel-head""><div class=""tw-flex tw-flex-wrap tw-items-center tw-gap-2""><h3 class=""panel-title"">" & H(ch) & "</h3>" & badge)
            If due.HasValue Then
                Dim overdue As Boolean = latest Is Nothing AndAlso due.Value.Date < DateTime.Today
                sb.Append("<span class=""tw-flex tw-items-center tw-gap-1 tw-text-xs " & If(overdue, "tw-text-bad", "tw-text-muted") & """><i data-lucide=""calendar-clock"" class=""tw-h-3.5 tw-w-3.5""></i>กำหนดส่ง " & Chapters.ThaiDate(due.Value) & "</span>")
            End If
            sb.Append("</div><div class=""tw-flex tw-gap-2"">")
            If canUpload Then sb.Append("<button type=""button"" class=""btn btn-secondary btn-sm"" onclick=""pickChapter('" & SafeOut.JsAttr(ch) & "')""><i data-lucide=""upload"" class=""tw-mr-1 tw-h-4 tw-w-4""></i>" & If(latest Is Nothing, "ส่ง", "ส่งเวอร์ชันใหม่") & "</button>")
            If canReview AndAlso latest IsNot Nothing Then sb.Append("<button type=""button"" class=""btn btn-primary btn-sm"" onclick=""startReview('" & latest("Sub_ID") & "','" & SafeOut.JsAttr(ch & " v" & Convert.ToString(latest("Version"))) & "')""><i data-lucide=""message-square-text"" class=""tw-mr-1 tw-h-4 tw-w-4""></i>ให้ความเห็น</button>")
            sb.Append("</div></div>")

            If versions.Count = 0 Then
                sb.Append("<div class=""tw-px-5 tw-py-6 tw-text-sm tw-text-muted"">ยังไม่มีไฟล์</div>")
            Else
                sb.Append("<div class=""tw-divide-y tw-divide-line"">")
                For Each v In versions
                    Dim isLatest As Boolean = v Is latest
                    Dim at As DateTime = CDate(v("Uploaded_At"))
                    Dim late As Boolean = due.HasValue AndAlso at.Date > due.Value.Date
                    sb.Append("<div class=""tw-px-5 tw-py-3" & If(isLatest, "", " tw-opacity-70") & """>")
                    sb.Append("<div class=""tw-flex tw-flex-wrap tw-items-center tw-justify-between tw-gap-2""><div class=""tw-flex tw-min-w-0 tw-items-center tw-gap-3"">")
                    sb.Append("<span class=""tw-shrink-0 tw-rounded-md tw-bg-accent/15 tw-px-2 tw-py-0.5 tw-text-xs tw-font-bold tw-text-accent"">v" & v("Version") & "</span>")
                    sb.Append("<a href=""Download.ashx?id=" & v("Sub_ID") & """ class=""tw-truncate tw-font-semibold"">" & H(v("File_Name")) & "</a>")
                    sb.Append("<span class=""tw-shrink-0 tw-text-xs tw-text-muted"">" & Chapters.FormatSize(CInt(v("File_Size"))) & "</span>")
                    If late Then sb.Append("<span class=""badge-soft tw-bg-bad/15 tw-text-bad"">ส่งช้า</span>")
                    sb.Append("</div><span class=""tw-text-xs tw-text-muted"">" & H(v("Uploaded_By")) & " · " & Chapters.ThaiDate(at, True) & "</span></div>")
                    If Convert.ToString(v("Note")) <> "" Then sb.Append("<div class=""tw-mt-1.5 tw-text-sm tw-text-muted"">📝 " & H(v("Note")) & "</div>")
                    For Each rv In reviews.Rows.Cast(Of DataRow)().Where(Function(r) CInt(r("Sub_ID")) = CInt(v("Sub_ID")))
                        Dim ok As Boolean = Convert.ToString(rv("Verdict")) = "ผ่าน"
                        sb.Append("<div class=""tw-mt-2 tw-flex tw-gap-2 tw-rounded-lg tw-border tw-border-line tw-bg-surface-2 tw-p-3 tw-text-sm"">")
                        sb.Append("<span class=""tw-mt-0.5 " & If(ok, "tw-text-ok", "tw-text-warn") & """><i data-lucide=""" & If(ok, "check-circle", "alert-circle") & """ class=""tw-h-4 tw-w-4""></i></span><div class=""tw-min-w-0"">")
                        sb.Append("<div class=""tw-font-semibold"">" & H(rv("Reviewer_Name")) & " · <span class=""" & If(ok, "tw-text-ok", "tw-text-warn") & """>" & If(ok, "ผ่าน", "ต้องแก้ไข") & "</span> <span class=""tw-text-xs tw-font-normal tw-text-muted"">" & Chapters.ThaiDate(CDate(rv("Created_At")), True) & "</span></div>")
                        If Convert.ToString(rv("Comment")) <> "" Then sb.Append("<div class=""tw-mt-0.5 tw-whitespace-pre-wrap tw-text-ink"">" & H(rv("Comment")) & "</div>")
                        sb.Append("</div></div>")
                    Next
                    sb.Append("</div>")
                Next
                sb.Append("</div>")
            End If
            sb.Append("</div>")
        Next
        ChaptersHtml = sb.ToString()
        SummaryHtml = Pill("ok", "ผ่าน", nPass) & Pill("warn", "ต้องแก้ไข", nFix) & Pill("info", "รอตรวจ", nWait) & If(nLate > 0, Pill("bad", "เลยกำหนด", nLate), "")
    End Sub

    Private Function Pill(tone As String, text As String, n As Integer) As String
        Return "<span class=""badge-soft tw-bg-" & tone & "/15 tw-px-3 tw-py-1 tw-text-sm tw-text-" & tone & """>" & HttpUtility.HtmlEncode(text) & " " & n & "</span>"
    End Function

    Private Function H(v As Object) As String
        Return HttpUtility.HtmlEncode(Convert.ToString(v))
    End Function

    Private Sub Alert(title As String, text As String, icon As String)
        ClientScript.RegisterStartupScript(Me.GetType(), "alert",
            "Swal.fire({ title: '" & SafeOut.Js(title) & "', text: '" & SafeOut.Js(text) & "', icon: '" & icon & "', confirmButtonText: 'ตกลง' });", True)
    End Sub

End Class
