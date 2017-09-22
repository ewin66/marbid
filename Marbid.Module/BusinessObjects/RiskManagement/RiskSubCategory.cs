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
  [DefaultProperty("Name")]
  [NavigationItem("Risk Management")]
  //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
  //[Persistent("DatabaseTableName")]
  // Specify more UI options using a declarative approach (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112701.aspx).
  public class RiskSubCategory : BaseObject
  { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    public RiskSubCategory(Session session)
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
    private DateTime _ModifyDate;
    private Employee _ModifiedBy;
    private DateTime _CreateDate;
    private Employee _CreatedBy;
    private RiskCategory _Category;
    private string _Description;
    private string _Name;

    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    [RuleRequiredField("", DefaultContexts.Save)]
    public string Name
    {
      get
      {
        return _Name;
      }
      set
      {
        SetPropertyValue("Name", ref _Name, value);
      }
    }

    [Association("RiskCategory-RiskSubCategories")]
    [RuleRequiredField("", DefaultContexts.Save)]
    public RiskCategory Category
    {
      get
      {
        return _Category;
      }
      set
      {
        SetPropertyValue("Category", ref _Category, value);
      }
    }
    [Size(500)]
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
