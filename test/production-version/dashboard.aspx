<%@ Page Title="" Language="VB" MasterPageFile="~/production-version/MasterPageHome.master" AutoEventWireup="false" CodeFile="dashboard.aspx.vb" Inherits="production_version_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="https://cdn.jsdelivr.net/npm/apexcharts@7.8.0/dist/apexcharts.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6 tw-flex tw-flex-wrap tw-items-end tw-justify-between tw-gap-4">
        <div>
            <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">สวัสดี <%: FirstName %></h1>
            <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted">สรุปสถานะโครงงานและความเคลื่อนไหวล่าสุดของคุณ</p>
        </div>
        <a href="Insert.aspx" class="btn-accent"><i data-lucide="file-plus"></i>เพิ่มโครงงาน</a>
    </div>

    <div class="tw-grid tw-gap-4 sm:tw-grid-cols-2 xl:tw-grid-cols-4">
        <%= KpiHtml %>
    </div>

    <div class="tw-mt-4 tw-grid tw-gap-4 xl:tw-grid-cols-3">
        <div class="panel xl:tw-col-span-2">
            <div class="panel-head"><div><h3 class="panel-title">ความเคลื่อนไหวในโครงงานของฉัน</h3><p class="panel-sub">การอัปโหลดไฟล์และการแก้ไขชื่อ · 12 เดือนล่าสุดที่มีข้อมูล</p></div></div>
            <div class="tw-p-3"><div id="chart-monthly"></div></div>
        </div>
        <div class="panel">
            <div class="panel-head"><div><h3 class="panel-title">โครงงานในระบบตามประเภท</h3><p class="panel-sub">ดูแนวทางจากรุ่นพี่</p></div></div>
            <div class="tw-p-3"><div id="chart-types"></div></div>
        </div>
    </div>

    <div class="tw-mt-4 tw-grid tw-gap-4 xl:tw-grid-cols-3">
        <div class="panel tw-overflow-hidden xl:tw-col-span-2">
            <div class="panel-head">
                <h3 class="panel-title">โครงงานของฉัน</h3>
                <a href="Main_Doc.aspx" class="tw-flex tw-items-center tw-gap-1 tw-text-sm tw-text-muted hover:tw-text-ink">ดูทั้งหมด <i data-lucide="arrow-right" class="tw-h-4 tw-w-4"></i></a>
            </div>
            <div class="tw-overflow-x-auto">
                <table class="data-table">
                    <thead><tr><th>โครงงาน</th><th>ประเภท</th><th>อาจารย์</th><th>สถานะ</th><th></th></tr></thead>
                    <tbody><%= MyProjectsHtml %></tbody>
                </table>
            </div>
        </div>
        <div class="panel tw-overflow-hidden">
            <div class="panel-head"><h3 class="panel-title">กิจกรรมล่าสุด</h3></div>
            <%= ActivityHtml %>
        </div>
    </div>

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            UI.area('chart-monthly', <%= MonthLabels %>, [
                { name: 'อัปโหลดไฟล์', data: <%= MonthFiles %> },
                { name: 'แก้ไข/สร้างโครงงาน', data: <%= MonthEdits %> }
            ]);
            UI.hbar('chart-types', <%= TypeLabels %>, <%= TypeValues %>, 'โครงงาน');
            UI.spark('spark-files', <%= MonthFiles %>, 'info');
        });
    </script>
</asp:Content>
