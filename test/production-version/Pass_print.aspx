<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="Pass_print.aspx.vb" Inherits="production_version_EditUploadfile" %>

<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.4.0/jspdf.umd.min.js"></script>
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
            text-align: center;
        }

        .gridview th, .gridview td {
            /* รูปแบบเซลล์ตาราง */
            border: none;
            padding: 8px;
            height: 55px;
            color: black; /* เพิ่มสีฟอนต์เป็นสีดำ */
            text-align: center;
        }

        .gridview tr:nth-child(even) {
            /* รูปแบบแถวคู่ของตาราง */
            background-color: #f9f9f9;
        }

        .gridview tr:hover {
            /* รูปแบบแถวเมื่อเมาส์ชี้ */
            background-color: #ddd;
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        th, td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }

        th {
            background-color: #f2f2f2;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="card col-md-12">
                    <div class="card-body">
                        
                       <button type="button" onclick="redirectToChangeFriend()" class="btn btn-secondary">กลับไปยังหน้าเอกสารโปรเจคของนักเรียน</button>
                   
                    <script>
                        function redirectToChangeFriend() {
                            var url = 'Main_Doc.aspx?';
                            window.location.href = url;
                        }
                    </script>
                    <hr />
                    <h5 class="card-title">ข้อมูลที่รอการกดผ่าน/ไม่ผ่านของอาจารย์แต่ละท่าน</h5>
                    <br />
                    <asp:GridView ID="GridView1" runat="server" CssClass="gridview" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
                        <Columns>
                            <asp:BoundField DataField="DocID" HeaderText="DocID" SortExpression="DocID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="ชื่อ-นามสกุล" SortExpression="Name"></asp:BoundField>
                            <asp:BoundField DataField="Urole" HeaderText="Urole" SortExpression="Urole" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="Submit_TC" HeaderText="ผลลับ" SortExpression="Submit_TC"></asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div style="text-align: center;">
                                ไม่มีข้อมูลที่จะแสดง
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>' SelectCommand="SELECT Permission_TB.DocID, Permission_TB.Name, Permission_TB.Urole, Permission_TB.Submit_TC FROM MAS_DOC INNER JOIN Permission_TB ON MAS_DOC.DocID = Permission_TB.DocID WHERE (Permission_TB.DocID = @DocID) AND (Permission_TB.Urole = N'2')">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="DocID" QueryStringField="DocID" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


