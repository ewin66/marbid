using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Dashboards.Web;
using DevExpress.DashboardWeb;
using System.Web.UI.WebControls;

namespace Marbid.Module.Web.Controllers
{
  // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
  public partial class DashboardViewerControllerWeb : ObjectViewController<DetailView, IDashboardData>
  {
    public DashboardViewerControllerWeb()
    {
      InitializeComponent();
      // Target required Views (via the TargetXXX properties) and create their Actions.
    }
    protected override void OnActivated()
    {
      base.OnActivated();
      // Perform various tasks depending on the target View.
      base.OnActivated();
      DevExpress.ExpressApp.Dashboards.Web.WebDashboardViewerViewItem dbViewerViewItem = View.FindItem("DashboardViewer") as DevExpress.ExpressApp.Dashboards.Web.WebDashboardViewerViewItem;

      if (dbViewerViewItem != null)
      {
        if (dbViewerViewItem.DashboardDesigner != null) SetHeight(dbViewerViewItem.DashboardDesigner);
        dbViewerViewItem.ControlCreated += DbViewerViewItem_ControlCreated;
      }
    }
    private void SetHeight(DevExpress.DashboardWeb.ASPxDashboard dashboardDesigner)
    {
      dashboardDesigner.Height = System.Web.UI.WebControls.Unit.Percentage(100);
    }

    private void DbViewerViewItem_ControlCreated(object sender, EventArgs e)
    {
      DevExpress.ExpressApp.Dashboards.Web.WebDashboardViewerViewItem dbViewItem = sender as DevExpress.ExpressApp.Dashboards.Web.WebDashboardViewerViewItem;
      DashboardConfigurator.PassCredentials = true;

      //SetHeight(dbViewItem.DashboardDesigner);

      dbViewItem.DashboardDesigner.AllowExportDashboardItems = true;
      dbViewItem.DashboardDesigner.EnableCustomSql = true;
      //dbViewItem.DashboardDesigner.ColorScheme = ASPxDashboardViewer.ColorSchemeLightCompact;
    }

    protected override void OnViewControlsCreated()
    {
      base.OnViewControlsCreated();
      // Access and customize the target View control.
    }
    protected override void OnDeactivated()
    {
      WebDashboardViewerViewItem dbViewerViewItem = View.FindItem("DashboardViewer") as WebDashboardViewerViewItem;
      if (dbViewerViewItem != null) dbViewerViewItem.ControlCreated -= DbViewerViewItem_ControlCreated;
      // Unsubscribe from previously subscribed events and release other references and resources.
      base.OnDeactivated();
    }
  }
}
