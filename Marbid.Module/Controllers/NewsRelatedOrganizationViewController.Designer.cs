namespace Marbid.Module.Controllers
{
    partial class NewsRelatedOrganizationViewController
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
      this.ShowRelatedCustomerAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
      this.RemoveRelatedOrganizationAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
      // 
      // ShowRelatedCustomerAction
      // 
      this.ShowRelatedCustomerAction.AcceptButtonCaption = null;
      this.ShowRelatedCustomerAction.CancelButtonCaption = null;
      this.ShowRelatedCustomerAction.Caption = "Add Related Organization";
      this.ShowRelatedCustomerAction.Category = "Edit";
      this.ShowRelatedCustomerAction.ConfirmationMessage = null;
      this.ShowRelatedCustomerAction.Id = "AddRelatedOrganization";
      this.ShowRelatedCustomerAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.CRM.News);
      this.ShowRelatedCustomerAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
      this.ShowRelatedCustomerAction.ToolTip = null;
      this.ShowRelatedCustomerAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
      this.ShowRelatedCustomerAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ShowRelatedCustomerAction_CustomizePopupWindowParams);
      this.ShowRelatedCustomerAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ShowRelatedCustomerAction_Execute);
      // 
      // RemoveRelatedOrganizationAction
      // 
      this.RemoveRelatedOrganizationAction.AcceptButtonCaption = null;
      this.RemoveRelatedOrganizationAction.CancelButtonCaption = null;
      this.RemoveRelatedOrganizationAction.Caption = "Remove Related Organization";
      this.RemoveRelatedOrganizationAction.Category = "Edit";
      this.RemoveRelatedOrganizationAction.ConfirmationMessage = null;
      this.RemoveRelatedOrganizationAction.Id = "RemoveRelatedOrganization";
      this.RemoveRelatedOrganizationAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
      this.RemoveRelatedOrganizationAction.ToolTip = null;
      this.RemoveRelatedOrganizationAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
      this.RemoveRelatedOrganizationAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.RemoveRelatedOrganizationAction_CustomizePopupWindowParams);
      this.RemoveRelatedOrganizationAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.RemoveRelatedOrganizationAction_Execute);
      // 
      // NewsRelatedOrganizationViewController
      // 
      this.Actions.Add(this.ShowRelatedCustomerAction);
      this.Actions.Add(this.RemoveRelatedOrganizationAction);
      this.TargetObjectType = typeof(Marbid.Module.BusinessObjects.CRM.News);
      this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
      this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ShowRelatedCustomerAction;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction RemoveRelatedOrganizationAction;
    }
}
