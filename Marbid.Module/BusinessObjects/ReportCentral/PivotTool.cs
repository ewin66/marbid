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

namespace Marbid.Module.BusinessObjects.ReportCentral
{
  [DefaultClassOptions]
  [NavigationItem("Reports and Statistics")]
  public class PivotTool : Analysis
  {
    private System.DateTime _createDate;
    private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
    private System.String _description;
    private System.Boolean _isActive;
    public PivotTool(DevExpress.Xpo.Session session)
      : base(session)
    {
    }
    [DevExpress.Xpo.AssociationAttribute("AllowedRoles-PivotTools")]
    public XPCollection<Marbid.Module.BusinessObjects.Administration.MarbidRole> AllowedRoles
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.Administration.MarbidRole>("AllowedRoles");
      }
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      CreateDate = DateTime.Now;
      CreatedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
    }
    public System.Boolean IsActive
    {
      get
      {
        return _isActive;
      }
      set
      {
        SetPropertyValue("IsActive", ref _isActive, value);
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
    [Association("ReportStatisticCategory-PivotTool")]
    public XPCollection<ReportStatisticCategory> Category
    {
      get
      {
        return GetCollection<ReportStatisticCategory>("Category");
      }
    }
  }
}
