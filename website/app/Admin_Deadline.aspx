<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageAdmin.master" AutoEventWireup="false" CodeFile="Admin_Deadline.aspx.vb" Inherits="production_version_Admin_Deadline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6">
        <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">กำหนดส่งงาน</h1>
        <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted">กำหนดวันส่งเอกสารแต่ละบทตามปีการศึกษา ระบบจะแจ้งนิสิตและแสดงงานที่เลยกำหนดบนแดชบอร์ด</p>
    </div>

    <div class="tw-grid tw-gap-4 xl:tw-grid-cols-[360px_1fr]">
        <div class="panel tw-h-fit">
            <div class="panel-head"><div><h3 class="panel-title">เพิ่ม/แก้ไขกำหนดส่ง</h3><p class="panel-sub">ถ้าบทนั้นในปีการศึกษานั้นมีอยู่แล้ว จะอัปเดตเป็นวันใหม่</p></div></div>
            <div class="tw-space-y-4 tw-p-5">
                <div>
                    <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ปีการศึกษา</label>
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" />
                </div>
                <div>
                    <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">บท</label>
                    <asp:DropDownList ID="ddlChapter" runat="server" CssClass="form-control" />
                </div>
                <div>
                    <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">วันที่กำหนดส่ง</label>
                    <asp:TextBox ID="txtDue" runat="server" TextMode="Date" CssClass="form-control" />
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDue" ErrorMessage="กรุณาเลือกวันที่" Display="Dynamic" CssClass="tw-mt-1 tw-block tw-text-xs tw-text-bad" />
                </div>
                <div>
                    <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">หมายเหตุ (ไม่บังคับ)</label>
                    <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" MaxLength="300" placeholder="เช่น ส่งพร้อมแบบฟอร์มขออนุมัติหัวข้อ" />
                </div>
                <asp:Button ID="btnSave" runat="server" Text="บันทึก" CssClass="btn btn-primary tw-w-full" />
            </div>
        </div>

        <div class="tw-space-y-4"><%= ListHtml %></div>
    </div>

    <asp:HiddenField ID="hfDelete" runat="server" />
    <asp:Button ID="btnDelete" runat="server" style="display:none" CausesValidation="false" />
    <script>
        function delDeadline(id, label) {
            Notiflix.Confirm.show('ลบกำหนดส่ง', 'ลบกำหนดส่ง "' + label + '" ใช่ไหม?', 'ลบ', 'ยกเลิก', function () {
                document.getElementById('<%= hfDelete.ClientID %>').value = id;
                document.getElementById('<%= btnDelete.ClientID %>').click();
            });
        }
    </script>
</asp:Content>
