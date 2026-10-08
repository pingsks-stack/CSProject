<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="Change_Friend.aspx.vb" Inherits="production_version_EditUploadfile" %>

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
                    <asp:DataList ID="DataList1" runat="server" DataKeyField="DocID" DataSourceID="SqlDataSource1">
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <td>
                                        <button type="button" class="btn btn-secondary" onclick="redirectToPage('<%# SafeOut.JsAttr(Eval("DocID")) %>');">กลับไปยังหน้าเอกสารของฉัน</button>
                                    </td>                                    
                                </tr>
                            </tbody>
                            </table>                                                  
                        </ItemTemplate>
                    </asp:DataList>
                    <script>
                        function redirectToPage(docID) {
                            // Redirect to the desired page with DocID as a query parameter
                            window.location.href = 'MAS_Doc.aspx?docID=' + docID;
                        }
                    </script>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ProviderName="Npgsql" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT MAS_DOC.DocID, Permission_TB.Email FROM MAS_DOC INNER JOIN Permission_TB ON MAS_DOC.DocID = Permission_TB.DocID WHERE (Permission_TB.Email = @Email)">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="Email" Name="Email"></asp:SessionParameter>
                        </SelectParameters>
                    </asp:SqlDataSource>                  
                    <hr />
                    <h5 class="card-title">เปลี่ยนคู่โปรเจค</h5>
                    <div class="form-row">
                        <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="100%" ClientInstanceName="cb_load4" OnCallback="cb_load4_Callback">
                            <PanelCollection>
                                <dx:PanelContent runat="server">
                                    <div class="form-row">
                                        <div class="col-md-6 mb-3">
                                            <label for="firstname" class="form-label">อีเมลล์(คนที่2)</label>
                                            <dx:BootstrapTextBox ID="BootstrapTextBoxQR" runat="server" ClientInstanceName="BootstrapTextBoxQR" NullText="ใส่อีเมลล์">
                                                <ClientSideEvents KeyUp="function(s, e) {cbload.PerformCallback();}"></ClientSideEvents>
                                            </dx:BootstrapTextBox>

                                        </div>
                                        <dx:ASPxCallback ID="cbload" runat="server" ClientInstanceName="cbload">
                                            <ClientSideEvents CallbackComplete="function(s, e) {txtname.SetValue(e.result); ASPxCallback1.PerformCallback();}" />
                                        </dx:ASPxCallback>
                                        <div class="col-md-6 mb-3">
                                            <label for="firstname" class="form-label">ชื่อ-นามสกุล(คนที่2)</label>
                                            <dx:BootstrapTextBox ID="txtname" runat="server" ClientInstanceName="txtname">
                                                <ClientSideEvents KeyUp="function(s, e) {ASPxCallback1.PerformCallback();}"></ClientSideEvents>
                                            </dx:BootstrapTextBox>
                                        </div>

                                        <dx:ASPxCallback ID="ASPxCallback1" runat="server" ClientInstanceName="ASPxCallback1">
                                            <ClientSideEvents CallbackComplete="function(s, e) {txturole.SetValue(e.result); ASPxCallback2.PerformCallback();}" />
                                        </dx:ASPxCallback>
                                        <div class="col-md-6 mb-3">
                                            <label for="firstname" class="form-label">รหัสนิสิต(คนที่2)</label>
                                            <dx:BootstrapTextBox ID="txturole" runat="server" ClientInstanceName="txturole">
                                                <ClientSideEvents KeyUp="function(s, e) {ASPxCallback2.PerformCallback();}"></ClientSideEvents>
                                            </dx:BootstrapTextBox>
                                        </div>

                                        <dx:ASPxCallback ID="ASPxCallback2" runat="server" ClientInstanceName="ASPxCallback2">
                                            <ClientSideEvents CallbackComplete="function(s, e) {txtMobile.SetValue(e.result);}" />
                                        </dx:ASPxCallback>
                                        <div class="col-md-6 mb-3">
                                            <label for="firstname" class="form-label">เบอร์โทร(คนที่2)</label>
                                            <dx:BootstrapTextBox ID="txtMobile" runat="server" ClientInstanceName="txtMobile">
                                            </dx:BootstrapTextBox>
                                        </div>
                                    </div>
                                    <asp:Button ID="Button2" runat="server" Text="เพิ่มข้อมูล" CssClass="btn btn-primary" />
                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>
                    </div>
                </div>
            </div>
        </div>
    
    </div>
</div>
  <%--   <br />
 <div class="container">
    <div class="row">
        <div class="col-md-12">
            <div class="card col-md-12">
                <div class="card-body">                            
                   
                    <h5 class="card-title">ข้อมูลที่รอการยืนยันการเปลี่ยนคู่โปรเจค/สถานะ</h5>
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
                            <asp:BoundField DataField="Status" HeaderText="สถานะ" SortExpression="Status"></asp:BoundField>
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
                    <asp:SqlDataSource runat="server" ID="SqlDataSource4" ProviderName="Npgsql" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Status_ID, DocID, Permission_ID, Name, Email, Mobile, Student_ID, Post_Name, Urole, Status, Date_Confirm, Last_Post_Name, Action FROM Status_Change_TB WHERE (DocID = @DocID) AND (Permission_ID = @Permission_ID) AND (Action = 'ขอเปลี่ยนคู่โปรเจค')">
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
                   
                    <h5 class="card-title">ลบคู่โปรเจค</h5>
                    <hr />
                    <asp:GridView ID="GridView2" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataKeyNames="Permission_ID" DataSourceID="SqlDataSource3">
                        <Columns>
                            <asp:BoundField DataField="Permission_ID" HeaderText="Permission_ID" ReadOnly="True" InsertVisible="False" SortExpression="Permission_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="ชื่อ-นามสกุล" SortExpression="Name"></asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="อีเมลล์" SortExpression="Email"></asp:BoundField>
                            <asp:BoundField DataField="Mobile" HeaderText="เบอร์โทรศัพท์" SortExpression="Mobile"></asp:BoundField>
                            <asp:BoundField DataField="Student_ID" HeaderText="รหัสนิสิต" SortExpression="Student_ID"></asp:BoundField>
                            <asp:TemplateField HeaderText="การกระทำ">
                                <ItemTemplate>
                                    <asp:Button ID="DeleteButton" CssClass="btn btn-danger" runat="server" Text="ลบ" CommandName="Delete" CommandArgument='<%# Eval("Permission_ID") %>' OnClick="DeleteAJButton_Click" Width="100%" OnClientClick="return confirmDelete();" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                                    <div style="text-align: center;">
                                        ไม่มีข้อมูลคู่โปรเจค
                                    </div>
                                </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource3" ProviderName="Npgsql" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Permission_ID, Name, Email, Mobile, Student_ID FROM Permission_TB WHERE (Permission_ID = @Permission_ID)">
                        <SelectParameters>
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
                   
                    <h5 class="card-title">ข้อมูลที่รอการยืนยันการลบคู่โปรเจค/สถานะ</h5>
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
                            <asp:BoundField DataField="Status" HeaderText="สถานะ" SortExpression="Status"></asp:BoundField>
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
                    <asp:SqlDataSource runat="server" ID="SqlDataSource2" ProviderName="Npgsql" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Status_ID, DocID, Permission_ID, Name, Email, Mobile, Student_ID, Post_Name, Urole, Status, Date_Confirm, Last_Post_Name, Action, Last_Email, Last_Mobile, Last_StudentID FROM Status_Change_TB WHERE (DocID = @DocID) AND (Permission_ID = @Permission_ID) AND (Action = 'ขอลบคู่โปรเจค')">
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

