<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="showSourcecode.aspx.vb" Inherits="production_version_showSourcecode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

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
                height: 230px;
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
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid animatedParent animateOnce my-3">
        <div class="container-fluid my-3">
        <div class="d-flex row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h1><strong>Source Code and Functions</strong></h1>
                    </div>
                    <div class="card-body">
                        
                        <asp:DataList ID="DataList_Show_DocList" runat="server" DataSourceID="SqlDataShow_DocList" Width="100%">
                                <ItemTemplate>
                                      <textarea id='lineCounter' wrap='off' readonly></textarea>
                                        <textarea id='codeEditor' wrap='off'><%#: Eval("Doc_List_Code")%></textarea>
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
                                                      
                               
                                </ItemTemplate>
                           </asp:DataList>
                            
                              
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

