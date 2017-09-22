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
    [DevExpress.Persistent.Base.ImageNameAttribute("Province")]
    public class Province : BaseObject
    {
        private System.String _bPSCode;
        private System.String _name;
        public Province(DevExpress.Xpo.Session session)
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
        [DevExpress.Xpo.AssociationAttribute("Regions-Province")]
        public XPCollection<Marbid.Module.BusinessObjects.Administration.Region> Regions
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.Administration.Region>("Regions");
            }
        }
        public System.String BPSCode
        {
            get
            {
                return _bPSCode;
            }
            set
            {
                SetPropertyValue("BPSCode", ref _bPSCode, value);
            }
        }
    }
}
