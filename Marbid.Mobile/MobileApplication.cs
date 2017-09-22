using System;
using System.Configuration;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Mobile;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;

namespace Marbid.Mobile {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWebWebApplicationMembersTopicAll.aspx
    public partial class MarbidMobileApplication : MobileApplication {
        private DevExpress.ExpressApp.SystemModule.SystemModule module1;
        private DevExpress.ExpressApp.Mobile.SystemModule.SystemMobileModule module2;
        private Marbid.Module.MarbidModule module3;
        private Marbid.Module.Mobile.MarbidMobileModule module4;
        private DevExpress.ExpressApp.Security.SecurityModule securityModule1;
        private DevExpress.ExpressApp.Security.SecurityStrategyComplex securityStrategyComplex1;
        private DevExpress.ExpressApp.Security.AuthenticationStandard authenticationStandard1;
        private DevExpress.ExpressApp.AuditTrail.AuditTrailModule auditTrailModule;
        private DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule objectsModule;
        private DevExpress.ExpressApp.Chart.ChartModule chartModule;
        private DevExpress.ExpressApp.CloneObject.CloneObjectModule cloneObjectModule;
        private DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule conditionalAppearanceModule;
        private DevExpress.ExpressApp.Dashboards.DashboardsModule dashboardsModule;
        private DevExpress.ExpressApp.FileAttachments.Mobile.FileAttachmentsMobileModule fileAttachmentsMobileModule;
        private DevExpress.ExpressApp.Kpi.KpiModule kpiModule;
        private DevExpress.ExpressApp.Notifications.NotificationsModule notificationsModule;
        private DevExpress.ExpressApp.PivotChart.PivotChartModuleBase pivotChartModuleBase;
        private DevExpress.ExpressApp.PivotGrid.PivotGridModule pivotGridModule;
        private DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModuleV2;
        private DevExpress.ExpressApp.ReportsV2.Mobile.ReportsMobileModuleV2 reportsMobileModuleV2;
        private DevExpress.ExpressApp.Scheduler.SchedulerModuleBase schedulerModuleBase;
        private DevExpress.ExpressApp.ScriptRecorder.ScriptRecorderModuleBase scriptRecorderModuleBase;
        private DevExpress.ExpressApp.StateMachine.StateMachineModule stateMachineModule;
        private DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase treeListEditorsModuleBase;
        private DevExpress.ExpressApp.Validation.ValidationModule validationModule;
        private DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule viewVariantsModule;
    private Xpand.ExpressApp.Validation.XpandValidationModule xpandValidationModule1;
    private Xpand.ExpressApp.Security.XpandSecurityModule xpandSecurityModule1;
    private Xpand.ExpressApp.Logic.LogicModule logicModule1;
    private Xpand.ExpressApp.Email.EmailModule emailModule1;
    private DevExpress.ExpressApp.Workflow.WorkflowModule workflowModule;

