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
using DevExpress.ExpressApp.Editors;

namespace Marbid.Module.BusinessObjects.HRM
{
  [DefaultClassOptions]
  [DevExpress.Persistent.Base.ImageNameAttribute("BO_Category")]
  //[ImageName("BO_Contact")]
  [DefaultProperty("Name")]
  [Appearance("AnnouncementCategoryDisabled", Enabled = false, TargetItems = "CreatedBy,ModifiedBy,CreateDate,ModifiedDate,Division")]
  //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
  //[Persistent("DatabaseTableName")]
  // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
  public class AnnouncementCategory : BaseObject
  { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    public AnnouncementCategory(Session session)
        : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
      CreateDate = DateTime.Now;
      Employee emp = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
      CreatedBy = emp;
      Division = Session.GetObjectByKey<Division>(emp.Division.Oid);
    }

    protected override void OnSaving()
    {
      base.OnSaving();
      ModifyDate = DateTime.Now;
      ModifiedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
    }
    private Employee _ModifiedBy;
    private DateTime _ModifyDate;
    private Employee _CreatedBy;
    private DateTime _CreateDate;
    private Marbid.Module.BusinessObjects.Administration.Division _division;
    private System.String _description;
    private System.String _name;
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
    [Size(SizeAttribute.Unlimited)]
    [EditorAlias(EditorAliases.HtmlPropertyEditor)]
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
    [VisibleInListView(false)]
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
    [VisibleInListView(false)]
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

    [VisibleInListView(false)]
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
    [VisibleInListView(false)]
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

  }
}