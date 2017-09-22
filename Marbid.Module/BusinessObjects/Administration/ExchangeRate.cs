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
    [RuleCombinationOfPropertiesIsUnique("CurrencyExchangeUniqueCriteria", DefaultContexts.Save, "Month, Year, Currency")]
    [DevExpress.Persistent.Base.ImageNameAttribute("Exchange")]
    public class ExchangeRate : BaseObject
    { 
        private System.Double _rate;
        private System.Int16 _year;
        private System.DateTime _modifyDate;
        private System.DateTime _createDate;
        private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
        private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
        private System.DateTime _exchangeDate;
        private Marbid.Module.BusinessObjects.Administration.Currency _currency;
        private Marbid.Module.BusinessObjects.MonthName _month;
        public ExchangeRate(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Month = (MonthName)DateTime.Today.Month;
            Year = (Int16)DateTime.Today.Year;
            CreateDate = DateTime.Now;
            CreatedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
        }
        protected override void OnSaving()
        {
            base.OnSaving();
            ModifiedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
            ModifyDate = DateTime.Now;
        }
        [DevExpress.Xpo.AssociationAttribute("Rates-Currency")]
        public Marbid.Module.BusinessObjects.Administration.Currency Currency
        {
            get
            {
                return _currency;
            }
            set
            {
                SetPropertyValue("Currency", ref _currency, value);
            }
        }
        public Marbid.Module.BusinessObjects.MonthName Month
        {
            get
            {
                return _month;
            }
            set
            {
                SetPropertyValue("Month", ref _month, value);
            }
        }
        public System.Int16 Year
        {
            get
            {
                return _year;
            }
            set
            {
                SetPropertyValue("Year", ref _year, value);
            }
        }
        public System.DateTime ExchangeDate
        {
            get
            {
                return _exchangeDate;
            }
            set
            {
                SetPropertyValue("ExchangeDate", ref _exchangeDate, value);
            }
        }
        public System.Double Rate
        {
            get
            {
                return _rate;
            }
            set
            {
                SetPropertyValue("Rate", ref _rate, value);
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
    }
}
