<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageAdmin.master" AutoEventWireup="false" CodeFile="infor_user.aspx.vb" Inherits="production_version_infor_user" %>
<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid animatedParent animateOnce my-3">
        <div class="container-fluid my-3">
        <div class="d-flex row">
            <div class="col-md-12">
                <div class="card text-center">
                    <div class="card-header">
                        <h1>ข้อมูลผู้ใช้</h1>
                    </div>
                    <div class="card-body">
                        <dx:BootstrapGridView ID="Infor_user" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="UserID">
                            <SettingsDataSecurity AllowEdit="True" AllowInsert="True" AllowDelete="True"></SettingsDataSecurity>
                            <Columns>
                                <dx:BootstrapGridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="False" ShowDeleteButton="True"></dx:BootstrapGridViewCommandColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="UserID" ReadOnly="True" VisibleIndex="1" Visible="False">
                                    <SettingsEditForm Visible="False"></SettingsEditForm>
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Username" VisibleIndex="2" Caption="ยูเซอร์เนม">
                                    <SettingsEditForm Visible="False"></SettingsEditForm>
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Email" VisibleIndex="4" Caption="อีเมลล์">
                                    <SettingsEditForm Visible="False"></SettingsEditForm>
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Name" VisibleIndex="5" Caption="ชื่อ-นามสกุล">
                                    <SettingsEditForm Visible="False"></SettingsEditForm>
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Urole" VisibleIndex="6" Caption="สถานะ"></dx:BootstrapGridViewTextColumn>
                            </Columns>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                        </dx:BootstrapGridView>
                        <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT UserID, Username, Email, Name, Mobile, Student_ID, Urole FROM [Signin_Signup]" DeleteCommand="DELETE FROM [Signin_Signup] WHERE [UserID] = @UserID" InsertCommand="INSERT INTO [Signin_Signup] ([Username], [Password], [Email], [Name], [Urole]) VALUES (@Username, @Password, @Email, @Name, @Urole)" UpdateCommand="UPDATE [Signin_Signup] SET [Username] = @Username, [Email] = @Email, [Name] = @Name, [Urole] = @Urole WHERE [UserID] = @UserID">
                            <DeleteParameters>
                                <asp:Parameter Name="UserID" Type="Int32"></asp:Parameter>
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="Username" Type="String"></asp:Parameter>
                                <asp:Parameter Name="Password" Type="String"></asp:Parameter>
                                <asp:Parameter Name="Email" Type="String"></asp:Parameter>
                                <asp:Parameter Name="Name" Type="String"></asp:Parameter>
                                <asp:Parameter Name="Urole" Type="String"></asp:Parameter>
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="Username" Type="String"></asp:Parameter>
                                <asp:Parameter Name="Email" Type="String"></asp:Parameter>
                                <asp:Parameter Name="Name" Type="String"></asp:Parameter>
                                <asp:Parameter Name="Urole" Type="String"></asp:Parameter>
                                <asp:Parameter Name="UserID" Type="Int32"></asp:Parameter>
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </div>                    
                </div>
            </div>
        </div>
    </div>
        </div>

</asp:Content>

