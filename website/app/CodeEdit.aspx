<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="CodeEdit.aspx.vb" Inherits="production_version_CodeEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .code-input { font-family: "JetBrains Mono", Consolas, "Courier New", monospace !important; font-size: 13.5px !important;
            line-height: 1.6; min-height: 340px; tab-size: 4; white-space: pre; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6 tw-flex tw-flex-wrap tw-items-end tw-justify-between tw-gap-4">
        <div>
            <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">โค้ดของโครงงาน</h1>
            <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted"><%: Subtitle %></p>
        </div>
        <a href="Code.aspx" class="btn btn-secondary"><i data-lucide="library" class="tw-mr-1 tw-h-4 tw-w-4"></i>ดูคลังซอร์สโค้ด</a>
    </div>

    <%-- ยังไม่ได้เลือกโครงงาน --%>
    <asp:Panel ID="pnlPick" runat="server" Visible="false">
        <div class="tw-grid tw-gap-4 sm:tw-grid-cols-2 xl:tw-grid-cols-3"><%= PickHtml %></div>
    </asp:Panel>

    <asp:Panel ID="pnlEdit" runat="server" Visible="false">
        <div class="tw-grid tw-gap-4 xl:tw-grid-cols-[340px_1fr]">
            <div class="panel tw-overflow-hidden">
                <div class="panel-head">
                    <h3 class="panel-title">ฟังก์ชันในโครงงาน</h3>
                    <a href="CodeEdit.aspx?DocID=<%: DocID %>" class="tw-text-sm tw-font-semibold">+ เพิ่มใหม่</a>
                </div>
                <div class="tw-max-h-[65vh] tw-overflow-y-auto"><%= ListHtml %></div>
            </div>

            <div class="panel">
                <div class="panel-head"><h3 class="panel-title"><%: FormTitle %></h3></div>
                <div class="tw-space-y-4 tw-p-5">
                    <div class="tw-grid tw-gap-4 md:tw-grid-cols-[220px_1fr]">
                        <div>
                            <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ภาษา</label>
                            <asp:DropDownList ID="ddlLang" runat="server" CssClass="form-control" />
                        </div>
                        <div>
                            <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ชื่อฟังก์ชัน / หัวข้อ</label>
                            <asp:TextBox ID="txtFunc" runat="server" CssClass="form-control" MaxLength="100" placeholder="เช่น ตรวจสอบการเข้าสู่ระบบ, คำนวณค่าเฉลี่ย" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFunc" ValidationGroup="code" ErrorMessage="กรุณาระบุชื่อฟังก์ชัน" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                        </div>
                    </div>
                    <div>
                        <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ซอร์สโค้ด</label>
                        <asp:TextBox ID="txtCode" runat="server" TextMode="MultiLine" CssClass="form-control code-input" spellcheck="false" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCode" ValidationGroup="code" ErrorMessage="กรุณาวางซอร์สโค้ด" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                    </div>
                    <div class="tw-flex tw-flex-wrap tw-gap-2">
                        <asp:Button ID="btnSave" runat="server" Text="บันทึก" CssClass="btn btn-primary" ValidationGroup="code" />
                        <asp:Button ID="btnDelete" runat="server" Text="ลบฟังก์ชันนี้" CssClass="btn btn-danger" CausesValidation="false" Visible="false"
                            OnClientClick="var b=this; Notiflix.Confirm.show('ลบโค้ด','ต้องการลบฟังก์ชันนี้ใช่ไหม?','ลบ','ยกเลิก',function(){ b.onclick=null; b.click(); }); return false;" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <script>
        // กด Tab ในช่องโค้ดให้เว้นวรรคแทนการย้ายช่อง
        document.addEventListener('keydown', function (e) {
            var t = e.target;
            if (e.key !== 'Tab' || !t.classList || !t.classList.contains('code-input')) return;
            e.preventDefault();
            var s = t.selectionStart, end = t.selectionEnd;
            t.value = t.value.substring(0, s) + '    ' + t.value.substring(end);
            t.selectionStart = t.selectionEnd = s + 4;
        });
    </script>
</asp:Content>
