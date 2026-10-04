
Partial Class production_version_infor_user
    Inherits System.Web.UI.Page


    ' หน้าผู้ใช้เดิม ย้ายไปใช้ Admin_Users.aspx
    Protected Sub RedirectToUsers(sender As Object, e As EventArgs) Handles Me.PreInit
        Response.Redirect("Admin_Users.aspx", True)
    End Sub
End Class
