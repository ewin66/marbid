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
using DevExpress.ExpressApp.ConditionalAppearance;

namespace Marbid.Module.BusinessObjects.ReportCentral.DataExplorer
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    [Appearance("Disabled", TargetItems = "Owner", Enabled = false)]
    [DefaultListViewOptions(true, NewItemRowPosition.Top)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ReportCentralPivotLayout : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ReportCentralPivotLayout(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            Owner = Session.GetLoadedObjectByKey<Employee>(SecuritySystem.CurrentUserId);
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

        ReportCentral dataExplorerItem;
        [Association("ReportCentral-PivotLayout")]
        public ReportCentral DataExplorerItem
        {
            get
            {
                return dataExplorerItem;
            }
            set
            {
                SetPropertyValue("DataExplorerItem", ref dataExplorerItem, value);
            }
        }

        Employee owner;
        public Employee Owner
        {
            get
            {
                return owner;
            }
            set
            {
                SetPropertyValue("Owner", ref owner, value);
            }
        }

        bool isPrivate;
        public bool IsPrivate
        {
            get
            {
                return isPrivate;
            }
            set
            {
                SetPropertyValue("IsPrivate", ref isPrivate, value);
            }
        }

        MediaDataObject layout;
        public MediaDataObject Layout
        {
            get
            {
                return layout;
            }
            set
            {
                SetPropertyValue("Layout", ref layout, value);
            }
        }

        string layoutData;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInDetailView(false)]
        public string LayoutData
        {
            get
            {
                return layoutData;
            }
            set
            {
                SetPropertyValue("LayoutData", ref layoutData, value);
            }
        }
    }
}