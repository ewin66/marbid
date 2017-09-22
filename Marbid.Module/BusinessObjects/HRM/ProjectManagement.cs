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
  [ImageName("BO_Project")]
  [DefaultProperty("Name")]
  public class ProjectManagement : BaseObject
  { 
    public ProjectManagement(Session session)
        : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
      CreateDate = DateTime.Now;
      ProjectParticipant partic = new ProjectParticipant(this.Session);
      partic.Employee = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
      partic.Responsibility = "Project Creator";
      Participants.Add(partic);
    }
    protected override void OnSaving()
    {
      base.OnSaving();
      ModifiedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
      ModifyDate = DateTime.Now;
    }

    private Employee _ProjectLeader;
    private DateTime _DueDate;
    private TaskStatus _Status;
    private DateTime _ModifyDate;
    private Employee _ModifiedBy;
    private DateTime _CreateDate;
    private Employee _CreatedBy;
    private string _Description;
    private DateTime _EndDate;
    private DateTime _StartDate;
    private string _Name;

    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    [RuleRequiredField]
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

    public TaskStatus Status
    {
      get
      {
        return _Status;
      }
      set
      {
        SetPropertyValue("Status", ref _Status, value);
      }
    }

    [RuleRequiredField]
    public Employee ProjectLeader
    {
      get
      {
        return _ProjectLeader;
      }
      set
      {
        SetPropertyValue("ProjectLeader", ref _ProjectLeader, value);
      }
    }

    public DateTime StartDate
    {
      get
      {
        return _StartDate;
      }
      set
      {
        SetPropertyValue("StartDate", ref _StartDate, value);
      }
    }

    public DateTime EndDate
    {
      get
      {
        return _EndDate;
      }
      set
      {
        SetPropertyValue("EndDate", ref _EndDate, value);
      }
    }

    public DateTime DueDate
    {
      get
      {
        return _DueDate;
      }
      set
      {
        SetPropertyValue("DueDate", ref _DueDate, value);
      }
    }

    [Size(SizeAttribute.Unlimited)]
    [RuleRequiredField]
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

    [Persistent("ProjectCompletion")]
    private decimal? _projectCompletion;
    [PersistentAlias("_projectCompletion")]
    public decimal? ProjectCompletion
    {
      get
      {
        if (!IsLoading && !IsSaving && _projectCompletion == null)
          UpdateTotalCompletion(false);
        return _projectCompletion;
      }
    }

    public void UpdateTotalCompletion(bool forceChangeEvent)
    {
      int projectcount = 0;
      decimal? oldProjectCompletion = _projectCompletion;
      decimal tempProjectCompletion = 0;
      foreach (Task detail in Tasks)
      {
        projectcount++;
        tempProjectCompletion += (decimal)detail.PercentCompleted;
      }
      if (projectcount != 0)
      {
        _projectCompletion = tempProjectCompletion / projectcount;
      } else
      {
        _projectCompletion = 0;
      }
      
      if (forceChangeEvent)
      {
        OnChanged("ProjectCompletion", oldProjectCompletion, _projectCompletion);
      }
    }

    [Association("ProjectManagement-Tasks")]
    public XPCollection<Task> Tasks
    {
      get
      {
        return GetCollection<Task>("Tasks");
      }
    }

    [Association("ProjectManagement-Participants")]
    public XPCollection<ProjectParticipant> Participants
    {
      get
      {
        return GetCollection<ProjectParticipant>("Participants");
      }
    }

    [ReadOnly(true)]
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

    [ReadOnly(true)]
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

    [ReadOnly(true)]
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

    [ReadOnly(true)]
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

    //[Action(Caption ="Add Project Member", ImageName = "BO_Employee", TargetObjectsCriteria = "[CreatedBy.Oid]=CurrentUserId() or [ProjectLeader.Oid]=CurrentUserId()", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
    //public void AddProjectParticipantAction(AddProjectParticipantParameter parameters)
    //{
    //  Session session = this.Session;
    //  ProjectParticipant partic = new ProjectParticipant(session);
    //  partic.Employee = parameters.Employee;
    //  partic.Responsibility = parameters.ProjectRole;
    //  Participants.Add(partic);
    //}
  }

  //[DomainComponent]
  //public class AddProjectParticipantParameter
  //{
  //  public Employee Employee { get; set; }
  //  public string ProjectRole { get; set; }
  //}
}