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
using Marbid.Module.BusinessObjects.Inventory;
using Marbid.Module.BusinessObjects;
using Marbid.Module.BusinessObjects.Administration;
namespace Marbid.Module.Controllers
{
  // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
  public partial class InventoryController : ViewController
  {
    public InventoryController()
    {
      InitializeComponent();
      // Target required Views (via the TargetXXX properties) and create their Actions.
      TargetObjectType = typeof(LogisticRequisition);
    }
    protected override void OnActivated()
    {
      base.OnActivated();
      // Perform various tasks depending on the target View.
      
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

    private void ApprovalAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
    {
      if (((LogisticRequisitionApprovalParametersObject)e.PopupWindow.View.CurrentObject).Approve == LogisticRequisitionApprovalParametersObject.LogisticApproval.Undefined)
        throw new UserFriendlyException("You have to select Approve or Not Approve!");
      Employee currentUser = ObjectSpace.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
      LogisticRequisition currentRequisition = (LogisticRequisition)View.CurrentObject;
      currentRequisition.AuthorizationDate = DateTime.Now;
      currentRequisition.AuthorizationNote = ((LogisticRequisitionApprovalParametersObject)e.PopupWindow.View.CurrentObject).Comment;
      currentRequisition.Status = (RequisitonStatus)((LogisticRequisitionApprovalParametersObject)e.PopupWindow.View.CurrentObject).Approve;
      currentRequisition.AuthorizedBy = currentUser;
      View.ObjectSpace.CommitChanges();
    }

    private void ApprovalAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
    {
      IObjectSpace objectSpace = Application.CreateObjectSpace();
      e.View = Application.CreateDetailView(Application.CreateObjectSpace(), LogisticRequisitionApprovalParametersObject.CreateLogisticRequisitionApprovalParametersObject());
      ((DetailView)e.View).ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
    }
  }
}
