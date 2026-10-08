
Imports Npgsql
Imports System.IO

Partial Class production_version_Insert
    Inherits System.Web.UI.Page

    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

    '    ' ... ตรวจสอบค่าซ้ำ และการเชื่อมต่อฐานข้อมูล ...

    '    Dim currentDate As DateTime = DateTime.Now
    '    Dim selectedMonth As Integer = currentDate.Month
    '    Dim selectedYear As Integer = currentDate.Year

    '    Dim classYearNo As String

    '    If (selectedMonth >= 6 And selectedMonth <= 10) Then
    '        classYearNo = (selectedYear + 543).ToString() + "/1"
    '    ElseIf (selectedMonth >= 11 And selectedMonth <= 12) Then
    '        classYearNo = (selectedYear + 543).ToString() + "/2"
    '    ElseIf (selectedMonth >= 1 And selectedMonth <= 3) Then
    '        classYearNo = (selectedYear + 542).ToString() + "/2"
    '    ElseIf (selectedMonth >= 4 And selectedMonth <= 5) Then
    '        classYearNo = (selectedYear + 542).ToString() + "/3"
    '    End If

    '    Dim studentID As String = Session("Student_ID").ToString()

    '    ' ตรวจสอบรหัสขึ้นต้นของ Student_ID เพื่อกำหนดค่า Class_No
    '    Dim classNo As String
    '    If studentID.StartsWith("66") Then
    '        classNo = "1"
    '    ElseIf studentID.StartsWith("65") Then
    '        classNo = "2"
    '    ElseIf studentID.StartsWith("64") Then
    '        classNo = "3"
    '    ElseIf studentID.StartsWith("63") Then
    '        classNo = "4"
    '    ElseIf studentID.StartsWith("62") Then
    '        classNo = "5"
    '    ElseIf studentID.StartsWith("61") Then
    '        classNo = "6"
    '    ElseIf studentID.StartsWith("60") Then
    '        classNo = "7"
    '    ElseIf studentID.StartsWith("59") Then
    '        classNo = "8"
    '    Else
    '        classNo = "0"
    '    End If

    '    Dim saveinsert As String = "insert into MAS_DOC (Student_Name, Student_ID, Email, Mobile, Doc_Name, Doc_NameEN, Class_No, Class_Year_No, Doc_dt_Save) VALUES (@Student_Name, @Student_ID, @Email, @Mobile, @Doc_Name, @Doc_NameEN, @Class_No, @Class_Year_No, NOW()) RETURNING DocID"
    '    Using connection As New NpgsqlConnection(connectionString)
    '        connection.Open()
    '        ' Insert ข้อมูลลงในฐานข้อมูล
    '        Dim saveinsertCommand As New NpgsqlCommand(saveinsert, connection)
    '        saveinsertCommand.Parameters.Add(PgDb.Param("@Student_Name", Session("Name")))
    '        saveinsertCommand.Parameters.Add(PgDb.Param("@Student_ID", Session("Student_ID")))
    '        saveinsertCommand.Parameters.Add(PgDb.Param("@Email", Session("Email")))
    '        saveinsertCommand.Parameters.Add(PgDb.Param("@Mobile", Session("Mobile")))
    '        saveinsertCommand.Parameters.Add(PgDb.Param("@Doc_Name", NamprojectTH.Text))
    '        saveinsertCommand.Parameters.Add(PgDb.Param("@Doc_NameEN", NameprojectEN.Text))
    '        saveinsertCommand.Parameters.Add(PgDb.Param("@Class_No", classNo)) ' กำหนดค่า Class_No
    '        saveinsertCommand.Parameters.Add(PgDb.Param("@Class_Year_No", classYearNo))
    '        saveinsertCommand.ExecuteNonQuery()

    '        ' ... ส่วนอื่น ๆ ของโค้ด ...

    '        connection.Close()
    '    End Using
    'End Sub





    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not Page.IsValid Then Return

        Dim db = PgDb.Platform()

        'ตรวจสอบค่า Doc_Name ซ้ำ
        Dim checkDocNameQuery As String = "SELECT COUNT(*) FROM MAS_DOC WHERE Doc_Name = @Doc_Name"
        Dim docNameExists As Integer = Convert.ToInt32(db.GetValue(checkDocNameQuery, db.NewParam("Doc_Name", NamprojectTH.Text)))

        'ตรวจสอบค่า Doc_NameEN ซ้ำ
        Dim checkDocNameENQuery As String = "SELECT COUNT(*) FROM MAS_DOC WHERE Doc_NameEN = @Doc_NameEN"
        Dim docNameENExists As Integer = Convert.ToInt32(db.GetValue(checkDocNameENQuery, db.NewParam("Doc_NameEN", NameprojectEN.Text)))

        Dim currentDate As DateTime = DateTime.Now
        Dim selectedMonth As Integer = currentDate.Month
        Dim selectedYear As Integer = currentDate.Year

        Dim classYearNo As String

        If (selectedMonth >= 6 And selectedMonth <= 10) Then
            classYearNo = (selectedYear + 543).ToString() + "/1"
        ElseIf (selectedMonth >= 11 And selectedMonth <= 12) Then
            classYearNo = (selectedYear + 543).ToString() + "/2"
        ElseIf (selectedMonth >= 1 And selectedMonth <= 3) Then
            classYearNo = (selectedYear + 542).ToString() + "/2"
        ElseIf (selectedMonth >= 4 And selectedMonth <= 5) Then
            classYearNo = (selectedYear + 542).ToString() + "/3"
        End If

        Dim studentID As String = Convert.ToString(Session("Student_ID"))

        ' ชั้นปี = ปีการศึกษาปัจจุบัน (พ.ศ. 2 หลักท้าย, เริ่มเดือน มิ.ย.) - 2 หลักแรกของรหัสนิสิต + 1
        ' เดิมฝังรหัส 59-66 ไว้ตายตัว ทำให้รหัส 67 ขึ้นไปได้ชั้นปี 0
        Dim academicYear As Integer = (If(selectedMonth >= 6, selectedYear + 543, selectedYear + 542)) Mod 100
        Dim entryYear As Integer
        Dim classNo As String = "0"
        If studentID.Length >= 2 AndAlso Integer.TryParse(studentID.Substring(0, 2), entryYear) Then
            Dim n As Integer = academicYear - entryYear + 1
            If n >= 1 AndAlso n <= 8 Then classNo = n.ToString()
        End If

        If docNameExists > 0 Or docNameENExists > 0 Then
            'แสดงการแจ้งเตือน Swal.fire เมื่อ Doc_Name หรือ Doc_NameEN ซ้ำ
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "swal.fire({ title: 'แจ้งเตือน', text: 'ชื่อโปรเจค(ภาษาไทย) หรือ ชื่อโปรเจค(ภาษาอังกฤษ) ซ้ำ', icon: 'warning', confirmButtonText: 'ตกลง' })", True)
        Else
            Dim saveinsert = db.GetValue("insert into MAS_DOC (Student_Name,Student_ID,Email,Mobile,Doc_Name,Doc_NameEN,Class_No,Class_Year_No,Doc_dt_Save,Submit_TC,Doc_Type) VALUES (@Student_Name,@Student_ID,@Email,@Mobile,@Doc_Name,@Doc_NameEN,@Class_No,@Class_Year_No,NOW(),'ยังไม่มีการกดผ่าน/ไม่ผ่าน',@Doc_Type) RETURNING DocID",
                   db.NewParam("Doc_Type", ddlDocType.SelectedValue),
                   db.NewParam("Student_Name", Session("Name")),
                   db.NewParam("Student_ID", Session("Student_ID")),
                   db.NewParam("Email", Session("Email")),
                   db.NewParam("Mobile", Session("Mobile")),
                   db.NewParam("Doc_Name", NamprojectTH.Text),
                   db.NewParam("Doc_NameEN", NameprojectEN.Text),
                   db.NewParam("Class_No", classNo),
                   db.NewParam("Class_Year_No", classYearNo))

            Dim savePermission = db.GetValue("insert into Permission_TB (DocID,UserID,Name,Email,Mobile,Urole,Student_ID) VALUES (@DocID,@UserID,@Name,@Email,@Mobile,'1',@Student_ID) RETURNING Permission_ID",
                    db.NewParam("DocID", saveinsert),
                    db.NewParam("UserID", Session("UserID")),
                    db.NewParam("Name", Session("Name")),
                    db.NewParam("Email", Session("Email")),
                    db.NewParam("Mobile", Session("Mobile")),
                    db.NewParam("Student_ID", Session("Student_ID")))


            Dim saveMasLog = db.GetValue("insert into MAS_DOC_LOG (DocID,Doc_Name,Doc_NameEN,Doc_dt_Save,Action,Post_Name) VALUES (@DocID,@Doc_Name,@Doc_NameEN,NOW(),'สร้าง',@Post_Name) RETURNING Doc_Log_ID",
                db.NewParam("DocID", saveinsert),
                db.NewParam("Doc_Name", NamprojectTH.Text),
                db.NewParam("Doc_NameEN", NameprojectEN.Text),
                db.NewParam("Post_Name", Session("Name")))





            'If FileUpload1.HasFile Then
            '    Dim iUploadedCnt As Integer = 0
            '    Dim iFailedCnt As Integer = 0
            '    Dim hfc As HttpFileCollection = Request.Files

            '    lblFileList.Text = "Select <b>" & hfc.Count & "</b> file(s)"

            '    If hfc.Count <= 10 Then
            '        For i As Integer = 0 To hfc.Count - 1
            '            Dim hpf As HttpPostedFile = hfc(i)
            '            If hpf.ContentLength > 0 Then
            '                Dim fileNameWithoutExtension As String = Path.GetFileNameWithoutExtension(hpf.FileName)
            '                Dim targetFolder As String = ""

            '                ' ตรวจสอบชื่อไฟล์และกำหนดโฟลเดอร์เป้าหมายตามชื่อไฟล์
            '                If fileNameWithoutExtension.StartsWith("บทที่ 1") Then
            '                    targetFolder = "Chapter1"
            '                ElseIf fileNameWithoutExtension.StartsWith("บทที่ 2") Then
            '                    targetFolder = "Chapter2"
            '                ElseIf fileNameWithoutExtension.StartsWith("บทที่ 3") Then
            '                    targetFolder = "Chapter3"
            '                ElseIf fileNameWithoutExtension.StartsWith("บทที่ 4") Then
            '                    targetFolder = "Chapter4"
            '                ElseIf fileNameWithoutExtension.StartsWith("บทที่ 5") Then
            '                    targetFolder = "Chapter5"
            '                End If

            '                If targetFolder <> "" Then
            '                    ' ตรวจสอบการมีอยู่ของโฟลเดอร์เป้าหมายและสร้างโฟลเดอร์ถ้าไม่มี
            '                    If Not Directory.Exists(Server.MapPath("Docs\") & targetFolder) Then
            '                        Directory.CreateDirectory(Server.MapPath("Docs\") & targetFolder)
            '                    End If

            '                    ' ตรวจสอบความซ้ำซ้อนของไฟล์
            '                    If Not File.Exists(Server.MapPath("Docs\") & targetFolder & "\" & hpf.FileName) Then
            '                        ' บันทึกไฟล์ลงในโฟลเดอร์เป้าหมาย
            '                        hpf.SaveAs(Server.MapPath("Docs\") & targetFolder & "\" & hpf.FileName)
            '                        iUploadedCnt = iUploadedCnt + 1

            '                        ' เพิ่มรายการไฟล์ลงในฐานข้อมูล
            '                        Dim saveFile = db.GetValue("INSERT INTO Doc_File (DocID, Doc_File) VALUES (@DocID, @Doc_File) RETURNING Doc_File_ID",
            '                            db.NewParam("DocID", saveinsert),
            '                            db.NewParam("Doc_File", hpf.FileName))

            '                        ' Insert the record into Doc_File_Log
            '                        Dim saveLog = db.Execute("INSERT INTO Doc_File_Log (Doc_File_ID, DocID, Doc_File,Post_Name, Date_Delete, Action) VALUES (@Doc_File_ID, @DocID, @Doc_File,@Post_Name, NOW(), 'เพิ่ม')",
            '                           db.NewParam("DocID", saveinsert),
            '                           db.NewParam("Doc_File_ID", saveFile),
            '                           db.NewParam("Post_Name", Session("Name")),
            '                           db.NewParam("Doc_File", hpf.FileName))
            '                    Else
            '                        ' ไฟล์ซ้ำ ไม่ทำการบันทึก
            '                        iFailedCnt = iFailedCnt + 1
            '                    End If
            '                Else
            '                    ' ไม่รู้จักบทหรือไม่ตรงกับบทที่มีโฟลเดอร์เป้าหมาย
            '                    iFailedCnt = iFailedCnt + 1
            '                End If
            '            End If
            '        Next i

            '        lblUploadStatus.Text = "<b>" & iUploadedCnt & "</b> file(s) Uploaded."
            '        lblFailedStatus.Text = "<b>" & iFailedCnt & "</b> duplicate file(s) or files with incorrect chapters could not be uploaded."
            '    Else
            '        lblUploadStatus.Text = "Max. 10 files allowed."
            '    End If
            'Else
            '    lblUploadStatus.Text = "No files selected."
            'End If

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
            '                            Dim saveFile = db.GetValue("INSERT INTO Doc_File (DocID, Doc_File) VALUES (@DocID, @Doc_File) RETURNING Doc_File_ID",
            '                                db.NewParam("DocID", saveinsert),
            '                                db.NewParam("Doc_File", hpf.FileName))

            '                            ' Insert the record into Doc_File_Log
            '                            Dim saveLog = db.Execute("INSERT INTO Doc_File_Log (Doc_File_ID, DocID, Doc_File,Post_Name, Date_Delete, Action) VALUES (@Doc_File_ID, @DocID, @Doc_File,@Post_Name, NOW(), 'เพิ่ม')",
            '                               db.NewParam("DocID", saveinsert),
            '                               db.NewParam("Doc_File_ID", saveFile),
            '                               db.NewParam("Post_Name", Session("Name")),
            '                               db.NewParam("Doc_File", hpf.FileName))

            '                            If (IsPostBack) Then
            '                                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'เพิ่มข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
            '                            End If

            '                        End If
            '                    End If
            '                End If
            '            Next i
            '            lblUploadStatus.Text = "<b>" & iUploadedCnt & "</b> อัพโหลดสำเร็จ"
            '            'lblFailedStatus.Text = "<b>" & iFailedCnt & "</b> duplicate file(s) could not be uploaded."
            '        Else
            '            lblUploadStatus.Text = "Max. 10 files allowed."
            '        End If
            '    Else
            '        lblUploadStatus.Text = "No files selected."
            '    End If
            'End If



            If FileUpload1.HasFile Then
                Dim iUploadedCnt As Integer = 0
                Dim hfc As HttpFileCollection = Request.Files

                lblFileList.Text = "Select <b>" & hfc.Count & "</b> file(s)"

                If hfc.Count <= 10 Then ' 10 FILES RESTRICTION.
                    For i As Integer = 0 To hfc.Count - 1
                        Dim hpf As HttpPostedFile = hfc(i)
                        If hpf.ContentLength > 0 AndAlso UploadSecurity.IsAllowedDocument(hpf.FileName) Then
                            Dim fileName As String = Path.GetFileName(hpf.FileName)

                            ' Save the file in a folder.
                            hpf.SaveAs(Server.MapPath("Docs\") & fileName)
                            iUploadedCnt = iUploadedCnt + 1

                            ' Insert the record into Doc_File
                            Dim saveFile = db.GetValue("INSERT INTO Doc_File (DocID, Doc_File) VALUES (@DocID, @Doc_File) RETURNING Doc_File_ID",
                    db.NewParam("DocID", saveinsert),
                    db.NewParam("Doc_File", fileName))

                            ' Insert the record into Doc_File_Log
                            Dim saveLog = db.Execute("INSERT INTO Doc_File_Log (Doc_File_ID, DocID, Doc_File,Post_Name, Date_Delete, Action) VALUES (@Doc_File_ID, @DocID, @Doc_File,@Post_Name, NOW(), 'เพิ่ม')",
                    db.NewParam("DocID", saveinsert),
                    db.NewParam("Doc_File_ID", saveFile),
                    db.NewParam("Post_Name", Session("Name")),
                    db.NewParam("Doc_File", fileName))
                        End If
                    Next i
                    lblUploadStatus.Text = "<b>" & iUploadedCnt & "</b> อัพโหลดสำเร็จ"
                Else
                    lblUploadStatus.Text = "Max. 10 files allowed."
                End If
            Else
                lblUploadStatus.Text = "No files selected."
            End If

        End If

        If (IsPostBack) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'เพิ่มข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
        End If






    End Sub





    'db.NewParam("Doc_Type", doctype.Text))

    'db.NewParam("Student_Name2", Name_lname2.Text),
    'db.NewParam("Student_ID2", Studentid2.Text),
    'db.NewParam("Mobile2", Phonenumber2.Text),





    'If FileUpload1.HasFile Then
    '    FileUpload1.SaveAs(Server.MapPath(FileUpload1.FileName))
    'End If





    'If FileUpload1.HasFile Then
    'Dim filename As String
    '    filename = Path.GetFileName(FileUpload1.FileName)
    '    FileUpload1.SaveAs(Server.MapPath("Docs/") + filename)

    'End If
    'If FileUpload1.HasFile Then
    'Dim fileEmp_Name As String = FileUpload1.FileName
    'Dim exten As String = Path.GetExtension(fileEmp_Name)
    '    'here we have to restrict file type           
    '    exten = exten.ToLower()
    '    Dim acceptedFileTypes As String() = New String(1) {}
    '    acceptedFileTypes(0) = ".pdf"
    '    acceptedFileTypes(1) = ".docx"
    '    Dim acceptFile As Boolean = False
    'For i As Integer = 0 To 1
    'If exten = acceptedFileTypes(i) Then
    '            acceptFile = True
    '        End If
    'Next
    'Dim r = Now.Ticks
    '    FileUpload1.SaveAs(Server.MapPath(Convert.ToString("Docs/") & r & exten))
    '    Dim filename = r & exten



    '    db.Execute("update mas_doc set doc_file = @doc_file where docid = @docid ",
    '               db.NewParam("doc_file", filename),
    '               db.NewParam("docid", saveinsert))


    'End If
    'If FileUpload1.HasFile Then
    'Dim filename As String
    '    filename = Path.GetFileName(FileUpload1.FileName)
    '    FileUpload1.SaveAs(Server.MapPath("Docs/") + filename)


    'End If


    Private Sub production_version_Insert_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim db = PgDb.Platform()
        If db.GetValue("SELECT COUNT(*) AS COUNT FROM MAS_DOC INNER JOIN Permission_TB ON MAS_DOC.DocID = Permission_TB.DocID GROUP BY MAS_DOC.DocID, Permission_TB.Email HAVING (Permission_TB.Email = @Email) ", db.NewParam("Email", Session("Email"))) > 0 Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ไม่สามารถเพิ่มได้', text: 'เนื่องจากท่านมีเอกสารโปรเจคอยู่แล้ว!', icon: 'warning', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
            'Response.Redirect("index.aspx")
        Else

        End If
    End Sub



    'Protected Sub btnalert(sender As Object, e As EventArgs) Handles Button2.Click
    '    Dim db = PgDb.Platform()
    '    Dim saveinsert = db.GetValue("insert into MAS_DOC (Student_Name,Mobile,Doc_Name,Doc_NameEN,Doc_Type,Doc_File,Email) VALUES (@Student_Name,@Mobile,@Doc_Name,@Doc_NameEN,@Doc_Type,@Doc_File,@Email) RETURNING DocID",
    '                db.NewParam("Student_Name", txtname.Text),
    '                db.NewParam("Mobile", txtphonenumber.Text),
    '                db.NewParam("Doc_Name", txtnamedoc.Text),
    '                db.NewParam("Doc_NameEN", txtnamedocen.Text),
    '                db.NewParam("Doc_Type", doctype.Text),
    '                db.NewParam("Doc_File", FileUpload1.FileName),
    '                db.NewParam("Email", Session("Email")))


    'ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert",
    '    "swal('เพิ่มข้อมูลสำเร็จ!', 'คลิก OK เพื่อไปหน้าต่อไป!', 'success')", True)









    'If FileUpload1.HasFile Then
    '    FileUpload1.SaveAs(Server.MapPath(FileUpload1.FileName))
    'End If
    'If FileUpload1.HasFile Then
    '        Dim fileEmp_Name As String = FileUpload1.FileName
    '        Dim exten As String = Path.GetExtension(fileEmp_Name)
    '        'here we have to restrict file type           
    '        exten = exten.ToLower()
    '        Dim acceptedFileTypes As String() = New String(1) {}
    '        acceptedFileTypes(0) = ".pdf"
    '        acceptedFileTypes(1) = ".docx"
    '        Dim acceptFile As Boolean = False
    '        For i As Integer = 0 To 1
    '            If exten = acceptedFileTypes(i) Then
    '                acceptFile = True
    '            End If
    '        Next
    '        Dim r = Now.Ticks
    '        FileUpload1.SaveAs(Server.MapPath(Convert.ToString("Docs/") & r & exten))
    '        Dim filename = r & exten

    '    End If

    '    db.Close()
    'End Sub
End Class
