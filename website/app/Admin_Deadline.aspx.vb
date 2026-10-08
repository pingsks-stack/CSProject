Imports System.Data
Imports System.Linq
Imports System.Text

' แอดมินกำหนดวันส่งเอกสารแต่ละบท ตามปีการศึกษา
Partial Class production_version_Admin_Deadline
    Inherits System.Web.UI.Page

    Protected ListHtml As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack Then Return
        For Each y In AcademicYears()
            ddlYear.Items.Add(y)
        Next
        ddlYear.SelectedValue = CurrentTerm()
        For Each ch In Chapters.List
            ddlChapter.Items.Add(ch)
        Next
    End Sub

    ' ภาคเรียนปัจจุบัน (เหมือนสูตรใน Insert.aspx.vb): มิ.ย.-ต.ค. = /1, พ.ย.-มี.ค. = /2, เม.ย.-พ.ค. = /3
    Public Shared Function CurrentTerm() As String
        Dim m As Integer = DateTime.Now.Month, y As Integer = DateTime.Now.Year
        If m >= 6 AndAlso m <= 10 Then Return (y + 543) & "/1"
        If m >= 11 Then Return (y + 543) & "/2"
        If m <= 3 Then Return (y + 542) & "/2"
        Return (y + 542) & "/3"
    End Function

    Private Function AcademicYears() As List(Of String)
        Dim years As New HashSet(Of String)
        For Each r As DataRow In Shell.Query("SELECT DISTINCT Class_Year_No FROM MAS_DOC WHERE COALESCE(Class_Year_No, '') <> '' UNION SELECT DISTINCT Class_Year_No FROM Deadline_TB").Rows
            years.Add(Convert.ToString(r(0)))
        Next
        ' ภาคปัจจุบันและภาคถัดไป
        Dim cur As String = CurrentTerm()
        years.Add(cur)
        Dim parts = cur.Split("/"c)
        Dim be As Integer = CInt(parts(0)), term As Integer = CInt(parts(1))
        years.Add(If(term < 3, be & "/" & (term + 1), (be + 1) & "/1"))
        Return years.OrderByDescending(Function(s) s).ToList()
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not Page.IsValid Then Return
        Dim due As DateTime
        If Not DateTime.TryParseExact(txtDue.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, due) Then
            Alert("วันที่ไม่ถูกต้อง", "กรุณาเลือกวันที่ใหม่", "warning") : Return
        End If
        Dim year As String = ddlYear.SelectedValue
        Dim chapter As String = ddlChapter.SelectedValue
        If Not Chapters.List.Contains(chapter) Then Return
        Dim updated = Shell.Exec("UPDATE Deadline_TB SET Due_Date = @Due, Note = @Note WHERE Class_Year_No = @Y AND Chapter = @C",
                                 Shell.P("@Due", due), Shell.P("@Note", txtNote.Text.Trim()), Shell.P("@Y", year), Shell.P("@C", chapter))
        If updated = 0 Then
            Shell.Exec("INSERT INTO Deadline_TB (Class_Year_No, Chapter, Due_Date, Note) VALUES (@Y, @C, @Due, @Note)",
                       Shell.P("@Y", year), Shell.P("@C", chapter), Shell.P("@Due", due), Shell.P("@Note", txtNote.Text.Trim()))
        End If
        txtNote.Text = ""
        Alert("บันทึกแล้ว", chapter & " ปีการศึกษา " & year & " กำหนดส่ง " & Chapters.ThaiDate(due), "success")
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim id As Integer
        If Integer.TryParse(hfDelete.Value, id) Then Shell.Exec("DELETE FROM Deadline_TB WHERE Deadline_ID = @ID", Shell.P("@ID", id))
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ' จำนวนโครงงานในปีนั้นที่ส่งบทนั้นแล้ว / ยังไม่ส่ง
        Dim dt = Shell.Query(
            "SELECT d.Deadline_ID, d.Class_Year_No, d.Chapter, d.Due_Date, d.Note, " &
            "(SELECT COUNT(*) FROM MAS_DOC m WHERE m.Class_Year_No = d.Class_Year_No) AS Projects, " &
            "(SELECT COUNT(DISTINCT s.DocID) FROM Doc_Submission s JOIN MAS_DOC m ON m.DocID = s.DocID WHERE m.Class_Year_No = d.Class_Year_No AND s.Chapter = d.Chapter) AS Submitted " &
            "FROM Deadline_TB d ORDER BY d.Class_Year_No DESC, d.Due_Date")
        Dim sb As New StringBuilder()
        If dt.Rows.Count = 0 Then
            sb.Append("<div class=""panel tw-p-12 tw-text-center tw-text-muted"">ยังไม่มีกำหนดส่ง เพิ่มได้จากแบบฟอร์มทางซ้าย</div>")
        End If
        For Each grp In dt.Rows.Cast(Of DataRow)().GroupBy(Function(r) Convert.ToString(r("Class_Year_No")))
            sb.Append("<div class=""panel tw-overflow-hidden""><div class=""panel-head""><h3 class=""panel-title"">ปีการศึกษา " & H(grp.Key) & "</h3><span class=""tw-text-sm tw-text-muted"">" & grp.Count() & " กำหนดส่ง</span></div>")
            sb.Append("<table class=""data-table""><thead><tr><th>บท</th><th>กำหนดส่ง</th><th>ส่งแล้ว</th><th>หมายเหตุ</th><th></th></tr></thead><tbody>")
            For Each r In grp
                Dim due As DateTime = CDate(r("Due_Date"))
                Dim projects As Integer = CInt(r("Projects")), submitted As Integer = CInt(r("Submitted"))
                Dim passed As Boolean = due.Date < DateTime.Today
                Dim days As Integer = CInt((due.Date - DateTime.Today).TotalDays)
                sb.Append("<tr><td class=""tw-font-semibold"">" & H(r("Chapter")) & "</td>")
                sb.Append("<td><div>" & Chapters.ThaiDate(due) & "</div><div class=""tw-text-xs " & If(passed, "tw-text-bad", "tw-text-muted") & """>" &
                          If(passed, "เลยมา " & -days & " วัน", If(days = 0, "วันนี้", "อีก " & days & " วัน")) & "</div></td>")
                sb.Append("<td>" & submitted & "/" & projects & If(passed AndAlso submitted < projects, " <span class=""badge-soft tw-bg-bad/15 tw-text-bad"">ค้าง " & (projects - submitted) & "</span>", "") & "</td>")
                sb.Append("<td class=""tw-text-muted"">" & H(r("Note")) & "</td>")
                sb.Append("<td class=""tw-text-right""><button type=""button"" class=""btn btn-danger btn-sm"" onclick=""delDeadline('" & r("Deadline_ID") & "','" & SafeOut.JsAttr(Convert.ToString(r("Chapter")) & " " & grp.Key) & "')"">ลบ</button></td></tr>")
            Next
            sb.Append("</tbody></table></div>")
        Next
        ListHtml = sb.ToString()
    End Sub

    Private Function H(v As Object) As String
        Return HttpUtility.HtmlEncode(Convert.ToString(v))
    End Function

    Private Sub Alert(title As String, text As String, icon As String)
        ClientScript.RegisterStartupScript(Me.GetType(), "alert",
            "Swal.fire({ title: '" & SafeOut.Js(title) & "', text: '" & SafeOut.Js(text) & "', icon: '" & icon & "', confirmButtonText: 'ตกลง' });", True)
    End Sub

End Class
