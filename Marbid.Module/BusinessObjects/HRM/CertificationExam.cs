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
    public class CertificationExam : BaseObject
    {
        private Marbid.Module.BusinessObjects.HRM.Certification _certification;
        private System.String _description;
        private System.String _moduleName;
        public CertificationExam(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public System.String ModuleName
        {
            get
            {
                return _moduleName;
            }
            set
            {
                SetPropertyValue("ModuleName", ref _moduleName, value);
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
    }
}
