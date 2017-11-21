<%@ Page Language="C#" AutoEventWireup="true" Inherits="Default" EnableViewState="false"
    ValidateRequest="false" CodeBehind="Default.aspx.cs" %>

<%@ Register Assembly="DevExpress.ExpressApp.Web.v17.1, Version=17.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.ExpressApp.Web.Templates" TagPrefix="cc3" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v17.1, Version=17.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.ExpressApp.Web.Controls" TagPrefix="cc4" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Main Page</title>
    <meta http-equiv="Expires" content="0" />
    <style type="text/css">
        .dxm-item.accountItem.dxm-subMenu .dx-vam {
            padding-left: 10px;
        }

        .dxm-item.accountItem.dxm-subMenu .dxm-image.dx-vam {
            border-radius: 42px;
            -moz-border-radius: 42px;
            -webkit-border-radius: 42px;
            padding-right: 0px !important;
            padding-left: 0px !important;
            max-height: 42px;
            max-width: 42px;
        }

        .Caption {
            color: dimgray;
        }

        .StaticText {
            color: dimgray;
        }

        .CardGroupBase > tbody > tr > td > .GroupHeader {
            color: #FFFFFF;
            background-color: #58B528;
            padding-top: 10px;
            padding-bottom: 11px;
            border-collapse: separate !important;
        }

        .xafNav .dxnbLite_XafTheme .dxnb-headerHover, .xafNav .dxnbLite_XafTheme .dxnb-headerCollapsedHover {
            background-color: #58b528;
        }

        .dxnbLite_XafTheme .dxnb-header, .dxnbLite_XafTheme .dxnb-headerCollapsed {
            font-size: 0.85em;
            border-top: 1px solid #c6c6c6;
            padding: 9px 4px 10px 4px;
            background-color: #58b528;
            color: #ffffff;
            white-space: nowrap;
            padding-left: 24px;
        }

        .dxgvHeader_XafTheme {
            cursor: pointer;
            white-space: nowrap;
            padding: 8px 10px 7px;
            border: 1px Solid #c6c6c6;
            background: #58b528;
            overflow: hidden;
            font-weight: normal;
            font-size: 0.85em;
            text-align: left;
        }

            .dxgvHeader_XafTheme, .dxgvHeader_XafTheme table {
                color: #ffffff;
            }

        .dxGridView_CTClearFilter_XafTheme, .dxGridView_CTClearGrouping_XafTheme, .dxGridView_CTClearGroupingDisabled_XafTheme, .dxGridView_CTDeleteRow_XafTheme, .dxGridView_CTEditRow_XafTheme, .dxGridView_CTFullCollapse_XafTheme, .dxGridView_CTFullExpand_XafTheme, .dxGridView_CTNewRow_XafTheme, .dxGridView_CTRefresh_XafTheme, .dxGridView_CTShowCustDialog_XafTheme, .dxGridView_CTShowCustDialogDisabled_XafTheme, .dxGridView_CTShowCustomizationWindow_XafTheme, .dxGridView_CTShowFilterEditor_XafTheme, .dxGridView_CTShowGroupPanel_XafTheme, .dxGridView_CTShowSearchPanel_XafTheme, .dxGridView_CTShowSearchPanelDisabled_XafTheme, .dxGridView_gvCellError_XafTheme, .dxGridView_gvCMClearFilter_XafTheme, .dxGridView_gvCMClearGrouping_XafTheme, .dxGridView_gvCMClearGroupingDisabled_XafTheme, .dxGridView_gvCMDeleteRow_XafTheme, .dxGridView_gvCMEditRow_XafTheme, .dxGridView_gvCMFullCollapse_XafTheme, .dxGridView_gvCMFullExpand_XafTheme, .dxGridView_gvCMGroupByColumn_XafTheme, .dxGridView_gvCMNewRow_XafTheme, .dxGridView_gvCMRefresh_XafTheme, .dxGridView_gvCMSearchPanel_XafTheme, .dxGridView_gvCMShowCustDialog_XafTheme, .dxGridView_gvCMShowCustDialogDisabled_XafTheme, .dxGridView_gvCMShowCustomizationWindow_XafTheme, .dxGridView_gvCMShowFilterEditor_XafTheme, .dxGridView_gvCMShowGroupPanel_XafTheme, .dxGridView_gvCMShowSearchPanel_XafTheme, .dxGridView_gvCMShowSearchPanelDisabled_XafTheme, .dxGridView_gvCMSortAscending_XafTheme, .dxGridView_gvCMSortDescending_XafTheme, .dxGridView_gvCMSummaryAverage_XafTheme, .dxGridView_gvCMSummaryCount_XafTheme, .dxGridView_gvCMSummaryMax_XafTheme, .dxGridView_gvCMSummaryMin_XafTheme, .dxGridView_gvCMSummarySum_XafTheme, .dxGridView_gvCMUngroupColumn_XafTheme, .dxGridView_gvCOApply_XafTheme, .dxGridView_gvCOApplyDisabled_XafTheme, .dxGridView_gvCOClearFilter_XafTheme, .dxGridView_gvCOClearFilterDisabled_XafTheme, .dxGridView_gvCOClose_XafTheme, .dxGridView_gvCOColumnDrag_XafTheme, .dxGridView_gvCOColumnDragDisabled_XafTheme, .dxGridView_gvCOColumnGroup_XafTheme, .dxGridView_gvCOColumnGroupDisabled_XafTheme, .dxGridView_gvCOColumnHide_XafTheme, .dxGridView_gvCOColumnHideDisabled_XafTheme, .dxGridView_gvCOColumnRemove_XafTheme, .dxGridView_gvCOColumnRemoveDisabled_XafTheme, .dxGridView_gvCOColumnShow_XafTheme, .dxGridView_gvCOColumnShowDisabled_XafTheme, .dxGridView_gvCOColumnSort_XafTheme, .dxGridView_gvCOColumnSortDisabled_XafTheme, .dxGridView_gvCOColumnSortDown_XafTheme, .dxGridView_gvCOColumnSortDownDisabled_XafTheme, .dxGridView_gvCOColumnSortUp_XafTheme, .dxGridView_gvCOColumnSortUpDisabled_XafTheme, .dxGridView_gvCOColumnUngroup_XafTheme, .dxGridView_gvCOColumnUngroupDisabled_XafTheme, .dxGridView_gvCOColumnUnsort_XafTheme, .dxGridView_gvCOColumnUnsortDisabled_XafTheme, .dxGridView_gvCODragAreaCollapse_XafTheme, .dxGridView_gvCODragAreaExpand_XafTheme, .dxGridView_gvCOFilterCollapse_XafTheme, .dxGridView_gvCOFilterExpand_XafTheme, .dxGridView_gvCollapsedButton_XafTheme, .dxGridView_gvCollapsedButtonRtl_XafTheme, .dxGridView_gvDetailCollapsedButton_XafTheme, .dxGridView_gvDetailCollapsedButtonRtl_XafTheme, .dxGridView_gvDetailExpandedButton_XafTheme, .dxGridView_gvDetailExpandedButtonRtl_XafTheme, .dxGridView_gvDragAndDropArrowDown_XafTheme, .dxGridView_gvDragAndDropArrowLeft_XafTheme, .dxGridView_gvDragAndDropArrowRight_XafTheme, .dxGridView_gvDragAndDropArrowUp_XafTheme, .dxGridView_gvDragAndDropHideColumn_XafTheme, .dxGridView_gvExpandedButton_XafTheme, .dxGridView_gvExpandedButtonRtl_XafTheme, .dxGridView_gvFilterRowButton_XafTheme, .dxGridView_gvFixedGroupRow_XafTheme, .dxGridView_gvHeaderFilter_XafTheme, .dxGridView_gvHeaderFilterActive_XafTheme, .dxGridView_gvHeaderSortDown_XafTheme, .dxGridView_gvHeaderSortUp_XafTheme, .dxGridView_gvHideAdaptiveDetailButton_XafTheme, .dxGridView_gvParentGroupRows_XafTheme, .dxGridView_gvShowAdaptiveDetailButton_XafTheme, .dxGridView_WindowResizer_XafTheme, .dxGridView_WindowResizerRtl_XafTheme {
            background-repeat: no-repeat;
            background-color: white;
        }
    </style>
</head>
<body class="VerticalTemplate">
    <form id="form2" runat="server">
        <cc4:ASPxProgressControl ID="ProgressControl" runat="server" />
        <div runat="server" id="Content" />
    </form>
</body>
</html>
