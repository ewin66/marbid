using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using Marbid.Module.CustomCodes;
using Marbid.Module.BusinessObjects.Administration;
using DevExpress.ExpressApp.ConditionalAppearance;
using Marbid.Module.BusinessObjects.CRM;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Editors;
using System.IO;
using DevExpress.ExpressApp.Model;

namespace Marbid.Module.BusinessObjects.HRM
{
    [DefaultClassOptions]
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Task")]
    [DefaultProperty("Subject")]
    [NavigationItem("Main Menu")]
    [Appearance("IsPrivateAssignedToDisabled", TargetItems = "AssignedTo", Enabled = false)]
    [Appearance("TaskDisabled", Enabled = false, TargetItems = "Project, CreateDate, PercentCompleted, Owner, Status, DateCompleted, StartDate, MinutesOfMeeting")]
    [Appearance("CompletedTask", TargetItems = "Subject", FontColor = "green", FontStyle = System.Drawing.FontStyle.Strikeout, Criteria = "Status = 'Completed'")]
    [Appearance("DueTasks", TargetItems = "DueDate", FontColor = "yellow", BackColor = "Red", FontStyle = System.Drawing.FontStyle.Bold, Criteria = "Status <> 'Completed' and DueDate <= LocalDateTimeToday()")]
    [Appearance("RemoveListViewActions", Context = "ListView", AppearanceItemType = "Action", TargetItems = "Task.AddNewNote.AddNewNoteParameterObject,Task.SendReminder.SendReminderParameterObject,Task.Postpone.PostponeParametersObject,Task.ReassignTask.ReasignParameterObject,Task.MarkComplete.CompleteParametersObject,Task.StartTask.StartTaskParameterObject", Visibility = ViewItemVisibility.Hide)]
    //[Appearance("AlmostDueTasks", TargetItems = "DueDate", FontColor = "red", BackColor = "yellow", FontStyle = System.Drawing.FontStyle.Italic, Criteria = "Status <> 'Completed' and ADDDATE(DueDate, -3) < LocalDateTimeToday()")]
    [ListViewFilter("AllMyTask", "[Owner.Oid] = CurrentUserId() or [AssignedTo.Oid] = CurrentUserId()", "All My Tasks", "Show only my tasks", 0, false)]
    [ListViewFilter("MyAllUncompletedTasks", "([Owner.Oid] = CurrentUserId() or [AssignedTo.Oid] = CurrentUserId()) and [Status] <> 'Completed'", "All My Pending Tasks", "Show only my pending tasks", 1, false)]
    [ListViewFilter("AllTasksRelatedToMe", "[AssignedTo.Oid] = CurrentUserId() Or [Owner.Oid] = CurrentUserId() Or [AssignedTo.Manager.Oid] = CurrentUserId() Or [Owner.Manager.Oid] = CurrentUserId() Or [Owner.Department.Manager.Oid] = CurrentUserId() Or [Owner.Division.Manager.Oid] = CurrentUserId() Or [Owner.Directorate.Manager.Oid] = CurrentUserId() Or [AssignedTo.Department.Manager.Oid] = CurrentUserId() Or [AssignedTo.Division.Manager.Oid] = CurrentUserId() Or [AssignedTo.Directorate.Manager.Oid] = CurrentUserId()", "All Tasks Related To Me", "Show all tasks related to me, including my subordinates' tasks", 2, false)]
    [ListViewFilter("AllUncompletedTasksRelatedToMe", "([AssignedTo.Oid] = CurrentUserId() Or [Owner.Oid] = CurrentUserId() Or [AssignedTo.Manager.Oid] = CurrentUserId() Or [Owner.Manager.Oid] = CurrentUserId() Or [Owner.Department.Manager.Oid] = CurrentUserId() Or [Owner.Division.Manager.Oid] = CurrentUserId() Or [Owner.Directorate.Manager.Oid] = CurrentUserId() Or [AssignedTo.Department.Manager.Oid] = CurrentUserId() Or [AssignedTo.Division.Manager.Oid] = CurrentUserId() Or [AssignedTo.Directorate.Manager.Oid] = CurrentUserId()) And Status <> 'Completed'", "All Uncompleted Tasks Related To Me", "Show all uncompleted tasks related to me, including my subordinates' tasks", 3, true)]


