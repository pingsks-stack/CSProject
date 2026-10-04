Imports System.Data
Imports System.Text

Partial Class production_version_CodeEdit
    Inherits System.Web.UI.Page

    Protected DocID As Integer
    Protected Subtitle, PickHtml, ListHtml, FormTitle As String
    Private codeID As Integer

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        ' แก้โค้ดได้เฉพาะนิสิต (สมาชิกโครงงาน) และแอดมิน
        Select Case Convert.ToString(Session("Urole"))
            Case "3" : MasterPageFile = "~/app/MasterPageAdmin.master"
            Case "1" : MasterPageFile = "~/app/MasterPageHome.master"
            Case Else : Response.Redirect("Code.aspx", True)
        End Select
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim email As String = Convert.ToString(Session("Email"))
        Integer.TryParse(Request.QueryString("DocID"), DocID)
        Integer.TryParse(Request.QueryString("id"), codeID)

        If DocID = 0 Then
            ShowPicker(email)
            Return
        End If
        ' ต้องเป็นสมาชิกของโครงงาน (แอดมินผ่าน)
        If Convert.ToString(Session("Urole")) <> "3" AndAlso Not DocAccess.CanAccess(DocID.ToString(), email) Then
            Response.Redirect("CodeEdit.aspx", True)
        End If

        Dim doc = Shell.Query("SELECT Doc_Name FROM MAS_DOC WHERE DocID = @DocID", Shell.P("@DocID", DocID))
        If doc.Rows.Count = 0 Then Response.Redirect("CodeEdit.aspx", True)
        Subtitle = "โครงงาน: " & Convert.ToString(doc.Rows(0)(0))
        pnlEdit.Visible = True

        If Not IsPostBack Then
            For i As Integer = 0 To CodeLib.Languages.GetLength(0) - 1
                ddlLang.Items.Add(New ListItem(CodeLib.Languages(i, 1), CodeLib.Languages(i, 0)))
            Next
            If codeID > 0 Then
                Dim row = Shell.Query("SELECT Doc_List_Type, Doc_Functions_Code, Doc_List_Code FROM Doc_List_TB WHERE Doc_List_ID = @ID AND DocID = @DocID",
                                      Shell.P("@ID", codeID), Shell.P("@DocID", DocID))
                If row.Rows.Count = 1 Then
                    Dim lang = CodeLib.Normalize(Convert.ToString(row.Rows(0)("Doc_List_Type")))
                    If ddlLang.Items.FindByValue(lang) Is Nothing Then ddlLang.Items.Add(New ListItem(lang, lang))
                    ddlLang.SelectedValue = lang
                    txtFunc.Text = Convert.ToString(row.Rows(0)("Doc_Functions_Code"))
                    txtCode.Text = Convert.ToString(row.Rows(0)("Doc_List_Code"))
                Else
                    codeID = 0
                End If
            End If
        End If
        FormTitle = If(codeID > 0, "แก้ไขฟังก์ชัน", "เพิ่มฟังก์ชันใหม่")
        btnDelete.Visible = codeID > 0
        ListHtml = BuildList()
    End Sub

    Private Sub ShowPicker(email As String)
        pnlPick.Visible = True
        Subtitle = "เลือกโครงงานที่ต้องการเพิ่มหรือแก้ไขซอร์สโค้ด"
        Dim dt = Shell.Query(
            "SELECT m.DocID, m.Doc_Name, m.Doc_Type, (SELECT COUNT(*) FROM Doc_List_TB c WHERE c.DocID = m.DocID) AS Codes " &
            "FROM MAS_DOC m WHERE m.DocID IN (SELECT DocID FROM Permission_TB WHERE Email = @Email) ORDER BY m.Doc_dt_Save DESC",
            Shell.P("@Email", email))
        Dim sb As New StringBuilder()
        For Each r As DataRow In dt.Rows
            sb.Append("<a href=""CodeEdit.aspx?DocID=" & r("DocID") & """ class=""panel tw-block tw-p-5 tw-no-underline hover:tw-border-accent hover:tw-no-underline"">")
            sb.Append("<span class=""kpi-icon tw-bg-accent/15 tw-text-accent""><i data-lucide=""folder-code""></i></span>")
            sb.Append("<div class=""tw-mt-3 tw-font-semibold tw-text-ink"">" & HttpUtility.HtmlEncode(Convert.ToString(r("Doc_Name"))) & "</div>")
            sb.Append("<div class=""tw-mt-1 tw-text-xs tw-text-muted"">" & r("Codes") & " ฟังก์ชัน</div></a>")
        Next
        If dt.Rows.Count = 0 Then
            sb.Append("<div class=""panel tw-col-span-full tw-p-10 tw-text-center tw-text-muted"">ยังไม่มีโครงงาน · <a href=""Insert.aspx"">เพิ่มโครงงาน</a></div>")
        End If
        PickHtml = sb.ToString()
    End Sub

    Private Function BuildList() As String
        Dim dt = Shell.Query("SELECT Doc_List_ID, Doc_List_Type, Doc_Functions_Code, Doc_Date_Save FROM Doc_List_TB WHERE DocID = @DocID ORDER BY Doc_Date_Save DESC",
                             Shell.P("@DocID", DocID))
        Dim sb As New StringBuilder()
        For Each r As DataRow In dt.Rows
            Dim active As Boolean = CInt(r("Doc_List_ID")) = codeID
            sb.Append("<a href=""CodeEdit.aspx?DocID=" & DocID & "&id=" & r("Doc_List_ID") & """ class=""tw-flex tw-items-center tw-justify-between tw-gap-2 tw-border-b tw-border-line tw-px-5 tw-py-3 tw-no-underline hover:tw-bg-surface-2 hover:tw-no-underline" & If(active, " tw-bg-accent/10", "") & """>")
            sb.Append("<span class=""tw-truncate tw-font-semibold tw-text-ink"">" & HttpUtility.HtmlEncode(Convert.ToString(r("Doc_Functions_Code"))) & "</span>")
            sb.Append("<span class=""badge-soft tw-shrink-0 tw-bg-gold/15 tw-text-gold"">" & HttpUtility.HtmlEncode(CodeLib.Display(Convert.ToString(r("Doc_List_Type")))) & "</span></a>")
        Next
        If dt.Rows.Count = 0 Then sb.Append("<div class=""tw-px-5 tw-py-10 tw-text-center tw-text-sm tw-text-muted"">ยังไม่มีโค้ด เพิ่มฟังก์ชันแรกได้ทางขวา</div>")
        Return sb.ToString()
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Page.Validate("code")
        If Not Page.IsValid OrElse DocID = 0 Then Return
        Dim lang As String = ddlLang.SelectedValue
        Dim func As String = txtFunc.Text.Trim()
        Dim code As String = txtCode.Text
        Dim action As String
        If codeID > 0 Then
            Shell.Exec("UPDATE Doc_List_TB SET Doc_List_Type = @Lang, Doc_Functions_Code = @Func, Doc_List_Code = @Code, Doc_Date_Save = GETDATE() WHERE Doc_List_ID = @ID AND DocID = @DocID",
                       Shell.P("@Lang", lang), Shell.P("@Func", func), Shell.P("@Code", code), Shell.P("@ID", codeID), Shell.P("@DocID", DocID))
            action = "Update"
        Else
            Dim newId = Shell.Query("INSERT INTO Doc_List_TB (DocID, Doc_List_Type, Doc_Functions_Code, Doc_List_Code, Doc_Date_Save) VALUES (@DocID, @Lang, @Func, @Code, GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS int)",
                                    Shell.P("@DocID", DocID), Shell.P("@Lang", lang), Shell.P("@Func", func), Shell.P("@Code", code))
            codeID = CInt(newId.Rows(0)(0))
            action = "Insert"
        End If
        Log(action, lang, func)
        Response.Redirect("CodeEdit.aspx?DocID=" & DocID & "&id=" & codeID & "&saved=1", False)
        Context.ApplicationInstance.CompleteRequest()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If codeID = 0 OrElse DocID = 0 Then Return
        Dim row = Shell.Query("SELECT Doc_List_Type, Doc_Functions_Code FROM Doc_List_TB WHERE Doc_List_ID = @ID AND DocID = @DocID", Shell.P("@ID", codeID), Shell.P("@DocID", DocID))
        If row.Rows.Count = 0 Then Return
        Shell.Exec("DELETE FROM Doc_List_TB WHERE Doc_List_ID = @ID AND DocID = @DocID", Shell.P("@ID", codeID), Shell.P("@DocID", DocID))
        Log("Delete", Convert.ToString(row.Rows(0)(0)), Convert.ToString(row.Rows(0)(1)))
        Response.Redirect("CodeEdit.aspx?DocID=" & DocID & "&deleted=1", False)
        Context.ApplicationInstance.CompleteRequest()
    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        If Request.QueryString("saved") = "1" Then Notify("success", "บันทึกโค้ดแล้ว")
        If Request.QueryString("deleted") = "1" Then Notify("success", "ลบฟังก์ชันแล้ว")
    End Sub

    Private Sub Log(action As String, lang As String, func As String)
        Shell.Exec("INSERT INTO Doc_List_Log (DocID, Doc_List_ID, Doc_List_Type, Doc_Functions_Code, Doc_Date_Save, Post_Name, Action) VALUES (@DocID, @ID, @Lang, @Func, GETDATE(), @Name, @Action)",
                   Shell.P("@DocID", DocID), Shell.P("@ID", codeID), Shell.P("@Lang", lang), Shell.P("@Func", func),
                   Shell.P("@Name", Convert.ToString(Session("Name"))), Shell.P("@Action", action))
    End Sub

    Private Sub Notify(kind As String, text As String)
        ClientScript.RegisterStartupScript(Me.GetType(), "notify", "document.addEventListener('DOMContentLoaded',function(){Notiflix.Notify." & kind & "('" & SafeOut.Js(text) & "');});", True)
    End Sub

End Class
