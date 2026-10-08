
Imports Npgsql
Imports System.IO
Imports DevExpress.Web

Partial Class production_version_EditUploadfile
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim docID As String = Request.QueryString("DocID")
            Dim permissionID As String = Request.QueryString("Permission_ID")

        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            Dim docID As String = Request.QueryString("DocID")
            Dim permissionID As String = Request.QueryString("Permission_ID")

            Dim masSelectQuery As String = "SELECT Name,Email,Mobile,Student_ID FROM Permission_TB WHERE Permission_ID = @Permission_ID"

            Using masSelectCommand As New NpgsqlCommand(masSelectQuery, connection)
                masSelectCommand.Parameters.Add(PgDb.Param("@Permission_ID", permissionID))
                Using reader As NpgsqlDataReader = masSelectCommand.ExecuteReader()
                    If reader.Read() Then
                        Dim name As String = reader("Name").ToString()
                        Dim emailLast As String = reader("Email").ToString()
                        Dim MobileLast As String = reader("Mobile").ToString()
                        Dim IDLast As String = reader("Student_ID").ToString()

                        reader.Close() ' ปิด DataReader ก่อนใช้ ExecuteNonQuery()

                        Dim subInsertQuery As String = "INSERT INTO Status_Change_TB (DocID, Permission_ID, Last_Post_Name, Name, Email, Mobile, Student_ID, Urole, Status, Post_Name,Last_Email,Last_Mobile,Last_StudentID,Status_Change,Status_Change1,Date_Send_NS,Action) VALUES (@DocID, @Permission_ID, @Last_Post_Name, @Name, @Email, @Mobile, @Student_ID, '1', 'รอการยืนยัน', @Post_Name,@Last_Email,@Last_Mobile,@Last_StudentID,'0','0',NOW(),'ขอเปลี่ยนคู่โปรเจค') RETURNING Status_ID"

                        Using subInsertCommand As New NpgsqlCommand(subInsertQuery, connection)
                            subInsertCommand.Parameters.Add(PgDb.Param("@DocID", docID))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Permission_ID", permissionID))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Last_Post_Name", name))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Email", BootstrapTextBoxQR.Text))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Name", txtname.Text))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Mobile", txtMobile.Text))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Student_ID", txturole.Text))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Post_Name", Session("Name")))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Last_Email", emailLast))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Last_Mobile", MobileLast))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Last_StudentID", IDLast))

                            subInsertCommand.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            End Using

            'Dim insertQuery As String = "INSERT INTO Status_Change_TB (DocID, Permission_ID, Name, Email, Mobile, Student_ID, Urole, Status, Post_Name) VALUES (@DocID, @Permission_ID, @Name, @Email, @Mobile, @Student_ID, '1', 'รอการยืนยัน', @Post_Name) RETURNING Status_ID"

            'Using insertCommand As New NpgsqlCommand(insertQuery, connection)
            '    insertCommand.Parameters.Add(PgDb.Param("@DocID", docID))
            '    insertCommand.Parameters.Add(PgDb.Param("@Permission_ID", permissionID))
            '    insertCommand.Parameters.Add(PgDb.Param("@Email", BootstrapTextBoxQR.Text))
            '    insertCommand.Parameters.Add(PgDb.Param("@Name", txtname.Text))
            '    insertCommand.Parameters.Add(PgDb.Param("@Mobile", txtMobile.Text))
            '    insertCommand.Parameters.Add(PgDb.Param("@Student_ID", txturole.Text))
            '    insertCommand.Parameters.Add(PgDb.Param("@Post_Name", Session("Name")))

            '    Dim insertedPermissionID As Integer = Convert.ToInt32(insertCommand.ExecuteScalar())
            'End Using



            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'อัปเดตข้อมูลสำเร็จ รอการยืนยันการอัพเดทจากอาจารย์ที่ปรึกษา!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)

        End Using
    End Sub



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

    Protected Sub DeleteAJButton_Click(sender As Object, e As EventArgs)
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()

            Dim docID As String = Request.QueryString("DocID")
            Dim permissionID As String = Request.QueryString("Permission_ID")

            Dim masSelectQuery As String = "SELECT Name,Email,Mobile,Student_ID FROM Permission_TB WHERE Permission_ID = @Permission_ID"

            Using masSelectCommand As New NpgsqlCommand(masSelectQuery, connection)
                masSelectCommand.Parameters.Add(PgDb.Param("@Permission_ID", permissionID))
                Using reader As NpgsqlDataReader = masSelectCommand.ExecuteReader()
                    If reader.Read() Then
                        Dim name As String = reader("Name").ToString()
                        Dim emailLast As String = reader("Email").ToString()
                        Dim MobileLast As String = reader("Mobile").ToString()
                        Dim IDLast As String = reader("Student_ID").ToString()

                        reader.Close() ' ปิด DataReader ก่อนใช้ ExecuteNonQuery()

                        Dim subInsertQuery As String = "INSERT INTO Status_Change_TB (DocID, Permission_ID, Last_Post_Name, Name, Email, Mobile, Student_ID, Urole, Status, Post_Name,Last_Email,Last_Mobile,Last_StudentID,Status_Change,Status_Change1,Date_Send_NS,Action) VALUES (@DocID, @Permission_ID, @Last_Post_Name, @Name, @Email, @Mobile, @Student_ID, '1', 'รอการยืนยัน', @Post_Name,@Last_Email,@Last_Mobile,@Last_StudentID,'0','0',NOW(),'ขอลบคู่โปรเจค') RETURNING Status_ID"

                        Using subInsertCommand As New NpgsqlCommand(subInsertQuery, connection)
                            subInsertCommand.Parameters.Add(PgDb.Param("@DocID", docID))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Permission_ID", permissionID))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Last_Post_Name", name))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Email", BootstrapTextBoxQR.Text))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Name", txtname.Text))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Mobile", txtMobile.Text))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Student_ID", txturole.Text))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Post_Name", Session("Name")))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Last_Email", emailLast))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Last_Mobile", MobileLast))
                            subInsertCommand.Parameters.Add(PgDb.Param("@Last_StudentID", IDLast))

                            subInsertCommand.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            End Using

            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'รอการยืนยันการลบจากอาจารย์ที่ปรึกษา!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)

        End Using
        GridView2.DataBind()
    End Sub

End Class
