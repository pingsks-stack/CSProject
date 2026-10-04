Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data
Imports DevExpress.Web.Bootstrap
Partial Class production_version_Doc_Student
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

    'Protected Sub btnGeneratePDF_Click(sender As Object, e As EventArgs)
    ''    Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
    '    Dim docID As String = Request.QueryString("DocID")
    '    Dim permissionID As String = Request.QueryString("Permission_ID")
    '    ' โค้ดสร้าง PDF จะถูกวางที่นี่


    '    ' สร้างเอกสาร PDF
    '    Dim doc As New Document()
    '    Dim pdfFilePath As String = Server.MapPath("~/YourPDFFileName.pdf")
    '    Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(pdfFilePath, FileMode.Create))

    '    ' เปิดเอกสาร PDF
    '    doc.Open()

    '    ' สร้างตาราง PDF
    '    Dim table As New PdfPTable(4) ' 4 คอลัมน์สำหรับ DocID, Name, Urole, และ Submit_TC

    '    ' เพิ่มหัวข้อคอลัมน์
    '    table.AddCell("DocID")
    '    table.AddCell("Name")
    '    table.AddCell("Urole")
    '    table.AddCell("Submit_TC")

    '    ' คำนวณข้อมูลจากฐานข้อมูล
    '    Dim sql As String = "SELECT DocID, Name, Urole, Submit_TC FROM Permission_TB WHERE DocID = @DocID"
    '    Dim parameters As New List(Of SqlParameter)()
    '    parameters.Add(New SqlParameter("@DocID", docID))

    '    ' ใช้ Zulu.Data.PlatformFactory เพื่อเรียกข้อมูลจากฐานข้อมูล
    '    Dim data = db.GetDataTable(sql, parameters.ToArray())

    '    ' ตรวจสอบว่า data ไม่เป็นค่า null
    '    If data IsNot Nothing Then
    '        ' อ่านข้อมูลจากตารางและเพิ่มลงในตาราง PDF
    '        For Each row As DataRow In data.Rows
    '            table.AddCell(row("DocID").ToString())
    '            table.AddCell(row("Name").ToString())
    '            table.AddCell(row("Urole").ToString())
    '            table.AddCell(row("Submit_TC").ToString())
    '        Next
    '    End If


    '    ' เพิ่มตาราง PDF ลงในเอกสาร
    '    doc.Add(table)

    '    ' ปิดเอกสาร PDF
    '    doc.Close()

    '    ' ดาวน์โหลดไฟล์ PDF
    '    Response.ContentType = "application/pdf"
    '    Response.AddHeader("content-disposition", "attachment;filename=YourPDFFileName.pdf")
    '    Response.TransmitFile(pdfFilePath)
    '    Response.End()
    'End Sub

    Protected Sub btnGeneratePDF_Click(sender As Object, e As EventArgs)
        Dim db = Zulu.Data.PlatformFactory.GetPlatform("DOCDBConnectionString", True, False)
        Dim docID As String = Request.QueryString("DocID")

        ' สร้างเอกสาร PDF
        ' สร้างฟอนต์สำหรับใช้ใน PDF
        Dim fontPath As String = Server.MapPath("~/app/Fonts/") ' เปลี่ยนเป็นพาทของโฟลเดอร์ที่เก็บฟอนต์ของคุณ
        Dim thaiFontPath As String = Path.Combine(fontPath, "TH Niramit AS Regular.ttf") ' ใช้ชื่อฟอนต์ที่คุณอัพโหลด
        Dim baseFont As BaseFont = BaseFont.CreateFont(thaiFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)

        ' สร้างฟอนต์ Thai สำหรับใช้ใน PDF
        Dim thaiFont As New Font(baseFont, 12)

        ' สร้างเอกสาร PDF
        Dim doc As New Document()
        Dim pdfFilePath As String = Server.MapPath("~/แบบฟรอมยืนยันโปรเจค.pdf")
        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, New FileStream(pdfFilePath, FileMode.Create))

        doc.Open()

        ' เพิ่มหัวข้อลงใน PDF
        Dim headerFont As New Font(baseFont, 21, Font.BOLD) ' กำหนดรูปแบบและขนาดฟอนต์สำหรับหัวข้อ

        Dim header As New Paragraph("แบบฟอร์มยืนยันโปรเจค", headerFont)
        header.Alignment = Element.ALIGN_CENTER ' จัดกลางข้อความ
        doc.Add(header) ' เพิ่มหัวข้อลงในเอกสาร

        Dim subTitleFont As New Font(baseFont, 18, Font.NORMAL) ' กำหนดรูปแบบและขนาดฟอนต์สำหรับข้อความย่อย
        Dim subTitleFont2 As New Font(baseFont, 18, Font.BOLD)

        Dim subTitle1 As New Paragraph("วิชา 225492 โครงงานวิทยาการคอมพิวเตอร์", subTitleFont)
        subTitle1.Alignment = Element.ALIGN_CENTER
        doc.Add(subTitle1)

        Dim subTitle2 As New Paragraph("สาขาวิทยาการคอมพิวเตอร์ คณะเทคโนโลยีสารสนเทศและการสื่อสาร", subTitleFont)
        subTitle2.Alignment = Element.ALIGN_CENTER
        doc.Add(subTitle2)

        Dim subTitle3 As New Paragraph("มหาวิทยาลัยพะเยา", subTitleFont)
        subTitle3.Alignment = Element.ALIGN_CENTER
        doc.Add(subTitle3)

        Dim subTitle4 As New Paragraph("------------------------------------------------------------", subTitleFont)
        subTitle4.Alignment = Element.ALIGN_CENTER
        doc.Add(subTitle4)

        ' ดึงข้อมูลชื่อโครงงานจากฐานข้อมูล
        Dim projectNames As DataTable = db.GetDataTable("SELECT Doc_Name, Doc_NameEN FROM MAS_DOC WHERE DocID = @DocID",
                                                 db.NewParam("DocID", docID))

        ' ตรวจสอบว่ามีข้อมูลหรือไม่
        If projectNames IsNot Nothing AndAlso projectNames.Rows.Count > 0 Then
            Dim projectNameRow As DataRow = projectNames.Rows(0)
            Dim projectNameThai As String = projectNameRow("Doc_Name").ToString()
            Dim projectNameEnglish As String = projectNameRow("Doc_NameEN").ToString()

            ' เพิ่มชื่อโครงงาน (ภาษาไทย)
            Dim projectNameThaiParagraph As New Paragraph("ชื่อโครงงาน (ภาษาไทย): " & projectNameThai, subTitleFont)
            doc.Add(projectNameThaiParagraph)

            ' เพิ่มชื่อโครงงาน (ภาษาอังกฤษ)
            Dim projectNameEnglishParagraph As New Paragraph("ชื่อโครงงาน (ภาษาอังกฤษ): " & projectNameEnglish, subTitleFont)
            doc.Add(projectNameEnglishParagraph)
        End If

        ' ดึงข้อมูลนิสิตคนที่ 1
        Dim student1Data As DataTable = db.GetDataTable("SELECT Student_Name, Student_ID, Email, Mobile FROM MAS_DOC WHERE DocID = @DocID",
                                                 db.NewParam("DocID", docID))

        ' ตรวจสอบว่ามีข้อมูลของนิสิตคนที่ 1
        If student1Data IsNot Nothing AndAlso student1Data.Rows.Count > 0 Then
            Dim student1Row As DataRow = student1Data.Rows(0)
            Dim student1Name As String = student1Row("Student_Name").ToString()
            Dim student1ID As String = student1Row("Student_ID").ToString()
            Dim student1Email As String = student1Row("Email").ToString()
            Dim student1Mobile As String = student1Row("Mobile").ToString()

            ' เพิ่มข้อมูลนิสิตคนที่ 1ใน PDF
            Dim student1InfoParagraph As New Paragraph("ข้อมูลนิสิตคนที่ 1", subTitleFont2)
            doc.Add(student1InfoParagraph)

            Dim student1NameIDParagraph As New Paragraph("ชื่อ: " & student1Name & " - รหัสนิสิต: " & student1ID, subTitleFont)
            doc.Add(student1NameIDParagraph)

            Dim student1ContactParagraph As New Paragraph("อีเมลล์: " & student1Email & " - เบอร์โทร: " & student1Mobile, subTitleFont)
            doc.Add(student1ContactParagraph)

        End If
        ' ดึงข้อมูลนิสิตคนที่ 2
        Dim student2Data As DataTable = db.GetDataTable("SELECT Student_Name2, Student_ID2, Email2, Mobile2 FROM MAS_DOC WHERE DocID = @DocID",
                                                 db.NewParam("DocID", docID))

        ' ตรวจสอบว่ามีข้อมูลของนิสิตคนที่ 2
        If student2Data IsNot Nothing AndAlso student2Data.Rows.Count > 0 Then
            Dim student2Row As DataRow = student2Data.Rows(0)
            Dim student2Name As String = student2Row("Student_Name2").ToString()
            Dim student2ID As String = student2Row("Student_ID2").ToString()
            Dim student2Email As String = student2Row("Email2").ToString()
            Dim student2Mobile As String = student2Row("Mobile2").ToString()

            ' เพิ่มข้อมูลนิสิตคนที่ 2 ใน PDF ด้านขวาของข้อมูลคนที่ 1
            Dim student2InfoParagraph As New Paragraph("ข้อมูลนิสิตคนที่ 2", subTitleFont2)
            doc.Add(student2InfoParagraph)

            Dim student2NameIDParagraph As New Paragraph("ชื่อ: " & student2Name & " - รหัสนิสิต: " & student2ID, subTitleFont)
            doc.Add(student2NameIDParagraph)

            Dim student2ContactParagraph As New Paragraph("อีเมลล์: " & student2Email & " - เบอร์โทร: " & student2Mobile, subTitleFont)
            doc.Add(student2ContactParagraph)

            doc.Add(New Paragraph(" "))
            doc.Add(New Paragraph(" "))


        End If


        Dim header2 As New Paragraph("ข้อมูลการยืนยันโปรเจค", headerFont)
        header2.Alignment = Element.ALIGN_CENTER ' จัดกลางข้อความ
        doc.Add(header2) ' เพิ่มหัวข้อลงในเอกสาร
        doc.Add(New Paragraph(" "))
        ' สร้างตาราง PDF
        Dim table As New PdfPTable(2) ' 2 คอลัมน์สำหรับ Name และ Submit_TC
        table.WidthPercentage = 100 ' กำหนดความกว้างของตารางให้เต็มหน้ากระดาษ

        ' เพิ่มส่วนหัวของตาราง
        Dim headerCell1 As New PdfPCell(New Phrase("ชื่อ-นามสกุลอาจารย์", subTitleFont2))
        headerCell1.Phrase.Font.Size = 18 ' กำหนดขนาดฟอนต์
        headerCell1.HorizontalAlignment = Element.ALIGN_CENTER ' จัดกลางแนวนอน
        headerCell1.VerticalAlignment = Element.ALIGN_MIDDLE ' จัดกลางแนวตั้ง
        table.AddCell(headerCell1)

        Dim headerCell2 As New PdfPCell(New Phrase("ผลลัพธ์", subTitleFont2))
        headerCell2.Phrase.Font.Size = 18 ' กำหนดขนาดฟอนต์
        headerCell2.HorizontalAlignment = Element.ALIGN_CENTER ' จัดกลางแนวนอน
        headerCell2.VerticalAlignment = Element.ALIGN_MIDDLE ' จัดกลางแนวตั้ง
        table.AddCell(headerCell2)

        ' ดึงข้อมูลจากฐานข้อมูล
        Dim sql As String = "SELECT Name, Submit_TC FROM Permission_TB WHERE DocID = @DocID AND Urole = 2"
        Dim parameters As New List(Of SqlParameter)()
        parameters.Add(New SqlParameter("@DocID", docID))

        Dim data = db.GetDataTable(sql, parameters.ToArray())

        ' ตรวจสอบว่า data ไม่เป็นค่า null
        If data IsNot Nothing Then
            For Each row As DataRow In data.Rows
                Dim cell1 As New PdfPCell(New Phrase(row("Name").ToString(), subTitleFont))
                cell1.Phrase.Font.Size = 18 ' กำหนดขนาดฟอนต์
                cell1.HorizontalAlignment = Element.ALIGN_CENTER ' จัดกลางแนวนอน
                cell1.VerticalAlignment = Element.ALIGN_MIDDLE ' จัดกลางแนวตั้ง
                table.AddCell(cell1)

                Dim cell2 As New PdfPCell(New Phrase(row("Submit_TC").ToString(), subTitleFont))
                cell2.Phrase.Font.Size = 18 ' กำหนดขนาดฟอนต์
                cell2.HorizontalAlignment = Element.ALIGN_CENTER ' จัดกลางแนวนอน
                cell2.VerticalAlignment = Element.ALIGN_MIDDLE ' จัดกลางแนวตั้ง
                table.AddCell(cell2)
            Next
        End If



        ' เพิ่มตาราง PDF ลงในเอกสาร
        doc.Add(table)
        doc.Close()

        ' ดาวน์โหลดไฟล์ PDF
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment;filename=แบบฟอร์มยืนยันโปรเจค.pdf")
        Response.TransmitFile(pdfFilePath)
        Response.End()

    End Sub







End Class
