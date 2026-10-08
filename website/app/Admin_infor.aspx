<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageAdmin.master" AutoEventWireup="false" CodeFile="Admin_infor.aspx.vb" Inherits="production_version_Admin_infor" %>
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
     .form-control {
        width: 200px;
        margin-bottom: 10px;
        margin-right: 10px;
        float: right;
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
                    <div class="card-body">                      
                        <asp:DropDownList ID="ddlFilter" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                            <asp:ListItem Value="all">ทั้งหมด</asp:ListItem>
                            <asp:ListItem Value="P4">ชั้นปีที่ 4</asp:ListItem>
                            <asp:ListItem Value="P5">ชั้นปีที่ 5</asp:ListItem>
                            <asp:ListItem Value="P6">ชั้นปีที่ 6</asp:ListItem>
                            <asp:ListItem Value="P7">ชั้นปีที่ 7</asp:ListItem>
                            <asp:ListItem Value="P8">ชั้นปีที่ 8</asp:ListItem>
                        </asp:DropDownList>

                         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="DocID" DataSourceID="SqlDataSourceInfor" CssClass="gridview">
                        <Columns>
                            <asp:BoundField DataField="DocID" HeaderText="DocID" ReadOnly="True" InsertVisible="False" SortExpression="DocID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Doc_Name" HeaderText="ชื่อโปรเจค" SortExpression="Doc_Name"></asp:BoundField>
                            <asp:BoundField DataField="Doc_NameEN" HeaderText="ชื่อโปรเจค(ภาษาอังกฤษ)" SortExpression="Doc_NameEN"></asp:BoundField>
                            <asp:BoundField DataField="Student_Name" HeaderText="ชื่อ-นามสกุล" SortExpression="Student_Name"></asp:BoundField>
                            <asp:BoundField DataField="Student_Name2" HeaderText="ชื่อ-นามสกุล" SortExpression="Student_Name2"></asp:BoundField>
                            <asp:BoundField DataField="Name_teacherPJ" HeaderText="ชื่ออาจารย์ที่ปรึกษา" SortExpression="Name_teacherPJ"></asp:BoundField>
                             <asp:TemplateField HeaderText="การกระทำ">
                                 <ItemTemplate>
                                     <button type="button" class="btn btn-primary" onclick="redirectToPage('<%# SafeOut.JsAttr(Eval("DocID")) %>');">ดูข้อมูลทั้งหมด</button>
                               </ItemTemplate>
                          </asp:TemplateField>
                        </Columns>
                             <EmptyDataTemplate>
                                <div style="text-align: center;">
                                    ไม่มีข้อมูลที่จะแสดง
                                </div>
                            </EmptyDataTemplate>
                    </asp:GridView>
                    <script>
                        function redirectToPage(docID) {
                            // Redirect to the desired page with DocID as a query parameter
                            window.location.href = 'Admin_infor_See.aspx?docID=' + docID;
                        }
                    </script>
                        <asp:SqlDataSource runat="server" ID="SqlDataSourceInfor" ProviderName="Npgsql" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT DocID, Doc_Name, Doc_NameEN, Student_Name, Student_Name2, Name_teacherPJ FROM MAS_DOC">
                    </asp:SqlDataSource>

                </div>
            </div>
        </div>
    </div>
        </div>
    </div>

</asp:Content>

