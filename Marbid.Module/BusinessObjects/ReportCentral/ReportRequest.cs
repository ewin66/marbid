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
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace Marbid.Module.BusinessObjects.ReportCentral
{
    [DefaultClassOptions]
    [NavigationItem("Reports and Statistics")]
    [ImageName("groupadd")]
    [Appearance("Disabled", TargetItems = "RequestBy,RequestDate", Enabled = false)]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ReportRequest : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ReportRequest(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            RequestBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        string subject;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                SetPropertyValue("Subject", ref subject, value);
            }
        }
        string requestDate;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string RequestDate
        {
            get
            {
                return requestDate;
            }
            set
            {
                SetPropertyValue("RequestDate", ref requestDate, value);
            }
        }
        Employee requestBy;
        public Employee RequestBy
        {
            get
            {
                return requestBy;
            }
            set
            {
                SetPropertyValue("RequestBy", ref requestBy, value);
            }
        }
        FileData reportFormat;
        public FileData ReportFormat
        {
            get
            {
                return reportFormat;
            }
            set
            {
                SetPropertyValue("ReportFormat", ref reportFormat, value);
            }
        }
        string description;
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
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

        string userAccess;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string UserAccess
        {
            get
            {
                userAccess = null;
                if (!IsLoading && !IsSaving && ReportRequestUsers.Count > 0)
                {
                    foreach(ReportRequestUser emp in ReportRequestUsers)
                    {
                        userAccess += emp.User.UserName + ", ";
                    }
                    char[] MyChar = { ',', ' ' };
                    userAccess += userAccess.TrimEnd(MyChar);
                    userAccess += userAccess.TrimEnd(MyChar);
                }
                return userAccess;
            }
        }

        [Association("ReportRequest-ReportRequestUsers"), DevExpress.Xpo.Aggregated]
        [ModelDefault("Caption", "User Access")]
        public XPCollection<ReportRequestUser> ReportRequestUsers
        {
            get
            {
                return GetCollection<ReportRequestUser>("ReportRequestUsers");
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