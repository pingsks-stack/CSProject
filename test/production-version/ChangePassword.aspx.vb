Imports System.Data

Partial Class production_version_ChangePassword
    Inherits System.Web.UI.Page

    ' ใช้เมนูตามบทบาทของผู้ใช้
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Select Case Convert.ToString(Session("Urole"))
            Case "3" : MasterPageFile = "~/production-version/MasterPageAdmin.master"
            Case "2" : MasterPageFile = "~/production-version/MasterPageTeacher.master"
            Case Else : MasterPageFile = "~/production-version/MasterPageHome.master"
        End Select
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        pnlAdmin.Visible = Convert.ToString(Session("Urole")) = "3"
    End Sub

    Protected Sub btnChange_Click(sender As Object, e As EventArgs) Handles btnChange.Click
        Page.Validate("mine")
        If Not Page.IsValid Then Return

        Dim userID = Session("UserID")
        Dim rows = Shell.Query("SELECT Password FROM Signin_Signup WHERE UserID = @UserID", Shell.P("@UserID", userID))
        If rows.Rows.Count <> 1 OrElse Not PasswordHasher.Verify(txtCurrent.Text, Convert.ToString(rows.Rows(0)("Password"))) Then
            Alert("รหัสผ่านไม่ถูกต้อง", "รหัสผ่านปัจจุบันไม่ถูกต้อง", "error")
            Return
        End If

        Shell.Exec("UPDATE Signin_Signup SET Password = @Password WHERE UserID = @UserID",
                    Shell.P("@Password", PasswordHasher.Hash(txtNew.Text)), Shell.P("@UserID", userID))
        Alert("สำเร็จ", "เปลี่ยนรหัสผ่านเรียบร้อยแล้ว", "success")
    End Sub

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If Convert.ToString(Session("Urole")) <> "3" Then Return
        Page.Validate("reset")
        If Not Page.IsValid Then Return

        Dim found = Dash.Count("SELECT COUNT(*) FROM Signin_Signup WHERE Username = @Username", Shell.P("@Username", txtResetUser.Text.Trim()))
        If found <> 1 Then
            Alert("ไม่พบผู้ใช้", "ไม่พบชื่อผู้ใช้นี้ในระบบ", "warning")
            Return
        End If
        Shell.Exec("UPDATE Signin_Signup SET Password = @Password WHERE Username = @Username",
                    Shell.P("@Password", PasswordHasher.Hash(txtResetPass.Text)), Shell.P("@Username", txtResetUser.Text.Trim()))
        Alert("สำเร็จ", "รีเซ็ตรหัสผ่านแล้ว แจ้งรหัสชั่วคราวให้ผู้ใช้และให้เปลี่ยนใหม่หลังเข้าสู่ระบบ", "success")
        txtResetUser.Text = ""
        txtResetPass.Text = ""
    End Sub

    Private Sub Alert(title As String, text As String, icon As String)
        ClientScript.RegisterStartupScript(Me.GetType(), "alert",
            "Swal.fire({ title: '" & SafeOut.Js(title) & "', text: '" & SafeOut.Js(text) & "', icon: '" & icon & "', confirmButtonText: 'ตกลง' });", True)
    End Sub

End Class
