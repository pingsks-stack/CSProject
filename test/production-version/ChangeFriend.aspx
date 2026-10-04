<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="ChangeFriend.aspx.vb" Inherits="production_version_EditUploadfile" %>

<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
 
</asp:Content>

