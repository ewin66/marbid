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
    [DevExpress.Persistent.Base.ImageNameAttribute("SubRegion")]
    public class SubRegion : BaseObject
    {
        private System.String _bPSCode;
        private Marbid.Module.BusinessObjects.Administration.Region _region;
        private System.String _name;
        public SubRegion(DevExpress.Xpo.Session session)
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
        [DevExpress.Xpo.AssociationAttribute("SubRegions-Region")]
        public Marbid.Module.BusinessObjects.Administration.Region Region
        {
            get
            {
                return _region;
            }
            set
            {
                SetPropertyValue("Region", ref _region, value);
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
