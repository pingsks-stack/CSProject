<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageTeacher.master" AutoEventWireup="false" CodeFile="Confirms_Change.aspx.vb" Inherits="production_version_Doc_Student" %>
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
                    <button type="button" onclick="redirectToChangeFriend()" class="btn btn-secondary">กลับไปยังหน้าเอกสารโปรเจคของนักเรียน</button>

                    <script>
                        function redirectToChangeFriend() {
                            var docID = "<%= docID %>"; // รับค่าจากตัวแปรที่ตั้งค่าใน Code-Behind
                            var url = 'Doc_Student.aspx?DocID=' + docID;
                            window.location.href = url;
                        }
                    </script>
                    <hr />
                    <h5 class="card-title">ข้อมูลที่รอการยืนยันการเปลี่ยนคู่โปรเจค/สถานะ</h5>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" DataKeyNames="Status_ID,DocID,Permission_ID">
                        <Columns>
                            <asp:BoundField DataField="Status_ID" HeaderText="Status_ID" ReadOnly="True" InsertVisible="False" SortExpression="Status_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Permission_ID" HeaderText="Permission_ID" SortExpression="Permission_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="ชื่อ-นามสกุล(คนใหม่)" SortExpression="Name"></asp:BoundField>
                            <asp:BoundField DataField="Last_Post_Name" HeaderText="ชื่อ-นามสกุล(คนเก่า)" SortExpression="Last_Post_Name"></asp:BoundField>                                                    
                            <asp:BoundField DataField="Urole" HeaderText="Urole" SortExpression="Urole" Visible="False"></asp:BoundField>
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
                             <asp:BoundField DataField="Status" HeaderText="สถานะ" SortExpression="Status"></asp:BoundField>
                            <asp:BoundField DataField="Action" HeaderText="การกระทำ" SortExpression="Action"></asp:BoundField>
                            <asp:BoundField DataField="Post_Name" HeaderText="โดย" SortExpression="Post_Name"></asp:BoundField>
                            <asp:BoundField DataField="Date_Send_NS" HeaderText="วันเวลา" SortExpression="Date_Send_NS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                        </Columns>
                        <EmptyDataTemplate>
                                    <div style="text-align: center;">
                                        ไม่มีการร้องขอการเปลี่ยนแปลงคู่โปรเจค
                                    </div>
                                </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Status_ID, DocID, Permission_ID, Name, Email, Mobile, Student_ID, Post_Name, Urole, Status, Last_Post_Name, Date_Send_NS, Action FROM Status_Change_TB WHERE (DocID = @DocID) AND (Permission_ID = @Permission_ID) AND (Status = N'รอการยืนยัน') AND (Action = N'ขอเปลี่ยนคู่โปรเจค')">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="DocID" QueryStringField="DocID" Type="String" />
                            <asp:QueryStringParameter Name="Permission_ID" QueryStringField="Permission_ID" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </div>
            </div>
        </div>
    
    </div>
</div>
   <%--<br />
 <div class="container">
    <div class="row">
        <div class="col-md-12">
            <div class="card col-md-12">
                <div class="card-body">                            
                   
                    <h5 class="card-title">ประวัติการยืนยันการเปลี่ยนคู่โปรเจค</h5>
                    <hr />
                    <asp:GridView ID="GridView2" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                        <Columns>
                            <asp:BoundField DataField="Status_ID" HeaderText="Status_ID" ReadOnly="True" InsertVisible="False" SortExpression="Status_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Permission_ID" HeaderText="Permission_ID" SortExpression="Permission_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="ชื่อ-นามสกุล(คนใหม่)" SortExpression="Name"></asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="อีเมลล์" SortExpression="Email" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Mobile" HeaderText="เบอร์โทรศัพท์" SortExpression="Mobile" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Last_Post_Name" HeaderText="ชื่อ-นามสกุล(คนเก่า)" SortExpression="Last_Post_Name"></asp:BoundField>                           
                            <asp:BoundField DataField="Urole" HeaderText="Urole" SortExpression="Urole" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="สถานะ" SortExpression="Status"></asp:BoundField>
                            <asp:BoundField DataField="Action" HeaderText="การกระทำ" SortExpression="Action"></asp:BoundField>
                            <asp:BoundField DataField="Post_Name" HeaderText="โดย" SortExpression="Post_Name"></asp:BoundField>
                            <asp:BoundField DataField="Date_Confirm" HeaderText="วันเวลา" SortExpression="Date_Confirm" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div style="text-align: center;">
                                ไม่มีการประวัติการเปลี่ยนคู่โปรจค
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Status_ID, DocID, Permission_ID, Name, Email, Mobile, Student_ID, Post_Name, Urole, Status, Status_Change, Date_Confirm, Last_Post_Name, Action FROM Status_Change_TB WHERE (DocID = @DocID) AND (Permission_ID = @Permission_ID) AND (Status_Change1 = N'1') AND (Action = N'ขอเปลี่ยนคู่โปรเจค')">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="DocID" QueryStringField="DocID" Type="String" />
                            <asp:QueryStringParameter Name="Permission_ID" QueryStringField="Permission_ID" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    
    </div>
