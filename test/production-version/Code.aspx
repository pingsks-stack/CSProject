<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="Code.aspx.vb" Inherits="production_version_Code" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/atom-one-dark.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/vbnet.min.js"></script>
    <style>
        .code-view { margin: 0; background: #1a1625 !important; border-radius: 0 0 16px 16px; }
        .code-view code { display: block; padding: 18px 20px !important; background: transparent !important;
            font-family: "JetBrains Mono", Consolas, "Courier New", monospace !important; font-size: 13.5px; line-height: 1.7; counter-reset: line; }
        .code-view .ln { display: block; }
        .code-view .ln::before { counter-increment: line; content: counter(line); display: inline-block; width: 2.6em; margin-right: 1em;
            text-align: right; color: #6b6383; user-select: none; }
        .code-view code *, .code-view code { font-family: "JetBrains Mono", Consolas, "Courier New", monospace !important; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6 tw-flex tw-flex-wrap tw-items-end tw-justify-between tw-gap-4">
        <div>
            <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">คลังซอร์สโค้ดและฟังก์ชัน</h1>
            <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted"><%: ScopeNote %></p>
        </div>
        <% If CanAdd Then %>
        <a href="CodeEdit.aspx" class="btn-accent"><i data-lucide="file-code"></i>เพิ่ม/จัดการโค้ดของโครงงาน</a>
        <% End If %>
    </div>

    <div class="panel tw-mb-4 tw-p-4">
        <div class="tw-grid tw-gap-3 md:tw-grid-cols-[1fr_220px_auto]">
            <label class="tw-m-0 tw-flex tw-h-10 tw-items-center tw-gap-2 tw-rounded-lg tw-border tw-border-line tw-bg-surface-2 tw-px-3 tw-text-muted">
                <i data-lucide="search" class="tw-h-4 tw-w-4"></i>
                <input id="c-q" type="search" value="<%: Q %>" placeholder="ชื่อฟังก์ชัน โค้ด หรือชื่อโครงงาน"
                    class="tw-w-full tw-border-0 tw-bg-transparent tw-text-sm tw-text-ink tw-outline-none" style="background:transparent!important;border:0!important">
            </label>
            <select id="c-lang" class="form-control tw-h-10"><%= LangOptions %></select>
            <button type="button" id="c-go" class="btn-accent tw-h-10 tw-justify-center"><i data-lucide="search"></i>ค้นหา</button>
        </div>
    </div>

    <div class="tw-grid tw-gap-4 xl:tw-grid-cols-[380px_1fr]">
        <div class="panel tw-overflow-hidden">
            <div class="panel-head"><h3 class="panel-title">ฟังก์ชัน</h3><span class="tw-text-sm tw-text-muted"><%: ResultCount %> รายการ</span></div>
            <div class="tw-max-h-[70vh] tw-overflow-y-auto"><%= ListHtml %></div>
        </div>

        <div class="panel tw-min-w-0 tw-overflow-hidden">
            <%= ViewerHtml %>
        </div>
    </div>

    <script>
        (function () {
            function go() {
                var p = new URLSearchParams();
                var q = document.getElementById('c-q').value.trim(); if (q) p.set('q', q);
                var l = document.getElementById('c-lang').value; if (l) p.set('lang', l);
                location.href = 'Code.aspx' + (p.toString() ? '?' + p : '');
            }
            document.getElementById('c-go').addEventListener('click', go);
            document.getElementById('c-lang').addEventListener('change', go);
            document.getElementById('c-q').addEventListener('keydown', function (e) { if (e.key === 'Enter') { e.preventDefault(); go(); } });

            document.addEventListener('DOMContentLoaded', function () {
                var code = document.querySelector('.code-view code');
                if (!code) return;
                if (window.hljs) hljs.highlightElement(code);
                // ใส่เลขบรรทัด
                code.innerHTML = code.innerHTML.split('\n').map(function (l) { return '<span class="ln">' + (l || ' ') + '</span>'; }).join('');
                var btn = document.getElementById('copy-code');
                if (btn) btn.addEventListener('click', function () {
                    var text = document.getElementById('raw-code').value;
                    (navigator.clipboard ? navigator.clipboard.writeText(text) : Promise.reject()).then(
                        function () { Notiflix.Notify.success('คัดลอกโค้ดแล้ว'); },
                        function () { var t = document.getElementById('raw-code'); t.style.display = 'block'; t.select(); document.execCommand('copy'); t.style.display = 'none'; Notiflix.Notify.success('คัดลอกโค้ดแล้ว'); });
                });
            });
        })();
    </script>
</asp:Content>
