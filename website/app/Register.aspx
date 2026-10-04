<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Register.aspx.vb" Inherits="production_version_Login" %>

<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<!DOCTYPE html>
<html lang="th" class="dark">
<head runat="server">
    <script>try { if (localStorage.getItem('theme') === 'light') document.documentElement.classList.remove('dark'); } catch (e) { }</script>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link rel="icon" href="assets/img/Project/up_logo.png" type="image/x-icon">
    <title>สมัครสมาชิก · CS Project</title>
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Noto+Sans+Thai:wght@300;400;500;600;700&display=swap">
    <link rel="stylesheet" href="assets/css/app.css">
    <link rel="stylesheet" href="assets/css/tailwind.css">
    <script src="https://cdn.jsdelivr.net/npm/notiflix@3.2.8/dist/notiflix-aio-3.2.8.min.js"></script>
    <script src="assets/js/ui.js"></script>
</head>
<body class="tw-min-h-screen tw-bg-bg">
    <form id="form1" runat="server">
        <div class="app-content tw-flex tw-min-h-screen tw-items-center tw-justify-center tw-p-4 sm:tw-p-8">
            <div class="tw-w-full tw-max-w-2xl tw-rounded-3xl tw-border tw-border-line tw-bg-surface tw-p-6 tw-shadow-2xl sm:tw-p-10">

                <div class="tw-mb-8 tw-flex tw-items-center tw-gap-4">
                    <img src="assets/img/Project/up_logo.png" alt="" class="tw-h-14 tw-w-14">
                    <div>
                        <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">สมัครสมาชิก</h1>
                        <p class="tw-m-0 tw-text-sm tw-text-muted">สร้างบัญชีนิสิตเพื่อส่งและติดตามโครงงาน</p>
                    </div>
                </div>

                <div class="tw-grid tw-gap-5 sm:tw-grid-cols-2">
                    <div>
                        <label for="inputUsername" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium tw-text-ink">ชื่อผู้ใช้</label>
                        <dx:BootstrapTextBox ID="inputUsername" runat="server" MaxLength="50"></dx:BootstrapTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="กรุณาระบุชื่อผู้ใช้" ControlToValidate="inputUsername" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-red-600"></asp:RequiredFieldValidator>
                    </div>

                    <div>
                        <label for="inputPassword" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium tw-text-ink">รหัสผ่าน</label>
                        <dx:BootstrapTextBox ID="inputPassword" runat="server" Password="True" MaxLength="50"></dx:BootstrapTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="กรุณาระบุรหัสผ่าน" ControlToValidate="inputPassword" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-red-600"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegexPassword" runat="server" ErrorMessage="รหัสผ่านต้องยาวอย่างน้อย 6 ตัวอักษร" ControlToValidate="inputPassword" ValidationExpression="^.{6,50}$" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-red-600"></asp:RegularExpressionValidator>
                    </div>

                    <div class="sm:tw-col-span-2">
                        <label for="inputName" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium tw-text-ink">ชื่อ-นามสกุล</label>
                        <dx:BootstrapTextBox ID="inputName" runat="server" MaxLength="50"></dx:BootstrapTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="กรุณาระบุชื่อ-นามสกุล" ControlToValidate="inputName" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-red-600"></asp:RequiredFieldValidator>
                    </div>

                    <div class="sm:tw-col-span-2">
                        <label for="inputEmail" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium tw-text-ink">อีเมล</label>
                        <dx:BootstrapTextBox ID="inputEmail" runat="server" MaxLength="50"></dx:BootstrapTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="กรุณาระบุอีเมล" ControlToValidate="inputEmail" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-red-600"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegexEmail" runat="server" ErrorMessage="รูปแบบอีเมลไม่ถูกต้อง" ControlToValidate="inputEmail" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-red-600"></asp:RegularExpressionValidator>
                    </div>

                    <div>
                        <label for="inputStudent_ID" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium tw-text-ink">รหัสนิสิต</label>
                        <dx:BootstrapTextBox ID="inputStudent_ID" runat="server" MaxLength="8"></dx:BootstrapTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="กรุณาระบุรหัสนิสิต" ControlToValidate="inputStudent_ID" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-red-600"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegexStudentID" runat="server" ErrorMessage="รหัสนิสิตต้องเป็นตัวเลข 8 หลัก" ControlToValidate="inputStudent_ID" ValidationExpression="^\d{8}$" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-red-600"></asp:RegularExpressionValidator>
                    </div>

                    <div>
                        <label for="inputMobile" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium tw-text-ink">เบอร์โทรศัพท์</label>
                        <dx:BootstrapTextBox ID="inputMobile" runat="server" MaxLength="10"></dx:BootstrapTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="กรุณาระบุเบอร์โทรศัพท์" ControlToValidate="inputMobile" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-red-600"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegexMobile" runat="server" ErrorMessage="เบอร์โทรต้องเป็นตัวเลข 10 หลัก ขึ้นต้นด้วย 0" ControlToValidate="inputMobile" ValidationExpression="^0\d{9}$" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-red-600"></asp:RegularExpressionValidator>
                    </div>
                </div>

                <asp:Button ID="Button1" runat="server" Text="สมัครสมาชิก" CssClass="btn btn-primary tw-mt-8 tw-w-full tw-py-2.5 tw-text-base" />
                <asp:Label ID="Label1" runat="server" Text="" CssClass="tw-mt-2 tw-block tw-text-sm tw-text-red-600"></asp:Label>

                <p class="tw-mb-0 tw-mt-6 tw-text-center tw-text-sm tw-text-muted">
                    มีบัญชีอยู่แล้ว?
                    <a href="Login.aspx" class="tw-font-semibold tw-text-accent hover:tw-brightness-110">เข้าสู่ระบบ</a>
                </p>
            </div>
        </div>
    </form>
</body>
</html>
