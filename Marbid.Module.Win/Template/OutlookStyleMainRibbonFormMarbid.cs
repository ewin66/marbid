using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win.Controls;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.ExpressApp.Win.Templates;
using DevExpress.ExpressApp.Win.Templates.Utils;
using DevExpress.Utils.Animation;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;

namespace Marbid.Module.Win.Template
{
  public partial class OutlookStyleMainRibbonFormMarbid : RibbonForm, IActionControlsSite, IContextMenuHolder, IWindowTemplate, IDockManagerHolder, IBarManagerHolder, ISupportViewChanged, IXafDocumentsHostWindow, ISupportUpdate, IViewSiteTemplate, ISupportStoreSettings, IViewHolder, IOfficeNavigationBarHolder, ITransitionManagerHolder
  {
    private static readonly object viewChanged = new object();
    private static readonly object settingsReloaded = new object();
    private StatusMessagesHelper statusMessagesHelper;

    protected virtual void InitializeImages()
    {
      barSubItemPanels.Glyph = ImageLoader.Instance.GetImageInfo("Action_Navigation").Image;
      barSubItemPanels.LargeGlyph = ImageLoader.Instance.GetLargeImageInfo("Action_Navigation").Image;
      barSubItemNavigationPane.Glyph = ImageLoader.Instance.GetImageInfo("NavigationOptions").Image;
      barSubItemNavigationPane.LargeGlyph = ImageLoader.Instance.GetLargeImageInfo("NavigationOptions").Image;
      barCheckItemNormal.Glyph = ImageLoader.Instance.GetImageInfo("OutlookNavigation_Normal").Image;
      barCheckItemReading.Glyph = ImageLoader.Instance.GetImageInfo("OutlookNavigation_Reading").Image;
    }

    protected virtual void RaiseViewChanged(DevExpress.ExpressApp.View view)
    {
      EventHandler<TemplateViewChangedEventArgs> handler = (EventHandler<TemplateViewChangedEventArgs>)Events[viewChanged];
      if (handler != null)
      {
        handler(this, new TemplateViewChangedEventArgs(view));
      }
    }
    protected virtual void RaiseSettingsReloaded()
    {
      EventHandler handler = (EventHandler)Events[settingsReloaded];
      if (handler != null)
      {
        handler(this, EventArgs.Empty);
      }
    }

    protected override FormShowMode ShowMode
    {
      get { return FormShowMode.AfterInitialization; }
    }

    public OutlookStyleMainRibbonFormMarbid()
    {
      InitializeComponent();
      InitializeImages();
      ribbonControl.Manager.ForceLinkCreate();
      statusMessagesHelper = new StatusMessagesHelper(barContainerStatusMessages);
      new ReadingModeSwitcher(barCheckItemNormal, barCheckItemReading, navBarControl, officeNavigationBar);
      new NavBarControlVisibilityHelper(navBarControl, barSubItemNavigationPane);
    }

    #region IActionControlsSite Members
    IEnumerable<IActionControlContainer> IActionControlsSite.ActionContainers
    {
      get { return ribbonControl.ActionContainers; }
    }
    IEnumerable<IActionControl> IActionControlsSite.ActionControls
    {
      get
      {
        List<IActionControl> actionControls = new List<IActionControl>(ribbonControl.ActionControls);
        actionControls.Add(navBarControl.ActionControl);
        return actionControls;
      }
    }
    IActionControlContainer IActionControlsSite.DefaultContainer
    {
      get { return barActionContainerDefault; }
    }
    #endregion

    #region IFrameTemplate Members
    void IFrameTemplate.SetView(DevExpress.ExpressApp.View view)
    {
      viewSiteManager.SetView(view);
      RaiseViewChanged(view);
    }
    ICollection<IActionContainer> IFrameTemplate.GetContainers()
    {
      return new IActionContainer[] { };
    }
    IActionContainer IFrameTemplate.DefaultContainer
    {
      get { return null; }
    }
    #endregion

