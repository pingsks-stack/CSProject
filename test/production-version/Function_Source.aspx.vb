
Partial Class production_version_Function_Source
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsPostBack Then
            ' กำหนดค่าเริ่มต้นให้ DropDownList1
            DropDownList1.SelectedValue = "" ' ให้เป็นค่าว่างเริ่มต้น
        End If

        ' อื่น ๆ ที่คุณทำใน Page_Load
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim selectedValue As String = DropDownList1.SelectedValue

        If selectedValue = "ข้อมูลทั้งหมด" Then
            ' คิวรีหรือคำสั่งสำหรับแสดงข้อมูลทั้งหมดใน DataList
            SqlDataShow_DocList.SelectCommand = "SELECT Doc_List_Type, Doc_List_Code, Doc_Functions_Code, Doc_List_ID FROM Doc_List_TB"
        Else
            ' คำสั่งสำหรับแสดงข้อมูลเฉพาะตามค่าที่ถูกเลือกใน DropDownList
            SqlDataShow_DocList.SelectCommand = "SELECT Doc_List_Type, Doc_List_Code, Doc_Functions_Code, Doc_List_ID FROM Doc_List_TB WHERE Doc_Functions_Code = @DocFunctionsCode"
        End If

        SqlDataShow_DocList.DataBind()
    End Sub






    ' หน้าซอร์สโค้ดเก่า ย้ายไปใช้ Code.aspx / CodeEdit.aspx
    Protected Sub RedirectToNewCode(sender As Object, e As EventArgs) Handles Me.PreInit
        Response.Redirect("Code.aspx", True)
    End Sub
End Class
