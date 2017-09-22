using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security;
using Marbid.Module.BusinessObjects.CRM;
using System;
using System.Linq;

namespace Marbid.Module.Controllers
{
   // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
   public partial class NewsRelatedOrganizationViewController : ViewController
   {
      public NewsRelatedOrganizationViewController()
      {
         InitializeComponent();
         // Target required Views (via the TargetXXX properties) and create their Actions.
         TargetObjectType = typeof(News);
      }
      protected override void OnActivated()
      {
         base.OnActivated();
         // Perform various tasks depending on the target View.
         bool isPermitted = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace, typeof(News), "RelatedOrganizations", null, SecurityOperations.Write));
         ShowRelatedCustomerAction.Active.SetItemValue("Security", isPermitted);
         RemoveRelatedOrganizationAction.Active.SetItemValue("Security", isPermitted);
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

      private void ShowRelatedCustomerAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
      {
         IObjectSpace objectSpace = Application.CreateObjectSpace();
         e.View = Application.CreateListView(Application.FindListViewId(typeof(Organization)), new CollectionSource(objectSpace, typeof(Organization)), true);
      }

      private void ShowRelatedCustomerAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
      {
         News news = (News)View.CurrentObject;
         foreach (Organization org in e.PopupWindowView.SelectedObjects)
         {
            Organization nOrg = ObjectSpace.GetObjectByKey<Organization>(org.Oid);
            news.RelatedOrganizations.Add(nOrg);
         }
         news.UpdateRelatedOrganization(true);
         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
            View.ObjectSpace.CommitChanges();
      }

      private void RemoveRelatedOrganizationAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
      {
         News currentNews = (News)View.CurrentObject;
         View.ObjectSpace.SetModified(currentNews);

         foreach (Organization emp in e.PopupWindow.View.SelectedObjects)
         {
            Organization nEmp = ObjectSpace.GetObjectByKey<Organization>(emp.Oid);
            currentNews.RelatedOrganizations.Remove(nEmp);
         }

         currentNews.UpdateRelatedOrganization(true);

         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
            View.ObjectSpace.CommitChanges();
      }

      private void RemoveRelatedOrganizationAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
      {
         IObjectSpace objectSpace = Application.CreateObjectSpace();
         News currentNews = (News)objectSpace.GetObject(View.CurrentObject);
         CollectionSourceBase collectionSource = Application.CreatePropertyCollectionSource(objectSpace, typeof(News), currentNews, View.ObjectTypeInfo.FindMember("RelatedOrganizations"), "Organization_ListView");
         e.View = Application.CreateListView("Organization_LookupListView", collectionSource, true);
      }
   }
}