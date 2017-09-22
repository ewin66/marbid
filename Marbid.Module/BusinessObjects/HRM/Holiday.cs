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

namespace Marbid.Module.BusinessObjects.HRM
{
    [DefaultClassOptions]
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Appointment")]
    [XafDefaultProperty("HolidayName")]
    public class Holiday : BaseObject
    {
        private System.DateTime _holidayDate;
        private System.String _holidayName;
        public Holiday(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [RuleRequiredField]
        public System.DateTime HolidayDate
        {
            get
            {
                return _holidayDate;
            }
            set
            {
                SetPropertyValue("HolidayDate", ref _holidayDate, value);
            }
        }
        [RuleRequiredField]
        public System.String HolidayName
        {
            get
            {
                return _holidayName;
            }
            set
            {
                SetPropertyValue("HolidayName", ref _holidayName, value);
            }
        }
    }
}