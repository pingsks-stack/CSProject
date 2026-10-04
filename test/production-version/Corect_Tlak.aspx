<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="Corect_Tlak.aspx.vb" Inherits="production_version_Doc_Student" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .centered-h5 {
            text-align: center;
            font-size: 24px;
            color: #333;
            font-weight: bold;
        }
        .comment {
            border: 1px solid #ccc;
            padding: 10px;
            margin-bottom: 10px;
            background-color: #f9f9f9;
        }
        .comment-info {
            font-weight: bold;
            margin-bottom: 5px;
        }
        .comment-name {
            color: #007bff;
        }
        .comment-date {
            float: right;
            color: #666;
        }
        .comment-text {
            clear: both;
            margin-top: 5px;
        }
        .contact-teacher {
            float: right;
            color: #007bff;
            margin-top: -15px;
            font-weight: bold;
        }
    </style>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="card col-md-8 mx-auto">
                    <div class="card-body">
                        <h5 class="centered-h5">ข้อมูลอาจารย์</h5>
                        <hr />
                        <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" Width="100%">
                            <ItemTemplate>
                                <div class="comment">
                                    <div class="comment-info">
                                        <span class="comment-name">อีเมลล์ : <%#: Eval("Email") %></span>
                                        <span class="comment-date">ชื่อ-นามสกุล : <%#: Eval("Name") %></span>
                                    </div>
                                    <div class="comment-text">
                                        เบอร์โทรศัพท์ : <%#: Eval("Mobile") %>
                                    </div>
                                    <div class="contact-teacher">
                                        <a href="Chat.aspx?with=<%#: HttpUtility.UrlEncode(Convert.ToString(Eval("Email"))) %>">ติดต่ออาจารย์</a>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Email, Name, Mobile, Urole FROM Signin_Signup WHERE (Urole = N'2')"></asp:SqlDataSource>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
