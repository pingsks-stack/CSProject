<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageAdmin.master" AutoEventWireup="false" CodeFile="Admin_DocType.aspx.vb" Inherits="production_version_Admin_DocType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6">
        <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">ประเภทโครงงาน</h1>
        <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted">หมวดหมู่ที่นิสิตเลือกตอนสร้างโครงงาน และใช้ในหน้าสถิติ/ค้นหา</p>
    </div>

    <div class="panel tw-mb-4 tw-p-4">
        <div class="tw-flex tw-flex-wrap tw-items-center tw-gap-3">
            <asp:TextBox ID="txtNewType" runat="server" CssClass="form-control tw-max-w-sm" MaxLength="50" placeholder="ชื่อประเภทใหม่ เช่น Cyber Security" />
            <asp:Button ID="btnAdd" runat="server" Text="เพิ่มประเภท" CssClass="btn btn-primary" ValidationGroup="add" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNewType" ValidationGroup="add" ErrorMessage="กรุณากรอกชื่อประเภท" Display="Dynamic" CssClass="tw-text-xs tw-text-bad" />
        </div>
    </div>

    <div class="panel tw-overflow-hidden">
        <div class="panel-head"><h3 class="panel-title">ประเภททั้งหมด</h3></div>
        <div class="tw-overflow-x-auto">
            <table class="data-table">
                <thead><tr><th>ลำดับ</th><th>ประเภท</th><th>จำนวนโครงงาน</th><th class="tw-text-right">จัดการ</th></tr></thead>
                <tbody><%= RowsHtml %></tbody>
            </table>
        </div>
    </div>

    <asp:HiddenField ID="hfAction" runat="server" />
    <asp:HiddenField ID="hfId" runat="server" />
    <asp:HiddenField ID="hfValue" runat="server" />
    <asp:Button ID="btnDo" runat="server" style="display:none" CausesValidation="false" />

    <script>
        function typeAction(action, id, name) {
            function send(value) {
                document.getElementById('<%= hfAction.ClientID %>').value = action;
                document.getElementById('<%= hfId.ClientID %>').value = id;
                document.getElementById('<%= hfValue.ClientID %>').value = value || '';
                document.getElementById('<%= btnDo.ClientID %>').click();
            }
            if (action === 'rename') {
                Notiflix.Confirm.prompt('เปลี่ยนชื่อประเภท', 'ชื่อใหม่ของ "' + name + '"', name, 'บันทึก', 'ยกเลิก',
                    function (answer) { if (answer && answer.trim() && answer.trim() !== name) send(answer.trim()); });
            } else {
                Notiflix.Confirm.show('ลบประเภท', 'ต้องการลบประเภท "' + name + '" ใช่ไหม?', 'ลบ', 'ยกเลิก', function () { send(''); });
            }
        }
    </script>
</asp:Content>
