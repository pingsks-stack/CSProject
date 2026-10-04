<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="Talk.aspx.vb" Inherits="production_version_Doc_Student" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     
    <style>
    .centered-h5 {
        text-align: center; /* จัดวางให้อยู่ตรงกลางแนวนอน */
        font-size: 24px; /* ขนาดตัวอักษร */
        color: #333; /* สีตัวอักษร */
        font-weight: bold; /* ตัวหนา */
        /* เพิ่มคุณสมบัติ CSS อื่นๆ ตามที่คุณต้องการ */
    }
    .comment {    
    padding: 10px;
    margin-bottom: 10px;
    background-color: #f9f9f9;
}

.comment-info {
    font-weight: bold;
    margin-bottom: 25px;
}

.comment-name {
    color: #007bff; /* สีข้อความชื่อผู้คอมเมนต์ */
}

.comment-date {
    float: right;
    color: #666; /* สีข้อความวัน-เวลา */
}

.comment-text {
    clear: both;
    margin-top: 5px;
}
.contact-teacher {
        float: right; /* จัดวางด้านขวา */
        color: #007bff; /* สีข้อความ "ติดต่ออาจารย์" */
        margin-top: -15px; /* เพิ่มการเลื่อนขึ้นเพื่อไม่ให้ข้อความตกลงมาจากกรอบสี่เหลี่ยม */
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
        <!-- ช่องซ้าย: ปุ่ม "ผ่าน", ปุ่ม "ไม่ผ่าน", และ ช่องคอมเมนต์ -->
        <div class="col-md-12">
            <div class="card col-md-8 mx-auto">
                <div class="card-body">
                    <h5 class="centered-h5">แชท</h5>
                    <hr />
                    <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" Width="100%">
                        <ItemTemplate>
                            <ul class="list-unstyled">                               
                                <li class="my-1">
                                    <div class="card no-b p-3">
                                        <div class="">
                                            <div class="float-right">
                                                <a href="Chat_NS.aspx?EmailAJ=<%#: Eval("Email_AJ") %>&NameAJ=<%#: Eval("Name") %>&EmailNS=<%#: Eval("Email_NS") %>" class="btn-fab btn-fab-sm btn-primary r-5">
                                                    <i class="icon-mail-envelope-closed2 p-0"></i>
                                                </a>
                                                
                                            </div>
                                            <div class="image mr-3  float-left">
                                                <img class="w-40px" src="assets/img/dummy/u1.png" alt="User Image">
                                            </div>
                                            <div>
                                                <div>
                                                    <strong><%#: Eval("Name") %></strong>
                                                </div>
                                                <small><%#: Eval("Email_AJ") %></small>
                                            </div>
                                        </div>
                                    </div>
                                </li>                              
                            </ul>
                            
                           <%-- Email:
                            <asp:Label Text='<%#: Eval("Email") %>' runat="server" ID="EmailLabel" /><br />
                            Name:
                            <asp:Label Text='<%#: Eval("Name") %>' runat="server" ID="NameLabel" /><br />
                            Mobile:
                            <asp:Label Text='<%#: Eval("Mobile") %>' runat="server" ID="MobileLabel" /><br />--%>
                           <%-- <div class="comment">
                                <div class="comment-info">
                                    <span class="comment-name"><span>จาก : </span><%#: Eval("Email_AJ") %></span>
                                    <span class="comment-date"><%#: Eval("Name") %></span>
                                </div>--%>
                                <%--<div class="comment-text">
                                    ข้อความ : <%#: Eval("Comment_Text") %>
                                </div>--%>
                                <%--<div class="contact-teacher">
                                    <a href="Chat_NS.aspx?EmailAJ=<%#: Eval("Email_AJ") %>&NameAJ=<%#: Eval("Name") %>&EmailNS=<%#: Eval("Email_NS") %>">ดูแชท</a>
                                </div>
                            </div>--%>
                        </ItemTemplate>
                    </asp:DataList>

                    <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT TOP (100) PERCENT MAX(Talk_ID) AS Expr1, Email_AJ, Email_NS, Name, MAX(Comment_Text) AS Expr2, MAX(Date_Save) AS Expr3, MAX(Post_Name_RS) AS Expr4, Urole FROM Talk_TB WHERE (Email_NS = @Email_NS) GROUP BY Email_AJ, Email_NS, Name, Urole HAVING (Urole = N'2') ORDER BY Expr1 DESC">
                        <SelectParameters>
                            <asp:SessionParameter SessionField="Email" Name="Email_NS"></asp:SessionParameter>
                        </SelectParameters>
                    </asp:SqlDataSource>
                   

                </div>
            </div>
        </div>
    </div>
</div>




    <style>
        .full-width {
            width: 100%;
        }
    </style>
   
</asp:Content>

