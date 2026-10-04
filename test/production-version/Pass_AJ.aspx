<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageTeacher.master" AutoEventWireup="false" CodeFile="Pass_AJ.aspx.vb" Inherits="production_version_Doc_Student" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
    .custom-textarea {
        width: 50%; /* กำหนดความกว้างเต็มแถว */
        height: 100px; /* กำหนดความสูง */
        padding: 5px; /* กำหนดระยะห่างของเนื้อหาภายใน */
        border: 1px solid #ccc; /* กำหนดเส้นขอบ */
        border-radius: 5px; /* กำหนดรูปร่างของขอบ */
        resize: vertical; /* กำหนดการปรับขนาดได้แค่แนวดิ่ง */
        font-family: Arial, sans-serif; /* กำหนดแบบอักษร */
        font-size: 14px; /* กำหนดขนาดตัวอักษร */
        color: #333; /* กำหนดสีตัวอักษร */
    }
</style>
      <style>
    /* CSS สำหรับ GridView */
    .gridview {
        /* รูปแบบตาราง */
        border-collapse: collapse;
        width: 100%;
        font-family: Arial, sans-serif;
        font-size: 16px;
        border: none;
         background-color: #e7e7e7;
    }
    
 .gridview th, .gridview td {
        /* รูปแบบเซลล์ตาราง */
        border: none;
        padding: 8px;
        height: 55px;
        color: black; /* เพิ่มสีฟอนต์เป็นสีดำ */
    }
    
    
    .gridview tr:nth-child(even) {
        /* รูปแบบแถวคู่ของตาราง */
        background-color: #f9f9f9;
    }
    
    .gridview tr:hover {
        /* รูปแบบแถวเมื่อเมาส์ชี้ */
        background-color: #ddd;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <br />
 <div class="container">
    <div class="row justify-content-center"> <!-- ใช้ justify-content-center เพื่อจัดตำแหน่งตรงกลาง -->
        <div class="col-md-12">
            <div class="card col-md-12">
                <div class="card-body text-center"> <!-- ใช้ text-center เพื่อจัดตำแหน่งตรงกลางข้อความ -->
                    <h5 class="card-title">ยินดีด้วย! โครงงานของคุณผ่านแล้ว</h5>
                    <br />
                    <h5 class="card-title">คลิ้กที่นี่เพื่อพิมพ์แบบฟอร์มการยืนยันโปรเจค</h5>
                    <asp:Button ID="btnGeneratePDF" runat="server" Text="สร้าง PDF" OnClick="btnGeneratePDF_Click" CssClass="btn btn-primary" /> <!-- เพิ่มคลาส btn btn-primary เพื่อจัดสีปุ่ม -->
                </div>
            </div>
        </div>
    </div>
</div>
   
</asp:Content>

