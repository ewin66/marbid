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
using System.Diagnostics;
using Marbid.Module.BusinessObjects.Administration;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace Marbid.Module.BusinessObjects.HRM
{
  [DefaultClassOptions]
  [ImageName("announcement")]
  [NavigationItem("Main Menu")]
  [DefaultProperty("Subject")]
  [DefaultListViewOptions(MasterDetailMode.ListViewAndDetailView, false, NewItemRowPosition.None)]
  [FileAttachment("Attachment")]
  //[Persistent("DatabaseTableName")]
  // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
  [Appearance("RedPriceObject", AppearanceItemType = "ViewItem", TargetItems = "*",
    Criteria = "[Published]=false", Context = "ListView", BackColor = "Red",
        FontColor = "Maroon", Priority = 2)]
  [Appearance("AnnouncementDisabled", TargetItems = "Published,CreatedBy, CreateDate, ModifiedBy, ModifyDate", Enabled = false)]
  [Appearance("AnnouncementThisWeek", TargetItems = "Subject,RefNumber", FontStyle = System.Drawing.FontStyle.Bold, Criteria = "[CreateDate] >= LocalDateTimeThisWeek()")]
  public class Announcement : BaseObject
  { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    public Announcement(Session session)
        : base(session)
    {
    }
    private System.Boolean _sendMail;
    private System.String _refNumber;
    private System.Boolean _published;
    private Marbid.Module.BusinessObjects.HRM.AnnouncementCategory _category;
    private System.DateTime _modifyDate;
    private System.DateTime _createDate;
    private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
    private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
    private System.String _content;
    private System.String _subject;

    [Action(Caption = "Publish", TargetObjectsCriteria = "Not [Published] And IsNewObject(this) = false And [CreatedBy].[Oid] = CurrentUserId()", ImageName = "Action_ShowItemOnDashboard", AutoCommit = true)]
    public void Publish()
    {
      Published = true;
    }
    [Action(Caption = "Unpublish", TargetObjectsCriteria = "[Published] = true and [CreatedBy].[Oid] = CurrentUserId()", ImageName = "Action_HideItemFromDashboard")]
    public void Unpublish()
    {
      Published = false;
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      CreateDate = DateTime.Now;
      CreatedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
      RefNumber = string.Format("{0}{1}", CreatedBy.Division.Code, Stopwatch.GetTimestamp());
      AnnouncementRecepients defaultRecepient = new AnnouncementRecepients(this.Session);
      defaultRecepient.Employee = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
      defaultRecepient.Email = defaultRecepient.Employee.CorporateEmail;
      AnnouncementRecepients.Add(defaultRecepient);
      Published = false;
    }
    protected override void OnSaving()
    {
      base.OnSaving();
      ModifyDate = DateTime.Now;
      ModifiedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
    }
    [RuleRequiredField]
    [Size(SizeAttribute.Unlimited)]
    [ModelDefault("RowCount","1")]
    [EditorAlias(EditorAliases.StringPropertyEditor)]
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
    [Size(SizeAttribute.Unlimited)]
    [ModelDefault("RowCount", "1")]
    [EditorAlias(EditorAliases.StringPropertyEditor)]
    public System.String RefNumber
    {
      get
      {
        return _refNumber;
      }
      set
      {
        SetPropertyValue("RefNumber", ref _refNumber, value);
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
    [CaptionsForBoolValues("Published", "Not Published")]
    public System.Boolean Published
    {
      get
      {
        return _published;
      }
      set
      {
        SetPropertyValue("Published", ref _published, value);
      }
    }
    [DevExpress.Xpo.SizeAttribute(SizeAttribute.Unlimited)]
    [RuleRequiredField]
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
    [RuleRequiredField("", DefaultContexts.Save)]
    [DataSourceCriteria("Division.Oid = '@This.CreatedBy.Division.Oid'")]
    public Marbid.Module.BusinessObjects.HRM.AnnouncementCategory Category
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
    [Association("AnnouncementRecepients-Announcement"), DevExpress.Xpo.Aggregated]
    [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
    public XPCollection<Marbid.Module.BusinessObjects.HRM.AnnouncementRecepients> AnnouncementRecepients
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.HRM.AnnouncementRecepients>("AnnouncementRecepients");
      }
    }
    string fRecepientEmails;
    [VisibleInDetailView(false),
    VisibleInListView(false)]
    [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
    public string RecepientEmails
    {
      get
      {
        if (!IsLoading && !IsSaving && fRecepientEmails == null)
          UpdateRecepientEmails(false);
        return fRecepientEmails;
      }
    }
    public void UpdateRecepientEmails(bool forceChangeEvents)
    {
      string tempRecepientEmails = null;
      string oldRecepientEmails = fRecepientEmails;
      foreach (AnnouncementRecepients detail in AnnouncementRecepients)
      {
        tempRecepientEmails += detail.Email;
        tempRecepientEmails += "; ";
      }
      char[] trimChar = { ';', ' ' };
      fRecepientEmails = tempRecepientEmails.TrimEnd(trimChar);
      if (forceChangeEvents)
        OnChanged("RecepientEmails", oldRecepientEmails, fRecepientEmails);
    }
    [CaptionsForBoolValues("Send Mail", "Do Not Send Mail")]
    public System.Boolean SendMail
    {
      get
      {
        return _sendMail;
      }
      set
      {
        SetPropertyValue("SendMail", ref _sendMail, value);
      }
    }
    FileData attachement;
    public FileData Attachment
    {
      get
      {
        return attachement;
      }
      set
      {
        SetPropertyValue("Attachment", ref attachement, value);
      }
    }
  }
}