<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="Delete_InputAJ.aspx.vb" Inherits="production_version_EditUploadfile" %>

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
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
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
                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT MAS_DOC.DocID, Permission_TB.Email FROM MAS_DOC INNER JOIN Permission_TB ON MAS_DOC.DocID = Permission_TB.DocID WHERE (Permission_TB.Email = @Email)">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="Email" Name="Email"></asp:SessionParameter>
                        </SelectParameters>
                    </asp:SqlDataSource>                  
                    <hr />
                    <h5 class="card-title">เพิ่มข้อมูลอาจารย์</h5>
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel8" runat="server" Width="100%" ClientInstanceName="cb_load11" OnCallback="cb_load11_Callback">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <div class="form-row">
                                    <div class="col-md-6 mb-3">
                                        <label for="firstname" class="form-label">อีเมลล์(อาจารย์)</label>
                                        <dx:BootstrapTextBox ID="EmailAJ" runat="server" ClientInstanceName="EmailAJ" NullText="ใส่อีเมลล์ของอาจารย์">
                                            <ClientSideEvents KeyUp="function(s, e) {ASPxCallback3.PerformCallback();}"></ClientSideEvents>
                                        </dx:BootstrapTextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณาระอีเมลล์" ControlToValidate="EmailAJ" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <dx:ASPxCallback ID="ASPxCallback3" runat="server" ClientInstanceName="ASPxCallback3">
                                        <ClientSideEvents CallbackComplete="function(s, e) {NameAJ.SetValue(e.result); ASPxCallback4.PerformCallback();}" />
                                    </dx:ASPxCallback>
                                    <div class="col-md-6 mb-3">
                                        <label for="firstname" class="form-label">ชื่อ-นามสกุล</label>
                                        <dx:BootstrapTextBox ID="NameAJ" runat="server" ClientInstanceName="NameAJ">
                                            <ClientSideEvents KeyUp="function(s, e) {ASPxCallback1.PerformCallback();}"></ClientSideEvents>
                                        </dx:BootstrapTextBox>
                                    </div>
                                    <dx:ASPxCallback ID="ASPxCallback4" runat="server" ClientInstanceName="ASPxCallback4">
                                        <ClientSideEvents CallbackComplete="function(s, e) {PhonenumberAJ.SetValue(e.result);}" />
                                    </dx:ASPxCallback>
                                    <div class="col-md-6 mb-3">
                                        <label for="firstname" class="form-label">เบอร์โทรอาจารย์</label>
                                        <dx:BootstrapTextBox ID="PhonenumberAJ" runat="server" ClientInstanceName="PhonenumberAJ">
                                        </dx:BootstrapTextBox>
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label for="firstname" class="form-label">สถานะอาจารย์์</label>
                                        <dx:BootstrapComboBox ID="BootstrapComboBox1" runat="server" AutoPostBack="False" SelectedIndex="0">
                                            <Items>
                                                <dx:BootstrapListEditItem Text="ที่ปรึกษา" Value="1">
                                                </dx:BootstrapListEditItem>
                                                <dx:BootstrapListEditItem Text="กรรมการ" Value="2">
                                                </dx:BootstrapListEditItem>
                                            </Items>
                                        </dx:BootstrapComboBox>
                                    </div>
                                </div>
                                <asp:Button ID="Button8" runat="server" class="btn btn-primary" Text="เพิ่มข้อมูล" OnClick="Button8_Click" />
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
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
                    <h4>ข้อมูลอาจารย์</h4>
                </div>
                <div class="card col-md-12">
                    <div class="card-body">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="Permission_ID" DataSourceID="SqlDataSourceInforAJ" CssClass="gridview">
                            <Columns>
                                <asp:BoundField DataField="Permission_ID" HeaderText="Permission_ID" ReadOnly="True" InsertVisible="False" SortExpression="Permission_ID" Visible="False"></asp:BoundField>
                                <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                                <asp:BoundField DataField="Name" HeaderText="ชื่อ-นามสกุล" SortExpression="Name"></asp:BoundField>
                                <asp:BoundField DataField="Email" HeaderText="อีเมลล์" SortExpression="Email"></asp:BoundField>
                                <asp:BoundField DataField="Mobile" HeaderText="เบอร์โทรศัพท์" SortExpression="Mobile"></asp:BoundField>
                                <asp:BoundField DataField="Urole" HeaderText="Urole" SortExpression="Urole" Visible="False"></asp:BoundField>
                                <asp:BoundField DataField="Urole_TC" HeaderText="สถานะ" SortExpression="Urole_TC"></asp:BoundField>
                                <asp:TemplateField HeaderText="การกระทำ">
                                    <ItemTemplate>
                                        <asp:Button ID="DeleteButton" CssClass="btn btn-danger" runat="server" Text="ลบ" CommandName="Delete" CommandArgument='<%# Eval("Permission_ID") %>' OnClick="DeleteAJButton_Click" Width="100%" OnClientClick="return confirmDelete();" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <script>
                            function confirmDelete() {
                                return confirm("คุณต้องการลบข้อมูลนี้หรือไม่?");
                            }
                        </script>
                        <asp:SqlDataSource runat="server" ID="SqlDataSourceInforAJ" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Permission_ID, DocID, Name, Email, Mobile, Urole, Urole_TC FROM Permission_TB WHERE (Urole = N'2') AND (DocID = @DocID) ORDER BY Urole_TC DESC">
                            <SelectParameters>
                                <asp:QueryStringParameter QueryStringField="docID" Name="DocID"></asp:QueryStringParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>

