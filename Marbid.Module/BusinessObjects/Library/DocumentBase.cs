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
    [DevExpress.Persistent.Base.ImageNameAttribute("BO_FileAttachment")]
    [NavigationItem(false)]
    [CreatableItem(false)]
    [FileAttachment("File")]
    public class DocumentBase : BaseObject
    {
        private Marbid.Module.BusinessObjects.Administration.Employee _owner;
        private System.DateTime _createDate;
        private System.String _description;
        private System.String _subject;
        private DevExpress.Persistent.BaseImpl.FileData _file;
        public DocumentBase(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CreateDate = DateTime.Now;
            Owner = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
        }
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
        public Marbid.Module.BusinessObjects.Administration.Employee Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                SetPropertyValue("Owner", ref _owner, value);
            }
        }
        [Size(SizeAttribute.Unlimited)]
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
        public DevExpress.Persistent.BaseImpl.FileData File
        {
            get
            {
                return _file;
            }
            set
            {
                SetPropertyValue("File", ref _file, value);
            }
        }
    }
}
