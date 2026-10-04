<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageAdmin.master" AutoEventWireup="false" CodeFile="For_Admin.aspx.vb" Inherits="production_version_For_Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .loader {
            position: fixed;
            left: 0;
            top: 0;
            width: 150%;
            height: 150%;
            background-color: #F5F8FA;
            z-index: 9998;
            text-align: center;
        }
        .plane-container {
            position: absolute;
            top: 50%;
            left: 50%;
        }
        .center {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function redirectToPage(id) {
            var pages = { '1229': 'Admin_infor.aspx', '1230': 'Showsourcecodead.aspx', '1231': 'infor_user.aspx' };
            if (pages[id]) window.location.href = pages[id];
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" Width="100%">
        <ItemTemplate>
            <div class="row my-3">
                <div class="col-lg-4">
                    <div class="counter-box white r-3">
                        <div class="p-4">
                            <div class="center">
                                <span class="icon icon-note-list text-light-blue s-48"></span>
                            </div>
                            <br />
                            <div class="center">
                                <button type="button" class="center btn btn-primary" onclick="redirectToPage('1229');">
                                    <h4>จำนวนโครงงานทั้งหมด</h4>
                                </button>
                            </div>
                            <h4 class="sc-counter mt-3 center"><%#: Eval("Max") %></h4>
                        </div>
                        <div class="progress progress-xs r-0">
                            <div class="progress-bar" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="counter-box white r-3">
                        <div class="p-4">
                            <div class="center">
                                <span class="icon icon-mail-envelope-open text-light-blue s-48"></span>
                            </div>
                            <br />
                            <div class="center">
                                <button type="button" class="center btn btn-primary" onclick="redirectToPage('1230');">
                                    <h4>จำนวนซอร์สโค้ดที่บันทึก</h4>
                                </button>
                            </div>
                            <h4 class="sc-counter mt-3 center"><%#: Eval("Doc_list") %></h4>
                        </div>
                        <div class="progress progress-xs r-0">
                            <div class="progress-bar" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="white counter-box">
                        <div class="p-4">
                            <div class="center">
                                <span class="icon icon-male text-light-blue s-48"></span>
                            </div>
                            <br />
                            <div class="center">
                                <button type="button" class="center btn btn-primary" onclick="redirectToPage('1231');">
                                    <h4>จำนวนผู้ใช้ทั้งหมด</h4>
                                </button>
                            </div>
                            <h4 class="sc-counter mt-3 center"><%#: Eval("User") %></h4>
                        </div>
                        <div class="progress progress-xs r-0">
                            <div class="progress-bar" role="progressbar" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList>

    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' 
        SelectCommand="SELECT (SELECT COUNT(*) FROM MAS_DOC) AS Max, (SELECT COUNT(*) FROM Signin_Signup) AS [User], (SELECT COUNT(*) FROM Doc_List_TB) AS Doc_list">
    </asp:SqlDataSource>
</asp:Content>
