<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="pushfriend.aspx.vb" Inherits="production_version_EditUploadfile" %>

<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Import Namespace="System.Collections.Generic" %>

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
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<div class="container">
    <div class="row">
        <div class="col-md-12">
            <div class="card col-md-12">
                <div class="card-body">
                    <asp:DataList ID="DataList1" runat="server" DataKeyField="DocID" DataSourceID="SqlDataSource2">
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
                    <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT MAS_DOC.DocID, Permission_TB.Email FROM MAS_DOC INNER JOIN Permission_TB ON MAS_DOC.DocID = Permission_TB.DocID WHERE (Permission_TB.Email = @Email)">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="Email" Name="Email"></asp:SessionParameter>
                        </SelectParameters>
                    </asp:SqlDataSource>                                                                     
                            <hr />
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
                                        <asp:Button ID="Button2" runat="server" Text="บันทึก" CssClass="btn btn-primary" />
                                    </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                    </div>                     
                </div>
            </div>
        </div>
    </div>
</div>
    <br />
    <div class="container">
    <div class="row">
        <div class="col-md-12">
            <div class="card-header white">
                <h4>ข้อมูลคู่โปรเจค</h4>
            </div>
            <div class="card col-md-12">
                <div class="card-body">

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Permission_ID" DataSourceID="SqlDataSource1" CssClass="gridview">
                        <Columns>
                            <asp:BoundField DataField="Permission_ID" HeaderText="Permission_ID" ReadOnly="True" InsertVisible="False" SortExpression="Permission_ID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="ชื่อ-นามสกุล" SortExpression="Name"></asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="อีเมลล์" SortExpression="Email"></asp:BoundField>
                            <asp:BoundField DataField="Mobile" HeaderText="เบอร์โทรศัพท์" SortExpression="Mobile"></asp:BoundField>
                            <asp:BoundField DataField="Student_ID" HeaderText="รหัสนิสิต" SortExpression="Student_ID"></asp:BoundField>
                            <asp:TemplateField HeaderText="การกระทำ">
                                <ItemTemplate>
                                    <asp:Button ID="DeleteButton" CssClass="btn btn-danger" runat="server" Text="ลบ" CommandName="Delete" CommandArgument='<%# Eval("Permission_ID") %>' OnClick="DeleteButton_Click" Width="100%" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Permission_TB.Permission_ID, Permission_TB.Name, Permission_TB.Email, Permission_TB.Mobile, Permission_TB.Student_ID, MAS_DOC.DocID, MAS_DOC.Student_ID2, MAS_DOC.Student_Name2, MAS_DOC.Email2, MAS_DOC.Mobile2 FROM MAS_DOC INNER JOIN Permission_TB ON MAS_DOC.DocID = Permission_TB.DocID WHERE (Permission_TB.DocID = @DocID)">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="DocID" QueryStringField="docID" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>
    </div>
</div>







</asp:Content>

