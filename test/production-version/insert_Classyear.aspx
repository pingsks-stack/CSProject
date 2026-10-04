<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageAdmin.master" AutoEventWireup="false" CodeFile="insert_Classyear.aspx.vb" Inherits="production_version_Admin_infor" %>
<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

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
     .my-textbox {
                /* กำหนดสไตล์ CSS ที่ต้องการให้กับ TextBox นี้ */
                border: 1px solid #ccc;
                padding: 7px;
                font-size: 15px;
                border-radius: 3px;
            }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="container-fluid animatedParent animateOnce my-3">
        <div class="container-fluid my-3">
        <div class="d-flex row">
            <div class="col-md-12">
                <div class="card text-center">
                    <div class="card-header">
                        <h1>ข้อมูล</h1>
                       
                    </div>
                    <br />
                   <h4><span>เพิ่มปีการศึกษา</span></h4>
                    <hr />
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="my-textbox"></asp:TextBox>
                    <br />
                   <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="เพิ่ม" OnClick="Button1_Click" Width="20%" />

                </div>
            
        </div>
    </div>
        </div>
    </div>

</asp:Content>

