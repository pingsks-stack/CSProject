<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="EditUploadfile.aspx.vb" Inherits="production_version_EditUploadfile" %>

<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

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
                    <h5 class="card-title">อัพโหลดไฟล์</h5>
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control-file"  />
                    <dx:aspxlabel runat="server" text="กรณีแทนที่ไฟล์ชื่อไฟล์ต้องเหมือนกับชื่อที่จะแทนที่ !"></dx:aspxlabel>
                    <hr />
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="บันทึก" />
                </div>
            </div>
        </div>
    
    </div>
</div>





</asp:Content>

