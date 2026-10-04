<%@ Page Title="" Language="VB" MasterPageFile="~/app/MasterPageHome.master" AutoEventWireup="false" CodeFile="dashboard_Project.aspx.vb" Inherits="production_version_dashboard_Project" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tw-mb-6">
        <h1 class="tw-m-0 tw-text-2xl tw-font-bold tw-text-ink">สถิติโครงงานตามประเภท</h1>
        <p class="tw-m-0 tw-mt-1 tw-text-sm tw-text-muted">จำนวนโครงงานในระบบแยกตามประเภท (นิสิตเลือกประเภทได้ที่หน้าเอกสารโปรเจคของฉัน)</p>
    </div>

    <div class="tw-grid tw-gap-4 sm:tw-grid-cols-2 xl:tw-grid-cols-3">
        <asp:Repeater ID="RepeaterTypes" runat="server" DataSourceID="SqlTypeStats">
            <ItemTemplate>
                <div class="tw-rounded-2xl tw-border tw-border-line tw-bg-surface tw-p-5 tw-shadow-sm">
                    <div class="tw-flex tw-items-start tw-justify-between tw-gap-3">
                        <div>
                            <div class="tw-text-sm tw-font-medium tw-text-muted"><%#: Eval("TypeName") %></div>
                            <div class="tw-mt-1 tw-text-3xl tw-font-bold tw-text-ink"><%#: Eval("Total") %></div>
                        </div>
                        <span class="tw-rounded-full tw-bg-accent/15 tw-px-2.5 tw-py-1 tw-text-xs tw-font-semibold tw-text-accent"><%#: Eval("Pct") %>%</span>
                    </div>
                    <div class="tw-mt-4 tw-h-2 tw-overflow-hidden tw-rounded-full tw-bg-surface-2">
                        <div class="tw-h-full tw-rounded-full tw-bg-accent" style="width: <%#: Eval("Pct") %>%"></div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <asp:SqlDataSource runat="server" ID="SqlTypeStats" ConnectionString='<%$ ConnectionStrings:DOCDBConnectionString %>'
        SelectCommand="
            WITH c AS (
                SELECT t.Doc_Type AS TypeName, COUNT(m.DocID) AS Total,
                       CASE WHEN t.Doc_Type = N'Other' THEN 1 ELSE 0 END AS SortGroup, t.TypeID AS SortID
                FROM DocTypeTB t LEFT JOIN MAS_DOC m ON m.Doc_Type = t.Doc_Type
                GROUP BY t.Doc_Type, t.TypeID
                UNION ALL
                SELECT N'ยังไม่ระบุประเภท', COUNT(*), 2, 0 FROM MAS_DOC WHERE Doc_Type IS NULL OR Doc_Type = N''
            )
            SELECT TypeName, Total, CAST(ROUND(Total * 100.0 / NULLIF(SUM(Total) OVER (), 0), 0) AS int) AS Pct
            FROM c ORDER BY SortGroup, SortID">
    </asp:SqlDataSource>
</asp:Content>
