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
    [XafDefaultProperty("Title")]
    [ImageName("degree")]
    public class Degree : BaseObject
    {
        private Marbid.Module.BusinessObjects.AcademicDegree _academicDegree;
        private System.String _abbreviation;
        private System.String _title;
        public Degree(DevExpress.Xpo.Session session)
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
        public Marbid.Module.BusinessObjects.AcademicDegree AcademicDegree
        {
            get
            {
                return _academicDegree;
            }
            set
            {
                SetPropertyValue("AcademicDegree", ref _academicDegree, value);
            }
        }
    }
}
