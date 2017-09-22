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
using Marbid.Module.BusinessObjects.General;

namespace Marbid.Module.BusinessObjects.CRM
{
    [DefaultClassOptions]
    [CreatableItem(false)]
    public class CustomerNote : Notes
    {
        private Marbid.Module.BusinessObjects.CRM.CustomerBase _customer;
        public CustomerNote(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [DevExpress.Xpo.AssociationAttribute("Notes-Customer")]
        public Marbid.Module.BusinessObjects.CRM.CustomerBase Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                SetPropertyValue("Customer", ref _customer, value);
            }
        }
    }
}
