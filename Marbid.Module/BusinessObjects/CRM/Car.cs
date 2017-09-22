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
  [XafDefaultProperty("DisplayName")]
  [DevExpress.Persistent.Base.ImageNameAttribute("car")]
  public class Car : BaseObject
  {
    private Marbid.Module.BusinessObjects.CRM.Driver _defaultDriver;
    private readonly static string displayNameFormat = "{Brand} {Model} {LicensePlate}";
    private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
    private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
    private System.DateTime _modifyDate;
    private System.DateTime _createDate;
    private System.DateTime _licenseExpiryDate;
    private System.DateTime _acquiredDate;
    private System.Int16 _buildYear;
    private System.String _model;
    private System.String _brand;
    private Marbid.Module.BusinessObjects.Administration.Employee _assignedTo;
    private System.String _licensePlate;
    public Car(DevExpress.Xpo.Session session)
      : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      CreateDate = DateTime.Now;
      CreatedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
    }
    protected override void OnSaving()
    {
      base.OnSaving();
      ModifyDate = DateTime.Now;
      ModifiedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
    }
    [Persistent]
    public string DisplayName
    {
      get
      {
        return ObjectFormatter.Format(displayNameFormat, this, EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty);
      }
    }
    [RuleRequiredField]
    public System.String Brand
    {
      get
      {
        return _brand;
      }
      set
      {
        SetPropertyValue("Brand", ref _brand, value);
      }
    }
    [RuleRequiredField]
    public System.String Model
    {
      get
      {
        return _model;
      }
      set
      {
        SetPropertyValue("Model", ref _model, value);
      }
    }
    public System.String LicensePlate
    {
      get
      {
        return _licensePlate;
      }
      set
      {
        SetPropertyValue("LicensePlate", ref _licensePlate, value);
      }
    }
    public System.DateTime LicenseExpiryDate
    {
      get
      {
        return _licenseExpiryDate;
      }
      set
      {
        SetPropertyValue("LicenseExpiryDate", ref _licenseExpiryDate, value);
      }
    }
    public Marbid.Module.BusinessObjects.Administration.Employee AssignedTo
    {
      get
      {
        return _assignedTo;
      }
      set
      {
        SetPropertyValue("AssignedTo", ref _assignedTo, value);
      }
    }
    public System.Int16 BuildYear
    {
      get
      {
        return _buildYear;
      }
      set
      {
        SetPropertyValue("BuildYear", ref _buildYear, value);
      }
    }
    public System.DateTime AcquiredDate
    {
      get
      {
        return _acquiredDate;
      }
      set
      {
        SetPropertyValue("AcquiredDate", ref _acquiredDate, value);
      }
    }
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
    [DevExpress.Xpo.AssociationAttribute("Schedules-AssignedCar")]
    public XPCollection<Marbid.Module.BusinessObjects.CRM.Schedule> Schedules
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.CRM.Schedule>("Schedules");
      }
    }
    public Marbid.Module.BusinessObjects.CRM.Driver DefaultDriver
    {
      get
      {
        return _defaultDriver;
      }
      set
      {
        if (_defaultDriver == value)
          return;
        Marbid.Module.BusinessObjects.CRM.Driver prevDefaultDriver = _defaultDriver;
        _defaultDriver = value;
        if (IsLoading)
          return;
        if (prevDefaultDriver != null && prevDefaultDriver.Car == this)
          prevDefaultDriver.Car = null;
        if (_defaultDriver != null)
          _defaultDriver.Car = this;
        OnChanged("DefaultDriver");
      }
    }
  }
}
