<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="Profile.aspx.vb" Inherits="production_version_Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6">
        <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">โปรไฟล์</h1>
        <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted">ข้อมูลบัญชีและการติดต่อของคุณ</p>
    </div>

    <div class="tw-grid tw-gap-4 xl:tw-grid-cols-[360px_1fr]">
        <div class="tw-space-y-4">
            <div class="panel tw-overflow-hidden">
                <div class="tw-h-20 tw-bg-gradient-to-r tw-from-[#3b1a63] tw-via-[#5b2c8c] tw-to-[#b8860b]"></div>
                <div class="tw-px-5 tw-pb-5">
                    <div class="-tw-mt-9 tw-flex tw-h-[72px] tw-w-[72px] tw-items-center tw-justify-center tw-rounded-full tw-border-4 tw-border-surface tw-bg-accent tw-text-xl tw-font-bold tw-text-accent-ink"><%= Ini %></div>
                    <div class="tw-mt-3 tw-text-lg tw-font-semibold tw-text-ink"><%: UserName %></div>
                    <div class="tw-text-sm tw-text-gold"><%: RoleName %></div>
                    <div class="tw-mt-4 tw-space-y-2 tw-text-sm">
                        <div class="tw-flex tw-items-center tw-gap-2 tw-text-muted"><i data-lucide="at-sign" class="tw-h-4 tw-w-4"></i><span class="tw-text-ink"><%: Login %></span></div>
                        <div class="tw-flex tw-items-center tw-gap-2 tw-text-muted"><i data-lucide="mail" class="tw-h-4 tw-w-4"></i><span class="tw-text-ink"><%: Email %></span></div>
                        <% If StudentID <> "" Then %>
                        <div class="tw-flex tw-items-center tw-gap-2 tw-text-muted"><i data-lucide="id-card" class="tw-h-4 tw-w-4"></i><span class="tw-text-ink">รหัสนิสิต <%: StudentID %></span></div>
                        <% End If %>
                        <div class="tw-flex tw-items-center tw-gap-2 tw-text-muted"><i data-lucide="phone" class="tw-h-4 tw-w-4"></i><span class="tw-text-ink"><%: If(Mobile = "", "-", Mobile) %></span></div>
                    </div>
                </div>
            </div>
            <div class="tw-grid tw-grid-cols-2 tw-gap-4"><%= StatsHtml %></div>
        </div>

        <div class="panel tw-h-fit">
            <div class="panel-head"><div><h3 class="panel-title">แก้ไขข้อมูล</h3><p class="panel-sub">อีเมลและชื่อผู้ใช้ใช้ผูกกับโครงงาน หากต้องการเปลี่ยนกรุณาติดต่อผู้ดูแลระบบ</p></div></div>
            <div class="tw-space-y-4 tw-p-5">
                <div>
                    <label for="<%= txtName.ClientID %>" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ชื่อ-นามสกุล</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="50" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName" ErrorMessage="กรุณากรอกชื่อ-นามสกุล" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                </div>
                <div>
                    <label for="<%= txtMobile.ClientID %>" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">เบอร์โทรศัพท์</label>
                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="10" />
                    <asp:RegularExpressionValidator runat="server" ControlToValidate="txtMobile" ValidationExpression="^0\d{9}$" ErrorMessage="เบอร์โทรต้องเป็นตัวเลข 10 หลัก ขึ้นต้นด้วย 0" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                </div>
                <div class="tw-grid tw-gap-4 md:tw-grid-cols-2">
                    <div>
                        <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium tw-text-muted">ชื่อผู้ใช้</label>
                        <input type="text" class="form-control" value="<%: Login %>" disabled>
                    </div>
                    <div>
                        <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium tw-text-muted">อีเมล</label>
                        <input type="text" class="form-control" value="<%: Email %>" disabled>
                    </div>
                </div>
                <div class="tw-flex tw-flex-wrap tw-gap-2 tw-pt-2">
                    <asp:Button ID="btnSave" runat="server" Text="บันทึกข้อมูล" CssClass="btn btn-primary" />
                    <a href="ChangePassword.aspx" class="btn btn-secondary"><i data-lucide="key-round" class="tw-mr-1 tw-h-4 tw-w-4"></i>เปลี่ยนรหัสผ่าน</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
