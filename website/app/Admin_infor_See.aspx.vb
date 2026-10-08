Imports System.Data
Imports Npgsql
Imports System.IO
Imports DevExpress.Web
Partial Class production_version_Admin_infor
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim docID As String = Request.QueryString("DocID")

        End If
    End Sub


    Protected Sub cb_load_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cb_load.Callback


    End Sub

    Protected Sub cb_load7_Callback(sender As Object, e As CallbackEventArgsBase) Handles ASPxCallbackPanel6.Callback
        BootstrapGridView4.DataBind()
    End Sub


    Protected Sub cb_load6_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles ASPxCallbackPanel5.Callback
        BootstrapGridView3.DataBind()
    End Sub

    Protected Sub cb_load15_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles ASPxCallbackPanel3.Callback
        GridView4.DataBind()
    End Sub




    Private Function GetDocFileByID(ByVal docFileID As Integer) As String
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Dim docFile As String = ""

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()
            Using command As New NpgsqlCommand("SELECT Doc_File FROM Doc_File WHERE Doc_File_ID = @Doc_File_ID", connection)
                command.Parameters.Add(PgDb.Param("@Doc_File_ID", docFileID))
                Dim reader As NpgsqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    docFile = reader("Doc_File").ToString()
                End If
            End Using
        End Using

        Return docFile
    End Function






    Protected Sub cbload_Load(source As Object, e As CallbackEventArgs) Handles cbload.Callback
        Dim db = PgDb.Platform()
        Dim fullname = db.GetValue("SELECT Name FROM Signin_Signup WHERE (Email = @Email)",
                               db.NewParam("Email", BootstrapTextBoxQR.Text))

        e.Result = fullname

        db.Close()
    End Sub

    Protected Sub ASPxCallback1_Callback(source As Object, e As CallbackEventArgs) Handles ASPxCallback1.Callback
        Dim db = PgDb.Platform()
        Dim fullname = db.GetValue("SELECT Student_ID FROM Signin_Signup WHERE (Name = @Name)",
                               db.NewParam("Name", txtname.Text))

        e.Result = fullname

        db.Close()
    End Sub

    Protected Sub ASPxCallback2_Callback(source As Object, e As CallbackEventArgs) Handles ASPxCallback2.Callback
        Dim db = PgDb.Platform()
        Dim fullname = db.GetValue("SELECT Mobile FROM Signin_Signup WHERE (Student_ID = @Student_ID)",
                               db.NewParam("Student_ID", txturole.Text))

        e.Result = fullname

        db.Close()
    End Sub

    Protected Sub cb_load4_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles ASPxCallbackPanel1.Callback
        'BootstrapGridView2.DataBind()
    End Sub


    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            Dim docID As String = Request.QueryString("docID")


            Dim checkQuery As String = "SELECT COUNT(*) FROM Permission_TB WHERE DocID = @DocID AND Urole = '1'"
            Dim count As Integer

            Using checkCommand As New NpgsqlCommand(checkQuery, connection)
                checkCommand.Parameters.Add(PgDb.Param("@DocID", docID))
                count = Convert.ToInt32(checkCommand.ExecuteScalar())
            End Using

            If count >= 2 Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'แจ้งเตือน', text: 'คุณได้เพิ่มคู่โปรเจคแล้ว!', icon: 'warning', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
            Else
                Dim insertQuery As String = "INSERT INTO Permission_TB (DocID, Name, Email, Mobile, Student_ID, Urole) VALUES (@DocID, @Name, @Email, @Mobile, @Student_ID, '1') RETURNING Permission_ID"

                Using insertCommand As New NpgsqlCommand(insertQuery, connection)
                    insertCommand.Parameters.Add(PgDb.Param("@DocID", docID))
                    insertCommand.Parameters.Add(PgDb.Param("@Email", BootstrapTextBoxQR.Text))
                    insertCommand.Parameters.Add(PgDb.Param("@Name", txtname.Text))
                    insertCommand.Parameters.Add(PgDb.Param("@Mobile", txtMobile.Text))
                    insertCommand.Parameters.Add(PgDb.Param("@Student_ID", txturole.Text))

                    Dim insertedPermissionID As Integer = Convert.ToInt32(insertCommand.ExecuteScalar())
                End Using


                Dim updateQuery As String = "UPDATE MAS_DOC SET Email2=@Email2, Student_Name2 = @Student_Name2, Student_ID2=@Student_ID2, Mobile2=@Mobile2 WHERE DocID = @DocID"

                Using updateCommand As New NpgsqlCommand(updateQuery, connection)
                    updateCommand.Parameters.Add(PgDb.Param("@Email2", BootstrapTextBoxQR.Text))
                    updateCommand.Parameters.Add(PgDb.Param("@Student_Name2", txtname.Text))
                    updateCommand.Parameters.Add(PgDb.Param("@Student_ID2", txturole.Text))
                    updateCommand.Parameters.Add(PgDb.Param("@Mobile2", txtMobile.Text))
                    updateCommand.Parameters.Add(PgDb.Param("@DocID", docID))

                    updateCommand.ExecuteNonQuery()
                End Using


                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'เพิ่มข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
            End If
        End Using
    End Sub


    Protected Sub cb_load11_Callback(sender As Object, e As CallbackEventArgsBase) Handles ASPxCallbackPanel8.Callback

    End Sub

    Protected Sub ASPxCallback3_Callback(source As Object, e As CallbackEventArgs) Handles ASPxCallback3.Callback
        Dim db = PgDb.Platform()
        Dim fullname = db.GetValue("SELECT Name FROM Signin_Signup WHERE (Email = @Email)",
                                   db.NewParam("Email", EmailAJ.Text))

        e.Result = fullname

        db.Close()
    End Sub



    Protected Sub ASPxCallback4_Callback(source As Object, e As CallbackEventArgs) Handles ASPxCallback4.Callback
        Dim db = PgDb.Platform()
        Dim fullname = db.GetValue("SELECT Mobile FROM Signin_Signup WHERE (Name = @Name)",
                               db.NewParam("Name", NameAJ.Text))

        e.Result = fullname

        db.Close()
    End Sub

    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim db = PgDb.Platform()
        Dim docID As String = Request.QueryString("docID")
        Dim uroleValue As String = GetUroleValueFromSigninSignup(EmailAJ.Text) ' Assuming you have a function to retrieve the Urole value from the Signin_Signup table

        If uroleValue <> "2" Then
            ' Urole is not equal to 2, do not insert
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ไม่สามารถเพิ่มข้อมูล', text: 'เนื่องจากอีเมลล์นี้ไม่ใช่อาจารย์!', icon: 'warning', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
            Return
        End If

        ' ตรวจสอบว่ามี DocID และ Email ที่ต้องการเพิ่มอยู่ในฐานข้อมูลหรือไม่
        Dim existingPermissionTable = db.GetDataTable("SELECT * FROM Permission_TB WHERE DocID = @DocID AND Email = @Email",
                                               db.NewParam("DocID", docID),
                                               db.NewParam("Email", EmailAJ.Text))

        If existingPermissionTable IsNot Nothing AndAlso existingPermissionTable.Rows.Count > 0 Then
            Dim existingPermission = existingPermissionTable.Rows(0)
            ' ข้อมูลซ้ำ ไม่ทำการเพิ่มข้อมูล
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ข้อมูลซ้ำ', text: 'ไม่สามารถเพิ่มข้อมูลได้เนื่องจากเอกสารนี้มีอีเมลล์นี้อยู่แล้ว!', icon: 'warning', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
            Return
        End If

        Dim teacherPermissionCount = db.GetValue("SELECT COUNT(*) FROM Permission_TB WHERE DocID = @DocID AND Urole = '2'",
                                             db.NewParam("DocID", docID))

        If teacherPermissionCount >= 3 Then
            ' มี Email ที่ Urole_Permis เป็น "Teacher" มากกว่าหรือเท่ากับ 3 รายการแล้ว ไม่ทำการเพิ่มข้อมูล
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ข้อมูลเกิน', text: 'ไม่สามารถเพิ่มข้อมูลเนื่องจากมีข้อมูลอาจารย์ครบแล้ว!', icon: 'warning', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
            Return
        End If

        ' ตรวจสอบว่าเป็น "อาจารย์ที่ปรึกษา" และมี Email เดียวเท่านั้น
        If BootstrapComboBox1.Text = "ที่ปรึกษา" Then
            Dim advisorPermissionCount = db.GetValue("SELECT COUNT(*) FROM Permission_TB WHERE DocID = @DocID AND Urole_TC = 'ที่ปรึกษา'",
                                              db.NewParam("DocID", docID))

            If advisorPermissionCount = 0 Then
                ' อัปเดตชื่อของอาจารย์ที่ Urole_TC = ที่ปรึกษาในตาราง MAS_DOC
                Dim updateTeacherNameQuery As String = "UPDATE MAS_DOC SET Name_teacherPJ = @Name_teacherPJ WHERE DocID = @DocID"
                db.Execute(updateTeacherNameQuery, db.NewParam("Name_teacherPJ", NameAJ.Text), db.NewParam("DocID", docID))
            ElseIf advisorPermissionCount > 0 Then
                ' มีการเพิ่มข้อมูลที่ปรึกษาอยู่แล้ว ไม่ทำการเพิ่มข้อมูล
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'เพิ่มข้อมูลไม่สำเร็จ', text: 'ไม่สามารถเพิ่มข้อมูลเนื่องจาก อาจารย์ที่ปรึกษาสามารถมีได้แค่คนเดียวเท่านั้น!', icon: 'warning', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
                Return
            End If
        End If


        ' เพิ่มข้อมูลในกรณีที่ไม่มีการเพิ่มข้อมูลที่ปรึกษาอยู่แล้ว
        Dim savePermission = db.Execute("INSERT INTO Permission_TB (DocID, Name, Email, Mobile, Urole, Urole_TC) VALUES (@DocID, @Name, @Email, @Mobile,'2', @Urole_TC) RETURNING Permission_ID",
                                        db.NewParam("DocID", docID),
                                        db.NewParam("Name", NameAJ.Text),
                                        db.NewParam("Email", EmailAJ.Text),
                                        db.NewParam("Mobile", PhonenumberAJ.Text),
                                        db.NewParam("Urole_TC", BootstrapComboBox1.Text))




        db.Close()

        If (IsPostBack) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'เพิ่มข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
        End If

        ' เคลียร์ค่าใน TextBox       
        NameAJ.Text = ""
        EmailAJ.Text = ""
        PhonenumberAJ.Text = ""
        BootstrapComboBox1.Text = ""

    End Sub

    Private Function GetUroleValueFromSigninSignup(email As String) As String
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Dim uroleValue As String = ""

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            Using command As New NpgsqlCommand("SELECT Urole FROM Signin_Signup WHERE Email = @Email", connection)
                command.Parameters.Add(PgDb.Param("@Email", email))

                Using reader As NpgsqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        uroleValue = reader("Urole").ToString()
                    End If
                End Using
            End Using
        End Using

        Return uroleValue
    End Function





End Class
