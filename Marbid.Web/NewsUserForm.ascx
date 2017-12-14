<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsUserForm.ascx.cs" Inherits="Marbid.Web.NewsUserForm" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<dx:ASPxNewsControl ID="ASPxNewsControl1" runat="server" EnableTheming="True" NavigateUrlFormatString="Read More{0}" Theme="Youthful" Width="400px">
    <Items>
        <dx:NewsItem Date="2017-09-01" HeaderText="Test" Name="Test" NavigateUrl="http://localost/test.aspx" Text="Test">
        </dx:NewsItem>
    </Items>
    <ItemSettings TailText="Read More">
    </ItemSettings>
</dx:ASPxNewsControl>

