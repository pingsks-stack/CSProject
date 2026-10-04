<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="Comment_Pass.aspx.vb" Inherits="production_version_EditUploadfile" %>

<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Import Namespace="System.Collections.Generic" %>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<div class="container">
    <div class="row">
        <div class="col-md-12">
            <div class="card col-md-12">
                <div class="card-body">
                    <a href="index.aspx" class="btn btn-secondary mt-3">กลับไปยังหน้าเอกสารของฉัน</a>
                    <hr />
                    <h5 class="card-title">อัพโหลดไฟล์</h5>
                    <textarea id="TextArea1" runat="server" class="custom-textarea" cols="20" rows="2"></textarea>

                    <hr />
                    <asp:TextBox ID="TextBox1" runat="server" Width="40%"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="ตรวจสอบภาษา" OnClick="Button1_Click" CssClass="absolute" />                   
                </div>
            </div>
        </div>
    </div>
</div>






</asp:Content>