    public class Task : BaseObject
    {
        private ProjectManagement _Project;
        private Marbid.Module.BusinessObjects.CRM.Organization _organization;
        private System.DateTime _createDate;
        private System.Int32 _percentCompleted;
        private Marbid.Module.BusinessObjects.Administration.Employee _owner;
        private System.String _description;
        private Marbid.Module.BusinessObjects.TaskStatus _status;
        private Marbid.Module.BusinessObjects.Priority _priority;
        private System.DateTime _dateCompleted;
        private System.DateTime _dueDate;
        private System.DateTime _startDate;
        private Marbid.Module.BusinessObjects.Administration.Employee _assignedTo;
        private System.String _subject;
        public Task(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Owner = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
            CreateDate = DateTime.Now;
            Priority = BusinessObjects.Priority.Normal;
            if (MinutesOfMeeting != null)
            {
                Organization = Session.GetObjectByKey<Marbid.Module.BusinessObjects.CRM.Organization>(MinutesOfMeeting.Schedule.Organization.Oid);
            }
        }
        protected override void OnSaving()
        {
            base.OnSaving();
            if (AssignedTo != null && Session.IsNewObject(this))
            {
                MailAssignNewTask();
            }
            if (MinutesOfMeeting != null && Organization == null)
            {
                Organization = Session.GetObjectByKey<CRM.Organization>(MinutesOfMeeting.Schedule.Organization.Oid);
            }
        }
        #region "Email"
        private bool MailAssignNewTask()
        {
            ViewShortcut shortcut = new ViewShortcut("Task_DetailView", Oid.ToString());
            string url = "http://rnd.ntmarein.local/Default.aspx" + "?Shortcut" + shortcut.ToString();
            string mSubject = string.Format("[MARBID] New task from {0}", Owner.FullName);
            string mSendTo = AssignedTo.CorporateEmail;
            string mBody = string.Format("Dear {0}, \r\n{1} have assigned you to a new task with the following information:\r\n\r\nSubject: {2}\r\nDue Date: {3:dddd, dd MMMM yyyy}\r\nPriority: {4}\r\n\r\nTask Description:\r\n{5}\r\n", AssignedTo.FullName, Owner.FullName, Subject, DueDate, Priority, Description);
            mBody = mBody + string.Format("\r\n \r\nPlease log in to Marbid for additional infomation by clicking the following URL: \r\n {0}\r\n \r\n \r\n ", url);
            mBody = mBody + "This is automated message, please do not reply.";
            Mailer sMailer = new Mailer("noreply-marbid@marein-re.com", mSendTo, mSubject, mBody);
            return sMailer.SendMail();
        }

        private bool MailReassignNewTask()
        {
            ViewShortcut shortcut = new ViewShortcut("Task_DetailView", Oid.ToString());
            string url = "http://rnd.ntmarein.local/Default.aspx" + "?Shortcut" + shortcut.ToString();
            string mSubject = string.Format("A task from {0} is reasign to to you", Owner.FullName);
            string mSendTo = AssignedTo.CorporateEmail;
            string mBody = string.Format("Dear {0}, \r\n{1} have reassigned a task you with following information:\r\n\r\nSubject: {2}\r\nDue Date: {3:dddd, dd MMMM yyyy}\r\nPriority: {4}\r\n\r\nTask Description:\r\n{5}\r\n", AssignedTo.FullName, Owner.FullName, Subject, DueDate, Priority, Description);
            mBody = mBody + string.Format("\r\n \r\nPlease log in to Marbid for additional infomation by clicking the following URL: \r\n {0}\r\n \r\n \r\n ", url);
            mBody = mBody + "This is automated message, please do not reply.";
            Mailer sMailer = new Mailer("noreply-marbid@marein-re.com", mSendTo, mSubject, mBody);
            return sMailer.SendMail();
        }

        #endregion

        protected override void OnSaved()
        {
            base.OnSaved();
        }

        [RuleRequiredField(DefaultContexts.Save)]
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

        [Association("ProjectManagement-Tasks")]
        public ProjectManagement Project
        {
            get
            {
                return _Project;
            }
            set
            {
                ProjectManagement oldProjectManagement = Project;
                SetPropertyValue("Project", ref _Project, value);
                if (!IsLoading && !IsSaving && oldProjectManagement != _Project)
                {
                    oldProjectManagement = oldProjectManagement ?? _Project;
                    oldProjectManagement.UpdateTotalCompletion(true);
                }
            }
        }

