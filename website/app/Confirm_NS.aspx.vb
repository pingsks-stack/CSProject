Imports System.Data
Imports Npgsql
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Bootstrap
Imports System.Diagnostics
Imports DevExpress.XtraRichEdit.Model
Imports System.Net
Imports DevExpress.XtraCharts.Native

Partial Class production_version_Doc_Student
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim docID As String = Request.QueryString("DocID")
            Dim permissionID As String = Request.QueryString("Permission_ID")

        End If
    End Sub

    Public ReadOnly Property docID() As String
        Get
            If ViewState("docID") IsNot Nothing Then
                Return ViewState("docID").ToString()
            Else
                ' ตั้งค่าค่าเริ่มต้นหรือการจัดการเมื่อไม่มีค่า ViewState("docID")
                Return String.Empty ' หรือค่าอื่นที่คุณต้องการ
            End If
        End Get
    End Property


    'Protected Sub ConfirmButton_Click(sender As Object, e As EventArgs)
    '    Dim btnConfirm As Button = CType(sender, Button)
    '    If btnConfirm.CommandName = "Confirm" Then
    '        Dim dataItemIndex As Integer = Convert.ToInt32(btnConfirm.CommandArgument)

    '        Dim email As String = Session("Email").ToString()


    '        ' ต่อไปดำเนินการเหมือนเดิม...

    '        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

    '        Using connection As New NpgsqlConnection(connectionString)
    '            connection.Open()

    '            Dim masSubSelectQuery As String = "SELECT SubAddAj_ID,DocID, Name, Email, Mobile, Urole, Urole_TC, Status, Status_Add FROM SubAdd_AJ_TB WHERE Email = @Email"

    '            Using masSubSelectCommand As New NpgsqlCommand(masSubSelectQuery, connection)
    '                masSubSelectCommand.Parameters.Add(PgDb.Param("@Email", email))
    '                Using reader As NpgsqlDataReader = masSubSelectCommand.ExecuteReader()
    '                    If reader.Read() Then
    '                        Dim SubAddAjID As String = reader("SubAddAj_ID").ToString()
    '                        Dim docid As String = reader("DocID").ToString()
    '                        Dim name As String = reader("Name").ToString()
    '                        Dim email1 As String = reader("Email").ToString()
    '                        Dim mobile As String = reader("Mobile").ToString()
    '                        Dim urole As String = reader("Urole").ToString()
    '                        Dim uroletc As String = reader("Urole_TC").ToString()


    '                        reader.Close() ' ปิด DataReader ก่อนใช้ ExecuteNonQuery()

    '                        Dim subInsertQuery As String = "INSERT INTO Permission_TB (DocID, Name, Email, Mobile, Urole, Urole_TC) VALUES (@DocID, @Name, @Email, @Mobile, @Urole, @Urole_TC)"

    '                        Using subInsertCommand As New NpgsqlCommand(subInsertQuery, connection)
    '                            subInsertCommand.Parameters.Add(PgDb.Param("@DocID", docid))
    '                            subInsertCommand.Parameters.Add(PgDb.Param("@Name", name))
    '                            subInsertCommand.Parameters.Add(PgDb.Param("@Email", email1))
    '                            subInsertCommand.Parameters.Add(PgDb.Param("@Mobile", mobile))
    '                            subInsertCommand.Parameters.Add(PgDb.Param("@Urole", urole))
    '                            subInsertCommand.Parameters.Add(PgDb.Param("@Urole_TC", uroletc))


    '                            subInsertCommand.ExecuteNonQuery()
    '                        End Using

    '                        Dim updateQuery As String = "UPDATE SubAdd_AJ_TB SET Status_Add = @Status_Add, Status = @Status WHERE SubAddAj_ID = @SubAddAj_ID"

    '                        Using updateCommand As New NpgsqlCommand(updateQuery, connection)
    '                            updateCommand.Parameters.Add(PgDb.Param("@Status_Add", "1"))
    '                            updateCommand.Parameters.Add(PgDb.Param("@Status", "ยืนยันแล้ว"))
    '                            updateCommand.Parameters.Add(PgDb.Param("@SubAddAj_ID", SubAddAjID))

    '                            updateCommand.ExecuteNonQuery()
    '                        End Using


    '                        Dim updateQuery2 As String = "UPDATE MAS_DOC SET Name_teacherPJ = @Name_teacherPJ WHERE DocID = @DocID"

    '                        Using updateCommand2 As New NpgsqlCommand(updateQuery2, connection)
    '                            updateCommand2.Parameters.Add(PgDb.Param("@Name_teacherPJ", name))
    '                            updateCommand2.Parameters.Add(PgDb.Param("@DocID", docid))

    '                            updateCommand2.ExecuteNonQuery()
    '                        End Using


    '                    End If
    '                End Using
    '            End Using



    '        End Using



    '        GridView1.DataBind()

    '        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ยืนยันคำร้องขอ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)
    '    End If

    'End Sub

    Protected Sub ConfirmButton_Click(sender As Object, e As EventArgs)
        Dim btnConfirm As Button = CType(sender, Button)
        Dim dataItemIndex As Integer

        If Integer.TryParse(btnConfirm.CommandArgument, dataItemIndex) Then
            Dim SubAddAjID As String = GridView1.DataKeys(dataItemIndex).Values("SubAddAj_ID").ToString()

            Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

            Using connection As New NpgsqlConnection(connectionString)
                connection.Open()

                Dim masSubSelectQuery As String = "SELECT SubAddAj_ID,DocID, Name, Email, Mobile, Urole, Urole_TC, Status, Status_Add FROM SubAdd_AJ_TB WHERE SubAddAj_ID = @SubAddAj_ID"

                Using masSubSelectCommand As New NpgsqlCommand(masSubSelectQuery, connection)
                    masSubSelectCommand.Parameters.Add(PgDb.Param("@SubAddAj_ID", SubAddAjID))
                    Using reader As NpgsqlDataReader = masSubSelectCommand.ExecuteReader()
                        If reader.Read() Then
                            Dim docid As String = reader("DocID").ToString()
                            Dim name As String = reader("Name").ToString()
                            Dim email1 As String = reader("Email").ToString()
                            Dim mobile As String = reader("Mobile").ToString()
                            Dim urole As String = reader("Urole").ToString()
                            Dim uroletc As String = reader("Urole_TC").ToString()


                            reader.Close() ' ปิด DataReader ก่อนใช้ ExecuteNonQuery()

                            Dim subInsertQuery As String = "INSERT INTO Permission_TB (DocID, Name, Email, Mobile, Urole, Urole_TC) VALUES (@DocID, @Name, @Email, @Mobile, @Urole, @Urole_TC)"

                            Using subInsertCommand As New NpgsqlCommand(subInsertQuery, connection)
                                subInsertCommand.Parameters.Add(PgDb.Param("@DocID", docid))
                                subInsertCommand.Parameters.Add(PgDb.Param("@Name", name))
                                subInsertCommand.Parameters.Add(PgDb.Param("@Email", email1))
                                subInsertCommand.Parameters.Add(PgDb.Param("@Mobile", mobile))
                                subInsertCommand.Parameters.Add(PgDb.Param("@Urole", urole))
                                subInsertCommand.Parameters.Add(PgDb.Param("@Urole_TC", uroletc))


                                subInsertCommand.ExecuteNonQuery()
                            End Using

                            Dim updateQuery As String = "UPDATE SubAdd_AJ_TB SET Status_Add = @Status_Add, Status = @Status, Date_Confirm = NOW() WHERE SubAddAj_ID = @SubAddAj_ID"

                            Using updateCommand As New NpgsqlCommand(updateQuery, connection)
                                updateCommand.Parameters.Add(PgDb.Param("@Status_Add", "1"))
                                updateCommand.Parameters.Add(PgDb.Param("@Status", "ยืนยันแล้ว"))
                                updateCommand.Parameters.Add(PgDb.Param("@SubAddAj_ID", SubAddAjID))

                                updateCommand.ExecuteNonQuery()
                            End Using


                            Dim updateQuery2 As String = "UPDATE MAS_DOC SET Name_teacherPJ = @Name_teacherPJ WHERE DocID = @DocID"

                            Using updateCommand2 As New NpgsqlCommand(updateQuery2, connection)
                                updateCommand2.Parameters.Add(PgDb.Param("@Name_teacherPJ", name))
                                updateCommand2.Parameters.Add(PgDb.Param("@DocID", docid))

                                updateCommand2.ExecuteNonQuery()
                            End Using


                        End If
                    End Using
                End Using



            End Using

            GridView1.DataBind()

            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ยืนยันคำร้องขอ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)

        Else
            ' กรณีไม่สามารถแปลงค่าเป็น integer ได้
            ' คุณอาจจะต้องแจ้งเตือนผู้ใช้หรือดำเนินการอื่นตามความเหมาะสม

        End If
    End Sub

    Protected Sub RejectButton_Click(sender As Object, e As EventArgs)
        Dim btnReject As Button = CType(sender, Button)
        Dim dataItemIndex As Integer

        If Integer.TryParse(btnReject.CommandArgument, dataItemIndex) Then
            Dim SubAddAjID As String = GridView1.DataKeys(dataItemIndex).Values("SubAddAj_ID").ToString()

            ' Debug: แสดงค่า statusID ใน Output Window หรือในหน้าเว็บ (ถ้าใช้ Response.Write)
            Debug.WriteLine("SubAddAj_ID: " & SubAddAjID)

            ' ตามด้วยโค้ดอื่น ๆ ที่ต้องการในการปรับสถานะ
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

            Using connection As New NpgsqlConnection(connectionString)
                connection.Open()

                Dim updateStatusQuery As String = "UPDATE SubAdd_AJ_TB SET Status = 'ถูกปฏิเสธ',Status_Add = '1', Date_Confirm = NOW() WHERE SubAddAj_ID = @SubAddAj_ID"

                Using updateStatusCommand As New NpgsqlCommand(updateStatusQuery, connection)
                    updateStatusCommand.Parameters.Add(PgDb.Param("@SubAddAj_ID", SubAddAjID))
                    updateStatusCommand.ExecuteNonQuery()
                End Using
            End Using

            ' หลังจากทำการอัพเดทแล้ว คุณอาจต้องทำการอัพเดท GridView ใหม่เพื่อแสดงการเปลี่ยนแปลง
            GridView1.DataBind()

            ' แสดงข้อความแจ้งเตือนหรือแจ้งสถานะหลังจากปรับสถานะเรียบร้อย
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ปฏิเสธคำร้องขอ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_All.aspx'; });", True)
        Else
            ' กรณีไม่สามารถแปลงค่าเป็น integer ได้
            ' คุณอาจจะต้องแจ้งเตือนผู้ใช้หรือดำเนินการอื่นตามความเหมาะสม

        End If
    End Sub

    'Protected Sub RejectButton_Click(sender As Object, e As EventArgs)
    '    Dim btnConfirm As Button = CType(sender, Button)
    '    If btnConfirm.CommandName = "Reject" Then

    '        Dim dataItemIndex As Integer = Convert.ToInt32(btnConfirm.CommandArgument)

    '        Dim email As String = Session("Email").ToString()




    '        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

    '        Using connection As New NpgsqlConnection(connectionString)
    '            connection.Open()

    '            Dim masSubSelectQuery As String = "SELECT SubAddAj_ID,DocID, Name, Email, Mobile, Urole, Urole_TC, Status, Status_Add FROM SubAdd_AJ_TB WHERE Email = @Email"

    '            Using masSubSelectCommand As New NpgsqlCommand(masSubSelectQuery, connection)
    '                masSubSelectCommand.Parameters.Add(PgDb.Param("@Email", email))
    '                Using reader As NpgsqlDataReader = masSubSelectCommand.ExecuteReader()
    '                    If reader.Read() Then
    '                        Dim SubAddAjID As String = reader("SubAddAj_ID").ToString()
    '                        Dim docid As String = reader("DocID").ToString()
    '                        Dim name As String = reader("Name").ToString()
    '                        Dim email1 As String = reader("Email").ToString()
    '                        Dim mobile As String = reader("Mobile").ToString()
    '                        Dim urole As String = reader("Urole").ToString()
    '                        Dim uroletc As String = reader("Urole_TC").ToString()


    '                        reader.Close() ' ปิด DataReader ก่อนใช้ ExecuteNonQuery()



    '                        Dim updateQuery As String = "UPDATE SubAdd_AJ_TB SET Status_Add = @Status_Add, Status = @Status WHERE SubAddAj_ID = @SubAddAj_ID"

    '                        Using updateCommand As New NpgsqlCommand(updateQuery, connection)
    '                            updateCommand.Parameters.Add(PgDb.Param("@Status_Add", "1"))
    '                            updateCommand.Parameters.Add(PgDb.Param("@Status", "ถูกปฏิเสธ"))
    '                            updateCommand.Parameters.Add(PgDb.Param("@SubAddAj_ID", SubAddAjID))

    '                            updateCommand.ExecuteNonQuery()
    '                        End Using




    '                    End If
    '                End Using
    '            End Using



    '        End Using



    '        GridView1.DataBind()

    '        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ยืนยันการเปลี่ยนคู่โปรเจค!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)
    '    End If

    'End Sub



End Class
