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
using System.Diagnostics;
using DevExpress.ExpressApp.Editors;
using System.IO;
using Marbid.Module.CustomCodes;
using DevExpress.ExpressApp.ConditionalAppearance;
using Marbid.Module.BusinessObjects.CRM;

namespace Marbid.Module.BusinessObjects.LibrarianSystem
{
   [DefaultClassOptions]
   [DefaultProperty("RegistrationNumber")]
   [FileAttachment("Attachment")]
   [NavigationItem("Librarian System")]
   [ImageName("BO_Library")]
   [Appearance("DocumentDisabled", Enabled = false, TargetItems = "RegisteredBy,ModifiedBy,VerifiedBy,RegisterDate,ModifyDate,VerifyDate,RegistrationNumber,IsVerified,DocumentStatus")]
   public class Document : BaseObject
   {
      public Document(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();

         RegisterDate = DateTime.Now;
         RegisteredBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
         RegistrationNumber = string.Format("{0}{1}", "DOC", Stopwatch.GetTimestamp());
      }
      protected override void OnSaving()
      {
         base.OnSaving();
         IsDocumentAttached = (Attachment != null);
         if (IsDocumentAttached)
         {
            using (MemoryStream ms = new MemoryStream())
            {
               Attachment.SaveToStream(ms);
               using (PdfHandling pdfHandling = new PdfHandling(ms))
               {
                  Excerpt = pdfHandling.DocumentText;
                  Pages = pdfHandling.PageCount;
               }
            }
            Attachment.FileName = RegistrationNumber + "." + Attachment.FileName.Split('.').Last();
         } else
         {
            Excerpt = string.Empty;
            ScanDate = DateTime.MinValue;
            ScannedBy = null;
            Pages = 0;
         }
         
         ModifyDate = DateTime.Now;
         ModifiedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
      }
      private Employee _VerifiedBy;
      private DateTime _VerifyDate;
      private bool _IsDocumentAttached;
      private DateTime _ScanDate;
      private Employee _ScannedBy;
      private bool _IsHardCopy;
      private DocumentSource _Source;
      private bool _IsVerified;
      private DocumentFolder _Folder;
      private DocumentType _DocumentType;
      private string _ArchiveRefNumber;
      private Employee _ModifiedBy;
      private DateTime _ModifyDate;
      private bool _IsLocked;
      private Employee _DocumentTo;
      private CRM.Organization _DocumentFrom;
      private string _Excerpt;
      private DocumentStatus _DocumentStatus;
      private DateTime _RegisterDate;
      private Employee _RegisteredBy;
      private string _RegistrationNumber;
      private string _Subject;
      private FileData attachment;

      
      public string RegistrationNumber
      {
         get
         {
            return _RegistrationNumber;
         }
         set
         {
            SetPropertyValue("RegistrationNumber", ref _RegistrationNumber, value);
         }
      }

      string senderRefNumber;
      [Size(SizeAttribute.Unlimited)]
      [ModelDefault("RowCount","1")]
      [EditorAlias(EditorAliases.StringPropertyEditor)]
      public string SenderRefNumber
      {
         get
         {
            return senderRefNumber;
         }
         set
         {
            SetPropertyValue("SenderRefNumber", ref senderRefNumber, value);
         }
      }

      [Size(SizeAttribute.DefaultStringMappingFieldSize), RuleRequiredField, ToolTip("Subject of cover letter or the title of the document", "Subject", DevExpress.Utils.ToolTipIconType.Information)]
      public string Subject
      {
         get
         {
            return _Subject;
         }
         set
         {
            SetPropertyValue("Subject", ref _Subject, value);
         }
      }

      [Size(SizeAttribute.DefaultStringMappingFieldSize), ReadOnly(true), VisibleInListView(false), VisibleInLookupListView(false)]
      public Employee RegisteredBy
      {
         get
         {
            return _RegisteredBy;
         }
         set
         {
            SetPropertyValue("RegisteredBy", ref _RegisteredBy, value);
         }
      }

      [VisibleInListView(false), VisibleInLookupListView(false)]
      [ModelDefault("DisplayFormat", "{0:g}")]
      public DateTime RegisterDate
      {
         get
         {
            return _RegisterDate;
         }
         set
         {
            SetPropertyValue("RegisterDate", ref _RegisterDate, value);
         }
      }

      [VisibleInListView(false), VisibleInLookupListView(false)]
      public Employee ModifiedBy
      {
         get
         {
            return _ModifiedBy;
         }
         set
         {
            SetPropertyValue("ModifiedBy", ref _ModifiedBy, value);
         }
      }

      [VisibleInListView(false), VisibleInLookupListView(false)]
      [ModelDefault("DisplayFormat", "{0:g}")]
      public DateTime ModifyDate
      {
         get
         {
            return _ModifyDate;
         }
         set
         {
            SetPropertyValue("ModifyDate", ref _ModifyDate, value);
         }
      }

      [VisibleInListView(false), VisibleInLookupListView(false)]
      public Employee ScannedBy
      {
         get
         {
            return _ScannedBy;
         }
         set
         {
            SetPropertyValue("ScannedBy", ref _ScannedBy, value);
         }
      }

