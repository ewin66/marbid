namespace Marbid.Module.Controllers
{
  partial class NewsAddShortDescriptionViewController
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
      this.AddShortDescriptionAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
      // 
      // AddShortDescriptionAction
      // 
      this.AddShortDescriptionAction.AcceptButtonCaption = null;
      this.AddShortDescriptionAction.CancelButtonCaption = null;
      this.AddShortDescriptionAction.Caption = "Add Short Description";
      this.AddShortDescriptionAction.Category = "Edit";
      this.AddShortDescriptionAction.ConfirmationMessage = null;
      this.AddShortDescriptionAction.Id = "AddShortDescription";
      this.AddShortDescriptionAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.CRM.News);
      this.AddShortDescriptionAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
      this.AddShortDescriptionAction.ToolTip = null;
      this.AddShortDescriptionAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
      this.AddShortDescriptionAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.AddShortDescriptionAction_CustomizePopupWindowParams);
      this.AddShortDescriptionAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.AddShortDescriptionAction_Execute);
      // 
      // NewsAddShortDescriptionViewController
      // 
      this.Actions.Add(this.AddShortDescriptionAction);
      this.TargetObjectType = typeof(Marbid.Module.BusinessObjects.CRM.News);
      this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
      this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

    }

    #endregion

    private DevExpress.ExpressApp.Actions.PopupWindowShowAction AddShortDescriptionAction;
  }
}
