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

namespace Marbid.Module.BusinessObjects.Administration
{
    [DefaultClassOptions]
    public class FeedbackType : BaseObject
    {
        private System.String _description;
        private System.String _name;
        public FeedbackType(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
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
        [DevExpress.Xpo.SizeAttribute(5000)]
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
        [DevExpress.Xpo.AssociationAttribute("Feedbacks-FeedbackType")]
        public XPCollection<Marbid.Module.BusinessObjects.Administration.Feedback> Feedbacks
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.Administration.Feedback>("Feedbacks");
            }
        }
    }
}
