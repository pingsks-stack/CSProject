<%@ WebHandler Language="VB" Class="DownloadSubmission" %>

Imports System.IO
Imports System.Web
Imports System.Web.SessionState

' ดาวน์โหลดไฟล์เอกสารรายบท (Doc_Submission) หลังตรวจสิทธิ์
Public Class DownloadSubmission
    Implements IHttpHandler, IRequiresSessionState

    Public ReadOnly Property IsReusable As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Public Sub ProcessRequest(ctx As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim id As Integer
        If ctx.Session("UserID") Is Nothing Then ctx.Response.Redirect("Login.aspx", True) : Return
        If Not Integer.TryParse(ctx.Request.QueryString("id"), id) Then ctx.Response.StatusCode = 400 : Return

        Dim dt = Shell.Query("SELECT DocID, File_Name, Stored_Name FROM Doc_Submission WHERE Sub_ID = @ID", Shell.P("@ID", id))
        If dt.Rows.Count = 0 Then ctx.Response.StatusCode = 404 : Return
        Dim docID As Integer = CInt(dt.Rows(0)("DocID"))
        If Not Chapters.CanView(docID, Convert.ToString(ctx.Session("Email")), Convert.ToString(ctx.Session("Urole"))) Then
            ctx.Response.StatusCode = 403 : Return
        End If

        ' Stored_Name สร้างโดยระบบ (GUID + นามสกุล) แต่ตัด path ทิ้งอีกชั้นกันไว้
        Dim path As String = System.IO.Path.Combine(Chapters.StorageFolder(docID), System.IO.Path.GetFileName(Convert.ToString(dt.Rows(0)("Stored_Name"))))
        If Not File.Exists(path) Then ctx.Response.StatusCode = 404 : Return

        Dim name As String = Convert.ToString(dt.Rows(0)("File_Name"))
        ctx.Response.ContentType = MimeMapping.GetMimeMapping(name)
        ctx.Response.AddHeader("Content-Disposition", "attachment; filename*=UTF-8''" & Uri.EscapeDataString(name))
        ctx.Response.AddHeader("X-Content-Type-Options", "nosniff")
        ctx.Response.TransmitFile(path)
    End Sub

End Class
