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
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.SystemModule;
using Marbid.Module.BusinessObjects.ReportCentral.DataExplorer;

namespace Marbid.Module.BusinessObjects.ReportCentral
{
    [DefaultClassOptions]
    [NavigationItem("Reports and Statistics")]
    [Appearance("DisbledControls", TargetItems = "CreatedBy", Enabled = false)]
    [Appearance("VisibleControls", TargetItems = "Parameters,QueryString,Category,AllowedRoles,IsActive", Visibility = ViewItemVisibility.Hide, Criteria = "[CreatedBy.Oid] <> CurrentUserId()")]
    [ListViewFilter("Favorite", "[DataExplorerFavorites][[Employee.Oid] = CurrentUserId()]", "Favorite Only", "Show only my favorite data", 1, false)]
    [ListViewFilter("ShowAll", null, "All Data", "Show all Data", 0, true)]
    [ImageName("BO_Appearance")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ReportCentral : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ReportCentral(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
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

        bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                SetPropertyValue("IsActive", ref isActive, value);
            }
        }

        public string Categories
        {
            get
            {
                string categories = null;
                if (!IsLoading && !IsSaving && Category != null)
                {
                    if (Category.Count > 0)
                    {
                        foreach (ReportStatisticCategory category in Category)
                        {
                            categories += category.Title + ", ";
                        }
                        char[] MyChar = { ',', ' ' };
                        categories.TrimEnd(MyChar);
                        categories = categories.TrimEnd(MyChar);
                    }
                }
                return categories;
            }
        }

        Connection connection;
        [RuleRequiredField]
        public Connection Connection
        {
            get
            {
                return connection;
            }
            set
            {
                SetPropertyValue("Connection", ref connection, value);
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

        string queryString;
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        public string QueryString
        {
            get
            {
                return queryString;
            }
            set
            {
                SetPropertyValue("QueryString", ref queryString, value);
            }
        }

        string gridViewXML;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInDetailView(false)]
        public string GridViewXML
        {
            get
            {
                return gridViewXML;
            }
            set
            {
                SetPropertyValue("GridViewXML", ref gridViewXML, value);
            }
        }

        string pivotViewXML;
        [Size(SizeAttribute.Unlimited)]
        [VisibleInDetailView(false)]
        public string PivotViewXML
        {
            get
            {
                return pivotViewXML;
            }
            set
            {
                SetPropertyValue("PivotViewXML", ref pivotViewXML, value);
            }
        }

        MediaDataObject pivotLayout;
        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.DropDownPictureEdit, ListViewImageEditorMode = ImageEditorMode.DropDownPictureEdit)]
        public MediaDataObject PivotLayout
        {
            get
            {
                return pivotLayout;
            }
            set
            {
                SetPropertyValue("PivotLayout", ref pivotLayout, value);
            }
        }

        MediaDataObject gridLayout;
        [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.DropDownPictureEdit, ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit)]
        public MediaDataObject GridLayout
        {
            get
            {
                return gridLayout;
            }
            set
            {
                SetPropertyValue("GridLayout", ref gridLayout, value);
            }
        }

        [Association("Reports-Parameters"), DevExpress.Xpo.Aggregated]
        public XPCollection<ReportParameter> Parameters
        {
            get
            {
                return GetCollection<ReportParameter>("Parameters");
            }
        }

        [Association("ReportStatisticCategory-ReportCentral")]
        public XPCollection<ReportStatisticCategory> Category
        {
            get
            {
                return GetCollection<ReportStatisticCategory>("Category");
            }
        }

        [DevExpress.Xpo.AssociationAttribute("AllowedRoles-ReportCentral")]
        public XPCollection<Marbid.Module.BusinessObjects.Administration.MarbidRole> AllowedRoles
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.Administration.MarbidRole>("AllowedRoles");
            }
        }

        [VisibleInDetailView(false)]
        [Association("ReportCentral-DataExplorerFavorites")]
        public XPCollection<DataExplorerFavorite> DataExplorerFavorites
        {
            get
            {
                return GetCollection<DataExplorerFavorite>("DataExplorerFavorites");
            }
        }

        [Association("ReportCentral-ReportCentralLayoutData"), DevExpress.Xpo.Aggregated]
        [VisibleInDetailView(false)]
        public XPCollection<ReportCentralLayoutData> ReportCentralLayoutData
        {
            get
            {
                return GetCollection<ReportCentralLayoutData>("ReportCentralLayoutData");
            }
        }

        [Association("ReportCentral-PivotLayout"), DevExpress.Xpo.Aggregated]
        public XPCollection<ReportCentralPivotLayout> PivotLayoutData
        {
            get
            {
                return GetCollection<ReportCentralPivotLayout>("PivotLayoutData");
            }
        }

        [Association("ReportCentral-GridLayout"), DevExpress.Xpo.Aggregated]
        public XPCollection<ReportCentralGridLayout> GridLayoutData
        {
            get
            {
                return GetCollection<ReportCentralGridLayout>("GridLayoutData");
            }
        }

        [Action(ImageName = "BO_Not_Favorite", AutoCommit = true, Caption = "Remove From Favorite", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject, TargetObjectsCriteria = "[DataExplorerFavorites][[Employee.Oid] = CurrentUserId()]")]
        public void RemoveFromFavorite()
        {
            DataExplorerFavorite rFavorite = null;
            foreach (DataExplorerFavorite favorite in DataExplorerFavorites)
            {
                if (favorite.Employee == Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId))
                {
                    rFavorite = favorite;
                }
            }

            if (rFavorite != null)
            {
                DataExplorerFavorites.Remove(rFavorite);
                Session.Save(this);
            }
        }

        [Action(ImageName = "BO_Favorite", AutoCommit = true, Caption = "Set As Favorite", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject, TargetObjectsCriteria = "Not [DataExplorerFavorites][[Employee.Oid] = CurrentUserId()]")]
        public void SetAsFavorite()
        {
            DataExplorerFavorite favorite = new DataExplorerFavorite(this.Session);
            favorite.Employee = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            DataExplorerFavorites.Add(favorite);
            Session.Save(this);
        }

    }
}