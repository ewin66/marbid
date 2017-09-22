namespace Marbid.Module.Controllers
{
  partial class ReportCentralController
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
      this.DashboardEdit = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
      // 
      // DashboardEdit
      // 
      this.DashboardEdit.Caption = "Edit";
      this.DashboardEdit.Category = "Edit";
      this.DashboardEdit.ConfirmationMessage = null;
      this.DashboardEdit.Id = "EditDashboard";
      this.DashboardEdit.ImageName = "Action_Edit";
      this.DashboardEdit.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
      this.DashboardEdit.TargetObjectsCriteria = "[CreatedBy.Oid] = CurrentUserId()";
      this.DashboardEdit.TargetObjectType = typeof(Marbid.Module.BusinessObjects.ReportCentral.BIDashboard);
      this.DashboardEdit.ToolTip = null;
      this.DashboardEdit.TypeOfView = typeof(DevExpress.ExpressApp.View);
      this.DashboardEdit.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DashboardEdit_Execute);
      // 
      // ReportCentralController
      // 
      this.Actions.Add(this.DashboardEdit);

    }

    #endregion

    private DevExpress.ExpressApp.Actions.SimpleAction DashboardEdit;
  }
}
