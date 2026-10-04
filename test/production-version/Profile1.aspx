<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="Profile1.aspx.vb" Inherits="production_version_Profile" %>
<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid animatedParent animateOnce my-3">
        <div class="container-fluid my-3">
            <div class="row">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-body">

                            <div class="row">
                                <div class="col-md-5">
                                    <div class="card">
                                        <div class="card-body">
                                            <h5 class="text-center"><strong>ข้อมูลโปรไฟล์</strong></h5>
                                            <br />
                                             <asp:DataList ID="DataList1" runat="server" DataKeyField="UserID" DataSourceID="SqlDataSourceProfile" Width="100%">
                                                <ItemTemplate>
                                                    <ul class="list-group list-group-flush">
                                                        <li class="list-group-item"><i class="icon icon-mail text-success"></i><strong class="s-12">Username</strong> <span class="float-right s-12"><strong><%#: Eval("Username") %></strong></span></li>
                                                        <li class="list-group-item"><i class="icon icon-web text-danger"></i><strong class="s-12">Email</strong> <span class="float-right s-12"><strong><%#: Eval("Email") %></strong></span></li>
                                                        <li class="list-group-item"><i class="icon icon-account_circle text-danger"></i><strong class="s-12">Name</strong> <span class="float-right s-12"><strong><%#: Eval("Name") %></strong></span></li>
                                                    </ul>
                                                   <%-- UserID:
                                                    <asp:Label Text='<%#: Eval("UserID") %>' runat="server" ID="UserIDLabel" /><br />
                                                    Username:
                                                    <asp:Label Text='<%#: Eval("Username") %>' runat="server" ID="UsernameLabel" /><br />
                                                    Email:
                                                    <asp:Label Text='<%#: Eval("Email") %>' runat="server" ID="EmailLabel" /><br />
                                                    Name:
                                                    <asp:Label Text='<%#: Eval("Name") %>' runat="server" ID="NameLabel" /><br />
                                                    Urole:
                                                    <asp:Label Text='<%#: Eval("Urole") %>' runat="server" ID="UroleLabel" /><br />
                                                    <br />--%>
                                                </ItemTemplate>
                                            </asp:DataList>

                                            <asp:SqlDataSource runat="server" ID="SqlDataSourceProfile" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT UserID, Username, Email, Name, Urole FROM Signin_Signup WHERE (UserID = @UserID)" UpdateCommand="UPDATE Signin_Signup SET Username = @Username, Email = @Email, Name = @Name WHERE (UserID = @UserID)">
                                                <SelectParameters>
                                                    <asp:SessionParameter SessionField="UserID" Name="UserID"></asp:SessionParameter>
                                                </SelectParameters>
                                                <UpdateParameters>
                                                    <asp:Parameter Name="Username"></asp:Parameter>
                                                    <asp:Parameter Name="Email"></asp:Parameter>
                                                    <asp:Parameter Name="Name"></asp:Parameter>
                                                    <asp:Parameter Name="UserID"></asp:Parameter>
                                                </UpdateParameters>
                                            </asp:SqlDataSource>
                                            
                                        </div>


                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="card">
                                        <div class="card text-center">
                                            <h5 class="text-center"><strong>แก้ไขข้อมูล</strong></h5>
                                            <br />
                                            <dx:BootstrapGridView ID="BootstrapGridViewEditProfile" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceProfile" KeyFieldName="UserID">
                                                <SettingsDataSecurity AllowEdit="True"></SettingsDataSecurity>
                                                <Columns>
                                                    <dx:BootstrapGridViewCommandColumn ShowEditButton="True" VisibleIndex="0"></dx:BootstrapGridViewCommandColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="UserID" ReadOnly="True" VisibleIndex="1" Caption="ลำดับ">
                                                        <SettingsEditForm Visible="False"></SettingsEditForm>
                                                    </dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Username" VisibleIndex="2"></dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Email" VisibleIndex="4"></dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Name" VisibleIndex="5"></dx:BootstrapGridViewTextColumn>
                                                </Columns>
                                            </dx:BootstrapGridView>
                                           
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

