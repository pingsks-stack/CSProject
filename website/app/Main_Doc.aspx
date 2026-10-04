<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="Main_Doc.aspx.vb" Inherits="production_version_Main_Doc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6 tw-flex tw-flex-wrap tw-items-end tw-justify-between tw-gap-4">
        <div>
            <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">โครงงานของฉัน</h1>
            <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted">ติดตามความคืบหน้าและจัดการเอกสาร อาจารย์ และซอร์สโค้ดของแต่ละโครงงาน</p>
        </div>
        <a href="Insert.aspx" class="btn-accent"><i data-lucide="file-plus"></i>เพิ่มโครงงาน</a>
    </div>

    <div class="tw-space-y-4">
        <%= CardsHtml %>
    </div>
</asp:Content>
