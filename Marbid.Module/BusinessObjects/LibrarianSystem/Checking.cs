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
using DevExpress.ExpressApp.Editors;
using System.Diagnostics;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace Marbid.Module.BusinessObjects.LibrarianSystem
{
   [DefaultClassOptions]
   [DefaultProperty("CheckoutRefNumber")]
   [NavigationItem("Librarian System")]
   [Appearance("CheckingDisabled", TargetItems = "CheckingStatus,DeliveryDate,CheckedOutBy,CheckInDate,CheckoutRefNumber,CheackoutRefNumber,RequestDate", Enabled = false)]
   public class Checking : BaseObject
   {
      public Checking(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         CheckedOutBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
         RequestDate = DateTime.Now;
         Status = CheckingStatus.Requested;
         CheckoutRefNumber = string.Format("{0}{1}", "CO", Stopwatch.GetTimestamp());
         SystemSetting setting = Session.FindObject<SystemSetting>(null);
         DueDate = DateTime.Now.AddDays((double)setting.DocumentLeaseMaxDays);
      }
      static string DueDateMessage = "";
      protected override void OnLoaded()
      {
         base.OnLoaded();
         SystemSetting setting = Session.FindObject<SystemSetting>(null);
         DueDateMessage = string.Format("Due date can not be exceed {0} days from today!", setting.DocumentLeaseMaxDays);
      }
      private CheckingStatus _Status;
      private DateTime _DeliveryDate;
      private Document _Document;
      private Employee _CheckedOutBy;
      private DateTime _CheckInDate;
      private DateTime _DueDate;
      private string _CheckoutRefNumber;
      private DateTime _RequestDate;

      [Association("Document-Checkings")]
      public Document Document
      {
         get
         {
            return _Document;
         }
         set
         {
            SetPropertyValue("Document", ref _Document, value);
         }
      }

      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string CheckoutRefNumber
      {
         get
         {
            return _CheckoutRefNumber;
         }
         set
         {
            SetPropertyValue("CheckoutRefNumber", ref _CheckoutRefNumber, value);
         }
      }

      public CheckingStatus Status
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
      [ModelDefault("DisplayFormat", "{0:g}")]
      public DateTime RequestDate
      {
         get
         {
            return _RequestDate;
         }
         set
         {
            SetPropertyValue("RequestDate", ref _RequestDate, value);
         }
      }
      [ModelDefault("DisplayFormat", "{0:g}")]
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
      //[RuleFromBoolProperty(CustomMessageTemplate = "Due date can not exceed allowed document lease by the system", Name = "CheckingDueDate", TargetPropertyName = "DueDate")]
      [RuleFromBoolProperty("CheckingDueDate", DefaultContexts.Save, "Due date cannot exceed alowed document lease days.", UsedProperties = "DueDate")]
      protected bool IsDueDateValid
      {
         get
         {
            SystemSetting setting = Session.FindObject<SystemSetting>(null);
            if (DueDate <= DateTime.Now.AddDays(setting.DocumentLeaseMaxDays))
               return true;
            else
               return false;
         }
      }
      [ModelDefault("DisplayFormat", "{0:g}")]
      public DateTime CheckInDate
      {
         get
         {
            return _CheckInDate;
         }
         set
         {
            SetPropertyValue("CheckInDate", ref _CheckInDate, value);
         }
      }

      public Employee CheckedOutBy
      {
         get
         {
            return _CheckedOutBy;
         }
         set
         {
            SetPropertyValue("CheckedOutBy", ref _CheckedOutBy, value);
         }
      }
      [ModelDefault("DisplayFormat", "{0:g}")]
      public DateTime DeliveryDate
      {
         get
         {
            return _DeliveryDate;
         }
         set
         {
            SetPropertyValue("DeliveryDate", ref _DeliveryDate, value);
         }
      }

      DateTime receivedDate;
      [ModelDefault("DisplayFormat", "{0:g}")]
      public DateTime ReceivedDate
      {
         get
         {
            return receivedDate;
         }
         set
         {
            SetPropertyValue("ReceivedDate", ref receivedDate, value);
         }
      }

      string comment;
      [Size(SizeAttribute.Unlimited)]
      [EditorAlias(EditorAliases.HtmlPropertyEditor)]
      public string Comment
      {
         get
         {
            return comment;
         }
         set
         {
            SetPropertyValue("Commenct", ref comment, value);
         }
      }

      #region Actions
      [Action(AutoCommit = true, ImageName = "cart", Caption = "Deliver Document", ConfirmationMessage = "Please make sure that the selected document is ready to be delivered, Confirm that you are ready to deliver the document?")]
      public void DocumentDelivery()
      {

      }
      [Action(AutoCommit = true, Caption = "Document Received", ConfirmationMessage = "Please make sure that you have received the correct document(s), Confirm document received?")]
      public void DocumentReceived()
      {

      }
      [Action(AutoCommit = true, ImageName = "checkin", Caption = "Checkin Document", ConfirmationMessage = "Please make sure that the selected document is ready to be picked up by The Librarian, Are you sure want to checkin the document?")]
      public void CheckInDocument()
      {

      }
      [Action(AutoCommit = true, ImageName = "checkedin", Caption = "Document Checked In", ConfirmationMessage = "Please make sure that the selected document is already received and stored to its corespondent folder, Confirm document checkedin?")]
      public void DocumentCheckedIn()
      {

      }
      #endregion
   }
}