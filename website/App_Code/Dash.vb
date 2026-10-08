Imports System.Data
Imports Npgsql
Imports System.Linq
Imports System.Text
Imports System.Web

' ตัวช่วยของหน้า dashboard: การ์ดตัวเลข, ป้ายสถานะ, ชุดข้อมูลรายเดือนสำหรับกราฟ และรายการกิจกรรม
Public Module Dash

    Private ReadOnly Th As New System.Globalization.CultureInfo("th-TH")

    Public Function Count(sql As String, ParamArray params As NpgsqlParameter()) As Integer
        Dim dt = Shell.Query(sql, params)
        If dt.Rows.Count = 0 OrElse IsDBNull(dt.Rows(0)(0)) Then Return 0
        Return Convert.ToInt32(dt.Rows(0)(0))
    End Function

    ' การ์ดตัวเลขแบบในธีม: ไอคอน + กราฟเส้นเล็ก + ค่า + คำอธิบาย
    ' tone: accent | info | ok | warn | bad
    Public Function Kpi(icon As String, tone As String, label As String, value As Object, note As String,
                        Optional sparkId As String = Nothing) As String
        Dim spark As String = If(sparkId Is Nothing, "", "<div id=""" & sparkId & """ class=""tw-h-10 tw-w-28 -tw-mr-2 -tw-mt-1""></div>")
        Return "<div class=""panel tw-p-5"">" &
               "<div class=""tw-flex tw-items-start tw-justify-between"">" &
               "<span class=""kpi-icon tw-bg-" & tone & "/15 tw-text-" & tone & """><i data-lucide=""" & icon & """></i></span>" & spark & "</div>" &
               "<div class=""tw-mt-3 tw-text-sm tw-text-muted"">" & HttpUtility.HtmlEncode(label) & "</div>" &
               "<div class=""tw-mt-1 tw-text-3xl tw-font-bold tw-text-ink"">" & HttpUtility.HtmlEncode(Convert.ToString(value)) & "</div>" &
               "<div class=""tw-mt-1 tw-text-xs tw-text-muted"">" & HttpUtility.HtmlEncode(note) & "</div>" &
               "</div>"
    End Function

    Public Function StatusBadge(status As Object) As String
        Dim s As String = Convert.ToString(status)
        Dim tone As String
        ' ต้องเช็ก "ยังไม่มีการกดผ่าน/ไม่ผ่าน" ก่อน เพราะข้อความนี้มีคำว่า "ไม่ผ่าน" อยู่ด้วย
        If s = "" OrElse s.StartsWith("ยังไม่มี") Then
            tone = "warn"
        ElseIf s.StartsWith("ผ่าน3/3") OrElse s = "ผ่าน" Then
            tone = "ok"
        ElseIf s.StartsWith("ผ่าน") Then
            tone = "info"
        ElseIf s.Contains("ไม่ผ่าน") Then
            tone = "bad"
        Else
            tone = "warn"
        End If
        If s = "" OrElse s.StartsWith("ยังไม่มี") Then s = "รอพิจารณา"
        Return "<span class=""badge-soft tw-bg-" & tone & "/15 tw-text-" & tone & """>" & HttpUtility.HtmlEncode(s) & "</span>"
    End Function

    ' ===== ชุดข้อมูลรายเดือน =====
    ' แต่ละ sql ต้องคืนคอลัมน์ D (datetime) — ระบบนับตามเดือน
    ' ใช้ 12 เดือนที่ลงท้ายด้วยเดือนล่าสุดที่มีข้อมูล (ข้อมูลเก่าจะยังแสดงเป็นกราฟได้)
    Public Function Monthly(sqls As String(), ParamArray params As NpgsqlParameter()) As MonthlyData
        Dim sets As New List(Of List(Of DateTime))
        Dim latest As DateTime = DateTime.MinValue
        For Each q As String In sqls
            Dim dates As New List(Of DateTime)
            For Each r As DataRow In Shell.Query(q, CloneParams(params)).Rows
                If Not IsDBNull(r("D")) Then
                    Dim d = CDate(r("D"))
                    dates.Add(d)
                    If d > latest Then latest = d
                End If
            Next
            sets.Add(dates)
        Next
        If latest = DateTime.MinValue Then latest = DateTime.Now

        Dim last As New DateTime(latest.Year, latest.Month, 1)
        Dim months = Enumerable.Range(0, 12).Select(Function(i) last.AddMonths(i - 11)).ToList()
        Dim result As New MonthlyData()
        result.Labels = "[" & String.Join(",", months.Select(Function(m) """" & m.ToString("MMM yy", Th) & """")) & "]"
        For Each dates In sets
            Dim counts = months.Select(Function(m) dates.Where(Function(d) d.Year = m.Year AndAlso d.Month = m.Month).Count())
            result.Series.Add("[" & String.Join(",", counts) & "]")
        Next
        Return result
    End Function

    Private Function CloneParams(ps As NpgsqlParameter()) As NpgsqlParameter()
        Return ps.Select(Function(p) PgDb.Param(p.ParameterName, p.Value)).ToArray()
    End Function

    Public Class MonthlyData
        Public Labels As String = "[]"
        Public Series As New List(Of String)
    End Class

    ' ชุดข้อมูลแบบชื่อ/ค่า (เช่น ตามประเภท) — sql คืนคอลัมน์ Name, N
    Public Function Pairs(sql As String, ParamArray params As NpgsqlParameter()) As String()
        Dim dt = Shell.Query(sql, params)
        Dim names = dt.Rows.Cast(Of DataRow)().Select(Function(r) """" & HttpUtility.JavaScriptStringEncode(Convert.ToString(r("Name"))) & """")
        Dim values = dt.Rows.Cast(Of DataRow)().Select(Function(r) Convert.ToString(Convert.ToInt32(r("N"))))
        Return {"[" & String.Join(",", names) & "]", "[" & String.Join(",", values) & "]"}
    End Function

    ' ===== รายการกิจกรรมล่าสุด ===== sql คืน Icon, Title, Detail, At
    Public Function Activity(sql As String, ParamArray params As NpgsqlParameter()) As String
        Dim dt = Shell.Query(sql, params)
        If dt.Rows.Count = 0 Then Return "<div class=""tw-px-5 tw-py-10 tw-text-center tw-text-sm tw-text-muted"">ยังไม่มีกิจกรรม</div>"
        Dim sb As New StringBuilder()
        For Each r As DataRow In dt.Rows
            sb.Append("<div class=""tw-flex tw-gap-3 tw-border-b tw-border-line tw-px-5 tw-py-3 last:tw-border-b-0"">")
            sb.Append("<span class=""kpi-icon tw-bg-accent/15 tw-text-accent""><i data-lucide=""" & HttpUtility.HtmlAttributeEncode(Convert.ToString(r("Icon"))) & """></i></span>")
            sb.Append("<div class=""tw-min-w-0 tw-flex-1""><div class=""tw-truncate tw-text-sm tw-font-semibold tw-text-ink"">" & HttpUtility.HtmlEncode(Convert.ToString(r("Title"))) & "</div>")
            sb.Append("<div class=""tw-truncate tw-text-xs tw-text-muted"">" & HttpUtility.HtmlEncode(Convert.ToString(r("Detail"))) & "</div></div>")
            sb.Append("<div class=""tw-shrink-0 tw-text-xs tw-text-muted"">" & HttpUtility.HtmlEncode(Shell.TimeAgo(r("At"))) & "</div></div>")
        Next
        Return sb.ToString()
    End Function

    ' ข้อความ SQL ของกิจกรรมโครงงาน (log เปลี่ยนชื่อ/สร้าง + log ไฟล์) จำกัดด้วยเงื่อนไข scope ของ DocID
    Public Function ActivitySql(docScope As String, top As Integer) As String
        Return "SELECT * FROM (" &
               " SELECT CASE WHEN l.Action IN ('สร้าง', 'Create') THEN 'folder-plus' ELSE 'pencil-line' END AS Icon," &
               "        CASE WHEN l.Action IN ('สร้าง', 'Create') THEN 'สร้างโครงงาน' ELSE 'แก้ไขชื่อโครงงาน' END AS Title," &
               "        COALESCE(l.Doc_Name, '') || ' · ' || COALESCE(l.Post_Name, '') AS Detail, l.Doc_dt_Save AS At" &
               " FROM MAS_DOC_LOG l WHERE " & docScope.Replace("{DocID}", "l.DocID") &
               " UNION ALL" &
               " SELECT CASE WHEN f.Action IN ('Delete', 'ลบไฟล์', 'ลบ') THEN 'trash-2' WHEN f.Action = 'Update' OR f.Action LIKE 'แทน%' THEN 'refresh-cw' ELSE 'file-up' END," &
               "        CASE WHEN f.Action IN ('Delete', 'ลบไฟล์', 'ลบ') THEN 'ลบไฟล์' WHEN f.Action = 'Update' OR f.Action LIKE 'แทน%' THEN 'แทนที่ไฟล์' ELSE 'อัปโหลดไฟล์' END," &
               "        COALESCE(f.Doc_File, '') || ' · ' || COALESCE(f.Post_Name, ''), f.Date_Delete" &
               " FROM Doc_File_Log f WHERE " & docScope.Replace("{DocID}", "f.DocID") &
               ") x WHERE At IS NOT NULL ORDER BY At DESC LIMIT " & top
    End Function

End Module
