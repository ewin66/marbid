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
using Marbid.Module.BusinessObjects.MiscObject;
using Marbid.Module.BusinessObjects;
using Marbid.Module.BusinessObjects.Administration;
namespace Marbid.Module.Web.Controllers
{
  // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
  public partial class NavigationLogController : WindowController
  {
    public NavigationLogController()
    {
      InitializeComponent();
      // Target required Windows (via the TargetXXX properties) and create their Actions.
      TargetWindowType = WindowType.Main;
    }
    protected override void OnActivated()
    {
      base.OnActivated();
      // Perform various tasks depending on the target Window.
      //Frame.ViewChanged += new EventHandler<ViewChangedEventArgs>(OnView_Changed);
    }

    private void OnView_Changed(object sender, ViewChangedEventArgs e)
    {
      if (Frame != null && Frame.View != null)
      {
        if (!Frame.View.Id.Contains("DetailView"))
        {
          IObjectSpace os = Application.CreateObjectSpace();
          AccessLog acl = os.CreateObject<AccessLog>();
          acl.AccessDate = DateTime.Now;
          acl.Employee = os.GetObjectByKey<Employee>(os.GetKeyValue(SecuritySystem.CurrentUser));
          acl.ViewId = Frame.View.Id;
          acl.ActionDescription = Window.View.Caption;
          acl.Save();
          os.CommitChanges();
        }
      }
    }
    protected override void OnDeactivated()
    {
      // Unsubscribe from previously subscribed events and release other references and resources.
      base.OnDeactivated();
      if (Frame != null)
      {
        Frame.ViewChanged -= new EventHandler<ViewChangedEventArgs>(OnView_Changed);
      }
    }
  }
}
