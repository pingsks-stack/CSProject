Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Zulu.Data
Imports DevExpress.Web
Imports System.IdentityModel.Tokens
Imports System.Diagnostics
Imports System.Security.Policy

Partial Class production_version_Doc_Student
    Inherits System.Web.UI.Page




    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' อ่านค่า docID และ uRole จาก URL
            Dim docID As String = Request.QueryString("docID")
            Dim uRole As String = Request.QueryString("uRole")

            ' ตรวจสอบค่า uRole
            'If uRole = "ที่ปรึกษา" Then
            '    ' ถ้า uRole เป็น "ที่ปรึกษา" ให้แสดง Alert
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('คุณเป็นที่ปรึกษา');", True)

            '    ' ซ่อนปุ่มใน GridView

            'Else
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('คุณไม่มีสิทธ');", True)
            '    ' ถ้าไม่ใช่ "ที่ปรึกษา" คุณสามารถดำเนินการอื่น ๆ ตามที่คุณต้องการ
            'End If
        End If
    End Sub







    Function get_disabled1(emailInGrid As String) As String
        Dim ret As String = ""

        Dim userEmail As String = Session("Email").ToString()
        If emailInGrid <> userEmail Then
            ret = "style='background-color: grey; cursor: not-allowed;' disabled"
        End If

        Return ret
    End Function



    Protected Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim emailInGrid As String = DataBinder.Eval(e.Row.DataItem, "Email").ToString()
            Dim button As Button = DirectCast(e.Row.FindControl("YourButtonID"), Button)

            If button IsNot Nothing AndAlso emailInGrid <> Session("Email").ToString() Then
                button.Enabled = False
                button.CssClass = "btn btn-secondary"
            End If
        End If
    End Sub


    Public ReadOnly Property docID() As String
        Get
            If ViewState("docID") IsNot Nothing Then
                Return ViewState("docID").ToString()
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property uRoleTC() As String
        Get
            If ViewState("uRole_TC") IsNot Nothing Then
                Return ViewState("uRole_TC").ToString()
            Else
                Return ""
            End If
        End Get
    End Property



    'Protected Sub ConfirmButton_Click(sender As Object, e As EventArgs)
    '    Dim btnConfirm As Button = CType(sender, Button)
    '    If btnConfirm.CommandName = "Confirm" Then
    '        Dim dataItemIndex As Integer = Convert.ToInt32(btnConfirm.CommandArgument)

    '        ' ดึงข้อมูลจาก GridView
    '        Dim docID As String = GridView5.DataKeys(dataItemIndex).Values("DocID").ToString()
    '        Dim docName As String = GridView5.DataKeys(dataItemIndex).Values("Doc_Name").ToString()
    '        Dim docNameEN As String = GridView5.DataKeys(dataItemIndex).Values("Doc_NameEN").ToString()
    '        Dim postName As String = GridView5.DataKeys(dataItemIndex).Values("Post_Name").ToString()

    '        ' ดำเนินการอัปเดตข้อมูลในตาราง MAS_DOC
    '        Dim updateMasDocQuery As String = "UPDATE MAS_DOC SET Doc_Name = @DocName, Doc_NameEN = @DocNameEN WHERE DocID = @DocID"

    '        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString)
    '            Using cmd As New SqlCommand(updateMasDocQuery, con)
    '                cmd.Parameters.AddWithValue("@DocID", docID)
    '                cmd.Parameters.AddWithValue("@DocName", docName)
    '                cmd.Parameters.AddWithValue("@DocNameEN", docNameEN)

    '                con.Open()
    '                cmd.ExecuteNonQuery()
    '            End Using
    '        End Using

    '        ' เพิ่มรายการในตาราง MAS_DOC_LOG
    '        Dim insertMasDocLogQuery As String = "INSERT INTO MAS_DOC_LOG (DocID, Doc_Name, Doc_NameEN, Action, Post_Name, Doc_dt_Save) VALUES (@DocID, @Doc_Name, @Doc_NameEN, 'แก้ไข', @Post_Name, GETDATE())"

    '        Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString)
    '            Using cmd As New SqlCommand(insertMasDocLogQuery, con)
    '                cmd.Parameters.AddWithValue("@DocID", docID)
    '                cmd.Parameters.AddWithValue("@Doc_Name", docName)
    '                cmd.Parameters.AddWithValue("@Doc_NameEN", docNameEN)
    '                cmd.Parameters.AddWithValue("@Post_Name", postName)

    '                con.Open()
    '                cmd.ExecuteNonQuery()
    '            End Using
    '        End Using

    '        ' อัพเดตข้อมูลใน GridView หรือทำอย่างอื่นตามที่คุณต้องการ
    '        GridView5.DataBind()

    '        ' เพิ่มการแจ้งเตือนหรือกระทำอื่น ๆ ตามที่คุณต้องการ
    '        ' ...

    '        ' ตำแหน่งของเว็บหน้าหลังการอัพเดต
    '        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ยืนยันการเปลี่ยนแปลงชื่อโปรเจคแล้ว!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)
    '    End If
    'End Sub

    Protected Sub ConfirmButton_Click(sender As Object, e As EventArgs)
        Dim btnConfirm As Button = CType(sender, Button)
        If btnConfirm.CommandName = "Confirm" Then
            Dim dataItemIndex As Integer = Convert.ToInt32(btnConfirm.CommandArgument)

            Dim SubNPJ_ID As String = GridView5.DataKeys(dataItemIndex).Values("SubNPJ_ID").ToString()
            Dim docID As String = GridView5.DataKeys(dataItemIndex).Values("DocID").ToString()


            ' ต่อไปดำเนินการเหมือนเดิม...

            Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim masSubSelectQuery As String = "SELECT SubNPJ_ID, DocID, Doc_Name, Doc_NameEN, Post_Name, Date_Save, Action, Status FROM Sub_NPJ_TB WHERE SubNPJ_ID = @SubNPJ_ID"

                Using masSubSelectCommand As New SqlCommand(masSubSelectQuery, connection)
                    masSubSelectCommand.Parameters.AddWithValue("@SubNPJ_ID", SubNPJ_ID)
                    Using reader As SqlDataReader = masSubSelectCommand.ExecuteReader()
                        If reader.Read() Then
                            Dim DocName As String = reader("Doc_Name").ToString()
                            Dim DocNameEN As String = reader("Doc_NameEN").ToString()
                            Dim PostName As String = reader("Post_Name").ToString()
                            Dim Datesave As String = reader("Date_Save").ToString()
                            Dim Action As String = reader("Action").ToString()
                            Dim Status As String = reader("Status").ToString()

                            reader.Close() ' ปิด DataReader ก่อนใช้ ExecuteNonQuery()

                            Dim updateMasDocQuery As String = "UPDATE MAS_DOC SET Doc_Name = @DocName, Doc_NameEN = @DocNameEN WHERE DocID = @DocID"

                            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString)
                                Using cmd As New SqlCommand(updateMasDocQuery, con)
                                    cmd.Parameters.AddWithValue("@DocID", docID)
                                    cmd.Parameters.AddWithValue("@DocName", DocName)
                                    cmd.Parameters.AddWithValue("@DocNameEN", DocNameEN)

                                    con.Open()
                                    cmd.ExecuteNonQuery()
                                End Using
                            End Using

                            ' เพิ่มรายการในตาราง MAS_DOC_LOG
                            Dim insertMasDocLogQuery As String = "INSERT INTO MAS_DOC_LOG (DocID, Doc_Name, Doc_NameEN, Action, Post_Name, Doc_dt_Save) VALUES (@DocID, @Doc_Name, @Doc_NameEN, 'แก้ไข', @Post_Name, GETDATE())"

                            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString)
                                Using cmd As New SqlCommand(insertMasDocLogQuery, con)
                                    cmd.Parameters.AddWithValue("@DocID", docID)
                                    cmd.Parameters.AddWithValue("@Doc_Name", DocName)
                                    cmd.Parameters.AddWithValue("@Doc_NameEN", DocNameEN)
                                    cmd.Parameters.AddWithValue("@Post_Name", PostName)

                                    con.Open()
                                    cmd.ExecuteNonQuery()
                                End Using
                            End Using

                            Dim updateCF As String = "UPDATE Sub_NPJ_TB SET Action = 'ยืนยันแล้ว', Status = '1' WHERE SubNPJ_ID = @SubNPJ_ID"

                            Using con As New SqlConnection(ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString)
                                Using cmd As New SqlCommand(updateCF, con)
                                    cmd.Parameters.AddWithValue("@SubNPJ_ID", SubNPJ_ID)


                                    con.Open()
                                    cmd.ExecuteNonQuery()
                                End Using
                            End Using

                        End If
                    End Using
                End Using


                'Dim masSelectQuery As String = "SELECT Name, Email, Mobile, Student_ID FROM Status_Change_TB WHERE Status_ID = @Status_ID"

                'Using masSelectCommand As New SqlCommand(masSelectQuery, connection)
                '    masSelectCommand.Parameters.AddWithValue("@Status_ID", statusID)
                '    Using reader As SqlDataReader = masSelectCommand.ExecuteReader()
                '        If reader.Read() Then
                '            Dim name As String = reader("Name").ToString()
                '            Dim email As String = reader("Email").ToString()
                '            Dim mobile As String = reader("Mobile").ToString()
                '            Dim studentID As String = reader("Student_ID").ToString()

                '            reader.Close() ' ปิด DataReader ก่อนใช้ ExecuteNonQuery()

                '            ' ... คำสั่งเพิ่มและอัพเดทข้อมูลตามที่คุณต้องการ ...
                '            'Dim subInsertQuery As String = "INSERT INTO SubInfor_TB (DocID, Permission_ID, Name, Email, Mobile, Student_ID, Urole ,Action) VALUES (@DocID, @Permission_ID, @Name, @Email, @Mobile, @Student_ID, '1', 'คู่โปรเจคใหม่')"

                '            'Using subInsertCommand As New SqlCommand(subInsertQuery, connection)
                '            '    subInsertCommand.Parameters.AddWithValue("@DocID", docID)
                '            '    subInsertCommand.Parameters.AddWithValue("@Permission_ID", permissionID)
                '            '    subInsertCommand.Parameters.AddWithValue("@Name", name)
                '            '    subInsertCommand.Parameters.AddWithValue("@Email", email)
                '            '    subInsertCommand.Parameters.AddWithValue("@Mobile", mobile)
                '            '    subInsertCommand.Parameters.AddWithValue("@Student_ID", studentID)

                '            '    subInsertCommand.ExecuteNonQuery()
                '            'End Using

                '            Dim updateQuery As String = "UPDATE Permission_TB SET Email = @Email, Name = @Name, Mobile = @Mobile, Student_ID = @Student_ID WHERE Permission_ID = @Permission_ID AND Urole = '1'"

                '            Using updateCommand As New SqlCommand(updateQuery, connection)
                '                updateCommand.Parameters.AddWithValue("@Email", email)
                '                updateCommand.Parameters.AddWithValue("@Name", name)
                '                updateCommand.Parameters.AddWithValue("@Mobile", mobile)
                '                updateCommand.Parameters.AddWithValue("@Student_ID", studentID)
                '                updateCommand.Parameters.AddWithValue("@Permission_ID", permissionID)

                '                updateCommand.ExecuteNonQuery()
                '            End Using

                '            Dim masUpdateQuery As String = "UPDATE MAS_DOC SET Email2 = @Email2, Student_Name2 = @Student_Name2, Student_ID2 = @Student_ID2, Mobile2 = @Mobile2 WHERE DocID = @DocID"

                '            Using masUpdateCommand As New SqlCommand(masUpdateQuery, connection)
                '                masUpdateCommand.Parameters.AddWithValue("@Email2", email)
                '                masUpdateCommand.Parameters.AddWithValue("@Student_Name2", name)
                '                masUpdateCommand.Parameters.AddWithValue("@Student_ID2", studentID)
                '                masUpdateCommand.Parameters.AddWithValue("@Mobile2", mobile)
                '                masUpdateCommand.Parameters.AddWithValue("@DocID", docID)

                '                masUpdateCommand.ExecuteNonQuery()
                '            End Using

                '            Dim masStatusUpdateQuery As String = "UPDATE Status_Change_TB SET Status = 'ยืนยันแล้ว' WHERE DocID = @DocID"

                '            Using masStatusUpdateCommand As New SqlCommand(masStatusUpdateQuery, connection)
                '                masStatusUpdateCommand.Parameters.AddWithValue("@DocID", docID)
                '                masStatusUpdateCommand.ExecuteNonQuery()
                '            End Using
                '        End If
                '    End Using
                'End Using
            End Using

            ' ... คำสั่งอื่นๆ ที่คุณต้องการ ...

            ' อัพเดทข้อมูลใน GridView
            GridView5.DataBind()

            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ยืนยันการเปลี่ยนแปลงชื่อโปรเจคสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)
        End If

    End Sub



    Protected Sub RejectButton_Click(sender As Object, e As EventArgs)
        Dim btnReject As Button = CType(sender, Button)
        Dim dataItemIndex As Integer

        If Integer.TryParse(btnReject.CommandArgument, dataItemIndex) Then
            Dim SubNPJ_ID As String = GridView5.DataKeys(dataItemIndex).Values("SubNPJ_ID").ToString()



            ' ตามด้วยโค้ดอื่น ๆ ที่ต้องการในการปรับสถานะ
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

            Using connection As New SqlConnection(connectionString)
                connection.Open()

                Dim updateStatusQuery As String = "UPDATE Sub_NPJ_TB SET Action = 'ถูกปฏิเสธ', Status = '1' WHERE SubNPJ_ID = @SubNPJ_ID"

                Using updateStatusCommand As New SqlCommand(updateStatusQuery, connection)
                    updateStatusCommand.Parameters.AddWithValue("@SubNPJ_ID", SubNPJ_ID)
                    updateStatusCommand.ExecuteNonQuery()
                End Using
            End Using

            ' หลังจากทำการอัพเดทแล้ว คุณอาจต้องทำการอัพเดท GridView ใหม่เพื่อแสดงการเปลี่ยนแปลง
            GridView5.DataBind()

            ' แสดงข้อความแจ้งเตือนหรือแจ้งสถานะหลังจากปรับสถานะเรียบร้อย
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ยกเลิกการเปลี่ยนแปลงคู่โปรเจคสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)
        Else
            ' กรณีไม่สามารถแปลงค่าเป็น integer ได้
            ' คุณอาจจะต้องแจ้งเตือนผู้ใช้หรือดำเนินการอื่นตามความเหมาะสม

        End If
    End Sub


    'Protected Sub btnChangeProject_Click(sender As Object, e As EventArgs)
    '    ' หาว่าปุ่มถูกคลิกในแถวใด
    '    Dim btn As Button = DirectCast(sender, Button)
    '    Dim row As GridViewRow = DirectCast(btn.NamingContainer, GridViewRow)
    '    Dim uRoleTC As String = row.Cells(2).Text ' แนบคอลัมน์ที่เก็บ Urole_TC


    '    If uRoleTC = "ที่ปรึกษา" Then
    '        ' แสดงปุ่ม
    '        Response.Write("<script>alert('คุณเป็นที่ปรึกษา');</script>")
    '    Else
    '        ' ไม่แสดงปุ่มหรือแจ้งเตือนว่าไม่มีสิทธิ์
    '        Response.Write("<script>alert('คุณไม่มีสิทธิ์ในการทำรายการนี้');</script>")
    '    End If
    'End Sub


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



    'Protected Sub Label3_Click(sender As Object, e As EventArgs)
    '    Dim docID As String = Request.QueryString("docID")
    '    Response.Redirect("Infor_Pass_Rej.aspx?DocID=" & docID)
    'End Sub



    Protected Sub SaveSubmit_Click(sender As Object, e As EventArgs)
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim docID As String = Request.QueryString("DocID")
        Dim email As String = Session("Email")

        ' ตรวจสอบใน Permission_TB ว่าอาจารย์ที่ล็อคอินอยู่เคยกดผ่านแล้วหรือไม่
        Dim permissionStatusObject As Object = db.GetValue("SELECT Submit_TC FROM Permission_TB WHERE DocID = @DocID AND Email = @Email",
                                                     db.NewParam("DocID", docID),
                                                     db.NewParam("Email", email))

        Dim permissionStatus As String = Convert.ToString(permissionStatusObject)

        If Not String.IsNullOrEmpty(permissionStatus) Then
            ' แจ้งเตือนว่าอาจารย์ที่ล็อคอินอยู่เคยกดผ่านแล้ว ไม่สามารถกดได้อีก
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ไม่สามารถกดผ่านได้', text: 'คุณได้ทำการกดผ่านเอกสารนี้แล้ว!', icon: 'warning', confirmButtonText: 'OK' });", True)
        Else
            ' ทำการอัปเดตใน Permission_TB และ MAS_DOC ตามเงื่อนไขเดิมที่คุณได้เขียนไว้ในฟังก์ชันนี้
            Dim updatePerson = db.Execute("UPDATE Permission_TB SET Submit_TC = 'ผ่าน' WHERE DocID = @DocID AND Email = @Email",
                                db.NewParam("DocID", docID),
                                db.NewParam("Email", email))

            If db.GetValue("SELECT COUNT(Submit_TC) AS รวม FROM  dbo.Permission_TB GROUP BY DocID, Urole HAVING (Urole = N'2') AND (DocID = @DocID) ", db.NewParam("DocID", docID)) >= 3 Then
                Dim updateSubmit = db.Execute("UPDATE MAS_DOC SET Submit_TC = 'ผ่าน3/3' WHERE DocID = @DocID",
                                db.NewParam("DocID", docID))

            Else
                Dim count As Integer = db.GetValue("SELECT COUNT(Submit_TC) AS รวม FROM  dbo.Permission_TB GROUP BY DocID, Urole HAVING (Urole = N'2') AND (DocID = @DocID) ", db.NewParam("DocID", docID))

                If count = 1 Then
                    Dim updateSubmit1 = db.Execute("UPDATE MAS_DOC SET Submit_TC = 'ผ่าน1/3' WHERE DocID = @DocID",
                                db.NewParam("DocID", docID))
                ElseIf count = 2 Then
                    Dim updateSubmit2 = db.Execute("UPDATE MAS_DOC SET Submit_TC = 'ผ่าน2/3' WHERE DocID = @DocID",
                                db.NewParam("DocID", docID))
                End If
            End If
        End If
    End Sub








End Class
