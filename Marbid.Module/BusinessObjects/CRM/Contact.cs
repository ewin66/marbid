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
using Marbid.Module.BusinessObjects.Administration;

namespace Marbid.Module.BusinessObjects.CRM
{
  [DefaultClassOptions]
  [DefaultProperty("FullName")]
  [ImageName("BO_Contact")]
  [NavigationItem("CRM")]
  public class Contact : People
  {
    private Marbid.Module.BusinessObjects.CRM.Organization _organization;
    public Contact(DevExpress.Xpo.Session session)
      : base(session)
    {
    }
    protected override void OnSaving()
    {
      base.OnSaving();
    }

    [DevExpress.Xpo.AssociationAttribute("Contacts-Organization")]
    public Marbid.Module.BusinessObjects.CRM.Organization Organization
    {
      get
      {
        return _organization;
      }
      set
      {
        SetPropertyValue("Organization", ref _organization, value);
      }
    }

    //string position;
    //[Size(SizeAttribute.DefaultStringMappingFieldSize)]
    //public string Position
    //{
    //  get
    //  {
    //    return position;
    //  }
    //  set
    //  {
    //    SetPropertyValue("Position", ref position, value);
    //  }
    //}

    private XPCollection<AuditDataItemPersistent> auditTrail;
    public XPCollection<AuditDataItemPersistent> AuditTrail
    {
      get
      {
        if (auditTrail == null)
        {
          auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
        }
        return auditTrail;
      }
    }
  }
}
