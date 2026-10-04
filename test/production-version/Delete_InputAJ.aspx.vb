
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web

Partial Class production_version_EditUploadfile
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim docID As String = Request.QueryString("docID")
            ' Use the docID value as needed
        End If
    End Sub

    Protected Sub DeleteAJButton_Click(sender As Object, e As EventArgs)
        ' Get the Permission_ID from the command argument
        Dim permissionId As Integer = Convert.ToInt32(DirectCast(sender, Button).CommandArgument)

        ' Delete the record from the database
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Using command As New SqlCommand("DELETE FROM Permission_TB WHERE Permission_ID = @Permission_ID", connection)
                command.Parameters.AddWithValue("@Permission_ID", permissionId)
                command.ExecuteNonQuery()
            End Using
        End Using

        ' Rebind the data to refresh the GridView
        GridView2.DataBind()
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
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ไม่สามารถเพิ่มข้อมูล', text: 'เนื่องจากอีเมลล์นี้ไม่ใช่อาจารย์!', icon: 'warning', confirmButtonText: 'OK' });", True)
            Return
        End If

        ' ตรวจสอบว่ามี DocID และ Email ที่ต้องการเพิ่มอยู่ในฐานข้อมูลหรือไม่
        Dim existingPermissionTable = db.GetDataTable("SELECT * FROM Permission_TB WHERE DocID = @DocID AND Email = @Email",
                                               db.NewParam("DocID", docID),
                                               db.NewParam("Email", EmailAJ.Text))

        If existingPermissionTable IsNot Nothing AndAlso existingPermissionTable.Rows.Count > 0 Then
            Dim existingPermission = existingPermissionTable.Rows(0)
            ' ข้อมูลซ้ำ ไม่ทำการเพิ่มข้อมูล
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ข้อมูลซ้ำ', text: 'ไม่สามารถเพิ่มข้อมูลได้เนื่องจากเอกสารนี้มีอีเมลล์นี้อยู่แล้ว!', icon: 'warning', confirmButtonText: 'OK' });", True)
            Return
        End If

        Dim teacherPermissionCount = db.GetValue("SELECT COUNT(*) FROM Permission_TB WHERE DocID = @DocID AND Urole = '2'",
                                             db.NewParam("DocID", docID))

        If teacherPermissionCount >= 3 Then
            ' มี Email ที่ Urole_Permis เป็น "Teacher" มากกว่าหรือเท่ากับ 3 รายการแล้ว ไม่ทำการเพิ่มข้อมูล
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'ข้อมูลเกิน', text: 'ไม่สามารถเพิ่มข้อมูลเนื่องจากมีข้อมูลอาจารย์ครบแล้ว!', icon: 'warning', confirmButtonText: 'OK' });", True)
            Return
        End If

        ' ตรวจสอบว่าเป็น "อาจารย์ที่ปรึกษา" และมี Email เดียวเท่านั้น
        If BootstrapComboBox1.Text = "ที่ปรึกษา" Then
            Dim advisorPermissionCount = db.GetValue("SELECT COUNT(*) FROM Permission_TB WHERE DocID = @DocID AND Urole_TC = 'ที่ปรึกษา'",
                                                      db.NewParam("DocID", docID))

            If advisorPermissionCount > 0 Then
                ' มีการเพิ่มข้อมูลที่ปรึกษาอยู่แล้ว ไม่ทำการเพิ่มข้อมูล
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'เพิ่มข้อมูลไม่สำเร็จ', text: 'ไม่สามารถเพิ่มข้อมูลเนื่องจาก อาจารย์ที่ปรึกษาสามารถมีได้แค่คนเดียวเท่านั้น!', icon: 'warning', confirmButtonText: 'OK' });", True)
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

End Class
