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
using DevExpress.ExpressApp.Editors;
using Marbid.Module.BusinessObjects.Administration;
using Marbid.Module.CustomCodes;
using System.IO;
using System.Diagnostics;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace Marbid.Module.BusinessObjects.Library
{
    [DefaultClassOptions]
    [FileAttachment("Attachment")]
    [NavigationItem("Library")]
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Product")]
    [DefaultProperty("Title")]
    [Appearance("ReadOnlyItems", AppearanceItemType = "ViewItem", Enabled = false, TargetItems = "ResourceNumber, CreatedBy, ModifiedBy, CreateDate, ModifyDate" )]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ResourceLibrary : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ResourceLibrary(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            CreateDate = DateTime.Now;
            CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            ResourceNumber = string.Format("{0}{1}", "DOC", Stopwatch.GetTimestamp());
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            ModifiedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            ModifyDate = DateTime.Now;

            IsDocumentAttached = (Attachment != null);
            if (IsDocumentAttached)
            {
                if (Attachment.FileName.Contains("pdf"))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Attachment.SaveToStream(ms);
                        using (PdfHandling pdfHandling = new PdfHandling(ms))
                        {
                            AttachmentMetaData = pdfHandling.DocumentText;
                        }
                    }
                }
                Attachment.FileName = ResourceNumber + "." + Attachment.FileName.Split('.').Last();
            }
            else
            {
                AttachmentMetaData = string.Empty;
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

        string resourceNumber;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        [VisibleInListView(false)]
        public string ResourceNumber
        {
            get
            {
                return resourceNumber;
            }
            set
            {
                SetPropertyValue("ResourceNumber", ref resourceNumber, value);
            }
        }

        string title;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
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

        Employee createdBy;
        [VisibleInListView(false)]
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
        [VisibleInListView(false)]
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

        Employee modifiedBy;
        [VisibleInListView(false)]
        public Employee ModifiedBy
        {
            get
            {
                return modifiedBy;
            }
            set
            {
                SetPropertyValue("ModifiedBy", ref modifiedBy, value);
            }
        }

        DateTime modifyDate;
        [VisibleInListView(false)]
        public DateTime ModifyDate
        {
            get
            {
                return modifyDate;
            }
            set
            {
                SetPropertyValue("ModifyDate", ref modifyDate, value);
            }
        }

        string description;
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        [RuleRequiredField]
        [VisibleInListView(false)]
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

        private string fCategories = null;
        public string Categories
        {
            get
            {
                if (!IsLoading && !IsSaving && fCategories == null)
                    UpdateCategories(false);
                return fCategories;
            }
        }

        public void UpdateCategories(bool forceChangeEvents)
        {
            string tempCategories = null;
            string oldCategories = fCategories;
            foreach (ResourceCategory detail in ResourceCategories)
            {
                if (detail.Name != null)
                {
                    tempCategories += detail.Name;
                    tempCategories += ", ";
                }
            }
            char[] trimChar = { ',', ' ' };
            if (tempCategories != null)
                tempCategories = tempCategories.TrimEnd(trimChar);
            char[] trimCharEmail = { ';', ' ' };
            fCategories = tempCategories;
            if (forceChangeEvents)
            {
                OnChanged("Categories", oldCategories, fCategories);
            }
        }


        [Association("Resources-ResourceCategories")]
        [VisibleInDetailView(false)]
        public XPCollection<ResourceCategory> ResourceCategories
        {
            get
            {
                return GetCollection<ResourceCategory>("ResourceCategories");
            }
        }

        string attachmentMetaData;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInDetailView(false)]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [VisibleInListView(false)]
        public string AttachmentMetaData
        {
            get
            {
                return attachmentMetaData;
            }
            set
            {
                SetPropertyValue("AttachmentMetaData", ref attachmentMetaData, value);
            }
        }

        FileData attachment;
        [VisibleInListView(false)]
        public FileData Attachment
        {
            get
            {
                return attachment;
            }
            set
            {
                SetPropertyValue("Attachment", ref attachment, value);
            }
        }

        private bool _IsDocumentAttached;
        [VisibleInListView(false), VisibleInDetailView(false)]
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
    }

    [NonPersistent, NavigationItem(false), CreatableItem(false)]
    public class TemporaryResourceCategory : BaseObject
    {
        public TemporaryResourceCategory(DevExpress.Xpo.Session session)
          : base(session)
        {
        }

        public ResourceCategory Category { get; set; }
    }
}