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
    [NavigationItem("Library")]
    public class CorporateResourceCategory : BaseObject
    {
        private System.String _description;
        private System.String _name;
        public CorporateResourceCategory(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [RuleRequiredField]
        [DevExpress.Xpo.SizeAttribute(200)]
        public System.String Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetPropertyValue("Name", ref _name, value);
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
    }
}