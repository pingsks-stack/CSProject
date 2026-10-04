<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="Detect_code.aspx.vb" Inherits="production_version_Detect_code" %>
<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/10.3.2/styles/default.min.css">
    <script src="../../../Scripts/WebForms/MSAjax/highlight.min.js"></script>
     <script>
         function Detect_Code() {
             var code = document.getElementById("Text_Code_I").value; /*ดึงชื่อ id memo ในเว็ปมา*/
             var result = hljs.highlightAuto(code);
             var highlightedCode = result.value;
             var language = result.language;
             console.log(language);
             txtname.SetValue(language); /*แสดง code ที่ detect แทนใน console*/
         }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid animatedParent animateOnce my-3">
        <div class="container-fluid my-3">
        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body">

                        <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">Detect Code Funtions</h5>                            
                            <br />                           
                            <asp:Label ID="name_code" runat="server" Text="Input your Source Code."></asp:Label>
                            <dx:BootstrapMemo ID="Text_Code" runat="server" ClientIDMode="Static" ClientInstanceName="Text_Code" Width="100%" Rows="5">
                                <ClientSideEvents KeyUp="function(s, e) {Detect_Code();}"></ClientSideEvents>
                            </dx:BootstrapMemo>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="กรุณาระบุ Source Code" ControlToValidate="Text_Code" CssClass="text-danger"></asp:RequiredFieldValidator>
                            <br />
                                      
                            <dx:ASPxCallback ID="Detect_Callback" runat="server" ClientInstanceName="Detect_Callback"></dx:ASPxCallback>
                           
                            
                            <dx:BootstrapTextBox ID="txtname" runat="server" ClientInstanceName="txtname" NullText="Code Language">
                            </dx:BootstrapTextBox>
                            <br />
                           
                        </div>
                    </div>
                </div>
                
            </div>
                </div>
            </div>
                </div>
                    </div>
                </div>
            </div>
</asp:Content>

