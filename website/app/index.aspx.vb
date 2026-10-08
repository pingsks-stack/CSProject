
Imports System.Data
Imports Npgsql
Imports System.IO
Imports DevExpress.Web
Partial Class production_version_index
    Inherits System.Web.UI.Page


    Protected Sub cb_load_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cb_load.Callback


    End Sub
    Protected Sub BootstrapGridView1_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles BootstrapGridView1.CustomCallback

        BootstrapGridView1.AddNewRow()
        'BootstrapGridView1.UpdateEdit()


        'PickList.AddNewRow()
        'BootstrapGridView1.Columns(1).Visible = False
        'BootstrapGridView1.DataBind()
    End Sub





    'Function get_disabled(DocID) As Boolean
    '    Dim db = PgDb.Platform()
    '    Dim ret As Boolean = False

    '    If db.GetValue("SELECT COUNT(*) AS COUNT FROM Permission_TB WHERE DocID = @DocID AND UserID = @UserID", db.NewParam("DocID", DocID), db.NewParam("UserID", Session("UserID"))) > 0 Then
    '        ret = True
    '    End If

    '    db.Close()
    '    Return ret
    'End Function



    Function get_disabled(DocID)
        Dim db = PgDb.Platform()
        Dim ret = ""

        If db.GetValue("SELECT COUNT(*) AS COUNT FROM     Permission_TB GROUP BY DocID, UserID HAVING (DocID = @DocID) AND (UserID = @UserID) ", db.NewParam("DocID", DocID), db.NewParam("UserID", Session("UserID"))) > 0 Then
            ret = ""
        Else
            ret = ""
        End If

        db.Close()
        Return ret
    End Function


    Protected Sub cb_load2_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles ASPxCallbackPanel1.Callback
        'Name1.DataBind()
    End Sub

    'Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '    Dim db = PgDb.Platform()
    '    Dim URL = Request.QueryString("DocID")
    '    db.Execute("update MAS_DOC set Student_Name=@Student_Name where (DocID = @DocID)", db.NewParam("Student_Name", BootstrapTextBox1.text), db.NewParam("DocID", URL))
    '    Dim err
    '    If db.IsError(err) Then
    '        MsgBox(err)
    '    End If
    '    db.Close()
    'End Sub

    Protected Sub cb_load4_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles ASPxCallbackPanel1.Callback
        'BootstrapGridView2.DataBind()
    End Sub

    Protected Sub cb_load5_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles ASPxCallbackPanel4.Callback
        BootstrapGridView5.DataBind()

    End Sub

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim db = PgDb.Platform()
    '    Dim URL = Request.QueryString("DocID")
    '    db.Execute("update MAS_DOC set Student_Name=@Student_Name where (DocID = @DocID)", db.NewParam("Student_Name", Name1.Text), db.NewParam("DocID", URL))
    '    Dim err
    '    If db.IsError(err) Then
    '        MsgBox(err)
    '    End If
    '    db.Close()
    'End Sub

    'Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
    '    Dim db = PgDb.Platform()
    '    Dim updateCommand As String = "
    '    UPDATE MAS_DOC
    '    SET Student_Name = @Student_Name,
    '        Student_ID = @Student_ID,
    '        Mobile = @Mobile
    '    WHERE DocID = @DocID;

    '    INSERT INTO MAS_DOC_LOG (DocID, Student_Name, Student_ID, Mobile, Doc_dt_Save)
    '    SELECT DocID, Student_Name, Student_ID, Mobile, NOW()
    '    FROM MAS_DOC
    '    WHERE DocID = @DocID;
    '"

    '    Dim parameters = New Zulu.Data.DbParameter() {
    '    db.NewParam("Student_Name", Name1.Text),
    '    db.NewParam("DocID", txt_ID.Value),
    '    db.NewParam("Student_ID", StudentID1.Text),
    '    db.NewParam("Mobile", Phonenumber1.Text)
    '}

    '    Dim updateResult = db.Execute(updateCommand, parameters)

    '    If db.IsError(updateResult) Then
    '        Dim err As String = db.GetErrorMessage(updateResult)
    '        MsgBox(err)
    '    Else
    '        If IsPostBack Then
    '            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('อัพเดทข้อมูลสำเร็จ !');window.location.href='index.aspx';", True)
    '        End If
    '    End If

    '    db.Close()
    'End Sub
    'Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
    '    Dim db = PgDb.Platform()

    '    ' Insert record into MAS_DOC_LOG table
    '    Dim insertQuery = "INSERT INTO MAS_DOC_LOG (DocID, Student_Name, Student_ID, Mobile, Doc_dt_Save) " & "VALUES (@DocID, @Student_Name, @Student_ID, @Mobile, NOW())"
    '    Dim insertParams = {
    '    db.NewParam("DocID", txt_ID.Value),
    '    db.NewParam("Student_Name", Name1.Text),
    '    db.NewParam("Student_ID", StudentID1.Text),
    '    db.NewParam("Mobile", Phonenumber1.Text)
    '}
    '    db.Execute(insertQuery, insertParams)

    '    ' Update record in MAS_DOC table
    '    Dim updateQuery = "UPDATE MAS_DOC " & "SET Student_Name = @Student_Name, Student_ID = @Student_ID, Mobile = @Mobile " & "WHERE DocID = @DocID"
    '    Dim updateParams = {
    '    db.NewParam("Student_Name", Name1.Text),
    '    db.NewParam("Student_ID", StudentID1.Text),
    '    db.NewParam("Mobile", Phonenumber1.Text),
    '    db.NewParam("DocID", txt_ID.Value)
    '}
    '    db.Execute(updateQuery, updateParams)

    '    ' Display success message and redirect
    '    If IsPostBack Then
    '        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('อัพเดทข้อมูลสำเร็จ !');window.location.href='index.aspx';", True)
    '    End If

    '    db.Close()
    'End Sub



    'Protected Sub DataList1_ItemCommand(ByVal source As Object, ByVal e As DataListCommandEventArgs)
    '    If e.CommandName = "Delete" Then
    '        Dim Doc_File_ID As Integer = Convert.ToInt32(e.CommandArgument)

    '        ' Delete the record from MAS_DOC
    '        DeleteMasDoc(Doc_File_ID)

    '        ' Save the delete action to MAS_DOC_LOG
    '        SaveDeleteAction(Doc_File_ID)

    '        ' Rebind the DataList to reflect the changes
    '        DataList1.DataBind()
    '    End If
    'End Sub
    'Private Sub DeleteMasDoc(ByVal Doc_File_ID As Integer)
    '    ' Implement your delete logic here using the docID
    '    ' For example, you can use the SqlDataSource control to delete the record

    '    ' Set the parameters for the delete operation
    '    SqlDataSource1.DeleteParameters("Doc_File_ID").DefaultValue = Doc_File_ID.ToString()

    '    ' Perform the delete operation
    '    SqlDataSource1.Delete()
    'End Sub
    'Private Sub SaveDeleteAction(ByVal docID As Integer)
    '    Dim db = PgDb.Platform()
    '    Dim saveLog = db.Execute("INSERT INTO MAS_DOC_LOG (DocID, Doc_dt_Save, Action) VALUES (@DocID, NOW(), 'Delete')",
    '                    db.NewParam("DocID", docID))

    '    'If db.IsError(saveLog) Then
    '    '    Dim err As String = db.GetErrorMessage(saveLog)
    '    '    MsgBox(err)
    '    'End If

    '    db.Close()
    'End Sub














    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim db = PgDb.Platform()
        Dim update1 = db.Execute("UPDATE MAS_DOC SET Student_Name=@Student_Name, Student_ID=@Student_ID, Mobile=@Mobile WHERE (DocID = @DocID)",
                          db.NewParam("Student_Name", Name1.Text),
                          db.NewParam("DocID", txt_ID.Value),
                          db.NewParam("Student_ID", StudentID1.Text),
                          db.NewParam("Mobile", Phonenumber1.Text))
        'Dim saveLog = db.GetValue("insert into MAS_DOC_LOG (DocID, Student_Name, Student_ID, Mobile, Doc_dt_Save) VALUES (@DocID, @Student_Name, @Student_ID, @Mobile, NOW())",
        '                  db.NewParam("Student_Name", Name1.Text),
        '                  db.NewParam("DocID", txt_ID.Value),
        '                  db.NewParam("Student_ID", StudentID1.Text),
        '                  db.NewParam("Mobile", Phonenumber1.Text))


        If IsPostBack Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('อัพเดทข้อมูลสำเร็จ !');window.location.href='index.aspx';", True)
        End If


        db.Close()



        'Dim update1 = db.Execute("UPDATE MAS_DOC SET Student_Name=@Student_Name, Student_ID=@Student_ID, Mobile=@Mobile WHERE (DocID = @DocID); INSERT INTO MAS_DOC_LOG (DocID, Student_Name, Student_ID, Mobile, Doc_dt_Save, Action) VALUES (@DocID, @Student_Name, @Student_ID, @Mobile, NOW(), 'Edit') RETURNING Doc_Log_ID",
        '                  db.NewParam("Student_Name", Name1.Text),
        '                  db.NewParam("DocID", txt_ID.Value),
        '                  db.NewParam("Student_ID", StudentID1.Text),
        '                  db.NewParam("Mobile", Phonenumber1.Text))


        'If db.IsError(update1) Then
        '    Dim err As String = db.GetErrorMessage(update1)
        '    MsgBox(err)
        'Else
        'If IsPostBack Then
        '    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('อัพเดทข้อมูลสำเร็จ !');window.location.href='index.aspx';", True)
        'End If
        ''End If

        'db.Close()
    End Sub
    'Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
    '    Dim db = PgDb.Platform()
    '    Dim update1 = db.Execute("update MAS_DOC set Student_Name=@Student_Name , Student_ID = @Student_ID , Mobile=@Mobile where  (DocID = @DocID)", db.NewParam("Student_Name", Name1.Text), db.NewParam("DocID", txt_ID.Value), db.NewParam("Student_ID", StudentID1.Text), db.NewParam("Mobile", Phonenumber1.Text))


    '    If (IsPostBack) Then
    '        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('อัพเดทข้อมูลสำเร็จ !');window.location.href='index.aspx';", True)
    '    End If
    '    Dim err
    '    If db.IsError(err) Then
    '        MsgBox(err)
    '    End If
    '    db.Close()
    'End Sub








    'Protected Sub DataList1_ItemCommand(ByVal source As Object, ByVal e As DataListCommandEventArgs)
    '    If e.CommandName = "Delete" Then
    '        Dim docFileId As Integer = Convert.ToInt32(e.CommandArgument)

    '        ' Delete the item using the docFileId
    '        DeleteDocFile(docFileId)

    '        ' Rebind the DataList to reflect the changes
    '        DataList1.DataBind()
    '    End If
    'End Sub
    'Private Sub DeleteDocFile(ByVal docFileId As Integer)
    '    Dim db = PgDb.Platform()
    '    Dim saveFile = db.GetValue("insert into Doc_File_Log (DocID,Doc_File) VALUES (@DocID,@Doc_File)",
    '               db.NewParam("DocID", txt_ID.Value),
    '               db.NewParam("Doc_File", docFileId))
    '    ' Implement your delete logic here using the docFileId
    '    ' For example, you can use the SqlDataSource control to delete the record

    '    ' Set the parameters for the delete operation
    '    SqlDataSource1.DeleteParameters("Doc_File_ID").DefaultValue = docFileId.ToString()

    '    ' Perform the delete operation
    '    SqlDataSource1.Delete()
    'End Sub





    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim db = PgDb.Platform()
        Dim update1 = db.Execute("update MAS_DOC set Doc_Name=@Doc_Name , Doc_NameEN = @Doc_NameEN  where  (DocID = @DocID);INSERT INTO MAS_DOC_LOG (DocID, Doc_Name, Doc_NameEN,Action,Post_Name,Doc_dt_Save) VALUES (@DocID, @Doc_Name, @Doc_NameEN,'แก้ไข',@Post_Name,NOW()) RETURNING Doc_Log_ID",
                            db.NewParam("Doc_Name", NamprojectTH.Text),
                            db.NewParam("DocID", txt_ID.Value),
                            db.NewParam("Post_Name", Session("Name")),
                            db.NewParam("Doc_NameEN", NameprojectEN.Text))


        If (IsPostBack) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('อัพเดทข้อมูลสำเร็จ !');window.location.href='index.aspx';", True)
        End If

        db.Close()
    End Sub

    Protected Sub cb_load6_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles ASPxCallbackPanel5.Callback
        BootstrapGridView3.DataBind()
    End Sub





    Private Function GetDocFileID(docID As String, fileName As String) As Integer
        ' Retrieve the Doc_File_ID based on DocID and Doc_File
        Dim db = PgDb.Platform()
        Dim docFileID As Integer = -1

        Dim query As String = "SELECT Doc_File_ID FROM Doc_File WHERE DocID = @DocID AND Doc_File = @Doc_File"
        Dim result = db.GetValue(query,
        db.NewParam("DocID", docID),
        db.NewParam("Doc_File", fileName))

        If result IsNot Nothing AndAlso Integer.TryParse(result.ToString(), docFileID) Then
            Return docFileID
        End If

        Return -1
    End Function


    Protected Sub DeleteButtonFile_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim button As Button = DirectCast(sender, Button)
        Dim docFileID As Integer = Convert.ToInt32(button.CommandArgument)

        ' Get the file name before deleting
        Dim docFile As String = GetDocFileByID(docFileID)

        ' Delete the record from the database
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()
            Using command As New NpgsqlCommand("DELETE FROM Doc_File WHERE Doc_File_ID = @Doc_File_ID", connection)
                command.Parameters.Add(PgDb.Param("@Doc_File_ID", docFileID))
                command.ExecuteNonQuery()
            End Using
        End Using

        ' Insert the record into Doc_File_Log
        Dim db = PgDb.Platform()

        Dim saveLog = db.Execute("INSERT INTO Doc_File_Log (Doc_File_ID, DocID, Doc_File, Post_Name, Date_Delete, Action) VALUES (@Doc_File_ID, @DocID, @Doc_File, @Post_Name, NOW(), 'ลบไฟล์')",
    db.NewParam("Doc_File_ID", docFileID),
    db.NewParam("DocID", txt_ID.Value),
    db.NewParam("Doc_File", docFile),
    db.NewParam("Post_Name", Session("Name"))
    )

        ' Rebind the data to refresh the GridView
        BootstrapGridView5.DataBind()

        If (IsPostBack) Then
            Dim script As String = "Swal.fire({ title: 'สำเร็จ', text: 'ลบข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='index.aspx'; });"
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", script, True)
        End If

        ' Display success message or perform other actions
        ' ...
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


    Protected Sub cb_load7_Callback(sender As Object, e As CallbackEventArgsBase) Handles ASPxCallbackPanel6.Callback
        BootstrapGridView4.DataBind()
    End Sub



    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()


            ' Insert query
            Dim insertQuery As String = "INSERT INTO Permission_TB (DocID, Name, Email, Mobile, Student_ID) VALUES (@DocID, @Name, @Email, @Mobile, @Student_ID) RETURNING Permission_ID"

            Using insertCommand As New NpgsqlCommand(insertQuery, connection)
                insertCommand.Parameters.Add(PgDb.Param("@DocID", txt_ID.Value))
                insertCommand.Parameters.Add(PgDb.Param("@Email", BootstrapTextBoxQR.Text))
                insertCommand.Parameters.Add(PgDb.Param("@Name", txtname.Text))
                insertCommand.Parameters.Add(PgDb.Param("@Mobile", txtMobile.Text))
                insertCommand.Parameters.Add(PgDb.Param("@Student_ID", txturole.Text))

                Dim insertedPermissionID As Integer = Convert.ToInt32(insertCommand.ExecuteScalar())
            End Using

            ' Update query
            Dim updateQuery As String = "UPDATE MAS_DOC SET Email2=@Email2, Student_Name2 = @Student_Name2, Student_ID2=@Student_ID2, Mobile2=@Mobile2 WHERE DocID = @DocID"

            Using updateCommand As New NpgsqlCommand(updateQuery, connection)
                updateCommand.Parameters.Add(PgDb.Param("@Email2", BootstrapTextBoxQR.Text))
                updateCommand.Parameters.Add(PgDb.Param("@Student_Name2", txtname.Text))
                updateCommand.Parameters.Add(PgDb.Param("@Student_ID2", txturole.Text))
                updateCommand.Parameters.Add(PgDb.Param("@Mobile2", txtMobile.Text))
                updateCommand.Parameters.Add(PgDb.Param("@DocID", txt_ID.Value))

                updateCommand.ExecuteNonQuery()
            End Using
        End Using

        If (IsPostBack) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'เพิ่มข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='MAS_Doc.aspx'; });", True)
        End If
    End Sub


    Protected Sub cb_load10_Callback(sender As Object, e As CallbackEventArgsBase) Handles ASPxCallbackPanel7.Callback
        BootstrapGridView2.DataBind()
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

        Dim uroleValue As String = GetUroleValueFromSigninSignup(EmailAJ.Text) ' Assuming you have a function to retrieve the Urole value from the Signin_Signup table

        If uroleValue <> "2" Then
            ' Urole is not equal to 2, do not insert
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ไม่สามารถเพิ่มข้อมูล', text: 'เนื่องจากอีเมลล์นี้ไม่ใช่อาจารย์!', icon: 'warning', confirmButtonText: 'OK' });", True)
            Return
        End If

        ' ตรวจสอบว่ามี DocID และ Email ที่ต้องการเพิ่มอยู่ในฐานข้อมูลหรือไม่
        Dim existingPermissionTable = db.GetDataTable("SELECT * FROM Permission_TB WHERE DocID = @DocID AND Email = @Email",
                                               db.NewParam("DocID", txt_ID.Value),
                                               db.NewParam("Email", EmailAJ.Text))

        If existingPermissionTable IsNot Nothing AndAlso existingPermissionTable.Rows.Count > 0 Then
            Dim existingPermission = existingPermissionTable.Rows(0)
            ' ข้อมูลซ้ำ ไม่ทำการเพิ่มข้อมูล
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ข้อมูลซ้ำ', text: 'ไม่สามารถเพิ่มข้อมูลได้เนื่องจากเอกสารนี้มีอีเมลล์นี้อยู่แล้ว!', icon: 'warning', confirmButtonText: 'OK' });", True)
            Return
        End If

        Dim teacherPermissionCount = db.GetValue("SELECT COUNT(*) FROM Permission_TB WHERE DocID = @DocID AND Urole = '2'",
                                             db.NewParam("DocID", txt_ID.Value))

        If teacherPermissionCount >= 3 Then
            ' มี Email ที่ Urole_Permis เป็น "Teacher" มากกว่าหรือเท่ากับ 3 รายการแล้ว ไม่ทำการเพิ่มข้อมูล
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ข้อมูลเกิน', text: 'ไม่สามารถเพิ่มข้อมูลเนื่องจากมีข้อมูลอาจารย์ครบแล้ว!', icon: 'warning', confirmButtonText: 'OK' });", True)
            Return
        End If

        ' ตรวจสอบว่าเป็น "อาจารย์ที่ปรึกษา" และมี Email เดียวเท่านั้น
        If BootstrapComboBox1.Text = "ที่ปรึกษา" Then
            Dim advisorPermissionCount = db.GetValue("SELECT COUNT(*) FROM Permission_TB WHERE DocID = @DocID AND Urole_TC = 'ที่ปรึกษา'",
                                                      db.NewParam("DocID", txt_ID.Value))

            If advisorPermissionCount > 0 Then
                ' มีการเพิ่มข้อมูลที่ปรึกษาอยู่แล้ว ไม่ทำการเพิ่มข้อมูล
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'เพิ่มข้อมูลไม่สำเร็จ', text: 'ไม่สามารถเพิ่มข้อมูลเนื่องจาก อาจารย์ที่ปรึกษาสามารถมีได้แค่คนเดียวเท่านั้น!', icon: 'warning', confirmButtonText: 'OK' });", True)
                Return
            End If
        End If

        ' เพิ่มข้อมูลในกรณีที่ไม่มีการเพิ่มข้อมูลที่ปรึกษาอยู่แล้ว
        Dim savePermission = db.Execute("INSERT INTO Permission_TB (DocID, Name, Email, Mobile, Urole, Urole_TC) VALUES (@DocID, @Name, @Email, @Mobile,'2', @Urole_TC) RETURNING Permission_ID",
                                        db.NewParam("DocID", txt_ID.Value),
                                        db.NewParam("Name", NameAJ.Text),
                                        db.NewParam("Email", EmailAJ.Text),
                                        db.NewParam("Mobile", PhonenumberAJ.Text),
                                        db.NewParam("Urole_TC", BootstrapComboBox1.Text))

        db.Close()

        If (IsPostBack) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'เพิ่มข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='index.aspx'; });", True)
        End If

        ' เคลียร์ค่าใน TextBox
        txt_ID.Value = ""
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

    Protected Sub DeleteButton_Click(sender As Object, e As EventArgs)
        ' Get the Permission_ID from the command argument
        Dim permissionId As Integer = Convert.ToInt32(DirectCast(sender, Button).CommandArgument)

        ' Delete the record from the database
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Using connection As New NpgsqlConnection(connectionString)
            connection.Open()
            Using command As New NpgsqlCommand("DELETE FROM Permission_TB WHERE Permission_ID = @Permission_ID", connection)
                command.Parameters.Add(PgDb.Param("@Permission_ID", permissionId))
                command.ExecuteNonQuery()
            End Using
        End Using
        If (IsPostBack) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ลบข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='index.aspx'; });", True)
        End If
        ' Rebind the data to refresh the GridView
        BootstrapGridView2.DataBind()
    End Sub




    Protected Sub cb_load11_Callback(sender As Object, e As CallbackEventArgsBase) Handles ASPxCallbackPanel8.Callback

    End Sub




    Protected Sub SaveSubmit_Click(sender As Object, e As EventArgs)
        Dim db = PgDb.Platform()
        Dim updatePerson = db.Execute("UPDATE Permission_TB SET Submit_TC = 'ผ่าน' WHERE DocID = @DocID AND Email = @Email",
                            db.NewParam("DocID", txt_ID.Value),
                            db.NewParam("Email", Session("Email")))

        If db.GetValue("SELECT COUNT(Submit_TC) AS รวม FROM  Permission_TB GROUP BY DocID, Urole HAVING (Urole = '2') AND (DocID = @DocID) ", db.NewParam("DocID", txt_ID.Value)) >= 3 Then
            Dim updateSubmit = db.Execute("UPDATE MAS_DOC SET Submit_TC = 'ผ่าน3/3' WHERE DocID = @DocID",
                            db.NewParam("DocID", txt_ID.Value))

        Else
            Dim count As Integer = db.GetValue("SELECT COUNT(Submit_TC) AS รวม FROM  Permission_TB GROUP BY DocID, Urole HAVING (Urole = '2') AND (DocID = @DocID) ", db.NewParam("DocID", txt_ID.Value))

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


