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
using Marbid.Module.CustomCodes;
using System.Collections;

namespace Marbid.Module.BusinessObjects.HRM
{
    [DefaultClassOptions]
    [RuleCriteria("OfficeLeave1", DefaultContexts.Save, "EndDate >= StartDate", "End Date must be equal or greater than Start Date", SkipNullOrEmptyValues = false)]
    [RuleCriteria("OfficeLeave2", DefaultContexts.Save, "TotalWorkDays <= AllowedOfficeLeaveDays", "Total work days exceeded maximum allowed office leave", SkipNullOrEmptyValues = false)]
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Appointment")]
    [XafDefaultProperty("DisplayName")]
    public class OfficeLeave : BaseObject
    {
        private System.Boolean _postForApproval;
        private System.DateTime _createDate;
        private Marbid.Module.BusinessObjects.Approval _directorApproval;
        private Marbid.Module.BusinessObjects.Approval _hRApproval;
        private Marbid.Module.BusinessObjects.Approval _managerApproval;
        private System.String _directorComment;
        private System.DateTime _directorApprovalDate;
        private Marbid.Module.BusinessObjects.Administration.Employee _director;
        private Marbid.Module.BusinessObjects.Administration.Employee _employee;
        private System.String _hRComment;
        private System.DateTime _hRApprovalDate;
        private Marbid.Module.BusinessObjects.Administration.Employee _hRPersonnel;
        private System.String _managerComment;
        private System.DateTime _managerApprovalDate;
        private Marbid.Module.BusinessObjects.Administration.Employee _manager;
        private System.String _reason;
        private System.DateTime _endDate;
        private System.DateTime _startDate;
        public OfficeLeave(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [NonPersistent]
        public string DisplayName
        {
            get
            {
                string mDisplayName;
                mDisplayName = string.Format("{0} ({1:dd MMM yy} to {2:dd MMM yy})", Employee.FullName, StartDate, EndDate);
                return mDisplayName;
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            CreateDate = DateTime.Now;
            Employee = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
            PostForApproval = false;
        }
        [NonPersistent,
        VisibleInDetailView(false),
        VisibleInListView(false),
        VisibleInLookupListView(false)]
        public string HRRoleName
        {
            get
            {
                SystemSetting setting = Session.FindObject<SystemSetting>(null);
                return setting.HRRole.Name;
            }
        }
        [NonPersistent,
        VisibleInDetailView(false),
        VisibleInListView(false),
        VisibleInLookupListView(false)]
        public int AllowedOfficeLeaveDays
        {
            get
            {
                SystemSetting setting = Session.FindObject<SystemSetting>(null);
                return setting.MaximumOfficeLeaveDays;
            }
        }
        [Persistent("TotalWorkDays")]
        private int mTotalWorkDays;
        [PersistentAlias("mTotalWorkDays")]
        public int TotalWorkDays
        {
            get
            {
                if (StartDate > EndDate)
                    return 0;
                XPCollection<Holiday> holidays = new XPCollection<Holiday>(Session, CriteriaOperator.Parse("GetYear([HolidayDate]) = ?", this.StartDate.Year));
                ArrayList HolidayList = new ArrayList();
                foreach (Holiday hday in holidays)
                {
                    HolidayList.Add(hday.HolidayDate);
                }
                DateTime[] holidayParam = HolidayList.ToArray(typeof(DateTime)) as DateTime[];
                Calculator mCalc = new Calculator();
                mTotalWorkDays = mCalc.BusinessDaysUntil(StartDate.Date, EndDate.Date, holidayParam);
                return mTotalWorkDays;
            }
        }
        [Persistent("TotalDays")]
        private int mTotalDays;
        [PersistentAlias("mTotalDays")]
        public int TotalDays
        {
            get
            {
                if (StartDate > EndDate)
                    return 0;
                mTotalDays = 1 + (int)(EndDate.Date - StartDate.Date).TotalDays;
                return mTotalDays;
            }
        }
        [DevExpress.Xpo.AssociationAttribute("OfficeLeave-Employee")]
        public Marbid.Module.BusinessObjects.Administration.Employee Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                Employee oldEmployee = _employee;
                SetPropertyValue("Employee", ref _employee, value);
                if (!IsLoading && !IsSaving && oldEmployee != _employee)
                {
                    oldEmployee = oldEmployee ?? _employee;
                    oldEmployee.UpdateDaysOffRemaining(true);
                }
            }
        }
        [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
        public System.DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (SetPropertyValue("StartDate", ref _startDate, value))
                {
                    OnChanged("TotalWorkDays");
                    OnChanged("TotalDays");
                }
            }
        }
        [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
        public System.DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (SetPropertyValue("EndDate", ref _endDate, value))
                {
                    OnChanged("TotalWorkDays");
                    OnChanged("TotalDays");
                }
            }
        }
        [DevExpress.Xpo.SizeAttribute(4000)]
        public System.String Reason
        {
            get
            {
                return _reason;
            }
            set
            {
                SetPropertyValue("Reason", ref _reason, value);
            }
        }
        public Marbid.Module.BusinessObjects.Approval ManagerApproval
        {
            get
            {
                return _managerApproval;
            }
            set
            {
                SetPropertyValue("ManagerApproval", ref _managerApproval, value);
            }
        }
        public System.DateTime ManagerApprovalDate
        {
            get
            {
                return _managerApprovalDate;
            }
            set
            {
                SetPropertyValue("ManagerApprovalDate", ref _managerApprovalDate, value);
            }
        }
        [DevExpress.Xpo.SizeAttribute(4000)]
        public System.String ManagerComment
        {
            get
            {
                return _managerComment;
            }
            set
            {
                SetPropertyValue("ManagerComment", ref _managerComment, value);
            }
        }
        public Marbid.Module.BusinessObjects.Administration.Employee Manager
        {
            get
            {
                return _manager;
            }
            set
            {
                SetPropertyValue("Manager", ref _manager, value);
            }
        }
        public Marbid.Module.BusinessObjects.Approval HRApproval
        {
            get
            {
                return _hRApproval;
            }
            set
            {
                SetPropertyValue("HRApproval", ref _hRApproval, value);
            }
        }
        public Marbid.Module.BusinessObjects.Administration.Employee HRPersonnel
        {
            get
            {
                return _hRPersonnel;
            }
            set
            {
                SetPropertyValue("HRPersonnel", ref _hRPersonnel, value);
            }
        }
        public System.DateTime HRApprovalDate
        {
            get
            {
                return _hRApprovalDate;
            }
            set
            {
                SetPropertyValue("HRApprovalDate", ref _hRApprovalDate, value);
            }
        }
        [DevExpress.Xpo.SizeAttribute(4000)]
        public System.String HRComment
        {
            get
            {
                return _hRComment;
            }
            set
            {
                SetPropertyValue("HRComment", ref _hRComment, value);
            }
        }
        public Marbid.Module.BusinessObjects.Approval DirectorApproval
        {
            get
            {
                return _directorApproval;
            }
            set
            {
                SetPropertyValue("DirectorApproval", ref _directorApproval, value);
            }
        }
        public Marbid.Module.BusinessObjects.Administration.Employee Director
        {
            get
            {
                return _director;
            }
            set
            {
                SetPropertyValue("Director", ref _director, value);
            }
        }
        public System.DateTime DirectorApprovalDate
        {
            get
            {
                return _directorApprovalDate;
            }
            set
            {
                SetPropertyValue("DirectorApprovalDate", ref _directorApprovalDate, value);
            }
        }
        [DevExpress.Xpo.SizeAttribute(4000)]
        public System.String DirectorComment
        {
            get
            {
                return _directorComment;
            }
            set
            {
                SetPropertyValue("DirectorComment", ref _directorComment, value);
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
        public System.Boolean PostForApproval
        {
            get
            {
                return _postForApproval;
            }
            set
            {
                SetPropertyValue("PostForApproval", ref _postForApproval, value);
            }
        }

        [NonPersistent, VisibleInDetailView(false), VisibleInListView(false)]
        public string EmployeeEmail
        {
            get { return Employee.CorporateEmail; }
        }

        [NonPersistent, VisibleInDetailView(false), VisibleInListView(false)]
        public string ManagerEmail
        {
            get { return Employee.Manager.CorporateEmail; }
        }

        [NonPersistent, VisibleInDetailView(true), VisibleInListView(false)]
        public string ManagerManagerEmail
        {
            get { return Employee.Manager.Manager.CorporateEmail; }
        }
    }
    [DomainComponent]
    [RuleCriteria("OfficeLeaveParameterCriteria", DefaultContexts.Save, "Approval <> 'NotYetApproved'", "Approval needed", SkipNullOrEmptyValues = false)]
    public class OfficeLeaveApprovalParametersObject
    {
        public static Type OfficeLeaveApprovalType = typeof(OfficeLeaveApprovalParametersObject);
        public static OfficeLeaveApprovalParametersObject CreateOfficeLeaveApprovalParameterObject()
        {
            return (OfficeLeaveApprovalParametersObject)ReflectionHelper.CreateObject(OfficeLeaveApprovalType);
        }
        public Approval Approval { get; set; }
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField]
        public string Comment { get; set; }
    }
}