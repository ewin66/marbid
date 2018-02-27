<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="NewsViewTemplate.ascx.vb" Inherits="WebControls.NewsViewTemplate" %>
<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<style type="text/css">
    .auto-style2 {
        width: 10%;
        height: 84px;
    }

    .auto-style3 {
        width: 90%;
        height: 84px;
    }
</style>

<dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent runat="server">
            <table style="width: 100%;">
                <tr>
                    <td style="vertical-align: top;" class="auto-style2">
                        <dx:ASPxBinaryImage ID="ASPxBinaryImage1" runat="server" Height="80px" Width="120px" Value='<%# Eval("YourImageField")%>'></dx:ASPxBinaryImage>
                    </td>
                    <td class="auto-style3">
                        <table style="width: 100%">
                            <tr>
                                <td colspan="2" style="vertical-align: top;">
                                    <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Tanggal
                                    <asp:Label ID="Date" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                </td>
                                <td>Sumber
                                    <asp:HyperLink ID="Source" runat="server" Text='<%# Eval("SourceName") %>' NavigateUrl='<%#Eval("SourceURL") %>'></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Literal ID="Content" Text='<%# Eval("Content") %>' runat="server"></asp:Literal>
                                </td>
                            </tr>


                        </table>
                    </td>
                </tr>
            </table>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxPanel>

