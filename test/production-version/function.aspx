<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="function.aspx.vb" Inherits="production_version_function" %>

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
    <link href="https://fonts.googleapis.com/css?family=Roboto:100,100i,300,300i,400,400i,500,500i,700,700i,900,900i&display=swap" rel="stylesheet">
        <link href="https://fonts.googleapis.com/css?family=Material+Icons|Material+Icons+Outlined|Material+Icons+Two+Tone|Material+Icons+Round|Material+Icons+Sharp" rel="stylesheet">
        <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet">
        <link href="../../assets/plugins/font-awesome/css/all.min.css" rel="stylesheet">
        <link href="../../assets/plugins/waves/waves.min.css" rel="stylesheet">

    <link href="assets/codemirror/lib/codemirror.css" rel="stylesheet" />
    <link href="assets/codemirror/theme/material.css" rel="stylesheet" />
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
        </style>

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
                            <a href="Main_Doc.aspx" class="btn btn-secondary mt-3">กลับไปยังหน้าเอกสารของฉัน</a>                                                       
                            <hr />
                            <h5 class="card-title">อัพโหลดโค้ด</h5>
                            <textarea id="TextArea1" runat="server" class="custom-textarea" cols="20" rows="2"></textarea>

                            <hr />
                            <asp:Label ID="Label3" runat="server" Text="ชื่อภาษาโปรแกรม"></asp:Label>
                            <br />
                            <asp:TextBox ID="TextBox1" runat="server" Width="40%" CssClass="my-textbox"></asp:TextBox>
                            <asp:Button ID="Button2" runat="server" Text="ตรวจสอบภาษา" OnClick="Button2_Click" CssClass="btn btn-primary" />
                            <br />
                            <br />
                            <asp:Label ID="name_function" runat="server" Text="ชื่อฟังก์ชัน"></asp:Label>
                            <br />
                            <asp:TextBox ID="TextBox2" runat="server"  Width="43%" CssClass="my-textbox"></asp:TextBox>
                              <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="บันทึก" data-bs-toggle="modal" data-bs-target="#modalCenter1" />
                           <%-- <dx:BootstrapMemo ID="Text_Code" runat="server" ClientIDMode="Static" ClientInstanceName="Text_Code" Width="100%" Rows="5">
                                <ClientSideEvents KeyUp="function(s, e) {Detect_Code();}"></ClientSideEvents>
                            </dx:BootstrapMemo>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="กรุณาระบุ Source Code" ControlToValidate="Text_Code" CssClass="text-danger"></asp:RequiredFieldValidator>
                            <br />
                                      
                            <dx:ASPxCallback ID="Detect_Callback" runat="server" ClientInstanceName="Detect_Callback"></dx:ASPxCallback>
                           
                            
                            <dx:BootstrapTextBox ID="txtname" runat="server" ClientInstanceName="txtname" NullText="Code Language">
                            </dx:BootstrapTextBox>
                            <br />
                            <asp:Label ID="name_function" runat="server" Text="Input name Functions."></asp:Label>
                            <dx:BootstrapTextBox ID="txtft" runat="server" ></dx:BootstrapTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณาระบุ Functions" ControlToValidate="txtft" CssClass="text-danger"></asp:RequiredFieldValidator>
                            <br />--%>
                          
                        </div>
                    </div>
                </div>
                <div class="col-md-7">
                    <div class="card">
                        <div class="card-body">
                            <br />
                            <h2 class="card-title">โค้ดที่บันทึก</h2>
                            <hr />
                            <asp:DataList ID="DataList_Show_DocList" runat="server" DataSourceID="SqlDataShow_DocList" Width="100%">
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
                                   <%-- <textarea id="Source_Code"><%#: Eval("Doc_List_Code")%></textarea>--%>                                   
                                   <%-- <table class="nav-justified">
                                        <tr>
                                            <td><b>Source Code :</b></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <textarea id="Source_Code"><%#: Eval("Doc_List_Code")%></textarea>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><b>Code Language :</b>
                                                <asp:Label ID="Label1" runat="server" Text='<%#: Eval("Doc_List_Type") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><b>Function Code :</b>
                                                <asp:Label ID="Label2" runat="server" Text='<%#: Eval("Doc_Functions_Code") %>'></asp:Label>
                                            </td>
                                        </tr>

                                    </table>--%>
                                                                                          
                                  <%--  Doc_Date_Save:                                   
                                    <asp:Label Text='<%#: Eval("Doc_Date_Save") %>' runat="server" ID="Doc_Date_SaveLabel" /><br />
                                    Doc_List_Code:
                                    <asp:Label Text='<%#: Eval("Doc_List_Code") %>' runat="server" ID="Doc_List_CodeLabel" /><br />
                                    Doc_Functions_Code:
                                    <asp:Label Text='<%#: Eval("Doc_Functions_Code") %>' runat="server" ID="Doc_Functions_CodeLabel" /><br />
                                    DocID:
                                    <asp:Label Text='<%#: Eval("DocID") %>' runat="server" ID="DocIDLabel" /><br />
                                    Doc_List_Type:
                                    <asp:Label Text='<%#: Eval("Doc_List_Type") %>' runat="server" ID="Doc_List_TypeLabel" /><br />
                                    <br />--%>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <%-- <script>
                             $(document).ready(function () {
                                 var editor_one = CodeMirror.fromTextArea(document.getElementById("Source_Code"), {
                                     lineNumbers: true,
                                     matchBrackets: true,
                                     styleActiveLine: true,
                                     theme: "material"
                                 }).setSize("820", 300);
                             });
                         </script>  --%>
                        <asp:SqlDataSource runat="server" ID="SqlDataShow_DocList" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Doc_Date_Save, Doc_List_Code, Doc_Functions_Code, DocID, Doc_List_Type FROM Doc_List_TB WHERE (DocID = @DocID)">
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


     <script src="../../assets/plugins/jquery/jquery-3.4.1.min.js"></script>
        <script src="../../assets/plugins/bootstrap/popper.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/waves/waves.min.js"></script>
        <script src="../../assets/plugins/jquery-slimscroll/jquery.slimscroll.min.js"></script>

       
    <script src="assets/codemirror/mode/javascript/javascript.js"></script>
    <script src="assets/codemirror/lib/codemirror.js"></script>
    
    <script src="../../assets/js/alpha.min.js"></script>
   
   <script>
       var codeEditor = document.getElementById('codeEditor');
       var lineCounter = document.getElementById('lineCounter');
       codeEditor.addEventListener('scroll', () => {
           lineCounter.scrollTop = codeEditor.scrollTop;
           lineCounter.scrollLeft = codeEditor.scrollLeft;
       });
       codeEditor.addEventListener('keydown', (e) => {
           let { keyCode } = e;
           let { value, selectionStart, selectionEnd } = codeEditor;
           if (keyCode === 9) {  // TAB = 9
               e.preventDefault();
               codeEditor.value = value.slice(0, selectionStart) + '\t' + value.slice(selectionEnd);
               codeEditor.setSelectionRange(selectionStart + 2, selectionStart + 2)
           }
       });
       var lineCountCache = 0;
       function line_counter() {
           var lineCount = codeEditor.value.split('\n').length;
           var outarr = new Array();
           if (lineCountCache != lineCount) {
               for (var x = 0; x < lineCount; x++) {
                   outarr[x] = (x + 1) + '.';
               }
               lineCounter.value = outarr.join('\n');
           }
           lineCountCache = lineCount;
       }
       codeEditor.addEventListener('input', () => {
           line_counter();
       });
   </script>


</asp:Content>

