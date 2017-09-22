namespace Marbid.Module.Controllers
{
  partial class DocumentViewController
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
         this.VerifyAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
         this.DocumentRequestCheckOutAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
         // 
         // VerifyAction
         // 
         this.VerifyAction.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
         this.VerifyAction.Caption = "Verify";
         this.VerifyAction.Category = "RecordEdit";
         this.VerifyAction.ConfirmationMessage = "Make sure registration information match attached document.\r\nAre you sure you wan" +
    "t to mark this document as verified?";
         this.VerifyAction.Id = "VerifyDocumentAction";
         this.VerifyAction.TargetObjectsCriteria = "[IsVerified] = false and [DocumentTo.Oid] = CurrentUserId()";
         this.VerifyAction.ToolTip = null;
         this.VerifyAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.VerifyAction_Execute);
         // 
         // DocumentRequestCheckOutAction
         // 
         this.DocumentRequestCheckOutAction.Caption = "Checkout";
         this.DocumentRequestCheckOutAction.ConfirmationMessage = null;
         this.DocumentRequestCheckOutAction.Id = "DocumentRequestCheckOut";
         this.DocumentRequestCheckOutAction.ImageName = "BO_Checkout";
         this.DocumentRequestCheckOutAction.TargetObjectsCriteria = "[IsVerified]=true";
         this.DocumentRequestCheckOutAction.ToolTip = null;
         this.DocumentRequestCheckOutAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DocumentRequestCheckOutAction_Execute_1);
         // 
         // DocumentViewController
         // 
         this.Actions.Add(this.VerifyAction);
         this.Actions.Add(this.DocumentRequestCheckOutAction);

    }

    #endregion
      private DevExpress.ExpressApp.Actions.SimpleAction VerifyAction;
      private DevExpress.ExpressApp.Actions.SimpleAction DocumentRequestCheckOutAction;
   }
}
