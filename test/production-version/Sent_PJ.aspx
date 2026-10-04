<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="Sent_PJ.aspx.vb" Inherits="production_version_EditUploadfile" %>

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
                    <h5 class="card-title">เสียใจด้วย โปรเจคของคุณยังไม่ผ่าน</h5>
                    <br />
                    <h5 class="card-title">คลิ้กที่นี่เพื่อส่งโปรเจคไปยังปีการศึกษาต่อไป</h5>
                    <asp:Button ID="btnGeneratePDF" runat="server" Text="ส่งข้อมูลโปรเจค" OnClick="btnGeneratePDF_Click" CssClass="btn btn-primary" /> <!-- เพิ่มคลาส btn btn-primary เพื่อจัดสีปุ่ม -->
                </div>
            </div>
        </div>
    </div>
</div>

</asp:Content>

