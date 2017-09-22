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
    public class BusinessCooperation : BaseObject
    {
        private System.Int16 _accountingQuarter;
        private System.Double _commission;
        private System.Double _claimPaid;
        private System.Double _grossPremium;
        private System.Int16 _accountingMonth;
        private System.Int16 _accountingYear;
        public BusinessCooperation(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public System.Int16 AccountingYear
        {
            get
            {
                return _accountingYear;
            }
            set
            {
                SetPropertyValue("AccountingYear", ref _accountingYear, value);
            }
        }
        public System.Int16 AccountingQuarter
        {
            get
            {
                return _accountingQuarter;
            }
            set
            {
                SetPropertyValue("AccountingQuarter", ref _accountingQuarter, value);
            }
        }
        public System.Int16 AccountingMonth
        {
            get
            {
                return _accountingMonth;
            }
            set
            {
                SetPropertyValue("AccountingMonth", ref _accountingMonth, value);
            }
        }
        public System.Double GrossPremium
        {
            get
            {
                return _grossPremium;
            }
            set
            {
                SetPropertyValue("GrossPremium", ref _grossPremium, value);
            }
        }
        public System.Double ClaimPaid
        {
            get
            {
                return _claimPaid;
            }
            set
            {
                SetPropertyValue("ClaimPaid", ref _claimPaid, value);
            }
        }
        public System.Double Commission
        {
            get
            {
                return _commission;
            }
            set
            {
                SetPropertyValue("Commission", ref _commission, value);
            }
        }
    }
}