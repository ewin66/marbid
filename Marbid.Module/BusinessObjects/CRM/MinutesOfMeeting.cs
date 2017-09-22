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

namespace Marbid.Module.BusinessObjects.CRM
{
  [DefaultClassOptions]
  [DevExpress.Persistent.Base.ImageNameAttribute("BO_Report")]
  [DefaultProperty("Subject")]
  [NavigationItem("CRM")]
  [Appearance("MOMDisabled", TargetItems = "CreateDate,ModifyDate,CreatedBy,ModifiedBy", Enabled = false)]
  public class MinutesOfMeeting : BaseObject
  {
    private Marbid.Module.BusinessObjects.Priority _priority;
    private People _expressedBy;
    private Marbid.Module.BusinessObjects.Administration.Employee _personInCharge;
    private System.String _actionPlan;
    private Marbid.Module.BusinessObjects.CRM.Schedule _schedule;
    private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
    private System.DateTime _modifyDate;
    private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
    private System.DateTime _createDate;
    private System.String _content;
    private System.String _subject;
    public MinutesOfMeeting(DevExpress.Xpo.Session session)
      : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      CreateDate = DateTime.Now;
      CreatedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
      Priority = Priority.Normal;
    }
    protected override void OnSaving()
    {
      base.OnSaving();
      ModifyDate = DateTime.Now;
      ModifiedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
    }
    [RuleRequiredField]
    public System.String Subject
    {
      get
      {
        return _subject;
      }
      set
      {
        SetPropertyValue("Subject", ref _subject, value);
      }
    }
    public People ExpressedBy
    {
      get
      {
        return _expressedBy;
      }
      set
      {
        SetPropertyValue("ExpressedBy", ref _expressedBy, value);
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
    [Size(SizeAttribute.Unlimited)]
    [RuleRequiredField]
    [EditorAlias(EditorAliases.HtmlPropertyEditor)]
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

    [Association("MinutesOfMeetings-Schedule")]
    public Marbid.Module.BusinessObjects.CRM.Schedule Schedule
    {
      get
      {
        return _schedule;
      }
      set
      {
        Schedule oldSchedule = _schedule;
        SetPropertyValue("Schedule", ref _schedule, value);
        if (!IsLoading && !IsSaving && oldSchedule != _schedule)
        {
          oldSchedule = oldSchedule ?? _schedule;
          oldSchedule.UpdateMOMCount(true);
        }
      }
    }
    [Size(SizeAttribute.Unlimited)]
    public System.String ActionPlan
    {
      get
      {
        return _actionPlan;
      }
      set
      {
        SetPropertyValue("ActionPlan", ref _actionPlan, value);
      }
    }
    public Marbid.Module.BusinessObjects.Administration.Employee PersonInCharge
    {
      get
      {
        return _personInCharge;
      }
      set
      {
        SetPropertyValue("PersonInCharge", ref _personInCharge, value);
      }
    }
    [NonPersistent,
    VisibleInDetailView(false),
    VisibleInListView(false)]
    public string ParticipantEmail
    {
      get
      {
        return Schedule.ParticipantsEmail;
      }
    }
    [Association("MinutesOfMeeting-ActionPlans"), DevExpress.Xpo.Aggregated]
    public XPCollection<Marbid.Module.BusinessObjects.HRM.Task> ActionPlans
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.HRM.Task>("ActionPlans");
      }
    }
    public Marbid.Module.BusinessObjects.Priority Priority
    {
      get
      {
        return _priority;
      }
      set
      {
        SetPropertyValue("Priority", ref _priority, value);
      }
    }
  }

  [DomainComponent]
  public class CreateTaskFromIssueParameter
  {
    private MinutesOfMeeting minutesofmeeting;
    public CreateTaskFromIssueParameter(MinutesOfMeeting minutesofmeeting)
    {
      Subject = minutesofmeeting.Subject;
      //Organization = Organization.Session.GetObjectByKey<Organization>(minutesofmeeting.Oid);
      //session.GetObjectByKey<Organization>(minutesofmeeting.Schedule.Organization.Oid);//minutesofmeeting.Session.GetObjectByKey<Organization>(minutesofmeeting.Schedule.Organization.Oid);
      Organization = minutesofmeeting.Schedule.Organization;
      AssignedTo = minutesofmeeting.PersonInCharge;
      //Organization = objectSpace.GetObjectByKey<Organization>(minutesofmeeting.Schedule.Organization.Oid);
      //UnitOfWork ow = new UnitOfWork(minutesofmeeting.Session.DataLayer);
      //Organization = ow.GetObjectByKey<Organization>(minutesofmeeting.Schedule.Organization.Oid);
      DueDate = DateTime.Now;
      Priority = minutesofmeeting.Priority;
      Description = string.Format("Issue:\r\n{0}\r\nAction Plan:\r\n{1}", minutesofmeeting.Content, minutesofmeeting.ActionPlan);
      //AssignedTo = minutesofmeeting.PersonInCharge;
      this.minutesofmeeting = minutesofmeeting;
      //Organization = this.minutesofmeeting.Session.GetObjectByKey<Organization>(minutesofmeeting.Schedule.Organization.Oid);
    }
    public string Subject { get; set; }
    [ReadOnly(true)]
    public Organization Organization { get; set; }
    [ReadOnly(true)]
    public Employee AssignedTo { get; set; }
    public DateTime DueDate { get; set; }
    public Priority Priority { get; set; }
    [Size(SizeAttribute.Unlimited)]
    public string Description { get; set; }
  }
}
