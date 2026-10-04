<%@ Application Language="VB" %>

<script runat="server">

    ' โฟลเดอร์หน้าเว็บเปลี่ยนชื่อจาก production-version เป็น app (ต.ค. 2569)
    ' ลิงก์/บุ๊กมาร์กเก่าจะถูกพาไปที่อยู่ใหม่ ส่วนหน้า / เปล่า ๆ พาไปหน้าเข้าสู่ระบบ
    Sub Application_BeginRequest(sender As Object, e As EventArgs)
        Dim path As String = Request.Path
        Const OldPrefix As String = "/production-version"
        If path.StartsWith(OldPrefix, StringComparison.OrdinalIgnoreCase) Then
            Dim rest As String = path.Substring(OldPrefix.Length)
            ' ส่งฟอร์มจากหน้าเก่าไม่ได้ (ViewState ของหน้าเก่า) จึงพาไปเปิดหน้านั้นใหม่แบบ GET
            Dim target As String = "/app" & If(rest = "" OrElse rest = "/", "/Login.aspx", rest) & Request.Url.Query
            Response.RedirectPermanent(target, True)
        ElseIf path = "/" OrElse path.Equals("/app", StringComparison.OrdinalIgnoreCase) OrElse path.Equals("/app/", StringComparison.OrdinalIgnoreCase) Then
            Response.Redirect("/app/Login.aspx", True)
        End If
    End Sub

</script>
