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
   [DefaultProperty("FolderRefNumber")]
   [NavigationItem("Librarian System")]
   [ImageName("BO_Folder")]
   public class DocumentFolder : BaseObject
   {
      public DocumentFolder(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         FolderRefNumber = string.Format("{0}{1}", "FOLDER", Stopwatch.GetTimestamp());
      }
      // Fields...
      private string _ArchiveREfNumber;
      private FolderStatus _Status;
      private string _FolderRefNumber;

      [Size(SizeAttribute.DefaultStringMappingFieldSize), ReadOnly(true)]
      public string FolderRefNumber
      {
         get
         {
            return _FolderRefNumber;
         }
         set
         {
            SetPropertyValue("FolderRefNumber", ref _FolderRefNumber, value);
         }
      }

      [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      public string ArchiveREfNumber
      {
         get
         {
            return _ArchiveREfNumber;
         }
         set
         {
            SetPropertyValue("ArchiveREfNumber", ref _ArchiveREfNumber, value);
         }
      }

      public FolderStatus Status
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

      [PersistentAlias("Documents.Count()")]
      public int TotalDocuments
      {
         get
         {
            return Convert.ToInt16(EvaluateAlias("TotalDocuments"));
         }
      }

      [PersistentAlias("Documents.Sum(Pages)")]
      public int TotalPages
      {
         get
         {
            return Convert.ToInt16(EvaluateAlias("TotalPages"));
         }
      }

      DocumentBox box;
      [Association("DocumentBox-Folders")]
      public DocumentBox Box
      {
         get
         {
            return box;
         }
         set
         {
            SetPropertyValue("Box", ref box, value);
         }
      }

      [Association("DocumentFolder-Documents")]
      public XPCollection<Document> Documents
      {
         get
         {
            return GetCollection<Document>("Documents");
         }
      }
   }
}