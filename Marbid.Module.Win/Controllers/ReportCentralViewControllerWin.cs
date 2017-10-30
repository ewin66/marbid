using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraVerticalGrid;
using Marbid.Module.BusinessObjects.ReportCentral;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Marbid.Module.CustomCodes;
using Marbid.Module.Win.CustomCodesWin;
using System.Collections.Generic;
using DevExpress.Xpo;
using Marbid.Module.BusinessObjects.Administration;

namespace Marbid.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ReportCentralViewControllerWin : ViewController
    {
        public ReportCentralViewControllerWin()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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

        private void BindToPivot_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ReportCentral rc = (ReportCentral)View.CurrentObject;
            
            PivotFormWin pivotForm = new PivotFormWin();
            pivotForm.QueryString = rc.QueryString;
            pivotForm.LayoutData = rc.PivotViewXML;
            pivotForm.DefaultLayoutData = rc.PivotViewXML;
            if (rc.CreatedBy.Oid.ToString() == SecuritySystem.CurrentUserId.ToString()) pivotForm.IsOwnwer = true;
            if (rc.ReportCentralLayoutData.Count > 0)
            {
                foreach (ReportCentralLayoutData layout in rc.ReportCentralLayoutData)
                {
                    if (layout.Owner.Oid.ToString() == SecuritySystem.CurrentUserId.ToString())
                    {
                        pivotForm.LayoutData = layout.PivotLayout;
                    }
                }
            }

            pivotForm.ConnectionString = rc.Connection.ConnectionString;
            pivotForm.ReportName = rc.Name;
            List<ParameterDefinition> list = new List<ParameterDefinition>();
            rc.Parameters.Sorting.Add(new SortProperty("ParameterIndex", DevExpress.Xpo.DB.SortingDirection.Ascending));
            if (rc.Parameters.Count > 0)
            {
                foreach (ReportParameter param in rc.Parameters)
                {
                    ParameterDefinition definition = new ParameterDefinition()
                    {
                        ParameterCaption = param.Caption,
                        ParameterName = param.Name,
                        ParameterPropertyType = param.ParameterType.Type,
                        ParameterDefaultValue = param.DefaultValue,
                        ParameterIndex = param.ParameterIndex
                    };

                    if (param.ParameterType.Type == BusinessObjects.ParameterPropertyType.DataSource)
                    {
                        definition.ParameterConnection = param.ParameterType.Connection.ConnectionString;
                        definition.ParameterQueryString = param.ParameterType.QueryString;
                    }
                    list.Add(definition);
                }
                pivotForm.ParameterDefinition = list;
            }
            pivotForm.Save += PivotForm_Save;
            pivotForm.SaveDefaultLayout += PivotForm_SaveDefaultLayout;
            pivotForm.ShowPivotForm();
        }

        private void PivotForm_SaveDefaultLayout(PivotFormWin m, SaveLayoutEventArgs e)
        {
            ReportCentral rc = (ReportCentral)View.CurrentObject;
            rc.PivotViewXML = e.LayoutXML;
            rc.Save();
            ObjectSpace.CommitChanges();
        }

        private void PivotForm_Save(PivotFormWin m, SaveLayoutEventArgs e)
        {
            ReportCentral rc = (ReportCentral)View.CurrentObject;
            ReportCentralLayoutData layoutData = this.ObjectSpace.CreateObject<ReportCentralLayoutData>();
            bool IsOkay = false;
            if (rc.ReportCentralLayoutData.Count > 0)
            {
                foreach (ReportCentralLayoutData layout in rc.ReportCentralLayoutData)
                {
                    if (layout.Owner.Oid.ToString() == SecuritySystem.CurrentUserId.ToString())
                    {
                        IsOkay = true;
                        layout.PivotLayout = e.LayoutXML;
                    }
                }
            }
            if (!IsOkay)
            {
                layoutData.Owner = ObjectSpace.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
                layoutData.PivotLayout = e.LayoutXML;
                rc.ReportCentralLayoutData.Add(layoutData);
            }
            ObjectSpace.CommitChanges();
        }

        private void BindToGrid_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ReportCentral rc = (ReportCentral)View.CurrentObject;
            GridForm gridForm = new GridForm();
            gridForm.LayoutData = rc.GridViewXML;
            gridForm.DefaultLayoutData = rc.GridViewXML;
            if (rc.ReportCentralLayoutData.Count > 0)
            {
                foreach (ReportCentralLayoutData layout in rc.ReportCentralLayoutData)
                {
                    if (layout.Owner.Oid.ToString() == SecuritySystem.CurrentUserId.ToString())
                    {
                        gridForm.LayoutData = layout.GridLayout;
                    }
                }
            }
            gridForm.ConnectionString = rc.Connection.ConnectionString;
            gridForm.QueryString = rc.QueryString;
            gridForm.ReportName = rc.Name;
            if (rc.CreatedBy.Oid.ToString() == SecuritySystem.CurrentUserId.ToString()) gridForm.IsOwner = true;
            List<ParameterDefinition> list = new List<ParameterDefinition>();
            rc.Parameters.Sorting.Add(new SortProperty("ParameterIndex", DevExpress.Xpo.DB.SortingDirection.Ascending));
            if (rc.Parameters.Count > 0)
            {
                foreach (ReportParameter param in rc.Parameters)
                {

                    ParameterDefinition definition = new ParameterDefinition()
                    {
                        ParameterCaption = param.Caption,
                        ParameterName = param.Name,
                        ParameterPropertyType = param.ParameterType.Type,
                        ParameterDefaultValue = param.DefaultValue,
                        ParameterIndex = param.ParameterIndex
                    };

                    if (param.ParameterType.Type == BusinessObjects.ParameterPropertyType.DataSource)
                    {
                        definition.ParameterConnection = param.ParameterType.Connection.ConnectionString;
                        definition.ParameterQueryString = param.ParameterType.QueryString;
                    }
                    list.Add(definition);
                }
                gridForm.ParameterDefinition = list;
            }
            gridForm.Save += GridForm_Save;
            gridForm.SaveDefaultLayout += GridForm_SaveDefaultLayout;
            gridForm.ShowGridForm();
        }

        private void GridForm_SaveDefaultLayout(GridForm m, SaveLayoutEventArgs e)
        {
            ReportCentral rc = (ReportCentral)View.CurrentObject;
            ReportCentralLayoutData layoutData = this.ObjectSpace.CreateObject<ReportCentralLayoutData>();
            rc.Save();
            ObjectSpace.CommitChanges();
        }

        private void GridForm_Save(GridForm m, SaveLayoutEventArgs e)
        {
            ReportCentral rc = (ReportCentral)View.CurrentObject;
            ReportCentralLayoutData layoutData = this.ObjectSpace.CreateObject<ReportCentralLayoutData>();
            bool IsOkay = false;
            if (rc.ReportCentralLayoutData.Count > 0)
            {
                foreach (ReportCentralLayoutData layout in rc.ReportCentralLayoutData)
                {
                    if (layout.Owner.Oid.ToString() == SecuritySystem.CurrentUserId.ToString())
                    {
                        IsOkay = true;
                        layout.GridLayout = e.LayoutXML;
                    }
                }
            }
            if (!IsOkay)
            {
                layoutData.Owner = ObjectSpace.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
                layoutData.GridLayout = e.LayoutXML;
                rc.ReportCentralLayoutData.Add(layoutData);
            }
            ObjectSpace.CommitChanges();
        }
    }
}