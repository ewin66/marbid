using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Marbid.Module.BusinessObjects.MiscObject;

namespace Marbid.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class MoTDViewController : ViewController
    {
        public MoTDViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewId = "PersonalDashboard";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            DashboardViewItem viewItem = ((DashboardView)View).FindItem("MessageOfTheDay") as DashboardViewItem;
            if (viewItem != null)
            {
                viewItem.ControlCreated += new EventHandler<EventArgs>(viewItem_ControlCreated);
            }
        }

        void viewItem_ControlCreated(object sender, EventArgs e)
        {
            DetailView detailView = (DetailView)((DashboardViewItem)sender).Frame.View;
            detailView.ViewEditMode = ViewEditMode.View;
            detailView.CurrentObject = detailView.ObjectSpace.FindObject<MessageOfTheDay>(CriteriaOperator.Parse("[IsPublished]=True"));
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
