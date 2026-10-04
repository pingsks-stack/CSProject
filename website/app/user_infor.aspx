<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="user_infor.aspx.vb" Inherits="production_version_user_infor" %>

<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <%--  <div class="container-fluid animatedParent animateOnce my-3">
        <div class="container-fluid my-3">
        <div class="d-flex row">
            <div class="col-md-12">
                <div class="card text-center">
                    <div class="card-header">
                        <h1>ข้อมูล</h1>
                    </div>
                    <div class="card-body">
                         <div class="form-row">
                                        <div class="col-md-3 mb-3">
                                            <label for="firstname" class="form-label">เลือกชั้นปีเพื่อแสดงข้อมูล</label>
                                            <dx:BootstrapComboBox ID="BootstrapComboBox1" runat="server" AutoPostBack="True" SelectedIndex="0">
                                                <Items>
                                                    <dx:BootstrapListEditItem Text="ชั้นปีที่ 1" Value="1">
                                                    </dx:BootstrapListEditItem>
                                                    <dx:BootstrapListEditItem Text="ชั้นปีที่ 2" Value="2">
                                                    </dx:BootstrapListEditItem>
                                                    <dx:BootstrapListEditItem Text="ชั้นปีที่ 3" Value="3">
                                                    </dx:BootstrapListEditItem>
                                                    <dx:BootstrapListEditItem Text="ชั้นปีที่ 4" Value="4">
                                                    </dx:BootstrapListEditItem>
                                                </Items>
                                            </dx:BootstrapComboBox>
                                        </div>
                                  
                                        <div class="col-md-6 mb-3">
                                                                                   
                                        </div>                                       
                                    </div>
                       
                       
                        <dx:BootstrapGridView ID="BootstrapGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" SettingsSearchPanel-AllowTextInputTimer="True" Settings-ShowGroupPanel="False" ClientInstanceName="BootstrapGridView1">
                            <Columns>
                                <dx:BootstrapGridViewCommandColumn VisibleIndex="0"></dx:BootstrapGridViewCommandColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Doc_Name" VisibleIndex="1" Caption="ชื่อโปรเจค(ภาษาไทย)"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Doc_NameEN" VisibleIndex="2" Caption="ชื่อโปรเจค(ภาษาอังกฤษ)"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Student_Name" VisibleIndex="4" Caption="ชื่อ-นามสกุล"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Email" VisibleIndex="5" Caption="อีเมลล์"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Doc_Type" VisibleIndex="8" Caption="ประเภท"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn Caption="ฟังก์ชัน" VisibleIndex="10">
                                    <DataItemTemplate>
                                        <button type="button" onclick="window.location.href='Code.aspx?doc='+'<%#Eval("DocID")%>'" class="btn btn-info " style="width: 100%"><span class="glyphicon glyphicon-print" aria-hidden="true">เปิดดู</span></button>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Doc_File" Caption="ไฟล์" VisibleIndex="9">
                                    <PropertiesTextEdit DisplayFormatString="{0}"></PropertiesTextEdit>

                                    <SettingsEditForm ShowCaption="False" Visible="False"></SettingsEditForm>
                                    <DataItemTemplate>

                                        <%-- <button type="button" onclick="'+'<%#Eval("Doc_File")%>'" class="btn btn-primary " style="width: 100%"><span class="glyphicon glyphicon-print" aria-hidden="true">เปิดดู</span></button>--%>

                                       <%-- <a href="Docs/<%#: Eval("Doc_File")%>"><strong>เอกสาร</strong></a>--%>
                                   <%-- </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Class_No" Caption="ชั้นปี" VisibleIndex="6"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Class_Year_No" Caption="ปีการศึกษา" VisibleIndex="7"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Student_ID" Caption="รหัสนิสิต" VisibleIndex="3"></dx:BootstrapGridViewTextColumn>
                            </Columns>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                        </dx:BootstrapGridView>
                       
                      
                        <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT DocID, Doc_Name, Doc_NameEN, Student_ID, Student_Name, FacultyID, Email, Mobile, Student_Status, Doc_Type, Class_No, Class_Year_No, Doc_dt_Save FROM MAS_DOC WHERE (Class_No = @Class_No)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="BootstrapComboBox1" PropertyName="Value" Name="Class_No"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                        </div>
                    </div>                   
                </div>
            </div>
        </div>
    </div>--%>
     <div class="container-fluid animatedParent animateOnce my-3">
        <div class="container-fluid my-3">
        <div class="d-flex row">
            <div class="col-md-12">
                <div class="card text-center">
                    <div class="card-header">
                        <h1>ข้อมูล</h1>
                       
                    </div>
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col-md-3 mb-3">
                                <label for="firstname" class="form-label">เลือกชั้นปีเพื่อแสดงข้อมูล</label>
                                <dx:BootstrapComboBox ID="BootstrapComboBox1" runat="server" AutoPostBack="True" SelectedIndex="0">
                                    <Items>
                                        <dx:BootstrapListEditItem Text="ชั้นปีที่ 1" Value="1">
                                        </dx:BootstrapListEditItem>
                                        <dx:BootstrapListEditItem Text="ชั้นปีที่ 2" Value="2">
                                        </dx:BootstrapListEditItem>
                                        <dx:BootstrapListEditItem Text="ชั้นปีที่ 3" Value="3">
                                        </dx:BootstrapListEditItem>
                                        <dx:BootstrapListEditItem Text="ชั้นปีที่ 4" Value="4">
                                        </dx:BootstrapListEditItem>
                                    </Items>
                                </dx:BootstrapComboBox>
                            </div>

                            <div class="col-md-6 mb-3">
                            </div>
                        </div>
                        <dx:BootstrapGridView ID="BootstrapGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource_infor_masdoc" KeyFieldName="DocID" SettingsEditing-Mode="EditFormAndDisplayRow" Width="100%" SettingsEditing-EditFormColumnSpan="6" SettingsExport-MaxColumnWidth="300" ClientInstanceName="BootstrapGridView1">
                            <Columns>
                                <dx:BootstrapGridViewCommandColumn VisibleIndex="0" Visible="False"></dx:BootstrapGridViewCommandColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="DocID" ReadOnly="True" VisibleIndex="1" Caption="ลำดับเอกสาร" HorizontalAlign="Center">
                                    <SettingsEditForm Visible="False"></SettingsEditForm>
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Doc_Name" VisibleIndex="2" Caption="ชื่อโปรเจค(ภาษาไทย)"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Doc_NameEN" VisibleIndex="3" Caption="ชื่อโปรเจค(ภาษาอังกฤษ)"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Email" VisibleIndex="14" Caption="อีเมลล์" Visible="False">
                                    <SettingsEditForm Visible="False"></SettingsEditForm>
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn VisibleIndex="17" Caption="ดูโค้ด" Width="100px">
                                    <SettingsEditForm Visible="False" ShowCaption="False"></SettingsEditForm>
                                    <DataItemTemplate>
                                        <button type="button" onclick="window.location.href='Code.aspx?doc='+'<%#Eval("DocID")%>'" class="btn btn-success">ดูโค้ด</button>

                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                                <%-- <dx:BootstrapGridViewTextColumn FieldName="Doc_File" Caption="ไฟล์" VisibleIndex="11">
                                    <PropertiesTextEdit DisplayFormatString="{0}"></PropertiesTextEdit>

                                    <SettingsEditForm ShowCaption="True" Visible="True"></SettingsEditForm>
                                    <DataItemTemplate>
                                        <a href="Docs/<%#: Eval("Doc_File")%>"><%#: Eval("Doc_File")%></a>
                                       
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>--%>
                                <%-- <dx:BootstrapGridViewTextColumn FieldName="Student_Status" Caption="สถานะ" VisibleIndex="10">
                                    <DataItemTemplate>
                                        <center>
                                            <button type="button" style='<%# IIf(Eval("Student_Status") = 1, "", "visibility:hidden;position: absolute")%>; width: 100%; background-color: #ED5564;' onclick="window.location.href='CodeEdit.aspx?DocID='+'<%#Eval("DocID")%>'+'&status='+'<%#Eval("Student_Status")%>'" class="btn btn-danger "><span class="glyphicon glyphicon-print" aria-hidden="true">รอเพิ่มฟังก์ชัน</span></button>
                                            <button type="button" class="btn btn-success" style='<%# IIf(Eval("Student_Status") = 2, "", "visibility:hidden;position: absolute")%>; width: 50%;'>เสร็จสิน</button>
                                        </center>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>--%>
                                <dx:BootstrapGridViewComboBoxColumn FieldName="Doc_Type" Caption="ประเภทเอกสาร" VisibleIndex="15" HorizontalAlign="Center" Visible="False">
                                    <PropertiesComboBox TextField="Doc_Type" DataSourceID="SqlDataType" ValueField="Doc_Type" DropDownStyle="DropDown" Width="100%"></PropertiesComboBox>
                                </dx:BootstrapGridViewComboBoxColumn>

                                <dx:BootstrapGridViewTextColumn VisibleIndex="16" Caption="เวลาที่บันทึก" FieldName="Doc_dt_Save">
                                    <SettingsEditForm Visible="False"></SettingsEditForm>

                                </dx:BootstrapGridViewTextColumn>
                             <%--   <dx:BootstrapGridViewTextColumn VisibleIndex="18" Width="300px">
                                    <DataItemTemplate>                                       >
                                        <button type="button" <%# get_disabled(Eval("DocID")) %> id="myButton" class="btn btn-primary" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');NamprojectTH.SetValue('<%# SafeOut.JsAttr(Eval("Doc_Name")) %>');NameprojectEN.SetValue('<%# SafeOut.JsAttr(Eval("Doc_NameEN")) %>');$('#exampleModal').modal('show');cb_load.PerformCallback();" data-toggle="modal" data-target="#exampleModal">Edit</button>
                                        <button type="button" <%# get_disabled(Eval("DocID")) %> id="myButton1" class="btn btn-warning" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');$('#exampleModal4').modal('show');cb_load4.PerformCallback();" data-toggle="modal" data-target="#exampleModal4">Add User</button>
                                        <button type="button" class="btn btn-danger" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');$('#exampleModal6').modal('show');cb_load6.PerformCallback();" data-toggle="modal" data-target="#exampleModal6">Log1</button>
                                        <button type="button" class="btn btn-primary" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');$('#exampleModal7').modal('show');cb_load7.PerformCallback();" data-toggle="modal" data-target="#exampleModal7">Log2</button>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>--%>
                                <dx:BootstrapGridViewTextColumn Caption="ชื่อ-นามสกุล" VisibleIndex="9" Width="200px" HorizontalAlign="Center">
                                    <DataItemTemplate>
                                        <button type="button" class="btn btn-primary" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');Name1.SetValue('<%# SafeOut.JsAttr(Eval("Student_Name")) %>');StudentID1.SetValue('<%# SafeOut.JsAttr(Eval("Student_ID")) %>');Phonenumber1.SetValue('<%# SafeOut.JsAttr(Eval("Mobile")) %>');$('#exampleModal1').modal('show');cb_load.PerformCallback();" data-toggle="modal" data-target="#exampleModal1">บุคคลที่ 1</button>
                                        <button type="button" class="btn btn-warning" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');Name2.SetValue('<%# SafeOut.JsAttr(Eval("Student_Name2")) %>');StudentID2.SetValue('<%# SafeOut.JsAttr(Eval("Student_ID2")) %>');Phonenumber2.SetValue('<%# SafeOut.JsAttr(Eval("Mobile2")) %>');$('#exampleModal3').modal('show');cb_load.PerformCallback();" data-toggle="modal" data-target="#exampleModal3">บุคคลที่ 2</button>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Class_No" Caption="ชั้นปี" VisibleIndex="11"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Class_Year_No" Caption="ปีการศึกษา" VisibleIndex="12"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn Caption="ไฟล์" VisibleIndex="13">
                                    <DataItemTemplate>
                                         <button type="button" class="btn btn-danger" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');$('#exampleModal5').modal('show');cb_load5.PerformCallback();" data-toggle="modal" data-target="#exampleModal5">ไฟล์</button>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                            </Columns>
                            <SettingsSearchPanel Visible="True"></SettingsSearchPanel>
                        </dx:BootstrapGridView>
                       
                        <asp:SqlDataSource runat="server" ID="SqlDataSource_infor_masdoc" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT MAS_DOC.DocID, MAS_DOC.Doc_Name, MAS_DOC.Doc_NameEN, MAS_DOC.Student_ID, MAS_DOC.Student_Name, MAS_DOC.Email, MAS_DOC.Mobile, MAS_DOC.Class_No, MAS_DOC.Class_Year_No, Permission_TB.UserID, MAS_DOC.Student_ID2, MAS_DOC.Student_Name2, MAS_DOC.Mobile2, MAS_DOC.Doc_dt_Save FROM MAS_DOC INNER JOIN Permission_TB ON MAS_DOC.DocID = Permission_TB.DocID GROUP BY MAS_DOC.DocID, MAS_DOC.Doc_Name, MAS_DOC.Doc_NameEN, MAS_DOC.Student_ID, MAS_DOC.Student_Name, MAS_DOC.Email, MAS_DOC.Mobile, MAS_DOC.Class_No, MAS_DOC.Class_Year_No, Permission_TB.UserID, MAS_DOC.Student_ID2, MAS_DOC.Student_Name2, MAS_DOC.Mobile2, MAS_DOC.Doc_dt_Save HAVING (MAS_DOC.Class_No = @Class_No)" DeleteCommand="DELETE FROM MAS_DOC WHERE (DocID = @DocID)" UpdateCommand="UPDATE MAS_DOC SET Doc_Name = @Doc_Name, Doc_NameEN = @Doc_NameEN, Student_ID = @Student_ID, Student_Name = @Student_Name, Email = @Email, Mobile = @Mobile, Doc_Type = @Doc_Type, Class_No = @Class_No, Class_Year_No = @Class_Year_No WHERE (DocID = @DocID)">
                            <DeleteParameters>
                                <asp:Parameter Name="DocID"></asp:Parameter>
                            </DeleteParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="BootstrapComboBox1" PropertyName="Value" Name="Class_No"></asp:ControlParameter>
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="Doc_Name"></asp:Parameter>
                                <asp:Parameter Name="Doc_NameEN"></asp:Parameter>
                                <asp:Parameter Name="Student_ID"></asp:Parameter>
                                <asp:Parameter Name="Student_Name"></asp:Parameter>
                                <asp:Parameter Name="Email"></asp:Parameter>
                                <asp:Parameter Name="Mobile"></asp:Parameter>
                                <asp:Parameter Name="Doc_Type"></asp:Parameter>
                                <asp:Parameter Name="Class_No"></asp:Parameter>
                                <asp:Parameter Name="Class_Year_No"></asp:Parameter>
                                <asp:Parameter Name="DocID"></asp:Parameter>
                            </UpdateParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource runat="server" ID="SqlDataSource_index" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Doc_Name, Doc_NameEN, Student_Name, Email, Mobile, Doc_Type FROM MAS_DOC WHERE (Email = @Email)">
                            <SelectParameters>
                                <asp:SessionParameter SessionField="Email" Name="Email"></asp:SessionParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>                                               
                    </div>
                    <div class="card-footer text-muted">
                         <dx:BootstrapTextBox ID="txt_ID" runat="server" Width="170px" ClientInstanceName="txt_ID"></dx:BootstrapTextBox>                      
                    </div>
                </div>
            </div>
        </div>
    </div>
        </div>
    

    <%--<dx:ASPxTextBox ID="txt_ID" runat="server" Width="170px" ClientInstanceName="txt_ID"></dx:ASPxTextBox>--%>
    <asp:SqlDataSource ID="SqlDataClass" runat="server" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT * FROM [Class_TB]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataClass_year" runat="server" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT * FROM [Class_Year_TB]"></asp:SqlDataSource>

   
    <asp:SqlDataSource ID="SqlDataType" runat="server" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT DocTypeTB.* FROM DocTypeTB"></asp:SqlDataSource>

    <div class="modal fade bd-example-modal-lg" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Edit</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
               
                <div class="modal-body">
                <dx:ASPxCallbackPanel ID="cb_load" runat="server" Width="100%" ClientInstanceName="cb_load1" OnCallback="cb_load_Callback">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                                    <div class="form-row">
                                        <div class="col-md-6 mb-3">
                                            <label for="namedoc" class="form-label">ชื่อโปรเจค(ภาษาไทย)</label>
                                            <dx:BootstrapTextBox ID="NamprojectTH" runat="server" Text='' ClientInstanceName="NamprojectTH"></dx:BootstrapTextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค" ControlToValidate="NamprojectTH" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="col-md-6 mb-3">
                                            <label for="namedoc" class="form-label">ชื่อโปรเจค(ภาษาอังกฤษ)</label>
                                            <dx:BootstrapTextBox ID="NameprojectEN" runat="server" Text='' ClientInstanceName="NameprojectEN"></dx:BootstrapTextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค(ภาษาอังกฤษ)" ControlToValidate="NameprojectEN" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                        </div>
                                         <%--<div class="col-md-4 mb-3">
                                            <label for="namedoc" class="form-label">File PDF</label>
                                             <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="True" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค(ภาษาอังกฤษ)" ControlToValidate="NameprojectEN" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                                        </div>--%>
                                    </div>   
                             <p>
                                        <asp:Label ID="lblFileList" runat="server"></asp:Label></p>
                                    <p>
                                        <asp:Label ID="lblUploadStatus" runat="server"></asp:Label></p>
                                    <p>
                                        <asp:Label ID="lblFailedStatus" runat="server"></asp:Label></p>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>

            </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                  <%--  <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Save changes" />--%>                  
                </div>
        </div>
    </div>
    </div>
    <div class="modal fade bd-example-modal-lg" id="exampleModal1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel1">ข้อมูลบุคคลที่ 1</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel2" runat="server" Width="100%" ClientInstanceName="cb_load" OnCallback="cb_load2_Callback">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <div class="form-row">
                                    <div class="col-md-4 mb-3">
                                        <label for="namedoc" class="form-label">ชื่อ-นามสกุล</label>
                                        <dx:BootstrapTextBox ID="Name1" runat="server" Text='' ClientInstanceName="Name1"></dx:BootstrapTextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค" ControlToValidate="NamprojectTH" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <label for="namedoc" class="form-label">รหัสนิสิต</label>
                                        <dx:BootstrapTextBox ID="StudentID1" runat="server" Text='' ClientInstanceName="StudentID1"></dx:BootstrapTextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค(ภาษาอังกฤษ)" ControlToValidate="NameprojectEN" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <label for="namedoc" class="form-label">เบอร์โทร</label>
                                        <dx:BootstrapTextBox ID="Phonenumber1" runat="server" Text='' ClientInstanceName="Phonenumber1"></dx:BootstrapTextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค" ControlToValidate="NamprojectTH" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                   <%-- <asp:Button ID="Button6" class="btn btn-primary" runat="server" Text="Save changes" />   --%>                 
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bd-example-modal-lg" id="exampleModal3" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel2">ข้อมูลบุคคลที่ 2</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel3" runat="server" Width="100%" ClientInstanceName="cb_load3" OnCallback="cb_load_Callback">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <div class="form-row">
                                    <div class="col-md-4 mb-3">
                                        <label for="namedoc" class="form-label">ชื่อ-นามสกุล</label>
                                        <dx:BootstrapTextBox ID="Name2" runat="server" Text='' ClientInstanceName="Name2"></dx:BootstrapTextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค" ControlToValidate="NamprojectTH" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <label for="namedoc" class="form-label">รหัสนิสิต</label>
                                        <dx:BootstrapTextBox ID="StudentID2" runat="server" Text='' ClientInstanceName="StudentID2"></dx:BootstrapTextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค(ภาษาอังกฤษ)" ControlToValidate="NameprojectEN" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="col-md-4 mb-3">
                                        <label for="namedoc" class="form-label">เบอร์โทร</label>
                                        <dx:BootstrapTextBox ID="Phonenumber2" runat="server" Text='' ClientInstanceName="Phonenumber2"></dx:BootstrapTextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค" ControlToValidate="NamprojectTH" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" aria-setsize="50">Close</button>
                   <%-- <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Save changes" />--%>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade bd-example-modal-lg" id="exampleModal4" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel3">Add User</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" Width="100%" ClientInstanceName="cb_load4" OnCallback="cb_load4_Callback">
                        <PanelCollection>
                            <dx:PanelContent runat="server">

                                <dx:BootstrapGridView ID="BootstrapGridView2" runat="server" ClientInstanceName="BootstrapGridView2" AutoGenerateColumns="False" DataSourceID="SqlDataSource_Permisson" KeyFieldName="Permission_ID">
                                    <SettingsDataSecurity AllowEdit="True" AllowInsert="True" AllowDelete="True"></SettingsDataSecurity>
                                    <Columns>
                                        <dx:BootstrapGridViewCommandColumn ShowEditButton="True" VisibleIndex="0" ShowNewButtonInHeader="True" ShowDeleteButton="True"></dx:BootstrapGridViewCommandColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="DocID" VisibleIndex="1" HorizontalAlign="Center" Caption="ลำดับเอกสาร"></dx:BootstrapGridViewTextColumn>

                                        <dx:BootstrapGridViewTextColumn FieldName="UserID" VisibleIndex="3" HorizontalAlign="Center">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Name" VisibleIndex="4" Caption="ชื่อ-นามสกุล"></dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Email" VisibleIndex="5" Caption="อีเมลล์"></dx:BootstrapGridViewTextColumn>
                                    </Columns>
                                </dx:BootstrapGridView>

                                <asp:SqlDataSource runat="server" ID="SqlDataSource_Permisson" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' DeleteCommand="DELETE FROM [Permission_TB] WHERE [Permission_ID] = @Permission_ID" InsertCommand="INSERT INTO Permission_TB(DocID, UserID, Name, Email) VALUES (@DocID, @UserID, @Name, @Email)" SelectCommand="SELECT DocID, UserID, Name, Email, Permission_ID FROM Permission_TB WHERE (DocID = @DocID)" UpdateCommand="UPDATE Permission_TB SET DocID = @DocID, UserID = @UserID, Name = @Name, Email = @Email WHERE (Permission_ID = @Permission_ID)">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Permission_ID" Type="Int32"></asp:Parameter>
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="DocID" Type="Int32"></asp:Parameter>
                                        <asp:Parameter Name="UserID" Type="Int32"></asp:Parameter>
                                        <asp:Parameter Name="Name" Type="String"></asp:Parameter>
                                        <asp:Parameter Name="Email" Type="String"></asp:Parameter>
                                    </InsertParameters>
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txt_ID" PropertyName="Text" Name="DocID"></asp:ControlParameter>
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="DocID" Type="Int32"></asp:Parameter>
                                        <asp:Parameter Name="UserID" Type="Int32"></asp:Parameter>
                                        <asp:Parameter Name="Name" Type="String"></asp:Parameter>
                                        <asp:Parameter Name="Email" Type="String"></asp:Parameter>
                                        <asp:Parameter Name="Permission_ID" Type="Int32"></asp:Parameter>
                                    </UpdateParameters>
                                </asp:SqlDataSource>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Save changes" />

                </div>

            </div>


        </div>
    </div>
    <div class="modal fade" id="exampleModal5" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel5">ไฟล์ที่อัพโหลด</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel4" runat="server" Width="100%" ClientInstanceName="cb_load5" OnCallback="cb_load5_Callback">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" DataKeyField="Doc_File_ID" Width="100%" OnItemCommand="DataList1_ItemCommand">
                                    <ItemTemplate>
                                        <div>
                                            <tr>
                                                <td>  <a href="Docs/<%#: Eval("Doc_File")%>"><button class="btn btn-link" style="width: 200px"><%#: Eval("Doc_File")%></button></a></td>
                                              <%--  <td><asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("Doc_File_ID") %>' OnClientClick="return confirm('คุณต้องการลบรายการนี้?');" /></td>--%>
                                            </tr>
                                            <!-- Display the data -->
                                           
                                             
                                            <!-- Delete button -->
                                            
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                               
                                <%--<asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" DataKeyField="DocID" Width="100%">
                                    <ItemTemplate>
                                        <a href="Docs/<%#: Eval("Doc_File")%>"><button class="btn btn-link"><%#: Eval("Doc_File")%></button></a>
                                        <asp:Button ID="Button3" runat="server" Text="delete" OnClick="Button3_Click" />

                                  
                                    </ItemTemplate>
                                </asp:DataList>--%>



                                <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT DocID, Doc_File, Doc_File_ID FROM Doc_File WHERE (DocID = @DocID)" DeleteCommand="DELETE FROM Doc_File WHERE (Doc_File_ID = @Doc_File_ID)">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Doc_File_ID"></asp:Parameter>
                                    </DeleteParameters>
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txt_ID" PropertyName="Text" Name="DocID"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                   <%-- <asp:Button ID="Button5" runat="server" class="btn btn-primary" Text="Save changes" />--%>

                </div>

            </div>


        </div>
    </div>
     <div class="modal fade bd-example-modal-lg" id="exampleModal6" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel6">Log File</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel5" runat="server" Width="100%" ClientInstanceName="cb_load6" OnCallback="cb_load6_Callback">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <dx:BootstrapGridView ID="BootstrapGridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" KeyFieldName="Doc_File_LogID">
                                    <Columns>
                                        <dx:BootstrapGridViewTextColumn FieldName="Doc_File_LogID" VisibleIndex="0" ReadOnly="True" Visible="False">
                                            <SettingsEditForm Visible="False"></SettingsEditForm>
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Doc_File_ID" VisibleIndex="1" Visible="False"></dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="DocID" VisibleIndex="2" Visible="False"></dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Doc_File" VisibleIndex="3" Caption="ชื่อไฟล์" HorizontalAlign="Center"></dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Post_Name" VisibleIndex="4" Caption="โดย" HorizontalAlign="Center"></dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Action" Caption="การกระทำ" VisibleIndex="6" HorizontalAlign="Center"></dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn HorizontalAlign="Center" FieldName="Date_Delete" Caption="วัน-เวลา" VisibleIndex="5"></dx:BootstrapGridViewTextColumn>


                                    </Columns>
                                </dx:BootstrapGridView>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Doc_File_LogID, Doc_File_ID, DocID, Doc_File, Post_Name, Date_Delete, Action FROM Doc_File_Log WHERE (DocID = @DocID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txt_ID" PropertyName="Text" Name="DocID"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                   <%-- <asp:Button ID="Button7" runat="server" class="btn btn-primary" Text="Save changes" />--%>

                </div>

            </div>


        </div>
    </div>
    <div class="modal fade bd-example-modal-lg" id="exampleModal7" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel7">Log File</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel6" runat="server" Width="100%" ClientInstanceName="cb_load7" OnCallback="cb_load7_Callback">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <dx:BootstrapGridView ID="BootstrapGridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" ClientInstanceName='<div class="modal fade bd-example-modal-lg" id="exampleModal6" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">'>
                                    <Columns>
                                        <dx:BootstrapGridViewTextColumn FieldName="Doc_Name" VisibleIndex="0" Caption="ชื่อโปรเจค" HorizontalAlign="Center">
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Doc_NameEN" VisibleIndex="1" Caption="ชื่อโปรเจค(ภาษาอังกฤษ)" HorizontalAlign="Center"></dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Action" VisibleIndex="5" Caption="การกระทำ" HorizontalAlign="Center"></dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="DocID" VisibleIndex="2" Visible="False"></dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Post_Name" VisibleIndex="3" Caption="โดย" HorizontalAlign="Center"></dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Doc_dt_Save" Caption="วันเวลา" VisibleIndex="4" HorizontalAlign="Center">                                          
                                        </dx:BootstrapGridViewTextColumn>

                                    </Columns>
                                </dx:BootstrapGridView>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource3" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Doc_Name, Doc_NameEN, Action, DocID, Post_Name, Doc_dt_Save FROM MAS_DOC_LOG WHERE (DocID = @DocID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txt_ID" PropertyName="Text" Name="DocID"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                  <%--  <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Save changes" />--%>

                </div>

            </div>


        </div>
    </div>
       
</asp:Content>

