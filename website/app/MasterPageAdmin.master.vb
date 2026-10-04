Imports System.Data.SqlClient

Partial Class production_version_MasterPageAdmin
    Inherits System.Web.UI.MasterPage

    ' Init ของ master ทำงานก่อน Page_Load และก่อน event ของปุ่ม จึงตรวจสิทธิ์ที่นี่
    Private Sub production_version_MasterPageAdmin_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Session("UserID") Is Nothing OrElse Convert.ToString(Session("Urole")) <> "3" Then
            Response.Redirect("Login.aspx", True)
        End If
    End Sub

    Private Sub production_version_MasterPageAdmin_Load(sender As Object, e As EventArgs) Handles Me.Load
        ASPxLabel1.Text = Convert.ToString(Session("Name"))
        Shell.FillShell(Me)
    End Sub
End Class
