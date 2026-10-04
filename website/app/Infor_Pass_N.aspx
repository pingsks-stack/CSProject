<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="Infor_Pass_N.aspx.vb" Inherits="production_version_EditUploadfile" %>

<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

   <style>
    /* CSS สำหรับ GridView */
   .gridview {
    font-family: Arial, sans-serif;
    font-size: 12px;
    width: 100%;
    border-collapse: collapse;
}

.gridview th, .gridview td {
    border: 1px solid #000;
    padding: 8px;
    text-align: left;
}

.gridview th {
    background-color: #f2f2f2;
    font-weight: bold;
}

/* ปรับขนาดและรูปแบบตามความต้องการ */

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <br />
 <div class="container">
    <div class="row justify-content-center"> <!-- ใช้ justify-content-center เพื่อจัดตำแหน่งตรงกลาง -->
        <div class="col-md-12">
            <div class="card col-md-12">
                <div class="card-body text-center"> <!-- ใช้ text-center เพื่อจัดตำแหน่งตรงกลางข้อความ -->
                    <h5 class="card-title">ยินดีด้วย! โครงงานของคุณแล้ว</h5>
                    <br />
                    <h5 class="card-title">คลิ้กที่นี่เพื่อพิมพ์แบบฟอร์มการยืนยันโปรเจค</h5>
                    <asp:Button ID="btnGeneratePDF" runat="server" Text="สร้าง PDF" OnClick="btnGeneratePDF_Click" CssClass="btn btn-success" /> <!-- เพิ่มคลาส btn btn-primary เพื่อจัดสีปุ่ม -->
                    <asp:Button ID="btnOpenOtherPage" runat="server" Text="ดูข้อมูลของฉัน" OnClientClick="redirectToOtherPage(); return false;" CssClass="btn btn-primary" />
                    <script type="text/javascript">
                        function redirectToOtherPage() {
                            // ดึงค่า DocID จาก Request.QueryString หรือค่าอื่น ๆ ที่คุณต้องการส่ง
                            var docID = "<%= Request.QueryString("DocID") %>"; // ให้ดึงค่าจาก ASP.NET

                            // สร้าง URL ใหม่ที่รวม DocID และเปลี่ยนเส้นทางไปยังหน้าอื่น
                            var otherPageURL = "Doc_Infor_Success.aspx?DocID=" + docID;

                            // นำคุณลักษณะคุณเปิดหน้าใหม่ (เช่น ในหน้าเว็บเบราวเซอร์)
                            window.location.href = otherPageURL;
                        }
                    </script>

                </div>
            </div>
        </div>
    </div>
</div>

</asp:Content>

