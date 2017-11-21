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

namespace Marbid.Module.BusinessObjects.ReportCentral
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ReportParameter : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ReportParameter(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            ParameterIndex = 0;
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
        string name;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetPropertyValue("Name", ref name, value);
            }
        }

        string caption;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Caption
        {
            get
            {
                return caption;
            }
            set
            {
                SetPropertyValue("Caption", ref caption, value);
            }
        }

        string defaultValue;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                SetPropertyValue("DefaultValue", ref defaultValue, value);
            }
        }

        ParameterType parameterType;
        [RuleRequiredField]
        public ParameterType ParameterType
        {
            get
            {
                return parameterType;
            }
            set
            {
                SetPropertyValue("ParameterType", ref parameterType, value);
            }
        }

        int parameterIndex;
        public int ParameterIndex
        {
            get
            {
                return parameterIndex;
            }
            set
            {
                SetPropertyValue("ParameterIndex", ref parameterIndex, value);
            }
        }

        ReportCentral report;
        [Association("Reports-Parameters")]
        public ReportCentral Report
        {
            get
            {
                return report;
            }
            set
            {
                SetPropertyValue("Report", ref report, value);
            }
        }
    }
}