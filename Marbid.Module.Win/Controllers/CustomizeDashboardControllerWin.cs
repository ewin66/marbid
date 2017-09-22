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
using DevExpress.ExpressApp.Dashboards.Win;
using DevExpress.DashboardWin;

namespace Marbid.Module.Win.Controllers
{
   // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
   public partial class CustomizeDashboardControllerWin : ObjectViewController<DetailView, IDashboardData>
   {
      protected override void OnActivated()
      {
         base.OnActivated();
         WinDashboardViewerViewItem dashboardViewerViewItem =
             View.FindItem("DashboardViewer") as WinDashboardViewerViewItem;
         if (dashboardViewerViewItem != null)
         {
            if (dashboardViewerViewItem.Viewer != null)
            {
               CustomizeDashboardViewer(dashboardViewerViewItem.Viewer);
            }
            dashboardViewerViewItem.ControlCreated += DashboardViewerViewItem_ControlCreated;
         }
      }
      private void DashboardViewerViewItem_ControlCreated(object sender, EventArgs e)
      {
         WinDashboardViewerViewItem dashboardViewerViewItem = sender as WinDashboardViewerViewItem;
         CustomizeDashboardViewer(dashboardViewerViewItem.Viewer);
      }
      private void CustomizeDashboardViewer(DashboardViewer dashboardViewer)
      {
         dashboardViewer.AllowPrintDashboardItems = true;
      }
      protected override void OnDeactivated()
      {
         WinDashboardViewerViewItem dashboardViewerViewItem =
             View.FindItem("DashboardViewer") as WinDashboardViewerViewItem;
         if (dashboardViewerViewItem != null)
         {
            dashboardViewerViewItem.ControlCreated -= DashboardViewerViewItem_ControlCreated;
         }
         base.OnDeactivated();
      }
   }
}
