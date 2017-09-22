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
  [DevExpress.Persistent.Base.ImageNameAttribute("building")]
  public class Organization : CustomerBase
  {
    private System.String _officialName;
    public Organization(DevExpress.Xpo.Session session)
      : base(session)
    {
    }
    [Association("Contacts-Organization")]
    public XPCollection<Marbid.Module.BusinessObjects.CRM.Contact> Contacts
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.CRM.Contact>("Contacts");
      }
    }
    [RuleRequiredField]
    public System.String OfficialName
    {
      get
      {
        return _officialName;
      }
      set
      {
        SetPropertyValue("OfficialName", ref _officialName, value);
      }
    }
    [DevExpress.Xpo.AssociationAttribute("Visits-Organization")]
    public XPCollection<Marbid.Module.BusinessObjects.CRM.Schedule> Visits
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.CRM.Schedule>("Visits");
      }
    }
    [DevExpress.Xpo.AssociationAttribute("RelatedOrganizations-InTheNews")]
    public XPCollection<Marbid.Module.BusinessObjects.CRM.News> InTheNews
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.CRM.News>("InTheNews");
      }
    }
    [DevExpress.Xpo.AssociationAttribute("Tasks-Organization")]
    public XPCollection<Marbid.Module.BusinessObjects.HRM.Task> Tasks
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.HRM.Task>("Tasks");
      }
    }
  }
}
