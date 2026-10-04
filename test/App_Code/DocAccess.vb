Imports System.Data.SqlClient
Imports System.Configuration
Imports System.IO
Imports System.Web

' ตรวจว่าผู้ใช้มีสิทธิ์ในโครงงาน (DocID ใน URL) ก่อนเข้าหน้าที่แก้ไข/ดูข้อมูลส่วนตัวของโครงงาน
' เรียกจาก Init ของ MasterPageHome / MasterPageTeacher ซึ่งทำงานก่อน event ของปุ่มทุกปุ่ม
Public Module DocAccess

    ' หน้าที่ต้องเป็นสมาชิกของโครงงาน (หน้าคลังโครงงานสาธารณะไม่อยู่ในรายการนี้)
    Private ReadOnly ProtectedPages As String() = {
        "mas_doc.aspx", "edituploadfile.aspx", "edit.aspx", "commentaj.aspx", "delete_inputaj.aspx",
        "history_changf.aspx", "changefriend.aspx", "change_friend.aspx", "pushfriend.aspx", "sent_pj.aspx",
        "infor_pass_n.aspx", "doc_infor_success.aspx", "pass_print.aspx", "index.aspx", "user_infor.aspx",
        "doc_infor_seeaj.aspx", "doc_student_infor.aspx", "confirm_ns.aspx", "confirms_change.aspx",
        "pass_aj.aspx", "comment_passaj.aspx", "infor_pass_rej.aspx", "history_changaj.aspx"
    }

    Public Sub Enforce(ctx As HttpContext)
        Dim page As String = Path.GetFileName(ctx.Request.Path).ToLowerInvariant()
        If Array.IndexOf(ProtectedPages, page) < 0 Then Return

        Dim docID As String = ctx.Request.QueryString("DocID")
        If String.IsNullOrEmpty(docID) Then Return ' หน้ารายการรวม ไม่ได้ระบุโครงงาน

        If Convert.ToString(ctx.Session("Urole")) = "3" Then Return ' แอดมินเข้าได้ทุกโครงงาน

        If Not CanAccess(docID, Convert.ToString(ctx.Session("Email"))) Then
            Dim home As String = If(Convert.ToString(ctx.Session("Urole")) = "2", "Doc_AllAJ.aspx", "Doc_All.aspx")
            ctx.Response.Redirect(home, True)
        End If
    End Sub

    Public Function CanAccess(docID As String, email As String) As Boolean
        Dim id As Integer
        If Not Integer.TryParse(docID, id) OrElse String.IsNullOrEmpty(email) Then Return False

        Dim cs As String = ConfigurationManager.ConnectionStrings("DOCDBConnectionString").ConnectionString
        Using con As New SqlConnection(cs)
            Using cmd As New SqlCommand(
                "SELECT CASE WHEN EXISTS (SELECT 1 FROM Permission_TB WHERE DocID = @DocID AND Email = @Email) " &
                "OR EXISTS (SELECT 1 FROM SubAdd_AJ_TB WHERE DocID = @DocID AND Email = @Email) THEN 1 ELSE 0 END", con)
                cmd.Parameters.AddWithValue("@DocID", id)
                cmd.Parameters.AddWithValue("@Email", email)
                con.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar()) = 1
            End Using
        End Using
    End Function

End Module
