<%@ Control Language="C#" CodeBehind="MarbidLogonTemplateContext.ascx.cs" ClassName="MarbidLogonTemplateContext" Inherits="Marbid.Web.MarbidLogonTemplateContext" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v17.1, Version=17.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Templates.ActionContainers"
    TagPrefix="xaf" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v17.1, Version=17.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Templates.Controls"
    TagPrefix="xaf" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v17.1, Version=17.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Controls"
    TagPrefix="xaf" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v17.1, Version=17.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Templates"
    TagPrefix="xaf" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v17.1, Version=17.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<meta name="viewport" content="width=device-width, user-scalable=no, maximum-scale=1.0, minimum-scale=1.0">
<script type="text/javascript">
    function ShowReadMoreWindow() {
        pcReadmore.Show();
    }
</script>
<div class="LogonTemplate">
    <xaf:XafUpdatePanel ID="UPPopupWindowControl" runat="server">
        <xaf:XafPopupWindowControl runat="server" ID="PopupWindowControl" />
    </xaf:XafUpdatePanel>
    <xaf:XafUpdatePanel ID="UPHeader" runat="server">
        <div class="white borderBottom width100" id="headerTableDiv">
            <div class="paddings sizeLimit" style="margin: auto">
                <table id="headerTable" class="headerTable xafAlignCenter white width100 sizeLimit" style="height: 60px;">
                    <tbody>
                        <tr>
                            <td>
                                <asp:HyperLink runat="server" NavigateUrl="#" ID="LogoLink">
                                    <xaf:ThemedImageControl ID="TIC" DefaultThemeImageLocation="Images" ImageName="Logo.png" BorderWidth="0px" runat="server" />
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </xaf:XafUpdatePanel>

    <div style="top: 15%; width: 100%; position: absolute">
        <table class="LogonMainTable LogonContentWidth">
            <tr>
                <td>
                    <xaf:XafUpdatePanel ID="UPEI" runat="server">
                        <xaf:ErrorInfoControl ID="ErrorInfo" Style="margin: 10px 0px 10px 0px" runat="server" />
                    </xaf:XafUpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="LogonContent LogonContentWidth">
                        <tr>
                            <td class="LogonContentCell">
                                <xaf:XafUpdatePanel ID="UPVSC" runat="server">
                                    <xaf:ViewSiteControl ID="viewSiteControl" runat="server" />
                                </xaf:XafUpdatePanel>

                                <xaf:XafUpdatePanel ID="UPPopupActions" runat="server" CssClass="right">
                                    <xaf:ActionContainerHolder ID="PopupActions" runat="server" Orientation="Horizontal" ContainerStyle="Buttons">
                                        <Menu Width="100%" ItemAutoWidth="False" />
                                        <ActionContainers>
                                            <xaf:WebActionContainer ContainerId="PopupActions" />
                                        </ActionContainers>
                                    </xaf:ActionContainerHolder>
                                </xaf:XafUpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td colspan="4">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="LogonContentCell">
                                            <span class="StaticText">For fast and reliable experience when using Marbid please use following browsers:
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <img src="images/edge_32x32.png" />
                                        </td>
                                        <td align="center">
                                            <img src="images/ie_32x32.png" />
                                        </td>
                                        <td align="center">
                                            <img src="images/chrome_32x32.png" />
                                        </td>
                                        <td align="center">
                                            <img src="images/opera_32x32.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <span class="StaticText">Microsoft Edge
                                            </span>
                                        </td>
                                        <td align="center">
                                            <span class="StaticText">Microsoft Internet Explorer
                                            </span>
                                        </td>
                                        <td align="center">
                                            <span class="StaticText">Google Chrome
                                            </span>
                                        </td>
                                        <td align="center">
                                            <span class="StaticText">Opera
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <div style="margin: 16px auto; width: 160px;">
                                                <dx:ASPxButton ID="btShowModal" runat="server" Text="Read More" AutoPostBack="False" UseSubmitBehavior="false" Width="100%">
                                                    <ClientSideEvents Click="function(s, e) { ShowReadMoreWindow(); }" />
                                                </dx:ASPxButton>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
<dx:ASPxPopupControl ID="pcReadmore" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcReadmore"
    HeaderText="Firefox Compatibility Issue" AllowDragging="True" PopupAnimationType="Slide" EnableViewState="False" Width="500px">
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            Hello Mareiners,
            <p>
                Currently, some users experiencing a very slow loading time when using Marbid especially when using Firefox browsers.
            </p>
            <p>
                The problem is caused by major bug in javascript processing and aunthentication problems on some versions of Firefox. Since Marbid using extensive javascripting some users who are using outdated Firefox versions will experience a slow loading time. The only solution to fix these issues require Firefox to be updated or reinstalled. 
            </p>
            <p>
                Updating or reinstalling Firefox requires help from IT Division, but in the meantime you can use any other browsers to access Marbid. There probably only two other browsers installed on your PC/Laptop by default:
            </p>
            <ul>
                <li><img src="images/edge_32x32.png" align="left"/> Microsoft Edge, we recommend you to use this browser if you already using Windows 10 on your PC/Laptop.<br />&nbsp;</li>
                <li><img src="images/ie_32x32.png" align="left"/>Microsoft Internet Explorer, we recommend you to use this browser if your PC/Laptop is using older versions of Windows (Windows XP/Vista/7/8).</li>
            </ul>
            <p>
                If you need further assisstance regarding this or any other issues feel free to contact us on extension 115 or you can use <a href="http://rnd.ntmarein.local/#ViewID=Feedback_ListView&ObjectClassName=Marbid.Module.BusinessObjects.Administration.Feedback">Feedback</a> menu from inside Marbid. thank you for your kind attention.
            </p>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ContentStyle>
        <Paddings PaddingBottom="5px" />
    </ContentStyle>
</dx:ASPxPopupControl>