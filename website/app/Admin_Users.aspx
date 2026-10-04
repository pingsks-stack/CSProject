<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageAdmin.master" AutoEventWireup="false" CodeFile="Admin_Users.aspx.vb" Inherits="production_version_Admin_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6 tw-flex tw-flex-wrap tw-items-end tw-justify-between tw-gap-4">
        <div>
            <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">ผู้ใช้งาน</h1>
            <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted">สร้างบัญชีอาจารย์/นิสิต เปลี่ยนบทบาท และรีเซ็ตรหัสผ่าน</p>
        </div>
        <button type="button" class="btn-accent" onclick="document.getElementById('new-user').classList.toggle('tw-hidden')"><i data-lucide="user-plus"></i>สร้างบัญชีใหม่</button>
    </div>

    <div class="tw-mb-4 tw-grid tw-gap-4 sm:tw-grid-cols-3"><%= StatsHtml %></div>

    <%-- ฟอร์มสร้างบัญชี --%>
    <div id="new-user" class="panel tw-mb-4 <%= If(KeepFormOpen, "", "tw-hidden") %>">
        <div class="panel-head"><div><h3 class="panel-title">สร้างบัญชีใหม่</h3><p class="panel-sub">แจ้งชื่อผู้ใช้และรหัสผ่านชั่วคราวให้เจ้าของบัญชี แล้วให้เปลี่ยนรหัสหลังเข้าสู่ระบบครั้งแรก</p></div></div>
        <div class="tw-grid tw-gap-4 tw-p-5 md:tw-grid-cols-3">
            <div>
                <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">บทบาท</label>
                <asp:DropDownList ID="ddlNewRole" runat="server" CssClass="form-control" onchange="toggleStudentId()">
                    <asp:ListItem Value="2" Text="อาจารย์" />
                    <asp:ListItem Value="1" Text="นิสิต" />
                    <asp:ListItem Value="3" Text="ผู้ดูแลระบบ" />
                </asp:DropDownList>
            </div>
            <div>
                <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ชื่อ-นามสกุล</label>
                <asp:TextBox ID="txtNewName" runat="server" CssClass="form-control" MaxLength="50" placeholder="เช่น อาจารย์ สมชาย ใจดี" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNewName" ValidationGroup="new" ErrorMessage="กรุณากรอกชื่อ" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
            </div>
            <div>
                <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ชื่อผู้ใช้ (Username)</label>
                <asp:TextBox ID="txtNewUser" runat="server" CssClass="form-control" MaxLength="50" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNewUser" ValidationGroup="new" ErrorMessage="กรุณากรอกชื่อผู้ใช้" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
            </div>
            <div>
                <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">อีเมล</label>
                <asp:TextBox ID="txtNewEmail" runat="server" CssClass="form-control" MaxLength="50" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNewEmail" ValidationGroup="new" ErrorMessage="กรุณากรอกอีเมล" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNewEmail" ValidationGroup="new" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ErrorMessage="รูปแบบอีเมลไม่ถูกต้อง" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
            </div>
            <div>
                <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">เบอร์โทรศัพท์</label>
                <asp:TextBox ID="txtNewMobile" runat="server" CssClass="form-control" MaxLength="10" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNewMobile" ValidationGroup="new" ValidationExpression="^0\d{9}$" ErrorMessage="เบอร์โทรต้องเป็นตัวเลข 10 หลัก" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
            </div>
            <div id="sid-box">
                <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">รหัสนิสิต</label>
                <asp:TextBox ID="txtNewSid" runat="server" CssClass="form-control" MaxLength="8" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNewSid" ValidationGroup="new" ValidationExpression="^\d{8}$" ErrorMessage="รหัสนิสิตต้องเป็นตัวเลข 8 หลัก" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
            </div>
            <div>
                <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">รหัสผ่านชั่วคราว</label>
                <asp:TextBox ID="txtNewPass" runat="server" CssClass="form-control" MaxLength="50" />
                <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNewPass" ValidationGroup="new" ValidationExpression="^.{6,50}$" ErrorMessage="อย่างน้อย 6 ตัวอักษร" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
            </div>
            <div class="tw-flex tw-items-end md:tw-col-span-2">
                <asp:Button ID="btnCreate" runat="server" Text="สร้างบัญชี" CssClass="btn btn-primary" ValidationGroup="new" />
            </div>
        </div>
    </div>

    <div class="panel tw-overflow-hidden">
        <div class="tw-flex tw-flex-wrap tw-items-center tw-justify-between tw-gap-3 tw-border-b tw-border-line tw-p-4">
            <div class="tw-flex tw-flex-wrap tw-gap-2"><%= ChipsHtml %></div>
            <label class="tw-m-0 tw-flex tw-h-9 tw-w-full tw-max-w-xs tw-items-center tw-gap-2 tw-rounded-lg tw-border tw-border-line tw-bg-surface-2 tw-px-3 tw-text-muted">
                <i data-lucide="search" class="tw-h-4 tw-w-4"></i>
                <input id="u-q" type="search" value="<%: Q %>" placeholder="ชื่อ ชื่อผู้ใช้ อีเมล รหัสนิสิต" class="tw-w-full tw-border-0 tw-bg-transparent tw-text-sm tw-text-ink tw-outline-none" style="background:transparent!important;border:0!important">
            </label>
        </div>
        <div class="tw-overflow-x-auto">
            <table class="data-table">
                <thead><tr><th>ผู้ใช้</th><th>อีเมล</th><th>บทบาท</th><th>รหัสนิสิต / เบอร์</th><th>โครงงาน</th><th class="tw-text-right">จัดการ</th></tr></thead>
                <tbody><%= RowsHtml %></tbody>
            </table>
        </div>
    </div>

    <asp:HiddenField ID="hfAction" runat="server" />
    <asp:HiddenField ID="hfId" runat="server" />
    <asp:HiddenField ID="hfValue" runat="server" />
    <asp:Button ID="btnDo" runat="server" style="display:none" CausesValidation="false" />

    <script>
        function toggleStudentId() {
            var role = document.getElementById('<%= ddlNewRole.ClientID %>').value;
            document.getElementById('sid-box').style.display = role === '1' ? '' : 'none';
        }
        toggleStudentId();

        function userAction(action, id, name, extra) {
            function send(value) {
                document.getElementById('<%= hfAction.ClientID %>').value = action;
                document.getElementById('<%= hfId.ClientID %>').value = id;
                document.getElementById('<%= hfValue.ClientID %>').value = value || '';
                document.getElementById('<%= btnDo.ClientID %>').click();
            }
            if (action === 'role') {
                var labels = { '1': 'นิสิต', '2': 'อาจารย์', '3': 'ผู้ดูแลระบบ' };
                Notiflix.Confirm.show('เปลี่ยนบทบาท', 'เปลี่ยน "' + name + '" เป็น ' + labels[extra] + ' ใช่ไหม?', 'เปลี่ยน', 'ยกเลิก', function () { send(extra); });
            } else if (action === 'reset') {
                Notiflix.Confirm.prompt('รีเซ็ตรหัสผ่าน', 'รหัสผ่านชั่วคราวใหม่ของ "' + name + '" (อย่างน้อย 6 ตัว)', extra, 'รีเซ็ต', 'ยกเลิก',
                    function (pw) { if (pw && pw.length >= 6) send(pw); else Notiflix.Notify.warning('รหัสผ่านต้องยาวอย่างน้อย 6 ตัวอักษร'); });
            } else if (action === 'delete') {
                Notiflix.Confirm.show('ลบบัญชี', 'ต้องการลบบัญชี "' + name + '" ใช่ไหม? ลบแล้วกู้คืนไม่ได้', 'ลบ', 'ยกเลิก', function () { send(''); });
            }
        }
        document.getElementById('u-q').addEventListener('keydown', function (e) {
            if (e.key !== 'Enter') return;
            e.preventDefault();
            var p = new URLSearchParams(location.search);
            if (this.value.trim()) p.set('q', this.value.trim()); else p.delete('q');
            location.href = 'Admin_Users.aspx' + (p.toString() ? '?' + p : '');
        });
    </script>
</asp:Content>
