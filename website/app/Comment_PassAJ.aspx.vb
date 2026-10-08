
Imports Npgsql
Imports DevExpress.Web
Imports DevExpress.XtraRichEdit.API.Layout

Partial Class production_version_Doc_AllAJ
    Inherits System.Web.UI.Page

    Private Sub production_version_Doc_AllAJ_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim docID As String = Request.QueryString("DocID")
        Dim permissionID As String = Request.QueryString("Permission_ID")
    End Sub








    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Dim db = PgDb.Platform()
        Dim docID As String = Request.QueryString("DocID")
        Dim permissionID As String = Request.QueryString("Permission_ID")

        If String.IsNullOrEmpty(commentText.Text) Then
            ' หากว่างเปล่าให้แสดงข้อความผิดพลาด
            RequiredFieldValidator14.ErrorMessage = "กรุณาคอมเมนต์ก่อนกดบันทึก"
            RequiredFieldValidator14.IsValid = False
        Else
            ' เช็คว่าคอมเมนต์ซ้ำหรือไม่ก่อน insert
            Dim commentExists As Boolean = CheckCommentExists(docID, Session("Email"), commentText.Text)

            If Not commentExists Then
                Dim saveinsert = db.GetValue("insert into Comment_TB (Permission_ID,DocID,Email,Name,Comment_Text,Date_Save,Post_Name_RS) VALUES (@Permission_ID,@DocID,@Email,@Name,@Comment_Text,NOW(),'อาจารย์')",
                      db.NewParam("Permission_ID", permissionID),
                      db.NewParam("DocID", docID),
                      db.NewParam("Email", Session("Email")),
                      db.NewParam("Name", Session("Name")),
                      db.NewParam("Comment_Text", commentText.Text))
            End If
        End If

        db.Close()
        If (IsPostBack) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'เพิ่มข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)
        End If
    End Sub

    Private Function CheckCommentExists(ByVal docID As String, ByVal email As String, ByVal commentText As String) As Boolean
        ' เชื่อมต่อกับฐานข้อมูล
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Using db As New NpgsqlConnection(connectionString)
            db.Open()

            ' ค้นหาคอมเมนต์ที่มีค่าเดียวกันในฐานข้อมูล
            Dim query As String = "SELECT COUNT(*) FROM Comment_TB WHERE DocID = @DocID AND Email = @Email AND Comment_Text = @CommentText"
            Using cmd As New NpgsqlCommand(query, db)
                cmd.Parameters.Add(PgDb.Param("@DocID", docID))
                cmd.Parameters.Add(PgDb.Param("@Email", email))
                cmd.Parameters.Add(PgDb.Param("@CommentText", commentText))

                ' นับจำนวนคอมเมนต์ที่ตรงเงื่อนไข
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' ถ้ามีคอมเมนต์ที่ซ้ำกันในฐานข้อมูลให้คืนค่า True
                ' ถ้าไม่มีคอมเมนต์ที่ซ้ำกันให้คืนค่า False
                Return count > 0
            End Using
        End Using
    End Function


    Protected Sub Button1_Click(sender As Object, e As System.EventArgs)
        Dim db = PgDb.Platform()

        Dim email As String = Session("Email")
        Dim docID As String = Request.QueryString("DocID")
        Dim permissionID As String = Request.QueryString("Permission_ID")

        Dim permissionStatusObject As Object = db.GetValue("SELECT Submit_TC FROM Permission_TB WHERE DocID = @DocID AND Permission_ID = @Permission_ID",
                                             db.NewParam("DocID", docID),
                                             db.NewParam("Permission_ID", permissionID))

        If Not IsDBNull(permissionStatusObject) Then
            ' ถ้าค่า Submit_TC ใน Permission_TB ไม่เท่ากับ DBNull
            Dim permissionStatus As String = permissionStatusObject.ToString()
            If permissionStatus = "ผ่าน" Then
                ' แจ้งเตือนว่าอาจารย์ที่ล็อคอินอยู่เคยกดผ่านแล้ว
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ไม่สามารถกดผ่านได้', text: 'คุณได้ทำการกดผ่านเอกสารนี้แล้ว!', icon: 'warning', confirmButtonText: 'OK' });", True)
                db.Close()
                Return ' จบการทำงานทันที
            ElseIf permissionStatus = "ไม่ผ่าน" Then
                ' แจ้งเตือนว่าอาจารย์ที่ล็อคอินอยู่เคยกด "ไม่ผ่าน" แล้ว
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ไม่สามารถกดผ่านได้', text: 'คุณได้ทำการกด ไม่ผ่าน เอกสารนี้แล้ว!', icon: 'warning', confirmButtonText: 'OK' });", True)
                db.Close()
                Return ' จบการทำงานทันที
            End If
        End If


        ' ตรวจสอบค่า Submit_TC จากฐานข้อมูล
        Dim submitTCObject As Object = db.GetValue("SELECT Submit_TC FROM MAS_DOC WHERE DocID = @DocID",
                                         db.NewParam("DocID", docID))

        ' ตรวจสอบค่า Submit_TC และดำเนินการตามเงื่อนไข
        If submitTCObject = "ยังไม่มีการกดผ่าน/ไม่ผ่าน" Then
            Dim updateMASDOCTC1 = db.Execute("UPDATE MAS_DOC SET Submit_TC = 'ผ่าน1/3' WHERE DocID = @DocID",
                db.NewParam("DocID", docID))
        ElseIf submitTCObject = "ผ่าน1/3" Then
            Dim updateMASDOCTC2 = db.Execute("UPDATE MAS_DOC SET Submit_TC = 'ผ่าน2/3' WHERE DocID = @DocID",
                db.NewParam("DocID", docID))
        ElseIf submitTCObject = "ผ่าน2/3" Then
            Dim updateMASDOCTC3 = db.Execute("UPDATE MAS_DOC SET Submit_TC = 'ผ่าน3/3' WHERE DocID = @DocID",
                db.NewParam("DocID", docID))
        End If




        ' ทำการอัปเดตใน Permission_TB ตามเงื่อนไขเดิมที่คุณได้เขียนไว้ในฟังก์ชันนี้
        Dim updatePermission = db.Execute("UPDATE Permission_TB SET Submit_TC = 'ผ่าน' WHERE DocID = @DocID AND Permission_ID = @Permission_ID",
                db.NewParam("DocID", docID),
                db.NewParam("Permission_ID", permissionID))

        If (IsPostBack) Then
            ' แสดง Alert และ Redirect หน้าเมื่อกดปุ่มเสร็จสิ้น
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'กดผ่านสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)
        End If

        db.Close()
    End Sub







    Protected Sub Button2_Click(sender As Object, e As System.EventArgs)
        Dim db = PgDb.Platform()
        Dim docID As String = Request.QueryString("DocID")
        Dim permissionID As String = Request.QueryString("Permission_ID")

        Dim permissionStatusObject As Object = db.GetValue("SELECT Submit_TC FROM Permission_TB WHERE DocID = @DocID AND Permission_ID = @Permission_ID",
                                             db.NewParam("DocID", docID),
                                             db.NewParam("Permission_ID", permissionID))

        If Not IsDBNull(permissionStatusObject) Then
            ' ถ้าค่า Submit_TC ใน Permission_TB ไม่เท่ากับ DBNull
            Dim permissionStatus As String = permissionStatusObject.ToString()
            If permissionStatus = "ผ่าน" Then
                ' แจ้งเตือนว่าอาจารย์ที่ล็อคอินอยู่เคยกดผ่านแล้ว
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ไม่สามารถกดผ่านได้', text: 'คุณได้ทำการกดผ่านเอกสารนี้แล้ว!', icon: 'warning', confirmButtonText: 'OK' });", True)
                db.Close()
                Return ' จบการทำงานทันที
            ElseIf permissionStatus = "ไม่ผ่าน" Then
                ' แจ้งเตือนว่าอาจารย์ที่ล็อคอินอยู่เคยกด "ไม่ผ่าน" แล้ว
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ไม่สามารถกดผ่านได้', text: 'คุณได้ทำการกด ไม่ผ่าน เอกสารนี้แล้ว!', icon: 'warning', confirmButtonText: 'OK' });", True)
                db.Close()
                Return ' จบการทำงานทันที
            End If
        End If


        Dim updatePermission = db.Execute("UPDATE Permission_TB SET Submit_TC = 'ไม่ผ่าน' WHERE DocID = @DocID AND Permission_ID = @Permission_ID",
               db.NewParam("DocID", docID),
               db.NewParam("Permission_ID", permissionID))

        Dim updateMASDOCTC1 = db.Execute("UPDATE MAS_DOC SET Submit_TC = 'ไม่ผ่าน' WHERE DocID = @DocID",
                db.NewParam("DocID", docID))

        If (IsPostBack) Then
            ' แสดง Alert และ Redirect หน้าเมื่อกดปุ่มเสร็จสิ้น
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'กดไม่ผ่านสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)
        End If

    End Sub






    ' ฟังก์ชันตรวจสอบความซ้ำของคอมเมนต์


    'Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
    '    Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
    '    Dim db As New NpgsqlConnection(connectionString)
    '    db.Open()


    '    Dim docID As String = Request.QueryString("DocID")
    '    Dim permissionID As String = Request.QueryString("Permission_ID")

    '    Dim queryInsertCM As String = "INSERT INTO Comment_TB (Permission_ID, DocID, Email, Name, Comment_Text, Date_Save) VALUES (@Permission_ID, @DocID, @Email, @Name, @Comment_Text, NOW())"
    '    Using cmd As New NpgsqlCommand(queryInsertCM, db)
    '        cmd.Parameters.Add(PgDb.Param("@Permission_ID", permissionID))
    '        cmd.Parameters.Add(PgDb.Param("@DocID", docID))
    '        cmd.Parameters.Add(PgDb.Param("@Email", Session("Email")))
    '        cmd.Parameters.Add(PgDb.Param("@Name", Session("Name")))
    '        cmd.Parameters.Add(PgDb.Param("@Comment_Text", commentText.Text))
    '        cmd.ExecuteNonQuery()
    '    End Using

    '    Dim queryInsertPS As String = "insert into Permission_TB (DocID,Submit_TC) VALUES (@DocID, 'ผ่าน')"
    '    Using cmd As New NpgsqlCommand(queryInsertPS, db)
    '        cmd.Parameters.Add(PgDb.Param("@DocID", docID))
    '        cmd.ExecuteNonQuery()
    '    End Using


    '    ' ตรวจสอบค่า Submit_TC จากตาราง MAS_DOC
    '    ' ตรวจสอบค่า Submit_TC จากตาราง MAS_DOC
    '    Dim submitTC As String = ""
    '    Dim queryCheckSubmitTC As String = "SELECT Submit_TC FROM MAS_DOC WHERE DocID = @DocID"
    '    Using cmd As New NpgsqlCommand(queryCheckSubmitTC, db)
    '        cmd.Parameters.Add(PgDb.Param("@DocID", docID))
    '        Dim result = cmd.ExecuteScalar()
    '        If result IsNot DBNull.Value Then
    '            submitTC = DirectCast(result, String)
    '        Else
    '            ' หากค่า Submit_TC เป็น DBNull ให้กำหนดค่าเริ่มต้นที่ต้องการที่นี่
    '            ' submitTC = "ค่าเริ่มต้นที่ต้องการ"
    '        End If
    '    End Using

    '    ' ตรวจสอบค่า Submit_TC และ insert ข้อมูลตามเงื่อนไข
    '    Dim commentTextValue As String = commentText.Text ' รับค่าจาก TextBox คอมเมนต์

    '    If submitTC = "" Then
    '        ' อัพเดตค่า Submit_TC ในตาราง MAS_DOC เป็น "ผ่าน1/3"
    '        Dim queryUpdateSubmitTC As String = "UPDATE MAS_DOC SET Submit_TC = 'ผ่าน1/3' WHERE DocID = @DocID"
    '        Using cmd As New NpgsqlCommand(queryUpdateSubmitTC, db)
    '            cmd.Parameters.Add(PgDb.Param("@DocID", docID))
    '            cmd.ExecuteNonQuery()
    '        End Using
    '    ElseIf submitTC = "ผ่าน1/3" Then
    '        ' อัพเดตค่า Submit_TC ในตาราง MAS_DOC เป็น "ผ่าน2/3"
    '        Dim queryUpdateSubmitTC As String = "UPDATE MAS_DOC SET Submit_TC = 'ผ่าน2/3' WHERE DocID = @DocID"
    '        Using cmd As New NpgsqlCommand(queryUpdateSubmitTC, db)
    '            cmd.Parameters.Add(PgDb.Param("@DocID", docID))
    '            cmd.ExecuteNonQuery()
    '        End Using
    '    ElseIf submitTC = "ผ่าน2/3" Then
    '        ' อัพเดตค่า Submit_TC ในตาราง MAS_DOC เป็น "ผ่าน3/3"
    '        Dim queryUpdateSubmitTC As String = "UPDATE MAS_DOC SET Submit_TC = 'ผ่าน3/3' WHERE DocID = @DocID"
    '        Using cmd As New NpgsqlCommand(queryUpdateSubmitTC, db)
    '            cmd.Parameters.Add(PgDb.Param("@DocID", docID))
    '            cmd.ExecuteNonQuery()
    '        End Using
    '    End If


    '    db.Close()
    'End Sub


End Class
