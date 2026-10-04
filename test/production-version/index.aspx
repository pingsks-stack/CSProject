<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="index.aspx.vb" Inherits="production_version_index" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>



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

    <div class="container-fluid animatedParent animateOnce my-3">
        <div class="container-fluid my-3">
        <div class="d-flex row">
            <div class="col-md-12">
                <div class="card text-center">
                    <div class="card-header">
                        <h1>ข้อมูล</h1>                                         
                    </div>
                    <div class="card-body">                                                    
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
                                <dx:BootstrapGridViewTextColumn VisibleIndex="17" Caption="เพิ่มโค้ด" Width="100px">
                                    <SettingsEditForm Visible="False" ShowCaption="False"></SettingsEditForm>
                                    <DataItemTemplate>
                                        <button type="button" onclick="window.location.href='function.aspx?DocID='+'<%#Eval("DocID")%>'" class="btn btn-success">เพิ่มโค้ด</button>

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
                                            <button type="button" style='<%# IIf(Eval("Student_Status") = 1, "", "visibility:hidden;position: absolute")%>; width: 100%; background-color: #ED5564;' onclick="window.location.href='function.aspx?DocID='+'<%#Eval("DocID")%>'+'&status='+'<%#Eval("Student_Status")%>'" class="btn btn-danger "><span class="glyphicon glyphicon-print" aria-hidden="true">รอเพิ่มฟังก์ชัน</span></button>
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
                                <dx:BootstrapGridViewTextColumn VisibleIndex="18" Width="300px">
                                    <DataItemTemplate>
                                        <%--<button type="button" <%# get_disabled(Eval("DocID")) %> onclick="window.location.href='Edit.aspx?DocID='+'<%#Eval("DocID")%>'" class="btn btn-light">Edit</button>--%>
                                     
                                        <button type="button" <%# get_disabled(Eval("DocID")) %> id="myButton1" class="btn btn-warning" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');$('#exampleModal10').modal('show');cb_load10.PerformCallback();" data-toggle="modal" data-target="#exampleModal10">ข้อมูลอาจารย์ที่ปรึกษา/กรรมการ</button>
                                    
                                   



                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn Caption="ชื่อ-นามสกุล" VisibleIndex="9" Width="200px" HorizontalAlign="Center">
                                    <DataItemTemplate>
                                         <button type="button" class="btn btn-primary" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');Name1.SetValue('<%# SafeOut.JsAttr(Eval("Student_Name")) %>');StudentID1.SetValue('<%# SafeOut.JsAttr(Eval("Student_ID")) %>');Phonenumber1.SetValue('<%# SafeOut.JsAttr(Eval("Mobile")) %>');txtEmail.SetValue('<%# SafeOut.JsAttr(Eval("Email")) %>');$('#exampleModal1').modal('show');cb_load.PerformCallback();" data-toggle="modal" data-target="#exampleModal1">คนที่ 1</button>
                                    <%# If(Eval("Student_ID2") Is DBNull.Value AndAlso Eval("Student_Name2") Is DBNull.Value AndAlso Eval("Mobile2") Is DBNull.Value AndAlso Eval("Email2") Is DBNull.Value,
                                                                                            String.Format("<button type='button' {0} id='myButton1' class='btn btn-warning' onclick='txt_ID.SetValue(&quot;{1}&quot;);$(&quot;#exampleModal4&quot;).modal(&quot;show&quot;);cb_load4.PerformCallback();' data-toggle='modal' data-target='#exampleModal4'>เพิ่ม</button>", get_disabled(Eval("DocID")), Eval("DocID")),
                                                                                            String.Format("<button type='button' class='btn btn-warning' onclick='txt_ID.SetValue(&quot;{0}&quot;);Name2.SetValue(&quot;{1}&quot;);StudentID2.SetValue(&quot;{2}&quot;);Phonenumber2.SetValue(&quot;{3}&quot;);Email2.SetValue(&quot;{4}&quot;);$(&quot;#exampleModal3&quot;).modal(&quot;show&quot;);cb_load.PerformCallback();' data-toggle='modal' data-target='#exampleModal3'>คนที่ 2</button>", Eval("DocID"), Eval("Student_Name2"), Eval("Student_ID2"), Eval("Mobile2"), Eval("Email2"))) %>

                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>

                                <dx:BootstrapGridViewTextColumn FieldName="Class_No" Caption="ชั้นปี" VisibleIndex="11"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn FieldName="Class_Year_No" Caption="ปีการศึกษา" VisibleIndex="12"></dx:BootstrapGridViewTextColumn>
                                <dx:BootstrapGridViewTextColumn Caption="ไฟล์/log file" Width="150px" VisibleIndex="13">
                                    <DataItemTemplate>
                                        <button type="button" class="btn btn-danger" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');$('#exampleModal5').modal('show');cb_load5.PerformCallback();" data-toggle="modal" data-target="#exampleModal5">ไฟล์</button>
                                        <button type="button" class="btn btn-danger" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');$('#exampleModal6').modal('show');cb_load6.PerformCallback();" data-toggle="modal" data-target="#exampleModal6">ประวัตืไฟล์</button>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>

                                <dx:BootstrapGridViewTextColumn Caption="แก้ไขชื่อโปรเจค/log" VisibleIndex="8">
                                    <DataItemTemplate>
                                        <button type="button" <%# get_disabled(Eval("DocID")) %> id="myButton" class="btn btn-primary" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');NamprojectTH.SetValue('<%# SafeOut.JsAttr(Eval("Doc_Name")) %>');NameprojectEN.SetValue('<%# SafeOut.JsAttr(Eval("Doc_NameEN")) %>');$('#exampleModal').modal('show');cb_load.PerformCallback();" data-toggle="modal" data-target="#exampleModal">แก้ไขชื่อโปรเจค</button>
                                        <button type="button" class="btn btn-primary" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');$('#exampleModal7').modal('show');cb_load7.PerformCallback();" data-toggle="modal" data-target="#exampleModal7">ประวัติการแก้ชื่อโปรเจค</button>
                                    </DataItemTemplate>
                                </dx:BootstrapGridViewTextColumn>
                            </Columns>
                        </dx:BootstrapGridView>
                       
                        <asp:SqlDataSource runat="server" ID="SqlDataSource_infor_masdoc" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT MAS_DOC.DocID, MAS_DOC.Doc_Name, MAS_DOC.Doc_NameEN, MAS_DOC.Student_ID, MAS_DOC.Student_Name, MAS_DOC.Mobile, MAS_DOC.Class_No, MAS_DOC.Class_Year_No, Permission_TB.UserID, MAS_DOC.Student_ID2, MAS_DOC.Student_Name2, MAS_DOC.Mobile2, MAS_DOC.Doc_dt_Save, Permission_TB.Email, MAS_DOC.Email2 FROM MAS_DOC INNER JOIN Permission_TB ON MAS_DOC.DocID = Permission_TB.DocID GROUP BY MAS_DOC.DocID, MAS_DOC.Doc_Name, MAS_DOC.Doc_NameEN, MAS_DOC.Student_ID, MAS_DOC.Student_Name, MAS_DOC.Mobile, MAS_DOC.Class_No, MAS_DOC.Class_Year_No, Permission_TB.UserID, MAS_DOC.Student_ID2, MAS_DOC.Student_Name2, MAS_DOC.Mobile2, MAS_DOC.Doc_dt_Save, Permission_TB.Email, MAS_DOC.Email2 HAVING (Permission_TB.Email = @Email)" DeleteCommand="DELETE FROM MAS_DOC WHERE (DocID = @DocID)" UpdateCommand="UPDATE MAS_DOC SET Doc_Name = @Doc_Name, Doc_NameEN = @Doc_NameEN, Student_ID = @Student_ID, Student_Name = @Student_Name, Email = @Email, Mobile = @Mobile, Doc_Type = @Doc_Type, Class_No = @Class_No, Class_Year_No = @Class_Year_No WHERE (DocID = @DocID)">
                            <DeleteParameters>
                                <asp:Parameter Name="DocID"></asp:Parameter>
                            </DeleteParameters>
                            <SelectParameters>
                                <asp:SessionParameter SessionField="Email" Name="Email"></asp:SessionParameter>
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
                <dx:ASPxCallbackPanel ID="cb_load" runat="server" Width="100%" ClientInstanceName="cb_load1" OnCallback="cb_load_Callback" style="display: none;">
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
                             
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>

            </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Save changes" />                  
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
                                   <%-- <div class="col-md-6 mb-3">
                                        <label for="namedoc" class="form-label">อีเมลล์</label>
                                        <dx:BootstrapTextBox ID="txtEmail" runat="server" Text='' ClientInstanceName="txtEmail"></dx:BootstrapTextBox>
                                      
                                    </div>--%>
                                    <div class="col-md-6 mb-3">
                                        <label for="namedoc" class="form-label">ชื่อ-นามสกุล</label>
                                        <dx:BootstrapTextBox ID="Name1" runat="server" Text='' ClientInstanceName="Name1"></dx:BootstrapTextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค" ControlToValidate="NamprojectTH" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                    </div>                                                                  
                               
                                                                  
                                     <div class="col-md-6 mb-3">
                                         <label for="namedoc" class="form-label">รหัสนิสิต</label>
                                         <dx:BootstrapTextBox ID="StudentID1" runat="server" Text='' ClientInstanceName="StudentID1"></dx:BootstrapTextBox>
                                         <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="กรุณาระบุชื่อโปรเจค(ภาษาอังกฤษ)" ControlToValidate="NameprojectEN" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
                                     </div>
                                        <div class="col-md-6 mb-3">
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
                    <asp:Button ID="Button6" class="btn btn-primary" runat="server" Text="Save changes" />                    
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
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" CssClass="gridview" DataKeyNames="Permission_ID">
                                    <Columns>
                                        <asp:BoundField DataField="Student_ID2" SortExpression="Student_ID2" HeaderText="รหัสนิสิต"></asp:BoundField>
                                        <asp:BoundField DataField="Student_Name2" SortExpression="Student_Name2" HeaderText="ชื่อ-นามสกุล"></asp:BoundField>
                                        <asp:BoundField DataField="Email2" SortExpression="Email2" HeaderText="อีเมลล์"></asp:BoundField>
                                        <asp:BoundField DataField="Mobile2" SortExpression="Mobile2" HeaderText="เบอร์โทรศัพท์"></asp:BoundField>
                                        <asp:TemplateField HeaderText="การกระทำ">
                                            <ItemTemplate>
                                                <asp:Button ID="DeleteButton" CssClass="btn btn-danger" runat="server" Text="ลบ" CommandName="Delete" CommandArgument='<%# Eval("Permission_ID") %>' OnClick="DeleteButton_Click" Width="100%" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <script>
                                    function redirectToPage(docID) {
                                        // Redirect to the desired page with DocID as a query parameter
                                        window.location.href = 'pushfriend.aspx?docID=' + docID;
                                    }
                                </script>
                                <script>
                                    function confirmDelete() {
                                        return confirm("คุณต้องการลบข้อมูลนี้หรือไม่?");
                                    }
                                </script>

                                <asp:SqlDataSource runat="server" ID="SqlDataSource5" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT MAS_DOC.Student_ID2, MAS_DOC.Student_Name2, MAS_DOC.Email2, MAS_DOC.Mobile2, MAS_DOC.DocID, Permission_TB.Permission_ID, Permission_TB.Email FROM MAS_DOC INNER JOIN Permission_TB ON MAS_DOC.DocID = Permission_TB.DocID WHERE (Permission_TB.Email = @Email)" DeleteCommand="DELETE FROM Permission_TB WHERE (Permission_ID = @Permission_ID)">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Permission_ID" Type="Int32"></asp:Parameter>
                                    </DeleteParameters>
                                    <SelectParameters>
                                        <asp:SessionParameter SessionField="Email" Name="Email"></asp:SessionParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button ID="Button4" runat="server" class="btn btn-primary" Text="Save changes" />
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
                                <div class="form-row">
                                    <div class="col-md-6 mb-3">
                                        <label for="firstname" class="form-label">อีเมลล์(คนที่2)</label>
                                        <dx:BootstrapTextBox ID="BootstrapTextBoxQR" runat="server" ClientInstanceName="BootstrapTextBoxQR" NullText="ใส่อีเมลล์">
                                            <ClientSideEvents KeyUp="function(s, e) {cbload.PerformCallback();}"></ClientSideEvents>
                                        </dx:BootstrapTextBox>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณาระอีเมลล์" ControlToValidate="BootstrapTextBoxQR" Font-Bold="True" CssClass="text-danger"></asp:RequiredFieldValidator>--%>
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
                              
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">CClose</button>
                    <asp:Button ID="Button2" runat="server" class="btn btn-primary" Text="Save changes" />

                </div>

            </div>

        </div>
        </div>
    <div class="modal fade bd-example-modal-lg" id="exampleModal10" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
              <%--  <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel10">ข้อมูลอาจารย์ที่ปรึกษา/กรรมการ</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>--%>

                <div class="modal-body">
                  
                    <div class="card-header white">
                        <div class="d-flex justify-content-between">                            
                           <div class="align-self-end float-right">
                            <ul class="nav nav-tabs card-header-tabs nav-material" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link show active" id="w2--tab1" data-toggle="tab" href="#w2-tab1" role="tab" aria-controls="tab1" aria-expanded="true" aria-selected="true">ข้อมูลอาจารย์</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" id="w2--tab2" data-toggle="tab" href="#w2-tab2" role="tab" aria-controls="tab2" aria-selected="false">เพิ่มอาจารย์</a>
                                </li>
                                <%--<li class="nav-item">
                                    <a class="nav-link" id="w2--tab3" data-toggle="tab" href="#w2-tab3" role="tab" aria-controls="tab3" aria-selected="false">Tab 3</a>
                                </li>--%>
                            </ul>
                        </div>
                        </div>
                    </div>
                    <div class="card-body no-p">
                        <div class="tab-content" id="v-pills-tabContent1">
                            <div class="tab-pane fade active show" id="w2-tab1" role="tabpanel" aria-labelledby="w2-tab1">
                                <br />
                                <dx:ASPxCallbackPanel ID="ASPxCallbackPanel7" runat="server" Width="100%" ClientInstanceName="cb_load10" OnCallback="cb_load10_Callback">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server">
                                            <dx:BootstrapGridView ID="BootstrapGridView2" runat="server" ClientInstanceName="BootstrapGridView2" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" KeyFieldName="Permission_ID">
                                                <Columns>
                                                    <dx:BootstrapGridViewTextColumn FieldName="DocID" VisibleIndex="1" Visible="False"></dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Name" VisibleIndex="2" Caption="ชื่อ-นามสกุล(อาจารย์)"></dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Email" VisibleIndex="3" Caption="อีเมลล์"></dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Urole_Permis" VisibleIndex="4" Visible="False"></dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Urole_TC" VisibleIndex="5" Caption="สถานะ(อาจารย์)"></dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn FieldName="Mobile" VisibleIndex="6" Caption="เบอร์โทรศัพท์(อาจารย์)"></dx:BootstrapGridViewTextColumn>
                                                    <dx:BootstrapGridViewTextColumn VisibleIndex="7">
                                                        <DataItemTemplate>
                                                            <asp:Button ID="DeleteButton" CssClass="btn btn-danger" runat="server" Text="ลบ" CommandName="Delete" CommandArgument='<%# Eval("Permission_ID") %>' OnClick="DeleteButton_Click" />
                                                           <asp:Button ID="SaveSubmit" runat="server" Text="ผ่าน" CssClass="btn btn-success" OnClick="SaveSubmit_Click" />
                                                        </DataItemTemplate>
                                                    </dx:BootstrapGridViewTextColumn>
                                                </Columns>
                                            </dx:BootstrapGridView>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource4" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Permission_TB.DocID, Permission_TB.Name, Permission_TB.Email, Permission_TB.Urole_TC, Permission_TB.Mobile, Permission_TB.Permission_ID, Permission_TB.Urole, Permission_TB.Submit_TC, MAS_DOC.Submit_TC AS ผลรวม FROM Permission_TB INNER JOIN MAS_DOC ON Permission_TB.DocID = MAS_DOC.DocID WHERE (Permission_TB.DocID = @DocID) AND (Permission_TB.Urole = N'2') ORDER BY Permission_TB.Urole_TC DESC" DeleteCommand="DELETE FROM Permission_TB WHERE (Permission_ID = @Permission_ID)">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Permission_ID"></asp:Parameter>
                                    </DeleteParameters>
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txt_ID" PropertyName="Text" Name="DocID"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                            <div class="tab-pane fade text-center p-5" id="w2-tab2" role="tabpanel" aria-labelledby="w2-tab2">                              
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
                                         </dx:PanelContent>
                                     </PanelCollection>
                                 </dx:ASPxCallbackPanel>

                             </div>
                          <%--  <div class="tab-pane fade text-center p-5" id="w2-tab3" role="tabpanel" aria-labelledby="w2-tab3">
                                <h4 class="card-title">Tab 3</h4>
                                <p class="card-text">With supporting text below as a natural lead-in to additional
                                    content.</p>
                                <a href="#" class="btn btn-primary">Go somewhere</a>
                            </div>--%>
                        </div>

                    </div>
                </div>
                          
                    
                <div class="modal-footer">
                     <%--<button type="button" class="btn btn-danger" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');$('#exampleModal11').modal('show');cb_load11.PerformCallback();" data-toggle="modal" data-target="#exampleModal11">เพิ่มอาจารย์</button>--%>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="hideInsertForm()">Close</button>
                    <asp:Button ID="Button8" runat="server" class="btn btn-primary" Text="Save changes" OnClick="Button8_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade bd-example-modal-lg" id="exampleModal11" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel11">เพิ่มอาจารย์</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                
                <div class="modal-footer">                 
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="hideInsertForm()">Close</button>
                    <asp:Button ID="Button9" runat="server" class="btn btn-primary" Text="Save changes" OnClick="Button8_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModal5" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
              <%--  <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel5">ไฟล์ที่อัพโหลด</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>--%>

                <div class="modal-body">
                    <div class="card-header white">
                        <div class="d-flex justify-content-between">                            
                           <div class="align-self-end float-right">
                            <ul class="nav nav-tabs card-header-tabs nav-material" role="tablist">
                                <li class="nav-item">
                                    <a class="nav-link show active" id="w2--tab3" data-toggle="tab" href="#w2-tab3" role="tab" aria-controls="tab1" aria-expanded="true" aria-selected="true">ไฟล์</a>
                                </li>
                            
                                <%--<li class="nav-item">
                                    <a class="nav-link" id="w2--tab3" data-toggle="tab" href="#w2-tab3" role="tab" aria-controls="tab3" aria-selected="false">Tab 3</a>
                                </li>--%>
                            </ul>
                        </div>
                        </div>
                    </div>
                    <div class="card-body no-p">
                        <div class="tab-content" id="v-pills-tabContent3">
                            <div class="tab-pane fade active show" id="w2-tab3" role="tabpanel" aria-labelledby="w2-tab1">
                                <br />
                               <dx:ASPxCallbackPanel ID="ASPxCallbackPanel4" runat="server"  ClientInstanceName="cb_load5" OnCallback="cb_load5_Callback">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                 <a href='EditUploadfile.aspx?DocID=<%= txt_ID.Text %>' class='btn btn-success'>แทนที่ไฟล์/เพิ่มไฟล์</a>
                                <br />
                                <br />

                                <script>
                                    function redirectToEditPage() {
                                        var docID = document.getElementById('<%= txt_ID.ClientID %>').value;
                                        window.location.href = 'EditUploadfile.aspx?DocID=' + docID;
                                    }
                                </script>
  
                                <dx:BootstrapGridView ID="BootstrapGridView5" runat="server" ClientInstanceName="BootstrapGridView5" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="Doc_File_ID">
                                    <Columns>
                                        <dx:BootstrapGridViewTextColumn FieldName="Doc_File_ID" ReadOnly="True" VisibleIndex="0" Visible="False">
                                            <SettingsEditForm Visible="False"></SettingsEditForm>
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="DocID" VisibleIndex="1" Visible="False"></dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn FieldName="Doc_File" VisibleIndex="3" Caption="ไฟล์" Visible="False"></dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn VisibleIndex="4">
                                            <DataItemTemplate>
                                                <asp:Button ID="DeleteButton" CssClass="btn btn-danger" runat="server" Text="ลบ" CommandName="Delete" CommandArgument='<%# Eval("Doc_File_ID") %>' Width="100%" OnClick="DeleteButtonFile_Click" />                                               
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewTextColumn>
                                        <dx:BootstrapGridViewTextColumn Caption="ไฟล์" VisibleIndex="2">
                                            <DataItemTemplate>
                                                <a href="Docs/<%#: Eval("Doc_File")%>"><%#: Eval("Doc_File")%></a>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewTextColumn>

                                    </Columns>
                                </dx:BootstrapGridView>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' DeleteCommand="DELETE FROM Doc_File WHERE (Doc_File_ID = @Doc_File_ID)" SelectCommand="SELECT Doc_File_ID, DocID, Doc_File FROM Doc_File WHERE (DocID = @DocID)">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Doc_File_ID"></asp:Parameter>
                                    </DeleteParameters>
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txt_ID" PropertyName="Text" Name="DocID"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>




                                <%-- <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT DocID, Doc_File, Doc_File_ID FROM Doc_File WHERE (DocID = @DocID)" DeleteCommand="DELETE FROM Doc_File WHERE (Doc_File_ID = @Doc_File_ID)">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Doc_File_ID"></asp:Parameter>
                                    </DeleteParameters>
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txt_ID" PropertyName="Text" Name="DocID"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>--%>
                            </dx:PanelContent>
                        </PanelCollection>
                               </dx:ASPxCallbackPanel>
                            </div>
                          
                        </div>
                         
                    </div>
                   

                </div>
                <div class="modal-footer">
                   <%-- <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <asp:Button ID="Button5" runat="server" class="btn btn-primary" Text="Save changes" />--%>

                </div>

            </div>


        </div>
    </div>
     <div class="modal fade bd-example-modal-lg" id="exampleModal6" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel6">ประวัติไฟล์</h5><button type="button" class="close" data-dismiss="modal" aria-label="Close">
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
                    <asp:Button ID="Button7" runat="server" class="btn btn-primary" Text="Save changes" />

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
                    <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="Save changes" />

                </div>

            </div>


        </div>
    </div>
  
</asp:Content>

