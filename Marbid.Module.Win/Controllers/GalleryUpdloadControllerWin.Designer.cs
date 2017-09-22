namespace Marbid.Module.Win.Controllers
{
  partial class GalleryUpdloadControllerWin
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
      this.UploadFolderAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
      // 
      // UploadFolderAction
      // 
      this.UploadFolderAction.Caption = "Upload Folder";
      this.UploadFolderAction.ConfirmationMessage = null;
      this.UploadFolderAction.Id = "UploadFolderAction";
      this.UploadFolderAction.ToolTip = null;
      this.UploadFolderAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.UploadFolderAction_Execute);
      // 
      // GalleryUpdloadControllerWin
      // 
      this.Actions.Add(this.UploadFolderAction);

    }

    #endregion

    private DevExpress.ExpressApp.Actions.SimpleAction UploadFolderAction;
  }
}
