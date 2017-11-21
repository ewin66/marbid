using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraVerticalGrid;
using Marbid.Module.CustomCodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Marbid.Module.Win.CustomCodesWin
{
    public class PivotFormWin
    {
        GridControl drillGrid;
        DevExpress.XtraGrid.Views.Grid.GridView drillView;
        PivotGridField field;
        OptionsLayoutGrid opt;
        XtraForm pivotForm;
        PivotGridControl pivotGrid;

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

        public delegate void SaveLayout(PivotFormWin m, SaveLayoutEventArgs e);

        public event SaveLayout Save;
        public event SaveLayout SaveDefaultLayout;

        private void AddNewField_Click(object sender, EventArgs e)
        {
            pivotGrid.Fields.Add(new PivotGridField());
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

        private void DrillGrid_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            DevExpress.XtraGrid.Menu.GridViewMenu gridViewMenu = e.Menu;
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

        private void FieldPropertyMenuItem_Click(object sender, EventArgs e)
        {
            PropertyGridControl propertyGridControl = new PropertyGridControl();
            propertyGridControl.SelectedObject = field;
            propertyGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            XtraForm propertyForm = new XtraForm();
            propertyForm.Text = "Pivot Field Property: " + field.Caption;
            propertyForm.Owner = pivotForm;
            propertyForm.Width = 400;
            propertyForm.Height = 500;
            propertyForm.Controls.Add(propertyGridControl);
            propertyForm.Show();
        }

        private void PivotGrid_CellDoubleClick(object sender, PivotCellEventArgs e)
        {
            XtraForm form = new XtraForm();
            form.Icon = Marbid.Module.Win.Properties.Resources.mareinico;
            form.Height = 800;
            form.Width = 600;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Owner = pivotForm;
            form.Text = "Drill Down: " + ReportName;
            drillGrid = new GridControl();
            drillView = new DevExpress.XtraGrid.Views.Grid.GridView();
            drillGrid.DataSource = e.CreateDrillDownDataSource();
            drillGrid.ViewCollection.Add(drillView);
            drillGrid.ForceInitialize();
            drillGrid.Parent = form;
            drillGrid.Dock = DockStyle.Fill;
            DevExpress.XtraGrid.Views.Grid.GridView view = (DevExpress.XtraGrid.Views.Grid.GridView)drillGrid.MainView;
            view.PopulateColumns();
            foreach (GridColumn col in view.Columns)
            {
                if (col.GetType() == typeof(System.Decimal) || col.GetType() == typeof(System.Double))
                {
                    col.DisplayFormat.FormatType = FormatType.Numeric;
                    col.DisplayFormat.FormatString = "n2";
                }
            }
            view.PopupMenuShowing += DrillGrid_PopupMenuShowing;
            view.OptionsView.ColumnAutoWidth = false;
            view.BestFitColumns(true);
            form.ShowDialog();
            form.Dispose();
        }

        private void PivotGrid_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {

            DXMenuItem fieldPropertyMenuItem = new DXMenuItem("Field Property");
            DXMenuItem pivotSaveLayoutMenuItem = new DXMenuItem("Save Layout");
            DXMenuItem pivotSaveToXlsxMenuItem = new DXMenuItem("Save to Xlsx (WYSIWYG)");
            DXMenuItem pivotLayoutProperty = new DXMenuItem("Pivot Property");
            DXMenuItem saveDefaultLayoutMenuItem = new DXMenuItem("Save as Default Layout");
            DXMenuItem addNewField = new DXMenuItem("Add New Empty Field");
            DXMenuItem removeCurrentField = new DXMenuItem("Remove Field");
            DXMenuItem pivotSaveToXlsxDataAwareMenuItem = new DXMenuItem("Save to Xlsx (Data-Aware)");
            DXMenuItem pivotPrintPreviewMenuItem = new DXMenuItem("Print Preview");

            DXSubMenuItem subMenuItem = new DXSubMenuItem("Layout");
            DXSubMenuItem exportSubMenu = new DXSubMenuItem("Export && Print");
            e.Menu.Items.Add(subMenuItem);
            e.Menu.Items.Add(exportSubMenu);
            subMenuItem.Items.Add(addNewField);
            subMenuItem.Items.Add(pivotSaveLayoutMenuItem);
            subMenuItem.Items.Add(saveDefaultLayoutMenuItem);
            exportSubMenu.Items.Add(pivotSaveToXlsxMenuItem);
            exportSubMenu.Items.Add(pivotSaveToXlsxDataAwareMenuItem);
            exportSubMenu.Items.Add(pivotPrintPreviewMenuItem);

            if (e.MenuType == PivotGridMenuType.Header)
            {
                field = e.Field;

                fieldPropertyMenuItem.Click += FieldPropertyMenuItem_Click;
                removeCurrentField.Click += RemoveCurrentField_Click;
                subMenuItem.Items.Add(fieldPropertyMenuItem);
                subMenuItem.Items.Add(removeCurrentField);
            }
            pivotSaveLayoutMenuItem.Click += PivotSaveLayoutMenuItem_Click;
            pivotSaveToXlsxMenuItem.Click += PivotSaveToXlsxMenuItem_Click;
            pivotSaveToXlsxDataAwareMenuItem.Click += PivotSaveToXlsxDataAwareMenuItem_Click;
            pivotLayoutProperty.Click += PivotLayoutProperty_Click;
            pivotPrintPreviewMenuItem.Click += PivotPrintPreviewMenuItem_Click;
            saveDefaultLayoutMenuItem.Click += SaveDefaultLayoutMenuItem_Click;
            addNewField.Click += AddNewField_Click;

            if (!IsOwnwer)
            {
                pivotLayoutProperty.Visible = false;
                fieldPropertyMenuItem.Visible = false;
                saveDefaultLayoutMenuItem.Visible = false;
                removeCurrentField.Visible = false;
                addNewField.Visible = false;
            }
        }

        private void PivotPrintPreviewMenuItem_Click(object sender, EventArgs e)
        {
            pivotGrid.ShowRibbonPrintPreview();
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

        private void PivotSaveToXlsxDataAwareMenuItem_Click(object sender, EventArgs e)
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

        private void PivotSaveToXlsxMenuItem_Click(object sender, EventArgs e)
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
                pivotGrid.ExportToXlsx(dialog.FileName, options);
            }
        }

        private void RemoveCurrentField_Click(object sender, EventArgs e)
        {
            pivotGrid.Fields.Remove(field);
        }

        private void SaveDefaultLayoutMenuItem_Click(object sender, EventArgs e)
        {
            Stream str = new MemoryStream();
            pivotGrid.SaveLayoutToStream(str, opt);
            str.Position = 0;
            var sr = new StreamReader(str);
            var myStr = sr.ReadToEnd();
            LayoutData = myStr;
            Bitmap b = new Bitmap(pivotGrid.Width, pivotGrid.Height);
            pivotGrid.DrawToBitmap(b, new Rectangle(0, 0, b.Width, b.Height));
            SaveLayoutEventArgs args = new SaveLayoutEventArgs()
            {
                LayoutXML = myStr,
                ImageData = b
            };
            SaveDefaultLayout(this, args);
        }

        public void ShowPivotForm()
        {
            pivotGrid = new PivotGridControl();
            pivotGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            pivotForm = new XtraForm();
            pivotForm.Icon = Marbid.Module.Win.Properties.Resources.mareinico;
            pivotForm.StartPosition = FormStartPosition.CenterScreen;
            pivotForm.Text = "Pivot Grid: " + ReportName;
            pivotForm.Controls.Add(pivotGrid);
            DataRetrieval retrieval = new DataRetrieval();
            pivotForm.Height = 768;
            pivotForm.Width = 1024;
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
                SplashScreenManager.CloseForm(false);
            }
            pivotGrid.OptionsMenu.EnableFormatRulesMenu = true;
            pivotGrid.DataSource = dataSet.Tables["ReportData"];
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
            else
            {
                pivotGrid.RetrieveFields();
                foreach (PivotGridField field in pivotGrid.Fields)
                {
                    if (field.DataType == typeof(System.Double) || field.DataType == typeof(System.Decimal))
                    {
                        field.CellFormat.FormatType = FormatType.Numeric;
                        field.CellFormat.FormatString = "n2";
                        field.AllowedAreas = PivotGridAllowedAreas.DataArea;
                    } else
                    {
                        field.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count;
                    }
                }
            }
            pivotForm.Show();
        }

        public string ConnectionString { get; set; }
        public string DefaultLayoutData { get; set; }
        public bool IsOwnwer { get; set; }
        public string LayoutData { get; set; }
        public List<ParameterDefinition> ParameterDefinition { get; set; }
        public string QueryString { get; set; }
        public string ReportName { get; set; }
    }
}
