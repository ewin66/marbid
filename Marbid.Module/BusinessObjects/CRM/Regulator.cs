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
    [ImageName("judge")]
    [NavigationItem("CRM")]
    public class Regulator : Organization
    {
        public Regulator(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [DevExpress.Xpo.AssociationAttribute("Regulations-Regulator")]
        public XPCollection<Marbid.Module.BusinessObjects.Library.Regulation> Regulations
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.Library.Regulation>("Regulations");
            }
        }
    }
}
