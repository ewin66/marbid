namespace Marbid.Module.Controllers
{
    partial class ResourceLibraryViewController
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
            this.RemoveCategory = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.AddCategory = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // RemoveCategory
            // 
            this.RemoveCategory.AcceptButtonCaption = null;
            this.RemoveCategory.CancelButtonCaption = null;
            this.RemoveCategory.Caption = "Remove Category";
            this.RemoveCategory.ConfirmationMessage = null;
            this.RemoveCategory.Id = "RemoveResourceCategory";
            this.RemoveCategory.ImageName = "BO_RemoveCategory";
            this.RemoveCategory.ToolTip = null;
            this.RemoveCategory.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.RemoveCategory_CustomizePopupWindowParams);
            this.RemoveCategory.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.RemoveCategory_Execute);
            // 
            // AddCategory
            // 
            this.AddCategory.AcceptButtonCaption = null;
            this.AddCategory.CancelButtonCaption = null;
            this.AddCategory.Caption = "Add Category";
            this.AddCategory.ConfirmationMessage = null;
            this.AddCategory.Id = "AddResourceCategory";
            this.AddCategory.ImageName = "BO_AddCategory";
            this.AddCategory.ToolTip = null;
            this.AddCategory.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.AddCategory_CustomizePopupWindowParams);
            this.AddCategory.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.AddCategory_Execute);
            // 
            // ResourceLibraryViewController
            // 
            this.Actions.Add(this.RemoveCategory);
            this.Actions.Add(this.AddCategory);
            this.TargetObjectType = typeof(Marbid.Module.BusinessObjects.Library.ResourceLibrary);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction RemoveCategory;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction AddCategory;
    }
}
