
Partial Class production_version_For_Admin
    Inherits System.Web.UI.Page

    ' หน้าแรกเดิมของแอดมิน ย้ายไปใช้ dashboard_Admin.aspx
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Response.Redirect("dashboard_Admin.aspx", True)
    End Sub

End Class
