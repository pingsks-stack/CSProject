<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="MAS_Doc.aspx.vb" Inherits="production_version_Doc_All" %>
<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
   <!-- jQuery -->
<script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha384-KyZeWdGD8EJfyjgCqa9GeTQpExGtpryJ18/FSwTRIAbBCPBlS0mIEYxE3EAdgLGs" crossorigin="anonymous"></script>

<!-- Bootstrap -->
<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
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
    .hidden-textbox {
  display: none;
}
.card.no-b h4 {
        text-align: center;
    }
 .project-details {
        font-size: 17px;
        font-weight: bold;
    }
</style>
    <style>
            #codeEditor, #lineCounter {
                font-family: lucida console, courier new, courier, monospace;
                margin: 0;
                padding: 10px 0;
                height: 260px;
                border-radius: 0;
                resize: none;
                font-size: 16px;
                line-height: 1.2;
                outline: none;
                -moz-box-sizing: border-box;
                -webkit-box-sizing: border-box;
                box-sizing: border-box;
            }

                #lineCounter:focus-visible,
                #codeEditor:focus-visible {
                    outline: none;
                }

            #lineCounter {
                display: flex;
                border-color: transparent;
                overflow-y: hidden;
                text-align: right;
                box-shadow: none;
                color: #707070;
                background-color: #d8d8d8;
                position: absolute;
                width: 3.5rem;
                /* Determine appearance of line counter */
                background-color: #3E3D32;
                border-color: #3E3D32;
                color: #928869;
            }

            #codeEditor {
                padding-left: calc(3.5rem + 5px);
                width: 100%;
                /* Determine appearance of code editor */
                background-color: #272822;
                border-color: #272822;
                color: #ffffff;
            }
            .custom-textarea {
                width: 100%; /* กำหนดความกว้างเต็มแถว */
                height: 200px; /* กำหนดความสูง */
                padding: 5px; /* กำหนดระยะห่างของเนื้อหาภายใน */
                border: 1px solid #ccc; /* กำหนดเส้นขอบ */
                border-radius: 5px; /* กำหนดรูปร่างของขอบ */
                resize: vertical; /* กำหนดการปรับขนาดได้แค่แนวดิ่ง */
                font-family: Arial, sans-serif; /* กำหนดแบบอักษร */
                font-size: 14px; /* กำหนดขนาดตัวอักษร */
                color: #333; /* กำหนดสีตัวอักษร */
            }
            .my-textbox {
                /* กำหนดสไตล์ CSS ที่ต้องการให้กับ TextBox นี้ */
                border: 1px solid #ccc;
                padding: 7px;
                font-size: 15px;
                border-radius: 3px;
            }
            .no-border-button {
            border: none;
            background-color: transparent; /* ตั้งค่าสีพื้นหลังให้เป็นโปร่งใส */
            padding: 0; /* ตั้งค่าระยะห่างขอบในปุ่มเป็น 0 */
            color: #9f9f9f; /* ตั้งค่าสีข้อความ */
            cursor: pointer; /* เปลี่ยนรูปแบบเมาส์เป็นแถบบอกที่สามารถคลิกได้ */
            font-size: 20px;
        }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   

    <div>
        <header class="blue accent-3 relative">
            <div class="container-fluid">
                <div class="row p-t-b-10 ">
                    <div class="col">
                        <div class="pb-3">
                            <div class="image mr-3  float-left">
                               <img src="img/file.png" width="120" height="130" alt="File Icon" />
                                <%--<img class="user_avatar no-b no-p" src="assets/img/dummy/u6.png" alt="User Image">--%>
                            </div>
                            <div>
                                <div class="project-details">
                                <asp:DataList ID="DataListNamePJ" runat="server" DataSourceID="SqlDataSourceNamePJ">
                                    <ItemTemplate>
                                        ชื่อโปรเจค:
                                        <asp:Label Text='<%#: Eval("Doc_Name") %>' runat="server" ID="Doc_NameLabel" /><br />
                                        ชื่อโปรเจคภาษาอังกฤษ:
                                        <asp:Label Text='<%#: Eval("Doc_NameEN") %>' runat="server" ID="Doc_NameENLabel" /><br />
                                        ชั้นปีที่:
                                        <asp:Label Text='<%#: Eval("Class_No") %>' runat="server" ID="Label3" />
                                        ปีการศึกษา:
                                        <asp:Label Text='<%#: Eval("Class_Year_No") %>' runat="server" ID="Label4" /><br />
                                        ประเภทโครงงาน:
                                        <asp:Label Text='<%#: If(String.IsNullOrEmpty(Convert.ToString(Eval("Doc_Type"))), "ยังไม่ระบุ", Eval("Doc_Type")) %>' runat="server" ID="DocTypeLabel" /><br />
                                      <%--  Email:
                                        <asp:Label Text='<%#: Eval("Email") %>' runat="server" ID="EmailLabel" /><br />--%>
                                        สถานะโปรเจค:
                                        <asp:Label Text='<%#: Eval("Submit_TC") %>' runat="server" ID="Submit_TCLabel" />
                                        <button type="button" onclick="redirectN('<%# SafeOut.JsAttr(Eval("DocID")) %>')" class="no-border-button">....</button>
                                    </ItemTemplate>
                                </asp:DataList>
                                     <script type="text/javascript">
                                         function redirectN(docID) {
                                             docID = parseInt(docID);

                                             // สร้าง URL พร้อม QueryString สำหรับการส่งค่า DocID
                                             var url = "Pass_print.aspx?DocID=" + docID;

                                             // เปลี่ยน URL เพื่อ Redirect ไปยังหน้าอื่น
                                             window.location.href = url;
                                         }
                                     </script>      
                                    </div>
                                <asp:SqlDataSource runat="server" ID="SqlDataSourceNamePJ" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT MAS_DOC.Doc_Name, MAS_DOC.Doc_Type, MAS_DOC.Doc_NameEN, MAS_DOC.Email, MAS_DOC.Submit_TC, Permission_TB.Email AS Expr1, Permission_TB.DocID, MAS_DOC.Class_No, MAS_DOC.Class_Year_No FROM MAS_DOC INNER JOIN Permission_TB ON MAS_DOC.DocID = Permission_TB.DocID WHERE (Permission_TB.Email = @Email) AND (MAS_DOC.DocID = @DocID)">
                                    <SelectParameters>
                                        <asp:SessionParameter SessionField="Email" Name="Email"></asp:SessionParameter>
                                        <asp:QueryStringParameter QueryStringField="DocID" Name="DocID" Type="Int32"></asp:QueryStringParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:DataList ID="DataList2" runat="server" DataKeyField="DocID" DataSourceID="SqlDataSource1">
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <button type="button" id="myButton" class="btn btn-secondary" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');NamprojectTH.SetValue('<%# SafeOut.JsAttr(Eval("Doc_Name")) %>');NameprojectEN.SetValue('<%# SafeOut.JsAttr(Eval("Doc_NameEN")) %>');$('#exampleModal').modal('show');cb_load.PerformCallback();" data-toggle="modal" data-target="#exampleModal">แก้ไขชื่อโปรเจค</button>
                                                <button type="button" class="btn btn-warning" onclick="txt_ID.SetValue('<%# SafeOut.JsAttr(Eval("DocID")) %>');$('#exampleModal7').modal('show');cb_load7.PerformCallback();" data-toggle="modal" data-target="#exampleModal7">ประวัติการแก้ชื่อโปรเจค</button>
                                            </tr>
                                        </tbody>
                                        </table>                                
                                    </ItemTemplate>
                                </asp:DataList>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT MAS_DOC.DocID, MAS_DOC.Doc_Name, MAS_DOC.Doc_NameEN, Permission_TB.Email FROM MAS_DOC INNER JOIN Permission_TB ON MAS_DOC.DocID = Permission_TB.DocID WHERE (Permission_TB.Email = @Email) AND (MAS_DOC.DocID = @DocID)">
                                    <SelectParameters>
                                        <asp:SessionParameter SessionField="Email" Name="Email"></asp:SessionParameter>
                                        <asp:QueryStringParameter QueryStringField="DocID" Name="DocID" Type="Int32"></asp:QueryStringParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="row">
                  <ul class="nav nav-material nav-material-black responsive-tab" id="v-pills-tab" role="tablist">
                      <li>
                          <a class="nav-link active" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="true"><i class="icon icon-home2"></i>Home</a>
                      </li>
                      <li>
                          <a class="nav-link" id="v-pills-payments-tab" data-toggle="pill" href="#v-pills-payments" role="tab" aria-controls="v-pills-payments" aria-selected="false"><i class="icon icon-money-1"></i>เพิ่มข้อมูลคู่โปรเจค</a>
                      </li>
                      <li>
                          <a class="nav-link" id="v-pills-timeline-tab" data-toggle="pill" href="#v-pills-timeline" role="tab" aria-controls="v-pills-timeline" aria-selected="false"><i class="icon icon-cog"></i>เพิ่มข้อมูลอาจารย์</a>
                      </li>
                      <%--<li>
                          <a class="nav-link" id="v-pills-settings-tab" data-toggle="pill" href="#v-pills-settings" role="tab" aria-controls="v-pills-settings" aria-selected="false"><i class="icon icon-cog"></i>Edit Profile</a>
                      </li>--%>
                  </ul>
              </div>

            </div>
        </header>

    <%-- ประเภทโครงงาน: ให้สมาชิกของโครงงานเลือก/แก้ไขได้ --%>
    <div class="tw-mx-3 tw-my-4 tw-flex tw-flex-wrap tw-items-end tw-gap-3 tw-rounded-2xl tw-border tw-border-line tw-bg-surface tw-p-4 tw-shadow-sm">
        <div class="tw-min-w-[220px] tw-flex-1">
            <label for="ddlDocType" class="tw-mb-1 tw-block tw-text-sm tw-font-medium tw-text-ink">ประเภทโครงงาน</label>
            <asp:DropDownList ID="ddlDocType" runat="server" CssClass="form-control" DataSourceID="SqlDocType"
                DataTextField="Doc_Type" DataValueField="Doc_Type" AppendDataBoundItems="true">
                <asp:ListItem Text="-- ยังไม่ระบุ --" Value=""></asp:ListItem>
            </asp:DropDownList>
            <asp:SqlDataSource runat="server" ID="SqlDocType" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>'
                SelectCommand="SELECT Doc_Type FROM DocTypeTB ORDER BY CASE WHEN Doc_Type = N'Other' THEN 1 ELSE 0 END, TypeID"></asp:SqlDataSource>
        </div>
        <asp:Button ID="btnSaveDocType" runat="server" Text="บันทึกประเภท" CssClass="btn btn-primary" CausesValidation="false" />
    </div>

        <div class="container-fluid animatedParent animateOnce my-3">
            <div class="animated fadeInUpShort go">
           <div class="tab-content" id="v-pills-tabContent">
               <div class="tab-pane fade active show" id="v-pills-home" role="tabpanel" aria-labelledby="v-pills-home-tab">
                   <div class="row">
                       
                       <div class="col-md-8">                        
                           <div class="row my-3">
                               <!-- bar charts group -->
                               <div class="col-md-12">
                                   <div class="card">
                                       <div class="card-header white">
                                           <h6>ข้อมูลเจ้าของโปรเจค</h6>
                                       </div>
                                       <div class="card-body">
                                           <button type="button" <%# get_disabled(Eval("DocID")) %> class="btn btn-warning" onclick="redirectToHistory()">ประวัติการลบ/เปลี่ยนคู่โปรเจค</button>

                                            <script>
                                                function redirectToHistory() {
                                                    var docID = '<%= SafeOut.Js(Request.QueryString("DocID")) %>';
                                                    txt_ID.SetValue(docID);
                                                    window.location.href = 'History_ChangF.aspx?DocID=' + docID;
                                                }
                                            </script>

                                           <hr />
                                           <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Permission_ID" DataSourceID="SqlDataSource3" CssClass="gridview">
                                               <Columns>
                                                   <asp:BoundField DataField="Permission_ID" HeaderText="Permission_ID" ReadOnly="True" InsertVisible="False" SortExpression="Permission_ID" Visible="False"></asp:BoundField>
                                                   <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                                                   <asp:BoundField DataField="Name" HeaderText="ชื่อ-นามสกุล" SortExpression="Name"></asp:BoundField>
                                                   <asp:BoundField DataField="Email" HeaderText="อีเมลล์" SortExpression="Email"></asp:BoundField>
                                                   <asp:BoundField DataField="Mobile" HeaderText="เบอร์โทรศัพท์" SortExpression="Mobile"></asp:BoundField>
                                                   <asp:BoundField DataField="Student_ID" HeaderText="รหัสนิสิต" SortExpression="Student_ID"></asp:BoundField>
                                                   <asp:TemplateField HeaderText="การกระทำ">
                                                       <ItemTemplate>                                                          
                                                           <button type="button" onclick="redirectToChangeFriend('<%# SafeOut.JsAttr(Eval("Permission_ID")) %>', '<%# SafeOut.JsAttr(Eval("DocID")) %>', '<%# SafeOut.JsAttr(Eval("Email")) %>')" class="btn btn-primary">ลบ/เปลี่ยนคู่โปรเจค/ข้อมูล</button>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                               </Columns>
                                           </asp:GridView>
                                           <script>
                                               function redirectToChangeFriend(permissionID, docID, emailInGrid) {
                                                  
                                                   var userEmail = '<%= Session("Email") %>'; 

                                                   if (emailInGrid === userEmail) {
                                                       alert('คุณไม่สามารถทำรายการของตนเองได้');
                                                       var button = event.target;
                                                       button.style.backgroundColor = 'grey';
                                                       button.style.cursor = 'not-allowed';
                                                       button.disabled = true;
                                                   } else {
                                                       var url = 'Change_Friend.aspx?Permission_ID=' + permissionID + '&DocID=' + docID;
                                                       window.location.href = url;
                                                   }
                                               }
                                           </script>

                                           <asp:SqlDataSource runat="server" ID="SqlDataSource3" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Permission_TB.Permission_ID, Permission_TB.Name, Permission_TB.Email, Permission_TB.Mobile, Permission_TB.Student_ID, MAS_DOC.DocID, MAS_DOC.Student_ID2, MAS_DOC.Student_Name2, MAS_DOC.Email2, MAS_DOC.Mobile2, Permission_TB.Urole FROM MAS_DOC INNER JOIN Permission_TB ON MAS_DOC.DocID = Permission_TB.DocID WHERE (Permission_TB.DocID = @DocID) AND (Permission_TB.Urole = N'1')">
                                               <SelectParameters>
                                                   <asp:QueryStringParameter Name="DocID" QueryStringField="docID" Type="String" />
                                               </SelectParameters>
                                           </asp:SqlDataSource>

                                       </div>
                                   </div>
                               </div>
                               <!-- /bar charts group -->


                           </div>
                          
                           <div class="row">
                               <div class="col-md-12">
                                   <div class="card">
                                       <div class="card-header white">
                                           <h6>ข้อมูลอาจารย์</h6>
                                       </div>
                                       <div class="card-body">
                                          
                                           <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="Permission_ID" DataSourceID="SqlDataSourceInforAJ" CssClass="gridview">
                                               <Columns>
                                                   <asp:BoundField DataField="Permission_ID" HeaderText="Permission_ID" ReadOnly="True" InsertVisible="False" SortExpression="Permission_ID" Visible="False"></asp:BoundField>
                                                   <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                                                   <asp:BoundField DataField="Name" HeaderText="ชื่อ-นามสกุล" SortExpression="Name"></asp:BoundField>
                                                   <asp:BoundField DataField="Email" HeaderText="อีเมลล์" SortExpression="Email" Visible="False"></asp:BoundField>
                                                   <asp:BoundField DataField="Mobile" HeaderText="เบอร์โทรศัพท์" SortExpression="Mobile"></asp:BoundField>
                                                   <asp:BoundField DataField="Urole" HeaderText="Urole" SortExpression="Urole" Visible="False"></asp:BoundField>
                                                   <asp:BoundField DataField="Urole_TC" HeaderText="สถานะ" SortExpression="Urole_TC"></asp:BoundField>
                                                   <asp:TemplateField HeaderText="การกระทำ">
                                                       <ItemTemplate>
                                                           <asp:Button ID="DeleteButton" CssClass="btn btn-danger" runat="server" Text="ลบ" CommandName="Delete" CommandArgument='<%# Eval("Permission_ID") %>' OnClick="DeleteAJButton_Click" Width="100%" OnClientClick="return confirmDelete();" />
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="การกระทำ">
                                                       <ItemTemplate>                                                          
                                                           <button type="button" onclick="redirectToCPAJ('<%# SafeOut.JsAttr(Eval("Permission_ID")) %>', '<%# SafeOut.JsAttr(Eval("DocID")) %>')" class="btn btn-primary">คอมเม้นต์อาจารย์</button>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                               </Columns>
                                           </asp:GridView>
                                            <script>
                                                function confirmDelete() {
                                                    return confirm("คุณต้องการลบข้อมูลนี้หรือไม่?");
                                                }
                                            </script>
                                           <script>
                                               function redirectToCPAJ(permissionID, docID) {
                                                   var url = 'CommentAJ.aspx?Permission_ID=' + permissionID + '&DocID=' + docID;
                                                   window.location.href = url;
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
                         <div class="col-md-4">                        
                           <div class="row my-3">
                               <!-- bar charts group -->
                               <div class="col-md-12">
                                   <div class="card">
                                       <div class="card-header white">
                                           <h6>ไฟล์/ลอคไฟล์</h6>
                                       </div>
                                       <div class="card-body">
                                           
                                           <button type="button" class="btn btn-secondary" onclick="redirectToOtherPage();">แทนที่ไฟล์/เพิ่มไฟล์</button>
                                           <button type="button" class="btn btn-warning" onclick="var docID = '<%= SafeOut.JsAttr(Request.QueryString("DocID")) %>'; txt_ID.SetValue(docID); $('#exampleModal6').modal('show'); cb_load6.PerformCallback();" data-toggle="modal" data-target="#exampleModal6">ประวัติไฟล์</button>                                        
                                           <dx:BootstrapTextBox ID="txt_ID" runat="server" Width="170px" ClientInstanceName="txt_ID" CssClasses-Control="hidden-textbox"></dx:BootstrapTextBox> 
                                           <br />
                                          <script>
                                              function redirectToOtherPage() {
                                                  var docID = '<%= SafeOut.Js(Request.QueryString("DocID")) %>';
                                                  txt_ID.SetValue(docID);
                                                  window.location.href = 'EditUploadfile.aspx?DocID=' + docID;
                                              }
                                          </script>
                                          <hr />
                                           <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="Doc_File_ID" DataSourceID="SqlDataSourceFile" CssClass="gridview">
                                               <Columns>
                                                   <asp:BoundField DataField="Doc_File_ID" HeaderText="Doc_File_ID" ReadOnly="True" InsertVisible="False" SortExpression="Doc_File_ID" Visible="False"></asp:BoundField>
                                                   <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                                                   <asp:BoundField DataField="Doc_File" HeaderText="ไฟล์" SortExpression="Doc_File" Visible="False"></asp:BoundField>                                                                                                                                                   
                                                   <asp:TemplateField HeaderText="ไฟล์">
                                                       <ItemTemplate>
                                                            <a href="Docs/<%#: Eval("Doc_File")%>"><%#: Eval("Doc_File")%></a>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>   
                                                   <asp:TemplateField HeaderText="ลบ">
                                                       <ItemTemplate>
                                                           <asp:Button ID="DeleteButton" CssClass="btn btn-danger" runat="server" Text="ลบ" CommandName="Delete" CommandArgument='<%# Eval("Doc_File_ID") %>' Width="100%" OnClick="DeleteButtonFile_Click" OnClientClick="return confirmfileDelete();" />
                                                       </ItemTemplate>
                                                   </asp:TemplateField>                                                  
                                               </Columns>
                                           </asp:GridView>
                                           <%--<asp:Label ID="lblMissingChapters" runat="server" Text=""></asp:Label>--%>

                                            <script>
                                                function confirmfileDelete() {
                                                    return confirm("คุณต้องการลบข้อมูลนี้หรือไม่?");
                                                }
                                            </script>                                         
                                           <asp:SqlDataSource runat="server" ID="SqlDataSourceFile" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Doc_File_ID, DocID, Doc_File, StatusMessage FROM Doc_File WHERE (DocID = @DocID) ORDER BY Doc_File">
                                               <SelectParameters>
                                                   <asp:QueryStringParameter QueryStringField="docID" Name="DocID"></asp:QueryStringParameter>
                                               </SelectParameters>
                                           </asp:SqlDataSource>
                                       </div>
                                   </div>
                               </div>
                               <!-- /bar charts group -->
                           </div>                                                    
                       </div>

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
                            <button type="button" class="btn btn-secondary" onclick="redirectToOtherPagefunc();">เพิ่ม Source Code</button>
                            <button type="button" class="btn btn-warning" onclick="var docID = '<%= SafeOut.JsAttr(Request.QueryString("DocID")) %>'; txt_ID.SetValue(docID); $('#exampleModal4').modal('show'); cb_load15.PerformCallback();" data-toggle="modal" data-target="#exampleModal4">ประวัติ Source Code</button>
                            <%--<dx:BootstrapTextBox ID="BootstrapTextBox1" runat="server" Width="170px" ClientInstanceName="txt_ID" CssClasses-Control="hidden-textbox"></dx:BootstrapTextBox>--%>
                            <br />
                            <script>
                                function redirectToOtherPagefunc() {
                                    var docID = '<%= SafeOut.Js(Request.QueryString("DocID")) %>';
                                    txt_ID.SetValue(docID);
                                    window.location.href = 'CodeEdit.aspx?DocID=' + docID;
                                }
                            </script>
                            <hr />
                            <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource5" Width="100%" DataKeyField="Doc_List_ID">
                                <ItemTemplate>
                                    <textarea id='lineCounter' wrap='off' readonly></textarea>
                                    <textarea id='codeEditor' wrap='off'><%#: Eval("Doc_List_Code")%></textarea>
                                    <tr>
                                        <td><b>ชื่อภาษาโปรแกรม :</b>
                                            <asp:Label ID="Label1" runat="server" Text='<%#: Eval("Doc_List_Type") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>ชื่อฟังก์ชัน :</b>
                                            <asp:Label ID="Label2" runat="server" Text='<%#: Eval("Doc_Functions_Code") %>'></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><b>การกระทำ :</b>
                                         <asp:Button ID="btnDelete" CssClass="btn btn-danger" runat="server" Text="ลบ" OnClick="btnDelete_Click" CommandArgument='<%# Eval("Doc_List_ID") %>' OnClientClick="return confirmSouDelete();"/>
                                        </td>
                                    </tr>                                 
                                </ItemTemplate>
                            </asp:DataList>
                             <script>
                                 function confirmSouDelete() {
                                     return confirm("คุณต้องการลบข้อมูลนี้หรือไม่?");
                                 }
                             </script>
                            <asp:SqlDataSource runat="server" ID="SqlDataSource5" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Doc_Date_Save, Doc_List_Code, Doc_Functions_Code, DocID, Doc_List_Type, Doc_List_ID FROM Doc_List_TB WHERE (DocID = @DocID)">
                                <SelectParameters>
                                    <asp:QueryStringParameter QueryStringField="DocID" Name="DocID"></asp:QueryStringParameter>
                                </SelectParameters>
                            </asp:SqlDataSource>
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
    


                   </div>
               </div>
               <div class="tab-pane fade" id="v-pills-payments" role="tabpanel" aria-labelledby="v-pills-payments-tab">
                   <div class="row">
                       <div class="col-md-12">
                           <div class="card no-b">
                               <br />
                               <h4><span>เพิ่มข้อมูลคู่โปรเจค</span></h4>
                               <hr />
                               <div class="container">
                                   <div class="row">
                                       <div class="col-md-12">
                                           <div class="card col-md-12">
                                               <div class="card-body">
                                                   
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
                                                                   <hr />
                                                                           <h5>ข้อมูลคู่โปรเจคคนเก่า</h5>
                                                                           <br />
                                                                   <asp:GridView ID="GridView7" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataSourceID="SqlDataSource9">
                                                                       <Columns>
                                                                           <asp:BoundField DataField="Status_ID" HeaderText="Status_ID" ReadOnly="True" InsertVisible="False" SortExpression="Status_ID" Visible="False"></asp:BoundField>
                                                                           <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                                                                           <asp:BoundField DataField="Permission_ID" HeaderText="Permission_ID" SortExpression="Permission_ID" Visible="False"></asp:BoundField>
                                                                           <asp:BoundField DataField="Last_Post_Name" HeaderText="ชื่อ-นามสกุล" SortExpression="Last_Post_Name"></asp:BoundField>
                                                                           <asp:BoundField DataField="Last_Email" HeaderText="อีเมลล์" SortExpression="Last_Email"></asp:BoundField>
                                                                           <asp:BoundField DataField="Last_Mobile" HeaderText="เบอร์โทรศัพท์" SortExpression="Last_Mobile"></asp:BoundField>
                                                                           <asp:BoundField DataField="Last_StudentID" HeaderText="รหัสนิสิต" SortExpression="Last_StudentID"></asp:BoundField>
                                                                           <asp:BoundField DataField="Status_Change" HeaderText="Status_Change" SortExpression="Status_Change" Visible="False"></asp:BoundField>
                                                                           <asp:BoundField DataField="Post_Name" HeaderText="โดย" SortExpression="Post_Name"></asp:BoundField>
                                                                       </Columns>
                                                                               <EmptyDataTemplate>
                                                                                   <div style="text-align: center;">
                                                                                       ไม่มีข้อมูลการเปลี่ยนแปลงคู่โปรเจค
                                                                                   </div>
                                                                               </EmptyDataTemplate>
                                                                           </asp:GridView>
                                                                   <asp:SqlDataSource runat="server" ID="SqlDataSource9" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Status_ID, DocID, Permission_ID, Last_Post_Name, Last_Email, Last_Mobile, Last_StudentID, Status_Change, Post_Name FROM Status_Change_TB WHERE (DocID = @DocID) AND (Status_Change = N'1')">
                                                                               <SelectParameters>
                                                                                   <asp:QueryStringParameter QueryStringField="DocID" Name="DocID"></asp:QueryStringParameter>

                                                                               </SelectParameters>
                                                                           </asp:SqlDataSource>

                                                               </dx:PanelContent>
                                                           </PanelCollection>
                                                       </dx:ASPxCallbackPanel>
                                                   </div>
                                               </div>
                                           </div>
                                       </div>
                                   </div>
                               </div>


                           </div>
                       </div>
                   </div>

                   <div id="extwaiokist" style="display:none" v="nipgg" q="cd28187d" c="76.06" i="9" u="0.560" s="06182305" sg="svr_04262315-ga_06182305-bai_undefined" d="1" w="false" e="" a="3" m="BMe=" vn="9adfy"><div id="extwaigglbit" style="display:none" v="nipgg" q="cd28187d" c="76.06" i="9" u="0.560" s="06182305" sg="svr_04262315-ga_06182305-bai_undefined" d="1" w="false" e="" a="3" m="BMe="></div></div></div>
               <div class="tab-pane fade" id="v-pills-timeline" role="tabpanel" aria-labelledby="v-pills-timeline-tab">

                   <div class="row">
                       <div class="col-md-12">
                           <div class="card no-b">
                               <br />
                               <h4><span>เพิ่มข้อมูลอาจารย์</span></h4>
                               <hr />
                               <div class="container">
                                   <div class="row">
                                       <div class="col-md-12">
                                           <div class="card col-md-12">
                                               <div class="card-body">
                                                   
                                                   <div class="form-row">
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
                                                                    <hr />
                                                                           <h5>ข้อมูลคำร้องขอแต่งตั้งอาจารย์</h5>
                                                                           <br />
                                                                           <asp:GridView ID="GridView6" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataKeyNames="SubAddAj_ID" DataSourceID="SqlDataSource8">
                                                                               <Columns>
                                                                                   <asp:BoundField DataField="SubAddAj_ID" HeaderText="SubAddAj_ID" ReadOnly="True" InsertVisible="False" SortExpression="SubAddAj_ID" Visible="False"></asp:BoundField>
                                                                                   <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                                                                                   <asp:BoundField DataField="Name" HeaderText="ชื่อ-นามสกุลอาจารย์" SortExpression="Name"></asp:BoundField>
                                                                                   <asp:BoundField DataField="Email" HeaderText="อีเมลล์" SortExpression="Email"></asp:BoundField>
                                                                                   <asp:BoundField DataField="Mobile" HeaderText="เบอร์โทรศัพท์" SortExpression="Mobile"></asp:BoundField>
                                                                                   <asp:BoundField DataField="Urole" HeaderText="Urole" SortExpression="Urole" Visible="False"></asp:BoundField>                                                                                 
                                                                                   <asp:BoundField DataField="Status" HeaderText="สถานะ" SortExpression="Status"></asp:BoundField>
                                                                                   <asp:BoundField DataField="Date_Confirm" HeaderText="วันเวลา" SortExpression="Date_Confirm" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                                                               </Columns>
                                                                               <EmptyDataTemplate>
                                                                                   <div style="text-align: center;">
                                                                                       ไม่มีข้อมูลคำร้องขอแต่งตั้งอาจารย์ในขณะนี้
                                                                                   </div>
                                                                               </EmptyDataTemplate>
                                                                           </asp:GridView>
                                                                   <asp:SqlDataSource runat="server" ID="SqlDataSource8" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT SubAddAj_ID, DocID, Name, Email, Mobile, Urole, Urole_TC, Status, Date_Confirm FROM SubAdd_AJ_TB WHERE (DocID = @DocID)">
                                                                               <SelectParameters>
                                                                                   <asp:QueryStringParameter QueryStringField="DocID" Name="DocID"></asp:QueryStringParameter>

                                                                               </SelectParameters>
                                                                           </asp:SqlDataSource>

                                                               </dx:PanelContent>
                                                           </PanelCollection>
                                                       </dx:ASPxCallbackPanel>

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
                                         <dx:BootstrapGridViewTextColumn Caption="ไฟล์" VisibleIndex="3">
                                            <DataItemTemplate>
                                                <a href="Docs/<%#: Eval("Doc_File")%>"><%#: Eval("Doc_File")%></a>
                                            </DataItemTemplate>
                                        </dx:BootstrapGridViewTextColumn>
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
    <div class="modal fade bd-example-modal-lg" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">แก้ไขชื่อโปรเจค</h5>
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
                            <hr />
                            <h5>ข้อมูลการเปลี่ยนแปลงและสถานะ</h5>
                            <br />
                            <asp:GridView ID="GridView5" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataKeyNames="SubNPJ_ID" DataSourceID="SqlDataSource7">
                                <Columns>
                                    <asp:BoundField DataField="SubNamePJ_ID" HeaderText="SubNamePJ_ID" ReadOnly="True" InsertVisible="False" SortExpression="SubNPJ_ID" Visible="False"></asp:BoundField>
                                    <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                                    <asp:BoundField DataField="Doc_Name" HeaderText="ชื่อโปรเจค" SortExpression="Doc_Name"></asp:BoundField>
                                    <asp:BoundField DataField="Doc_NameEN" HeaderText="ชื่อโปรเจค(อังกฤษ)" SortExpression="Doc_NameEN"></asp:BoundField>
                                    <asp:BoundField DataField="Post_Name" HeaderText="โดย" SortExpression="Post_Name"></asp:BoundField>
                                    <asp:BoundField DataField="Date_Save" HeaderText="วัน-เวลา" SortExpression="Date_Save"></asp:BoundField>
                                    <asp:BoundField DataField="Action" HeaderText="สถานะ" SortExpression="Action"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div style="text-align: center;">
                                        ไม่มีข้อมูลการร้องขอการเปลี่ยนแปลงชื่อโปรเจคในขณะนี้
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <asp:SqlDataSource runat="server" ID="SqlDataSource7" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT SubNPJ_ID, DocID, Doc_Name, Doc_NameEN, Post_Name, Date_Save, Action FROM Sub_NPJ_TB WHERE (DocID = @DocID)">
                                <SelectParameters>
                                    <asp:QueryStringParameter QueryStringField="DocID" Name="DocID"></asp:QueryStringParameter>

                                </SelectParameters>
                            </asp:SqlDataSource>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
                    
            </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">ปิด</button>
                    <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="บันทึก" />                  
                </div>
        </div>
    </div>
    </div>
   <div class="modal fade bd-example-modal-lg" id="exampleModal7" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel7">ประวัติการแก้ไขชื่อโปรเจค</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel6" runat="server" Width="100%" ClientInstanceName="cb_load7" OnCallback="cb_load7_Callback">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                <dx:BootstrapGridView ID="BootstrapGridView4" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" ClientInstanceName='<div class="modal fade bd-example-modal-lg" id="exampleModal6" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">'>
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
                                <asp:SqlDataSource runat="server" ID="SqlDataSource4" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Doc_Name, Doc_NameEN, Action, DocID, Post_Name, Doc_dt_Save FROM MAS_DOC_LOG WHERE (DocID = @DocID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txt_ID" PropertyName="Text" Name="DocID"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">ปิด</button>
                    <asp:Button ID="Button3" runat="server" class="btn btn-primary" Text="บันทึก" />

                </div>

            </div>


        </div>
    </div>
  
    <%--textarea--%>
   
    <div class="modal fade bd-example-modal-lg" id="exampleModal4" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel3">ประวัติ Source Code</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <dx:ASPxCallbackPanel ID="ASPxCallbackPanel3" runat="server" Width="100%" ClientInstanceName="cb_load15" OnCallback="cb_load15_Callback">
                        <PanelCollection>
                            <dx:PanelContent runat="server">
                                
                                 <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" DataKeyNames="Doc_List_Log_ID" DataSourceID="SqlDataSource6" CssClass="gridview" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="Doc_List_Log_ID" HeaderText="Doc_List_Log_ID" ReadOnly="True" InsertVisible="False" SortExpression="Doc_List_Log_ID" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="Doc_List_ID" HeaderText="Doc_List_ID" SortExpression="Doc_List_ID" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                                        <asp:BoundField DataField="Doc_List_Type" HeaderText="ภาษา" SortExpression="Doc_List_Type"></asp:BoundField>
                                        <asp:BoundField DataField="Doc_Functions_Code" HeaderText="ชื่อฟังก์ชัน" SortExpression="Doc_Functions_Code"></asp:BoundField>
                                        <asp:BoundField DataField="Action" HeaderText="การกระทำ" SortExpression="Action"></asp:BoundField>
                                        <asp:BoundField DataField="Post_Name" HeaderText="โดย" SortExpression="Post_Name"></asp:BoundField>
                                        <asp:BoundField DataField="Doc_Date_Save" HeaderText="วัน-เวลา" SortExpression="Doc_Date_Save"></asp:BoundField>                                                                              
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource6" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Doc_List_Log_ID, Doc_List_ID, DocID, Doc_List_Type, Doc_Functions_Code, Doc_Date_Save, Post_Name, Action FROM Doc_List_Log WHERE (DocID = @DocID)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="txt_ID" PropertyName="Text" Name="DocID"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                              
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">CClose</button>
                    <asp:Button ID="Button5" runat="server" class="btn btn-primary" Text="Save changes" />

                </div>

            </div>

        </div>
        </div>

</asp:Content>