        public MarbidMobileApplication() {
			SecurityAdapterHelper.Enable();
		    Tracing.Initialize();
            if(ConfigurationManager.ConnectionStrings["ConnectionString"] != null) {
                ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
            if(System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
            InitializeComponent();
        }
        protected override void SetLogonParametersForUIBuilder(object logonParameters) {
            base.SetLogonParametersForUIBuilder(logonParameters);
            ((AuthenticationStandardLogonParameters)logonParameters).UserName = "Admin";
            ((AuthenticationStandardLogonParameters)logonParameters).Password = "";
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProvider = new SecuredObjectSpaceProvider((SecurityStrategyComplex)Security, GetDataStoreProvider(args.ConnectionString, args.Connection), true);
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }
        private IXpoDataStoreProvider GetDataStoreProvider(string connectionString, System.Data.IDbConnection connection) {
            IXpoDataStoreProvider dataStoreProvider = null;
            if(!String.IsNullOrEmpty(connectionString)) {
                dataStoreProvider = new ConnectionStringDataStoreProvider(connectionString);
            }
            else if(connection != null) {
                dataStoreProvider = new ConnectionDataStoreProvider(connection);
            }
			return dataStoreProvider;
        }
        private void MarbidMobileApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
				string message = "The application cannot connect to the specified database, " +
					"because the database doesn't exist, its version is older " +
					"than that of the application or its schema does not match " +
					"the ORM data model structure. To avoid this error, use one " +
					"of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

                if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                }
                throw new InvalidOperationException(message);
            }
#endif
        }
        private void InitializeComponent() {
      this.module1 = new DevExpress.ExpressApp.SystemModule.SystemModule();
      this.module2 = new DevExpress.ExpressApp.Mobile.SystemModule.SystemMobileModule();
      this.module3 = new Marbid.Module.MarbidModule();
      this.module4 = new Marbid.Module.Mobile.MarbidMobileModule();
      this.securityModule1 = new DevExpress.ExpressApp.Security.SecurityModule();
      this.securityStrategyComplex1 = new DevExpress.ExpressApp.Security.SecurityStrategyComplex();
      this.authenticationStandard1 = new DevExpress.ExpressApp.Security.AuthenticationStandard();
      this.auditTrailModule = new DevExpress.ExpressApp.AuditTrail.AuditTrailModule();
      this.objectsModule = new DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule();
      this.chartModule = new DevExpress.ExpressApp.Chart.ChartModule();
      this.cloneObjectModule = new DevExpress.ExpressApp.CloneObject.CloneObjectModule();
      this.conditionalAppearanceModule = new DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule();
      this.dashboardsModule = new DevExpress.ExpressApp.Dashboards.DashboardsModule();
      this.fileAttachmentsMobileModule = new DevExpress.ExpressApp.FileAttachments.Mobile.FileAttachmentsMobileModule();
      this.kpiModule = new DevExpress.ExpressApp.Kpi.KpiModule();
      this.notificationsModule = new DevExpress.ExpressApp.Notifications.NotificationsModule();
      this.pivotChartModuleBase = new DevExpress.ExpressApp.PivotChart.PivotChartModuleBase();
      this.pivotGridModule = new DevExpress.ExpressApp.PivotGrid.PivotGridModule();
      this.reportsModuleV2 = new DevExpress.ExpressApp.ReportsV2.ReportsModuleV2();
      this.reportsMobileModuleV2 = new DevExpress.ExpressApp.ReportsV2.Mobile.ReportsMobileModuleV2();
      this.schedulerModuleBase = new DevExpress.ExpressApp.Scheduler.SchedulerModuleBase();
      this.scriptRecorderModuleBase = new DevExpress.ExpressApp.ScriptRecorder.ScriptRecorderModuleBase();
      this.stateMachineModule = new DevExpress.ExpressApp.StateMachine.StateMachineModule();
      this.treeListEditorsModuleBase = new DevExpress.ExpressApp.TreeListEditors.TreeListEditorsModuleBase();
      this.validationModule = new DevExpress.ExpressApp.Validation.ValidationModule();
      this.viewVariantsModule = new DevExpress.ExpressApp.ViewVariantsModule.ViewVariantsModule();
      this.workflowModule = new DevExpress.ExpressApp.Workflow.WorkflowModule();
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
      this.dashboardsModule.DashboardDataType = typeof(DevExpress.Persistent.BaseImpl.DashboardData);
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
      // reportsModuleV2
      // 
      this.reportsModuleV2.EnableInplaceReports = true;
      this.reportsModuleV2.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.ReportDataV2);
      this.reportsModuleV2.ReportStoreMode = DevExpress.ExpressApp.ReportsV2.ReportStoreModes.XML;
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
      // MarbidMobileApplication
      // 
      this.ApplicationName = "Marbid";
      this.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
      this.Modules.Add(this.module1);
      this.Modules.Add(this.securityModule1);
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
      this.Modules.Add(this.xpandSecurityModule1);
      this.Modules.Add(this.logicModule1);
      this.Modules.Add(this.emailModule1);
      this.Modules.Add(this.module3);
      this.Modules.Add(this.fileAttachmentsMobileModule);
      this.Modules.Add(this.reportsMobileModuleV2);
      this.Modules.Add(this.module4);
      this.Security = this.securityStrategyComplex1;
      this.DatabaseVersionMismatch += new System.EventHandler<DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs>(this.MarbidMobileApplication_DatabaseVersionMismatch);
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
    }
}
