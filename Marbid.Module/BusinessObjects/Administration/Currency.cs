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
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Sale_v92")]
    public class Currency : BaseObject
    {
        private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
        private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
        private System.DateTime _modifyDate;
        private System.DateTime _createDate;
        private System.String _sign;
        private System.String _name;
        private System.String _currencyCode;
        public Currency(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [RuleUniqueValue]
        [RuleRequiredField]
        public System.String CurrencyCode
        {
            get
            {
                return _currencyCode;
            }
            set
            {
                SetPropertyValue("CurrencyCode", ref _currencyCode, value);
            }
        }
        [RuleRequiredField]
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
        public System.String Sign
        {
            get
            {
                return _sign;
            }
            set
            {
                SetPropertyValue("Sign", ref _sign, value);
            }
        }
        public Marbid.Module.BusinessObjects.Administration.Employee CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                SetPropertyValue("CreatedBy", ref _createdBy, value);
            }
        }
        public System.DateTime CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                SetPropertyValue("CreateDate", ref _createDate, value);
            }
        }
        public Marbid.Module.BusinessObjects.Administration.Employee ModifiedBy
        {
            get
            {
                return _modifiedBy;
            }
            set
            {
                SetPropertyValue("ModifiedBy", ref _modifiedBy, value);
            }
        }
        public System.DateTime ModifyDate
        {
            get
            {
                return _modifyDate;
            }
            set
            {
                SetPropertyValue("ModifyDate", ref _modifyDate, value);
            }
        }
        [DevExpress.Xpo.AssociationAttribute("Rates-Currency")]
        public XPCollection<Marbid.Module.BusinessObjects.Administration.ExchangeRate> Rates
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.Administration.ExchangeRate>("Rates");
            }
        }
    }
}
