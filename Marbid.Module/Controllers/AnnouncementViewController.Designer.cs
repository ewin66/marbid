namespace Marbid.Module.Controllers
{
    partial class AnnouncementViewController
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
            this.AddRecipientAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.AddRecepientAllAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.AddRecipientByRankGroupAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // AddRecipientAction
            // 
            this.AddRecipientAction.AcceptButtonCaption = null;
            this.AddRecipientAction.CancelButtonCaption = null;
            this.AddRecipientAction.Caption = "Add Recipient";
            this.AddRecipientAction.Category = "Edit";
            this.AddRecipientAction.ConfirmationMessage = null;
            this.AddRecipientAction.Id = "AddRecipientAction";
            this.AddRecipientAction.ImageName = "groupadd";
            this.AddRecipientAction.TargetObjectsCriteria = "[CreatedBy.Oid] = CurrentUserId() And Published=False";
            this.AddRecipientAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.HRM.Announcement);
            this.AddRecipientAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.AddRecipientAction.ToolTip = null;
            this.AddRecipientAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.AddRecipientAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.AddRecipientAction_CustomizePopupWindowParams);
            this.AddRecipientAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.AddRecipientAction_Execute);
            // 
            // AddRecepientAllAction
            // 
            this.AddRecepientAllAction.Caption = "Add Recepient (All User)";
            this.AddRecepientAllAction.Category = "RecordEdit";
            this.AddRecepientAllAction.ConfirmationMessage = null;
            this.AddRecepientAllAction.Id = "AddRecepientAllAction";
            this.AddRecepientAllAction.ImageName = "groupadd";
            this.AddRecepientAllAction.TargetObjectsCriteria = "[CreatedBy.Oid] = CurrentUserId() And Published = False";
            this.AddRecepientAllAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.HRM.Announcement);
            this.AddRecepientAllAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.AddRecepientAllAction.ToolTip = null;
            this.AddRecepientAllAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.AddRecepientAllAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.AddRecepientAllAction_Execute);
            // 
            // AddRecipientByRankGroupAction
            // 
            this.AddRecipientByRankGroupAction.AcceptButtonCaption = null;
            this.AddRecipientByRankGroupAction.CancelButtonCaption = null;
            this.AddRecipientByRankGroupAction.Caption = "Add Recipient (By Rank Group)";
            this.AddRecipientByRankGroupAction.Category = "Edit";
            this.AddRecipientByRankGroupAction.ConfirmationMessage = null;
            this.AddRecipientByRankGroupAction.Id = "AddRecipientByRankGroupAction";
            this.AddRecipientByRankGroupAction.ImageName = "groupadd";
            this.AddRecipientByRankGroupAction.TargetObjectsCriteria = "[CreatedBy.Oid] = CurrentUserId() And Published=False";
            this.AddRecipientByRankGroupAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.HRM.Announcement);
            this.AddRecipientByRankGroupAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.AddRecipientByRankGroupAction.ToolTip = null;
            this.AddRecipientByRankGroupAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.AddRecipientByRankGroupAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.AddRecipientByRankGroupAction_CustomizePopupWindowParams);
            this.AddRecipientByRankGroupAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.AddRecipientByRankGroupAction_Execute);
            // 
            // AnnouncementViewController
            // 
            this.Actions.Add(this.AddRecipientAction);
            this.Actions.Add(this.AddRecepientAllAction);
            this.Actions.Add(this.AddRecipientByRankGroupAction);
            this.TargetObjectType = typeof(Marbid.Module.BusinessObjects.HRM.Announcement);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction AddRecipientAction;
        private DevExpress.ExpressApp.Actions.SimpleAction AddRecepientAllAction;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction AddRecipientByRankGroupAction;
    }
}
