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
    [NavigationItem(false)]
    public class CorporateResourceAttachment : DocumentBase
    {
        private Marbid.Module.BusinessObjects.Library.CorporateResource _corporateResource;
        public CorporateResourceAttachment(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [Association("Attachments-CorporateResource")]
        public Marbid.Module.BusinessObjects.Library.CorporateResource CorporateResource
        {
            get
            {
                return _corporateResource;
            }
            set
            {
                CorporateResource oldCorporateResource = CorporateResource; ;
                SetPropertyValue("CorporateResource", ref _corporateResource, value);
                if (!IsLoading && !IsSaving && oldCorporateResource != _corporateResource)
                {
                    oldCorporateResource = oldCorporateResource ?? _corporateResource;
                    oldCorporateResource.GetLatestFile(true);
                }
            }
        }
    }
}
