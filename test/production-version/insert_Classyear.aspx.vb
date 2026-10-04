
Partial Class production_version_Admin_infor
    Inherits System.Web.UI.Page


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Get the value from TextBox1
        Dim classYearNo As String = TextBox1.Text.Trim()

        ' Perform validation if necessary
        If String.IsNullOrEmpty(classYearNo) Then
            ' Handle validation error (e.g., show an error message)
            ' ...
            Return
        End If

        ' Perform the insert operation to the database
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim insertQuery As String = "INSERT INTO Class_Year_TB (Class_Year_No) VALUES (@ClassYearNo)"
        Dim insertResult = db.Execute(insertQuery, db.NewParam("ClassYearNo", classYearNo))

        ' Check if the insert was successful and perform further actions if needed
        If insertResult > 0 Then
            ' Insert successful, do something (e.g., show success message, refresh data, etc.)
            ' ...
        Else
            ' Insert failed, do something (e.g., show error message)
            ' ...
        End If

        ' Close the database connection
        db.Close()

        ' Clear the TextBox after successful insert
        TextBox1.Text = String.Empty
    End Sub


End Class
