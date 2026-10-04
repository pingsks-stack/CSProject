Imports System.Data
Imports System.Data.SqlClient
Imports System.Text

' จัดการผู้ใช้สำหรับแอดมิน: สร้างบัญชี (รวมอาจารย์), เปลี่ยนบทบาท, รีเซ็ตรหัสผ่าน, ลบบัญชีที่ไม่มีโครงงาน
Partial Class production_version_Admin_Users
    Inherits System.Web.UI.Page

    Protected StatsHtml, ChipsHtml, RowsHtml, Q As String
    Protected KeepFormOpen As Boolean

    Private Shared ReadOnly RoleNames As New Dictionary(Of String, String) From {{"1", "นิสิต"}, {"2", "อาจารย์"}, {"3", "ผู้ดูแลระบบ"}}

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then txtNewPass.Text = RandomPassword()
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        Dim role As String = If(Request.QueryString("role"), "")
        Q = If(Request.QueryString("q"), "").Trim()

        Dim counts As New Dictionary(Of String, Integer) From {{"1", 0}, {"2", 0}, {"3", 0}}
        For Each r As DataRow In Shell.Query("SELECT Urole, COUNT(*) FROM Signin_Signup GROUP BY Urole").Rows
            Dim k As String = Convert.ToString(r(0))
            If counts.ContainsKey(k) Then counts(k) = CInt(r(1))
        Next
        StatsHtml = Dash.Kpi("graduation-cap", "accent", "นิสิต", counts("1"), "บัญชีนิสิตทั้งหมด") &
                    Dash.Kpi("presentation", "gold", "อาจารย์", counts("2"), "อาจารย์ที่ปรึกษาและกรรมการ") &
                    Dash.Kpi("shield-check", "info", "ผู้ดูแลระบบ", counts("3"), "บัญชีแอดมิน")
        ChipsHtml = Chip("", "ทั้งหมด", counts("1") + counts("2") + counts("3"), role) &
                    Chip("1", "นิสิต", counts("1"), role) & Chip("2", "อาจารย์", counts("2"), role) & Chip("3", "แอดมิน", counts("3"), role)

        Dim conds As New List(Of String)
        Dim ps As New List(Of SqlParameter)
        If role <> "" Then conds.Add("u.Urole = @Role") : ps.Add(Shell.P("@Role", role))
        If Q <> "" Then
            conds.Add("(u.Name LIKE @Q OR u.Username LIKE @Q OR u.Email LIKE @Q OR u.Student_ID LIKE @Q)")
            ps.Add(Shell.P("@Q", "%" & Q.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]") & "%"))
        End If
        Dim dt = Shell.Query(
            "SELECT u.UserID, u.Username, u.Name, u.Email, u.Mobile, u.Student_ID, u.Urole, " &
            "(SELECT COUNT(DISTINCT p.DocID) FROM Permission_TB p WHERE p.Email = u.Email) AS Projects " &
            "FROM Signin_Signup u" & If(conds.Count > 0, " WHERE " & String.Join(" AND ", conds), "") &
            " ORDER BY u.Urole DESC, u.Name", ps.ToArray())

        Dim myId As String = Convert.ToString(Session("UserID"))
        Dim sb As New StringBuilder()
        For Each r As DataRow In dt.Rows
            Dim id As String = Convert.ToString(r("UserID"))
            Dim name As String = Convert.ToString(r("Name"))
            Dim urole As String = Convert.ToString(r("Urole"))
            Dim projects As Integer = CInt(r("Projects"))
            Dim tone As String = If(urole = "3", "info", If(urole = "2", "gold", "accent"))
            sb.Append("<tr>")
            sb.Append("<td><div class=""tw-flex tw-items-center tw-gap-3""><span class=""tw-flex tw-h-9 tw-w-9 tw-shrink-0 tw-items-center tw-justify-center tw-rounded-full tw-bg-" & tone & "/15 tw-text-xs tw-font-bold tw-text-" & tone & """>" & Shell.Initials(name) & "</span>")
            sb.Append("<div><div class=""tw-font-semibold"">" & H(name) & "</div><div class=""tw-text-xs tw-text-muted"">" & H(r("Username")) & "</div></div></div></td>")
            sb.Append("<td class=""tw-text-muted"">" & H(r("Email")) & "</td>")
            sb.Append("<td><span class=""badge-soft tw-bg-" & tone & "/15 tw-text-" & tone & """>" & H(If(RoleNames.ContainsKey(urole), RoleNames(urole), urole)) & "</span></td>")
            sb.Append("<td class=""tw-text-muted"">" & H(Convert.ToString(r("Student_ID")).Trim()) & If(Convert.ToString(r("Mobile")).Trim() <> "", "<div class=""tw-text-xs"">" & H(Convert.ToString(r("Mobile")).Trim()) & "</div>", "") & "</td>")
            sb.Append("<td>" & projects & "</td>")
            sb.Append("<td class=""tw-whitespace-nowrap tw-text-right"">")
            If id <> myId Then
                sb.Append("<select class=""form-control tw-inline-block tw-h-8 tw-w-auto tw-text-xs"" onchange=""if(this.value){userAction('role','" & id & "','" & SafeOut.JsAttr(name) & "',this.value);this.value='';}"">")
                sb.Append("<option value="""">เปลี่ยนบทบาท…</option>")
                For Each kv In RoleNames
                    If kv.Key <> urole Then sb.Append("<option value=""" & kv.Key & """>" & kv.Value & "</option>")
                Next
                sb.Append("</select> ")
            End If
            sb.Append("<button type=""button"" class=""btn btn-secondary btn-sm"" onclick=""userAction('reset','" & id & "','" & SafeOut.JsAttr(name) & "','" & RandomPassword() & "')"">รีเซ็ตรหัส</button> ")
            If id <> myId AndAlso projects = 0 Then
                sb.Append("<button type=""button"" class=""btn btn-danger btn-sm"" onclick=""userAction('delete','" & id & "','" & SafeOut.JsAttr(name) & "','')"">ลบ</button>")
            End If
            sb.Append("</td></tr>")
        Next
        If dt.Rows.Count = 0 Then sb.Append("<tr><td colspan=""6"" class=""tw-py-10 tw-text-center tw-text-muted"">ไม่พบผู้ใช้</td></tr>")
        RowsHtml = sb.ToString()
    End Sub

    Protected Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Page.Validate("new")
        KeepFormOpen = True
        If Not Page.IsValid Then Return
        Dim role As String = ddlNewRole.SelectedValue
        If Not RoleNames.ContainsKey(role) Then Return
        Dim user As String = txtNewUser.Text.Trim()
        Dim email As String = txtNewEmail.Text.Trim()
        Dim sid As String = If(role = "1", txtNewSid.Text.Trim(), "")
        Dim mobile As String = txtNewMobile.Text.Trim()

        If role = "1" AndAlso sid = "" Then Alert("ข้อมูลไม่ครบ", "บัญชีนิสิตต้องมีรหัสนิสิต", "warning") : Return
        If txtNewPass.Text.Length < 6 Then Alert("รหัสผ่านสั้นเกินไป", "รหัสผ่านชั่วคราวต้องยาวอย่างน้อย 6 ตัวอักษร", "warning") : Return
        Dim dup As String = Duplicate(user, email, sid, mobile)
        If dup <> "" Then Alert("ข้อมูลซ้ำ", dup, "warning") : Return

        Shell.Exec("INSERT INTO Signin_Signup (Username, Password, Email, Name, Mobile, Student_ID, Urole) VALUES (@User, @Pass, @Email, @Name, @Mobile, @Sid, @Role)",
                   Shell.P("@User", user), Shell.P("@Pass", PasswordHasher.Hash(txtNewPass.Text)), Shell.P("@Email", email),
                   Shell.P("@Name", txtNewName.Text.Trim()), Shell.P("@Mobile", mobile), Shell.P("@Sid", sid), Shell.P("@Role", role))
        Alert("สร้างบัญชีแล้ว", "ชื่อผู้ใช้: " & user & "  รหัสผ่านชั่วคราว: " & txtNewPass.Text, "success")
        KeepFormOpen = False
        txtNewName.Text = "" : txtNewUser.Text = "" : txtNewEmail.Text = "" : txtNewMobile.Text = "" : txtNewSid.Text = ""
        txtNewPass.Text = RandomPassword()
    End Sub

    Private Function Duplicate(user As String, email As String, sid As String, mobile As String) As String
        If Dash.Count("SELECT COUNT(*) FROM Signin_Signup WHERE Username = @V", Shell.P("@V", user)) > 0 Then Return "ชื่อผู้ใช้นี้มีอยู่แล้ว"
        If Dash.Count("SELECT COUNT(*) FROM Signin_Signup WHERE Email = @V", Shell.P("@V", email)) > 0 Then Return "อีเมลนี้มีอยู่แล้ว"
        If sid <> "" AndAlso Dash.Count("SELECT COUNT(*) FROM Signin_Signup WHERE Student_ID = @V", Shell.P("@V", sid)) > 0 Then Return "รหัสนิสิตนี้มีอยู่แล้ว"
        If mobile <> "" AndAlso Dash.Count("SELECT COUNT(*) FROM Signin_Signup WHERE Mobile = @V", Shell.P("@V", mobile)) > 0 Then Return "เบอร์โทรศัพท์นี้มีอยู่แล้ว"
        Return ""
    End Function

    Protected Sub btnDo_Click(sender As Object, e As EventArgs) Handles btnDo.Click
        Dim id As Integer
        If Not Integer.TryParse(hfId.Value, id) Then Return
        Dim isMe As Boolean = id.ToString() = Convert.ToString(Session("UserID"))
        Dim row = Shell.Query("SELECT Name, Email FROM Signin_Signup WHERE UserID = @ID", Shell.P("@ID", id))
        If row.Rows.Count = 0 Then Return
        Dim name As String = Convert.ToString(row.Rows(0)("Name"))

        Select Case hfAction.Value
            Case "role"
                If isMe OrElse Not RoleNames.ContainsKey(hfValue.Value) Then Return
                Shell.Exec("UPDATE Signin_Signup SET Urole = @Role WHERE UserID = @ID", Shell.P("@Role", hfValue.Value), Shell.P("@ID", id))
                Alert("สำเร็จ", "เปลี่ยน " & name & " เป็น" & RoleNames(hfValue.Value) & "แล้ว (มีผลเมื่อเข้าสู่ระบบครั้งถัดไป)", "success")
            Case "reset"
                If hfValue.Value.Length < 6 OrElse hfValue.Value.Length > 50 Then Return
                Shell.Exec("UPDATE Signin_Signup SET Password = @Pass WHERE UserID = @ID", Shell.P("@Pass", PasswordHasher.Hash(hfValue.Value)), Shell.P("@ID", id))
                Alert("รีเซ็ตแล้ว", "รหัสผ่านชั่วคราวของ " & name & ": " & hfValue.Value, "success")
            Case "delete"
                If isMe Then Return
                If Dash.Count("SELECT COUNT(*) FROM Permission_TB WHERE Email = @Email", Shell.P("@Email", row.Rows(0)("Email"))) > 0 Then
                    Alert("ลบไม่ได้", "ผู้ใช้นี้อยู่ในโครงงาน", "warning")
                    Return
                End If
                Shell.Exec("DELETE FROM Signin_Signup WHERE UserID = @ID", Shell.P("@ID", id))
                Alert("สำเร็จ", "ลบบัญชี " & name & " แล้ว", "success")
        End Select
    End Sub

    ' รหัสผ่านชั่วคราวอ่านง่าย (ไม่มีตัวที่สับสน เช่น 0/O, 1/l)
    Private Shared Function RandomPassword() As String
        Const chars As String = "ABCDEFGHJKMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz23456789"
        Dim bytes(9) As Byte
        Using rng = System.Security.Cryptography.RandomNumberGenerator.Create()
            rng.GetBytes(bytes)
        End Using
        Dim sb As New StringBuilder()
        For Each b In bytes
            sb.Append(chars(b Mod chars.Length))
        Next
        Return sb.ToString()
    End Function

    Private Function Chip(value As String, text As String, count As Integer, current As String) As String
        Dim active As Boolean = value = current
        Dim qs As String = If(Q <> "", "q=" & HttpUtility.UrlEncode(Q), "")
        Dim href As String = "Admin_Users.aspx" & If(value <> "" OrElse qs <> "", "?" & If(value <> "", "role=" & value & If(qs <> "", "&", ""), "") & qs, "")
        Return "<a href=""" & href & """ class=""tw-flex tw-items-center tw-gap-2 tw-rounded-full tw-border tw-px-3 tw-py-1.5 tw-text-sm tw-no-underline hover:tw-no-underline " &
               If(active, "tw-border-accent tw-bg-accent/15 tw-text-accent", "tw-border-line tw-text-muted hover:tw-text-ink") & """>" &
               HttpUtility.HtmlEncode(text) & "<span class=""tw-rounded-full tw-bg-surface-2 tw-px-1.5 tw-text-xs"">" & count & "</span></a>"
    End Function

    Private Function H(v As Object) As String
        Return HttpUtility.HtmlEncode(Convert.ToString(v))
    End Function

    Private Sub Alert(title As String, text As String, icon As String)
        ClientScript.RegisterStartupScript(Me.GetType(), "alert",
            "Swal.fire({ title: '" & SafeOut.Js(title) & "', text: '" & SafeOut.Js(text) & "', icon: '" & icon & "', confirmButtonText: 'ตกลง' });", True)
    End Sub

End Class
