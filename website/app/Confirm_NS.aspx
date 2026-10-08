<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageTeacher.master" AutoEventWireup="false" CodeFile="Confirm_NS.aspx.vb" Inherits="production_version_Doc_Student" %>
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
         text-align: center;
    }
    
 .gridview th, .gridview td {
        /* รูปแบบเซลล์ตาราง */
        border: none;
        padding: 8px;
        height: 55px;
        color: black; /* เพิ่มสีฟอนต์เป็นสีดำ */
        text-align: center;
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
    <div class="row">
        <div class="col-md-12">
            <div class="card col-md-12">
                <div class="card-body">                 
                    <h5 class="card-title">ข้อมูลที่รอการยืนยันการเป็นอาจารย์ที่ปรึกษา</h5>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" DataKeyNames="SubAddAj_ID,DocID">
                        <Columns>
                            <asp:BoundField DataField="SubAddAj_ID" HeaderText="SubAddAj_ID" ReadOnly="True" InsertVisible="False" SortExpression="SubAddAj_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Doc_Name" HeaderText="ชื่อโปรเจค" SortExpression="Doc_Name"></asp:BoundField>
                            <asp:BoundField DataField="Doc_NameEN" HeaderText="ชื่อโปรเจคอังกฤษ" SortExpression="Doc_NameEN"></asp:BoundField>
                            <asp:BoundField DataField="Mobile" HeaderText="อีเมลล์" SortExpression="Mobile" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Urole" HeaderText="เบอร์โทรศัพท์" SortExpression="Urole" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Urole_TC" HeaderText="สถานะอาจารย์" SortExpression="Urole_TC" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="สถานะอาจารย์" SortExpression="Status" Visible="False"></asp:BoundField>                             
                            <asp:BoundField DataField="Status" HeaderText="สถานะ" SortExpression="Status"></asp:BoundField>                            
                            <asp:TemplateField HeaderText="การกระทำ">
                                <ItemTemplate>
                                    <asp:Button ID="ConfirmButton" runat="server" Text="ยืนยัน" CssClass="btn btn-success" Width="100%" OnClick="ConfirmButton_Click" CommandName="Confirm" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="การกระทำ">
                                <ItemTemplate>
                                    <asp:Button ID="RejectButton" CssClass="btn btn-secondary" runat="server" Text="ปฏิเสธ" Width="100%" OnClick="RejectButton_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Post_Name" HeaderText="โดย" SortExpression="Post_Name"></asp:BoundField>
                            <asp:BoundField DataField="Date_Send_NS" HeaderText="วันเวลา" SortExpression="Date_Send_NS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                        </Columns>
                        <EmptyDataTemplate>
                                    <div style="text-align: center;">
                                        ไม่มีข้อมูลที่รอการยืนยัน
                                    </div>
                                </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource2" ProviderName="Npgsql" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT SubAdd_AJ_TB.SubAddAj_ID, SubAdd_AJ_TB.DocID, SubAdd_AJ_TB.Name, SubAdd_AJ_TB.Email, SubAdd_AJ_TB.Mobile, SubAdd_AJ_TB.Urole, SubAdd_AJ_TB.Urole_TC, SubAdd_AJ_TB.Status, SubAdd_AJ_TB.Post_Name, SubAdd_AJ_TB.Status_Add, MAS_DOC.Doc_Name, MAS_DOC.Doc_NameEN, SubAdd_AJ_TB.Date_Send_NS FROM SubAdd_AJ_TB INNER JOIN MAS_DOC ON SubAdd_AJ_TB.DocID = MAS_DOC.DocID WHERE (SubAdd_AJ_TB.Email = @Email) AND (SubAdd_AJ_TB.Status_Add = '0')">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="Email" Name="Email"></asp:SessionParameter>

                        </SelectParameters>
                    </asp:SqlDataSource>
                    <hr />
                    <h5 class="card-title">ประวัติการยืนยัน/ปฏิเสธ</h5>
                    <br />
                    <asp:GridView ID="GridView2" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="SubAddAj_ID">
                        <Columns>
                            <asp:BoundField DataField="SubAddAj_ID" HeaderText="SubAddAj_ID" ReadOnly="True" InsertVisible="False" SortExpression="SubAddAj_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Doc_Name" HeaderText="ชื่อโปรเจค" SortExpression="Doc_Name"></asp:BoundField>
                            <asp:BoundField DataField="Doc_NameEN" HeaderText="ชื่อโปรเจคอังกฤษ" SortExpression="Doc_NameEN"></asp:BoundField>
                            <asp:BoundField DataField="Mobile" HeaderText="อีเมลล์" SortExpression="Mobile" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Urole" HeaderText="เบอร์โทรศัพท์" SortExpression="Urole" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Urole_TC" HeaderText="สถานะอาจารย์" SortExpression="Urole_TC" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="สถานะอาจารย์" SortExpression="Status" Visible="False"></asp:BoundField>                             
                            <asp:BoundField DataField="Status" HeaderText="สถานะ" SortExpression="Status" ItemStyle-ForeColor="#00CC00"></asp:BoundField>                           
                            <asp:BoundField DataField="Post_Name" HeaderText="โดย" SortExpression="Post_Name"></asp:BoundField>
                            <asp:BoundField DataField="Date_Confirm" HeaderText="วันเวลา" SortExpression="Date_Confirm" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                        </Columns>
                        <EmptyDataTemplate>
                                    <div style="text-align: center;">
                                        ไม่มีข้อมูลประวัติการยืนยัน/ปฏิเสธ
                                    </div>
                                </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ProviderName="Npgsql" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT SubAdd_AJ_TB.SubAddAj_ID, SubAdd_AJ_TB.DocID, SubAdd_AJ_TB.Name, SubAdd_AJ_TB.Email, SubAdd_AJ_TB.Mobile, SubAdd_AJ_TB.Urole, SubAdd_AJ_TB.Urole_TC, SubAdd_AJ_TB.Status, SubAdd_AJ_TB.Post_Name, SubAdd_AJ_TB.Status_Add, MAS_DOC.Doc_Name, MAS_DOC.Doc_NameEN, SubAdd_AJ_TB.Date_Confirm FROM SubAdd_AJ_TB INNER JOIN MAS_DOC ON SubAdd_AJ_TB.DocID = MAS_DOC.DocID WHERE (SubAdd_AJ_TB.Email = @Email) AND (SubAdd_AJ_TB.Status_Add = '1')">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="Email" Name="Email"></asp:SessionParameter>

                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    
    </div>
</div>
   
</asp:Content>

