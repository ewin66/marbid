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
using Marbid.Module.BusinessObjects.CRM;

namespace Marbid.Module.Controllers
{
   // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
   public partial class DisableCloneSchedulerViewController : ViewController
   {
      public DisableCloneSchedulerViewController()
      {
         InitializeComponent();
         // Target required Views (via the TargetXXX properties) and create their Actions.
      }
      protected override void OnActivated()
      {
         base.OnActivated();
         // Perform various tasks depending on the target View.

         if (View is ListView && View.ObjectTypeInfo.Type == typeof(Schedule))
         {
            View.SelectionChanged += View_SelectionChanged;
         }
      }

      private void View_SelectionChanged(object sender, EventArgs e)
      {
         if (View.CurrentObject != null)
            if (((Schedule)View.CurrentObject).CreatedBy.Oid != (Guid)SecuritySystem.CurrentUserId)
               Frame.GetController<DevExpress.ExpressApp.CloneObject.CloneObjectViewController>().CloneObjectAction.Active.SetItemValue("Controller active", false);
            else
               Frame.GetController<DevExpress.ExpressApp.CloneObject.CloneObjectViewController>().CloneObjectAction.Active.SetItemValue("Controller active", true);
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
         View.SelectionChanged -= View_SelectionChanged;
      }
   }
}
