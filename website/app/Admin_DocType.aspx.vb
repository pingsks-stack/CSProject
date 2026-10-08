Imports System.Data
Imports System.Text

Partial Class production_version_Admin_DocType
    Inherits System.Web.UI.Page

    Protected RowsHtml As String

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        Dim dt = Shell.Query(
            "SELECT t.TypeID, t.Doc_Type, (SELECT COUNT(*) FROM MAS_DOC m WHERE m.Doc_Type = t.Doc_Type) AS Used " &
            "FROM DocTypeTB t ORDER BY CASE WHEN t.Doc_Type = 'Other' THEN 1 ELSE 0 END, t.TypeID")
        Dim sb As New StringBuilder()
        Dim i As Integer = 0
        For Each r As DataRow In dt.Rows
            i += 1
            Dim name As String = Convert.ToString(r("Doc_Type"))
            Dim id As String = Convert.ToString(r("TypeID"))
            Dim used As Integer = CInt(r("Used"))
            sb.Append("<tr>")
            sb.Append("<td class=""tw-text-muted"">" & i & "</td>")
            sb.Append("<td class=""tw-font-semibold"">" & HttpUtility.HtmlEncode(name) & "</td>")
            sb.Append("<td>" & used & "</td>")
            sb.Append("<td class=""tw-text-right tw-whitespace-nowrap"">")
            sb.Append("<button type=""button"" class=""btn btn-secondary btn-sm"" onclick=""typeAction('rename', '" & id & "', '" & SafeOut.JsAttr(name) & "')"">เปลี่ยนชื่อ</button> ")
            If used = 0 Then
                sb.Append("<button type=""button"" class=""btn btn-danger btn-sm"" onclick=""typeAction('delete', '" & id & "', '" & SafeOut.JsAttr(name) & "')"">ลบ</button>")
            Else
                sb.Append("<button type=""button"" class=""btn btn-secondary btn-sm"" disabled title=""มีโครงงานใช้ประเภทนี้อยู่"">ลบ</button>")
            End If
            sb.Append("</td></tr>")
        Next
        RowsHtml = sb.ToString()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Page.Validate("add")
        If Not Page.IsValid Then Return
        Dim name As String = txtNewType.Text.Trim()
        If Exists(name, -1) Then
            Alert("ซ้ำ", "มีประเภทนี้อยู่แล้ว", "warning")
            Return
        End If
        Shell.Exec("INSERT INTO DocTypeTB (TypeID, Doc_Type) SELECT COALESCE(MAX(TypeID), 0) + 1, @Name FROM DocTypeTB", Shell.P("@Name", name))
        txtNewType.Text = ""
        Alert("สำเร็จ", "เพิ่มประเภท """ & name & """ แล้ว", "success")
    End Sub

    Protected Sub btnDo_Click(sender As Object, e As EventArgs) Handles btnDo.Click
        Dim id As Integer
        If Not Integer.TryParse(hfId.Value, id) Then Return
        Dim current = Shell.Query("SELECT Doc_Type FROM DocTypeTB WHERE TypeID = @ID", Shell.P("@ID", id))
        If current.Rows.Count = 0 Then Return
        Dim oldName As String = Convert.ToString(current.Rows(0)(0))

        If hfAction.Value = "rename" Then
            Dim newName As String = hfValue.Value.Trim()
            If newName = "" OrElse newName.Length > 50 Then Return
            If Exists(newName, id) Then
                Alert("ซ้ำ", "มีประเภทชื่อนี้อยู่แล้ว", "warning")
                Return
            End If
            ' เปลี่ยนชื่อในโครงงานที่ใช้ประเภทเดิมด้วย
            Shell.Exec("UPDATE MAS_DOC SET Doc_Type = @New WHERE Doc_Type = @Old; UPDATE DocTypeTB SET Doc_Type = @New WHERE TypeID = @ID",
                        Shell.P("@New", newName), Shell.P("@Old", oldName), Shell.P("@ID", id))
            Alert("สำเร็จ", "เปลี่ยนชื่อประเภทแล้ว", "success")

        ElseIf hfAction.Value = "delete" Then
            If Dash.Count("SELECT COUNT(*) FROM MAS_DOC WHERE Doc_Type = @Name", Shell.P("@Name", oldName)) > 0 Then
                Alert("ลบไม่ได้", "มีโครงงานใช้ประเภทนี้อยู่", "warning")
                Return
            End If
            Shell.Exec("DELETE FROM DocTypeTB WHERE TypeID = @ID", Shell.P("@ID", id))
            Alert("สำเร็จ", "ลบประเภทแล้ว", "success")
        End If
    End Sub

    Private Function Exists(name As String, exceptId As Integer) As Boolean
        Return Dash.Count("SELECT COUNT(*) FROM DocTypeTB WHERE Doc_Type = @Name AND TypeID <> @ID",
                          Shell.P("@Name", name), Shell.P("@ID", exceptId)) > 0
    End Function

    Private Sub Alert(title As String, text As String, icon As String)
        ClientScript.RegisterStartupScript(Me.GetType(), "alert",
            "Swal.fire({ title: '" & SafeOut.Js(title) & "', text: '" & SafeOut.Js(text) & "', icon: '" & icon & "', confirmButtonText: 'ตกลง' });", True)
    End Sub

End Class
