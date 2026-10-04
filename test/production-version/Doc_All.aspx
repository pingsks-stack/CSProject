<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="Doc_All.aspx.vb" Inherits="production_version_Doc_All" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6 tw-flex tw-flex-wrap tw-items-end tw-justify-between tw-gap-4">
        <div>
            <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">คลังโครงงาน</h1>
            <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted">โครงงานที่ผ่านการพิจารณาครบ 3/3 ใช้เป็นแนวทางสำหรับรุ่นต่อไป</p>
        </div>
        <div class="tw-flex tw-gap-2">
            <a href="Code.aspx" class="btn btn-secondary"><i data-lucide="code-xml" class="tw-mr-1 tw-h-4 tw-w-4"></i>คลังซอร์สโค้ด</a>
        </div>
    </div>

    <div class="panel tw-mb-4 tw-p-4">
        <div class="tw-grid tw-gap-3 md:tw-grid-cols-2 xl:tw-grid-cols-[1fr_190px_170px_150px_auto]">
            <label class="tw-m-0 tw-flex tw-h-10 tw-items-center tw-gap-2 tw-rounded-lg tw-border tw-border-line tw-bg-surface-2 tw-px-3 tw-text-muted">
                <i data-lucide="search" class="tw-h-4 tw-w-4"></i>
                <input id="f-q" type="search" value="<%: Q %>" placeholder="ชื่อโครงงาน นิสิต หรืออาจารย์"
                    class="tw-w-full tw-border-0 tw-bg-transparent tw-text-sm tw-text-ink tw-outline-none" style="background:transparent!important;border:0!important">
            </label>
            <select id="f-type" class="form-control tw-h-10"><%= TypeOptions %></select>
            <select id="f-year" class="form-control tw-h-10"><%= YearOptions %></select>
            <select id="f-class" class="form-control tw-h-10"><%= ClassOptions %></select>
            <button type="button" id="f-go" class="btn-accent tw-h-10 tw-justify-center"><i data-lucide="search"></i>ค้นหา</button>
        </div>
    </div>

    <div class="tw-mb-3 tw-text-sm tw-text-muted">พบ <span class="tw-font-semibold tw-text-ink"><%: ResultCount %></span> โครงงาน</div>
    <div class="tw-grid tw-gap-4 md:tw-grid-cols-2 2xl:tw-grid-cols-3">
        <%= CardsHtml %>
    </div>

    <script>
        (function () {
            function go() {
                var p = new URLSearchParams();
                [['q', 'f-q'], ['type', 'f-type'], ['year', 'f-year'], ['class', 'f-class']].forEach(function (x) {
                    var v = document.getElementById(x[1]).value.trim(); if (v) p.set(x[0], v);
                });
                location.href = 'Doc_All.aspx' + (p.toString() ? '?' + p : '');
            }
            document.getElementById('f-go').addEventListener('click', go);
            ['f-type', 'f-year', 'f-class'].forEach(function (id) { document.getElementById(id).addEventListener('change', go); });
            document.getElementById('f-q').addEventListener('keydown', function (e) { if (e.key === 'Enter') { e.preventDefault(); go(); } });
        })();
    </script>
</asp:Content>
