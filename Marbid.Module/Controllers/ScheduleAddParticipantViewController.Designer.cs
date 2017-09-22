namespace Marbid.Module.Controllers
{
   partial class ScheduleAddParticipantViewController
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
         this.AddParticipantAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
         this.RemoveParticipantAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
         this.LockScheduleAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
         this.AddInternalParticipantAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
         this.AddMoM = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
         this.AssignCarAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
         // 
         // AddParticipantAction
         // 
         this.AddParticipantAction.AcceptButtonCaption = null;
         this.AddParticipantAction.CancelButtonCaption = null;
         this.AddParticipantAction.Caption = "Add Contacts";
         this.AddParticipantAction.Category = "Edit";
         this.AddParticipantAction.ConfirmationMessage = null;
         this.AddParticipantAction.Id = "AddParticipant";
         this.AddParticipantAction.ImageName = "groupadd";
         this.AddParticipantAction.TargetObjectsCriteria = "[CreatedBy.Oid] = CurrentUserId() And IsLocked = False";
         this.AddParticipantAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.CRM.Schedule);
         this.AddParticipantAction.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
         this.AddParticipantAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
         this.AddParticipantAction.ToolTip = "Add Organization Contacts to Schedule";
         this.AddParticipantAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
         this.AddParticipantAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.AddParticipantAction_CustomizePopupWindowParams);
         this.AddParticipantAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.AddParticipantAction_Execute);
         // 
         // RemoveParticipantAction
         // 
         this.RemoveParticipantAction.AcceptButtonCaption = null;
         this.RemoveParticipantAction.CancelButtonCaption = null;
         this.RemoveParticipantAction.Caption = "Remove Participant";
         this.RemoveParticipantAction.Category = "Edit";
         this.RemoveParticipantAction.ConfirmationMessage = null;
         this.RemoveParticipantAction.Id = "RemoveParticipant";
         this.RemoveParticipantAction.ImageName = "groupremove";
         this.RemoveParticipantAction.TargetObjectsCriteria = "[CreatedBy.Oid] = CurrentUserId() And IsLocked = False";
         this.RemoveParticipantAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.CRM.Schedule);
         this.RemoveParticipantAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
         this.RemoveParticipantAction.ToolTip = "Remove Participants From Schedule";
         this.RemoveParticipantAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
         this.RemoveParticipantAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.RemoveParticipantAction_CustomizePopupWindowParams);
         this.RemoveParticipantAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.RemoveParticipantAction_Execute);
         // 
         // LockScheduleAction
         // 
         this.LockScheduleAction.Caption = "Lock Schedule";
         this.LockScheduleAction.Category = "RecordEdit";
         this.LockScheduleAction.ConfirmationMessage = "Once a schedule is locked, it can not be edited anymore.\r\nDo you want to continue" +
    "?";
         this.LockScheduleAction.Id = "LockSchedule";
         this.LockScheduleAction.ImageName = "BO_Security";
         this.LockScheduleAction.TargetObjectsCriteria = "[CreatedBy.Oid] = CurrentUserId() And IsLocked = False";
         this.LockScheduleAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.CRM.Schedule);
         this.LockScheduleAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
         this.LockScheduleAction.ToolTip = null;
         this.LockScheduleAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
         this.LockScheduleAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.LockScheduleAction_Execute);
         // 
         // AddInternalParticipantAction
         // 
         this.AddInternalParticipantAction.AcceptButtonCaption = null;
         this.AddInternalParticipantAction.CancelButtonCaption = null;
         this.AddInternalParticipantAction.Caption = "Add Employees";
         this.AddInternalParticipantAction.Category = "Edit";
         this.AddInternalParticipantAction.ConfirmationMessage = null;
         this.AddInternalParticipantAction.Id = "AddInternalParticipants";
         this.AddInternalParticipantAction.ImageName = "groupadd";
         this.AddInternalParticipantAction.TargetObjectsCriteria = "[CreatedBy.Oid] = CurrentUserId() And IsLocked = False";
         this.AddInternalParticipantAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.CRM.Schedule);
         this.AddInternalParticipantAction.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
         this.AddInternalParticipantAction.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
         this.AddInternalParticipantAction.ToolTip = "Add Marein Employees to Schedule";
         this.AddInternalParticipantAction.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
         this.AddInternalParticipantAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.AddInternalParticipantAction_CustomizePopupWindowParams);
         this.AddInternalParticipantAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.AddInternalParticipantAction_Execute);
         // 
         // AddMoM
         // 
         this.AddMoM.Caption = "Add New MoM";
         this.AddMoM.Category = "RecordEdit";
         this.AddMoM.ConfirmationMessage = null;
         this.AddMoM.Id = "AddMoM";
         this.AddMoM.ImageName = "BO_Report";
         this.AddMoM.TargetObjectsCriteria = "[CreatedBy.Oid] = CurrentUserId() or [ScheduleParticipants][[Participant.Oid] = C" +
    "urrentUserId()]";
         this.AddMoM.TargetObjectType = typeof(Marbid.Module.BusinessObjects.CRM.Schedule);
         this.AddMoM.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
         this.AddMoM.ToolTip = null;
         this.AddMoM.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
         this.AddMoM.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
         // 
         // AssignCarAction
         // 
         this.AssignCarAction.AcceptButtonCaption = null;
         this.AssignCarAction.CancelButtonCaption = null;
         this.AssignCarAction.Caption = "Assign Car";
         this.AssignCarAction.Category = "RecordEdit";
         this.AssignCarAction.ConfirmationMessage = null;
         this.AssignCarAction.Id = "AssignCarAction";
         this.AssignCarAction.ImageName = "car";
         this.AssignCarAction.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
         this.AssignCarAction.TargetObjectsCriteria = "[CarOrder] = True And IsCurrentUserInRole([DriverManagerRoleName])";
         this.AssignCarAction.TargetObjectType = typeof(Marbid.Module.BusinessObjects.CRM.Schedule);
         this.AssignCarAction.ToolTip = "Assign car and driver to selected schedule";
         this.AssignCarAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.AssignCarAction_CustomizePopupWindowParams);
         this.AssignCarAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.AssignCarAction_Execute);
         // 
         // ScheduleAddParticipantViewController
         // 
         this.Actions.Add(this.AddParticipantAction);
         this.Actions.Add(this.RemoveParticipantAction);
         this.Actions.Add(this.LockScheduleAction);
         this.Actions.Add(this.AddInternalParticipantAction);
         this.Actions.Add(this.AddMoM);
         this.Actions.Add(this.AssignCarAction);
         this.TargetObjectType = typeof(Marbid.Module.BusinessObjects.CRM.Schedule);
         this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
         this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

      }

      #endregion

      private DevExpress.ExpressApp.Actions.PopupWindowShowAction AddParticipantAction;
      private DevExpress.ExpressApp.Actions.PopupWindowShowAction RemoveParticipantAction;
      private DevExpress.ExpressApp.Actions.SimpleAction LockScheduleAction;
      private DevExpress.ExpressApp.Actions.PopupWindowShowAction AddInternalParticipantAction;
      private DevExpress.ExpressApp.Actions.SimpleAction AddMoM;
      private DevExpress.ExpressApp.Actions.PopupWindowShowAction AssignCarAction;
   }
}
