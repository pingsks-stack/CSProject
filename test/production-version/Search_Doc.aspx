<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="Search_Doc.aspx.vb" Inherits="production_version_Search_Doc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6">
        <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">ค้นหาโครงงาน</h1>
        <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted"><%: ScopeNote %></p>
    </div>

    <div class="panel tw-mb-4 tw-p-4">
        <div class="tw-grid tw-gap-3 md:tw-grid-cols-[1fr_200px_200px_auto]">
            <label class="tw-m-0 tw-flex tw-h-10 tw-items-center tw-gap-2 tw-rounded-lg tw-border tw-border-line tw-bg-surface-2 tw-px-3 tw-text-muted">
                <i data-lucide="search" class="tw-h-4 tw-w-4"></i>
                <input id="s-q" type="search" value="<%: Q %>" placeholder="ชื่อโครงงาน (ไทย/อังกฤษ) ชื่อนิสิต หรืออาจารย์"
                    class="tw-w-full tw-border-0 tw-bg-transparent tw-text-sm tw-text-ink tw-outline-none" style="background:transparent!important;border:0!important">
            </label>
            <select id="s-type" class="form-control tw-h-10"><%= TypeOptions %></select>
            <select id="s-status" class="form-control tw-h-10"><%= StatusOptions %></select>
            <button type="button" id="s-go" class="btn-accent tw-h-10 tw-justify-center"><i data-lucide="search"></i>ค้นหา</button>
        </div>
    </div>

    <div class="panel tw-overflow-hidden">
        <div class="panel-head">
            <h3 class="panel-title">ผลการค้นหา</h3>
            <span class="tw-text-sm tw-text-muted"><%: ResultCount %> โครงงาน</span>
        </div>
        <div class="tw-overflow-x-auto">
            <table class="data-table">
                <thead><tr><th>โครงงาน</th><th>นิสิต</th><th>อาจารย์ที่ปรึกษา</th><th>ประเภท</th><th>ปีการศึกษา</th><th>สถานะ</th><th></th></tr></thead>
                <tbody><%= ResultsHtml %></tbody>
            </table>
        </div>
    </div>

    <script>
        (function () {
            function go() {
                var p = new URLSearchParams();
                var q = document.getElementById('s-q').value.trim();
                if (q) p.set('q', q);
                var t = document.getElementById('s-type').value; if (t) p.set('type', t);
                var s = document.getElementById('s-status').value; if (s) p.set('status', s);
                location.href = 'Search_Doc.aspx' + (p.toString() ? '?' + p.toString() : '');
            }
            document.getElementById('s-go').addEventListener('click', go);
            ['s-type', 's-status'].forEach(function (id) { document.getElementById(id).addEventListener('change', go); });
            // Enter ในช่องค้นหาต้องไม่ส่งฟอร์ม ASP.NET
            document.getElementById('s-q').addEventListener('keydown', function (e) { if (e.key === 'Enter') { e.preventDefault(); go(); } });
        })();
    </script>
</asp:Content>
