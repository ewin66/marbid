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

namespace Marbid.Module.BusinessObjects.Administration
{
    [DefaultClassOptions]
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Category")]
    public class Division : WorkUnit
    {
        private Marbid.Module.BusinessObjects.Administration.Directorate _directorate;
        private System.Boolean _isBusinessUnit;
        public Division(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [DevExpress.Xpo.AssociationAttribute("Departments-Division")]
        public XPCollection<Marbid.Module.BusinessObjects.Administration.Department> Departments
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.Administration.Department>("Departments");
            }
        }
        public System.Boolean IsBusinessUnit
        {
            get
            {
                return _isBusinessUnit;
            }
            set
            {
                SetPropertyValue("IsBusinessUnit", ref _isBusinessUnit, value);
            }
        }
        [DevExpress.Xpo.AssociationAttribute("Divisions-Directorate")]
        public Marbid.Module.BusinessObjects.Administration.Directorate Directorate
        {
            get
            {
                return _directorate;
            }
            set
            {
                SetPropertyValue("Directorate", ref _directorate, value);
            }
        }
    }
}
