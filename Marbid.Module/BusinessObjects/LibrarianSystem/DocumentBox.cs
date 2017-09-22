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

namespace Marbid.Module.BusinessObjects.LibrarianSystem
{
   [DefaultClassOptions]
   [ImageName("BO_Product")]
   [NavigationItem("Librarian System")]
   [DefaultProperty("BoxRefNumber")]
   //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class DocumentBox : BaseObject
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public DocumentBox(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
         BoxRefNumber = string.Format("{0}{1}", "BOX", Stopwatch.GetTimestamp());
      }

      string boxRefNumber;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string BoxRefNumber
      {
         get
         {
            return boxRefNumber;
         }
         set
         {
            SetPropertyValue("BoxRefNumber", ref boxRefNumber, value);
         }
      }

      string vendorRefNumber;
      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string VendorRefNumber
      {
         get
         {
            return vendorRefNumber;
         }
         set
         {
            SetPropertyValue("VendorRefNumber", ref vendorRefNumber, value);
         }
      }

      [PersistentAlias("Folders.Sum(TotalDocuments)")]
      public int TotalDocuments
      {
         get
         {
            return Convert.ToInt16(EvaluateAlias("TotalDocuments"));
         }
      }

      [PersistentAlias("Folders.Sum(TotalPages)")]
      public int TotalPages
      {
         get
         {
            return Convert.ToInt16(EvaluateAlias("TotalPages"));
         }
      }

      [PersistentAlias("Folders.Count()")]
      public int TotalFolders
      {
         get
         {
            return Convert.ToInt16(EvaluateAlias("TotalFolders"));
         }
      }

      [Association("DocumentBox-Folders")]
      public XPCollection<DocumentFolder> Folders
      {
         get
         {
            return GetCollection<DocumentFolder>("Folders");
         }
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
   }
}