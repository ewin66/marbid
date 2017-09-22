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
    [DevExpress.ExpressApp.DC.XafDisplayNameAttribute("Promotion/Mutation")]
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Resume")]
    public class Promotion : BaseObject
    {
        private Marbid.Module.BusinessObjects.Administration.Employee _employee;
        private System.String _remark;
        private Marbid.Module.BusinessObjects.HRM.FunctionalPosition _functionalPosition;
        private Marbid.Module.BusinessObjects.HRM.StructuralPosition _structuralPosition;
        private Marbid.Module.BusinessObjects.Administration.Department _department;
        private Marbid.Module.BusinessObjects.Administration.Division _division;
        private Marbid.Module.BusinessObjects.Administration.Directorate _directorate;
        private Marbid.Module.BusinessObjects.PromotionType _type;
        private System.DateTime _decreeDate;
        public Promotion(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public System.DateTime DecreeDate
        {
            get
            {
                return _decreeDate;
            }
            set
            {
                SetPropertyValue("DecreeDate", ref _decreeDate, value);
            }
        }
        public Marbid.Module.BusinessObjects.PromotionType Type
        {
            get
            {
                return _type;
            }
            set
            {
                SetPropertyValue("Type", ref _type, value);
            }
        }
        [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
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
        [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
        [DevExpress.Persistent.Base.DataSourcePropertyAttribute("Directorate.Divisions")]
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
        [DevExpress.Persistent.Base.DataSourcePropertyAttribute("Division.Departments")]
        public Marbid.Module.BusinessObjects.Administration.Department Department
        {
            get
            {
                return _department;
            }
            set
            {
                SetPropertyValue("Department", ref _department, value);
            }
        }
        public Marbid.Module.BusinessObjects.HRM.StructuralPosition StructuralPosition
        {
            get
            {
                return _structuralPosition;
            }
            set
            {
                SetPropertyValue("StructuralPosition", ref _structuralPosition, value);
            }
        }
        public Marbid.Module.BusinessObjects.HRM.FunctionalPosition FunctionalPosition
        {
            get
            {
                return _functionalPosition;
            }
            set
            {
                SetPropertyValue("FunctionalPosition", ref _functionalPosition, value);
            }
        }
        [DevExpress.Xpo.SizeAttribute(4000)]
        public System.String Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                SetPropertyValue("Remark", ref _remark, value);
            }
        }
        [DevExpress.Xpo.AssociationAttribute("PromotionMutation-Employee")]
        public Marbid.Module.BusinessObjects.Administration.Employee Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                SetPropertyValue("Employee", ref _employee, value);
            }
        }
    }
}
