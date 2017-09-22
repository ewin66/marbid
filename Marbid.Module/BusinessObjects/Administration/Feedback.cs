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
using DevExpress.ExpressApp.Editors;

namespace Marbid.Module.BusinessObjects.Administration
{
   [DefaultClassOptions]
   [ImageName("Mail")]
   [NavigationItem("Main Menu")]
   [Appearance("FeedbackDisableProperty", Enabled = false, TargetItems = "CreateDate,CreatedBy,ModifiedBy,ModifyDate")]
   public class Feedback : BaseObject
   {
      private System.DateTime _lastDescriptionDate;
      private Marbid.Module.BusinessObjects.Administration.Employee _lastDescriptionEmployee;
      private System.String _lastDescription;
      private Marbid.Module.BusinessObjects.Administration.FeedbackType _feedbackType;
      private System.String _description;
      private System.DateTime _createDate;
      private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
      private Marbid.Module.BusinessObjects.Administration.FeedbackCategory _category;
      private System.String _subject;
      public Feedback(DevExpress.Xpo.Session session)
        : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         CreateDate = DateTime.Now;
         CreatedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
      }
      protected override void OnSaving()
      {
         base.OnSaving();
         if (Session.IsNewObject(this))
         {
            LastDescription = Description;
            LastDescriptionDate = CreateDate;
            LastDescriptionEmployee = CreatedBy;
         }
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
      [RuleRequiredField]
      [DevExpress.Xpo.AssociationAttribute("Feedbacks-Category")]
      public Marbid.Module.BusinessObjects.Administration.FeedbackCategory Category
      {
         get
         {
            return _category;
         }
         set
         {
            SetPropertyValue("Category", ref _category, value);
         }
      }
      [Size(SizeAttribute.Unlimited)]
      [RuleRequiredField]
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
      [DevExpress.Xpo.AssociationAttribute("Feedbacks-FeedbackType")]
      [RuleRequiredField]
      public Marbid.Module.BusinessObjects.Administration.FeedbackType FeedbackType
      {
         get
         {
            return _feedbackType;
         }
         set
         {
            SetPropertyValue("FeedbackType", ref _feedbackType, value);
         }
      }
      [DevExpress.Persistent.Base.VisibleInDetailViewAttribute(false)]
      [DevExpress.Persistent.Base.VisibleInListViewAttribute(false)]
      [DevExpress.Persistent.Base.VisibleInLookupListViewAttribute(false)]
      [Size(SizeAttribute.Unlimited)]
      public System.String LastDescription
      {
         get
         {
            return _lastDescription;
         }
         set
         {
            SetPropertyValue("LastDescription", ref _lastDescription, value);
         }
      }
      [DevExpress.Persistent.Base.VisibleInDetailViewAttribute(false)]
      [DevExpress.Persistent.Base.VisibleInListViewAttribute(false)]
      [DevExpress.Persistent.Base.VisibleInLookupListViewAttribute(false)]
      public Marbid.Module.BusinessObjects.Administration.Employee LastDescriptionEmployee
      {
         get
         {
            return _lastDescriptionEmployee;
         }
         set
         {
            SetPropertyValue("LastDescriptionEmployee", ref _lastDescriptionEmployee, value);
         }
      }
      [DevExpress.Persistent.Base.VisibleInDetailViewAttribute(false)]
      [DevExpress.Persistent.Base.VisibleInListViewAttribute(false)]
      [DevExpress.Persistent.Base.VisibleInLookupListViewAttribute(false)]
      public System.DateTime LastDescriptionDate
      {
         get
         {
            return _lastDescriptionDate;
         }
         set
         {
            SetPropertyValue("LastDescriptionDate", ref _lastDescriptionDate, value);
         }
      }
      [DevExpress.Persistent.Base.VisibleInDetailViewAttribute(false)]
      [DevExpress.Persistent.Base.VisibleInListViewAttribute(false)]
      [DevExpress.Persistent.Base.VisibleInLookupListViewAttribute(false)]
      [NonPersistent]
      public string CreatedByEmail
      {
         get { return CreatedBy.CorporateEmail; }
      }
      [DevExpress.Persistent.Base.VisibleInDetailViewAttribute(false)]
      [DevExpress.Persistent.Base.VisibleInListViewAttribute(false)]
      [DevExpress.Persistent.Base.VisibleInLookupListViewAttribute(false)]
      [NonPersistent]
      public string UpdaterEmail
      {
         get { return LastDescriptionEmployee.CorporateEmail; }
      }
   }
   [DomainComponent]
   [ModelDefault("Caption", "Please Enter Your Feedback")]
   public class ReplyParametersObject
   {
      public static Type ReplyParameterType = typeof(ReplyParametersObject);
      public static ReplyParametersObject CreateReplyParametersObject()
      {
         return (ReplyParametersObject)ReflectionHelper.CreateObject(ReplyParameterType);
      }
      public ReplyParametersObject()
      {
      }
      [Size(SizeAttribute.Unlimited)]
      [RuleRequiredField]
      [EditorAlias(EditorAliases.HtmlPropertyEditor)]
      public string Reply { get; set; }
   }
}
