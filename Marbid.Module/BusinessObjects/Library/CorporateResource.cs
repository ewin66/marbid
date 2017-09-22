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
namespace Marbid.Module.BusinessObjects.Library
{
    [DefaultClassOptions]
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_Product")]
    [NavigationItem("Library")]
    [DefaultProperty("Title")]
    public class CorporateResource : BaseObject
    {
        private System.DateTime _createDate;
        private System.DateTime _modifyDate;
        private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
        private Marbid.Module.BusinessObjects.Administration.Employee _modifiedBy;
        private Marbid.Module.BusinessObjects.Library.CorporateResourceCategory _category;
        private System.String _content;
        private System.String _title;
        public CorporateResource(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        protected override void OnLoaded()
        {
            Reset();
            base.OnLoaded();
        }
        private void Reset()
        {
            _latestFile = null;
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
            ModifyDate = DateTime.Now;
            ModifiedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
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
        [RuleRequiredField]
        public Marbid.Module.BusinessObjects.Library.CorporateResourceCategory Category
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
        [RuleRequiredField]
        [Size(SizeAttribute.Unlimited)]
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
        [Association("Attachments-CorporateResource"), DevExpress.Xpo.Aggregated]
        public XPCollection<Marbid.Module.BusinessObjects.Library.CorporateResourceAttachment> Attachments
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.Library.CorporateResourceAttachment>("Attachments");
            }
        }
        private FileData _latestFile = null;
        public FileData LatestFile
        {
            get
            {
                if (!IsSaving && !IsLoading && _latestFile == null)
                {
                    GetLatestFile(false);
                }
                return _latestFile;
            }
        }
        public void GetLatestFile(Boolean Force)
        {
            FileData oldLatestFile = _latestFile;
            DateTime maxDate = Convert.ToDateTime("1990/1/1");
            foreach (CorporateResourceAttachment detail in Attachments)
            {
                if (maxDate < detail.CreateDate)
                {
                    _latestFile = detail.File;
                }
            }
            if (Force)
                OnChanged("LatestFile", oldLatestFile, _latestFile);
        }
    }
}
