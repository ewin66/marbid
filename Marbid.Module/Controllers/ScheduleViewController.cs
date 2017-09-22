using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using Marbid.Module.BusinessObjects.CRM;
using System;
using System.Linq;

namespace Marbid.Module.Controllers
{
   // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
   public partial class ScheduleViewController : ViewController
   {
      public ScheduleViewController()
      {
         InitializeComponent();
         // Target required Views (via the TargetXXX properties) and create their Actions.
         TargetObjectType = typeof(Schedule);
      }

      private void NewObjectAction_Executed1(object sender, ActionBaseEventArgs e)
      {
         Schedule currentSchedule = e.ShowViewParameters.CreatedView.CurrentObject as Schedule;
         if (currentSchedule != null)
         {
            currentSchedule.Label = 2;
            currentSchedule.Status = 2;
         }
      }

      protected override void OnActivated()
      {
         base.OnActivated();
         // Perform various tasks depending on the target View.

         Frame.GetController<NewObjectViewController>().NewObjectAction.Executed += NewObjectAction_Executed1;
      }
      protected override void OnDeactivated()
      {
         // Unsubscribe from previously subscribed events and release other references and resources.
         base.OnDeactivated();
      }

      protected override void OnViewControlsCreated()
      {
         base.OnViewControlsCreated();
         // Access and customize the target View control.
      }
   }
}
