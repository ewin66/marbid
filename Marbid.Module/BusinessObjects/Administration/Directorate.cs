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
    public class Directorate : WorkUnit
    {
        public Directorate(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [DevExpress.Xpo.AssociationAttribute("Divisions-Directorate")]
        public XPCollection<Marbid.Module.BusinessObjects.Administration.Division> Divisions
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.Administration.Division>("Divisions");
            }
        }
    }
}