
Partial Class production_version_Login
    Inherits System.Web.UI.Page


    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim db = PgDb.Platform()
    'Dim Register = db.GetValue("insert into member (Name,ID_Student,Email,Password) VALUES (@Name,@ID_Student,@Email,@Password) /*TODO RETURNING*/",
    '           db.NewParam("Name", Studentname.Text),
    '           db.NewParam("ID_Student", Student_ID.Text),
    '           db.NewParam("Email", Email.Text),
    '           db.NewParam("Password", Password.Text))
    'Response.Redirect("Login.aspx")
    '    db.Close()
    'End Sub

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim db = PgDb.Platform()
    '    Dim saveinsert = db.GetValue("insert into Signin_Signup (Username,Password,Email,Name,Student_ID,Mobile,Urole) VALUES (@Username,@Password,@Email,@Name,@Student_ID,@Mobile,1) RETURNING UserID",
    '                db.NewParam("Username", inputUsername.Text),
    '                db.NewParam("Password", PasswordHasher.Hash(inputPassword.Text)),
    '                db.NewParam("Email", inputEmail.Text),
    '                db.NewParam("Name", inputName.Text),
    '                db.NewParam("Student_ID", inputStudent_ID.Text),
    '                db.NewParam("Mobile", inputMobile.Text))

    '    If (IsPostBack) Then
    '        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('สมัครสมาชิกสำเร็จ !');window.location.href='Login.aspx';", True)
    '    End If
    '    'Dim login = db.GetDataTable("select * from Signin_Signup where Username ='" + inputUsername.Text + "' and Password='" + inputPassword.Text + "'")
    '    'If login.Rows.Count > 0 Then

    '    '    Label1.Text = Session("Name")
    '    'End If
    'End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' validator ฝั่ง client ข้ามได้ ต้องเช็กซ้ำฝั่ง server
        If Not Page.IsValid Then Return

        Dim db = PgDb.Platform()

        ' ตรวจสอบการซ้ำของชื่อผู้ใช้งาน
        Dim usernameExists = db.GetValue("SELECT COUNT(*) FROM Signin_Signup WHERE Username = @Username",
                                         db.NewParam("Username", inputUsername.Text))
        If usernameExists > 0 Then
            ' แสดงแจ้งเตือนถ้าชื่อผู้ใช้งานซ้ำ
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ข้อมูลซ้ำ', text: 'ชื่อผู้ใช้งานนี้ถูกใช้งานแล้ว', icon: 'error', confirmButtonText: 'OK' });", True)
            Return
        End If

        ' ตรวจสอบการซ้ำของอีเมล
        Dim emailExists = db.GetValue("SELECT COUNT(*) FROM Signin_Signup WHERE Email = @Email",
                                      db.NewParam("Email", inputEmail.Text))
        If emailExists > 0 Then
            ' แสดงแจ้งเตือนถ้าอีเมลซ้ำ
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ข้อมูลซ้ำ', text: 'อีเมลนี้ถูกใช้งานแล้ว', icon: 'error', confirmButtonText: 'OK' });", True)
            Return
        End If

        ' ตรวจสอบการซ้ำของรหัสนิสิต
        Dim studentIdExists = db.GetValue("SELECT COUNT(*) FROM Signin_Signup WHERE Student_ID = @Student_ID",
                                          db.NewParam("Student_ID", inputStudent_ID.Text))
        If studentIdExists > 0 Then
            ' แสดงแจ้งเตือนถ้ารหัสนิสิตซ้ำ
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ข้อมูลซ้ำ', text: 'รหัสนิสิตนี้ถูกใช้งานแล้ว', icon: 'error', confirmButtonText: 'OK' });", True)
            Return
        End If

        ' ตรวจสอบการซ้ำของเบอร์โทรศัพท์
        Dim mobileExists = db.GetValue("SELECT COUNT(*) FROM Signin_Signup WHERE Mobile = @Mobile",
                                       db.NewParam("Mobile", inputMobile.Text))
        If mobileExists > 0 Then
            ' แสดงแจ้งเตือนถ้าเบอร์โทรศัพท์ซ้ำ
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ข้อมูลซ้ำ', text: 'เบอร์โทรศัพท์นี้ถูกใช้งานแล้ว', icon: 'error', confirmButtonText: 'OK' });", True)
            Return
        End If

        ' เพิ่มข้อมูลสมาชิกใหม่ลงในฐานข้อมูล
        Dim saveinsert = db.GetValue("INSERT INTO Signin_Signup (Username, Password, Email, Name, Student_ID, Mobile, Urole) VALUES (@Username, @Password, @Email, @Name, @Student_ID, @Mobile, 1) RETURNING UserID",
                                     db.NewParam("Username", inputUsername.Text),
                                     db.NewParam("Password", PasswordHasher.Hash(inputPassword.Text)),
                                     db.NewParam("Email", inputEmail.Text),
                                     db.NewParam("Name", inputName.Text),
                                     db.NewParam("Student_ID", inputStudent_ID.Text),
                                     db.NewParam("Mobile", inputMobile.Text))

        If (IsPostBack) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'สมัครสมาชิกสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Login.aspx'; });", True)
        End If
    End Sub


End Class
