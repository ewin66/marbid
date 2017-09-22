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
using DevExpress.ExpressApp.ConditionalAppearance;

namespace Marbid.Module.BusinessObjects.Library
{
  [DefaultClassOptions]
  [ImageName("law")]
  [DefaultProperty("Title")]
  [NavigationItem("Library")]
  [Appearance("DisableRegulation", TargetItems = "CreatedBy,ModifiedBy,CreateDate,ModifyDate", Enabled = false)]
  public class Regulation : BaseObject
  {
    private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
    private System.DateTime _modifyDate;
    private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
    private System.DateTime _createDate;
    private DevExpress.Persistent.BaseImpl.FileData _document;
    private Marbid.Module.BusinessObjects.CRM.Regulator _regulator;
    private System.DateTime _effectiveDate;
    private System.String _content;
    private System.String _regulationNumber;
    private System.String _title;
    public Regulation(DevExpress.Xpo.Session session)
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
    [RuleRequiredField]
    [DevExpress.Xpo.AssociationAttribute("Regulations-Regulator")]
    public Marbid.Module.BusinessObjects.CRM.Regulator Regulator
    {
      get
      {
        return _regulator;
      }
      set
      {
        SetPropertyValue("Regulator", ref _regulator, value);
      }
    }
    public System.DateTime EffectiveDate
    {
      get
      {
        return _effectiveDate;
      }
      set
      {
        SetPropertyValue("EffectiveDate", ref _effectiveDate, value);
      }
    }
    [RuleRequiredField]
    public System.String RegulationNumber
    {
      get
      {
        return _regulationNumber;
      }
      set
      {
        SetPropertyValue("RegulationNumber", ref _regulationNumber, value);
      }
    }
    public DevExpress.Persistent.BaseImpl.FileData Document
    {
      get
      {
        return _document;
      }
      set
      {
        SetPropertyValue("Document", ref _document, value);
      }
    }
    [DevExpress.Xpo.SizeAttribute(5000)]
    public System.String Content
    {
      get
      {
        return _content;
      }
      set
      {
        SetPropertyValue("Content", ref _content, value);
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
    [DevExpress.Xpo.AssociationAttribute("Reports-Regulation")]
    public XPCollection<Marbid.Module.BusinessObjects.Library.RegulationReporting> Reports
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.Library.RegulationReporting>("Reports");
      }
    }
  }
}
