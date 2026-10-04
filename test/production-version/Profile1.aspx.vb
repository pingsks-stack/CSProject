
Partial Class production_version_Profile
    Inherits System.Web.UI.Page


    ' โปรไฟล์ของอาจารย์ รวมไปใช้ Profile.aspx
    Protected Sub RedirectToProfile(sender As Object, e As EventArgs) Handles Me.PreInit
        Response.Redirect("Profile.aspx", True)
    End Sub
End Class
