<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="Chapters.aspx.vb" Inherits="production_version_Chapters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6 tw-flex tw-flex-wrap tw-items-end tw-justify-between tw-gap-4">
        <div class="tw-min-w-0">
            <a href="<%: BackUrl %>" class="tw-mb-1 tw-inline-flex tw-items-center tw-gap-1 tw-text-sm tw-text-muted hover:tw-text-ink"><i data-lucide="arrow-left" class="tw-h-4 tw-w-4"></i>กลับ</a>
            <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">เอกสารรายบท</h1>
            <p class="tw-m-0 tw-mt-1 tw-truncate tw-text-sm tw-text-muted"><%: DocName %><%: If(YearNo <> "", " · ปีการศึกษา " & YearNo, "") %></p>
        </div>
        <div class="tw-flex tw-flex-wrap tw-gap-2"><%= SummaryHtml %></div>
    </div>

    <div class="tw-grid tw-gap-4 xl:tw-grid-cols-[1fr_360px]">
        <div class="tw-space-y-4"><%= ChaptersHtml %></div>

        <div class="tw-space-y-4">
            <%-- นิสิต: ส่งเวอร์ชันใหม่ --%>
            <asp:Panel ID="pnlUpload" runat="server" CssClass="panel tw-sticky tw-top-20" Visible="false">
                <div class="panel-head"><div><h3 class="panel-title">ส่งเอกสาร</h3><p class="panel-sub">ส่งซ้ำบทเดิมได้ ระบบจะเก็บเป็นเวอร์ชันใหม่</p></div></div>
                <div class="tw-space-y-4 tw-p-5">
                    <div>
                        <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">บท</label>
                        <asp:DropDownList ID="ddlChapter" runat="server" CssClass="form-control" ClientIDMode="Static" />
                    </div>
                    <div>
                        <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">ไฟล์</label>
                        <asp:FileUpload ID="fuFile" runat="server" CssClass="form-control"
                            accept=".pdf,.doc,.docx,.ppt,.pptx,.xls,.xlsx,.zip,.rar,.7z,.txt,.png,.jpg,.jpeg" />
                        <div class="tw-mt-1 tw-text-xs tw-text-muted">ไม่เกิน 30 MB · PDF, Word, PowerPoint, Excel, ZIP/RAR</div>
                    </div>
                    <div>
                        <label class="tw-mb-1.5 tw-block tw-text-sm tw-font-medium">หมายเหตุถึงอาจารย์ (ไม่บังคับ)</label>
                        <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" MaxLength="500" placeholder="เช่น แก้ไขตามที่อาจารย์แนะนำในหัวข้อ 2.3 แล้ว" />
                    </div>
                    <asp:Button ID="btnUpload" runat="server" Text="ส่งเอกสาร" CssClass="btn btn-primary tw-w-full" />
                </div>
            </asp:Panel>

            <%-- อาจารย์: ให้ความเห็น --%>
            <asp:Panel ID="pnlReview" runat="server" CssClass="panel tw-sticky tw-top-20" Visible="false">
                <div class="panel-head"><div><h3 class="panel-title">ให้ความเห็น</h3><p id="review-target" class="panel-sub">เลือก "ให้ความเห็น" ที่บทที่ต้องการทางซ้าย</p></div></div>
                <div class="tw-space-y-4 tw-p-5">
                    <asp:HiddenField ID="hfSubId" runat="server" ClientIDMode="Static" />
                    <div class="tw-grid tw-grid-cols-2 tw-gap-2">
                        <label class="tw-m-0 tw-flex tw-cursor-pointer tw-items-center tw-justify-center tw-gap-2 tw-rounded-lg tw-border tw-border-line tw-p-2.5 tw-text-sm has-[:checked]:tw-border-ok has-[:checked]:tw-bg-ok/10 has-[:checked]:tw-text-ok">
                            <input type="radio" name="verdict" value="ผ่าน" class="tw-hidden" checked> <i data-lucide="check" class="tw-h-4 tw-w-4"></i>ผ่าน</label>
                        <label class="tw-m-0 tw-flex tw-cursor-pointer tw-items-center tw-justify-center tw-gap-2 tw-rounded-lg tw-border tw-border-line tw-p-2.5 tw-text-sm has-[:checked]:tw-border-warn has-[:checked]:tw-bg-warn/10 has-[:checked]:tw-text-warn">
                            <input type="radio" name="verdict" value="แก้ไข" class="tw-hidden"> <i data-lucide="pencil-line" class="tw-h-4 tw-w-4"></i>ต้องแก้ไข</label>
                    </div>
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" MaxLength="1000" placeholder="ความเห็น / สิ่งที่ต้องแก้ไข" />
                    <asp:Button ID="btnReview" runat="server" Text="บันทึกความเห็น" CssClass="btn btn-primary tw-w-full" OnClientClick="return checkReview();" />
                </div>
            </asp:Panel>

            <div class="panel tw-p-5 tw-text-sm tw-text-muted">
                <div class="tw-mb-2 tw-font-semibold tw-text-ink">สถานะของแต่ละบท</div>
                <div class="tw-space-y-1.5">
                    <div><span class="badge-soft tw-bg-surface-2 tw-text-muted">ยังไม่ส่ง</span> ยังไม่มีไฟล์</div>
                    <div><span class="badge-soft tw-bg-info/15 tw-text-info">รอตรวจ</span> ส่งแล้ว รออาจารย์ให้ความเห็น</div>
                    <div><span class="badge-soft tw-bg-warn/15 tw-text-warn">ต้องแก้ไข</span> อาจารย์ขอให้แก้ไข ส่งเวอร์ชันใหม่ได้</div>
                    <div><span class="badge-soft tw-bg-ok/15 tw-text-ok">ผ่าน</span> อาจารย์ให้ผ่านเวอร์ชันล่าสุด</div>
                    <div><span class="badge-soft tw-bg-bad/15 tw-text-bad">เลยกำหนด</span> เลยกำหนดส่งแล้วยังไม่ส่ง</div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function pickChapter(ch) {
            var s = document.getElementById('ddlChapter');
            if (s) { s.value = ch; s.focus(); s.scrollIntoView({ behavior: 'smooth', block: 'center' }); }
        }
        function startReview(subId, label) {
            document.getElementById('hfSubId').value = subId;
            document.getElementById('review-target').textContent = 'กำลังให้ความเห็น: ' + label;
            var t = document.getElementById('<%= txtComment.ClientID %>');
            t.focus(); t.scrollIntoView({ behavior: 'smooth', block: 'center' });
        }
        function checkReview() {
            if (!document.getElementById('hfSubId').value) { Notiflix.Notify.warning('เลือกบทที่จะให้ความเห็นก่อน'); return false; }
            var v = document.querySelector('input[name=verdict]:checked').value;
            if (v === 'แก้ไข' && !document.getElementById('<%= txtComment.ClientID %>').value.trim()) {
                Notiflix.Notify.warning('ระบุสิ่งที่ต้องแก้ไขให้นิสิตด้วย'); return false;
            }
            return true;
        }
    </script>
</asp:Content>
