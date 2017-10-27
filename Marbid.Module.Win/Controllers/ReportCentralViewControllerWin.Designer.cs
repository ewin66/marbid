namespace Marbid.Module.Win.Controllers
{
    partial class ReportCentralViewControllerWin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BindToPivot = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.BindToGrid = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // BindToPivot
            // 
            this.BindToPivot.Caption = "Bind To Pivot";
            this.BindToPivot.Category = "View";
            this.BindToPivot.ConfirmationMessage = null;
            this.BindToPivot.Id = "BindToPivot";
            this.BindToPivot.ImageName = "BO_PivotChart";
            this.BindToPivot.ToolTip = null;
            this.BindToPivot.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.BindToPivot_Execute);
            // 
            // BindToGrid
            // 
            this.BindToGrid.Caption = "Bind To Grid";
            this.BindToGrid.Category = "View";
            this.BindToGrid.ConfirmationMessage = null;
            this.BindToGrid.Id = "BindToGrid";
            this.BindToGrid.ImageName = "BO_Appointment";
            this.BindToGrid.ToolTip = null;
            this.BindToGrid.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.BindToGrid_Execute);
            // 
            // ReportCentralViewControllerWin
            // 
            this.Actions.Add(this.BindToPivot);
            this.Actions.Add(this.BindToGrid);
            this.TargetObjectType = typeof(Marbid.Module.BusinessObjects.ReportCentral.ReportCentral);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction BindToPivot;
        private DevExpress.ExpressApp.Actions.SimpleAction BindToGrid;
    }
}
