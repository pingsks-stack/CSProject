<%@ WebHandler Language="VB" Class="ChatApi" %>

Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.Script.Serialization

' API ของหน้าแชท (Chat.aspx) ตอบเป็น JSON
'   GET  ?action=list                       รายการคู่สนทนา + ข้อความล่าสุด + จำนวนยังไม่อ่าน
'   GET  ?action=thread&with=<email>&after=<id>   ข้อความในห้อง (ใหม่กว่า id) และทำเครื่องหมายว่าอ่านแล้ว
'   GET  ?action=contacts                   รายชื่อที่เริ่มแชทใหม่ได้
'   POST action=send, with=<email>, text=<ข้อความ>
' แชทได้เฉพาะนิสิต <-> อาจารย์ และผู้ส่งเอามาจาก session เสมอ
Public Class ChatApi
    Implements IHttpHandler, IRequiresSessionState

    Public ReadOnly Property IsReusable As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Public Sub ProcessRequest(ctx As HttpContext) Implements IHttpHandler.ProcessRequest
        ctx.Response.ContentType = "application/json"
        ctx.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        Dim role As String = Convert.ToString(ctx.Session("Urole"))
        Dim me_ As String = Convert.ToString(ctx.Session("Email"))
        ' ต้องล็อกอินเป็นนิสิตหรืออาจารย์ และต้องเรียกจากหน้าเว็บของเรา (header นี้ส่งข้ามโดเมนไม่ได้ถ้าไม่ผ่าน CORS)
        If ctx.Session("UserID") Is Nothing OrElse (role <> "1" AndAlso role <> "2") OrElse
           ctx.Request.Headers("X-Requested-With") <> "fetch" Then
            Fail(ctx, 403, "forbidden")
            Return
        End If

        Select Case If(ctx.Request("action"), "")
            Case "list" : Write(ctx, ListConversations(me_, role))
            Case "contacts" : Write(ctx, Contacts(me_, role))
            Case "thread"
                Dim partner = ValidPartner(ctx.Request.QueryString("with"), role)
                If partner Is Nothing Then Fail(ctx, 400, "invalid partner") : Return
                Dim after As Integer
                Integer.TryParse(ctx.Request.QueryString("after"), after)
                Write(ctx, Thread(me_, role, partner, after))
            Case "send"
                If ctx.Request.HttpMethod <> "POST" Then Fail(ctx, 405, "POST only") : Return
                Dim partner = ValidPartner(ctx.Request.Form("with"), role)
                Dim text As String = If(ctx.Request.Form("text"), "").Trim()
                If partner Is Nothing Then Fail(ctx, 400, "invalid partner") : Return
                If text = "" OrElse text.Length > 500 Then Fail(ctx, 400, "ข้อความต้องยาว 1-500 ตัวอักษร") : Return
                Send(me_, Convert.ToString(ctx.Session("Name")), role, partner, text)
                Write(ctx, New With {.ok = True})
            Case Else
                Fail(ctx, 400, "unknown action")
        End Select
    End Sub

    ' คู่สนทนาต้องเป็นผู้ใช้ที่มีอยู่จริงและอยู่คนละฝั่ง (นิสิต <-> อาจารย์)
    Private Function ValidPartner(email As String, myRole As String) As DataRow
        If String.IsNullOrEmpty(email) Then Return Nothing
        Dim want As String = If(myRole = "1", "2", "1")
        Dim dt = Shell.Query("SELECT TOP 1 Email, Name FROM Signin_Signup WHERE Email = @Email AND Urole = @Role",
                             Shell.P("@Email", email), Shell.P("@Role", want))
        Return If(dt.Rows.Count = 1, dt.Rows(0), Nothing)
    End Function

    Private Function MyCol(role As String) As String
        Return If(role = "1", "Email_NS", "Email_AJ")
    End Function
    Private Function OtherCol(role As String) As String
        Return If(role = "1", "Email_AJ", "Email_NS")
    End Function

    Private Function ListConversations(me_ As String, role As String) As Object
        Dim sql As String =
            "SELECT p.Partner, u.Name, last.Comment_Text, last.Date_Save, last.Sender_Email, " &
            "(SELECT COUNT(*) FROM Talk_TB x WHERE x." & MyCol(role) & " = @Me AND x." & OtherCol(role) & " = p.Partner AND x.Is_Read = 0 AND x.Sender_Email <> @Me) AS Unread " &
            "FROM (SELECT DISTINCT " & OtherCol(role) & " AS Partner FROM Talk_TB WHERE " & MyCol(role) & " = @Me) p " &
            "OUTER APPLY (SELECT TOP 1 Comment_Text, Date_Save, Sender_Email FROM Talk_TB t WHERE t." & MyCol(role) & " = @Me AND t." & OtherCol(role) & " = p.Partner ORDER BY t.Talk_ID DESC) last " &
            "LEFT JOIN Signin_Signup u ON u.Email = p.Partner " &
            "ORDER BY last.Date_Save DESC"
        Return Shell.Query(sql, Shell.P("@Me", me_)).Rows.Cast(Of DataRow)().Select(Function(r) New With {
            .email = Convert.ToString(r("Partner")),
            .name = If(Convert.ToString(r("Name")) = "", Convert.ToString(r("Partner")), Convert.ToString(r("Name"))),
            .last = Convert.ToString(r("Comment_Text")),
            .mine = Convert.ToString(r("Sender_Email")) = me_,
            .at = Shell.TimeAgo(r("Date_Save")),
            .unread = CInt(r("Unread"))
        }).ToList()
    End Function

    Private Function Contacts(me_ As String, role As String) As Object
        Dim sql As String
        If role = "1" Then
            ' นิสิตเลือกได้ทุกอาจารย์ (อาจารย์ในโครงงานของตัวเองขึ้นก่อน)
            sql = "SELECT s.Email, s.Name, CASE WHEN EXISTS (SELECT 1 FROM Permission_TB t WHERE t.Email = s.Email AND t.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Me)) THEN 1 ELSE 0 END AS Mine " &
                  "FROM Signin_Signup s WHERE s.Urole = N'2' ORDER BY Mine DESC, s.Name"
        Else
            ' อาจารย์เลือกได้นิสิตในโครงงานที่ตัวเองดูแล
            sql = "SELECT DISTINCT s.Email, s.Name, 1 AS Mine FROM Signin_Signup s JOIN Permission_TB p ON p.Email = s.Email " &
                  "WHERE s.Urole = N'1' AND p.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Me AND Urole = N'2') ORDER BY s.Name"
        End If
        Return Shell.Query(sql, Shell.P("@Me", me_)).Rows.Cast(Of DataRow)().Select(Function(r) New With {
            .email = Convert.ToString(r("Email")),
            .name = Convert.ToString(r("Name")),
            .related = CInt(r("Mine")) = 1
        }).ToList()
    End Function

    Private Function Thread(me_ As String, role As String, partner As DataRow, after As Integer) As Object
        Dim other As String = Convert.ToString(partner("Email"))
        Dim cond As String = MyCol(role) & " = @Me AND " & OtherCol(role) & " = @Other"
        Dim rows = Shell.Query("SELECT TOP 300 Talk_ID, Comment_Text, Date_Save, Sender_Email FROM Talk_TB WHERE " & cond & " AND Talk_ID > @After ORDER BY Talk_ID",
                               Shell.P("@Me", me_), Shell.P("@Other", other), Shell.P("@After", after))
        Shell.Exec("UPDATE Talk_TB SET Is_Read = 1 WHERE " & cond & " AND Sender_Email = @Other AND Is_Read = 0",
                   Shell.P("@Me", me_), Shell.P("@Other", other))
        Dim th As New System.Globalization.CultureInfo("th-TH")
        Return New With {
            .partner = New With {.email = other, .name = Convert.ToString(partner("Name"))},
            .messages = rows.Rows.Cast(Of DataRow)().Select(Function(r) New With {
                .id = CInt(r("Talk_ID")),
                .text = Convert.ToString(r("Comment_Text")),
                .mine = Convert.ToString(r("Sender_Email")) = me_,
                .at = If(IsDBNull(r("Date_Save")), "", CDate(r("Date_Save")).ToString("d MMM HH:mm", th))
            }).ToList()
        }
    End Function

    Private Sub Send(me_ As String, myName As String, role As String, partner As DataRow, text As String)
        Dim other As String = Convert.ToString(partner("Email"))
        Shell.Exec("INSERT INTO Talk_TB (Email_AJ, Email_NS, Name, Comment_Text, Date_Save, Post_Name_RS, Urole, Sender_Email, Is_Read) " &
                   "VALUES (@AJ, @NS, @ToName, @Text, GETDATE(), @FromName, @Role, @Me, 0)",
                   Shell.P("@AJ", If(role = "2", me_, other)),
                   Shell.P("@NS", If(role = "1", me_, other)),
                   Shell.P("@ToName", Convert.ToString(partner("Name"))),
                   Shell.P("@Text", text),
                   Shell.P("@FromName", myName),
                   Shell.P("@Role", role),
                   Shell.P("@Me", me_))
    End Sub

    Private Sub Write(ctx As HttpContext, data As Object)
        ctx.Response.Write(New JavaScriptSerializer().Serialize(data))
    End Sub

    Private Sub Fail(ctx As HttpContext, code As Integer, message As String)
        ctx.Response.StatusCode = code
        Write(ctx, New With {.message = message})
    End Sub

End Class
