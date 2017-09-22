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
    public class Certification : BaseObject
    {
        private Marbid.Module.BusinessObjects.CRM.Organization _certifier;
        private System.String _description;
        private System.String _abbreviation;
        private System.String _title;
        public Certification(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public System.String Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetPropertyValue("Title", ref _title, value);
            }
        }
        public System.String Abbreviation
        {
            get
            {
                return _abbreviation;
            }
            set
            {
                SetPropertyValue("Abbreviation", ref _abbreviation, value);
            }
        }
        public Marbid.Module.BusinessObjects.CRM.Organization Certifier
        {
            get
            {
                return _certifier;
            }
            set
            {
                SetPropertyValue("Certifier", ref _certifier, value);
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
        [DevExpress.Xpo.AssociationAttribute("Exams-Certification")]
        [DevExpress.Xpo.AggregatedAttribute]
        public XPCollection<Marbid.Module.BusinessObjects.HRM.CertificationExam> Exams
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.HRM.CertificationExam>("Exams");
            }
        }
    }
}
