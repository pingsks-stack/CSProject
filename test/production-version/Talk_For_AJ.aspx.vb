Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Zulu.Data
Imports DevExpress.Web
Imports DevExpress.Web.Bootstrap
Imports System.Diagnostics

Partial Class production_version_Doc_Student
    Inherits System.Web.UI.Page









    ' หน้าแชทเก่า ย้ายไปใช้ Chat.aspx (หน้าเดียวสำหรับนิสิตและอาจารย์)
    Protected Sub RedirectToNewChat(sender As Object, e As EventArgs) Handles Me.PreInit
        Response.Redirect("Chat.aspx", True)
    End Sub
End Class
