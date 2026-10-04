<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="CommentAJ.aspx.vb" Inherits="production_version_Doc_Student" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
 .btnSave {
    opacity: 0.65; /* ลดความโปร่งใสของปุ่มที่ถูกปิด */
    cursor: not-allowed; /* เปลี่ยนเคอร์เซอร์เป็นเครื่องหมายไม่อนุญาตเมื่อผู้ใช้พยายามคลิก */
    pointer-events: none; /* ป้องกันปุ่มจากการคลิก */
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
        <div class="col-md-6">
            <div class="card col-md-12">
                <div class="card-body">
                    <!-- ปุ่ม "ผ่าน" -->
                   <%-- <h5>ผ่าน/ไม่ผ่าน</h5>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="ผ่าน" CssClass="btn btn-success" OnClick="Button1_Click" />

                    <!-- ปุ่ม "ไม่ผ่าน" -->
                    <asp:Button ID="Button2" runat="server" Text="ไม่ผ่าน" CssClass="btn btn-secondary" OnClick="Button2_Click" />
                    <hr />--%>
                    <h5>เพิ่มข้อมูลคอมเมนต์ที่นี่</h5>
                    <asp:TextBox ID="commentText" runat="server" CssClass="form-control mt-3" TextMode="MultiLine" Rows="4" placeholder="เพิ่มความคิดเห็น"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="กรุณาคอมเม้นต์ก่อนกดบันทึก" ControlToValidate="commentText" Font-Bold="True" CssClass="text-danger" ValidationGroup="CommentValidation"></asp:RequiredFieldValidator>

                    <!-- ปุ่ม "บันทึก" ที่ช่องคอมเมนต์ -->
                    <asp:Button ID="btnSave" runat="server" Text="บันทึก" CssClass="btn btn-secondary mt-3 full-width" OnClick="btnSave_Click" ValidationGroup="CommentValidation" />

                </div>
            </div>
        </div>

        <!-- ช่องขวา: แสดงคอมเมนต์ -->
        <div class="col-md-6">
            <div class="card col-md-12">
                <div class="card-body">
                    <!-- แสดงรายละเอียดเพิ่มเติม -->
                    <h5>รายละเอียดคอมเมนต์:</h5>
                     <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSourceShow" Width="100%">
                        <ItemTemplate>
                            <div class="comment">
                                <div class="comment-info">
                                    <span class="comment-name"><%#: Eval("Name") %>(<%# Eval("Post_Name_RS") %>)</span>
                                    <span class="comment-date"><%#: Eval("Date_Save", "{0:dd MMM yyyy HH:mm}") %></span>
                                </div>
                                <div class="comment-text">
                                    <%#: Eval("Comment_Text") %>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>

                    <asp:SqlDataSource runat="server" ID="SqlDataSourceShow" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Permission_ID, DocID, Name, Comment_Text, Date_Save, Post_Name_RS FROM Comment_TB WHERE (DocID = @DocID) AND (Permission_ID = @Permission_ID)">
                        <SelectParameters>
                            <asp:QueryStringParameter QueryStringField="DocID" Name="DocID"></asp:QueryStringParameter>
                            <asp:QueryStringParameter QueryStringField="Permission_ID" Name="Permission_ID"></asp:QueryStringParameter>
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

