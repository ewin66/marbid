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
using Marbid.Module.CustomCodes;
using Marbid.Module.BusinessObjects.HRM;
using DevExpress.ExpressApp.SystemModule;
using Marbid.Module.BusinessObjects.CRM;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base.Security;

namespace Marbid.Module.BusinessObjects.Administration
{
   [DefaultClassOptions,
 DefaultProperty("UserName")]
   [CurrentUserDisplayImage("Photo")]
   [ListViewFilter("All Employees", null, Index = 0)]
   [ListViewFilter("Active Employees", "[IsActive] = true", Index = 1)]
   [ImageName("BO_Employee")]
   public class Employee : People, ISecurityUser, IAuthenticationStandardUser, IAuthenticationActiveDirectoryUser, IPermissionPolicyUser, ISecurityUserWithRoles, ICanInitialize
   {
      private System.DateTime _terminationDate;
      private System.DateTime _appointmentDate;
      private System.String _nomorPokok;
      private System.Int16 _loggedOnTimes;
      private System.DateTime _joinDate;
      private Marbid.Module.BusinessObjects.Administration.Directorate _directorate;
      private Marbid.Module.BusinessObjects.Administration.Employee _manager;
      private Marbid.Module.BusinessObjects.HRM.FunctionalPosition _functionalPosition;
      private Marbid.Module.BusinessObjects.HRM.StructuralPosition _structuralPosition;
      private Marbid.Module.BusinessObjects.Administration.Department _department;
      private Marbid.Module.BusinessObjects.Administration.Division _division;
      public Employee(DevExpress.Xpo.Session session)
        : base(session)
      {
      }
      protected override void OnLoaded()
      {
         Reset();
         base.OnLoaded();
      }
      public void Reset()
      {
         _rankGroup = null;
         _serviceTime = null;
         fDaysOffRemaining = null;
      }
      protected override void OnSaving()
      {
         base.OnSaving();
         if (StructuralPosition != null)
            Position = StructuralPosition.Name;
      }
      #region ISecurityUser Members
      private bool isActive = true;
      public bool IsActive
      {
         get { return isActive; }
         set { SetPropertyValue("IsActive", ref isActive, value); }
      }
      private string userName = String.Empty;
      [RuleRequiredField("EmployeeUserNameRequired", DefaultContexts.Save)]
      [RuleUniqueValue("EmployeeUserNameIsUnique", DefaultContexts.Save,
          "The login with the entered user name was already registered within the system.")]
      public string UserName
      {
         get { return userName; }
         set { SetPropertyValue("UserName", ref userName, value); }
      }
      #endregion

      #region IAuthenticationStandardUser Members
      private bool changePasswordOnFirstLogon;
      public bool ChangePasswordOnFirstLogon
      {
         get { return changePasswordOnFirstLogon; }
         set
         {
            SetPropertyValue("ChangePasswordOnFirstLogon", ref changePasswordOnFirstLogon, value);
         }
      }
      private string storedPassword;
      [Browsable(false), Size(SizeAttribute.Unlimited), Persistent, SecurityBrowsable]
      protected string StoredPassword
      {
         get { return storedPassword; }
         set { storedPassword = value; }
      }
      public bool ComparePassword(string password)
      {
         return PasswordCryptographer.VerifyHashedPasswordDelegate(this.storedPassword, password);
      }
      public void SetPassword(string password)
      {
         this.storedPassword = PasswordCryptographer.HashPasswordDelegate(password);
         OnChanged("StoredPassword");
      }
      #endregion

      #region ISecurityUserWithRoles Members
      IList<ISecurityRole> ISecurityUserWithRoles.Roles
      {
         get
         {
            IList<ISecurityRole> result = new List<ISecurityRole>();
            foreach (MarbidRole role in MarbidRoles)
            {
               result.Add(role);
            }
            return result;
         }
      }
      #endregion
      [Association("Employees-MarbidRoles")]
      [RuleRequiredField("MarbidRoleIsRequired", DefaultContexts.Save,
          TargetCriteria = "IsActive",
          CustomMessageTemplate = "An active employee must have at least one role assigned")]
      public XPCollection<MarbidRole> MarbidRoles
      {
         get
         {
            return GetCollection<MarbidRole>("MarbidRoles");
         }
      }
      #region IPermissionPolicyUser Members
      IEnumerable<IPermissionPolicyRole> IPermissionPolicyUser.Roles
      {
         get { return MarbidRoles.OfType<IPermissionPolicyRole>(); }
      }
      #endregion
      #region ICanInitialize Members
      void ICanInitialize.Initialize(IObjectSpace objectSpace, SecurityStrategyComplex security)
      {
         MarbidRole newUserRole = (MarbidRole)objectSpace.FindObject<MarbidRole>(
             new BinaryOperator("Name", security.NewUserRoleName));
         if (newUserRole == null)
         {
            newUserRole = objectSpace.CreateObject<MarbidRole>();
            newUserRole.Name = security.NewUserRoleName;
            newUserRole.IsAdministrative = true;
            newUserRole.Employees.Add(this);
         }
      }
      #endregion



