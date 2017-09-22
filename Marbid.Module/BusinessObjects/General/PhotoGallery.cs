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
   [NavigationItem("Library")]
   [ImageName("BO_Gallery")]
   [DefaultProperty("Title")]
   [Appearance("DisablePhoto", Enabled = false, TargetItems = "CreatedBy, CreateDate")]
   [Appearance("Gallery", TargetItems = "Title", FontStyle = System.Drawing.FontStyle.Bold, Criteria = "[CreateDate] >= LocalDateTimeThisWeek()")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class PhotoGallery : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public PhotoGallery(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
         CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
         CreateDate = DateTime.Now;
      }
      //private string _PersistentProperty;
      //[XafDisplayName("My display name"), ToolTip("My hint message")]
      //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
      //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
      //public string PersistentProperty {
      //    get { return _PersistentProperty; }
      //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
      //}

      //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
      //public void ActionMethod() {
      //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
      //    this.PersistentProperty = "Paid";
      //}
      string title;
      [Size(SizeAttribute.Unlimited)]
      [ModelDefault("RowCount", "1")]
      [EditorAlias(EditorAliases.StringPropertyEditor)]
      [RuleRequiredField]
      public string Title
      {
         get
         {
            return title;
         }
         set
         {
            SetPropertyValue("Tilte", ref title, value);
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
      DateTime createDate;
      [ModelDefault("DisplayFormat", "g")]
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
      [Association("Gallery-Photos"), Aggregated]
      public XPCollection<Photo> Photos
      {
         get
         {
            return GetCollection<Photo>("Photos");
         }
      }
   }
}