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
        Dim email As String = Request.QueryString("email")
        Dim name As String = Request.QueryString("name")

    End Sub






    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim email As String = Request.QueryString("email")
        Dim name As String = Request.QueryString("name")
        Dim comment As String = commentText.Text

        If String.IsNullOrEmpty(comment) Then
            ' หาก comment ว่างเปล่าให้แสดงข้อความผิดพลาด
            RequiredFieldValidator14.ErrorMessage = "กรุณาคอมเมนต์ก่อนกดบันทึก"
            RequiredFieldValidator14.IsValid = False
        Else
            ' เช็คว่า comment ไม่ซ้ำในฐานข้อมูลก่อน insert
            Dim isCommentAlreadyExists As Boolean = IsCommentExists(email, Session("Email"), comment)

            If Not isCommentAlreadyExists Then
                Dim saveInsert = db.GetValue("INSERT INTO Talk_TB (Email_AJ, Email_NS, Name, Comment_Text, Date_Save, Post_Name_RS,Urole) VALUES (@Email_AJ, @Email_NS, @Name, @Comment_Text, GETDATE(), @Post_Name_RS,'2')",
                      db.NewParam("Email_AJ", email),
                      db.NewParam("Email_NS", Session("Email")),
                      db.NewParam("Name", name),
                      db.NewParam("Comment_Text", comment),
                      db.NewParam("Post_Name_RS", Session("Name")))
            Else
                ' แสดงข้อความว่า comment ซ้ำในฐานข้อมูล (หรือทำอะไรกับสถานการณ์นี้ตามที่คุณต้องการ)
            End If
        End If

        db.Close()
        If IsPostBack Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'เพิ่มข้อมูลสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Talk.aspx'; });", True)
        End If
    End Sub

    Private Function IsCommentExists(ByVal emailAJ As String, ByVal emailNS As String, ByVal commentText As String) As Boolean
        ' เชื่อมต่อกับฐานข้อมูล
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Using db As New SqlConnection(connectionString)
            db.Open()

            ' ค้นหา comment ที่มีค่าเดียวกันในฐานข้อมูล
            Dim query As String = "SELECT COUNT(*) FROM Talk_TB WHERE Email_AJ = @Email_AJ AND Email_NS = @Email_NS AND Comment_Text = @Comment_Text"
            Using cmd As New SqlCommand(query, db)
                cmd.Parameters.AddWithValue("@Email_AJ", emailAJ)
                cmd.Parameters.AddWithValue("@Email_NS", emailNS)
                cmd.Parameters.AddWithValue("@Comment_Text", commentText)

                ' นับจำนวน comment ที่ตรงเงื่อนไข
                Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' ถ้ามี comment ที่ซ้ำกันในฐานข้อมูลให้คืนค่า True
                ' ถ้าไม่มี comment ที่ซ้ำกันให้คืนค่า False
                Return count > 0
            End Using
        End Using
    End Function










    ' หน้าแชทเก่า ย้ายไปใช้ Chat.aspx (หน้าเดียวสำหรับนิสิตและอาจารย์)
    Protected Sub RedirectToNewChat(sender As Object, e As EventArgs) Handles Me.PreInit
        Response.Redirect("Chat.aspx?with=" & HttpUtility.UrlEncode(If(Request.QueryString("email"), "")), True)
    End Sub
End Class
