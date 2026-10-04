<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageTeacher.master" AutoEventWireup="false" CodeFile="Doc_Student.aspx.vb" Inherits="production_version_Doc_Student" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6 tw-flex tw-flex-wrap tw-items-end tw-justify-between tw-gap-4">
        <div>
            <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">โครงงานที่รับผิดชอบ</h1>
            <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted">โครงงานที่คุณเป็นอาจารย์ที่ปรึกษาหรือกรรมการ</p>
        </div>
        <div class="tw-flex tw-flex-wrap tw-gap-2"><%= FilterHtml %></div>
    </div>

    <div class="tw-grid tw-gap-4 lg:tw-grid-cols-2">
        <%= CardsHtml %>
    </div>
</asp:Content>
