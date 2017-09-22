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

namespace Marbid.Module.BusinessObjects.HRM
{
  [XafDefaultProperty("Name")]
  [DefaultClassOptions]
  [DevExpress.Persistent.Base.ImageNameAttribute("BO_Category")]
  public class RankGroup : BaseObject
  {
    private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
    private System.DateTime _modifyDate;
    private System.DateTime _createDate;
    private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
    private System.String _name;
    private System.Int16 _groupIndex;
    public RankGroup(DevExpress.Xpo.Session session)
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
    public System.Int16 GroupIndex
    {
      get
      {
        return _groupIndex;
      }
      set
      {
        SetPropertyValue("GroupIndex", ref _groupIndex, value);
      }
    }
    [RuleRequiredField]
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
    [DevExpress.Xpo.AssociationAttribute("StructuralPositions-RankGroup")]
    public XPCollection<Marbid.Module.BusinessObjects.HRM.StructuralPosition> StructuralPositions
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.HRM.StructuralPosition>("StructuralPositions");
      }
    }
  }
}
