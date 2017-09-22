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
using DevExpress.ExpressApp.Security;
using Marbid.Module.BusinessObjects.CRM;

namespace Marbid.Module.Controllers
{
   // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
   public partial class NewsAddShortDescriptionViewController : ViewController
   {
      public NewsAddShortDescriptionViewController()
      {
         InitializeComponent();
         // Target required Views (via the TargetXXX properties) and create their Actions.
         TargetObjectType = typeof(News);
      }
      protected override void OnActivated()
      {
         base.OnActivated();
         // Perform various tasks depending on the target View.
         bool isPermitted = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace, typeof(News), "ShortDescription", null, SecurityOperations.Write));
         AddShortDescriptionAction.Active.SetItemValue("Security", isPermitted);
         //AddShortDescriptionAction.Active.SetItemValue("Security", SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(News), "ShortDescription", ObjectSpace.GetObjectHandle(View.CurrentObject), SecurityOperations.Write)));

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

      private void AddShortDescriptionAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
      {
         News news = (News)View.CurrentObject;
         View.ObjectSpace.SetModified(news);
         news.ShortDescription = ((ShortDescriptionParametersObject)e.PopupWindow.View.CurrentObject).ShortDescription;
      }

      private void AddShortDescriptionAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
      {
         IObjectSpace objectSpace = Application.CreateObjectSpace();
         e.View = Application.CreateDetailView(Application.CreateObjectSpace(), ShortDescriptionParametersObject.CreateShortDescriptionParametersObject());
         ((DetailView)e.View).ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
      }
   }
}
