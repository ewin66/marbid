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

namespace Marbid.Module.BusinessObjects.Library
{
    [DefaultClassOptions]
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Audit_ChangeHistory")]
    [NavigationItem(false)]
    public class OperatingStep : BaseObject
    {
        private Marbid.Module.BusinessObjects.Library.StandardOperatingProcedure _standardOperatingProcedure;
        private System.DateTime _modifyDate;
        private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
        private System.DateTime _createDate;
        private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
        private System.Int16 _stepNumber;
        private System.String _description;
        public OperatingStep(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public System.Int16 StepNumber
        {
            get
            {
                return _stepNumber;
            }
            set
            {
                SetPropertyValue("StepNumber", ref _stepNumber, value);
            }
        }
        [DevExpress.Xpo.SizeAttribute(4000)]
        public System.String Description
        {
            get
            {
                return _description;
            }
            set
            {
                SetPropertyValue("Description", ref _description, value);
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
        [DevExpress.Xpo.AssociationAttribute("OperatingSteps-StandardOperatingProcedure")]
        public Marbid.Module.BusinessObjects.Library.StandardOperatingProcedure StandardOperatingProcedure
        {
            get
            {
                return _standardOperatingProcedure;
            }
            set
            {
                SetPropertyValue("StandardOperatingProcedure", ref _standardOperatingProcedure, value);
            }
        }
    }
}
