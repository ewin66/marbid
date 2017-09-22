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
using Marbid.Module.BusinessObjects.Administration;
namespace Marbid.Module.BusinessObjects.MiscObject
{
    [DefaultClassOptions]
    public class AccessLog : BaseObject
    { 
        public AccessLog(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private string _ViewId;
        private string _ActionDescription;
        private DateTime _AccessDate;
        private Employee _Employee;

        public Employee Employee
        {
            get
            {
                return _Employee;
            }
            set
            {
                SetPropertyValue("Employee", ref _Employee, value);
            }
        }


        public DateTime AccessDate
        {
            get
            {
                return _AccessDate;
            }
            set
            {
                SetPropertyValue("AccessDate", ref _AccessDate, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ViewId
        {
            get
            {
                return _ViewId;
            }
            set
            {
                SetPropertyValue("ViewId", ref _ViewId, value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ActionDescription
        {
            get
            {
                return _ActionDescription;
            }
            set
            {
                SetPropertyValue("ActionDescription", ref _ActionDescription, value);
            }
        }


    }
}