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
using Marbid.Module.BusinessObjects.Administration;
using Marbid.Module.BusinessObjects.HRM;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

namespace Marbid.Module.Controllers
{
   // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
   public partial class OfficeLeaveViewController : ViewController
   {
      public OfficeLeaveViewController()
      {
         InitializeComponent();
         // Target required Views (via the TargetXXX properties) and create their Actions.
         TargetObjectType = typeof(OfficeLeave);
      }
      protected override void OnActivated()
      {

         base.OnActivated();
         UpdateApprovalActionState();
      }
      void View_CurrentObjectChanged(object sender, EventArgs e)
      {
         UpdateApprovalActionState();
      }
      private void UpdateApprovalActionState()
      {
         bool isGranted = false;
         string objectHandle = ObjectSpace.GetObjectHandle(View.CurrentObject);
         bool isDirector = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace, typeof(OfficeLeave), "DirectorComment", null, SecurityOperations.Write));//SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(OfficeLeave), "DirectorComment", objectHandle, SecurityOperations.Write));
         bool isHR = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace, typeof(OfficeLeave), "HRComment", null, SecurityOperations.Write)); //SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(OfficeLeave), "HRComment", objectHandle, SecurityOperations.Write));
         bool isManager = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace, typeof(OfficeLeave), "ManagerComment", null, SecurityOperations.Write));//SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(OfficeLeave), "ManagerComment", objectHandle, SecurityOperations.Write));
         if (isDirector || isHR || isManager)
         {
            isGranted = true;
         }
         ApproveOffiveLeaveAction.Enabled.SetItemValue("Security", isGranted);
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

      private void ApproveOffiveLeaveAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
      {
         IObjectSpace objectSpace = Application.CreateObjectSpace();
         e.View = Application.CreateDetailView(Application.CreateObjectSpace(), OfficeLeaveApprovalParametersObject.CreateOfficeLeaveApprovalParameterObject());
         ((DetailView)e.View).ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
      }

      private void ApproveOffiveLeaveAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
      {
         IObjectSpace objectSpace = Application.CreateObjectSpace();
         Employee currentUser = objectSpace.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);

         OfficeLeave officeLeave = (OfficeLeave)View.CurrentObject;
         View.ObjectSpace.SetModified(officeLeave);
         if (officeLeave.Employee.Manager.Oid == currentUser.Oid)
         {
            officeLeave.Manager = ObjectSpace.GetObjectByKey<Employee>(currentUser.Oid);
            officeLeave.ManagerApproval = ((OfficeLeaveApprovalParametersObject)e.PopupWindow.View.CurrentObject).Approval;
            officeLeave.ManagerComment = ((OfficeLeaveApprovalParametersObject)e.PopupWindow.View.CurrentObject).Comment;
            officeLeave.ManagerApprovalDate = DateTime.Now;
         }

         SystemSetting setting = objectSpace.FindObject<SystemSetting>(null);
         foreach (MarbidRole role in currentUser.MarbidRoles)
         {
            if (setting.HRRole == role)
            {
               officeLeave.HRPersonnel = ObjectSpace.GetObjectByKey<Employee>(currentUser.Oid);
               officeLeave.HRApproval = ((OfficeLeaveApprovalParametersObject)e.PopupWindow.View.CurrentObject).Approval;
               officeLeave.HRComment = ((OfficeLeaveApprovalParametersObject)e.PopupWindow.View.CurrentObject).Comment;
               officeLeave.HRApprovalDate = DateTime.Now;
            }
         }

         if (officeLeave.Employee.Directorate.Manager.Oid == currentUser.Oid)
         {
            officeLeave.Director = ObjectSpace.GetObjectByKey<Employee>(currentUser.Oid);
            officeLeave.DirectorApproval = ((OfficeLeaveApprovalParametersObject)e.PopupWindow.View.CurrentObject).Approval;
            officeLeave.DirectorComment = ((OfficeLeaveApprovalParametersObject)e.PopupWindow.View.CurrentObject).Comment;
            officeLeave.DirectorApprovalDate = DateTime.Now;
         }

         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
         {
            View.ObjectSpace.CommitChanges();
         }
      }

      private void PostForApprovalAction_Execute(object sender, SimpleActionExecuteEventArgs e)
      {
         OfficeLeave officeLeave = (OfficeLeave)View.CurrentObject;
         View.ObjectSpace.SetModified(officeLeave);
         officeLeave.PostForApproval = true;

         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
         {
            View.ObjectSpace.CommitChanges();
         }
      }
   }
}
