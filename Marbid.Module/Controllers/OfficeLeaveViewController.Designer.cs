namespace Marbid.Module.Controllers
{
  partial class OfficeLeaveViewController
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.ApproveOffiveLeaveAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
      this.PostForApprovalAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
      // 
      // ApproveOffiveLeaveAction
      // 
      this.ApproveOffiveLeaveAction.AcceptButtonCaption = null;
      this.ApproveOffiveLeaveAction.CancelButtonCaption = null;
      this.ApproveOffiveLeaveAction.Caption = "Approve Office Leave";
      this.ApproveOffiveLeaveAction.ConfirmationMessage = null;
      this.ApproveOffiveLeaveAction.Id = "ApproveOfficeLeave";
      this.ApproveOffiveLeaveAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.HRM.OfficeLeave);
      this.ApproveOffiveLeaveAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
      this.ApproveOffiveLeaveAction.ToolTip = null;
      this.ApproveOffiveLeaveAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
      this.ApproveOffiveLeaveAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ApproveOffiveLeaveAction_CustomizePopupWindowParams);
      this.ApproveOffiveLeaveAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ApproveOffiveLeaveAction_Execute);
      // 
      // PostForApprovalAction
      // 
      this.PostForApprovalAction.Caption = "Post For Approval";
      this.PostForApprovalAction.ConfirmationMessage = "Once leave application is posted for approval it can\'t be edited,\r\nDo you want to" +
    " continue?";
      this.PostForApprovalAction.Id = "PostForApproval";
      this.PostForApprovalAction.TargetObjectsCriteria = "[Employee.Oid] = CurrentUserId() And PostForApproval = false";
      this.PostForApprovalAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.HRM.OfficeLeave);
      this.PostForApprovalAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
      this.PostForApprovalAction.ToolTip = null;
      this.PostForApprovalAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
      this.PostForApprovalAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.PostForApprovalAction_Execute);
      // 
      // OfficeLeaveViewController
      // 
      this.Actions.Add(this.ApproveOffiveLeaveAction);
      this.Actions.Add(this.PostForApprovalAction);
      this.TargetObjectType = typeof(Marbid.Module.BusinessObjects.HRM.OfficeLeave);
      this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
      this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

    }

    #endregion

    private DevExpress.ExpressApp.Actions.PopupWindowShowAction ApproveOffiveLeaveAction;
    private DevExpress.ExpressApp.Actions.SimpleAction PostForApprovalAction;
  }
}
