using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Ical.Net.DataTypes;
using Ical.Net.Serialization.iCalendar.Serializers;
using Marbid.Module.BusinessObjects.Administration;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Marbid.Module.BusinessObjects.CRM
{
    [DefaultClassOptions]
    [NavigationItem("Main Menu")]
    [Appearance("ScheduleDisableProperty", TargetItems = "CreatedBy, ModifiedBy, CreateDate, ModifyDate, AssignedDriver, CarAssignedBy, CarAssignmentNote, AssignedCar, CarAssignedDate", Enabled = false)]
    [Appearance("DisableClone", Criteria = "[CreatedBy.Oid] <> CurrentUserId()", TargetItems = "CloneObject", Enabled = false)]
    //[FileAttachment("ExternalMoM")]
    [DefaultListViewOptions(false, NewItemRowPosition.None)]
    public class Schedule : DevExpress.Persistent.BaseImpl.Event
    {
        private System.Boolean _isCarAssigned;
        private System.String _carAssignmentNote;
        private System.Boolean _isLocked;
        private System.DateTime _carAssignedDate;
        private Marbid.Module.BusinessObjects.Administration.Employee _carAssignedBy;
        private System.DateTime _modifyDate;
        private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
        private System.DateTime _createDate;
        private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
        private Marbid.Module.BusinessObjects.CRM.MeetingRoom _meetingRoom;
        private Marbid.Module.BusinessObjects.CRM.Organization _organization;
        private Marbid.Module.BusinessObjects.CRM.Driver _assignedDriver;
        private Marbid.Module.BusinessObjects.CRM.Car _assignedCar;
        private System.Boolean _carOrder;

        public Schedule(DevExpress.Xpo.Session session)
          : base(session)
        {
        }

        protected override void OnLoaded()
        {
            Reset();
            base.OnLoaded();
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CreateDate = DateTime.Now;
            CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            Status = 2;
            Label = 2;
            People defaultParticipant = Session.GetObjectByKey<People>(SecuritySystem.CurrentUserId);
            ScheduleParticipant participant = new ScheduleParticipant(Session);
            participant.Participant = defaultParticipant;
            ScheduleParticipants.Add(participant);
            UpdateParticipants(true);
            IsCarAssigned = false;
            CarOrder = false;
        }

        private void Reset()
        {
            fParticipants = null;
            fParticipantsEmails = null;
            _ownerEmail = null;
        }

        protected override void OnSaving()
        {
            ModifyDate = DateTime.Now;
            ModifiedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            base.OnSaving();
        }

        protected override void OnSaved()
        {
            base.OnSaved();
        }

        private string fParticipants = null;
        public string Participants
        {
            get
            {
                if (!IsLoading && !IsSaving && fParticipants == null)
                    UpdateParticipants(false);
                return fParticipants;
            }
        }

        public void UpdateParticipants(bool forceChangeEvents)
        {
            string tempParticipant = null;
            string oldParticipants = fParticipants;
            string tempParticipantsEmails = null;
            string oldParticipantsEmails = fParticipantsEmails;
            foreach (ScheduleParticipant detail in ScheduleParticipants)
            {
                if (detail.Participant != null)
                {
                    tempParticipant += detail.Participant.FirstName;
                    tempParticipant += ", ";
                }
                if (detail.Participant != null && detail.Participant.CorporateEmail != null && detail.Participant.UserType == "Employee")
                {
                    tempParticipantsEmails += detail.Participant.CorporateEmail;
                    tempParticipantsEmails += "; ";
                }
            }
            char[] trimChar = { ',', ' ' };
            if (tempParticipant != null)
                tempParticipant = tempParticipant.TrimEnd(trimChar);
            char[] trimCharEmail = { ';', ' ' };
            if (tempParticipantsEmails != null)
                fParticipantsEmails = tempParticipantsEmails.TrimEnd(trimCharEmail);
            fParticipants = tempParticipant;
            if (forceChangeEvents)
            {
                OnChanged("ParticipantsEmails", oldParticipantsEmails, fParticipantsEmails);
                OnChanged("Participants", oldParticipants, fParticipants);
            }
        }

        private string fParticipantsEmails;

        [VisibleInDetailView(false), VisibleInListView(false)]
        public string ParticipantsEmail
        {
            get
            {
                if (!IsLoading && !IsSaving && fParticipantsEmails == null)
                    UpdateParticipants(false);
                return fParticipantsEmails;
            }
        }

        public System.Boolean CarOrder
        {
            get
            {
                return _carOrder;
            }
            set
            {
                SetPropertyValue("CarOrder", ref _carOrder, value);
            }
        }

        [DevExpress.Xpo.AssociationAttribute("Schedules-AssignedCar")]
        [ModelDefault("Caption", "Car")]
        public Marbid.Module.BusinessObjects.CRM.Car AssignedCar
        {
            get
            {
                return _assignedCar;
            }
            set
            {
                SetPropertyValue("AssignedCar", ref _assignedCar, value);
            }
        }

        [DevExpress.Xpo.AssociationAttribute("Schedules-AssignedDriver")]
        [ModelDefault("Caption", "Driver")]
        public Marbid.Module.BusinessObjects.CRM.Driver AssignedDriver
        {
            get
            {
                return _assignedDriver;
            }
            set
            {
                SetPropertyValue("AssignedDriver", ref _assignedDriver, value);
            }
        }

        [DevExpress.Xpo.AssociationAttribute("Visits-Organization")]
        [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
        [RuleRequiredField("", DefaultContexts.Save)]
        public Marbid.Module.BusinessObjects.CRM.Organization Organization
        {
            get
            {
                return _organization;
            }
            set
            {
                if (SetPropertyValue("Organization", ref _organization, value))
                {
                    if (!IsLoading)
                    {
                        if (MeetingRoom == null && Organization != null)
                        {
                            if (Organization.DisplayName != "Other" && Organization.DisplayName != "Internal")
                                if (Organization.Address != null)
                                    Location = Organization.Address;
                                else
                                    Location = Organization.DisplayName;
                        }
                    }
                }
            }
        }

        [DevExpress.Xpo.AssociationAttribute("Schedules-MeetingRoom")]
        [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
        [DataSourceCriteria("[IsActive] = True")]
        public Marbid.Module.BusinessObjects.CRM.MeetingRoom MeetingRoom
        {
            get
            {
                return _meetingRoom;
            }
            set
            {
                if (SetPropertyValue("MeetingRoom", ref _meetingRoom, value))
                {
                    if (!IsLoading)
                    {
                        if (MeetingRoom != null)
                        {
                            Location = MeetingRoom.Name;
                        }
                    }
                }
            }
        }

        [Association("Schedule-SceduleParticipants"), DevExpress.Xpo.Aggregated]
        public XPCollection<ScheduleParticipant> ScheduleParticipants
        {
            get
            {
                return GetCollection<ScheduleParticipant>("ScheduleParticipants");
            }
        }

        private bool requestLaptop;

        public bool RequestLaptop
        {
            get
            {
                return requestLaptop;
            }
            set
            {
                SetPropertyValue("RequestLaptop", ref requestLaptop, value);
            }
        }

        private bool requestTeleconference;

        public bool RequestTeleconference
        {
            get
            {
                return requestTeleconference;
            }
            set
            {
                SetPropertyValue("RequestTeleconference", ref requestTeleconference, value);
            }
        }

        private bool requestVideoconference;

        public bool RequestVideoconference
        {
            get
            {
                return requestVideoconference;
            }
            set
            {
                SetPropertyValue("RequestVideoconference", ref requestVideoconference, value);
            }
        }

        //[DevExpress.Xpo.AssociationAttribute("OurParticipants-Schedules")]
        //[DataSourceProperty("ActiveEmployee")]
        //public XPCollection<Employee> OurParticipants
        //{
        //  get
        //  {
        //    return GetCollection<Employee>("OurParticipants");
        //  }
        //}
        //protected override XPCollection<T> CreateCollection<T>(DevExpress.Xpo.Metadata.XPMemberInfo property)
        //{
        //  XPCollection<T> col = base.CreateCollection<T>(property);
        //  if (property.Name == "OurParticipants")
        //    col.CollectionChanged += new XPCollectionChangedEventHandler(col_CollectionChanged);
        //  return col;
        //}

        private void col_CollectionChanged(object sender, XPCollectionChangedEventArgs e)
        {
            // call TotalsChanged if the grid is not the only control that modifies the Children collection
            ParticipantsChanged();
        }

        internal void ParticipantsChanged()
        {
            OnChanged("ScheduleParticipants");
        }

        private XPCollection<Employee> activeEmployee;

        [Browsable(false)]
        public XPCollection<Employee> ActiveEmployee
        {
            get
            {
                if (activeEmployee == null)
                {
                    activeEmployee = new XPCollection<Employee>(Session);
                }
                RefreshActiveEmployee();
                return activeEmployee;
            }
        }

        private void RefreshActiveEmployee()
        {
            if (activeEmployee == null)
                return;
            activeEmployee.Criteria = CriteriaOperator.Parse("[IsActive] = true");
        }

        [ReadOnly(true)]
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

        [Custom("Editable", "False")]
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

        [ReadOnly(true)]
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

        [VisibleInDetailView(false),
        VisibleInListView(false),
        VisibleInLookupListView(false)]
        public string DriverManagerRoleName
        {
            get
            {
                SystemSetting setting = Session.FindObject<SystemSetting>(null);
                return setting.DriverManagerRole.Name;
            }
        }

        //[Persistent("OwnerEmail")]
        private string _ownerEmail = null;

        //[PersistentAlias("_ownerEmail")]
        [VisibleInDetailView(false),
        VisibleInListView(false),
        VisibleInLookupListView(false)]
        public string OwnerEmail
        {
            get
            {
                return _ownerEmail;
            }
        }

        public void GetOwnerEmail()
        {
            _ownerEmail = this.CreatedBy.CorporateEmail;
        }

        public Marbid.Module.BusinessObjects.Administration.Employee CarAssignedBy
        {
            get
            {
                return _carAssignedBy;
            }
            set
            {
                SetPropertyValue("CarAssignedBy", ref _carAssignedBy, value);
            }
        }

        [NonPersistent, VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string DriverEmail
        {
            get
            {
                if (AssignedDriver != null)
                    return AssignedDriver.DriverName.CorporateEmail;
                else
                    return null;
            }
        }

        public System.DateTime CarAssignedDate
        {
            get
            {
                return _carAssignedDate;
            }
            set
            {
                SetPropertyValue("CarAssignedDate", ref _carAssignedDate, value);
            }
        }

        public System.Boolean IsLocked
        {
            get
            {
                return _isLocked;
            }
            set
            {
                SetPropertyValue("IsLocked", ref _isLocked, value);
            }
        }

        public System.String CarAssignmentNote
        {
            get
            {
                return _carAssignmentNote;
            }
            set
            {
                SetPropertyValue("CarAssignmentNote", ref _carAssignmentNote, value);
            }
        }

        [Association("MinutesOfMeetings-Schedule"), DevExpress.Xpo.Aggregated]
        public XPCollection<Marbid.Module.BusinessObjects.CRM.MinutesOfMeeting> MinutesOfMeetings
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.CRM.MinutesOfMeeting>("MinutesOfMeetings");
            }
        }

        public System.Boolean IsCarAssigned
        {
            get
            {
                return _isCarAssigned;
            }
            set
            {
                SetPropertyValue("IsCarAssigned", ref _isCarAssigned, value);
            }
        }

        [RuleFromBoolProperty("MeetingRoomUsed", DefaultContexts.Save, "Selected meeting room is already booked on selected Date/Time!", UsedProperties = "StartOn,EndOn,MeetingRoom")]
        protected bool IsRoomFree
        {
            get
            {
                //CriteriaOperator criteriaOperator = CriteriaOperator.Parse("? between (StartOn, EndOn) and MeetingRoom = ?", StartOn, MeetingRoom );
                //XPCollection<Schedule> schedules = new XPCollection<Schedule>(PersistentCriteriaEvaluationBehavior.InTransaction, Session, criteriaOperator);
                if (MeetingRoom == null) { return true; }
                int rowCount = 0;
                string Query = String.Format("select count(*) from dbo.Schedule a inner join dbo.Event b on a.Oid = b.Oid inner join dbo.MeetingRoom c on a.MeetingRoom = c.Oid where c.Oid = '{0}' and ('{1}' <= b.EndOn) and ('{2}' >= b.StartOn) and a.Oid <> '{3}'", MeetingRoom.Oid, String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm:ss}", StartOn), String.Format(CultureInfo.InvariantCulture, "{0:yyyy-MM-dd HH:mm:ss}", EndOn), Oid);
                rowCount = (int)Session.ExecuteScalar(Query);
                if (rowCount > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private FileData externalMoM;

        public FileData ExternalMoM
        {
            get
            {
                return externalMoM;
            }
            set
            {
                SetPropertyValue("ExternalMoM", ref externalMoM, value);
            }
        }

        private int? momCount = null;

        public int? MOMCount
        {
            get
            {
                if (!IsLoading && !IsSaving && momCount == null)
                    UpdateMOMCount(false);
                return momCount;
            }
        }

        public void UpdateMOMCount(bool forceChangeEvents)
        {
            int? oldMOMCount = momCount;
            momCount = Convert.ToInt16(Evaluate(CriteriaOperator.Parse("MinutesOfMeetings.Count")));
            if (forceChangeEvents)
                OnChanged("MOMCount", oldMOMCount, momCount);
        }

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInLookupListView(false)]
        [VisibleInListView(false)]
        public string invite
        {
            get
            {
                return GetCalendarData();
            }
        }

        private string GetCalendarData()
        {
            Ical.Net.Calendar calendar = new Ical.Net.Calendar();
            Ical.Net.Event calevent = new Ical.Net.Event();
            calevent.DtStart = new CalDateTime(StartOn);
            calevent.DtEnd = new CalDateTime(EndOn);
            calevent.Description = Description;
            calevent.Location = Location;
            calevent.Summary = Subject;
            calevent.Organizer = new Organizer(CreatedBy.CorporateEmail);
            calevent.IsAllDay = AllDay;
            calevent.Uid = Oid.ToString();

            foreach (ScheduleParticipant peeps in ScheduleParticipants)
            {
                string role;
                switch ((int)peeps.AttendanceType)
                {
                    case 0:
                        role = "REQ-PARTICIPANT";
                        break;

                    case 1:
                        role = "OPT-PARTICIPANT";
                        break;

                    case 2:
                        role = "NON-PARTICIPANT";
                        break;

                    default:
                        role = "REQ-PARTICIPANT";
                        break;
                }
                Attendee attendee = new Attendee()
                {
                    CommonName = peeps.Participant.FullName,
                    Role = role,
                    Rsvp = true,
                    Value = new Uri(String.Format("mailto:{0}", peeps.Participant.CorporateEmail))
                };
                calevent.Attendees.Add(attendee);
            }
            calendar.Events.Add(calevent);
            calendar.Method = "REQUEST";
            CalendarSerializer serializer = new CalendarSerializer(calendar);
            string calData = serializer.SerializeToString();
            return calData;
        }
    }

    public class SendCalendarViewController : ViewController
    {
        public SendCalendarViewController()
        {
            TargetObjectType = typeof(Schedule);
            TargetViewType = ViewType.ListView;
            SimpleAction SendCalendar = new SimpleAction(this, "SendCalendar", "Edit");
            //SendCalendar.TargetObjectsCriteria = "[CreatedBy.Oid] = CurrentUserId()";
            //SendCalendar.TargetObjectsCriteriaMode = TargetObjectsCriteriaMode.TrueForAll;
            SendCalendar.TargetObjectType = typeof(Schedule);
            SendCalendar.TargetViewType = ViewType.ListView;
            SendCalendar.ImageName = "BO_Appointment"; ;
            SendCalendar.ConfirmationMessage = "Send your upcoming appointment to your Email?";
            Actions.Add(SendCalendar);
        }

        public class AssignDriverViewController : ViewController
        {
            public AssignDriverViewController()
            {
                TargetObjectType = typeof(AssignCarParametersObject);
            }

            protected override void OnActivated()
            {
                base.OnActivated();
                //ObjectSpace.ObjectChanged += new EventHandler<ObjectChangedEventArgs>(OnCarChanged);
            }

            private void OnCarChanged(object sender, ObjectChangedEventArgs e)
            {
                if (e.PropertyName == "Driver")
                {
                    AssignCarParametersObject param = (AssignCarParametersObject)View.CurrentObject;
                    param.DesignatedCar = ObjectSpace.GetObjectByKey<Car>(param.DesignatedDriver.Car.Oid);
                }
            }
        }
    }

    [NonPersistent, NavigationItem(false), CreatableItem(false)]
    public class TemporaryScheduleParticipant : BaseObject
    {
        public TemporaryScheduleParticipant(DevExpress.Xpo.Session session)
          : base(session)
        {
        }

        public People Participant { get; set; }
    }

    [DomainComponent]
    public class AddNewMOMParametersObject
    {
        public static Type AddNewMOMParametersType = typeof(AddNewMOMParametersObject);

        public static AddNewMOMParametersObject CreateAddNewMOMParatersObject()
        {
            return (AddNewMOMParametersObject)ReflectionHelper.CreateObject(AddNewMOMParametersType);
        }

        [RuleRequiredField]
        public string Subject { get; set; }

        public People ExpressedBy { get; set; }

        public AddNewMOMParametersObject()
        {
        }

        [RuleRequiredField]
        [Size(SizeAttribute.Unlimited)]
        public string Issue { get; set; }

        [Size(SizeAttribute.Unlimited)]
        public string ActionPlan { get; set; }

        public Employee PersonInCharge { get; set; }
        public Priority Priority { get; set; }
    }

    [DomainComponent]
    public class AssignCarParametersObject
    {
        public static Type AssignCarParametersType = typeof(AssignCarParametersObject);

        public static AssignCarParametersObject CreateAssignCarParameterObject()
        {
            return (AssignCarParametersObject)ReflectionHelper.CreateObject(AssignCarParametersType);
        }

        public AssignCarParametersObject()
        {
        }

        [RuleRequiredField]
        public Car DesignatedCar { get; set; }

        [RuleRequiredField]
        public Driver DesignatedDriver { get; set; }

        [Size(SizeAttribute.Unlimited)]
        public string Comment { get; set; }
    }

    [DomainLogic(typeof(AssignCarParametersObject))]
    public class AssignCarParametersObjectLogic
    {
        public static void OnChanged(AssignCarParametersObject instance)
        {
        }
    }
}