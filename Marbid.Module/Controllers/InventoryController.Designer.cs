namespace Marbid.Module.Controllers
{
  partial class InventoryController
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
      this.ApprovalAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
      // 
      // ApprovalAction
      // 
      this.ApprovalAction.AcceptButtonCaption = null;
      this.ApprovalAction.CancelButtonCaption = null;
      this.ApprovalAction.Caption = "Approval";
      this.ApprovalAction.Category = "RecordEdit";
      this.ApprovalAction.ConfirmationMessage = null;
      this.ApprovalAction.Id = "ApprovalAction";
      this.ApprovalAction.TargetObjectsCriteria = "[Status] = \'RequestApproval\' AND [RequestBy].[Manager].[Oid] = CurrentUserId() AN" +
    "D [AuthorizationLevel].[GroupIndex] <= [RequestBy].[Manager].[RankGroup].[GroupI" +
    "ndex]";
      this.ApprovalAction.TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueForAll;
      this.ApprovalAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.Inventory.LogisticRequisition);
      this.ApprovalAction.ToolTip = null;
      this.ApprovalAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ApprovalAction_CustomizePopupWindowParams);
      this.ApprovalAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ApprovalAction_Execute);
      // 
      // InventoryController
      // 
      this.Actions.Add(this.ApprovalAction);

    }

    #endregion

    private DevExpress.ExpressApp.Actions.PopupWindowShowAction ApprovalAction;
  }
}