      [VisibleInListView(false), VisibleInLookupListView(false)]
      [ModelDefault("DisplayFormat", "{0:g}")]
      public DateTime ScanDate
      {
         get
         {
            return _ScanDate;
         }
         set
         {
            SetPropertyValue("ScanDate", ref _ScanDate, value);
         }
      }
      public DocumentStatus DocumentStatus
      {
         get
         {
            return _DocumentStatus;
         }
         set
         {
            SetPropertyValue("DocumentStatus", ref _DocumentStatus, value);
         }
      }

      [CaptionsForBoolValues("Verified", "Not Verified"), VisibleInListView(false)]
      public bool IsVerified
      {
         get
         {
            return _IsVerified;
         }
         set
         {
            SetPropertyValue("IsVerified", ref _IsVerified, value);
         }
      }

      [CaptionsForBoolValues("Attached", "Not Attached"), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
      public bool IsDocumentAttached
      {
         get
         {
            return _IsDocumentAttached;
         }
         set
         {
            SetPropertyValue("IsDocumentAttached", ref _IsDocumentAttached, value);
         }
      }

      [VisibleInListView(false), VisibleInLookupListView(false)]
      public DocumentSource Source
      {
         get
         {
            return _Source;
         }
         set
         {
            SetPropertyValue("Source", ref _Source, value);
         }
      }

      [CaptionsForBoolValues("Hard Copy Available", "No Hard Copy"), VisibleInListView(false), VisibleInLookupListView(false)]
      public bool IsHardCopy
      {
         get
         {
            return _IsHardCopy;
         }
         set
         {
            SetPropertyValue("IsHardCopy", ref _IsHardCopy, value);
         }
      }

      [RuleRequiredField]
      public DocumentType DocumentType
      {
         get
         {
            return _DocumentType;
         }
         set
         {
            SetPropertyValue("DocumentType", ref _DocumentType, value);
         }
      }

      [Association("DocumentFolder-Documents"), VisibleInListView(false)]
      public DocumentFolder Folder
      {
         get
         {
            return _Folder;
         }
         set
         {
            SetPropertyValue("Folder", ref _Folder, value);
         }
      }

      [RuleRequiredField, ToolTip("Organization or Ceding Company sent this document", "From", DevExpress.Utils.ToolTipIconType.Information), DevExpress.Xpo.DisplayName("From")]
      public CRM.Organization DocumentFrom
      {
         get
         {
            return _DocumentFrom;
         }
         set
         {
            SetPropertyValue("DocumentFrom", ref _DocumentFrom, value);
         }
      }

      Contact contact;
      [DataSourceProperty("DocumentFrom.Contacts")]
      [VisibleInListView(false)]
      public Contact Contact
      {
         get
         {
            return contact;
         }
         set
         {
            SetPropertyValue("Contact", ref contact, value);
         }
      }

      [RuleRequiredField, ToolTip("Intended recipient or PIC of this document", "To", DevExpress.Utils.ToolTipIconType.Information), DevExpress.Xpo.DisplayName("To")]
      public Employee DocumentTo
      {
         get
         {
            return _DocumentTo;
         }
         set
         {
            SetPropertyValue("DocumentTo", ref _DocumentTo, value);
         }
      }

      [CaptionsForBoolValues("Locked", "Unlocked"), VisibleInListView(false)]
      public bool IsLocked
      {
         get
         {
            return _IsLocked;
         }
         set
         {
            SetPropertyValue("IsLocked", ref _IsLocked, value);
         }
      }

      [Size(SizeAttribute.DefaultStringMappingFieldSize), VisibleInListView(false)]
      public string ArchiveRefNumber
      {
         get
         {
            return _ArchiveRefNumber;
         }
         set
         {
            SetPropertyValue("ArchiveRefNumber", ref _ArchiveRefNumber, value);
         }
      }

      [FileTypeFilter("Adobe Portable File", 1, "*.pdf")]
      public FileData Attachment
      {
         get { return attachment; }
         set
         {
            SetPropertyValue("File", ref attachment, value);
         }
      }

      [Size(SizeAttribute.Unlimited)]
      [EditorAlias(EditorAliases.HtmlPropertyEditor)]
      [ToolTip("Some part of document content an will be available for indexing and searching", "Excerpt", DevExpress.Utils.ToolTipIconType.Information)]
      public string Excerpt
      {
         get
         {
            return _Excerpt;
         }
         set
         {
            SetPropertyValue("Excerpt", ref _Excerpt, value);
         }
      }

      public Employee VerifiedBy
      {
         get
         {
            return _VerifiedBy;
         }
         set
         {
            SetPropertyValue("VerifiedBy", ref _VerifiedBy, value);
         }
      }
      [ModelDefault("DisplayFormat", "{0:g}")]
      public DateTime VerifyDate
      {
         get
         {
            return _VerifyDate;
         }
         set
         {
            SetPropertyValue("VerifyDate", ref _VerifyDate, value);
         }
      }

      int pages;
      public int Pages
      {
         get { return pages; }
         set { SetPropertyValue("Pages", ref pages, value); }
      }

      [Association("Document-Checkings"), DevExpress.Xpo.Aggregated]
      public XPCollection<Checking> Checkings
      {
         get
         {
            return GetCollection<Checking>("Checkings");
         }
      }
   }
   #region ActionParameters
   [DomainComponent]
   public class CheckoutActionParameter
   {
      [RuleRequiredField]
      public DateTime RequiredDateTime { get; set; }
      [RuleRequiredField]
      public string MessageToLibrarian { get; set; }
   }
   #endregion
}