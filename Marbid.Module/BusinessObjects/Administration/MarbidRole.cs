using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Security.Strategy;
using Marbid.Module.BusinessObjects.ReportCentral;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

namespace Marbid.Module.BusinessObjects.Administration
{
    [DefaultClassOptions]
    [ImageName("BO_Role")]
    public class MarbidRole : PermissionPolicyRoleBase, IPermissionPolicyRoleWithUsers
    {
        public MarbidRole(DevExpress.Xpo.Session session)
          : base(session)
        {
        }

        [Association("Employees-MarbidRoles")]
        public XPCollection<Employee> Employees
        {
            get
            {
                return GetCollection<Employee>("Employees");
            }
        }
        IEnumerable<IPermissionPolicyUser> IPermissionPolicyRoleWithUsers.Users
        {
            get { return Employees.OfType<IPermissionPolicyUser>(); }
        }

        [DevExpress.Xpo.AssociationAttribute("AllowedRoles-Reports")]
        public XPCollection<Marbid.Module.BusinessObjects.ReportCentral.Reporting> Reports
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.ReportCentral.Reporting>("Reports");
            }
        }
        [DevExpress.Xpo.AssociationAttribute("AllowedRoles-PivotTools")]
        public XPCollection<Marbid.Module.BusinessObjects.ReportCentral.PivotTool> PivotTools
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.ReportCentral.PivotTool>("PivotTools");
            }
        }
        [DevExpress.Xpo.AssociationAttribute("AllowedRoles-BIDashboard")]
        public XPCollection<Marbid.Module.BusinessObjects.ReportCentral.BIDashboard> BIDashboards
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.ReportCentral.BIDashboard>("BIDashboards");
            }
        }

        [DevExpress.Xpo.AssociationAttribute("AllowedRoles-ReportCentral")]
        public XPCollection<Marbid.Module.BusinessObjects.ReportCentral.ReportCentral> DataExplorer
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.ReportCentral.ReportCentral>("DataExplorer");
            }
        }
    }
}
