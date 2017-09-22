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
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Contract")]
    public class EmployeeCertification : BaseObject
    {
        private System.Drawing.Image _certificate;
        private Marbid.Module.BusinessObjects.Administration.Employee _employee;
        private System.String _remark;
        private System.DateTime _expiryDate;
        private System.DateTime _certifyDate;
        private Marbid.Module.BusinessObjects.HRM.Certification _certification;
        public EmployeeCertification(DevExpress.Xpo.Session session)
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
        public System.DateTime CertifyDate
        {
            get
            {
                return _certifyDate;
            }
            set
            {
                SetPropertyValue("CertifyDate", ref _certifyDate, value);
            }
        }
        public System.DateTime ExpiryDate
        {
            get
            {
                return _expiryDate;
            }
            set
            {
                SetPropertyValue("ExpiryDate", ref _expiryDate, value);
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
        [DevExpress.Xpo.AssociationAttribute("Certifications-Employee")]
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
        [DevExpress.Xpo.ValueConverterAttribute(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        public System.Drawing.Image Certificate
        {
            get
            {
                return _certificate;
            }
            set
            {
                SetPropertyValue("Certificate", ref _certificate, value);
            }
        }
    }
}
