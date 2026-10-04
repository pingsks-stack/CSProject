<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="ChangePassword.aspx.vb" Inherits="production_version_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6">
        <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">เปลี่ยนรหัสผ่าน</h1>
        <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted">รหัสผ่านถูกเก็บแบบเข้ารหัส (hash) ผู้ดูแลระบบก็ไม่เห็นรหัสผ่านของคุณ</p>
    </div>

    <div class="tw-grid tw-gap-4 xl:tw-grid-cols-2">
        <div class="panel">
            <div class="panel-head"><div><h3 class="panel-title">รหัสผ่านของฉัน</h3><p class="panel-sub">ยืนยันรหัสผ่านเดิมก่อนตั้งรหัสใหม่</p></div></div>
            <div class="tw-space-y-4 tw-p-5">
                <div>
                    <label for="<%= txtCurrent.ClientID %>" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">รหัสผ่านปัจจุบัน</label>
                    <asp:TextBox ID="txtCurrent" runat="server" TextMode="Password" CssClass="form-control" MaxLength="50" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCurrent" ValidationGroup="mine" ErrorMessage="กรุณากรอกรหัสผ่านปัจจุบัน" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                </div>
                <div>
                    <label for="<%= txtNew.ClientID %>" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">รหัสผ่านใหม่</label>
                    <asp:TextBox ID="txtNew" runat="server" TextMode="Password" CssClass="form-control" MaxLength="50" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNew" ValidationGroup="mine" ErrorMessage="กรุณากรอกรหัสผ่านใหม่" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNew" ValidationGroup="mine" ValidationExpression="^.{6,50}$" ErrorMessage="รหัสผ่านต้องยาวอย่างน้อย 6 ตัวอักษร" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                </div>
                <div>
                    <label for="<%= txtConfirm.ClientID %>" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ยืนยันรหัสผ่านใหม่</label>
                    <asp:TextBox ID="txtConfirm" runat="server" TextMode="Password" CssClass="form-control" MaxLength="50" />
                    <asp:CompareValidator runat="server" ControlToValidate="txtConfirm" ControlToCompare="txtNew" ValidationGroup="mine" ErrorMessage="รหัสผ่านใหม่ไม่ตรงกัน" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                </div>
                <asp:Button ID="btnChange" runat="server" Text="บันทึกรหัสผ่านใหม่" CssClass="btn btn-primary" ValidationGroup="mine" />
            </div>
        </div>

        <asp:Panel ID="pnlAdmin" runat="server" CssClass="panel" Visible="false">
            <div class="panel-head"><div><h3 class="panel-title">รีเซ็ตรหัสผ่านผู้ใช้</h3><p class="panel-sub">สำหรับผู้ดูแลระบบ เมื่อนิสิตหรืออาจารย์ลืมรหัสผ่าน</p></div></div>
            <div class="tw-space-y-4 tw-p-5">
                <div>
                    <label for="<%= txtResetUser.ClientID %>" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ชื่อผู้ใช้ (Username)</label>
                    <asp:TextBox ID="txtResetUser" runat="server" CssClass="form-control" MaxLength="50" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtResetUser" ValidationGroup="reset" ErrorMessage="กรุณากรอกชื่อผู้ใช้" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                </div>
                <div>
                    <label for="<%= txtResetPass.ClientID %>" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">รหัสผ่านใหม่ชั่วคราว</label>
                    <asp:TextBox ID="txtResetPass" runat="server" CssClass="form-control" MaxLength="50" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtResetPass" ValidationGroup="reset" ValidationExpression="^.{6,50}$" ErrorMessage="รหัสผ่านต้องยาวอย่างน้อย 6 ตัวอักษร" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtResetPass" ValidationGroup="reset" ErrorMessage="กรุณากรอกรหัสผ่านใหม่" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                </div>
                <asp:Button ID="btnReset" runat="server" Text="รีเซ็ตรหัสผ่าน" CssClass="btn btn-warning" ValidationGroup="reset" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
