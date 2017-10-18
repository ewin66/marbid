using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.ExpressApp.ReportsV2.Win;
using DevExpress.ExpressApp;

namespace Marbid.Module.Win.Controllers
{
    public class ReportDesignerCustomizationController : ObjectViewController<ListView, IReportDataV2>
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<WinReportServiceController>().DesignFormCreated += ReportDesignerCustomizationController_DesignFormCreated;
        }
        void ReportDesignerCustomizationController_DesignFormCreated(object sender, DesignFormEventArgs e)
        {
            e.DesignForm.DesignMdiController.SqlWizardSettings.EnableCustomSql = true;
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            Frame.GetController<WinReportServiceController>().DesignFormCreated -= ReportDesignerCustomizationController_DesignFormCreated;
        }
    }
}
