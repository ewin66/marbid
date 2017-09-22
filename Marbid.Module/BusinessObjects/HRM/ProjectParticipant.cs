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
  [DefaultClassOptions]
  [RuleCombinationOfPropertiesIsUnique("Employee, Project")]
  [DefaultProperty("DisplayName")]
  [ImageName("BO_Employee")]
  [NavigationItem(false)]
  //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
  //[Persistent("DatabaseTableName")]
  // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
  public class ProjectParticipant : BaseObject
  { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    public ProjectParticipant(Session session)
        : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
    }
    //private string _PersistentProperty;
    //[XafDisplayName("My display name"), ToolTip("My hint message")]
    //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
    //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
    //public string PersistentProperty {
    //    get { return _PersistentProperty; }
    //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
    //}

    //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
    //public void ActionMethod() {
    //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
    //    this.PersistentProperty = "Paid";
    //}
    // Fields...
    private string _Responsibility;
    private Employee _Employee;
    private ProjectManagement _Project;
    private readonly static string displayNameFormat = "{Employee} ({Responsibility})";
    [Association("ProjectManagement-Participants")]
    public ProjectManagement Project
    {
      get
      {
        return _Project;
      }
      set
      {
        SetPropertyValue("Project", ref _Project, value);
      }
    }
    [RuleRequiredField]
    public Employee Employee
    {
      get
      {
        return _Employee;
      }
      set
      {
        SetPropertyValue("Employee", ref _Employee, value);
      }
    }
    [RuleRequiredField]
    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    public string Responsibility
    {
      get
      {
        return _Responsibility;
      }
      set
      {
        SetPropertyValue("Responsibility", ref _Responsibility, value);
      }
    }
    [Persistent]
    [VisibleInListView(false)]
    public string DisplayName
    {
      get
      {
        return ObjectFormatter.Format(displayNameFormat, this, EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty);
      }
    }
  }
}