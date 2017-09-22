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

namespace Marbid.Module.BusinessObjects.CRM
{
    [DefaultClassOptions]
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Category")]
    public class InsuranceDivision : BaseObject
    {
        private Marbid.Module.BusinessObjects.Administration.Directorate _directorate;
        private System.String _code;
        private System.String _name;
        public InsuranceDivision(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public System.String Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetPropertyValue("Name", ref _name, value);
            }
        }
        public System.String Code
        {
            get
            {
                return _code;
            }
            set
            {
                SetPropertyValue("Code", ref _code, value);
            }
        }
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
        [DevExpress.Xpo.AssociationAttribute("InsuranceDivisions-Companies")]
        public XPCollection<Marbid.Module.BusinessObjects.CRM.Company> Companies
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.CRM.Company>("Companies");
            }
        }
    }
}
