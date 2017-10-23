using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.Dashboards.Win;
using DevExpress.DashboardWin;
using DevExpress.DashboardWin.Native;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.DashboardCommon;

namespace Marbid.Module.Win.Controllers
{
    public class DashboardDesignerCustomizationController : ObjectViewController<ListView, IDashboardData>
    {
        WinShowDashboardDesignerController winShowDashboardDesignerController;
        protected override void OnActivated()
        {
            base.OnActivated();
            winShowDashboardDesignerController = Frame.GetController<WinShowDashboardDesignerController>();
            if (winShowDashboardDesignerController != null)
            {
                winShowDashboardDesignerController.DashboardDesignerManager = new CustomDashboardDesignerManager(Application);
            }
            Frame.GetController<WinShowDashboardDesignerController>().DashboardDesignerManager.DashboardDesignerCreated += DashboardDesignerManager_DashboardDesignerCreated;
        }

        private void DashboardDesignerManager_DashboardDesignerCreated(object sender, DashboardDesignerShownEventArgs e)
        {
            e.DashboardDesigner.DataSourceWizard.SqlWizardSettings.EnableCustomSql = true;
            e.DashboardDesigner.DashboardLoaded += DashboardDesigner_DashboardLoaded;
        }

        private void DashboardDesigner_DashboardLoaded(object sender, DashboardLoadedEventArgs e)
        {
            var dashboard = e.Dashboard;
            dashboard.DataSources.OfType<DashboardSqlDataSource>().ToList().ForEach(dataSource => {
                dataSource.ConnectionOptions.DbCommandTimeout = 10000;
            });
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            Frame.GetController<WinShowDashboardDesignerController>().DashboardDesignerManager.DashboardDesignerCreated -= DashboardDesignerManager_DashboardDesignerCreated;
        }
    }
    public class CustomDashboardDesignerManager : DashboardDesignerManager
    {
        protected override void OnDesignerShown(DashboardDesigner dashboardDesigner)
        {
            dashboardDesigner.DataSourceWizard.SqlWizardSettings.EnableCustomSql = true;
            dashboardDesigner.DataSourceWizard.SqlWizardSettings.DatabaseCredentialsSavingBehavior = DevExpress.DataAccess.Wizard.SensitiveInfoSavingBehavior.Prompt;
            base.OnDesignerShown(dashboardDesigner);
        }

        protected override void ProcessDashboardBeforeSaving(Dashboard dashboard) { }
        public CustomDashboardDesignerManager(XafApplication application) : base(application) { }
    }
}
