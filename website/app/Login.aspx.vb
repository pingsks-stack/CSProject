Imports System.Data
Imports Npgsql
Partial Class production_version_Login
    Inherits System.Web.UI.Page

    Dim con As New NpgsqlConnection
    Dim cmd As New NpgsqlCommand






    ' เมนู "ออกจากระบบ" ทุกหน้าลิงก์มาที่ Login.aspx จึงล้าง session ที่นี่
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session.Clear()
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim db = PgDb.Platform()
        Dim login = db.GetDataTable("SELECT * FROM Signin_Signup WHERE Username = @Username",
                                    db.NewParam("Username", inputEmailAddress.Text))

        ' ตรวจรหัสผ่านด้วย hash (บัญชีเก่าที่ยังเป็นข้อความธรรมดาจะถูกแปลงเป็น hash ทันทีที่ล็อกอินสำเร็จ)
        Dim ok As Boolean = login.Rows.Count = 1 AndAlso
            PasswordHasher.Verify(inputChoosePassword.Text, Convert.ToString(login.Rows(0)("Password")))
        If ok AndAlso Not PasswordHasher.IsHashed(Convert.ToString(login.Rows(0)("Password"))) Then
            db.Execute("UPDATE Signin_Signup SET Password = @Password WHERE UserID = @UserID",
                       db.NewParam("Password", PasswordHasher.Hash(inputChoosePassword.Text)),
                       db.NewParam("UserID", login.Rows(0)("UserID")))
        End If

        If ok Then
            Session.Clear()
            Session("UserID") = login.Rows(0).Item("UserID")
            Session("Username") = login.Rows(0).Item("Username")
            Session("Email") = login.Rows(0).Item("Email")
            Session("Name") = login.Rows(0).Item("Name")
            Session("Urole") = login.Rows(0).Item("Urole")
            Session("Mobile") = login.Rows(0).Item("Mobile")
            Session("Student_ID") = login.Rows(0).Item("Student_ID")

            'Label1.Text = Session("Name")

            If login.Rows(0)("Urole") = "3" Then
                Response.Redirect("dashboard_Admin.aspx")
            ElseIf login.Rows(0)("Urole") = "2" Then
                Response.Redirect("dashboard_Teacher.aspx")
            Else
                Response.Redirect("dashboard.aspx")
            End If
        Else
            'Label1.Text = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง"

            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'เกิดข้อผิดพลาด', text: 'ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง', icon: 'error', confirmButtonText: 'OK' });", True)
        End If
    End Sub



End Class
