Imports System.Data.SqlClient

Partial Class production_version_MasterPageTeacher
    Inherits System.Web.UI.MasterPage

    ' Init ของ master ทำงานก่อน Page_Load และก่อน event ของปุ่ม จึงตรวจสิทธิ์ที่นี่
    Private Sub production_version_MasterPageTeacher_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim urole As String = Convert.ToString(Session("Urole"))
        If Session("UserID") Is Nothing OrElse (urole <> "2" AndAlso urole <> "3") Then
            Response.Redirect("Login.aspx", True)
        End If
        DocAccess.Enforce(Context)
    End Sub

    Private Sub production_version_MasterPageTeacher_Load(sender As Object, e As EventArgs) Handles Me.Load
        ASPxLabel1.Text = Convert.ToString(Session("Name"))
        Shell.FillShell(Me)
    End Sub
End Class
