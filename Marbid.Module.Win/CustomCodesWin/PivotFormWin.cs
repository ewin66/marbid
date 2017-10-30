using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraVerticalGrid;
using Marbid.Module.CustomCodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marbid.Module.Win.CustomCodesWin
{
    public class PivotFormWin
    {
        PivotGridControl pivotGrid;
        XtraForm pivotForm;
        PivotGridField field;
        OptionsLayoutGrid opt;
        GridControl drillGrid;
        DevExpress.XtraGrid.Views.Grid.GridView drillView;
        public event SaveLayout Save;
        public event SaveLayout SaveDefaultLayout;
        //public EventArgs e = null;
        public delegate void SaveLayout(PivotFormWin m, SaveLayoutEventArgs e);
        public PivotFormWin()
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
            IsOwnwer = false;
            ParameterDefinition = new List<ParameterDefinition>();
        }
        public string ReportName { get; set; }
        public List<ParameterDefinition> ParameterDefinition { get; set; }
        public string QueryString { get; set; }
        public string ConnectionString { get; set; }
        public string LayoutData { get; set; }
        public string DefaultLayoutData { get; set; }
        public bool IsOwnwer { get; set; }

        public void ShowPivotForm()
        {
            pivotGrid = new PivotGridControl();
            pivotGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            pivotForm = new XtraForm();
            pivotForm.Text = ReportName;
            pivotForm.Controls.Add(pivotGrid);
            DataRetrieval retrieval = new DataRetrieval();
            pivotForm.Height = 480;
            pivotForm.Width = 640;
            List<ParameterDictionary> paramDictionary;
            if (ParameterDefinition.Count > 0)
            {
                ParameterForm parameterForm = new ParameterForm();
                parameterForm.ParameterDefinition = ParameterDefinition;
                if (parameterForm.ShowParameterDialog() == DialogResult.Cancel) return;

                paramDictionary = parameterForm.ParameterDictionary;
                retrieval.Parameters = paramDictionary;
            }
            retrieval.ConnectionString = ConnectionString;
            retrieval.QueryString = QueryString;
            DataSet dataSet = new DataSet();
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
            pivotGrid.DataSource = dataSet.Tables["ReportData"];
            pivotGrid.RetrieveFields();
            pivotGrid.PopupMenuShowing += PivotGrid_PopupMenuShowing;
            pivotGrid.CellDoubleClick += PivotGrid_CellDoubleClick;

            if (!String.IsNullOrWhiteSpace(LayoutData))
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(LayoutData);
                writer.Flush();
                stream.Position = 0;
                pivotGrid.RestoreLayoutFromStream(stream, opt);
            }
            pivotForm.Show();
        }

        private void PivotGrid_CellDoubleClick(object sender, PivotCellEventArgs e)
        {
            // Create a new form.
            XtraForm form = new XtraForm();
            //form.Text = "Records Drill-Down: " + e.ColumnField.Caption;
            form.Height = 480;
            form.Width = 640;
            // Place a DataGrid control on the form.
            form.Owner = pivotForm;

            drillGrid = new GridControl();
            drillView = new DevExpress.XtraGrid.Views.Grid.GridView();
            drillGrid.DataSource = e.CreateDrillDownDataSource();
            drillGrid.ViewCollection.Add(drillView);
            drillGrid.ForceInitialize();
            drillGrid.Parent = form;
            drillGrid.Dock = DockStyle.Fill;
            // Get the recrd set associated with the current cell and bind it to the grid.
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)drillGrid.MainView;
            view.PopulateColumns();
            view.PopupMenuShowing += DrillGrid_PopupMenuShowing;
            // Display the form.
            form.ShowDialog();
            form.Dispose();
        }

        private void DrillGrid_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            DevExpress.XtraGrid.Menu.GridViewMenu gridViewMenu = e.Menu as DevExpress.XtraGrid.Menu.GridViewMenu;
            if (gridViewMenu == null)
            {
                return;
            }
            DXMenuItem gridViewSaveToXlsxItem = new DXMenuItem("Save to Xlsx (WYSIWYG)");
            DXMenuItem gridViewSaveToXlsxDataAwareItem = new DXMenuItem("Save to Xlsx (Data-Aware)");
            gridViewMenu.Items.Add(gridViewSaveToXlsxItem);
            gridViewMenu.Items.Add(gridViewSaveToXlsxDataAwareItem);
            gridViewSaveToXlsxItem.Click += DrillGrid_GridViewSaveToXlsxItem_Click;
            gridViewSaveToXlsxDataAwareItem.Click += DrillGrid_GridViewSaveToXlsxDataAwareItem_Click;
        }

        private void DrillGrid_GridViewSaveToXlsxDataAwareItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel File (*.xlsx)|*.Xlsx|All files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.DefaultExt = "Xlsx";
            dialog.AddExtension = true;
            DevExpress.XtraPrinting.XlsxExportOptionsEx options = new DevExpress.XtraPrinting.XlsxExportOptionsEx();
            options.ExportType = DevExpress.Export.ExportType.DataAware;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                drillGrid.MainView.ExportToXlsx(dialog.FileName, options);
            }
        }

        private void DrillGrid_GridViewSaveToXlsxItem_Click(object sender, EventArgs e)
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
                drillGrid.MainView.ExportToXlsx(dialog.FileName, options);
            }
        }

        private void PivotGrid_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {

            DXMenuItem fieldPropertyMenuItem = new DXMenuItem("Field Property");
            DXMenuItem pivotSaveLayoutMenuItem = new DXMenuItem("Save Layout");
            DXMenuItem pivotSaveToXlsxMenuItem = new DXMenuItem("Save to Xlsx");
            DXMenuItem pivotLayoutProperty = new DXMenuItem("Pivot Property");
            DXMenuItem saveDefaultLayoutMenuItem = new DXMenuItem("Save as Default Layout");
            DXSubMenuItem subMenuItem = new DXSubMenuItem("Layout");
            DXSubMenuItem exportSubMenu = new DXSubMenuItem("Export && Print");
            e.Menu.Items.Add(subMenuItem);
            e.Menu.Items.Add(exportSubMenu);
            subMenuItem.Items.Add(pivotSaveLayoutMenuItem);
            exportSubMenu.Items.Add(pivotSaveToXlsxMenuItem);
            subMenuItem.Items.Add(pivotLayoutProperty);
            if (e.MenuType == PivotGridMenuType.Header)
            {
                field = e.Field;

                fieldPropertyMenuItem.Click += FieldPropertyMenuItem_Click;
                e.Menu.Items.Add(fieldPropertyMenuItem);
                subMenuItem.Items.Add(fieldPropertyMenuItem);
            }
            //e.Menu.Items.Add(pivotSaveLayoutMenuItem);
            //e.Menu.Items.Add(pivotSaveToXlsxMenuItem);
            //e.Menu.Items.Add(pivotLayoutProperty);
            pivotSaveLayoutMenuItem.Click += PivotSaveLayoutMenuItem_Click;
            pivotSaveToXlsxMenuItem.Click += PivotSaveToXlsxMenuItem_Click;
            pivotLayoutProperty.Click += PivotLayoutProperty_Click;
            saveDefaultLayoutMenuItem.Click += SaveDefaultLayoutMenuItem_Click;

            if (!IsOwnwer)
            {
                pivotLayoutProperty.Visible = false;
                fieldPropertyMenuItem.Visible = false;
                saveDefaultLayoutMenuItem.Visible = false;
            }
        }

        private void SaveDefaultLayoutMenuItem_Click(object sender, EventArgs e)
        {
            Stream str = new MemoryStream();
            pivotGrid.SaveLayoutToStream(str, opt);
            str.Position = 0;
            var sr = new StreamReader(str);
            var myStr = sr.ReadToEnd();
            LayoutData = myStr;

            SaveLayoutEventArgs args = new SaveLayoutEventArgs()
            {
                LayoutXML = myStr
            };
            SaveDefaultLayout(this, args);
        }

        private void PivotLayoutProperty_Click(object sender, EventArgs e)
        {
            PropertyGridControl propertyGridControl = new PropertyGridControl();
            propertyGridControl.SelectedObject = pivotGrid;
            propertyGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            XtraForm propertyForm = new XtraForm();
            propertyForm.Text = "Pivot Property: " + pivotForm.Text;
            propertyForm.Controls.Add(propertyGridControl);
            propertyForm.Owner = pivotForm;
            propertyForm.Width = 300;
            propertyForm.Height = 500;
            propertyForm.Show();
        }

        private void PivotSaveToXlsxMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel File (*.xlsx)|*.Xlsx|All files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.DefaultExt = "Xlsx";
            dialog.AddExtension = true;
            DevExpress.XtraPrinting.XlsxExportOptionsEx options = new DevExpress.XtraPrinting.XlsxExportOptionsEx();
            options.ExportType = DevExpress.Export.ExportType.DataAware;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pivotGrid.ExportToXlsx(dialog.FileName, options);
            }
        }

        private void PivotSaveLayoutMenuItem_Click(object sender, EventArgs e)
        {
            Stream str = new MemoryStream();
            pivotGrid.SaveLayoutToStream(str, opt);
            str.Position = 0;
            var sr = new StreamReader(str);
            var myStr = sr.ReadToEnd();
            LayoutData = myStr;

            SaveLayoutEventArgs args = new SaveLayoutEventArgs()
            {
                LayoutXML = myStr
            };
            Save(this, args);
        }

        private void FieldPropertyMenuItem_Click(object sender, EventArgs e)
        {
            PropertyGridControl propertyGridControl = new PropertyGridControl();
            propertyGridControl.SelectedObject = field;
            propertyGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            XtraForm propertyForm = new XtraForm();
            propertyForm.Text = "Pivot Field Property: " + field.Caption;
            propertyForm.Owner = pivotForm;
            propertyForm.Width = 300;
            propertyForm.Height = 500;
            propertyForm.Controls.Add(propertyGridControl);
            propertyForm.Show();
        }
    }
}
