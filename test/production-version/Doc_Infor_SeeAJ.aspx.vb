Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Zulu.Data
Imports DevExpress.Web
Imports System
Imports System.Collections.Generic
Imports DevExpress.Web.Bootstrap
Imports DevExpress.XtraExport.Helpers

Partial Class production_version_Doc_All
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

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)


    '    Dim existingDocNameTH As String = ""
    '    Dim existingDocNameEN As String = ""

    '    Dim existingData = db.GetDataTable("SELECT TOP 1 Doc_Name, Doc_NameEN FROM MAS_DOC WHERE DocID = @DocID", db.NewParam("DocID", txt_ID.Value))
    '    If existingData IsNot Nothing AndAlso existingData.Rows.Count > 0 Then
    '        Dim existingDataRow = existingData.Rows(0)
    '        existingDocNameTH = existingDataRow("Doc_Name").ToString()
    '        existingDocNameEN = existingDataRow("Doc_NameEN").ToString()
    '    End If


    '    Dim update1 = db.Execute("UPDATE MAS_DOC SET Doc_Name = @Doc_Name, Doc_NameEN = @Doc_NameEN WHERE DocID = @DocID;INSERT INTO MAS_DOC_LOG (DocID, Doc_Name, Doc_NameEN, Action, Post_Name, Doc_dt_Save) VALUES (@DocID, @Doc_Name, @Doc_NameEN, 'แก้ไข', @Post_Name, GETDATE());SELECT SCOPE_IDENTITY()",
    '                         db.NewParam("Doc_Name", NamprojectTH.Text),
    '                         db.NewParam("DocID", txt_ID.Value),
    '                         db.NewParam("Post_Name", Session("Name")),
    '                         db.NewParam("Doc_NameEN", NameprojectEN.Text))

    '    db.Close()

    '    If (IsPostBack) Then
    '        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({title: 'อัพเดทสำเร็จ',html: '<p>อัปเดตชื่อโปรเจคสำเร็จ!</p><p>ชื่อโปรเจคใหม่ (ไทย): " & NamprojectTH.Text & "</p><p>ชื่อโปรเจคใหม่ (อังกฤษ): " & NameprojectEN.Text & "</p>',icon: 'success',confirmButtonText: 'OK'}).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
    '    End If
    'End Sub





    'Protected Sub DeleteButton_Click(sender As Object, e As EventArgs)
    '    Dim button As Button = DirectCast(sender, Button)
    '    Dim dataItem As GridViewRow = DirectCast(button.NamingContainer, GridViewRow)
    '    Dim permissionId As Integer = Convert.ToInt32(button.CommandArgument)
    '    Dim docID As String = Request.QueryString("docID")

    '    ' เชื่อมต่อกับฐานข้อมูล
    '    Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
    '    Using connection As New SqlConnection(connectionString)
    '        connection.Open()

    '        ' สร้างคำสั่ง SQL สำหรับลบข้อมูลในตาราง Permission_TB ตาม DocID และ Permission_ID
    '        Dim deleteCommand As New SqlCommand("DELETE FROM Permission_TB WHERE Permission_ID = @PermissionID", connection)
    '        deleteCommand.Parameters.AddWithValue("@PermissionID", permissionId)

    '        ' ลบข้อมูล
    '        deleteCommand.ExecuteNonQuery()

    '        'สร้างคำสั่ง Sql สำหรับอัปเดตแถวในตาราง MAS_DOC
    '        Dim updateDocCommand As New SqlCommand("UPDATE MAS_DOC SET Student_ID2 = NULL, Student_Name2 = NULL, Email2 = NULL, Mobile2 = NULL WHERE DocID = @DocID", connection)
    '        updateDocCommand.Parameters.AddWithValue("@DocID", docID)

    '        ' อัปเดตแถวในตาราง MAS_DOC
    '        updateDocCommand.ExecuteNonQuery()

    '    End Using

    '    ' โหลดข้อมูลใหม่หลังจากลบและอัปเดต
    '    GridView1.DataBind()
    'End Sub

    'Protected Sub DeleteAJButton_Click(sender As Object, e As EventArgs)
    '    ' Get the Permission_ID from the command argument
    '    Dim permissionId As Integer = Convert.ToInt32(DirectCast(sender, Button).CommandArgument)

    '    ' Get the DocID and Urole_TC of the record to be deleted
    '    Dim docId As Integer
    '    Dim uroleTc As String
    '    Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
    '    Using connection As New SqlConnection(connectionString)
    '        connection.Open()
    '        Using command As New SqlCommand("SELECT DocID, Urole_TC FROM Permission_TB WHERE Permission_ID = @Permission_ID", connection)
    '            command.Parameters.AddWithValue("@Permission_ID", permissionId)
    '            Using reader As SqlDataReader = command.ExecuteReader()
    '                If reader.Read() Then
    '                    docId = Convert.ToInt32(reader("DocID"))
    '                    uroleTc = Convert.ToString(reader("Urole_TC"))
    '                End If
    '            End Using
    '        End Using
    '    End Using

    '    ' Delete the record from the database
    '    Using connection As New SqlConnection(connectionString)
    '        connection.Open()
    '        Using command As New SqlCommand("DELETE FROM Permission_TB WHERE Permission_ID = @Permission_ID", connection)
    '            command.Parameters.AddWithValue("@Permission_ID", permissionId)
    '            command.ExecuteNonQuery()
    '        End Using
    '    End Using

    '    ' Check if the deleted record was with Urole_TC = "ที่ปรึกษา" and update Name_teacherPJ in MAS_DOC to NULL
    '    If uroleTc = "ที่ปรึกษา" Then
    '        Using connection As New SqlConnection(connectionString)
    '            connection.Open()
    '            Using command As New SqlCommand("UPDATE MAS_DOC SET Name_teacherPJ = NULL WHERE DocID = @DocID AND (SELECT COUNT(*) FROM Permission_TB WHERE DocID = @DocID AND Urole_TC = 'ที่ปรึกษา') = 0", connection)
    '                command.Parameters.AddWithValue("@DocID", docId)
    '                command.ExecuteNonQuery()
    '            End Using
    '        End Using
    '    End If

    '    ' Rebind the data to refresh the GridView
    '    GridView2.DataBind()
    'End Sub



    'Protected Sub DeleteButtonFile_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim button As Button = DirectCast(sender, Button)
    '    Dim docFileID As Integer = Convert.ToInt32(button.CommandArgument)
    '    Dim docID As String = Request.QueryString("docID")
    '    ' Get the file name before deleting
    '    Dim docFile As String = GetDocFileByID(docFileID)

    '    ' Delete the record from the database
    '    Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
    '    Using connection As New SqlConnection(connectionString)
    '        connection.Open()
    '        Using command As New SqlCommand("DELETE FROM Doc_File WHERE Doc_File_ID = @Doc_File_ID", connection)
    '            command.Parameters.AddWithValue("@Doc_File_ID", docFileID)
    '            command.ExecuteNonQuery()
    '        End Using
    '    End Using

    '    ' Insert the record into Doc_File_Log
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)

    '    Dim saveLog = db.Execute("INSERT INTO Doc_File_Log (Doc_File_ID, DocID, Doc_File, Post_Name, Date_Delete, Action) VALUES (@Doc_File_ID, @DocID, @Doc_File, @Post_Name, GETDATE(), 'ลบไฟล์')",
    'db.NewParam("Doc_File_ID", docFileID),
    'db.NewParam("DocID", docID),
    'db.NewParam("Doc_File", docFile),
    'db.NewParam("Post_Name", Session("Name"))
    ')

    '    ' Rebind the data to refresh the GridView
    '    GridView3.DataBind()



    '    ' Display success message or perform other actions
    '    ' ...
    'End Sub


    Private Function GetDocFileByID(ByVal docFileID As Integer) As String
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Dim docFile As String = ""

        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlCommand("SELECT Doc_File FROM Doc_File WHERE Doc_File_ID = @Doc_File_ID", connection)
                command.Parameters.AddWithValue("@Doc_File_ID", docFileID)
                Dim reader As SqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    docFile = reader("Doc_File").ToString()
                End If
            End Using
        End Using

        Return docFile
    End Function






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


    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Dim docID As String = Request.QueryString("docID")


            Dim checkQuery As String = "SELECT COUNT(*) FROM Permission_TB WHERE DocID = @DocID AND Urole = '1'"
            Dim count As Integer

            Using checkCommand As New SqlCommand(checkQuery, connection)
                checkCommand.Parameters.AddWithValue("@DocID", docID)
                count = Convert.ToInt32(checkCommand.ExecuteScalar())
            End Using

            If count >= 2 Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'แจ้งเตือน', text: 'คุณได้เพิ่มคู่โปรเจคแล้ว!', icon: 'warning', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
            Else
                Dim insertQuery As String = "INSERT INTO Permission_TB (DocID, Name, Email, Mobile, Student_ID, Urole) VALUES (@DocID, @Name, @Email, @Mobile, @Student_ID, '1'); SELECT SCOPE_IDENTITY()"

                Using insertCommand As New SqlCommand(insertQuery, connection)
                    insertCommand.Parameters.AddWithValue("@DocID", docID)
                    insertCommand.Parameters.AddWithValue("@Email", BootstrapTextBoxQR.Text)
                    insertCommand.Parameters.AddWithValue("@Name", txtname.Text)
                    insertCommand.Parameters.AddWithValue("@Mobile", txtMobile.Text)
                    insertCommand.Parameters.AddWithValue("@Student_ID", txturole.Text)

                    Dim insertedPermissionID As Integer = Convert.ToInt32(insertCommand.ExecuteScalar())
                End Using


                Dim updateQuery As String = "UPDATE MAS_DOC SET Email2=@Email2, Student_Name2 = @Student_Name2, Student_ID2=@Student_ID2, Mobile2=@Mobile2 WHERE DocID = @DocID"

                Using updateCommand As New SqlCommand(updateQuery, connection)
                    updateCommand.Parameters.AddWithValue("@Email2", BootstrapTextBoxQR.Text)
                    updateCommand.Parameters.AddWithValue("@Student_Name2", txtname.Text)
                    updateCommand.Parameters.AddWithValue("@Student_ID2", txturole.Text)
                    updateCommand.Parameters.AddWithValue("@Mobile2", txtMobile.Text)
                    updateCommand.Parameters.AddWithValue("@DocID", docID)

                    updateCommand.ExecuteNonQuery()
                End Using


                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'เพิ่มข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
            End If
        End Using
    End Sub


    Protected Sub cb_load11_Callback(sender As Object, e As CallbackEventArgsBase) Handles ASPxCallbackPanel8.Callback

    End Sub

    Protected Sub ASPxCallback3_Callback(source As Object, e As CallbackEventArgs) Handles ASPxCallback3.Callback
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim fullname = db.GetValue("SELECT Name FROM Signin_Signup WHERE (Email = @Email)",
                                   db.NewParam("Email", EmailAJ.Text))

        e.Result = fullname

        db.Close()
    End Sub



    Protected Sub ASPxCallback4_Callback(source As Object, e As CallbackEventArgs) Handles ASPxCallback4.Callback
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim fullname = db.GetValue("SELECT Mobile FROM Signin_Signup WHERE (Name = @Name)",
                               db.NewParam("Name", NameAJ.Text))

        e.Result = fullname

        db.Close()
    End Sub

    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
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
        Dim savePermission = db.Execute("INSERT INTO Permission_TB (DocID, Name, Email, Mobile, Urole, Urole_TC) VALUES (@DocID, @Name, @Email, @Mobile,'2', @Urole_TC); SELECT SCOPE_IDENTITY()",
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

        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Using command As New SqlCommand("SELECT Urole FROM Signin_Signup WHERE Email = @Email", connection)
                command.Parameters.AddWithValue("@Email", email)

                Using reader As SqlDataReader = command.ExecuteReader()
                    If reader.Read() Then
                        uroleValue = reader("Urole").ToString()
                    End If
                End Using
            End Using
        End Using

        Return uroleValue
    End Function



    'Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    ' Get the Doc_List_ID from the command argument
    '    Dim docListId As Integer = Convert.ToInt32(DirectCast(sender, Button).CommandArgument)

    '    ' Get the existing data from Doc_List_TB before deletion
    '    Dim existingData As DataTable = New DataTable()
    '    Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
    '    Using connection As New SqlConnection(connectionString)
    '        connection.Open()
    '        Using command As New SqlCommand("SELECT DocID, Doc_List_Type, Doc_Functions_Code, Doc_Date_Save FROM Doc_List_TB WHERE Doc_List_ID = @Doc_List_ID", connection)
    '            command.Parameters.AddWithValue("@Doc_List_ID", docListId)
    '            Using adapter As New SqlDataAdapter(command)
    '                adapter.Fill(existingData)
    '            End Using
    '        End Using
    '    End Using

    '    ' Delete the record from the database
    '    Using connection As New SqlConnection(connectionString)
    '        connection.Open()
    '        Using command As New SqlCommand("DELETE FROM Doc_List_TB WHERE Doc_List_ID = @Doc_List_ID", connection)
    '            command.Parameters.AddWithValue("@Doc_List_ID", docListId)
    '            command.ExecuteNonQuery()
    '        End Using
    '    End Using

    '    ' Log the deletion in Doc_List_Log
    '    If existingData.Rows.Count > 0 Then
    '        Dim docID As Integer = Convert.ToInt32(existingData.Rows(0)("DocID"))
    '        Dim docListType As String = existingData.Rows(0)("Doc_List_Type").ToString()
    '        Dim docFunctionsCode As String = existingData.Rows(0)("Doc_Functions_Code").ToString()
    '        Dim docDateSave As DateTime = Convert.ToDateTime(existingData.Rows(0)("Doc_Date_Save"))
    '        Dim postName As String = Session("Name").ToString()
    '        Dim action As String = "ลบ"

    '        Using connection As New SqlConnection(connectionString)
    '            connection.Open()
    '            Using command As New SqlCommand("INSERT INTO Doc_List_Log (DocID, Doc_List_ID, Doc_List_Type, Doc_Functions_Code, Doc_Date_Save, Post_Name, Action) VALUES (@DocID, @Doc_List_ID, @Doc_List_Type, @Doc_Functions_Code, @Doc_Date_Save, @Post_Name, @Action)", connection)
    '                command.Parameters.AddWithValue("@DocID", docID)
    '                command.Parameters.AddWithValue("@Doc_List_ID", docListId)
    '                command.Parameters.AddWithValue("@Doc_List_Type", docListType)
    '                command.Parameters.AddWithValue("@Doc_Functions_Code", docFunctionsCode)
    '                command.Parameters.AddWithValue("@Doc_Date_Save", docDateSave)
    '                command.Parameters.AddWithValue("@Post_Name", postName)
    '                command.Parameters.AddWithValue("@Action", action)
    '                command.ExecuteNonQuery()
    '            End Using
    '        End Using
    '    End If

    '    ' Rebind the data to refresh the DataList
    '    DataList1.DataBind()
    'End Sub


    Protected Sub SaveSubmit_Click(sender As Object, e As EventArgs)
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim updatePerson = db.Execute("UPDATE Permission_TB SET Submit_TC = 'ผ่าน' WHERE DocID = @DocID AND Email = @Email",
                            db.NewParam("DocID", txt_ID.Value),
                            db.NewParam("Email", Session("Email")))

        If db.GetValue("SELECT COUNT(Submit_TC) AS รวม FROM  dbo.Permission_TB GROUP BY DocID, Urole HAVING (Urole = N'2') AND (DocID = @DocID) ", db.NewParam("DocID", txt_ID.Value)) >= 3 Then
            Dim updateSubmit = db.Execute("UPDATE MAS_DOC SET Submit_TC = 'ผ่าน3/3' WHERE DocID = @DocID",
                            db.NewParam("DocID", txt_ID.Value))

        Else
            Dim count As Integer = db.GetValue("SELECT COUNT(Submit_TC) AS รวม FROM  dbo.Permission_TB GROUP BY DocID, Urole HAVING (Urole = N'2') AND (DocID = @DocID) ", db.NewParam("DocID", txt_ID.Value))

            If count = 1 Then
                Dim updateSubmit1 = db.Execute("UPDATE MAS_DOC SET Submit_TC = 'ผ่าน1/3' WHERE DocID = @DocID",
                            db.NewParam("DocID", txt_ID.Value))
            ElseIf count = 2 Then
                Dim updateSubmit2 = db.Execute("UPDATE MAS_DOC SET Submit_TC = 'ผ่าน2/3' WHERE DocID = @DocID",
                            db.NewParam("DocID", txt_ID.Value))
            End If
        End If




    End Sub



End Class
