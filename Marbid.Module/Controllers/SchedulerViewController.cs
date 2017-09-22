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
using Marbid.Module.BusinessObjects;
using Marbid.Module.BusinessObjects.Administration;
using Marbid.Module.BusinessObjects.CRM;

namespace Marbid.Module.Controllers
{
  // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
  public partial class SchedulerViewController : ViewController
  {
    public SchedulerViewController()
    {
      InitializeComponent();
      // Target required Views (via the TargetXXX properties) and create their Actions.
      //TargetObjectType = typeof(Employee);
      //TargetViewType = ViewType.ListView;
      //TargetViewNesting = Nesting.Nested;
      TargetViewId = "Schedule_ScheduleParticipants_ListView";
      TargetObjectType = typeof(Schedule);
    }
    protected override void OnActivated()
    {
      base.OnActivated();
      // Perform various tasks depending on the target View.
      // Access and customize the target View control.
      Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("Active", false);
      Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("Active", false);
    }

    protected override void OnViewControlsCreated()
    {
      base.OnViewControlsCreated();
    }
    protected override void OnDeactivated()
    {
      // Unsubscribe from previously subscribed events and release other references and resources.
      base.OnDeactivated();
    }
  }
}
