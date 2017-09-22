using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Editors;
using Marbid.Module.BusinessObjects.Administration;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace Marbid.Module.BusinessObjects.General
{
  [DefaultClassOptions]
  [ImageName("image")]
  [DefaultProperty("Title")]
  //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
  //[Persistent("DatabaseTableName")]
  // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
  [Appearance("DisableGallery", Enabled = false,TargetItems ="CreateDate,CreatedBy")]
  public class Gallery : BaseObject
  { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    public Gallery(Session session)
        : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
      CreateDate = DateTime.Now;
      CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
    }
    string title;
    [RuleRequiredField]
    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    public string Title
    {
      get
      {
        return title;
      }
      set
      {
        SetPropertyValue("Title", ref title, value);
      }
    }
    string description;
    [Size(SizeAttribute.Unlimited)]
    [EditorAlias(EditorAliases.HtmlPropertyEditor)]
    [RuleRequiredField]
    public string Description
    {
      get
      {
        return description;
      }
      set
      {
        SetPropertyValue("Description", ref description, value);
      }
    }
    DateTime createDate;
    public DateTime CreateDate
    {
      get
      {
        return createDate;
      }
      set
      {
        SetPropertyValue("CreateDate", ref createDate, value);
      }
    }

    Employee createdBy;
    public Employee CreatedBy
    {
      get
      {
        return createdBy;
      }
      set
      {
        SetPropertyValue("CreatedBy", ref createdBy, value);
      }
    }

    [Association("Gallery-Images"), Aggregated]
    public XPCollection<GalleryImage> Images
    {
      get
      {
        return GetCollection<GalleryImage>("Images");
      }
    }
  }
}