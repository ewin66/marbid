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
using DevExpress.ExpressApp.SystemModule;

namespace Marbid.Module.BusinessObjects.ReportCentral
{
  [DefaultClassOptions]
  [NavigationItem("Reports and Statistics")]
  public class Reporting : ReportDataV2
  {
    private bool _IsActive;
    private System.DateTime _createDate;
    private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
    private System.String _description;
    public Reporting(DevExpress.Xpo.Session session)
      : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      CreateDate = DateTime.Now;
      CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
    }
    protected override void OnSaving()
    {
      base.OnSaving();
      ModifiedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
      ModifyDate = DateTime.Now; 
    }
    [DevExpress.Xpo.AssociationAttribute("AllowedRoles-Reports")]
    public XPCollection<Marbid.Module.BusinessObjects.Administration.MarbidRole> AllowedRoles
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.Administration.MarbidRole>("AllowedRoles");
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

    Employee modifiedBy;
    public Employee ModifiedBy
    {
      get
      {
        return modifiedBy;
      }
      set
      {
        SetPropertyValue("ModifiedBy", ref modifiedBy, value);
      }
    }

    DateTime modifyDate;
    public DateTime ModifyDate
    {
      get
      {
        return modifyDate;
      }
      set
      {
        SetPropertyValue("ModifyDate", ref modifyDate, value);
      }
    }
    public bool IsActive
    {
      get
      {
        return _IsActive;
      }
      set
      {
        SetPropertyValue("IsActive", ref _IsActive, value);
      }
    }
    [Association("ReportStatisticCategory-Reporting")]
    public XPCollection<ReportStatisticCategory> Category
    {
      get
      {
        return GetCollection<ReportStatisticCategory>("Category");
      }
    }
  }
}
