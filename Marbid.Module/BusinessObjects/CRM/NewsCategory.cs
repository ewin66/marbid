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

namespace Marbid.Module.BusinessObjects.CRM
{
    [DefaultClassOptions]
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Category")]
    [NavigationItem("CRM")]
    public class NewsCategory : BaseObject
    {
        private System.Int16 _index;
        private System.String _description;
        private System.String _name;
        public NewsCategory(DevExpress.Xpo.Session session)
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
        [Size(SizeAttribute.Unlimited)]
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
        [DevExpress.Xpo.AssociationAttribute("News-NewsCategory")]
        public XPCollection<Marbid.Module.BusinessObjects.CRM.News> News
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.CRM.News>("News");
            }
        }
        public System.Int16 Index
        {
            get
            {
                return _index;
            }
            set
            {
                SetPropertyValue("Index", ref _index, value);
            }
        }
    }
}
