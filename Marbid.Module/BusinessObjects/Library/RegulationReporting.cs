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

namespace Marbid.Module.BusinessObjects.Library
{
  [DefaultClassOptions]
  [DevExpress.Persistent.Base.ImageNameAttribute("BO_Report")]
  [NavigationItem("Library")]
  public class RegulationReporting : BaseObject
  {
    private Marbid.Module.BusinessObjects.Library.Regulation _regulation;
    private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
    private System.DateTime _modifyDate;
    private System.DateTime _createDate;
    private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
    private System.Int16 _submitDate;
    private Marbid.Module.BusinessObjects.PeriodType _periodType;
    private Marbid.Module.BusinessObjects.Administration.Employee _personInCharge;
    private System.String _description;
    private System.String _title;
    public RegulationReporting(DevExpress.Xpo.Session session)
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
    [RuleRequiredField]
    public System.String Title
    {
      get
      {
        return _title;
      }
      set
      {
        SetPropertyValue("Title", ref _title, value);
      }
    }
    [DevExpress.Xpo.SizeAttribute(5000)]
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
    public Marbid.Module.BusinessObjects.Administration.Employee PersonInCharge
    {
      get
      {
        return _personInCharge;
      }
      set
      {
        SetPropertyValue("PersonInCharge", ref _personInCharge, value);
      }
    }
    public Marbid.Module.BusinessObjects.PeriodType PeriodType
    {
      get
      {
        return _periodType;
      }
      set
      {
        SetPropertyValue("PeriodType", ref _periodType, value);
      }
    }
    [RuleRange(1, 31)]
    public System.Int16 SubmitDate
    {
      get
      {
        return _submitDate;
      }
      set
      {
        SetPropertyValue("SubmitDate", ref _submitDate, value);
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
    [DevExpress.Xpo.AssociationAttribute("Reports-Regulation")]
    public Marbid.Module.BusinessObjects.Library.Regulation Regulation
    {
      get
      {
        return _regulation;
      }
      set
      {
        SetPropertyValue("Regulation", ref _regulation, value);
      }
    }
  }
}
