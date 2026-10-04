Imports System.Data

' โปรไฟล์ผู้ใช้ (นิสิตและอาจารย์)
Partial Class production_version_Profile
    Inherits System.Web.UI.Page

    Protected Ini, UserName, RoleName, Login, Email, StudentID, Mobile, StatsHtml As String

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Select Case Convert.ToString(Session("Urole"))
            Case "3" : MasterPageFile = "~/app/MasterPageAdmin.master"
            Case "2" : MasterPageFile = "~/app/MasterPageTeacher.master"
            Case Else : MasterPageFile = "~/app/MasterPageHome.master"
        End Select
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadUser(Not IsPostBack)
    End Sub

    Private Sub LoadUser(fillForm As Boolean)
        Dim rows = Shell.Query("SELECT Username, Email, Name, Mobile, Student_ID, Urole FROM Signin_Signup WHERE UserID = @ID", Shell.P("@ID", Session("UserID")))
        If rows.Rows.Count <> 1 Then Response.Redirect("Login.aspx", True)
        Dim r = rows.Rows(0)
        UserName = Convert.ToString(r("Name"))
        Ini = Shell.Initials(UserName)
        Login = Convert.ToString(r("Username"))
        Email = Convert.ToString(r("Email"))
        StudentID = Convert.ToString(r("Student_ID")).Trim()
        Mobile = Convert.ToString(r("Mobile")).Trim()
        Dim role As String = Convert.ToString(r("Urole"))
        RoleName = If(role = "3", "ผู้ดูแลระบบ", If(role = "2", "อาจารย์", "นิสิต"))
        If fillForm Then
            txtName.Text = UserName
            txtMobile.Text = Mobile
        End If

        Dim projects = Dash.Count("SELECT COUNT(DISTINCT DocID) FROM Permission_TB WHERE Email = @Email", Shell.P("@Email", Email))
        Dim messages = Dash.Count("SELECT COUNT(*) FROM Talk_TB WHERE Sender_Email = @Email", Shell.P("@Email", Email))
        StatsHtml = Stat("folder-open", If(role = "2", "โครงงานที่ดูแล", "โครงงาน"), projects) & Stat("message-square", "ข้อความที่ส่ง", messages)
    End Sub

    Private Function Stat(icon As String, label As String, value As Integer) As String
        Return "<div class=""panel tw-p-4""><span class=""kpi-icon tw-bg-accent/15 tw-text-accent""><i data-lucide=""" & icon & """></i></span>" &
               "<div class=""tw-mt-2 tw-text-2xl tw-font-bold tw-text-ink"">" & value & "</div><div class=""tw-text-xs tw-text-muted"">" & HttpUtility.HtmlEncode(label) & "</div></div>"
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not Page.IsValid Then Return
        Dim name As String = txtName.Text.Trim()
        Dim mobile As String = txtMobile.Text.Trim()
        If mobile <> "" AndAlso Dash.Count("SELECT COUNT(*) FROM Signin_Signup WHERE Mobile = @Mobile AND UserID <> @ID",
                                            Shell.P("@Mobile", mobile), Shell.P("@ID", Session("UserID"))) > 0 Then
            Alert("ข้อมูลซ้ำ", "เบอร์โทรศัพท์นี้มีผู้ใช้อื่นใช้แล้ว", "warning")
            Return
        End If
        Dim email As String = Convert.ToString(Session("Email"))
        Shell.Exec("UPDATE Signin_Signup SET Name = @Name, Mobile = @Mobile WHERE UserID = @ID",
                   Shell.P("@Name", name), Shell.P("@Mobile", mobile), Shell.P("@ID", Session("UserID")))
        ' ชื่อและเบอร์ถูกคัดลอกไว้ในรายชื่อสมาชิกโครงงานด้วย
        Shell.Exec("UPDATE Permission_TB SET Name = @Name, Mobile = @Mobile WHERE Email = @Email",
                   Shell.P("@Name", name), Shell.P("@Mobile", mobile), Shell.P("@Email", email))
        Session("Name") = name
        Session("Mobile") = mobile
        LoadUser(False)
        Alert("สำเร็จ", "บันทึกข้อมูลแล้ว", "success")
    End Sub

    Private Sub Alert(title As String, text As String, icon As String)
        ClientScript.RegisterStartupScript(Me.GetType(), "alert",
            "Swal.fire({ title: '" & SafeOut.Js(title) & "', text: '" & SafeOut.Js(text) & "', icon: '" & icon & "', confirmButtonText: 'ตกลง' });", True)
    End Sub

End Class
