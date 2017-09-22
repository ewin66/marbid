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
using Marbid.Module.BusinessObjects.ReportCentral;
using Marbid.Module.BusinessObjects.CRM;
using Marbid.Module.BusinessObjects.Administration;
using DevExpress.ExpressApp.Dashboards;

namespace Marbid.Module.Controllers
{
  // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
  public partial class ReportCentralController : ViewController
  {
    public ReportCentralController()
    {
      InitializeComponent();
      // Target required Views (via the TargetXXX properties) and create their Actions.
    }
    protected override void OnActivated()
    {
      base.OnActivated();
         TargetObjectType = typeof(BIDashboard);
    }

    protected override void OnViewControlsCreated()
    {
      base.OnViewControlsCreated();
      // Access and customize the target View control.
    }
    
    protected override void OnDeactivated()
    {
      // Unsubscribe from previously subscribed events and release other references and resources.
      base.OnDeactivated();
    }

    private void DashboardEdit_Execute(object sender, SimpleActionExecuteEventArgs e)
    {
      IObjectSpace os = Application.CreateObjectSpace();      
      BIDashboard dashboard = os.GetObjectByKey<BIDashboard>(((BIDashboard)View.CurrentObject).Oid);
      DetailView dv = Application.CreateDetailView(os, "BIDashboard_DetailView", true, dashboard);
      dv.ViewEditMode = ViewEditMode.Edit;
      e.ShowViewParameters.CreatedView = dv;
    }
  }
}
