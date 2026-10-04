
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web

Partial Class production_version_EditUploadfile
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Dim docID As String = Request.QueryString("docID")
            Dim permissionID As String = Request.QueryString("Permission_ID")

            Dim updateQuery As String = "UPDATE Permission_TB SET Email = @Email, Name = @Name, Mobile = @Mobile, Student_ID = @Student_ID WHERE Permission_ID = @Permission_ID AND Urole = '1'"

            Using updateCommand As New SqlCommand(updateQuery, connection)
                updateCommand.Parameters.AddWithValue("@Email", BootstrapTextBoxQR.Text)
                updateCommand.Parameters.AddWithValue("@Name", txtname.Text)
                updateCommand.Parameters.AddWithValue("@Mobile", txtMobile.Text)
                updateCommand.Parameters.AddWithValue("@Student_ID", txturole.Text)
                updateCommand.Parameters.AddWithValue("@Permission_ID", permissionID)

                updateCommand.ExecuteNonQuery()
            End Using

            Dim masSelectQuery As String = "SELECT Name, Email, Mobile, Student_ID FROM Permission_TB WHERE Permission_ID = @Permission_ID"

            Using masSelectCommand As New SqlCommand(masSelectQuery, connection)
                masSelectCommand.Parameters.AddWithValue("@Permission_ID", permissionID)
                Using reader As SqlDataReader = masSelectCommand.ExecuteReader()
                    If reader.Read() Then
                        Dim name As String = reader("Name").ToString()
                        Dim email As String = reader("Email").ToString()
                        Dim mobile As String = reader("Mobile").ToString()
                        Dim studentID As String = reader("Student_ID").ToString()

                        reader.Close() ' ปิด DataReader ก่อนใช้ ExecuteNonQuery()

                        Dim subInsertQuery As String = "INSERT INTO SubInfor_TB (DocID, Permission_ID, Name, Email, Mobile, Student_ID, Urole) VALUES (@DocID, @Permission_ID, @Name, @Email, @Mobile, @Student_ID, '1')"

                        Using subInsertCommand As New SqlCommand(subInsertQuery, connection)
                            subInsertCommand.Parameters.AddWithValue("@DocID", docID)
                            subInsertCommand.Parameters.AddWithValue("@Permission_ID", permissionID)
                            subInsertCommand.Parameters.AddWithValue("@Name", name)
                            subInsertCommand.Parameters.AddWithValue("@Email", email)
                            subInsertCommand.Parameters.AddWithValue("@Mobile", mobile)
                            subInsertCommand.Parameters.AddWithValue("@Student_ID", studentID)

                            subInsertCommand.ExecuteNonQuery()
                        End Using
                    End If
                End Using
            End Using


            Dim masUpdateQuery As String = "UPDATE MAS_DOC SET Email2 = @Email2, Student_Name2 = @Student_Name2, Student_ID2 = @Student_ID2, Mobile2 = @Mobile2 WHERE DocID = @DocID"

            Using masUpdateCommand As New SqlCommand(masUpdateQuery, connection)
                masUpdateCommand.Parameters.AddWithValue("@Email2", BootstrapTextBoxQR.Text)
                masUpdateCommand.Parameters.AddWithValue("@Student_Name2", txtname.Text)
                masUpdateCommand.Parameters.AddWithValue("@Student_ID2", txturole.Text)
                masUpdateCommand.Parameters.AddWithValue("@Mobile2", txtMobile.Text)
                masUpdateCommand.Parameters.AddWithValue("@DocID", docID)

                masUpdateCommand.ExecuteNonQuery()
            End Using

            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'อัปเดตข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)

        End Using
    End Sub



    Protected Sub cbload_Load(source As Object, e As CallbackEventArgs) Handles cbload.Callback
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim fullname = db.GetValue("SELECT Name FROM Signin_Signup WHERE (Email = @Email)",
                               db.NewParam("Email", BootstrapTextBoxQR.Text))

        e.Result = fullname

        db.Close()
    End Sub

    Protected Sub ASPxCallback1_Callback(source As Object, e As CallbackEventArgs) Handles ASPxCallback1.Callback
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim fullname = db.GetValue("SELECT Student_ID FROM Signin_Signup WHERE (Name = @Name)",
                               db.NewParam("Name", txtname.Text))

        e.Result = fullname

        db.Close()
    End Sub

    Protected Sub ASPxCallback2_Callback(source As Object, e As CallbackEventArgs) Handles ASPxCallback2.Callback
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim fullname = db.GetValue("SELECT Mobile FROM Signin_Signup WHERE (Student_ID = @Student_ID)",
                               db.NewParam("Student_ID", txturole.Text))

        e.Result = fullname

        db.Close()
    End Sub

    Protected Sub cb_load4_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles ASPxCallbackPanel1.Callback
        'BootstrapGridView2.DataBind()
    End Sub


End Class
