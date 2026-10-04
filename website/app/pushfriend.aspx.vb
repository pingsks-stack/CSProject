
Imports System.IO
Imports System.Collections.Generic
Imports DevExpress.XtraRichEdit.Layout
Imports System.Data
Imports System.Data.SqlClient
Imports Zulu.Data
Imports DevExpress.Web
Imports System

Partial Class production_version_EditUploadfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim docID As String = Request.QueryString("docID")
            ' Use the docID value as needed
        End If
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
    Protected WithEvents txt_ID As TextBox

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Dim docID As String = Request.QueryString("docID")

            ' Insert query
            Dim insertQuery As String = "INSERT INTO Permission_TB (DocID, Name, Email, Mobile, Student_ID) VALUES (@DocID, @Name, @Email, @Mobile, @Student_ID); SELECT SCOPE_IDENTITY()"

            Using insertCommand As New SqlCommand(insertQuery, connection)
                insertCommand.Parameters.AddWithValue("@DocID", docID)
                insertCommand.Parameters.AddWithValue("@Email", BootstrapTextBoxQR.Text)
                insertCommand.Parameters.AddWithValue("@Name", txtname.Text)
                insertCommand.Parameters.AddWithValue("@Mobile", txtMobile.Text)
                insertCommand.Parameters.AddWithValue("@Student_ID", txturole.Text)

                Dim insertedPermissionID As Integer = Convert.ToInt32(insertCommand.ExecuteScalar())
            End Using

            ' Update query
            Dim updateQuery As String = "UPDATE MAS_DOC SET Email2=@Email2, Student_Name2 = @Student_Name2, Student_ID2=@Student_ID2, Mobile2=@Mobile2 WHERE DocID = @DocID"

            Using updateCommand As New SqlCommand(updateQuery, connection)
                updateCommand.Parameters.AddWithValue("@Email2", BootstrapTextBoxQR.Text)
                updateCommand.Parameters.AddWithValue("@Student_Name2", txtname.Text)
                updateCommand.Parameters.AddWithValue("@Student_ID2", txturole.Text)
                updateCommand.Parameters.AddWithValue("@Mobile2", txtMobile.Text)
                updateCommand.Parameters.AddWithValue("@DocID", docID)

                updateCommand.ExecuteNonQuery()
            End Using
        End Using

        If (IsPostBack) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'เพิ่มข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='MAS_Doc.aspx'; });", True)
        End If
    End Sub





    Protected Sub DeleteButton_Click(sender As Object, e As EventArgs)
        Dim button As Button = DirectCast(sender, Button)
        Dim dataItem As GridViewRow = DirectCast(button.NamingContainer, GridViewRow)
        Dim permissionId As Integer = Convert.ToInt32(button.CommandArgument)
        Dim docID As String = Request.QueryString("docID")

        ' เชื่อมต่อกับฐานข้อมูล
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            ' สร้างคำสั่ง SQL สำหรับลบข้อมูลในตาราง Permission_TB ตาม DocID และ Permission_ID
            Dim deleteCommand As New SqlCommand("DELETE FROM Permission_TB WHERE Permission_ID = @PermissionID", connection)
            deleteCommand.Parameters.AddWithValue("@PermissionID", permissionId)

            ' ลบข้อมูล
            deleteCommand.ExecuteNonQuery()

            'สร้างคำสั่ง Sql สำหรับอัปเดตแถวในตาราง MAS_DOC
            Dim updateDocCommand As New SqlCommand("UPDATE MAS_DOC SET Student_ID2 = NULL, Student_Name2 = NULL, Email2 = NULL, Mobile2 = NULL WHERE DocID = @DocID", connection)
            updateDocCommand.Parameters.AddWithValue("@DocID", docID)

            ' อัปเดตแถวในตาราง MAS_DOC
            updateDocCommand.ExecuteNonQuery()

        End Using

        ' โหลดข้อมูลใหม่หลังจากลบและอัปเดต
        GridView1.DataBind()
    End Sub

End Class
