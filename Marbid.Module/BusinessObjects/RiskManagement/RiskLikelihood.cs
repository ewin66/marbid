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
namespace Marbid.Module.BusinessObjects.RiskManagement
{
  [DefaultClassOptions]
  //[ImageName("BO_Contact")]
  [DefaultProperty("Label")]
  [NavigationItem("Risk Management")]
  //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
  //[Persistent("DatabaseTableName")]
  // Specify more UI options using a declarative approach (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112701.aspx).
  public class RiskLikelihood : BaseObject
  { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    public RiskLikelihood(Session session)
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
      ModifiedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
      ModifyDate = DateTime.Now;
    }
    private double _LikelihoodValue;
    private DateTime _ModifyDate;
    private Employee _ModifiedBy;
    private DateTime _CreateDate;
    private Employee _CreatedBy;
    private string _Description;
    private string _Label;

    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    [RuleRequiredField("", DefaultContexts.Save)]
    public string Label
    {
      get
      {
        return _Label;
      }
      set
      {
        SetPropertyValue("Label", ref _Label, value);
      }
    }

    public double LikelihoodValue
    {
      get
      {
        return _LikelihoodValue;
      }
      set
      {
        SetPropertyValue("LikelihoodValue", ref _LikelihoodValue, value);
      }
    }
    [Size(200)]
    [RuleRequiredField("", DefaultContexts.Save)]
    public string Description
    {
      get
      {
        return _Description;
      }
      set
      {
        SetPropertyValue("Description", ref _Description, value);
      }
    }
    [ReadOnly(true)]
    public Employee CreatedBy
    {
      get
      {
        return _CreatedBy;
      }
      set
      {
        SetPropertyValue("CreatedBy", ref _CreatedBy, value);
      }
    }
    [ReadOnly(true)]
    public DateTime CreateDate
    {
      get
      {
        return _CreateDate;
      }
      set
      {
        SetPropertyValue("CreateDate", ref _CreateDate, value);
      }
    }
    [ReadOnly(true)]
    public Employee ModifiedBy
    {
      get
      {
        return _ModifiedBy;
      }
      set
      {
        SetPropertyValue("ModifiedBy", ref _ModifiedBy, value);
      }
    }
    [ReadOnly(true)]
    public DateTime ModifyDate
    {
      get
      {
        return _ModifyDate;
      }
      set
      {
        SetPropertyValue("ModifyDate", ref _ModifyDate, value);
      }
    }
  }
}
