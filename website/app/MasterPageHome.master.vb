Imports Npgsql

Partial Class production_version_MasterPageHome
    Inherits System.Web.UI.MasterPage

    ' Init ของ master ทำงานก่อน Page_Load และก่อน event ของปุ่ม จึงตรวจสิทธิ์ที่นี่
    Private Sub production_version_MasterPageHome_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Session("UserID") Is Nothing Then
            Response.Redirect("Login.aspx", True)
        End If
        DocAccess.Enforce(Context)
    End Sub

    Private Sub production_version_MasterPageHome_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label1.Text = HttpUtility.HtmlEncode(Convert.ToString(Session("Name")))
        Shell.FillShell(Me)
    End Sub
End Class
