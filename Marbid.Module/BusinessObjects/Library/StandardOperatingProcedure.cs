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
  [DevExpress.Persistent.Base.ImageNameAttribute("BO_StateMachine")]
  [XafDefaultProperty("Title")]
  [NavigationItem("Library")]
  [FileAttachment("SOPDocument")]
  public class StandardOperatingProcedure : BaseObject
  {
    private System.String _documentNumber;
    private DevExpress.Persistent.BaseImpl.FileData _sOPDocument;
    private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
    private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
    private Marbid.Module.BusinessObjects.Administration.Employee _preparedBy;
    private Marbid.Module.BusinessObjects.Administration.Directorate _directorate;
    private System.Boolean _isInEffect;
    private System.Int16 _revision;
    private System.DateTime _effectiveDate;
    private System.String _objectives;
    private Marbid.Module.BusinessObjects.Administration.Department _department;
    private Marbid.Module.BusinessObjects.Administration.Division _division;
    private System.DateTime _modifyDate;
    private System.DateTime _createDate;
    private System.String _title;
    public StandardOperatingProcedure(DevExpress.Xpo.Session session)
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
      ModifiedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
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
    public System.String DocumentNumber
    {
      get
      {
        return _documentNumber;
      }
      set
      {
        SetPropertyValue("DocumentNumber", ref _documentNumber, value);
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
    public Marbid.Module.BusinessObjects.Administration.Employee PreparedBy
    {
      get
      {
        return _preparedBy;
      }
      set
      {
        SetPropertyValue("PreparedBy", ref _preparedBy, value);
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
    [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
    public Marbid.Module.BusinessObjects.Administration.Directorate Directorate
    {
      get
      {
        return _directorate;
      }
      set
      {
        SetPropertyValue("Directorate", ref _directorate, value);
      }
    }
    [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
    [DevExpress.Persistent.Base.DataSourcePropertyAttribute("Directorate.Divisions")]
    public Marbid.Module.BusinessObjects.Administration.Division Division
    {
      get
      {
        return _division;
      }
      set
      {
        SetPropertyValue("Division", ref _division, value);
      }
    }
    [DevExpress.Persistent.Base.DataSourcePropertyAttribute("Division.Departments")]
    public Marbid.Module.BusinessObjects.Administration.Department Department
    {
      get
      {
        return _department;
      }
      set
      {
        SetPropertyValue("Department", ref _department, value);
      }
    }
    public DevExpress.Persistent.BaseImpl.FileData SOPDocument
    {
      get
      {
        return _sOPDocument;
      }
      set
      {
        SetPropertyValue("SOPDocument", ref _sOPDocument, value);
      }
    }
    [Size(SizeAttribute.Unlimited)]
    public System.String Objectives
    {
      get
      {
        return _objectives;
      }
      set
      {
        SetPropertyValue("Objectives", ref _objectives, value);
      }
    }
    public System.Int16 Revision
    {
      get
      {
        return _revision;
      }
      set
      {
        SetPropertyValue("Revision", ref _revision, value);
      }
    }
    public System.Boolean IsInEffect
    {
      get
      {
        return _isInEffect;
      }
      set
      {
        SetPropertyValue("IsInEffect", ref _isInEffect, value);
      }
    }
    [DevExpress.Xpo.AssociationAttribute("Definitions-SOP")]
    public XPCollection<Marbid.Module.BusinessObjects.Library.Definition> Definitions
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.Library.Definition>("Definitions");
      }
    }
    [Association("OperatingSteps-StandardOperatingProcedure"), DevExpress.Xpo.Aggregated]
    public XPCollection<Marbid.Module.BusinessObjects.Library.OperatingStep> OperatingSteps
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.Library.OperatingStep>("OperatingSteps");
      }
    }
    [Association("OperatingProcedureAttachments-StandardOperatingProcedure"), DevExpress.Xpo.Aggregated]
    public XPCollection<Marbid.Module.BusinessObjects.Library.OperatingProcedureAttachment> OperatingProcedureAttachments
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.Library.OperatingProcedureAttachment>("OperatingProcedureAttachments");
      }
    }
  }
}
