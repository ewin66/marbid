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
    public class Department : WorkUnit
    {
        private Marbid.Module.BusinessObjects.Administration.Division _division;
        public Department(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [DevExpress.Xpo.AssociationAttribute("Departments-Division")]
        public Marbid.Module.BusinessObjects.Administration.Division Division
        {
            get
            {
                return _division;
            }
            set
            {
                SetPropertyValue("Division", ref _division, value);
            }
        }
    }
}