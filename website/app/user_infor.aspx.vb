Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Zulu.Data
Imports DevExpress.Web
Partial Class production_version_user_infor
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







    Function get_disabled(DocID)
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim ret = ""

        If db.GetValue("SELECT COUNT(*) AS COUNT FROM     dbo.Permission_TB GROUP BY DocID, UserID HAVING (DocID = @DocID) AND (UserID = @UserID) ", db.NewParam("DocID", DocID), db.NewParam("UserID", Session("UserID"))) > 0 Then
            ret = "disabled"
        Else
            ret = ""
        End If

        db.Close()
        Return ret
    End Function


    Protected Sub cb_load2_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles ASPxCallbackPanel1.Callback
        Name1.DataBind()
    End Sub

    'Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
    '    Dim URL = Request.QueryString("DocID")
    '    db.Execute("update MAS_DOC set Student_Name=@Student_Name where (DocID = @DocID)", db.NewParam("Student_Name", BootstrapTextBox1.text), db.NewParam("DocID", URL))
    '    Dim err
    '    If db.IsError(err) Then
    '        MsgBox(err)
    '    End If
    '    db.Close()
    'End Sub

    Protected Sub cb_load4_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles ASPxCallbackPanel1.Callback
        BootstrapGridView2.DataBind()
    End Sub

    Protected Sub cb_load5_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles ASPxCallbackPanel4.Callback
        DataList1.DataBind()

    End Sub

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
    '    Dim URL = Request.QueryString("DocID")
    '    db.Execute("update MAS_DOC set Student_Name=@Student_Name where (DocID = @DocID)", db.NewParam("Student_Name", Name1.Text), db.NewParam("DocID", URL))
    '    Dim err
    '    If db.IsError(err) Then
    '        MsgBox(err)
    '    End If
    '    db.Close()
    'End Sub

    'Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
    '    Dim updateCommand As String = "
    '    UPDATE MAS_DOC
    '    SET Student_Name = @Student_Name,
    '        Student_ID = @Student_ID,
    '        Mobile = @Mobile
    '    WHERE DocID = @DocID;

    '    INSERT INTO MAS_DOC_LOG (DocID, Student_Name, Student_ID, Mobile, Doc_dt_Save)
    '    SELECT DocID, Student_Name, Student_ID, Mobile, GETDATE()
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
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)

    '    ' Insert record into MAS_DOC_LOG table
    '    Dim insertQuery = "INSERT INTO MAS_DOC_LOG (DocID, Student_Name, Student_ID, Mobile, Doc_dt_Save) " & "VALUES (@DocID, @Student_Name, @Student_ID, @Mobile, GETDATE())"
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
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
    '    Dim saveLog = db.Execute("INSERT INTO MAS_DOC_LOG (DocID, Doc_dt_Save, Action) VALUES (@DocID, GETDATE(), 'Delete')",
    '                    db.NewParam("DocID", docID))

    '    'If db.IsError(saveLog) Then
    '    '    Dim err As String = db.GetErrorMessage(saveLog)
    '    '    MsgBox(err)
    '    'End If

    '    db.Close()
    'End Sub














    'Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
    '    Dim update1 = db.Execute("UPDATE MAS_DOC SET Student_Name=@Student_Name, Student_ID=@Student_ID, Mobile=@Mobile WHERE (DocID = @DocID);SELECT SCOPE_IDENTITY()",
    '                      db.NewParam("Student_Name", Name1.Text),
    '                      db.NewParam("DocID", txt_ID.Value),
    '                      db.NewParam("Student_ID", StudentID1.Text),
    '                      db.NewParam("Mobile", Phonenumber1.Text))
    '    'Dim saveLog = db.GetValue("insert into MAS_DOC_LOG (DocID, Student_Name, Student_ID, Mobile, Doc_dt_Save) VALUES (@DocID, @Student_Name, @Student_ID, @Mobile, GETDATE())",
    '    '                  db.NewParam("Student_Name", Name1.Text),
    '    '                  db.NewParam("DocID", txt_ID.Value),
    '    '                  db.NewParam("Student_ID", StudentID1.Text),
    '    '                  db.NewParam("Mobile", Phonenumber1.Text))


    '    If IsPostBack Then
    '        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('อัพเดทข้อมูลสำเร็จ !');window.location.href='index.aspx';", True)
    '    End If


    '    db.Close()



    'Dim update1 = db.Execute("UPDATE MAS_DOC SET Student_Name=@Student_Name, Student_ID=@Student_ID, Mobile=@Mobile WHERE (DocID = @DocID); INSERT INTO MAS_DOC_LOG (DocID, Student_Name, Student_ID, Mobile, Doc_dt_Save, Action) VALUES (@DocID, @Student_Name, @Student_ID, @Mobile, GETDATE(), 'Edit');SELECT SCOPE_IDENTITY()",
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
    'End Sub
    'Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
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
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
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



    'Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
    '    db.Execute("update MAS_DOC set Student_Name2=@Student_Name2 , Student_ID2 = @Student_ID2 , Mobile2=@Mobile2 where  (DocID = @DocID)", db.NewParam("Student_Name2", Name2.Text), db.NewParam("DocID", txt_ID.Value), db.NewParam("Student_ID2", StudentID2.Text), db.NewParam("Mobile2", Phonenumber2.Text))

    '    If (IsPostBack) Then
    '        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('อัพเดทข้อมูลสำเร็จ !');window.location.href='index.aspx';", True)
    '    End If
    '    Dim err
    '    If db.IsError(err) Then
    '        MsgBox(err)
    '    End If
    '    db.Close()
    'End Sub

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
    '    Dim update1 = db.Execute("update MAS_DOC set Doc_Name=@Doc_Name , Doc_NameEN = @Doc_NameEN  where  (DocID = @DocID);INSERT INTO MAS_DOC_LOG (DocID, Doc_Name, Doc_NameEN,Action,Post_Name,Doc_dt_Save) VALUES (@DocID, @Doc_Name, @Doc_NameEN,'Edit',@Post_Name,getdate());SELECT SCOPE_IDENTITY()",
    '                        db.NewParam("Doc_Name", NamprojectTH.Text),
    '                        db.NewParam("DocID", txt_ID.Value),
    '                        db.NewParam("Post_Name", Session("Name")),
    '                        db.NewParam("Doc_NameEN", NameprojectEN.Text))


    'db.Execute("update MAS_DOC set Doc_Name=@Doc_Name , Doc_NameEN = @Doc_NameEN where  (DocID = @DocID)", db.NewParam("Doc_Name", NamprojectTH.Text), db.NewParam("DocID", txt_ID.Value), db.NewParam("Doc_NameEN", NameprojectEN.Text))

    'If FileUpload1.HasFile Then
    '    Dim iUploadedCnt As Integer = 0
    '    Dim iFailedCnt As Integer = 0
    '    Dim hfc As HttpFileCollection = Request.Files

    '    lblFileList.Text = "Select <b>" & hfc.Count & "</b> file(s)"

    '    If hfc.Count <= 10 Then             ' 10 FILES RESTRICTION.
    '        For i As Integer = 0 To hfc.Count - 1
    '            Dim hpf As HttpPostedFile = hfc(i)
    '            If hpf.ContentLength > 0 Then
    '                If Not File.Exists(Server.MapPath("Docs\") &
    '                        Path.GetFileName(hpf.FileName)) Then

    '                    Dim objDir As New DirectoryInfo(Server.MapPath("Docs\"))

    '                    Dim objFI As FileInfo() =
    '                        objDir.GetFiles(Replace(Path.GetFileName(hpf.FileName),
    '                            Path.GetExtension(hpf.FileName), "") & ".*")
    '                    If objFI.Length > 0 Then
    '                        ' CHECK IF FILE WITH SAME NAME EXISTS (IGNORING THE EXTENTIONS).
    '                        For Each file As FileInfo In objFI
    '                            If Replace(objFI(0).Name, Path.GetExtension(objFI(0).Name), "") =
    '                                Replace(Path.GetFileName(hpf.FileName),
    '                                    Path.GetExtension(hpf.FileName), "") Then

    '                                iFailedCnt = iFailedCnt + 1
    '                                Exit For
    '                            End If
    '                        Next
    '                    Else
    '                        ' SAVE THE FILE IN A FOLDER.                              
    '                        hpf.SaveAs(Server.MapPath("Docs\") &
    '                                Path.GetFileName(hpf.FileName))
    '                        iUploadedCnt = iUploadedCnt + 1

    '                        'db.Execute("update Doc_File set Doc_File=@Doc_File where  (DocID = @DocID);INSERT INTO MAS_DOC_LOG (DocID, Doc_File,Doc_dt_Save) VALUES (@DocID, @Doc_File,getdate());SELECT SCOPE_IDENTITY()",
    '                        '        db.NewParam("Doc_File", hpf.FileName),
    '                        '        db.NewParam("DocID", txt_ID.Value))
    '                        'Dim saveFileLog = db.GetValue("insert into MAS_DOC_LOG (DocID,Doc_File) VALUES (@DocID,@Doc_File);SELECT SCOPE_IDENTITY()",
    '                        '        db.NewParam("DocID", txt_ID.Value),
    '                        '        db.NewParam("Doc_File", hpf.FileName))
    '                        Dim saveFile = db.GetValue("insert into Doc_File (DocID,Doc_File) VALUES (@DocID,@Doc_File)",
    '                                db.NewParam("DocID", txt_ID.Value),
    '                                db.NewParam("Doc_File", hpf.FileName))

    '                        'db.Execute("insert into MAS_DOC set doc_file = @doc_file where docid = @docid ",
    '                        '               db.NewParam("doc_file", hpf.FileName),
    '                        '               db.NewParam("docid", saveinsert))
    '                    End If
    '                End If
    '            End If
    '        Next i
    '        lblUploadStatus.Text = "<b>" & iUploadedCnt & "</b> file(s) Uploaded."
    '        lblFailedStatus.Text = "<b>" & iFailedCnt &
    '            "</b> duplicate file(s) could not be uploaded."
    '    Else
    '        lblUploadStatus.Text = "Max. 10 files allowed."
    '    End If
    'Else
    '    lblUploadStatus.Text = "No files selected."
    'End If
    'If (IsPostBack) Then
    '        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('อัพเดทข้อมูลสำเร็จ !');window.location.href='index.aspx';", True)
    '    End If
    '    Dim err
    '    If db.IsError(err) Then
    '        MsgBox(err)
    '    End If
    '    db.Close()
    'End Sub

    Protected Sub cb_load6_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles ASPxCallbackPanel5.Callback
        BootstrapGridView3.DataBind()
    End Sub

    'Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
    '    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
    '    If FileUpload1.HasFile Then
    '        Dim iUploadedCnt As Integer = 0
    '        Dim iFailedCnt As Integer = 0
    '        Dim hfc As HttpFileCollection = Request.Files

    '        lblFileList.Text = "Select <b>" & hfc.Count & "</b> file(s)"

    '        If hfc.Count <= 10 Then             ' 10 FILES RESTRICTION.
    '            For i As Integer = 0 To hfc.Count - 1
    '                Dim hpf As HttpPostedFile = hfc(i)
    '                If hpf.ContentLength > 0 Then
    '                    If Not File.Exists(Server.MapPath("Docs\") & Path.GetFileName(hpf.FileName)) Then
    '                        Dim objDir As New DirectoryInfo(Server.MapPath("Docs\"))
    '                        Dim objFI As FileInfo() = objDir.GetFiles(Replace(Path.GetFileName(hpf.FileName), Path.GetExtension(hpf.FileName), "") & ".*")
    '                        If objFI.Length > 0 Then
    '                            ' CHECK IF FILE WITH SAME NAME EXISTS (IGNORING THE EXTENSIONS).
    '                            For Each file As FileInfo In objFI
    '                                If Replace(objFI(0).Name, Path.GetExtension(objFI(0).Name), "") = Replace(Path.GetFileName(hpf.FileName), Path.GetExtension(hpf.FileName), "") Then
    '                                    iFailedCnt = iFailedCnt + 1
    '                                    Exit For
    '                                End If
    '                            Next
    '                        Else
    '                            ' SAVE THE FILE IN A FOLDER.                              
    '                            hpf.SaveAs(Server.MapPath("Docs\") & Path.GetFileName(hpf.FileName))
    '                            iUploadedCnt = iUploadedCnt + 1

    '                            ' Insert the record into Doc_File
    '                            Dim saveFile = db.GetValue("INSERT INTO Doc_File (DocID, Doc_File) VALUES (@DocID, @Doc_File); SELECT SCOPE_IDENTITY()",
    '                                db.NewParam("DocID", txt_ID.Value),
    '                                db.NewParam("Doc_File", hpf.FileName))

    '                            ' Insert the record into Doc_File_Log
    '                            Dim saveLog = db.Execute("INSERT INTO Doc_File_Log (Doc_File_ID, DocID, Doc_File,Post_Name, Date_Delete, Action) VALUES (@Doc_File_ID, @DocID, @Doc_File,@Post_Name, GETDATE(), 'Insert')",
    '                               db.NewParam("DocID", txt_ID.Value),
    '                               db.NewParam("Doc_File_ID", saveFile),
    '                               db.NewParam("Post_Name", Session("Name")),
    '                               db.NewParam("Doc_File", hpf.FileName))
    '                        End If
    '                    End If
    '                End If
    '            Next i
    '            lblUploadStatus.Text = "<b>" & iUploadedCnt & "</b> file(s) Uploaded."
    '            lblFailedStatus.Text = "<b>" & iFailedCnt & "</b> duplicate file(s) could not be uploaded."
    '        Else
    '            lblUploadStatus.Text = "Max. 10 files allowed."
    '        End If
    '    Else
    '        lblUploadStatus.Text = "No files selected."
    '    End If
    '    If (IsPostBack) Then
    '        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('อัพโหลดไฟล์สำเร็จ !');window.location.href='index.aspx';", True)
    '    End If
    'End Sub
    Private Function GetFileName(ByVal docFileID As Integer) As String
        ' Implement your logic to retrieve the file name using the docFileID
        ' For example, you can use a database query to fetch the file name based on the ID

        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim fileName As String = ""

        ' Execute a query to fetch the file name based on the docFileID
        Dim query = "SELECT Doc_File FROM Doc_File WHERE Doc_File_ID = @Doc_File_ID"
        Dim result = db.GetValue(query, db.NewParam("Doc_File_ID", docFileID))

        If result IsNot Nothing Then
            fileName = result.ToString()
        End If

        db.Close()

        Return fileName
    End Function

    Protected Sub DataList1_ItemCommand(ByVal source As Object, ByVal e As DataListCommandEventArgs)
        If e.CommandName = "Delete" Then
            Dim docFileID As Integer = Convert.ToInt32(e.CommandArgument)

            ' Retrieve the file name before deleting the record
            Dim fileName As String = GetFileName(docFileID)

            ' Delete the record from Doc_File
            DeleteDocFile(docFileID)

            ' Save the delete action to Doc_File_Log
            SaveDeleteAction(docFileID, fileName)

            ' Rebind the DataList to reflect the changes
            DataList1.DataBind()
        End If
    End Sub

    Private Sub DeleteDocFile(ByVal docFileID As Integer)
        ' Implement your delete logic here using the docFileID
        ' For example, you can use the SqlDataSource control to delete the record

        ' Set the parameters for the delete operation
        SqlDataSource1.DeleteParameters("Doc_File_ID").DefaultValue = docFileID.ToString()

        ' Perform the delete operation
        SqlDataSource1.Delete()
    End Sub

    Private Sub SaveDeleteAction(ByVal docFileID As Integer, ByVal fileName As String)
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim saveLog = db.Execute("INSERT INTO Doc_File_Log (Doc_File_ID, DocID, Doc_File,Post_Name, Date_Delete, Action) VALUES (@Doc_File_ID, @DocID, @Doc_File,@Post_Name, GETDATE(), 'Delete')",
            db.NewParam("DocID", txt_ID.Value),
            db.NewParam("Doc_File_ID", docFileID),
            db.NewParam("Post_Name", Session("Name")),
            db.NewParam("Doc_File", fileName))


        db.Close()



    End Sub



    Protected Sub cb_load7_Callback(sender As Object, e As CallbackEventArgsBase) Handles ASPxCallbackPanel6.Callback
        BootstrapGridView4.DataBind()
    End Sub

    'Private Sub BootstrapComboBox1_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles BootstrapComboBox1.Callback
    '    BootstrapComboBox1.DataBind()
    '    BootstrapComboBox1.SelectedIndex = 0
    'End Sub

    'Private Sub BootstrapComboBox1_TextChanged(sender As Object, e As EventArgs) Handles BootstrapComboBox1.TextChanged
    '    BootstrapComboBox1.DataBind()
    '    BootstrapComboBox1.SelectedIndex = 0
    'End Sub
End Class
