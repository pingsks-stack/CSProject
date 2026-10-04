<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="production_version_Login" %>

<%@ Register Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<!DOCTYPE html>
<html lang="th" class="dark">
<head runat="server">
    <script>try { if (localStorage.getItem('theme') === 'light') document.documentElement.classList.remove('dark'); } catch (e) { }</script>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link rel="icon" href="assets/img/Project/up_logo.png" type="image/x-icon">
    <title>เข้าสู่ระบบ · CS Project</title>
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
        <div class="tw-grid tw-min-h-screen lg:tw-grid-cols-2">

            <%-- ฝั่งซ้าย: แนะนำระบบ (ซ่อนบนจอเล็ก) --%>
            <div class="tw-relative tw-hidden tw-overflow-hidden tw-bg-gradient-to-br tw-from-[#2a1146] tw-via-[#4b2378] tw-to-[#6b3aa0] lg:tw-flex lg:tw-flex-col lg:tw-justify-between tw-p-12 tw-text-white">
                <div class="tw-absolute -tw-right-24 -tw-top-24 tw-h-96 tw-w-96 tw-rounded-full tw-bg-white/10"></div>
                <div class="tw-absolute -tw-bottom-32 -tw-left-16 tw-h-80 tw-w-80 tw-rounded-full tw-bg-white/10"></div>

                <div class="tw-relative tw-flex tw-items-center tw-gap-3">
                    <img src="assets/img/Project/up_logo.png" alt="" class="tw-h-12 tw-w-12 tw-rounded-xl tw-bg-surface tw-p-1">
                    <div>
                        <div class="tw-text-lg tw-font-bold">CS Project</div>
                        <div class="tw-text-sm tw-text-white/70">มหาวิทยาลัยพะเยา</div>
                    </div>
                </div>

                <div class="tw-relative">
                    <h1 class="tw-mb-4 tw-text-4xl tw-font-bold tw-leading-tight tw-text-white">ระบบรวบรวม<br>และติดตามโครงงาน</h1>
                    <p class="tw-max-w-md tw-text-lg tw-text-white/80">รวบรวมโครงงานสาขาเทคโนโลยีสารสนเทศ เพื่อใช้ศึกษาและเป็นแนวทางในการทำโครงงานของน้องนิสิตรุ่นต่อไป</p>
                </div>

                <div class="tw-relative tw-grid tw-grid-cols-3 tw-gap-4 tw-text-sm">
                    <div class="tw-rounded-2xl tw-bg-white/10 tw-p-4 tw-backdrop-blur"><div class="tw-font-semibold">ส่งเอกสาร</div><div class="tw-text-white/70">อัปโหลดทุกบท</div></div>
                    <div class="tw-rounded-2xl tw-bg-white/10 tw-p-4 tw-backdrop-blur"><div class="tw-font-semibold">อาจารย์ยืนยัน</div><div class="tw-text-white/70">ติดตามสถานะ</div></div>
                    <div class="tw-rounded-2xl tw-bg-white/10 tw-p-4 tw-backdrop-blur"><div class="tw-font-semibold">คลังโครงงาน</div><div class="tw-text-white/70">ค้นหาง่าย</div></div>
                </div>
            </div>

            <%-- ฝั่งขวา: ฟอร์มเข้าสู่ระบบ --%>
            <div class="app-content tw-flex tw-items-center tw-justify-center tw-p-6 sm:tw-p-12">
                <div class="tw-w-full tw-max-w-md">
                    <div class="tw-mb-8 tw-flex tw-items-center tw-gap-3 lg:tw-hidden">
                        <img src="assets/img/Project/up_logo.png" alt="" class="tw-h-11 tw-w-11">
                        <div class="tw-text-lg tw-font-bold tw-text-ink">CS Project</div>
                    </div>

                    <h2 class="tw-mb-1 tw-text-3xl tw-font-bold tw-text-ink">เข้าสู่ระบบ</h2>
                    <p class="tw-mb-8 tw-text-muted">ยินดีต้อนรับกลับ กรุณากรอกข้อมูลเพื่อเข้าใช้งาน</p>

                    <div class="tw-space-y-5">
                        <div>
                            <label for="inputEmailAddress" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium tw-text-ink">ชื่อผู้ใช้</label>
                            <dx:BootstrapTextBox ID="inputEmailAddress" runat="server">
                                <ValidationSettings SetFocusOnError="True">
                                    <RequiredField IsRequired="True" ErrorText="กรุณาระบุชื่อผู้ใช้" />
                                </ValidationSettings>
                            </dx:BootstrapTextBox>
                        </div>

                        <div>
                            <label for="inputChoosePassword" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium tw-text-ink">รหัสผ่าน</label>
                            <div class="tw-relative" id="show_hide_password">
                                <dx:BootstrapTextBox ID="inputChoosePassword" runat="server" Password="True">
                                    <ValidationSettings SetFocusOnError="True">
                                        <RequiredField IsRequired="True" ErrorText="กรุณาระบุรหัสผ่าน" />
                                    </ValidationSettings>
                                </dx:BootstrapTextBox>
                                <button type="button" id="togglePassword" class="tw-absolute tw-right-2 tw-top-[7px] tw-rounded-md tw-border-0 tw-bg-transparent tw-px-2 tw-py-1 tw-text-xs tw-font-medium tw-text-accent hover:tw-bg-accent/10">แสดง</button>
                            </div>
                        </div>

                        <asp:Button ID="Button1" runat="server" Text="เข้าสู่ระบบ" CssClass="btn btn-primary tw-w-full tw-py-2.5 tw-text-base"
                            OnClientClick="if (!ASPxClientEdit.ValidateGroup(null)) {return false};" />
                    </div>

                    <p class="tw-mt-8 tw-text-center tw-text-sm tw-text-muted">
                        ยังไม่มีบัญชี?
                        <a href="Register.aspx" class="tw-font-semibold tw-text-accent hover:tw-brightness-110">สมัครสมาชิก</a>
                    </p>
                </div>
            </div>
        </div>

        <script>
            // ใช้ placeholder ของ HTML แทน NullText ของ DevExpress (NullText ถูกส่งไปกับฟอร์มเป็นค่า)
            document.getElementById('inputEmailAddress_I').placeholder = 'กรอกชื่อผู้ใช้';
            document.getElementById('inputChoosePassword_I').placeholder = 'กรอกรหัสผ่าน';

            document.getElementById('togglePassword').addEventListener('click', function () {
                var input = document.querySelector('#show_hide_password input');
                var show = input.type === 'password';
                input.type = show ? 'text' : 'password';
                this.textContent = show ? 'ซ่อน' : 'แสดง';
            });
        </script>
    </form>
</body>
</html>
