Imports System.IO

' ตรวจสอบไฟล์ที่ผู้ใช้อัปโหลด ป้องกันการอัปโหลดไฟล์สคริปต์ (.aspx, .config ฯลฯ) ขึ้นเซิร์ฟเวอร์
Public Module UploadSecurity

    Private ReadOnly AllowedDocumentExtensions As String() = {
        ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx",
        ".txt", ".zip", ".rar", ".7z",
        ".jpg", ".jpeg", ".png", ".gif"
    }

    Public Function IsAllowedDocument(fileName As String) As Boolean
        If String.IsNullOrEmpty(fileName) Then Return False
        Dim ext As String = Path.GetExtension(fileName).ToLowerInvariant()
        Return Array.IndexOf(AllowedDocumentExtensions, ext) >= 0
    End Function

End Module
