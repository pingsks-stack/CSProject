Imports System.Data
Imports Npgsql
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Bootstrap
Imports System.Diagnostics
Imports System.Net

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





    Protected Sub ConfirmButton_Click(sender As Object, e As EventArgs)
        Dim btnConfirm As Button = CType(sender, Button)
        If btnConfirm.CommandName = "Confirm" Then
            Dim dataItemIndex As Integer = Convert.ToInt32(btnConfirm.CommandArgument)

            Dim statusID As String = GridView1.DataKeys(dataItemIndex).Values("Status_ID").ToString()
            Dim docID As String = GridView1.DataKeys(dataItemIndex).Values("DocID").ToString()
            Dim permissionID As String = GridView1.DataKeys(dataItemIndex).Values("Permission_ID").ToString()

            ' ต่อไปดำเนินการเหมือนเดิม...

            Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

            Using connection As New NpgsqlConnection(connectionString)
                connection.Open()

                'Dim masSubSelectQuery As String = "SELECT Name, Email, Mobile, Student_ID FROM Permission_TB WHERE Permission_ID = @Permission_ID"

                'Using masSubSelectCommand As New NpgsqlCommand(masSubSelectQuery, connection)
                '    masSubSelectCommand.Parameters.Add(PgDb.Param("@Permission_ID", permissionID))
                '    Using reader As NpgsqlDataReader = masSubSelectCommand.ExecuteReader()
                '        If reader.Read() Then
                '            Dim name1 As String = reader("Name").ToString()
                '            Dim email1 As String = reader("Email").ToString()
                '            Dim mobile1 As String = reader("Mobile").ToString()
                '            Dim studentID1 As String = reader("Student_ID").ToString()

                '            reader.Close() ' ปิด DataReader ก่อนใช้ ExecuteNonQuery()

                '            Dim subInsertQuery As String = "INSERT INTO SubInfor_TB (DocID, Permission_ID, Name, Email, Mobile, Student_ID, Urole ,Action) VALUES (@DocID, @Permission_ID, @Name, @Email, @Mobile, @Student_ID, '1', 'คู่โปรเจคคนเก่า')"

                '            Using subInsertCommand As New NpgsqlCommand(subInsertQuery, connection)
                '                subInsertCommand.Parameters.Add(PgDb.Param("@DocID", docID))
                '                subInsertCommand.Parameters.Add(PgDb.Param("@Permission_ID", permissionID))
                '                subInsertCommand.Parameters.Add(PgDb.Param("@Name", name1))
                '                subInsertCommand.Parameters.Add(PgDb.Param("@Email", email1))
                '                subInsertCommand.Parameters.Add(PgDb.Param("@Mobile", mobile1))
                '                subInsertCommand.Parameters.Add(PgDb.Param("@Student_ID", studentID1))

                '                subInsertCommand.ExecuteNonQuery()
                '            End Using
                '        End If
                '    End Using
                'End Using


                Dim masSelectQuery As String = "SELECT Name, Email, Mobile, Student_ID FROM Status_Change_TB WHERE Status_ID = @Status_ID"

                Using masSelectCommand As New NpgsqlCommand(masSelectQuery, connection)
                    masSelectCommand.Parameters.Add(PgDb.Param("@Status_ID", statusID))
                    Using reader As NpgsqlDataReader = masSelectCommand.ExecuteReader()
                        If reader.Read() Then
                            Dim name As String = reader("Name").ToString()
                            Dim email As String = reader("Email").ToString()
                            Dim mobile As String = reader("Mobile").ToString()
                            Dim studentID As String = reader("Student_ID").ToString()

                            reader.Close() ' ปิด DataReader ก่อนใช้ ExecuteNonQuery()

                            ' ... คำสั่งเพิ่มและอัพเดทข้อมูลตามที่คุณต้องการ ...
                            'Dim subInsertQuery As String = "INSERT INTO SubInfor_TB (DocID, Permission_ID, Name, Email, Mobile, Student_ID, Urole ,Action) VALUES (@DocID, @Permission_ID, @Name, @Email, @Mobile, @Student_ID, '1', 'คู่โปรเจคใหม่')"

                            'Using subInsertCommand As New NpgsqlCommand(subInsertQuery, connection)
                            '    subInsertCommand.Parameters.Add(PgDb.Param("@DocID", docID))
                            '    subInsertCommand.Parameters.Add(PgDb.Param("@Permission_ID", permissionID))
                            '    subInsertCommand.Parameters.Add(PgDb.Param("@Name", name))
                            '    subInsertCommand.Parameters.Add(PgDb.Param("@Email", email))
                            '    subInsertCommand.Parameters.Add(PgDb.Param("@Mobile", mobile))
                            '    subInsertCommand.Parameters.Add(PgDb.Param("@Student_ID", studentID))

                            '    subInsertCommand.ExecuteNonQuery()
                            'End Using

                            Dim updateQuery As String = "UPDATE Permission_TB SET Email = @Email, Name = @Name, Mobile = @Mobile, Student_ID = @Student_ID WHERE Permission_ID = @Permission_ID AND Urole = '1'"

                            Using updateCommand As New NpgsqlCommand(updateQuery, connection)
                                updateCommand.Parameters.Add(PgDb.Param("@Email", email))
                                updateCommand.Parameters.Add(PgDb.Param("@Name", name))
                                updateCommand.Parameters.Add(PgDb.Param("@Mobile", mobile))
                                updateCommand.Parameters.Add(PgDb.Param("@Student_ID", studentID))
                                updateCommand.Parameters.Add(PgDb.Param("@Permission_ID", permissionID))

                                updateCommand.ExecuteNonQuery()
                            End Using

                            Dim masUpdateQuery As String = "UPDATE MAS_DOC SET Email2 = @Email2, Student_Name2 = @Student_Name2, Student_ID2 = @Student_ID2, Mobile2 = @Mobile2 WHERE DocID = @DocID"

                            Using masUpdateCommand As New NpgsqlCommand(masUpdateQuery, connection)
                                masUpdateCommand.Parameters.Add(PgDb.Param("@Email2", email))
                                masUpdateCommand.Parameters.Add(PgDb.Param("@Student_Name2", name))
                                masUpdateCommand.Parameters.Add(PgDb.Param("@Student_ID2", studentID))
                                masUpdateCommand.Parameters.Add(PgDb.Param("@Mobile2", mobile))
                                masUpdateCommand.Parameters.Add(PgDb.Param("@DocID", docID))

                                masUpdateCommand.ExecuteNonQuery()
                            End Using

                            Dim masStatusUpdateQuery As String = "UPDATE Status_Change_TB SET Status = 'ยืนยันแล้ว',Status_Change = '1',Status_Change1 = '1', Date_Confirm = NOW() WHERE Status_ID = @Status_ID"

                            Using masStatusUpdateCommand As New NpgsqlCommand(masStatusUpdateQuery, connection)
                                masStatusUpdateCommand.Parameters.Add(PgDb.Param("@Status_ID", statusID))
                                masStatusUpdateCommand.ExecuteNonQuery()
                            End Using
                        End If
                    End Using
                End Using
            End Using

            ' ... คำสั่งอื่นๆ ที่คุณต้องการ ...

            ' อัพเดทข้อมูลใน GridView
            GridView1.DataBind()

            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ยืนยันการเปลี่ยนคู่โปรเจค!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)
        End If

    End Sub

    Protected Sub RejectButton_Click(sender As Object, e As EventArgs)
        Dim btnReject As Button = CType(sender, Button)
        Dim dataItemIndex As Integer

        If Integer.TryParse(btnReject.CommandArgument, dataItemIndex) Then
            Dim statusID As String = GridView1.DataKeys(dataItemIndex).Values("Status_ID").ToString()

            ' Debug: แสดงค่า statusID ใน Output Window หรือในหน้าเว็บ (ถ้าใช้ Response.Write)
            Debug.WriteLine("statusID: " & statusID)

            ' ตามด้วยโค้ดอื่น ๆ ที่ต้องการในการปรับสถานะ
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

            Using connection As New NpgsqlConnection(connectionString)
                connection.Open()

                Dim updateStatusQuery As String = "UPDATE Status_Change_TB SET Status = 'ถูกปฏิเสธ',Status_Change1 = '1', Date_Confirm = NOW() WHERE Status_ID = @Status_ID"

                Using updateStatusCommand As New NpgsqlCommand(updateStatusQuery, connection)
                    updateStatusCommand.Parameters.Add(PgDb.Param("@Status_ID", statusID))
                    updateStatusCommand.ExecuteNonQuery()
                End Using

            End Using

            ' หลังจากทำการอัพเดทแล้ว คุณอาจต้องทำการอัพเดท GridView ใหม่เพื่อแสดงการเปลี่ยนแปลง
            GridView1.DataBind()

            ' แสดงข้อความแจ้งเตือนหรือแจ้งสถานะหลังจากปรับสถานะเรียบร้อย
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ยกเลิกการเปลี่ยนแปลงคู่โปรเจคสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)
        Else
            ' กรณีไม่สามารถแปลงค่าเป็น integer ได้
            ' คุณอาจจะต้องแจ้งเตือนผู้ใช้หรือดำเนินการอื่นตามความเหมาะสม

        End If
    End Sub

    Protected Sub ConfirmButton1_Click(sender As Object, e As EventArgs)
        ' ฟังก์ชันที่เรียกเมื่อปุ่ม "ยืนยัน" ถูกคลิก
        Dim button As Button = TryCast(sender, Button)
        If button IsNot Nothing Then
            Dim rowIndex As Integer = Integer.Parse(button.CommandArgument.ToString())
            Dim permissionID As String = GridView3.DataKeys(rowIndex).Values("Permission_ID").ToString()
            Dim docID As String = GridView3.DataKeys(rowIndex).Values("DocID").ToString()
            Dim statusID As String = GridView3.DataKeys(rowIndex).Values("Status_ID").ToString()

            Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

            Using connection As New NpgsqlConnection(connectionString)
                connection.Open()
                Using command As New NpgsqlCommand("DELETE FROM Permission_TB WHERE Permission_ID = @Permission_ID", connection)
                    command.Parameters.Add(PgDb.Param("@Permission_ID", permissionID))
                    command.ExecuteNonQuery()
                End Using
            End Using

            Using connection As New NpgsqlConnection(connectionString)
                connection.Open()

                Dim masStatusUpdateQuery As String = "UPDATE Status_Change_TB SET Status = 'ยืนยันแล้ว',Status_Change = '1',Status_Change1 = '1', Date_Confirm = NOW() WHERE Status_ID = @Status_ID"

                Using masStatusUpdateCommand As New NpgsqlCommand(masStatusUpdateQuery, connection)
                    masStatusUpdateCommand.Parameters.Add(PgDb.Param("@Status_ID", statusID))
                    masStatusUpdateCommand.ExecuteNonQuery()
                End Using

                Dim masUpdateQuery As String = "UPDATE MAS_DOC SET Email2 = NULL, Student_Name2 = NULL, Student_ID2 = NULL, Mobile2 = NULL WHERE DocID = @DocID"

                Using masUpdateCommand As New NpgsqlCommand(masUpdateQuery, connection)
                    masUpdateCommand.Parameters.Add(PgDb.Param("@DocID", docID))

                    masUpdateCommand.ExecuteNonQuery()
                End Using


            End Using

            GridView3.DataBind()

            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ยืนยันการลบคู่โปรเจคสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)

        End If
    End Sub

    Protected Sub RejectButton1_Click(sender As Object, e As EventArgs)
        ' ฟังก์ชันที่เรียกเมื่อปุ่ม "ปฏิเสธ" ถูกคลิก
        Dim button As Button = TryCast(sender, Button)
        If button IsNot Nothing Then
            Dim rowIndex As Integer = Integer.Parse(button.CommandArgument.ToString())
            Dim permissionID As String = GridView3.DataKeys(rowIndex).Values("Permission_ID").ToString()
            Dim docID As String = GridView3.DataKeys(rowIndex).Values("DocID").ToString()
            Dim statusID As String = GridView3.DataKeys(rowIndex).Values("Status_ID").ToString()

            Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString

            Using connection As New NpgsqlConnection(connectionString)
                connection.Open()

                Dim updateStatusQuery As String = "UPDATE Status_Change_TB SET Status = 'ถูกปฏิเสธ',Status_Change1 = '1', Date_Confirm = NOW() WHERE Status_ID = @Status_ID"

                Using updateStatusCommand As New NpgsqlCommand(updateStatusQuery, connection)
                    updateStatusCommand.Parameters.Add(PgDb.Param("@Status_ID", statusID))
                    updateStatusCommand.ExecuteNonQuery()
                End Using
            End Using

            GridView3.DataBind()

            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ยกเลิกการลบคู่โปรเจคสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Doc_Student.aspx'; });", True)

        End If
    End Sub


End Class
