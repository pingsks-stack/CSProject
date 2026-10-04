<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="Edit.aspx.vb" Inherits="production_version_Edit" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid animatedParent animateOnce my-3">
            <div class="container-fluid my-3">
                <div class="d-flex row">
                    <div class="col-md-12">
                        <div class="container">
                            <div class="card">
                                <div class="card-header white">
                                    <h4>แก้ไขข้อมูล</h4>
                                </div>

                                <div class="card-body">
                                    <div class="form-row">
                                        <div class="col-md-6 mb-3">
                                            <label for="firstname" class="form-label">ชื่อ-นามสกุล(บุคคลที่่ 1)</label>
                                            <dx:BootstrapTextBox ID="Name_lname" runat="server">
                                            </dx:BootstrapTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="กรุณาระบุชื่อ-นามสกุล" ControlToValidate="Name_lname" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-6 mb-3">
                                            <label for="firstname" class="form-label">รหัสนิสิต(บุคคลที่่ 1)</label>
                                            <dx:BootstrapTextBox ID="Studentid" runat="server">
                                            </dx:BootstrapTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="กรุณาระบุรหัสนิสิต" ControlToValidate="Studentid" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-md-6 mb-3">
                                            <label for="pdfupload" class="form-label">ชื่อ-นามสกุล(บุคคลที่่ 2)</label>
                                            <dx:BootstrapTextBox ID="Name_lname2" runat="server" NullText="กรณีมีคนเดียวให้ใส่ -">
                                            </dx:BootstrapTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณาระบุชื่อ-นามสกุล" ControlToValidate="Name_lname2" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-6 mb-3">
                                            <label for="pdfupload" class="form-label">รหัสนิสิต(บุคคลที่ 2)</label>
                                            <dx:BootstrapTextBox ID="Studentid2" runat="server" NullText="กรณีมีคนเดียวให้ใส่ -">
                                            </dx:BootstrapTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="กรุณาระบุรหัสนิสิต" ControlToValidate="Studentid2" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-md-6 mb-3">
                                            <label for="firstname" class="form-label">เบอร์โทรศัพท์(บุคคลที่่ 1)</label>
                                            <dx:BootstrapTextBox ID="Phonenumber" runat="server">
                                            </dx:BootstrapTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="กรุณาระบุเบอร์โทรศัพท์" ControlToValidate="Phonenumber" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-6 mb-3">
                                            <label for="firstname" class="form-label">เบอร์โทรศัพท์(บุคคลที่ 2)</label>
                                            <dx:BootstrapTextBox ID="Phonenumber2" runat="server" NullText="กรณีมีคนเดียวให้ใส่ -">
                                            </dx:BootstrapTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="กรุณาระบุเบอร์โทรศัพท์" ControlToValidate="Phonenumber2" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-md-6 mb-3">
                                            <label for="namedoc" class="form-label">ชื่อโปรเจค(ภาษาไทย)</label>
                                            <dx:BootstrapTextBox ID="NamprojectTH" runat="server"></dx:BootstrapTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค" ControlToValidate="NamprojectTH" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="col-md-6 mb-3">
                                            <label for="namedoc" class="form-label">ชื่อโปรเจค(ภาษาอังกฤษ)</label>
                                            <dx:BootstrapTextBox ID="NameprojectEN" runat="server"></dx:BootstrapTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค(ภาษาอังกฤษ)" ControlToValidate="NameprojectEN" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>

                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-md-6 mb-3">
                                            <label for="validationServer01">ชั้นปี</label>
                                            <dx:BootstrapComboBox ID="ClassNo" runat="server" DataSourceID="SqlDataSource1" ValueField="Class_ID" TextField="Class_No"></dx:BootstrapComboBox>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT * FROM [Class_TB]"></asp:SqlDataSource>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="กรุณาเลือกชั้นปี" ControlToValidate="ClassNo" Font-Bold="True" Font-Overline="False" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </div>
                                       <%-- <div class="col-md-6 mb-3">
                                            <label for="validationServer02">ปีการศึกษา</label>
                                            <dx:BootstrapComboBox ID="Class_year" runat="server" DataSourceID="SqlDataSource2" ValueField="Year_ID" TextField="Class_Year_No"></dx:BootstrapComboBox>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT * FROM [Class_Year_TB]"></asp:SqlDataSource>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="กรุณาเลือกปีการศึกษา" ControlToValidate="Class_year" Font-Bold="True" Font-Overline="False" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>--%>
                                    <%--  <div class="form-group">         
                                          <label for="pdfupload" class="form-label">อัพโหลดเอกสาร(.PDF)</label>
                                                <font color="red">*.pdf</font>
                                         <asp:FileUpload ID="FileUpload1" AllowMultiple="true" runat="server" Width="100%" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="กรุณาอัพโหลดไฟล์(.PDF)" ControlToValidate="FileUpload1" Font-Bold="True" Font-Overline="False" CssClass="text-danger"></asp:RequiredFieldValidator>
                                    </div>--%>
                                    <%--SHOW UPLOAD MESSAGE--%>
                                    <p>
                                        <asp:Label ID="lblFileList" runat="server"></asp:Label></p>
                                    <p>
                                        <asp:Label ID="lblUploadStatus" runat="server"></asp:Label></p>
                                    <p>
                                        <asp:Label ID="lblFailedStatus" runat="server"></asp:Label></p>
                                    
                                    
                                  

                                                </div>
                                      
                                
                                  <asp:Button ID="Button2" runat="server" Text="Button" />
                            </div>
                        </div>
                    </div>

                        </div>


                </div>
            </div>

</asp:Content>

