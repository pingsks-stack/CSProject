
Partial Class production_version_Edit
    Inherits System.Web.UI.Page


    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim db = PgDb.Platform()
        Dim URL = Request.QueryString("DocID")
        db.Execute("update MAS_DOC set Student_Name=@Student_Name where (DocID = @DocID)", db.NewParam("Student_Name", Name_lname.Text), db.NewParam("DocID", URL))
        Dim err
        If db.IsError(err) Then
            MsgBox(err)
        End If
        db.Close()
    End Sub
End Class
