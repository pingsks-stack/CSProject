Imports System.Linq

' ภาษาของซอร์สโค้ดในคลัง (Doc_List_TB.Doc_List_Type)
' ข้อมูลเดิมเขียนชื่อภาษาไม่ตรงกัน (เช่น vbnet / VB.NET) จึงรวมให้เป็นชื่อเดียว
Public Module CodeLib

    ' ค่าที่บันทึก, ชื่อที่แสดง, class ของ highlight.js
    Public ReadOnly Languages As String(,) = {
        {"python", "Python", "python"},
        {"java", "Java", "java"},
        {"C#", "C#", "csharp"},
        {"vbnet", "VB.NET", "vbnet"},
        {"cpp", "C/C++", "cpp"},
        {"javascript", "JavaScript", "javascript"},
        {"typescript", "TypeScript", "typescript"},
        {"PHP", "PHP", "php"},
        {"sql", "SQL", "sql"},
        {"html", "HTML", "xml"},
        {"css", "CSS", "css"},
        {"dart", "Dart", "dart"},
        {"kotlin", "Kotlin", "kotlin"},
        {"swift", "Swift", "swift"},
        {"other", "อื่น ๆ", "plaintext"}
    }

    Private Function Index(lang As String) As Integer
        Dim key As String = Normalize(lang)
        For i As Integer = 0 To Languages.GetLength(0) - 1
            If String.Equals(Languages(i, 0), key, StringComparison.OrdinalIgnoreCase) Then Return i
        Next
        Return -1
    End Function

    Public Function Normalize(lang As String) As String
        Dim s As String = If(lang, "").Trim()
        Select Case s.ToLowerInvariant()
            Case "vb.net", "vb", "vbnet" : Return "vbnet"
            Case "c#", "csharp", "cs" : Return "C#"
            Case "c++", "cpp", "c" : Return "cpp"
            Case "js", "javascript" : Return "javascript"
            Case "ts", "typescript" : Return "typescript"
            Case "" : Return "other"
        End Select
        Return s
    End Function

    Public Function Display(lang As String) As String
        Dim i = Index(lang)
        Return If(i >= 0, Languages(i, 1), If(String.IsNullOrEmpty(lang), "อื่น ๆ", lang))
    End Function

    Public Function Hljs(lang As String) As String
        Dim i = Index(lang)
        Return If(i >= 0, Languages(i, 2), "plaintext")
    End Function

    ' ค่าที่อาจเก็บไว้ในฐานข้อมูลสำหรับภาษาเดียวกัน ใช้ตอนกรอง
    Public Function Aliases(lang As String) As String()
        Select Case Normalize(lang)
            Case "vbnet" : Return {"vbnet", "VB.NET", "vb"}
            Case "C#" : Return {"C#", "csharp", "cs"}
            Case "cpp" : Return {"cpp", "c++", "c"}
            Case "javascript" : Return {"javascript", "js"}
            Case "typescript" : Return {"typescript", "ts"}
            Case "other" : Return {"other", ""}
        End Select
        Return {Normalize(lang)}
    End Function

End Module
