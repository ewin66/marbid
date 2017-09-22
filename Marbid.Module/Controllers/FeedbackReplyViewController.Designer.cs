namespace Marbid.Module.Controllers
{
    partial class FeedbackReplyViewController
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
            this.replyFeedbackPopupWindowShowAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // replyFeedbackPopupWindowShowAction
            // 
            this.replyFeedbackPopupWindowShowAction.AcceptButtonCaption = null;
            this.replyFeedbackPopupWindowShowAction.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.replyFeedbackPopupWindowShowAction.CancelButtonCaption = null;
            this.replyFeedbackPopupWindowShowAction.Caption = "Reply";
            this.replyFeedbackPopupWindowShowAction.Category = "Edit";
            this.replyFeedbackPopupWindowShowAction.Id = "f98f304a-86ff-4d42-83e7-1e1b9f361ea1";
            this.replyFeedbackPopupWindowShowAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.Administration.Feedback);
            this.replyFeedbackPopupWindowShowAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.replyFeedbackPopupWindowShowAction.ToolTip = null;
            this.replyFeedbackPopupWindowShowAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.replyFeedbackPopupWindowShowAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.replyFeedbackPopupWindowShowAction_CustomizePopupWindowParams);
            this.replyFeedbackPopupWindowShowAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.replyFeedbackPopupWindowShowAction_Execute);
            // 
            // FeedbackReplyViewController
            // 
            this.Actions.Add(this.replyFeedbackPopupWindowShowAction);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction replyFeedbackPopupWindowShowAction;
    }
}
