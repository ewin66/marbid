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
using DevExpress.ExpressApp.ConditionalAppearance;

namespace Marbid.Module.BusinessObjects.Administration
{
  [DefaultClassOptions]
  [NavigationItem(false)]
  [CreatableItem(false)]
  [DevExpress.Persistent.Base.ImageNameAttribute("BO_Category")]
  [Appearance("WorkUnitDisable", Enabled = false, TargetItems = "CreatedBy,CreateDate,ModifyDate,ModifiedDate")]
  public class WorkUnit : BaseObject
  {
    private Marbid.Module.BusinessObjects.Administration.Employee _manager;
    private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
    private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
    private System.DateTime _modifyDate;
    private System.DateTime _createDate;
    private System.String _name;
    private System.String _code;
    public WorkUnit(DevExpress.Xpo.Session session)
      : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      CreatedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
      CreateDate = DateTime.Now;
    }
    protected override void OnSaving()
    {
      base.OnSaving();
      ModifiedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
      ModifyDate = DateTime.Now;
    }
    [DevExpress.Xpo.SizeAttribute(5)]
    public System.String Code
    {
      get
      {
        return _code;
      }
      set
      {
        SetPropertyValue("Code", ref _code, value);
      }
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
    [ReadOnly(true)]
    public System.DateTime CreateDate
    {
      get
      {
        return _createDate;
      }
      set
      {
        SetPropertyValue("CreateDate", ref _createDate, value);
      }
    }
    [ReadOnly(true)]
    public Marbid.Module.BusinessObjects.Administration.Employee CreatedBy
    {
      get
      {
        return _createdBy;
      }
      set
      {
        SetPropertyValue("CreatedBy", ref _createdBy, value);
      }
    }
    [ReadOnly(true)]
    public System.DateTime ModifyDate
    {
      get
      {
        return _modifyDate;
      }
      set
      {
        SetPropertyValue("ModifyDate", ref _modifyDate, value);
      }
    }
    [ReadOnly(true)]
    public Marbid.Module.BusinessObjects.Administration.Employee ModifiedBy
    {
      get
      {
        return _modifiedBy;
      }
      set
      {
        SetPropertyValue("ModifiedBy", ref _modifiedBy, value);
      }
    }
    public Marbid.Module.BusinessObjects.Administration.Employee Manager
    {
      get
      {
        return _manager;
      }
      set
      {
        SetPropertyValue("Manager", ref _manager, value);
      }
    }
  }
}
