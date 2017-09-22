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
using DevExpress.ExpressApp.Xpo;

namespace Marbid.Module.BusinessObjects.Administration
{
   [DevExpress.Persistent.Base.ImageNameAttribute("settings")]
   [RuleObjectExists("AnotherSingletonExists", DefaultContexts.Save, "True", InvertResult = true, CustomMessageTemplate = "System Setting already exists")]
   [RuleCriteria("CannotDeleteSingleton", DefaultContexts.Delete, "False", CustomMessageTemplate = "System Setting cannot be deleted")]
   public class SystemSetting : BaseObject
   {
      private MarbidRole _hRRole;
      private System.Int16 _yearlyOfficeLeaveDays;
      private System.Int16 _maximumOfficeLeaveDays;
      private MarbidRole _driverManagerRole;
      public SystemSetting(DevExpress.Xpo.Session session)
        : base(session)
      {
      }

      public MarbidRole DriverManagerRole
      {
         get
         {
            return _driverManagerRole;
         }
         set
         {
            SetPropertyValue("DriverManagerRole", ref _driverManagerRole, value);
         }
      }
      public System.Int16 MaximumOfficeLeaveDays
      {
         get
         {
            return _maximumOfficeLeaveDays;
         }
         set
         {
            SetPropertyValue("MaximumOfficeLeaveDays", ref _maximumOfficeLeaveDays, value);
         }
      }
      public System.Int16 YearlyOfficeLeaveDays
      {
         get
         {
            return _yearlyOfficeLeaveDays;
         }
         set
         {
            SetPropertyValue("YearlyOfficeLeaveDays", ref _yearlyOfficeLeaveDays, value);
         }
      }
      public MarbidRole HRRole
      {
         get
         {
            return _hRRole;
         }  
         set
         {
            SetPropertyValue("HRRole", ref _hRRole, value);
         }
      }

      MarbidRole documentManagerRole;
      public MarbidRole DocumentManagerRole
      {
         get
         {
            return documentManagerRole;
         }
         set
         {
            SetPropertyValue("DocumentManagerRole", ref documentManagerRole, value);
         }
      }

      int documentLeaseMaxDays;
      [ToolTip("Maximum days the document can be leased by the user.")]
      public int DocumentLeaseMaxDays
      {
         get
         {
            return documentLeaseMaxDays;
         }
         set
         {
            SetPropertyValue("DocumentLeaseMaxDays", ref documentLeaseMaxDays, value);
         }
      }
   }
}
