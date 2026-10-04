
Partial Class production_version_Doc_AllAJ
    Inherits System.Web.UI.Page

    Protected Sub ddlFilter_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim selectedValue As String = ddlFilter.SelectedValue
        Dim sqlDataSource As SqlDataSource = SqlDataSourceInfor
        Dim selectCommand As String = sqlDataSource.SelectCommand
        ' รับค่าที่เลือกจากทั้งสอง DropDownList
        Dim selectedClass As String = ddlFilter.SelectedValue
        'Dim selectedYear As String = ddYear.SelectedValue

        If selectedValue = "all" Then
            ' ไม่มีการกรอง
            sqlDataSource.SelectCommand = "SELECT DocID, Doc_Name, Doc_NameEN, Student_Name, Student_Name2, Name_teacherPJ, Submit_TC FROM MAS_DOC WHERE Submit_TC = N'ผ่าน3/3'"
        ElseIf selectedValue = "P4" Then
            ' กรองเฉพาะชั้นปี 4
            sqlDataSource.SelectCommand = "SELECT DocID, Doc_Name, Doc_NameEN, Student_Name, Student_Name2, Name_teacherPJ, Submit_TC, Class_No FROM MAS_DOC WHERE (Class_No = '4') AND (Submit_TC = N'ผ่าน3/3')"
        ElseIf selectedValue = "P5" Then
            ' กรองเฉพาะชั้นปี 5
            sqlDataSource.SelectCommand = "SELECT DocID, Doc_Name, Doc_NameEN, Student_Name, Student_Name2, Name_teacherPJ, Submit_TC, Class_No FROM MAS_DOC WHERE (Class_No = '5') AND (Submit_TC = N'ผ่าน3/3')"
        ElseIf selectedValue = "P6" Then
            ' กรองเฉพาะชั้นปี 6
            sqlDataSource.SelectCommand = "SELECT DocID, Doc_Name, Doc_NameEN, Student_Name, Student_Name2, Name_teacherPJ, Submit_TC, Class_No FROM MAS_DOC WHERE (Class_No = '6') AND (Submit_TC = N'ผ่าน3/3')"
        ElseIf selectedValue = "P7" Then
            ' กรองเฉพาะชั้นปี 7
            sqlDataSource.SelectCommand = "SELECT DocID, Doc_Name, Doc_NameEN, Student_Name, Student_Name2, Name_teacherPJ, Submit_TC, Class_No FROM MAS_DOC WHERE (Class_No = '7') AND (Submit_TC = N'ผ่าน3/3')"
        ElseIf selectedValue = "P8" Then
            ' กรองเฉพาะชั้นปี 8
            sqlDataSource.SelectCommand = "SELECT DocID, Doc_Name, Doc_NameEN, Student_Name, Student_Name2, Name_teacherPJ, Submit_TC, Class_No FROM MAS_DOC WHERE (Class_No = '8') AND (Submit_TC = N'ผ่าน3/3')"
        End If

        ' รีเฟรช GridView เพื่อแสดงข้อมูลใหม่
        GridView1.DataBind()
        'ApplyFilters(selectedClass, selectedYear)
    End Sub


    ' คลังโครงงานของอาจารย์ รวมไปใช้ Doc_All.aspx (เลือกเมนูตามบทบาทเอง)
    Protected Sub RedirectToLibrary(sender As Object, e As EventArgs) Handles Me.PreInit
        Response.Redirect("Doc_All.aspx", True)
    End Sub
End Class