    #region IWindowTemplate Members
    void IWindowTemplate.SetCaption(string caption)
    {
      ribbonControl.ApplicationCaption = " ";
      ribbonControl.ApplicationDocumentCaption = caption;
    }
    void IWindowTemplate.SetStatus(ICollection<string> statusMessages)
    {
      statusMessagesHelper.SetMessages(statusMessages);
    }
    bool IWindowTemplate.IsSizeable
    {
      get { return FormBorderStyle == FormBorderStyle.Sizable; }
      set { FormBorderStyle = (value ? FormBorderStyle.Sizable : FormBorderStyle.FixedDialog); }
    }
    #endregion

    #region IBarManagerHolder Members
    BarManager IBarManagerHolder.BarManager
    {
      get { return ribbonControl.Manager; }
    }
    event EventHandler IBarManagerHolder.BarManagerChanged
    {
      add { }
      remove { }
    }
    #endregion

    #region IDockManagerHolder Members
    DevExpress.XtraBars.Docking.DockManager IDockManagerHolder.DockManager
    {
      get { return dockManager; }
    }
    #endregion

    #region IContextMenuHolder
    PopupMenu IContextMenuHolder.ContextMenu
    {
      get { return contextMenu; }
    }
    #endregion

    #region ISupportViewChanged Members
    event EventHandler<TemplateViewChangedEventArgs> ISupportViewChanged.ViewChanged
    {
      add { Events.AddHandler(viewChanged, value); }
      remove { Events.RemoveHandler(viewChanged, value); }
    }
    #endregion

    #region IDocumentsHostWindow Members
    bool IDocumentsHostWindow.DestroyOnRemovingChildren
    {
      get { return true; }
    }
    DocumentManager IDocumentsHostWindow.DocumentManager
    {
      get { return documentManager; }
    }
    #endregion

    #region IXafDocumentsHostWindow Members
    UIType IXafDocumentsHostWindow.UIType { get; set; }
    #endregion

    #region ISupportUpdate Members
    void ISupportUpdate.BeginUpdate()
    {
      if (ribbonControl.Manager != null)
      {
        ribbonControl.Manager.BeginUpdate();
      }
    }
    void ISupportUpdate.EndUpdate()
    {
      if (ribbonControl.Manager != null)
      {
        ribbonControl.Manager.EndUpdate();
      }
    }
    #endregion

    #region IViewSiteTemplate Members
    object IViewSiteTemplate.ViewSiteControl
    {
      get { return viewSiteManager.ViewSiteControl; }
    }
    #endregion

    #region ISupportStoreSettings Members
    void ISupportStoreSettings.SetSettings(IModelTemplate settings)
    {
      IModelTemplateWin templateModel = (IModelTemplateWin)settings;
      TemplatesHelper templatesHelper = new TemplatesHelper(templateModel);
      formStateModelSynchronizer.Model = templatesHelper.GetFormStateNode();
      navBarControlModelSynchronizer.Model = templatesHelper.GetNavBarCustomizationNode();
      officeNavigationBarCustomizationModelSynchronizer.Model = templatesHelper.GetOfficeNavigationBarCustomizationNode();
      templatesHelper.SetRibbonSettings(ribbonControl);
    }
    void ISupportStoreSettings.ReloadSettings()
    {
      modelSynchronizationManager.ApplyModel();
      RaiseSettingsReloaded();
    }
    void ISupportStoreSettings.SaveSettings()
    {
      SuspendLayout();
      try
      {
        modelSynchronizationManager.SynchronizeModel();
      }
      finally
      {
        ResumeLayout();
      }
    }
    event EventHandler ISupportStoreSettings.SettingsReloaded
    {
      add { Events.AddHandler(settingsReloaded, value); }
      remove { Events.RemoveHandler(settingsReloaded, value); }
    }
    #endregion

    #region IViewHolder Members
    DevExpress.ExpressApp.View IViewHolder.View
    {
      get { return viewSiteManager.View; }
    }
    #endregion

    #region IOfficeNavigationBarHolder Members
    OfficeNavigationBar IOfficeNavigationBarHolder.OfficeNavigationBar
    {
      get { return officeNavigationBar; }
    }
    #endregion

    #region ITransitionManagerHolder Members
    TransitionManager ITransitionManagerHolder.TransitionManager
    {
      get { return transitionManager; }
    }
    Control ITransitionManagerHolder.TransitionControl
    {
      get { return mainContainer; }
    }
    #endregion
  }
}
