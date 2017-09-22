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
  [ImageName("meeting")]
  [NavigationItem("CRM")]
  public class MeetingRoom : BaseObject
  {
    private System.Int16 _capacity;
    private System.String _name;
    private System.Boolean _isActive;
    public MeetingRoom(DevExpress.Xpo.Session session)
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
    public System.Int16 Capacity
    {
      get
      {
        return _capacity;
      }
      set
      {
        SetPropertyValue("Capacity", ref _capacity, value);
      }
    }
    [Association("Schedules-MeetingRoom")]
    public XPCollection<Marbid.Module.BusinessObjects.CRM.Schedule> Schedules
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.CRM.Schedule>("Schedules");
      }
    }
    public Boolean IsActive
    {
      get { return _isActive; }
      set { SetPropertyValue("IsActive", ref _isActive, value); }
    }
  }
}