
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data
Imports DevExpress.Web.Bootstrap
Imports DevExpress.DataProcessing


Partial Class production_version_EditUploadfile
    Inherits System.Web.UI.Page




    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim docID As String = Request.QueryString("DocID")
            Dim permissionID As String = Request.QueryString("Permission_ID")

        End If
    End Sub

    Public ReadOnly Property docID() As String
        Get
            If ViewState("docID") IsNot Nothing Then
                Return ViewState("docID").ToString()
            Else
                ' ตั้งค่าค่าเริ่มต้นหรือการจัดการเมื่อไม่มีค่า ViewState("docID")
                Return String.Empty ' หรือค่าอื่นที่คุณต้องการ
            End If
        End Get
    End Property



    Protected Sub btnGeneratePDF_Click(sender As Object, e As EventArgs)
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim docID As String = Request.QueryString("DocID")
        Dim currentDate As DateTime = DateTime.Now
        Dim selectedMonth As Integer = currentDate.Month
        Dim selectedYear As Integer = currentDate.Year

        Dim classYearNo As String

        If (selectedMonth >= 6 And selectedMonth <= 7) Then
            classYearNo = (selectedYear + 543).ToString() + "/1"
        ElseIf (selectedMonth >= 8 And selectedMonth <= 12) Then
            classYearNo = (selectedYear + 543).ToString() + "/2"
        ElseIf (selectedMonth >= 1 And selectedMonth <= 3) Then
            classYearNo = (selectedYear + 542).ToString() + "/2"
        ElseIf (selectedMonth >= 4 And selectedMonth <= 5) Then
            classYearNo = (selectedYear + 542).ToString() + "/3"
        End If

        Dim updatePermission = db.Execute("UPDATE MAS_DOC SET Class_Year_No = @Class_Year_No, Submit_TC = 'ยังไม่มีการกดผ่าน/ไม่ผ่าน' WHERE DocID = @DocID",
                                  db.NewParam("DocID", docID),
                                  db.NewParam("Class_Year_No", classYearNo))

        Dim updatePermission1 = db.Execute("UPDATE Permission_TB SET Submit_TC = NULL WHERE DocID = @DocID",
                                  db.NewParam("DocID", docID))

        If (IsPostBack) Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Swal.fire({ title: 'สำเร็จ', text: 'ส่งข้อมูลโปรเจคไปยังปีการศึกษาต่อไปสำเร็จ!', icon: 'success', confirmButtonText: 'OK' }).then((result) => { window.location.href='Main_Doc.aspx'; });", True)
        End If

        db.Close()

    End Sub









End Class
