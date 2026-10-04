<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="Function_Source.aspx.vb" Inherits="production_version_Function_Source" %>
<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        #myInput {
            background-image: url('/css/searchicon.png'); /* Add a search icon to input */
            background-position: 10px 12px; /* Position the search icon */
            background-repeat: no-repeat; /* Do not repeat the icon image */
            width: 100%; /* Full-width */
            font-size: 16px; /* Increase font-size */
            padding: 12px 20px 12px 40px; /* Add some padding */
            border: 1px solid #ddd; /* Add a grey border */
            margin-bottom: 12px; /* Add some space below the input */
        }

        #myUL {
            /* Remove default list styling */
            list-style-type: none;
            padding: 0;
            margin: 0;
        }

            #myUL li a {
                border: 1px solid #ddd; /* Add a border to all links */
                margin-top: -1px; /* Prevent double borders */
                background-color: #f6f6f6; /* Grey background color */
                padding: 12px; /* Add some padding */
                text-decoration: none; /* Remove default text underline */
                font-size: 18px; /* Increase the font-size */
                color: black; /* Add a black text color */
                display: block; /* Make it into a block element to fill the whole list */
            }

                #myUL li a:hover:not(.header) {
                    background-color: #eee; /* Add a hover effect to all links, except for headers */
                }
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
            .form-control {
        width: 200px;
        margin-bottom: 10px;
        margin-right: 10px;
        float: right;
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
                        <h1>ข้อมูลฟังก์ชันและโค้ดทั้งหมด</h1>
                    </div>
                    <div class="card-body">
                        <asp:DropDownList ID="DropDownList1" runat="server" class="form-control" Style="height: 35px;" DataSourceID="SqlDataSourceDropdown" DataTextField="Doc_Functions_Code" DataValueField="Doc_Functions_Code" AutoPostBack="true">
                            <asp:ListItem Text="ข้อมูลทั้งหมด" Value="" />
                        </asp:DropDownList>

                        <asp:SqlDataSource runat="server" ID="SqlDataSourceDropdown" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT DISTINCT Doc_Functions_Code FROM Doc_List_TB"></asp:SqlDataSource>

                        <asp:DataList ID="DataList_Show_DocList" runat="server" DataSourceID="SqlDataShow_DocList" Width="100%" DataKeyField="Doc_List_ID">
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
                        <asp:SqlDataSource runat="server" ID="SqlDataShow_DocList" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Doc_List_Type, Doc_List_Code, Doc_Functions_Code, Doc_List_ID FROM Doc_List_TB WHERE (@DocFunctionsCode IS NULL OR Doc_Functions_Code = @DocFunctionsCode)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList1" PropertyName="SelectedValue" Name="DocFunctionsCode" ConvertEmptyStringToNull="true" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                    </div>
                    <div class="card-footer text-muted">
                        ข้อมูลฟังก์ชันและโค้ด
                    </div>
                </div>
            </div>
        </div>
    </div>
        </div>

</asp:Content>

