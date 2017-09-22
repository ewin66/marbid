namespace Marbid.Win
{
   partial class MarbidWindowsFormsApplication
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
         this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
         this.module2 = new DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule();
         this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
         this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
         this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
         this.auditTrailModule = new DevExpress.ExpressApp.AuditTrail.AuditTrailModule();
         this.objectsModule = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
         this.chartModule = new DevExpress.ExpressApp.Chart.ChartModule();
         this.chartWindowsFormsModule = new DevExpress.ExpressApp.Chart.Win.ChartWindowsFormsModule();
         this.cloneObjectModule = new DevExpress.ExpressApp.CloneObject.CloneObjectModule();
         this.conditionalAppearanceModule = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
         this.dashboardsModule = new DevExpress.ExpressApp.Dashboards.DashboardsModule();
         this.dashboardsWindowsFormsModule = new DevExpress.ExpressApp.Dashboards.Win.DashboardsWindowsFormsModule();
         this.fileAttachmentsWindowsFormsModule = new DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule();
         this.htmlPropertyEditorWindowsFormsModule = new DevExpress.ExpressApp.HtmlPropertyEditor.Win.HtmlPropertyEditorWindowsFormsModule();
         this.kpiModule = new DevExpress.ExpressApp.Kpi.KpiModule();
         this.notificationsModule = new DevExpress.ExpressApp.Notifications.NotificationsModule();
         this.notificationsWindowsFormsModule = new DevExpress.ExpressApp.Notifications.Win.NotificationsWindowsFormsModule();
         this.pivotChartModuleBase = new DevExpress.ExpressApp.PivotChart.PivotChartModuleBase();
         this.pivotChartWindowsFormsModule = new DevExpress.ExpressApp.PivotChart.Win.PivotChartWindowsFormsModule();
         this.pivotGridModule = new DevExpress.ExpressApp.PivotGrid.PivotGridModule();
         this.pivotGridWindowsFormsModule = new DevExpress.ExpressApp.PivotGrid.Win.PivotGridWindowsFormsModule();
         this.reportsWindowsFormsModuleV2 = new DevExpress.ExpressApp.ReportsV2.Win.ReportsWindowsFormsModuleV2();
         this.schedulerModuleBase = new DevExpress.ExpressApp.Scheduler.SchedulerModuleBase();
         this.schedulerWindowsFormsModule = new DevExpress.ExpressApp.Scheduler.Win.SchedulerWindowsFormsModule();
         this.scriptRecorderModuleBase = new DevExpress.ExpressApp.ScriptRecorder.ScriptRecorderModuleBase();
         this.scriptRecorderWindowsFormsModule = new DevExpress.ExpressApp.ScriptRecorder.Win.ScriptRecorderWindowsFormsModule();
         this.stateMachineModule = new DevExpress.ExpressApp.StateMachine.StateMachineModule();
         this.treeListEditorsModuleBase = new DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase();
         this.treeListEditorsWindowsFormsModule = new DevExpress.ExpressApp.TreeListEditors.Win.TreeListEditorsWindowsFormsModule();
         this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
         this.validationWindowsFormsModule = new DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule();
         this.viewVariantsModule = new DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule();
         this.workflowModule = new DevExpress.ExpressApp.Workflow.WorkflowModule();
         this.workflowWindowsFormsModule = new DevExpress.ExpressApp.Workflow.Win.WorkflowWindowsFormsModule();
         this.reportsModuleV2 = new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2();
         this.module3 = new Marbid.Module.MarbidModule();
         this.module4 = new Marbid.Module.Win.MarbidWindowsFormsModule();
         this.xpandValidationModule1 = new Xpand.ExpressApp.Validation.XpandValidationModule();
         this.xpandSecurityModule1 = new Xpand.ExpressApp.Security.XpandSecurityModule();
         this.logicModule1 = new Xpand.ExpressApp.Logic.LogicModule();
         this.emailModule1 = new Xpand.ExpressApp.Email.EmailModule();
         ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
         // 
         // securityStrategyComplex1
         // 
         this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
         this.securityStrategyComplex1.RoleType = typeof(Marbid.Module.BusinessObjects.Administration.MarbidRole);
         this.securityStrategyComplex1.SupportNavigationPermissionsForTypes = false;
         this.securityStrategyComplex1.UserType = typeof(Marbid.Module.BusinessObjects.Administration.Employee);
         // 
         // authenticationStandard1
         // 
         this.authenticationStandard1.LogonParametersType = typeof(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters);
         // 
         // auditTrailModule
         // 
         this.auditTrailModule.AuditDataItemPersistentType = typeof(DevExpress.Persistent.BaseImpl.AuditDataItemPersistent);
         // 
         // dashboardsModule
         // 
         this.dashboardsModule.DashboardDataType = typeof(Marbid.Module.BusinessObjects.ReportCentral.BIDashboard);
         // 
         // dashboardsWindowsFormsModule
         // 
         this.dashboardsWindowsFormsModule.DesignerFormStyle = null;
         // 
         // notificationsModule
         // 
         this.notificationsModule.CanAccessPostponedItems = false;
         this.notificationsModule.NotificationsRefreshInterval = System.TimeSpan.Parse("00:05:00");
         this.notificationsModule.NotificationsStartDelay = System.TimeSpan.Parse("00:00:05");
         this.notificationsModule.ShowDismissAllAction = false;
         this.notificationsModule.ShowNotificationsWindow = true;
         this.notificationsModule.ShowRefreshAction = false;
         // 
         // pivotChartModuleBase
         // 
         this.pivotChartModuleBase.DataAccessMode = DevExpress.ExpressApp.CollectionSourceDataAccessMode.Client;
         this.pivotChartModuleBase.ShowAdditionalNavigation = false;
         // 
         // stateMachineModule
         // 
         this.stateMachineModule.StateMachineStorageType = typeof(DevExpress.ExpressApp.StateMachine.Xpo.XpoStateMachine);
         // 
         // validationModule
         // 
         this.validationModule.AllowValidationDetailsAccess = true;
         this.validationModule.IgnoreWarningAndInformationRules = false;
         // 
         // workflowModule
         // 
         this.workflowModule.RunningWorkflowInstanceInfoType = typeof(DevExpress.ExpressApp.Workflow.Xpo.XpoRunningWorkflowInstanceInfo);
         this.workflowModule.StartWorkflowRequestType = typeof(DevExpress.ExpressApp.Workflow.Xpo.XpoStartWorkflowRequest);
         this.workflowModule.UserActivityVersionType = typeof(DevExpress.ExpressApp.Workflow.Versioning.XpoUserActivityVersion);
         this.workflowModule.WorkflowControlCommandRequestType = typeof(DevExpress.ExpressApp.Workflow.Xpo.XpoWorkflowInstanceControlCommandRequest);
         this.workflowModule.WorkflowDefinitionType = typeof(DevExpress.ExpressApp.Workflow.Xpo.XpoWorkflowDefinition);
         this.workflowModule.WorkflowInstanceKeyType = typeof(DevExpress.Workflow.Xpo.XpoInstanceKey);
         this.workflowModule.WorkflowInstanceType = typeof(DevExpress.Workflow.Xpo.XpoWorkflowInstance);
         // 
         // reportsModuleV2
         // 
         this.reportsModuleV2.EnableInplaceReports = true;
         this.reportsModuleV2.ReportDataType = typeof(Marbid.Module.BusinessObjects.ReportCentral.Reporting);
         this.reportsModuleV2.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
         // 
         // MarbidWindowsFormsApplication
         // 
         this.ApplicationName = "Marbid";
         this.Modules.Add(this.module1);
         this.Modules.Add(this.module2);
         this.Modules.Add(this.auditTrailModule);
         this.Modules.Add(this.objectsModule);
         this.Modules.Add(this.chartModule);
         this.Modules.Add(this.cloneObjectModule);
         this.Modules.Add(this.conditionalAppearanceModule);
         this.Modules.Add(this.dashboardsModule);
         this.Modules.Add(this.validationModule);
         this.Modules.Add(this.kpiModule);
         this.Modules.Add(this.notificationsModule);
         this.Modules.Add(this.pivotChartModuleBase);
         this.Modules.Add(this.pivotGridModule);
         this.Modules.Add(this.reportsModuleV2);
         this.Modules.Add(this.schedulerModuleBase);
         this.Modules.Add(this.scriptRecorderModuleBase);
         this.Modules.Add(this.stateMachineModule);
         this.Modules.Add(this.treeListEditorsModuleBase);
         this.Modules.Add(this.viewVariantsModule);
         this.Modules.Add(this.workflowModule);
         this.Modules.Add(this.xpandValidationModule1);
         this.Modules.Add(this.securityModule1);
         this.Modules.Add(this.xpandSecurityModule1);
         this.Modules.Add(this.logicModule1);
         this.Modules.Add(this.emailModule1);
         this.Modules.Add(this.module3);
         this.Modules.Add(this.chartWindowsFormsModule);
         this.Modules.Add(this.dashboardsWindowsFormsModule);
         this.Modules.Add(this.fileAttachmentsWindowsFormsModule);
         this.Modules.Add(this.htmlPropertyEditorWindowsFormsModule);
         this.Modules.Add(this.notificationsWindowsFormsModule);
         this.Modules.Add(this.pivotChartWindowsFormsModule);
         this.Modules.Add(this.pivotGridWindowsFormsModule);
         this.Modules.Add(this.reportsWindowsFormsModuleV2);
         this.Modules.Add(this.schedulerWindowsFormsModule);
         this.Modules.Add(this.scriptRecorderWindowsFormsModule);
         this.Modules.Add(this.treeListEditorsWindowsFormsModule);
         this.Modules.Add(this.validationWindowsFormsModule);
         this.Modules.Add(this.workflowWindowsFormsModule);
         this.Modules.Add(this.module4);
         this.Security = this.securityStrategyComplex1;
         this.UseOldTemplates = false;
         this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.MarbidWindowsFormsApplication_DatabaseVersionMismatch);
         this.CustomizeLanguagesList += new System.EventHandler<DevExpress.ExpressApp.CustomizeLanguagesListEventArgs>(this.MarbidWindowsFormsApplication_CustomizeLanguagesList);
         ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

      }

      #endregion

      private DevExpress.ExpressApp.SystemModule.SystemModule module1;
      private DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule module2;
      private Marbid.Module.MarbidModule module3;
      private Marbid.Module.Win.MarbidWindowsFormsModule module4;
      private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
      private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex1;
      private DevExpress.ExpressApp.Security.AuthenticationStandard authenticationStandard1;
      private DevExpress.ExpressApp.AuditTrail.AuditTrailModule auditTrailModule;
      private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule objectsModule;
      private DevExpress.ExpressApp.Chart.ChartModule chartModule;
      private DevExpress.ExpressApp.Chart.Win.ChartWindowsFormsModule chartWindowsFormsModule;
      private DevExpress.ExpressApp.CloneObject.CloneObjectModule cloneObjectModule;
      private DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule conditionalAppearanceModule;
      private DevExpress.ExpressApp.Dashboards.DashboardsModule dashboardsModule;
      private DevExpress.ExpressApp.Dashboards.Win.DashboardsWindowsFormsModule dashboardsWindowsFormsModule;
      private DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule fileAttachmentsWindowsFormsModule;
      private DevExpress.ExpressApp.HtmlPropertyEditor.Win.HtmlPropertyEditorWindowsFormsModule htmlPropertyEditorWindowsFormsModule;
      private DevExpress.ExpressApp.Kpi.KpiModule kpiModule;
      private DevExpress.ExpressApp.Notifications.NotificationsModule notificationsModule;
      private DevExpress.ExpressApp.Notifications.Win.NotificationsWindowsFormsModule notificationsWindowsFormsModule;
      private DevExpress.ExpressApp.PivotChart.PivotChartModuleBase pivotChartModuleBase;
      private DevExpress.ExpressApp.PivotChart.Win.PivotChartWindowsFormsModule pivotChartWindowsFormsModule;
      private DevExpress.ExpressApp.PivotGrid.PivotGridModule pivotGridModule;
      private DevExpress.ExpressApp.PivotGrid.Win.PivotGridWindowsFormsModule pivotGridWindowsFormsModule;
      private DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModuleV2;
      private DevExpress.ExpressApp.ReportsV2.Win.ReportsWindowsFormsModuleV2 reportsWindowsFormsModuleV2;
      private DevExpress.ExpressApp.Scheduler.SchedulerModuleBase schedulerModuleBase;
      private DevExpress.ExpressApp.Scheduler.Win.SchedulerWindowsFormsModule schedulerWindowsFormsModule;
      private DevExpress.ExpressApp.ScriptRecorder.ScriptRecorderModuleBase scriptRecorderModuleBase;
      private DevExpress.ExpressApp.ScriptRecorder.Win.ScriptRecorderWindowsFormsModule scriptRecorderWindowsFormsModule;
      private DevExpress.ExpressApp.StateMachine.StateMachineModule stateMachineModule;
      private DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase treeListEditorsModuleBase;
      private DevExpress.ExpressApp.TreeListEditors.Win.TreeListEditorsWindowsFormsModule treeListEditorsWindowsFormsModule;
      private DevExpress.ExpressApp.Validation.ValidationModule validationModule;
      private DevExpress.ExpressApp.Validation.Win.ValidationWindowsFormsModule validationWindowsFormsModule;
      private DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule viewVariantsModule;
      private DevExpress.ExpressApp.Workflow.WorkflowModule workflowModule;
      private DevExpress.ExpressApp.Workflow.Win.WorkflowWindowsFormsModule workflowWindowsFormsModule;
      private Xpand.ExpressApp.Validation.XpandValidationModule xpandValidationModule1;
      private Xpand.ExpressApp.Security.XpandSecurityModule xpandSecurityModule1;
      private Xpand.ExpressApp.Logic.LogicModule logicModule1;
      private Xpand.ExpressApp.Email.EmailModule emailModule1;
   }
}
