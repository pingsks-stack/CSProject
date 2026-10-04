Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Zulu.Data
Imports DevExpress.Web
Imports DevExpress.Web.Bootstrap
Imports System.Diagnostics

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




    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
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
                Dim saveinsert = db.GetValue("insert into Comment_TB (Permission_ID,DocID,Email,Name,Comment_Text,Date_Save,Post_Name_RS) VALUES (@Permission_ID,@DocID,@Email,@Name,@Comment_Text,getdate(),'นักศึกษา')",
                      db.NewParam("Permission_ID", permissionID),
                      db.NewParam("DocID", docID),
                      db.NewParam("Email", Session("Email")),
                      db.NewParam("Name", Session("Name")),
                      db.NewParam("Comment_Text", commentText.Text))
            End If
        End If

        db.Close()
        If (IsPostBack) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'เพิ่มข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
        End If
    End Sub

    Private Function CheckCommentExists(ByVal docID As String, ByVal email As String, ByVal commentText As String) As Boolean
        ' เชื่อมต่อกับฐานข้อมูล
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Using db As New SqlConnection(connectionString)
            db.Open()

            ' ค้นหาคอมเมนต์ที่มีค่าเดียวกันในฐานข้อมูล
            Dim query As String = "SELECT COUNT(*) FROM Comment_TB WHERE DocID = @DocID AND Email = @Email AND Comment_Text = @CommentText"
            Using cmd As New SqlCommand(query, db)
                cmd.Parameters.AddWithValue("@DocID", docID)
                cmd.Parameters.AddWithValue("@Email", email)
                cmd.Parameters.AddWithValue("@CommentText", commentText)

                ' นับจำนวนคอมเมนต์ที่ตรงเงื่อนไข
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' ถ้ามีคอมเมนต์ที่ซ้ำกันในฐานข้อมูลให้คืนค่า True
                ' ถ้าไม่มีคอมเมนต์ที่ซ้ำกันให้คืนค่า False
                Return count > 0
            End Using
        End Using
    End Function





End Class
