using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraVerticalGrid;
using Marbid.Module.BusinessObjects;
using Marbid.Module.CustomCodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Marbid.Module.Win.CustomCodesWin
{
    public class SaveLayoutEventArgs : EventArgs
    {
        public string LayoutXML { get; set; }
    }

    public class GridForm
    {
        private GridControl gridControl;
        private GridView gridView;
        private XtraForm form;
        private OptionsLayoutGrid opt;
        private GridColumn column;
        private string layoutData;
        private string queryString;
        private string connectionString;
        private List<ParameterDefinition> parameterDefinition = new List<CustomCodesWin.ParameterDefinition>();

        public event SaveLayout Save;
        public event SaveLayout SaveDefaultLayout;

        public EventArgs e = null;

        public delegate void SaveLayout(GridForm m, SaveLayoutEventArgs e);

        public string ReportName { get; set; }
        public bool IsOwner { get; set; }
        public string DefaultLayoutData { get; set; }

        public string LayoutData
        {
            get
            {
                return layoutData;
            }
            set
            {
                layoutData = value;
            }
        }

        public List<ParameterDefinition> ParameterDefinition
        {
            get
            {
                return parameterDefinition;
            }
            set
            {
                parameterDefinition = value;
            }
        }

        public string QueryString
        {
            get
            {
                return queryString;
            }
            set
            {
                queryString = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        public GridForm()
        {
            opt = new OptionsLayoutGrid();
            opt.Columns.AddNewColumns = true;
            opt.Columns.RemoveOldColumns = true;
            opt.Columns.StoreAllOptions = true;
            opt.Columns.StoreAppearance = true;
            opt.Columns.StoreLayout = true;
            opt.StoreAllOptions = true;
            opt.StoreAppearance = true;
            opt.StoreFormatRules = true;
            opt.StoreVisualOptions = true;
            opt.StoreDataSettings = true;
            IsOwner = false;
        }

        public void ShowGridForm()
        {
            gridView = new GridView();
            gridControl = new GridControl();
            form = new XtraForm();
            form.Text = ReportName;
            gridControl.ViewCollection.Add(gridView);
            gridControl.Dock = DockStyle.Fill;
            DataSet dataSet = new DataSet();

            DataRetrieval retrieval = new DataRetrieval();
            List<ParameterDictionary> paramDictionary;
            if (parameterDefinition.Count > 0)
            {
                ParameterForm parameterForm = new ParameterForm();
                parameterForm.ParameterDefinition = parameterDefinition;
                if (parameterForm.ShowParameterDialog() == DialogResult.Cancel) return;

                paramDictionary = parameterForm.ParameterDictionary;
                retrieval.Parameters = paramDictionary;
            }

            retrieval.ConnectionString = connectionString;
            retrieval.QueryString = queryString;
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            try
            {
                dataSet = retrieval.GetDataSet("ReportData", dataSet);
            }
            finally
            {
                //Close Wait Form
                SplashScreenManager.CloseForm(false);
            }
            gridControl.DataSource = dataSet.Tables["ReportData"];
            gridView.PopulateColumns();
            gridControl.ForceInitialize();
            form.Controls.Add(gridControl);
            form.Shown += GridForm_Shown;
            GridView view = (GridView)gridControl.MainView;
            foreach (GridColumn col in view.Columns)
            {
                col.OptionsColumn.ReadOnly = true;
            }
            form.Height = 480;
            form.Width = 640;
            view.PopupMenuShowing += GridView_PopupMenuShowing;
            SplashScreenManager.CloseForm(false);
            form.Show();
        }

        protected void GridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            GridViewMenu gridViewMenu = e.Menu as GridViewMenu;
            if (gridViewMenu == null)
            {
                return;
            }


            DXMenuItem menuItem = new DXMenuItem("Column Properties");
            DXMenuItem gridViewPropertyMenuItem = new DXMenuItem("Grid View Property");
            DXMenuItem gridViewSaveLayoutMenuItem = new DXMenuItem("Save Layout");
            DXMenuItem gridViewSaveToXlsxItem = new DXMenuItem("Save to Xlsx (WYSIWYG)");
            DXMenuItem gridViewSaveToXlsxDataAwareItem = new DXMenuItem("Save to Xlsx (Data-Aware)");
            DXMenuItem saveDefaultLayoutMenuItem = new DXMenuItem("Save as Default Layout");
            DXSubMenuItem subMenuItem = new DXSubMenuItem("Layout");
            DXSubMenuItem exportSubMenu = new DXSubMenuItem("Export && Print");

            if (e.MenuType == GridMenuType.Column)
            {
                GridViewColumnMenu menu = e.Menu as GridViewColumnMenu;
                if (menu.Column != null)
                {

                    column = e.HitInfo.Column;
                    menuItem.Click += GridForm_GridViewColumnMenu_click;
                    subMenuItem.Items.Add(menuItem);
                }
            }

            subMenuItem.Items.Add(gridViewPropertyMenuItem);
            subMenuItem.Items.Add(gridViewSaveLayoutMenuItem);
            exportSubMenu.Items.Add(gridViewSaveToXlsxItem);
            exportSubMenu.Items.Add(gridViewSaveToXlsxDataAwareItem);
            subMenuItem.Items.Add(saveDefaultLayoutMenuItem);
            gridViewMenu.Items.Add(exportSubMenu);
            gridViewMenu.Items.Add(subMenuItem);
            gridViewPropertyMenuItem.Click += GridForm_GridViewPropertyMenuItem_Click;
            gridViewSaveLayoutMenuItem.Click += GridForm_GridViewSaveLayoutMenuItem_Click;
            gridViewSaveToXlsxItem.Click += GridForm_GridViewSaveToXlsxItem_Click;
            gridViewSaveToXlsxDataAwareItem.Click += GridForm_GridViewSaveToXlsxDataAwareItem_Click;
            saveDefaultLayoutMenuItem.Click += SaveDefaultLayoutMenuItem_Click;
            if (!IsOwner)
            {
                gridViewPropertyMenuItem.Visible = false;
                menuItem.Visible = false;
                saveDefaultLayoutMenuItem.Visible = false;
            }
        }

        private void SaveDefaultLayoutMenuItem_Click(object sender, EventArgs e)
        {
            Stream str = new MemoryStream();

            gridControl.MainView.SaveLayoutToStream(str, opt);
            str.Position = 0;
            var sr = new StreamReader(str);
            var myStr = sr.ReadToEnd();
            layoutData = myStr;

            SaveLayoutEventArgs args = new SaveLayoutEventArgs()
            {
                LayoutXML = myStr
            };
            SaveDefaultLayout(this, args);
        }

        private void GridForm_GridViewSaveToXlsxDataAwareItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel File (*.xlsx)|*.Xlsx|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.DefaultExt = "Xlsx";
            dialog.AddExtension = true;
            DevExpress.XtraPrinting.XlsxExportOptionsEx options = new DevExpress.XtraPrinting.XlsxExportOptionsEx();
            options.ExportType = DevExpress.Export.ExportType.DataAware;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                gridControl.MainView.ExportToXlsx(dialog.FileName, options);
            }
        }

        private void GridForm_GridViewSaveToXlsxItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel File (*.xlsx)|*.Xlsx|All files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.DefaultExt = "Xlsx";
            dialog.AddExtension = true;
            DevExpress.XtraPrinting.XlsxExportOptionsEx options = new DevExpress.XtraPrinting.XlsxExportOptionsEx();
            options.ExportType = DevExpress.Export.ExportType.WYSIWYG;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                gridControl.MainView.ExportToXlsx(dialog.FileName, options);
            }
        }

        private void GridForm_GridViewSaveLayoutMenuItem_Click(object sender, EventArgs e)
        {
            Stream str = new MemoryStream();

            gridControl.MainView.SaveLayoutToStream(str, opt);
            str.Position = 0;
            var sr = new StreamReader(str);
            var myStr = sr.ReadToEnd();
            layoutData = myStr;

            SaveLayoutEventArgs args = new SaveLayoutEventArgs()
            {
                LayoutXML = myStr
            };
            Save(this, args);
        }

        private void GridForm_GridViewPropertyMenuItem_Click(object sender, EventArgs e)
        {
            PropertyGridControl propertyGridControl = new PropertyGridControl();
            GridView view = (GridView)gridControl.MainView;
            propertyGridControl.SelectedObject = view;
            propertyGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            XtraForm propertyForm = new XtraForm();
            propertyForm.Text = "Grid Property: " + form.Text;
            propertyForm.Owner = form;
            propertyForm.Controls.Add(propertyGridControl);
            propertyForm.Width = 300;
            propertyForm.Height = 500;
            propertyForm.Show();
        }

        private void GridForm_GridViewColumnMenu_click(object sender, EventArgs e)
        {
            PropertyGridControl propertyGridControl = new PropertyGridControl();
            propertyGridControl.SelectedObject = column;
            propertyGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            XtraForm propertyForm = new XtraForm();
            propertyForm.Owner = form;
            propertyForm.Text = "Grid Column Property: " + column.Caption;
            propertyForm.Controls.Add(propertyGridControl);
            propertyForm.Width = 300;
            propertyForm.Height = 500;
            propertyForm.Show();
        }

        private void GridForm_Shown(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(layoutData))
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(layoutData);
                writer.Flush();
                stream.Position = 0;
                GridView view = (GridView)gridControl.MainView;
                view.RestoreLayoutFromStream(stream, opt);
            }
        }
    }


}