<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="History_ChangF.aspx.vb" Inherits="production_version_EditUploadfile" %>

<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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
    .hidden-textbox {
  display: none;
}
.card.no-b h4 {
        text-align: center;
    }
 .project-details {
        font-size: 17px;
        font-weight: bold;
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
                   
                    <h5 class="card-title">ประวัติ/ข้อมูลที่รอการยืนยันการเปลี่ยนคู่โปรเจค/สถานะ</h5>
                    <hr />
                    <asp:GridView ID="GridView3" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataSourceID="SqlDataSource4">
                        <Columns>
                            <asp:BoundField DataField="Status_ID" HeaderText="Status_ID" ReadOnly="True" InsertVisible="False" SortExpression="Status_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Permission_ID" HeaderText="Permission_ID" SortExpression="Permission_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="ชื่อ-นามสกุล(คนใหม่)" SortExpression="Name"></asp:BoundField>
                            <asp:BoundField DataField="Last_Post_Name" HeaderText="ชื่อ-นามสกุล(คนเก่า)" SortExpression="Last_Post_Name"></asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="อีเมลล์" SortExpression="Email" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Mobile" HeaderText="เบอร์โทรศัพท์" SortExpression="Mobile" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Student_ID" HeaderText="รหัสนิสิต" SortExpression="Student_ID" Visible="False"></asp:BoundField>                           
                            <asp:BoundField DataField="Urole" HeaderText="Urole" SortExpression="Urole" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="สถานะ" SortExpression="Status" ItemStyle-ForeColor="#00CC00"></asp:BoundField>
                            <asp:BoundField DataField="Action" HeaderText="การกระทำ" SortExpression="Action"></asp:BoundField>
                            <asp:BoundField DataField="Post_Name" HeaderText="โดย" SortExpression="Post_Name"></asp:BoundField>
                            <asp:BoundField DataField="Date_Confirm" HeaderText="วันเวลาที่ยืนยัน/ปฏิเสธ" SortExpression="Date_Confirm" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div style="text-align: center;">
                                ไม่มีข้อมูลการเปลี่ยนแปลงคู่โปรเจค
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource4" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Status_ID, DocID, Permission_ID, Name, Email, Mobile, Student_ID, Post_Name, Urole, Status, Date_Confirm, Last_Post_Name, Action FROM Status_Change_TB WHERE (DocID = @DocID) AND (Action = N'ขอเปลี่ยนคู่โปรเจค')">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="DocID" QueryStringField="DocID" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    
    </div>
</div>
    <hr />
    <hr />
 <div class="container">
    <div class="row">
        <div class="col-md-12">
            <div class="card col-md-12">
                <div class="card-body">                            
                   
                    <h5 class="card-title">ประวัติ/ข้อมูลที่รอการยืนยันการลบคู่โปรเจค/สถานะ</h5>
                    <hr />
                    <asp:GridView ID="GridView1" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
                        <Columns>
                            <asp:BoundField DataField="Status_ID" HeaderText="Status_ID" ReadOnly="True" InsertVisible="False" SortExpression="Status_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Permission_ID" HeaderText="Permission_ID" SortExpression="Permission_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="ชื่อ-นามสกุล(คนใหม่)" SortExpression="Name" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Last_Post_Name" HeaderText="ชื่อ-นามสกุล" SortExpression="Last_Post_Name"></asp:BoundField>
                            <asp:BoundField DataField="Last_Email" HeaderText="อีเมลล์" SortExpression="Last_Email"></asp:BoundField>
                            <asp:BoundField DataField="Last_Mobile" HeaderText="เบอร์โทรศัพท์" SortExpression="Last_Mobile"></asp:BoundField>
                            <asp:BoundField DataField="Last_StudentID" HeaderText="รหัสนิสิต" SortExpression="Last_StudentID"></asp:BoundField>                           
                            <asp:BoundField DataField="Urole" HeaderText="Urole" SortExpression="Urole" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="สถานะ" SortExpression="Status" ItemStyle-ForeColor="#00CC00"></asp:BoundField>
                            <asp:BoundField DataField="Action" HeaderText="การกระทำ" SortExpression="Action"></asp:BoundField>
                            <asp:BoundField DataField="Post_Name" HeaderText="โดย" SortExpression="Post_Name"></asp:BoundField>
                            <asp:BoundField DataField="Date_Confirm" HeaderText="วันเวลาที่ยืนยัน/ปฏิเสธ" SortExpression="Date_Confirm" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                        </Columns>
                        <EmptyDataTemplate>
                                    <div style="text-align: center;">
                                        ไม่มีข้อมูลการลบคู่โปรเจค
                                    </div>
                                </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Status_ID, DocID, Permission_ID, Name, Email, Mobile, Student_ID, Post_Name, Urole, Status, Date_Confirm, Last_Post_Name, Action, Last_Email, Last_Mobile, Last_StudentID FROM Status_Change_TB WHERE (DocID = @DocID) AND (Action = N'ขอลบคู่โปรเจค')">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="DocID" QueryStringField="DocID" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    
    </div>
</div>
</asp:Content>

