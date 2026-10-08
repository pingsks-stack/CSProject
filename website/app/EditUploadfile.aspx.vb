
Imports System.IO

Partial Class production_version_EditUploadfile
    Inherits System.Web.UI.Page


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim db = PgDb.Platform()
        If FileUpload1.HasFile Then
            Dim hfc As HttpFileCollection = Request.Files
            Dim docID As String = Request.QueryString("DocID")

            ' เพิ่มส่วนนี้เพื่อกำหนดค่าให้กับตัวแปร hpf
            Dim hpf As HttpPostedFile = TryCast(hfc(0), HttpPostedFile)

            ' ตรวจสอบ hpf และดำเนินการต่อ
            If hpf IsNot Nothing AndAlso hpf.ContentLength > 0 Then
                Dim fileName As String = Path.GetFileName(hpf.FileName)

                If Not UploadSecurity.IsAllowedDocument(fileName) Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ไม่อนุญาต', text: 'ไม่รองรับไฟล์ประเภทนี้', icon: 'error', confirmButtonText: 'OK' });", True)
                    Return
                End If

                Dim existingFileID As Integer = db.GetValue("SELECT Doc_File_ID FROM Doc_File WHERE DocID = @DocID AND Doc_File = @Doc_File",
                    db.NewParam("DocID", docID),
                    db.NewParam("Doc_File", fileName))

                If existingFileID <> Nothing Then
                    ' File with the same name already exists, update the file
                    hpf.SaveAs(Server.MapPath("Docs\") & fileName)

                    ' Update the record in Doc_File
                    Dim updateFile = db.Execute("UPDATE Doc_File SET Doc_File = @Doc_File WHERE Doc_File_ID = @Doc_File_ID",
                        db.NewParam("Doc_File", fileName),
                        db.NewParam("Doc_File_ID", existingFileID))

                    ' Check if there is a log entry for the updated file
                    Dim existingLogID As Integer = db.GetValue("SELECT Doc_File_Log_ID FROM Doc_File_Log WHERE Doc_File_ID = @Doc_File_ID AND Action = 'Update'",
                        db.NewParam("Doc_File_ID", existingFileID))

                    If existingLogID = Nothing Then
                        ' Insert the record into Doc_File_Log for updated file
                        Dim saveLog = db.Execute("INSERT INTO Doc_File_Log (Doc_File_ID, DocID, Doc_File, Post_Name, Date_Delete, Action) VALUES (@Doc_File_ID, @DocID, @Doc_File, @Post_Name, NOW(), 'แทนที่ไฟล์')",
                            db.NewParam("DocID", docID),
                            db.NewParam("Doc_File_ID", existingFileID),
                            db.NewParam("Post_Name", Session("Name")),
                            db.NewParam("Doc_File", fileName))

                        ' Show success message for file update
                        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'อัพเดตไฟล์สำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
                    Else
                        ' Show warning message that file with the same name already exists
                        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'คำเตือน', text: 'มีไฟล์ที่มีชื่อเดียวกันอยู่แล้ว!', icon: 'warning', confirmButtonText: 'OK' });", True)
                    End If
                Else
                    ' File with the same name does not exist, insert new file
                    hpf.SaveAs(Server.MapPath("Docs\") & fileName)

                    ' Insert the record into Doc_File
                    Dim saveFile = db.GetValue("INSERT INTO Doc_File (DocID, Doc_File) VALUES (@DocID, @Doc_File) RETURNING Doc_File_ID",
                        db.NewParam("DocID", docID),
                        db.NewParam("Doc_File", fileName))

                    ' Insert the record into Doc_File_Log
                    Dim saveLog = db.Execute("INSERT INTO Doc_File_Log (Doc_File_ID, DocID, Doc_File, Post_Name, Date_Delete, Action) VALUES (@Doc_File_ID, @DocID, @Doc_File, @Post_Name, NOW(), 'เพิ่มไฟล์')",
                        db.NewParam("DocID", docID),
                        db.NewParam("Doc_File_ID", saveFile),
                        db.NewParam("Post_Name", Session("Name")),
                        db.NewParam("Doc_File", fileName))

                    ' Show success message for file upload
                    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'อัพโหลดไฟล์สำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
                End If

            End If

        End If



        If IsPostBack Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'อัพโหลดไฟล์สำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
        End If
    End Sub
End Class
