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
  [DefaultProperty("RiskElement")]
  [NavigationItem("Risk Management")]
  //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
  //[Persistent("DatabaseTableName")]
  // Specify more UI options using a declarative approach (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112701.aspx).
  public class RiskRegister : BaseObject
  { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    public RiskRegister(Session session)
      : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      CreateDate = DateTime.Now;
      CreatedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
      Owner = CreatedBy.Department;
    }

    protected override void OnSaving()
    {
      base.OnSaving();
      ModifiedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
      ModifyDate = DateTime.Now;
    }
    private double _AppetiteValue;
    private double _ToleranceValue;
    private RiskSubCategory _SubCategory;
    private RiskCategory _Category;
    private Department _Owner;
    private string _Description;
    private string _RiskElement;
    private DateTime _ModifyDate;
    private Employee _ModifiedBy;
    private DateTime _CreateDate;
    private Employee _CreatedBy;

    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    [RuleRequiredField("", DefaultContexts.Save)]
    public string RiskElement
    {
      get
      {
        return _RiskElement;
      }
      set
      {
        SetPropertyValue("RiskElement", ref _RiskElement, value);
      }
    }
    [RuleRequiredField("", DefaultContexts.Save)]
    public Department Owner
    {
      get
      {
        return _Owner;
      }
      set
      {
        SetPropertyValue("Owner", ref _Owner, value);
      }
    }
    [ImmediatePostData(true)]
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
    [DataSourceProperty("Category.SubCategories")]
    public RiskSubCategory SubCategory
    {
      get
      {
        return _SubCategory;
      }
      set
      {
        SetPropertyValue("SubCategory", ref _SubCategory, value);
      }
    }
        
    public double ToleranceValue
    {
      get
      {
        return _ToleranceValue;
      }
      set
      {
        SetPropertyValue("ToleranceValue", ref _ToleranceValue, value);
      }
    }
    [ImmediatePostData(true)]
    public double AppetiteValue
    {
      get
      {
        return _AppetiteValue;
      }
      set
      {
        SetPropertyValue("AppetiteValue", ref _AppetiteValue, value);
      }
    }

    [Persistent("Appetite")]
    private RiskAppetite _Appetite;
    [ReadOnly(true)]
    [PersistentAlias("_Appetite")]
    public RiskAppetite Appetite
    {
      get
      {
        
        _Appetite = Session.FindObject<RiskAppetite>(CriteriaOperator.Parse("[MinimumValue] <= ? AND [MaximumValue] >= ?", _AppetiteValue, _AppetiteValue));
        return _Appetite;
      }
    }
    [Size(5000)]
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
