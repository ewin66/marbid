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
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Task")]
    public class EmployeeExam : BaseObject
    {
        private Marbid.Module.BusinessObjects.Administration.Employee _employee;
        private Marbid.Module.BusinessObjects.ExamStatus _status;
        private System.DateTime _examDate;
        private Marbid.Module.BusinessObjects.HRM.CertificationExam _module;
        private Marbid.Module.BusinessObjects.HRM.Certification _certification;
        private System.String _remark;
        public EmployeeExam(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public Marbid.Module.BusinessObjects.HRM.Certification Certification
        {
            get
            {
                return _certification;
            }
            set
            {
                SetPropertyValue("Certification", ref _certification, value);
            }
        }
        public Marbid.Module.BusinessObjects.HRM.CertificationExam Module
        {
            get
            {
                return _module;
            }
            set
            {
                SetPropertyValue("Module", ref _module, value);
            }
        }
        public System.DateTime ExamDate
        {
            get
            {
                return _examDate;
            }
            set
            {
                SetPropertyValue("ExamDate", ref _examDate, value);
            }
        }
        public Marbid.Module.BusinessObjects.ExamStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                SetPropertyValue("Status", ref _status, value);
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
        [DevExpress.Xpo.AssociationAttribute("Exams-Employee")]
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
