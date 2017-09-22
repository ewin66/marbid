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

namespace Marbid.Module.BusinessObjects.CRM
{
  [DefaultClassOptions]
  [NavigationItem(false)]
  [ImageName("meeting")]
  [CreatableItem(false)]
  //[ImageName("BO_Contact")]
  [DefaultProperty("Participant")]
  //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
  //[Persistent("DatabaseTableName")]
  // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
  public class ScheduleParticipant : BaseObject
  { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    public ScheduleParticipant(Session session)
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
    // Fields...
    private People _Participant;
    private Schedule _Schedule;

    [Association("Schedule-SceduleParticipants")]
    public Schedule Schedule
    {
      get
      {
        return _Schedule;
      }
      set
      {
        Schedule oldSchedule = _Schedule;
        SetPropertyValue("Schedule", ref _Schedule, value);
        if (!IsLoading && !IsSaving && oldSchedule != _Schedule)
        {
          oldSchedule = oldSchedule ?? _Schedule;
          oldSchedule.UpdateParticipants(true);
        }
      }
    }
    public People Participant
    { 
      get
      {
        return _Participant;
      }
      set
      {
        SetPropertyValue("Participant", ref _Participant, value);
      }
    }
    AttendeeType attendanceType;
    public AttendeeType AttendanceType
    {
      get
      {
        return attendanceType;
      } 
        
      set
      {
        SetPropertyValue("AttendanceType", ref attendanceType, value);
      }
    }

    public enum AttendeeType
    {
      Required,
      Optional,
      Resource
    }
  }
}