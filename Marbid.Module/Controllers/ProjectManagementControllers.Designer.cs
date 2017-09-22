namespace Marbid.Module.Controllers
{
  partial class ProjectManagementControllers
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
      this.AddProjectParticipantAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
      // 
      // AddProjectParticipantAction
      // 
      this.AddProjectParticipantAction.AcceptButtonCaption = null;
      this.AddProjectParticipantAction.CancelButtonCaption = null;
      this.AddProjectParticipantAction.Caption = "Add Project Participant Action";
      this.AddProjectParticipantAction.ConfirmationMessage = null;
      this.AddProjectParticipantAction.Id = "AddProjectParticipantAction";
      this.AddProjectParticipantAction.ImageName = "BO_Employee";
      this.AddProjectParticipantAction.TargetObjectsCriteria = "[CreatedBy.Oid]=CurrentUserId() or [ProjectManager.Oid] = CurrentUserId()";
      this.AddProjectParticipantAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.HRM.ProjectManagement);
      this.AddProjectParticipantAction.ToolTip = null;
      this.AddProjectParticipantAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.AddProjectParticipantAction_Execute);
      // 
      // ProjectManagementControllers
      // 
      this.Actions.Add(this.AddProjectParticipantAction);
      this.TargetObjectType = typeof(Marbid.Module.BusinessObjects.HRM.ProjectManagement);

    }

    #endregion

    private DevExpress.ExpressApp.Actions.PopupWindowShowAction AddProjectParticipantAction;
  }
}
