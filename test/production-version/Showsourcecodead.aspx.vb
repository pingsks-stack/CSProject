
Partial Class production_version_Showsourcecodead
    Inherits System.Web.UI.Page


    ' หน้าซอร์สโค้ดเก่า ย้ายไปใช้ Code.aspx / CodeEdit.aspx
    Protected Sub RedirectToNewCode(sender As Object, e As EventArgs) Handles Me.PreInit
        Response.Redirect("Code.aspx?doc=" & HttpUtility.UrlEncode(If(Request.QueryString("DocID"), "")), True)
    End Sub
End Class
