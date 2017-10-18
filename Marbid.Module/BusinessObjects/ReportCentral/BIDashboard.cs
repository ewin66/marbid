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
using DevExpress.ExpressApp.SystemModule;

namespace Marbid.Module.BusinessObjects.ReportCentral
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [NavigationItem("Reports and Statistics")]
    [Appearance("DashboardDisabled", TargetItems = "CreatedBy,ModifiedBy,CreateDate,ModifyDate", Enabled = false)]
    [Appearance("DashboardEnabled", TargetItems = "Description", Enabled = true)]
    [Appearance("DashboardEditHide", TargetItems = "Modify,Edit", Visibility = ViewItemVisibility.Hide, AppearanceItemType = "Action")]
    [ListViewFilter("Favorite", "[BIDashboardFavorites][[Employee.Oid] = CurrentUserId()]", "Favorite Only", "Show only my favorite dashboards", 1, false)]
    [ListViewFilter("ShowAll", null, "All Dashboards", "Show all ashboards", 0, true)]
    [DefaultProperty("Title")]
    public class BIDashboard : DashboardData
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public BIDashboard(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            CreateDate = DateTime.Now;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            ModifiedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            ModifyDate = DateTime.Now;
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
        DateTime createDate;
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
        [VisibleInLookupListView(false)]
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
        [VisibleInLookupListView(false)]
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
        Boolean isActive;
        public Boolean IsActive
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
        [Association("ReportStatisticCategory-BIDashboard")]
        public XPCollection<ReportStatisticCategory> Category
        {
            get
            {
                return GetCollection<ReportStatisticCategory>("Category");
            }
        }
        [DevExpress.Xpo.AssociationAttribute("AllowedRoles-BIDashboard")]
        public XPCollection<Marbid.Module.BusinessObjects.Administration.MarbidRole> AllowedRoles
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.Administration.MarbidRole>("AllowedRoles");
            }
        }

        
        [VisibleInDetailView(false)]
        [Association("BIDashboard-BIDashboardFavorites")]
        public XPCollection<BIDashboardFavorite> BIDashboardFavorites
        {
            get
            {
                return GetCollection<BIDashboardFavorite>("BIDashboardFavorites");
            }
        }

        [Action(ImageName = "BO_Not_Favorite", AutoCommit = true, Caption = "Remove From Favorite", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject, TargetObjectsCriteria = "[BIDashboardFavorites][[Employee.Oid] = CurrentUserId()]")]
        public void RemoveFromFavorite()
        {
            BIDashboardFavorite rFavorite = null;
            foreach(BIDashboardFavorite favorite in BIDashboardFavorites)
            {
                if (favorite.Employee == Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId))
                {
                    rFavorite = favorite;
                }
            }

            if (rFavorite != null)
            {
                BIDashboardFavorites.Remove(rFavorite);
                Session.Save(this);
            }
        }

        [Action(ImageName = "BO_Favorite", AutoCommit = true, Caption = "Set As Favorite", SelectionDependencyType = MethodActionSelectionDependencyType.RequireSingleObject, TargetObjectsCriteria = "Not [BIDashboardFavorites][[Employee.Oid] = CurrentUserId()]")]
        public void SetAsFavorite()
        {
            BIDashboardFavorite favorite = new BIDashboardFavorite(this.Session);
            favorite.Employee = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            BIDashboardFavorites.Add(favorite);
            Session.Save(this);
        }

    }
}