</div>--%>
    <hr />
    <hr />
    <div class="container">
    <div class="row">
        <div class="col-md-12">
            <div class="card col-md-12">
                <div class="card-body">                                     
                    <h5 class="card-title">ข้อมูลที่รอการยืนยันการลบคู่โปรเจค/สถานะ</h5>
                    <br />
                    <asp:GridView ID="GridView3" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" DataKeyNames="Status_ID,DocID,Permission_ID">
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
                            <asp:TemplateField HeaderText="การกระทำ">
                                <ItemTemplate>
                                    <asp:Button ID="ConfirmButton1" runat="server" Text="ยืนยัน" CssClass="btn btn-success" Width="100%" OnClick="ConfirmButton1_Click" CommandName="Confirm" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="การกระทำ">
                                <ItemTemplate>
                                    <asp:Button ID="RejectButton1" CssClass="btn btn-secondary" runat="server" Text="ปฏิเสธ" Width="100%" OnClick="RejectButton1_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="Status" HeaderText="สถานะ" SortExpression="Status"></asp:BoundField>
                            <asp:BoundField DataField="Action" HeaderText="การกระทำ" SortExpression="Action"></asp:BoundField>
                            <asp:BoundField DataField="Post_Name" HeaderText="โดย" SortExpression="Post_Name"></asp:BoundField>
                            <asp:BoundField DataField="Date_Send_NS" HeaderText="วันเวลา" SortExpression="Date_Send_NS" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                        </Columns>
                        <EmptyDataTemplate>
                                    <div style="text-align: center;">
                                        ไม่มีการร้องขอการลบคู่โปรเจค
                                    </div>
                                </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource3" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Status_ID, DocID, Permission_ID, Name, Email, Mobile, Student_ID, Post_Name, Urole, Status, Last_Post_Name, Date_Send_NS, Action, Last_Email, Last_Mobile, Last_StudentID FROM Status_Change_TB WHERE (DocID = @DocID) AND (Permission_ID = @Permission_ID) AND (Status = N'รอการยืนยัน') AND (Action = N'ขอลบคู่โปรเจค')">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="DocID" QueryStringField="DocID" Type="String" />
                            <asp:QueryStringParameter Name="Permission_ID" QueryStringField="Permission_ID" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </div>
            </div>
        </div>
    
    </div>
</div>
  <%-- <br />
 <div class="container">
    <div class="row">
        <div class="col-md-12">
            <div class="card col-md-12">
                <div class="card-body">                            
                   
                    <h5 class="card-title">ประวัติการยืนยันการลบคู่โปรเจค</h5>
                    <hr />
                    <asp:GridView ID="GridView4" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataSourceID="SqlDataSource4">
                        <Columns>
                            <asp:BoundField DataField="Status_ID" HeaderText="Status_ID" ReadOnly="True" InsertVisible="False" SortExpression="Status_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Permission_ID" HeaderText="Permission_ID" SortExpression="Permission_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="ชื่อ-นามสกุล(คนใหม่)" SortExpression="Name" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="อีเมลล์" SortExpression="Email" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Mobile" HeaderText="เบอร์โทรศัพท์" SortExpression="Mobile" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Last_Post_Name" HeaderText="ชื่อ-นามสกุล" SortExpression="Last_Post_Name"></asp:BoundField>
                            <asp:BoundField DataField="Last_Email" HeaderText="อีเมลล์" SortExpression="Last_Email"></asp:BoundField>
                            <asp:BoundField DataField="Last_Mobile" HeaderText="เบอร์โทรศัพท์" SortExpression="Last_Mobile"></asp:BoundField>
                            <asp:BoundField DataField="Last_StudentID" HeaderText="รหัสนิสิต" SortExpression="Last_StudentID"></asp:BoundField>                           
                            <asp:BoundField DataField="Urole" HeaderText="Urole" SortExpression="Urole" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="สถานะ" SortExpression="Status"></asp:BoundField>
                            <asp:BoundField DataField="Action" HeaderText="การกระทำ" SortExpression="Action"></asp:BoundField>
                            <asp:BoundField DataField="Post_Name" HeaderText="โดย" SortExpression="Post_Name"></asp:BoundField>
                            <asp:BoundField DataField="Date_Confirm" HeaderText="วันเวลา" SortExpression="Date_Confirm" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                        </Columns>
                        <EmptyDataTemplate>
                            <div style="text-align: center;">
                                ไม่มีการประวัติการลบคู่โปรเจค
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource4" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Status_ID, DocID, Permission_ID, Name, Email, Mobile, Student_ID, Post_Name, Urole, Status, Status_Change, Date_Confirm, Last_Post_Name, Action, Last_Email, Last_Mobile, Last_StudentID FROM Status_Change_TB WHERE (DocID = @DocID) AND (Permission_ID = @Permission_ID) AND (Status_Change1 = N'1') AND (Action = N'ขอลบคู่โปรเจค')">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="DocID" QueryStringField="DocID" Type="String" />
                            <asp:QueryStringParameter Name="Permission_ID" QueryStringField="Permission_ID" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    
    </div>
</div>--%>
</asp:Content>