      [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
      [VisibleInLookupListView(false)]
      public System.DateTime JoinDate
      {
         get
         {
            return _joinDate;
         }
         set
         {
            if (SetPropertyValue("JoinDate", ref _joinDate, value))
               OnChanged("ServiceTime");
         }
      }
      [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
      [VisibleInLookupListView(true)]
      public Marbid.Module.BusinessObjects.Administration.Directorate Directorate
      {
         get
         {
            return _directorate;
         }
         set
         {
            SetPropertyValue("Directorate", ref _directorate, value);
         }
      }
      [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
      [VisibleInLookupListView(false)]
      [DevExpress.Persistent.Base.DataSourcePropertyAttribute("Directorate.Divisions")]
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
      [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
      [DevExpress.Persistent.Base.DataSourcePropertyAttribute("Division.Departments")]
      [VisibleInLookupListView(true)]
      public Marbid.Module.BusinessObjects.Administration.Department Department
      {
         get
         {
            return _department;
         }
         set
         {
            SetPropertyValue("Department", ref _department, value);
         }
      }


      [DevExpress.Xpo.AssociationAttribute("Employees-StructuralPosition")]
      [RuleRequiredField]
      [VisibleInLookupListView(true)]
      public Marbid.Module.BusinessObjects.HRM.StructuralPosition StructuralPosition
      {
         get
         {
            return _structuralPosition;
         }
         set
         {
            SetPropertyValue("StructuralPosition", ref _structuralPosition, value);
         }
      }
      [DevExpress.Xpo.AssociationAttribute("Employees-FunctionalPosition")]
      [VisibleInLookupListView(false)]
      public Marbid.Module.BusinessObjects.HRM.FunctionalPosition FunctionalPosition
      {
         get
         {
            return _functionalPosition;
         }
         set
         {
            SetPropertyValue("FunctionalPosition", ref _functionalPosition, value);
         }
      }

      [DevExpress.Xpo.AssociationAttribute("Employees-Manager")]
      [VisibleInLookupListView(false)]
      public Marbid.Module.BusinessObjects.Administration.Employee Manager
      {
         get
         {
            return _manager;
         }
         set
         {
            SetPropertyValue("Manager", ref _manager, value);
         }
      }
      [DevExpress.Xpo.AssociationAttribute("Employees-Manager")]
      [VisibleInLookupListView(false)]
      public XPCollection<Marbid.Module.BusinessObjects.Administration.Employee> Employees
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.Administration.Employee>("Employees");
         }
      }

      [DevExpress.Xpo.AssociationAttribute("Tasks-Employee")]
      public XPCollection<Marbid.Module.BusinessObjects.HRM.Task> Tasks
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.HRM.Task>("Tasks");
         }
      }
      [DevExpress.Xpo.AssociationAttribute("Notes-Owner")]
      public XPCollection<Marbid.Module.BusinessObjects.General.Notes> Notes
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.General.Notes>("Notes");
         }
      }
      //[DevExpress.Xpo.AssociationAttribute("OurParticipants-Schedules")]
      //public XPCollection<Marbid.Module.BusinessObjects.CRM.Schedule> Schedules
      //{
      //  get
      //  {
      //    return GetCollection<Marbid.Module.BusinessObjects.CRM.Schedule>("Schedules");
      //  }
      //}
      [DevExpress.Xpo.AssociationAttribute("OfficeLeave-Employee")]
      public XPCollection<Marbid.Module.BusinessObjects.HRM.OfficeLeave> OfficeLeave
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.HRM.OfficeLeave>("OfficeLeave");
         }
      }
      [Persistent("DaysOffRemaining")]
      private int? fDaysOffRemaining = null;
      [PersistentAlias("fDaysOffRemaining")]
      [VisibleInLookupListView(false)]
      public int? DaysOffRemaining
      {
         get
         {
            if (!IsLoading && !IsSaving && fDaysOffRemaining == null)
               UpdateDaysOffRemaining(false);
            return fDaysOffRemaining;
         }
      }
      public void UpdateDaysOffRemaining(bool forceChangeEvents)
      {
         int? oldDaysOffRemaining = fDaysOffRemaining;
         int tempDaysOff = 0;
         foreach (OfficeLeave officeLeave in OfficeLeave)
         {
            if (officeLeave.StartDate.Year == DateTime.Now.Year && officeLeave.ManagerApproval == Approval.Approved && officeLeave.HRApproval == Approval.Approved && officeLeave.DirectorApproval == Approval.Approved)
               tempDaysOff += officeLeave.TotalWorkDays;
         }
         SystemSetting setting = Session.FindObject<SystemSetting>(null);
         tempDaysOff = setting.YearlyOfficeLeaveDays - tempDaysOff;
         fDaysOffRemaining = tempDaysOff;
         if (forceChangeEvents)
         {
            OnChanged("DaysOffRemaining", oldDaysOffRemaining, fDaysOffRemaining);
         }
      }
      [VisibleInLookupListView(false)]
      public System.Int16 LoggedOnTimes
      {
         get
         {
            return _loggedOnTimes;
         }
         set
         {
            SetPropertyValue("LoggedOnTimes", ref _loggedOnTimes, value);
         }
      }
      [DevExpress.Xpo.AssociationAttribute("AcademicHistory-Employee")]
      [DevExpress.Xpo.AggregatedAttribute]
      public XPCollection<Marbid.Module.BusinessObjects.HRM.AcademicHistory> AcademicHistory
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.HRM.AcademicHistory>("AcademicHistory");
         }
      }
      [DevExpress.Xpo.AssociationAttribute("PromotionMutation-Employee")]
      [DevExpress.Xpo.AggregatedAttribute]
      [DevExpress.ExpressApp.DC.XafDisplayNameAttribute("Promotion/Mutation")]
      public XPCollection<Marbid.Module.BusinessObjects.HRM.Promotion> PromotionMutation
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.HRM.Promotion>("PromotionMutation");
         }
      }
      [DevExpress.Xpo.AssociationAttribute("Certifications-Employee")]
      [DevExpress.Xpo.AggregatedAttribute]
      public XPCollection<Marbid.Module.BusinessObjects.HRM.EmployeeCertification> Certifications
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.HRM.EmployeeCertification>("Certifications");
         }
      }
      [DevExpress.Xpo.AssociationAttribute("Exams-Employee")]
      [DevExpress.Xpo.AggregatedAttribute]
      public XPCollection<Marbid.Module.BusinessObjects.HRM.EmployeeExam> Exams
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.HRM.EmployeeExam>("Exams");
         }
      }

      [VisibleInLookupListView(false)]
      public System.String NomorPokok
      {
         get
         {
            return _nomorPokok;
         }
         set
         {
            SetPropertyValue("NomorPokok", ref _nomorPokok, value);
         }
      }
      [Persistent("ServiceTime")]
      private string _serviceTime = null;
      [PersistentAlias("_serviceTime")]
      [VisibleInLookupListView(false)]
      public string ServiceTime
      {
         get
         {
            if (!IsLoading && !IsSaving && _serviceTime == null)
               GetServiceTime();
            return _serviceTime;
         }
      }
      public void GetServiceTime()
      {
         if (JoinDate != null)
         {
            Calculator calc = new Calculator();
            if (IsActive == false)
            {
               _serviceTime = calc.TimeSpanToDate(JoinDate, TerminationDate);
            }
            else
            {
               _serviceTime = calc.TimeSpanToDate(JoinDate, DateTime.Now);
            }
         }
         else
         {
            _serviceTime = "Join date is empty";
         }
      }
      [VisibleInLookupListView(false)]
      public System.DateTime AppointmentDate
      {
         get
         {
            return _appointmentDate;
         }
         set
         {
            SetPropertyValue("AppointmentDate", ref _appointmentDate, value);
         }
      }
      [VisibleInLookupListView(false)]
      public System.DateTime TerminationDate
      {
         get
         {
            return _terminationDate;
         }
         set
         {
            SetPropertyValue("TerminationDate", ref _terminationDate, value);
         }
      }


      [Persistent("RankGroup"),
      VisibleInListView(false),
      VisibleInDetailView(false)]
      private RankGroup _rankGroup = null;
      [PersistentAlias("_rankGroup")]
      [VisibleInLookupListView(true)]
      public RankGroup RankGroup
      {
         get
         {
            if (!IsLoading && !IsSaving && _rankGroup == null)
               GetRankGroup();
            return _rankGroup;
         }
      }
      public void GetRankGroup()
      {
         Employee currentEmployee = Session.GetObjectByKey<Employee>(Oid);
         if (currentEmployee.StructuralPosition != null)
         {
            _rankGroup = Session.GetObjectByKey<RankGroup>(currentEmployee.StructuralPosition.RankGroup.Oid);
         }
         else
         {
            _rankGroup = Session.FindObject<RankGroup>(CriteriaOperator.Parse("GroupIndex=?", 1));
         }
      }

      public int Age
      {
         get
         {
            if (!IsLoading && !IsSaving && BirthDate != null)
            {
               Calculator calc = new Calculator();
               return calc.Age(BirthDate, DateTime.Now);
            } else
            {
               return 0;
            }
         }
      }
   }
}
