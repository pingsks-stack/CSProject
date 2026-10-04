
Partial Class production_version_Chat
    Inherits System.Web.UI.Page

    Protected Subtitle As String

    ' แชทมีเฉพาะนิสิตกับอาจารย์ ใช้เมนูตามบทบาท
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Select Case Convert.ToString(Session("Urole"))
            Case "2" : MasterPageFile = "~/app/MasterPageTeacher.master"
            Case "1" : MasterPageFile = "~/app/MasterPageHome.master"
            Case Else : Response.Redirect("Login.aspx", True)
        End Select
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Subtitle = If(Convert.ToString(Session("Urole")) = "2", "คุยกับนิสิตในโครงงานที่คุณดูแล", "สอบถามอาจารย์ที่ปรึกษาและกรรมการ (★ = อาจารย์ในโครงงานของคุณ)")
    End Sub

End Class