        //[DevExpress.Persistent.Base.DataSourcePropertyAttribute("Owner.Employees")]
        [DataSourceCriteria("[Manager.Oid] = CurrentUserId() Or [StructuralPosition.RankGroup.Oid] = [<Employee>][Oid=CurrentUserId()].Single(StructuralPosition.RankGroup.Oid) and [IsActive] = true")]
        public Marbid.Module.BusinessObjects.Administration.Employee AssignedTo
        {
            get
            {
                return _assignedTo;
            }
            set
            {
                SetPropertyValue("AssignedTo", ref _assignedTo, value);
            }
        }
        public System.DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                SetPropertyValue("StartDate", ref _startDate, value);
            }
        }
        [RuleRequiredField(DefaultContexts.Save)]
        public System.DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                SetPropertyValue("DueDate", ref _dueDate, value);
            }
        }
        public System.DateTime DateCompleted
        {
            get
            {
                return _dateCompleted;
            }
            set
            {
                SetPropertyValue("DateCompleted", ref _dateCompleted, value);
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
        public Marbid.Module.BusinessObjects.TaskStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                SetPropertyValue("Status", ref _status, value);
            }
        }
        [Size(SizeAttribute.Unlimited)]
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

        [ReadOnly(true)]
        [DevExpress.Xpo.AssociationAttribute("Tasks-Employee")]
        public Marbid.Module.BusinessObjects.Administration.Employee Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                SetPropertyValue("Owner", ref _owner, value);
            }
        }

        public System.Int32 PercentCompleted
        {
            get
            {
                return _percentCompleted;
            }
            set
            {
                SetPropertyValue("PercentCompleted", ref _percentCompleted, value);
            }
        }
        [ReadOnly(true)]
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
        [DevExpress.Xpo.AssociationAttribute("TaskNotes-Task"), DevExpress.Xpo.Aggregated]
        public XPCollection<Marbid.Module.BusinessObjects.HRM.TaskNote> TaskNotes
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.HRM.TaskNote>("TaskNotes");
            }
        }
        [DevExpress.Xpo.AssociationAttribute("Tasks-Organization")]
        public Marbid.Module.BusinessObjects.CRM.Organization Organization
        {
            get
            {
                return _organization;
            }
            set
            {
                SetPropertyValue("Organization", ref _organization, value);
            }
        }
        bool isPrivate;
        [ImmediatePostData(true)]
        public bool IsPrivate
        {
            get
            {
                return isPrivate;
            }
            set
            {
                SetPropertyValue("IsPrivate", ref isPrivate, value);
                if (!IsLoading && !IsSaving && value == true && AssignedTo != null)
                {
                    AssignedTo = null;
                }
            }
        }
        //public Marbid.Module.BusinessObjects.CRM.MinutesOfMeeting MinutesOfMeeting
        //{
        //  get
        //  {
        //    return _minutesOfMeeting;
        //  }
        //  set
        //  {
        //    if (_minutesOfMeeting == value)
        //      return;
        //    Marbid.Module.BusinessObjects.CRM.MinutesOfMeeting prevMinutesOfMeeting = _minutesOfMeeting;
        //    _minutesOfMeeting = value;
        //    if (IsLoading)
        //      return;
        //    if (prevMinutesOfMeeting != null && prevMinutesOfMeeting.Task == this)
        //      prevMinutesOfMeeting.Task = null;
        //    if (_minutesOfMeeting != null)
        //      _minutesOfMeeting.Task = this;
        //    OnChanged("MinutesOfMeeting");
        //  }
        //}
        MinutesOfMeeting minutesOfMeeting;
        [Association("MinutesOfMeeting-ActionPlans")]
        public MinutesOfMeeting MinutesOfMeeting
        {
            get
            {
                return minutesOfMeeting;
            }
            set
            {
                SetPropertyValue("MinutesOfMeeting", ref minutesOfMeeting, value);
            }
        }
        #region Actions
        [Action(Caption = "Add New Note", TargetObjectsCriteria = "([Owner.Oid] = CurrentUserId() Or [AssignedTo.Oid] = CurrentUserId()) and IsNewObject(this) = false", ImageName = "BO_Note", AutoCommit = true, SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void AddNewNote(AddNewNoteParameterObject parameters)
        {
            Session session = this.Session;
            TaskNote tnote = new TaskNote(session);
            tnote.Owner = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
            tnote.CreateDate = DateTime.Now;
            tnote.IsSystemNote = false;
            tnote.Note = parameters.Content;
            tnote.IsPrivate = parameters.IsPrivate;
            if (parameters.Attachment != null)
            {
                FileData tData = new FileData(session);
                MemoryStream ms = new MemoryStream();
                parameters.Attachment.SaveToStream(ms);
                ms.Position = 0;
                tData.LoadFromStream(parameters.Attachment.FileName, ms);
                tnote.Attachment = tData;
            }
            TaskNotes.Add(tnote);
            tnote.Save();
            Session.CommitTransaction();
        }
        [Action(Caption = "Mark Complete", TargetObjectsCriteria = "(([Owner.Oid] = CurrentUserId() and Status <> 4 and IsNewObject(this) =false) or  ([AssignedTo.Oid] = CurrentUserId() and Status <> 4) or IsNewObject(this) = false)", ImageName = "CompleteTask", AutoCommit = true, SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void MarkComplete(CompleteParametersObject parameters)
        {
            Session session = this.Session;
            TaskNote tnote = new TaskNote(session);
            tnote.CreateDate = DateTime.Now;
            tnote.Owner = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            tnote.Note = "<strong>Task is completed</strong><br />";
            tnote.Note += parameters.Comment;
            tnote.IsSystemNote = true;
            if (parameters.Attachment != null)
            {
                FileData tData = new FileData(session);
                MemoryStream ms = new MemoryStream();
                parameters.Attachment.SaveToStream(ms);
                ms.Position = 0;
                tData.LoadFromStream(parameters.Attachment.FileName, ms);
                tnote.Attachment = tData;
            }
            Status = TaskStatus.Completed;
            PercentCompleted = 100;
            DateCompleted = DateTime.Now;
            tnote.IsPrivate = false;
            TaskNotes.Add(tnote);
            tnote.Save();
            Session.CommitTransaction();
            Session.Save(this);
        }
        [Action(Caption = "Postpone", TargetObjectsCriteria = "([Owner.Oid] = CurrentUserId() or [AssignedTo.Oid] = CurrentUserId()) And Status <> 4 And IsNewObject(this) = false", ImageName = "Wait", AutoCommit = true, SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void Postpone(PostponeParametersObject parameters)
        {
            Session session = this.Session;
            TaskNote tnote = new TaskNote(session);
            tnote.CreateDate = DateTime.Now;
            tnote.Owner = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
            tnote.Note = "<strong>Task is postponed.</strong><br />";
            tnote.Note += parameters.Comment;
            tnote.IsPrivate = false;
            tnote.IsSystemNote = true;
            if (parameters.Attachment != null)
            {
                FileData tData = new FileData(session);
                MemoryStream ms = new MemoryStream();
                parameters.Attachment.SaveToStream(ms);
                ms.Position = 0;
                tData.LoadFromStream(parameters.Attachment.FileName, ms);
                tnote.Attachment = tData;
            }
            TaskNotes.Add(tnote);
            DueDate += TimeSpan.FromDays(parameters.PostponeForDays);
            tnote.Save();
            Session.CommitTransaction();
        }
        [Action(Caption = "Start Task", TargetObjectsCriteria = "Status <> 1 and IsNewObject(this) = false and ([Owner.Oid] = CurrentUserId() or [AssignedTo.Oid] = CurrentUserId())", ImageName = "StartTask", AutoCommit = true, SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void StartTask(StartTaskParameterObject parameters)
        {
            Session session = this.Session;
            TaskNote tnote = new TaskNote(session);
            tnote.CreateDate = DateTime.Now;
            tnote.Owner = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
            tnote.Note = "<strong>Task is started.</strong><br/>";
            tnote.Note += parameters.Comment;
            tnote.IsSystemNote = true;
            StartDate = DateTime.Now;
            Status = TaskStatus.InProgress;
            if (parameters.Attachment != null)
            {
                FileData tData = new FileData(session);
                MemoryStream ms = new MemoryStream();
                parameters.Attachment.SaveToStream(ms);
                ms.Position = 0;
                tData.LoadFromStream(parameters.Attachment.FileName, ms);
                tnote.Attachment = tData;
            }
            tnote.IsPrivate = false;
            TaskNotes.Add(tnote);
            tnote.Save();
            Session.CommitTransaction();
            Session.Save(this);
        }
        [Action(Caption = "Advance Task", TargetObjectsCriteria = "Status = 1 and IsNewObject(this) = false and ([Owner.Oid] = CurrentUserId() or [AssignedTo.Oid] = CurrentUserId())", ImageName = "StartTask", AutoCommit = true, SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject)]
        public void AdvanceTask(AndvanceTaskParameterObject parameters)
        {
            Session session = this.Session;
            TaskNote tnote = new TaskNote(session);
            tnote.CreateDate = DateTime.Now;
            tnote.Note = string.Format("<strong>Task is advanced, current completion :{0}%</strong><br/>", parameters.PercentCompleted);
            tnote.Note += parameters.Comment;
            tnote.IsSystemNote = true;
            tnote.IsPrivate = false;
            if (parameters.Attachment != null)
            {
                FileData tData = new FileData(session);
                MemoryStream ms = new MemoryStream();
                parameters.Attachment.SaveToStream(ms);
                ms.Position = 0;
                tData.LoadFromStream(parameters.Attachment.FileName, ms);
                tnote.Attachment = tData;
            }
            PercentCompleted = (int)parameters.PercentCompleted;
            TaskNotes.Add(tnote);
            tnote.Save();
            Session.CommitTransaction();
            Session.Save(this);
        }
        [Action(Caption = "Reassign Task", TargetObjectsCriteria = "([AssignedTo] = CurrentUserId() or [Owner.Oid] = CurrentUserId() or [AssignedTo.Manager.Oid] = CurrentUserId()) And Status <> 4 And IsNewObject(this) = false", ImageName = "BO_Department", ToolTip = "Reassign this task to other employee", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject, AutoCommit = true)]
        public void ReassignTask(ReasignParameterObject param)
        {
            Session session = this.Session;
            TaskNote tnote = new TaskNote(session);
            tnote.CreateDate = DateTime.Now;
            tnote.Note = string.Format("<strong>Task is reassigned to: {0}<br/><strong>Reason:</strong><br/>{1}", param.ReassignTo, param.ReassignReason);
            tnote.IsPrivate = false;
            if (param.Attachment != null)
            {
                FileData tData = new FileData(session);
                MemoryStream ms = new MemoryStream();
                param.Attachment.SaveToStream(ms);
                ms.Position = 0;
                tData.LoadFromStream(param.Attachment.FileName, ms);
                tnote.Attachment = tData;
            }
            AssignedTo = Session.GetObjectByKey<Employee>(param.ReassignTo.Oid);
            DueDate = param.ChangeDueDate;
            tnote.IsSystemNote = true;
            TaskNotes.Add(tnote);
            tnote.Save();
            MailReassignNewTask();
            Session.CommitTransaction();
            Session.Save(this);
        }
        [Action(Caption = "Send Reminder", TargetObjectsCriteria = "IsNewObject(this) = false and [AssignedTo] is not null and [Status] <> 4 and ([Owner.Oid] = CurrentUserId() or [AssignedTo.Oid] = CurrentUserId() or [AssignedTo.Manager.Oid] = CurrentUserId())", ToolTip = "Send a reminder via email", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject, ImageName = "BO_Attention", ConfirmationMessage = "This will send a reminder email, continue?", AutoCommit = true)]
        public void SendReminder(SendReminderParameterObject param)
        {
            Employee sender = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            ViewShortcut shortcut = new ViewShortcut("Task_DetailView", Oid.ToString());
            string url = "http://rnd.ntmarein.local/Default.aspx" + "?Shortcut" + shortcut.ToString();
            string mSubject = string.Format("Reminder: {0}", this.Subject);
            string mSendTo = AssignedTo.CorporateEmail;
            string mBody = string.Format("Dear {0},<br/>", AssignedTo.FullName);
            mBody += string.Format("<p>This is a reminder from {0} for your task <strong><a href='{1}'>{2}</a></strong>.</p>", sender.FullName, url, Subject);
            mBody += string.Format("<p>The task will due on {0}. Please review detailed task information from Marbid system and complete the task immediately.</p>", DueDate);
            mBody += string.Format("<p>Bellow is an additional comment from {0}:</p>", sender.FullName);
            mBody += string.Format("<quote>{0}</quote>", param.AdditionalMessage);
            mBody += string.Format("<p>To review the task please click following URL:<br/><a href='{0}'>{0}</a>", url);
            Mailer sMailer = new Mailer("noreply-marbid@marein-re.com", AssignedTo.CorporateEmail, mSubject, mBody);
            if (param.SendCopyToMySelf)
            {
                sMailer.CC = sender.CorporateEmail;
            }
            sMailer.SendMail();

            Session session = this.Session;
            TaskNote tnote = new TaskNote(session);
            tnote.CreateDate = DateTime.Now;
            tnote.Note = string.Format("<strong>Reminder is sent:</strong><br/>{0}", param.AdditionalMessage);
            tnote.IsSystemNote = true;
            TaskNotes.Add(tnote);
            tnote.Save();
            Session.CommitTransaction();
            Session.Save(this);
        }
        #endregion
    }
    #region ActionParameter
    [DomainComponent]
    public class PostponeParametersObject
    {
        public PostponeParametersObject()
        {
            PostponeForDays = 1;
        }
        public uint PostponeForDays { get; set; }
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField]
        public string Comment { get; set; }
        public FileData Attachment { get; set; }
    }
    [DomainComponent]
    public class CompleteParametersObject
    {
        private Task task;
        public CompleteParametersObject(Task task)
        {
            this.task = task;
        }
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        [RuleRequiredField]
        public string Comment { get; set; }
        public FileData Attachment { get; set; }
    }
    [DomainComponent]
    public class StartTaskParameterObject
    {
        private Task task;
        public StartTaskParameterObject(Task task)
        {
            this.task = task;
        }
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        [RuleRequiredField]
        public string Comment { get; set; }
        public FileData Attachment { get; set; }
    }
    [DomainComponent]
    public class AndvanceTaskParameterObject
    {
        private Task task;
        public AndvanceTaskParameterObject(Task task)
        {
            this.task = task;
            PercentCompleted = (uint)task.PercentCompleted;
            PercentCompleted += 10;
            if (PercentCompleted >= 100)
            {
                PercentCompleted = 100;
            }
        }
        [RuleRange(0, 100)]
        public uint PercentCompleted { get; set; }
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        [RuleRequiredField]
        public string Comment { get; set; }
        public FileData Attachment { get; set; }
    }
    [DomainComponent]
    public class ReasignParameterObject
    {
        private Task task;
        public ReasignParameterObject(Task task)
        {
            this.task = task;
            ReassignTo = task.AssignedTo;
            ChangeDueDate = task.DueDate;
        }
        [DataSourceCriteria("[Manager.Oid] = CurrentUserId() Or [StructuralPosition.RankGroup.Oid] = [<Employee>][Oid=CurrentUserId()].Single(StructuralPosition.RankGroup.Oid) and [IsActive] = true")]
        [RuleRequiredField]
        public Employee ReassignTo { get; set; }
        [RuleRequiredField]
        public DateTime ChangeDueDate { get; set; }
        [Size(SizeAttribute.Unlimited)]
        [RuleRequiredField]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        public string ReassignReason { get; set; }
        public FileData Attachment { get; set; }
    }
    [DomainComponent]
    public class SendReminderParameterObject
    {
        public SendReminderParameterObject()
        {
            SendCopyToMySelf = true;
        }
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        [RuleRequiredField]
        public string AdditionalMessage { get; set; }
        public bool SendCopyToMySelf { get; set; }
        public bool SendCopyToMyManager { get; set; }
        public bool SendCopyToAssigneeManager { get; set; }
    }

    [DomainComponent]
    public class AddNewNoteParameterObject
    {

        public AddNewNoteParameterObject()
        {
            IsPrivate = false;
        }
        [CaptionsForBoolValues("Everyone can see this note", "Only me can see this note")]
        public bool IsPrivate { get; set; }
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        [RuleRequiredField]
        public string Content { get; set; }
        public FileData Attachment { get; set; }
    }
    #endregion
}
