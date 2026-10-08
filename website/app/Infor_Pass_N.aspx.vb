
Imports Npgsql
Imports System.IO
Imports DevExpress.Web
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data
Imports DevExpress.Web.Bootstrap


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






    'Protected Sub btnGeneratePDF_Click(sender As Object, e As EventArgs)
    ''    Dim db = PgDb.Platform()
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
    '    Dim parameters As New List(Of NpgsqlParameter)()
    '    parameters.Add(New NpgsqlParameter("@DocID", docID))

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
        Dim db = PgDb.Platform()
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
        Dim subTitleFont3 As New Font(baseFont, 18, Font.NORMAL)


        Dim header As New Paragraph("อาจารย์ที่ปรึกษาและประธานหลักสูตรวิทยาศาสตร์บัณฑิต", subTitleFont3)
        header.Alignment = Element.ALIGN_CENTER ' จัดกลางข้อความ
        doc.Add(header) ' เพิ่มหัวข้อลงในเอกสาร

        Dim subTitleFont As New Font(baseFont, 18, Font.NORMAL) ' กำหนดรูปแบบและขนาดฟอนต์สำหรับข้อความย่อย
        Dim subTitleFont2 As New Font(baseFont, 18, Font.BOLD)

        Dim subTitle1 As New Paragraph("สาขาวิชาวิทยาการ คอมพิวเตอร์คณะเทคโนโลยีสารสนเทศและการสื่อสาร มหาวิทยาลัยพะเยา ", subTitleFont)
        subTitle1.Alignment = Element.ALIGN_CENTER
        doc.Add(subTitle1)

        Dim subTitle2 As New Paragraph("สาขาวิทยาการคอมพิวเตอร์ คณะเทคโนโลยีสารสนเทศและการสื่อสาร", subTitleFont)
        subTitle2.Alignment = Element.ALIGN_CENTER
        doc.Add(subTitle2)


        Dim projectNamesc As DataTable = db.GetDataTable("SELECT Doc_Name, Doc_NameEN FROM MAS_DOC WHERE DocID = @DocID",
                                                 db.NewParam("DocID", docID))

        ' ตรวจสอบว่ามีข้อมูลหรือไม่
        If projectNamesc IsNot Nothing AndAlso projectNamesc.Rows.Count > 0 Then
            Dim projectNameRow As DataRow = projectNamesc.Rows(0)
            Dim projectNameThai As String = projectNameRow("Doc_Name").ToString()
            Dim projectNameEnglish As String = projectNameRow("Doc_NameEN").ToString()

            Dim subTitle4 As New Paragraph("ได้พิจารณาภาค นิพนธ์เรื่อง " & projectNameThai, subTitleFont)
            subTitle4.Alignment = Element.ALIGN_CENTER
            doc.Add(subTitle4)

        End If

        Dim subTitle5 As New Paragraph("เห็นสมควรรับเป็นส่วนหนึ่งของการศึกษารายวิชา 225492 ", subTitleFont)
        subTitle5.Alignment = Element.ALIGN_CENTER
        doc.Add(subTitle5)

        Dim subTitle6 As New Paragraph("โครงงานวิทยาการคอมพิวเตอร์ภาคการศึกษาต้นปีการศึกษา 2566 มหาวิทยาลัยพะเยา ", subTitleFont)
        subTitle6.Alignment = Element.ALIGN_CENTER
        doc.Add(subTitle6)

        Dim subTitle7 As New Paragraph("------------------------------------------------------------", subTitleFont)
        subTitle7.Alignment = Element.ALIGN_CENTER
        doc.Add(subTitle7)

        doc.Add(New Paragraph(" "))
        doc.Add(New Paragraph(" "))
        doc.Add(New Paragraph(" "))

        Dim aj1Displayed As Boolean = False
        Dim aj2Displayed As Boolean = False
        Dim aj3Displayed As Boolean = False



        Dim AJ1 As DataTable = db.GetDataTable("SELECT Name, Urole_TC FROM Permission_TB WHERE  (DocID = @DocID) AND (Urole_TC = 'ที่ปรึกษา')",
                                                 db.NewParam("DocID", docID))

        ' ตรวจสอบว่ามีข้อมูลหรือไม่
        If AJ1 IsNot Nothing AndAlso AJ1.Rows.Count > 0 AndAlso Not aj1Displayed Then
            Dim NameAJ1row As DataRow = AJ1.Rows(0)
            Dim Nameaj1 As String = NameAJ1row("Name").ToString()
            Dim UroleAJ As String = NameAJ1row("Urole_TC").ToString()

            Dim subTitleAj11 As New Paragraph(Nameaj1, subTitleFont)
            subTitleAj11.Alignment = Element.ALIGN_CENTER
            doc.Add(subTitleAj11)

            Dim subTitleAj1 As New Paragraph(UroleAJ, subTitleFont)
            subTitleAj1.Alignment = Element.ALIGN_CENTER
            doc.Add(subTitleAj1)

            ' บันทึกว่าข้อมูลของอาจารย์ 1 ถูกแสดงแล้ว
            aj1Displayed = True
        End If

        doc.Add(New Paragraph(" "))
        doc.Add(New Paragraph(" "))
        doc.Add(New Paragraph(" "))


        Dim AJ2 As DataTable = db.GetDataTable("SELECT Name, Urole_TC FROM Permission_TB WHERE  (DocID = @DocID) AND (Urole_TC = 'กรรมการ')",
                                         db.NewParam("DocID", docID))

        If AJ2 IsNot Nothing AndAlso AJ2.Rows.Count > 0 Then
            For Each row As DataRow In AJ2.Rows
                Dim Nameaj2 As String = row("Name").ToString()
                Dim UroleAJ As String = row("Urole_TC").ToString()

                Dim subTitleAj22 As New Paragraph(Nameaj2, subTitleFont)
                subTitleAj22.Alignment = Element.ALIGN_CENTER
                doc.Add(subTitleAj22)

                Dim subTitleAj222 As New Paragraph(UroleAJ, subTitleFont)
                subTitleAj222.Alignment = Element.ALIGN_CENTER
                doc.Add(subTitleAj222)

                doc.Add(New Paragraph(" "))
                doc.Add(New Paragraph(" "))
                doc.Add(New Paragraph(" "))

            Next
        End If

        doc.Add(New Paragraph(" "))
        doc.Add(New Paragraph(" "))


        Dim subTitle10 As New Paragraph("อาจารย์ วรกิต แสนโภช", subTitleFont)
        subTitle10.Alignment = Element.ALIGN_CENTER
        doc.Add(subTitle10)

        doc.Add(New Paragraph(" "))

        Dim subTitle11 As New Paragraph("ประธานหลักสูตรวิทยาศาสตร์บัณฑิต สาขาวิชาวิทยาการคอมพิวเตอร์", subTitleFont)
        subTitle11.Alignment = Element.ALIGN_CENTER
        doc.Add(subTitle11)

        Dim subTitle12 As New Paragraph("คณะเทคโนโลยีสารสนเทศและการสื่อสาร มหาวิทยาลัยพะเยา", subTitleFont)
        subTitle12.Alignment = Element.ALIGN_CENTER
        doc.Add(subTitle12)

        'Dim AJ3 As DataTable = db.GetDataTable("SELECT Name, Urole_TC FROM Permission_TB WHERE  (DocID = @DocID) AND (Urole_TC = 'กรรมการ')",
        '                                 db.NewParam("DocID", docID))

        'If AJ3 IsNot Nothing AndAlso AJ3.Rows.Count > 0 Then
        '    For Each row As DataRow In AJ3.Rows
        '        Dim Nameaj3 As String = row("Name").ToString()
        '        Dim UroleAJ As String = row("Urole_TC").ToString()

        '        Dim subTitleAj33 As New Paragraph(Nameaj3, subTitleFont)
        '        subTitleAj33.Alignment = Element.ALIGN_CENTER
        '        doc.Add(subTitleAj33)

        '        Dim subTitleAj333 As New Paragraph(UroleAJ, subTitleFont)
        '        subTitleAj333.Alignment = Element.ALIGN_CENTER
        '        doc.Add(subTitleAj333)
        '    Next
        'End If


        doc.Close()

        ' ดาวน์โหลดไฟล์ PDF
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment;filename=แบบฟอร์มยืนยันโปรเจค.pdf")
        Response.TransmitFile(pdfFilePath)
        Response.End()

    End Sub









End Class
