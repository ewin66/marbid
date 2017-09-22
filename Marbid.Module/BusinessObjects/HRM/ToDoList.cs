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
using DevExpress.ExpressApp.ConditionalAppearance;
using Marbid.Module.BusinessObjects.Administration;
using DevExpress.ExpressApp.Editors;

namespace Marbid.Module.BusinessObjects.HRM
{
  [DefaultClassOptions]
  [ImageName("BO_Task")]
  [DefaultProperty("ToDo")]
  [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
  //[Persistent("DatabaseTableName")]
  // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
  [Appearance("IsDone", FontColor = "green", FontStyle = System.Drawing.FontStyle.Strikeout, Criteria = "IsDone = True", TargetItems = "*")]
  [Appearance("CreatedByDisabled", Enabled = false, TargetItems = "CreatedBy,DateCompleted,IsDone,CreateDate")]
  public class ToDoList : BaseObject
  { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    public ToDoList(Session session)
        : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
      CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
      CreateDate = DateTime.Now;
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
    string toDo;
    [Size(SizeAttribute.Unlimited)]
    [ModelDefault("RowCount", "1")]
    [EditorAlias(EditorAliases.StringPropertyEditor)]
    public string ToDo
    {
      get
      {
        return toDo;
      }
      set
      {
        SetPropertyValue("ToDo", ref toDo, value);
      }
    }
    Employee createdBy;
    [VisibleInListView(false)]
    public Employee CreatedBy
    {
      get
      {
        return createdBy;
      }
      set
      {
        SetPropertyValue("CreatedBy", ref createdBy, value);
      }
    }
    bool isDone;
    [VisibleInListView(false)]
    public bool IsDone
    {
      get
      {
        return isDone;
      }
      set
      {
        SetPropertyValue("IsDone", ref isDone, value);
      }
    }
    DateTime createDate;
    [VisibleInListView(false)]
    public DateTime CreateDate
    {
      get
      {
        return createDate;
      }
      set
      {
        SetPropertyValue("CreateDate", ref createDate, value);
      }
    }
    DateTime dateCompleted;
    [VisibleInListView(false)]
    public DateTime DateCompleted
    {
      get
      {
        return dateCompleted;
      }
      set
      {
        SetPropertyValue("DateCompleted", ref dateCompleted, value);
      }
    }

    [Action(AutoCommit = true, Caption = "Done", TargetObjectsCriteria = "IsDone = false", ImageName = "State_Task_Completed", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
    public void MarkDone()
    {
      IsDone = true;
      DateCompleted = DateTime.Now;
    }
    [Action(AutoCommit = true, Caption = "Not Done", TargetObjectsCriteria = "IsDone = true", ImageName = "State_Validation_Invalid", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
    public void UnmarkDone()
    {
      IsDone = false;
      DateCompleted = DateTime.MinValue;
    }
  }
}