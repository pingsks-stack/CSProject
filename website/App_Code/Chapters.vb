Imports System.Data
Imports System.Linq
Imports System.IO
Imports System.Web

' ส่งเอกสารแยกบท (Doc_Submission) + ความเห็นอาจารย์ (Doc_Review) + กำหนดส่ง (Deadline_TB)
' ไฟล์เก็บใน App_Data\submissions\<DocID>\ ซึ่ง IIS ไม่เปิดให้ดาวน์โหลดตรง ต้องผ่าน Download.ashx ที่ตรวจสิทธิ์
Public Module Chapters

    Public ReadOnly List As String() = {"บทที่ 1", "บทที่ 2", "บทที่ 3", "บทที่ 4", "บทที่ 5", "ภาคผนวก", "เล่มสมบูรณ์"}

    Public Const MaxBytes As Integer = 30 * 1024 * 1024

    Public Function StorageFolder(docID As Integer) As String
        Dim dir As String = HttpContext.Current.Server.MapPath("~/App_Data/submissions/" & docID)
        Directory.CreateDirectory(dir)
        Return dir
    End Function

    Public Function IsMember(docID As Integer, email As String) As Boolean
        Return Dash.Count("SELECT COUNT(*) FROM Permission_TB WHERE DocID = @D AND Email = @E", Shell.P("@D", docID), Shell.P("@E", email)) > 0
    End Function

    ' ดูไฟล์ได้: แอดมิน, สมาชิกโครงงาน, หรือทุกคนถ้าโครงงานผ่าน 3/3 (อยู่ในคลังโครงงาน)
    Public Function CanView(docID As Integer, email As String, role As String) As Boolean
        If role = "3" OrElse IsMember(docID, email) Then Return True
        Return Dash.Count("SELECT COUNT(*) FROM MAS_DOC WHERE DocID = @D AND Submit_TC = 'ผ่าน3/3'", Shell.P("@D", docID)) > 0
    End Function

    ' ส่งไฟล์ได้: นิสิตที่เป็นสมาชิกโครงงาน
    Public Function CanUpload(docID As Integer, email As String, role As String) As Boolean
        Return role = "1" AndAlso IsMember(docID, email)
    End Function

    ' ให้ความเห็นได้: อาจารย์ที่อยู่ในโครงงาน
    Public Function CanReview(docID As Integer, email As String, role As String) As Boolean
        Return role = "2" AndAlso Dash.Count("SELECT COUNT(*) FROM Permission_TB WHERE DocID = @D AND Email = @E AND Urole = '2'",
                                             Shell.P("@D", docID), Shell.P("@E", email)) > 0
    End Function

    ' กำหนดส่งของโครงงาน (ตามปีการศึกษาของโครงงาน)
    Public Function Deadlines(docID As Integer) As Dictionary(Of String, DateTime)
        Dim result As New Dictionary(Of String, DateTime)
        For Each r As DataRow In Shell.Query(
            "SELECT d.Chapter, d.Due_Date FROM Deadline_TB d JOIN MAS_DOC m ON m.Class_Year_No = d.Class_Year_No WHERE m.DocID = @D",
            Shell.P("@D", docID)).Rows
            result(Convert.ToString(r("Chapter"))) = CDate(r("Due_Date"))
        Next
        Return result
    End Function

    ' แผงงานเลยกำหนด (บทที่เลยวันส่งแล้วแต่ยังไม่มีไฟล์) สำหรับ dashboard
    ' docScope เช่น "1 = 1" หรือ "m.DocID IN (...)" ; linkPrefix คือหน้าที่จะเปิดเมื่อคลิก
    Public Function OverduePanel(docScope As String, linkPrefix As String, ParamArray params As Npgsql.NpgsqlParameter()) As String
        Dim dt = Shell.Query(
            "SELECT m.DocID, m.Doc_Name, d.Chapter, d.Due_Date, (CURRENT_DATE - d.Due_Date::date) AS LateDays " &
            "FROM MAS_DOC m JOIN Deadline_TB d ON d.Class_Year_No = m.Class_Year_No " &
            "WHERE " & docScope & " AND d.Due_Date < CURRENT_DATE " &
            "AND NOT EXISTS (SELECT 1 FROM Doc_Submission s WHERE s.DocID = m.DocID AND s.Chapter = d.Chapter) " &
            "ORDER BY d.Due_Date" & " LIMIT 8", params)
        Dim total As Integer = Dash.Count(
            "SELECT COUNT(*) FROM MAS_DOC m JOIN Deadline_TB d ON d.Class_Year_No = m.Class_Year_No " &
            "WHERE " & docScope & " AND d.Due_Date < CURRENT_DATE " &
            "AND NOT EXISTS (SELECT 1 FROM Doc_Submission s WHERE s.DocID = m.DocID AND s.Chapter = d.Chapter)",
            params.Select(Function(p) PgDb.Param(p.ParameterName, p.Value)).ToArray())
        If dt.Rows.Count = 0 Then Return ""
        Dim sb As New System.Text.StringBuilder()
        sb.Append("<div class=""panel tw-mt-4 tw-overflow-hidden""><div class=""panel-head""><div><h3 class=""panel-title"">งานเลยกำหนด</h3><p class=""panel-sub tw-text-bad"">")
        sb.Append(total & " รายการที่เลยกำหนดส่งแล้วยังไม่ส่ง</p></div></div><div class=""tw-grid md:tw-grid-cols-2"">")
        For Each r As DataRow In dt.Rows
            sb.Append("<a href=""" & linkPrefix & r("DocID") & """ class=""tw-flex tw-items-center tw-gap-3 tw-border-b tw-border-line tw-px-5 tw-py-3 tw-no-underline hover:tw-bg-surface-2 hover:tw-no-underline"">")
            sb.Append("<span class=""kpi-icon tw-bg-bad/15 tw-text-bad""><i data-lucide=""alarm-clock""></i></span>")
            sb.Append("<div class=""tw-min-w-0 tw-flex-1""><div class=""tw-truncate tw-font-semibold tw-text-ink"">" & HttpUtility.HtmlEncode(Convert.ToString(r("Doc_Name"))) & "</div>")
            sb.Append("<div class=""tw-text-xs tw-text-muted"">" & HttpUtility.HtmlEncode(Convert.ToString(r("Chapter"))) & " · กำหนด " & ThaiDate(CDate(r("Due_Date"))) & "</div></div>")
            sb.Append("<span class=""badge-soft tw-bg-bad/15 tw-text-bad"">เลย " & r("LateDays") & " วัน</span></a>")
        Next
        sb.Append("</div></div>")
        Return sb.ToString()
    End Function

    Public Function FormatSize(bytes As Integer) As String
        If bytes >= 1024 * 1024 Then Return (bytes / 1024.0 / 1024.0).ToString("0.0") & " MB"
        Return Math.Max(1, CInt(Math.Round(bytes / 1024.0))) & " KB"
    End Function

    Public Function ThaiDate(d As DateTime, Optional withTime As Boolean = False) As String
        Return d.ToString(If(withTime, "d MMM yy HH:mm", "d MMM yy"), New System.Globalization.CultureInfo("th-TH"))
    End Function

End Module
