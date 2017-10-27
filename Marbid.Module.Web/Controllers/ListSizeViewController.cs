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
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp.Web.Editors.ASPx;

namespace Marbid.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ListSizeViewController : ViewController<ListView>
    {
        //private ListViewController controller;
        public ListSizeViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            if (this.View is ListView)
            {
                ListView lv = (ListView)this.View;
                ((IModelListViewWeb)lv.Model).PageSize = 10;
            }

            ASPxGridListEditor listEditor = View.Editor as ASPxGridListEditor;
            if (listEditor != null)
            {
                listEditor.IsAdaptive = true;
            }

            //controller = Frame.GetController<ListViewController>();
            //if (controller != null)
            //{
            //    controller.EditAction.Active["ViewController1"] = false;
            //}


            //XafApplication app = this.Application;
            //((IModelListViewWeb)app.FindModelView("Contact_ListView")).PageSize = 50;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            //if (controller != null)
            //{
            //    controller.EditAction.Active.RemoveItem("ViewController1");
            //}
        }
    }
}
