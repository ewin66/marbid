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
    [DevExpress.Persistent.Base.ImageNameAttribute("Region")]
    public class Region : BaseObject
    {
        private System.String _bPSCode;
        private Marbid.Module.BusinessObjects.Administration.Province _province;
        private System.String _name;
        public Region(DevExpress.Xpo.Session session)
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
        public Marbid.Module.BusinessObjects.Administration.Province Province
        {
            get
            {
                return _province;
            }
            set
            {
                SetPropertyValue("Province", ref _province, value);
            }
        }
        [DevExpress.Xpo.AssociationAttribute("SubRegions-Region")]
        public XPCollection<Marbid.Module.BusinessObjects.Administration.SubRegion> SubRegions
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.Administration.SubRegion>("SubRegions");
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
