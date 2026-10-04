<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="Insert.aspx.vb" Inherits="production_version_Insert" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v21.1, Version=21.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6">
        <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">เพิ่มโครงงาน</h1>
        <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted">กรอกข้อมูลโครงงานและอัปโหลดเอกสาร จากนั้นเชิญอาจารย์ที่ปรึกษาและกรรมการได้ที่หน้าจัดการโครงงาน</p>
    </div>

    <div class="tw-grid tw-gap-4 xl:tw-grid-cols-[1fr_320px]">
        <div class="panel">
            <div class="panel-head"><div><h3 class="panel-title">ข้อมูลโครงงาน</h3><p class="panel-sub">ชื่อโครงงานต้องไม่ซ้ำกับโครงงานที่มีอยู่ในระบบ</p></div></div>
            <div class="tw-space-y-5 tw-p-5">
                <div class="tw-grid tw-gap-5 md:tw-grid-cols-2">
                    <div>
                        <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ชื่อโครงงาน (ภาษาไทย)</label>
                        <dx:BootstrapTextBox ID="NamprojectTH" runat="server" MaxLength="200"></dx:BootstrapTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="กรุณาระบุชื่อโครงงาน (ภาษาไทย)" ControlToValidate="NamprojectTH" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                        <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ชื่อโครงงาน (ภาษาอังกฤษ)</label>
                        <dx:BootstrapTextBox ID="NameprojectEN" runat="server" MaxLength="200"></dx:BootstrapTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="กรุณาระบุชื่อโครงงาน (ภาษาอังกฤษ)" ControlToValidate="NameprojectEN" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="tw-max-w-sm">
                    <label for="<%= ddlDocType.ClientID %>" class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ประเภทโครงงาน</label>
                    <asp:DropDownList ID="ddlDocType" runat="server" CssClass="form-control" DataSourceID="SqlDocType"
                        DataTextField="Doc_Type" DataValueField="Doc_Type" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- เลือกประเภท --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredDocType" runat="server" ErrorMessage="กรุณาเลือกประเภทโครงงาน" ControlToValidate="ddlDocType" InitialValue="" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad"></asp:RequiredFieldValidator>
                    <asp:SqlDataSource runat="server" ID="SqlDocType" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>'
                        SelectCommand="SELECT Doc_Type FROM DocTypeTB ORDER BY CASE WHEN Doc_Type = N'Other' THEN 1 ELSE 0 END, TypeID"></asp:SqlDataSource>
                </div>

                <div>
                    <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">เอกสารโครงงาน</label>
                    <label id="dropzone" for="<%= FileUpload1.ClientID %>" class="tw-m-0 tw-flex tw-cursor-pointer tw-flex-col tw-items-center tw-justify-center tw-gap-2 tw-rounded-xl tw-border-2 tw-border-dashed tw-border-line tw-bg-surface-2 tw-px-6 tw-py-10 tw-text-center tw-transition hover:tw-border-accent">
                        <span class="kpi-icon tw-bg-accent/15 tw-text-accent"><i data-lucide="upload-cloud"></i></span>
                        <span class="tw-text-sm tw-font-semibold tw-text-ink">คลิกเพื่อเลือกไฟล์ หรือลากไฟล์มาวาง</span>
                        <span class="tw-text-xs tw-text-muted">PDF, Word, PowerPoint, Excel, ZIP/RAR · สูงสุด 10 ไฟล์</span>
                        <span id="file-list" class="tw-mt-2 tw-flex tw-flex-wrap tw-justify-center tw-gap-2"></span>
                    </label>
                    <asp:FileUpload ID="FileUpload1" AllowMultiple="true" runat="server" CssClass="tw-hidden"
                        accept=".pdf,.doc,.docx,.ppt,.pptx,.xls,.xlsx,.txt,.zip,.rar,.7z,.png,.jpg,.jpeg" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="กรุณาเลือกไฟล์เอกสารอย่างน้อย 1 ไฟล์" ControlToValidate="FileUpload1" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblFileList" runat="server" CssClass="tw-hidden"></asp:Label>
                    <asp:Label ID="lblUploadStatus" runat="server" CssClass="tw-mt-2 tw-block tw-text-sm tw-text-muted"></asp:Label>
                    <asp:Label ID="lblFailedStatus" runat="server" CssClass="tw-block tw-text-sm tw-text-bad"></asp:Label>
                </div>

                <div class="tw-flex tw-justify-end tw-gap-2 tw-border-t tw-border-line tw-pt-5">
                    <a href="Main_Doc.aspx" class="btn btn-secondary">ยกเลิก</a>
                    <asp:Button ID="Button1" runat="server" Text="บันทึกโครงงาน" CssClass="btn btn-primary"
                        OnClientClick="return confirmSave(this);" />
                </div>
            </div>
        </div>

        <div class="panel tw-h-fit">
            <div class="panel-head"><h3 class="panel-title">ขั้นตอนถัดไป</h3></div>
            <ol class="tw-m-0 tw-list-none tw-space-y-4 tw-p-5">
                <li class="tw-flex tw-gap-3"><span class="kpi-icon tw-bg-accent/15 tw-text-accent"><i data-lucide="file-plus"></i></span><div><div class="tw-text-sm tw-font-semibold">1. สร้างโครงงาน</div><div class="tw-text-xs tw-text-muted">กรอกชื่อ ประเภท และอัปโหลดเอกสาร</div></div></li>
                <li class="tw-flex tw-gap-3"><span class="kpi-icon tw-bg-surface-2 tw-text-muted"><i data-lucide="graduation-cap"></i></span><div><div class="tw-text-sm tw-font-semibold">2. เชิญอาจารย์</div><div class="tw-text-xs tw-text-muted">ที่ปรึกษา 1 ท่าน และกรรมการ (รวมไม่เกิน 3)</div></div></li>
                <li class="tw-flex tw-gap-3"><span class="kpi-icon tw-bg-surface-2 tw-text-muted"><i data-lucide="code-xml"></i></span><div><div class="tw-text-sm tw-font-semibold">3. เพิ่มซอร์สโค้ด</div><div class="tw-text-xs tw-text-muted">ฟังก์ชันสำคัญของระบบ ใช้เป็นแนวทางให้รุ่นน้อง</div></div></li>
                <li class="tw-flex tw-gap-3"><span class="kpi-icon tw-bg-surface-2 tw-text-muted"><i data-lucide="badge-check"></i></span><div><div class="tw-text-sm tw-font-semibold">4. รอผลพิจารณา</div><div class="tw-text-xs tw-text-muted">อาจารย์กดผ่านครบ 3/3 แล้วพิมพ์ใบยืนยันได้</div></div></li>
            </ol>
        </div>
    </div>

    <script>
        (function () {
            var input = document.getElementById('<%= FileUpload1.ClientID %>');
            var zone = document.getElementById('dropzone'), list = document.getElementById('file-list');
            function show() {
                list.innerHTML = '';
                Array.prototype.slice.call(input.files || []).forEach(function (f) {
                    var s = document.createElement('span');
                    s.className = 'badge-soft tw-bg-accent/15 tw-text-accent';
                    s.textContent = f.name + ' (' + Math.max(1, Math.round(f.size / 1024)) + ' KB)';
                    list.appendChild(s);
                });
            }
            input.addEventListener('change', show);
            ['dragenter', 'dragover'].forEach(function (ev) { zone.addEventListener(ev, function (e) { e.preventDefault(); zone.classList.add('tw-border-accent'); }); });
            ['dragleave', 'drop'].forEach(function (ev) { zone.addEventListener(ev, function (e) { e.preventDefault(); zone.classList.remove('tw-border-accent'); }); });
            zone.addEventListener('drop', function (e) { input.files = e.dataTransfer.files; show(); });

            // ยืนยันก่อนบันทึกด้วย Notiflix (ตรวจ validator ก่อน)
            window.confirmSave = function (btn) {
                if (btn.dataset.ok === '1') return true;
                if (typeof Page_ClientValidate === 'function' && !Page_ClientValidate()) return false;
                if ((input.files || []).length > 10) { Notiflix.Notify.warning('เลือกได้สูงสุด 10 ไฟล์'); return false; }
                Notiflix.Confirm.show('บันทึกโครงงาน', 'ยืนยันการสร้างโครงงานนี้ใช่ไหม?', 'บันทึก', 'ยกเลิก', function () {
                    btn.dataset.ok = '1'; btn.click();
                });
                return false;
            };
        })();
    </script>
</asp:Content>
