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
using System.Drawing;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace Marbid.Module.BusinessObjects.CRM
{
  public interface INewsItem
  {
    string ID { get; }
    string Title { get; }
    MediaDataObject Image { get; }
    string Text { get; }
    DateTime Date { get; }
  }
  [DefaultClassOptions]
  [NavigationItem("Main Menu")]
  [ImageName("News")]
  [DefaultProperty("Title")]
  [Appearance("NewsAppearance", Enabled = false, TargetItems = "CreateDate, Author")]
  public class News : BaseObject, INewsItem
  {
    public News(Session session)
        : base(session)
    {
    }
    private System.Boolean _runningText;
    private Marbid.Module.BusinessObjects.Administration.Employee _author;
    private System.DateTime _createDate;
    private System.Boolean _published;
    private System.String _sourceURL;
    private Marbid.Module.BusinessObjects.CRM.NewsCategory _newsCategory;
    private System.String _content;
    private System.String _shortDescription;
    private System.String _title;
    private MediaDataObject _cover;
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      CreateDate = DateTime.Now;
      Author = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
    }
    [Persistent("RelatedOrganization")]
    private string fRelatedOrganization;
    [PersistentAlias("fRelatedOrganization")]
    public string RelatedOrganization
    {
      get
      {
        if (!IsLoading && !IsSaving && fRelatedOrganization == null)
          UpdateRelatedOrganization(false);
        return fRelatedOrganization;
      }
    }
    public void UpdateRelatedOrganization(bool forceChangeEvents)
    {
      if (RelatedOrganizations.Count == 0)
      {
        fRelatedOrganization = null;
        return;
      }
      string tempRelatedOrganization = null;
      string oldRelatedOrganization = fRelatedOrganization;
      foreach (Organization detail in RelatedOrganizations)
      {
        tempRelatedOrganization += detail.DisplayName;
        tempRelatedOrganization += ",";
      }
      char[] trimChar = { ',' };
      tempRelatedOrganization = tempRelatedOrganization.TrimEnd(trimChar);
      fRelatedOrganization = tempRelatedOrganization;
      if (forceChangeEvents)
        OnChanged("RelatedOrganization", oldRelatedOrganization, fRelatedOrganization);
    }
    [RuleRequiredField]
    public System.String Title
    {
      get
      {
        return _title;
      }
      set
      {
        SetPropertyValue("Title", ref _title, value);
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
    public Marbid.Module.BusinessObjects.Administration.Employee Author
    {
      get
      {
        return _author;
      }
      set
      {
        SetPropertyValue("Author", ref _author, value);
      }
    }
    [Size(SizeAttribute.Unlimited)]
    [RuleRequiredField]
    public System.String ShortDescription
    {
      get
      {
        return _shortDescription;
      }
      set
      {
        SetPropertyValue("ShortDescription", ref _shortDescription, value);
      }
    }
    [RuleRequiredField]
    [DevExpress.Xpo.SizeAttribute(SizeAttribute.Unlimited)]
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
    [DevExpress.Xpo.AssociationAttribute("News-NewsCategory")]
    [RuleRequiredField]
    public Marbid.Module.BusinessObjects.CRM.NewsCategory NewsCategory
    {
      get
      {
        return _newsCategory;
      }
      set
      {
        SetPropertyValue("NewsCategory", ref _newsCategory, value);
      }
    }
    [DevExpress.Xpo.SizeAttribute(300)]
    public System.String SourceURL
    {
      get
      {
        return _sourceURL;
      }
      set
      {
        SetPropertyValue("SourceURL", ref _sourceURL, value);
      }
    }
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
    public System.Boolean RunningText
    {
      get
      {
        return _runningText;
      }
      set
      {
        SetPropertyValue("RunningText", ref _runningText, value);
      }
    }
    [DevExpress.Xpo.AssociationAttribute("RelatedOrganizations-InTheNews"), DevExpress.Xpo.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
    public XPCollection<Marbid.Module.BusinessObjects.CRM.Organization> RelatedOrganizations
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.CRM.Organization>("RelatedOrganizations");
      }
    }
    public MediaDataObject Cover
    {
      get { return _cover; }
      set { SetPropertyValue("Cover", ref _cover, value); }
    }
    MediaDataObject INewsItem.Image
    {
      get { return Cover; }
    }
    string INewsItem.Title
    {
      get { return Title; }
    }
    DateTime INewsItem.Date
    {
      get { return CreateDate; }
    }
    string INewsItem.Text
    {
      get { return ShortDescription; }
    }
    string INewsItem.ID
    {
      get { return Oid.ToString(); }
    }
    [Action("View", Caption ="Open Source", ToolTip = "Open news source in web browser", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject, ImageName = "BO_Country_v92")]
    public void OpenSource()
    {
      System.Diagnostics.Process.Start(SourceURL);
    }
  }
  [DomainComponent]
  public class ShortDescriptionParametersObject
  {
    public static Type ShortDescriptionParametersType = typeof(ShortDescriptionParametersObject);
    public static ShortDescriptionParametersObject CreateShortDescriptionParametersObject()
    {
      return (ShortDescriptionParametersObject)ReflectionHelper.CreateObject(ShortDescriptionParametersType);
    }
    [Size(SizeAttribute.Unlimited)]
    public string ShortDescription { get; set; }
  }
}