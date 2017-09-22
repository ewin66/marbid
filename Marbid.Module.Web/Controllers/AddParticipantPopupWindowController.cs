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
using DevExpress.ExpressApp.Web;
using System.Web.UI.WebControls;

namespace Marbid.Module.Web.Controllers
{
   // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
   public partial class AddParticipantPopupWindowController : WindowController
   {
      public AddParticipantPopupWindowController()
      {
         InitializeComponent();
         // Target required Windows (via the TargetXXX properties) and create their Actions.
         TargetWindowType = WindowType.Child;
      }
      protected override void OnActivated()
      {
         base.OnActivated();
         // Perform various tasks depending on the target Window.
            if (Application.MainWindow != null && Application.MainWindow.View != null && Application.MainWindow.View.ObjectTypeInfo != null && Application.MainWindow.View.ObjectTypeInfo.Name == "Schedule")
               ((WebApplication)Application).PopupWindowManager.PopupShowing += PopupWindowManager_PopupShowing;
      }

      private void PopupWindowManager_PopupShowing(object sender, PopupShowingEventArgs e)
      {

         e.PopupControl.CustomizePopupWindowSize += PopupControl_CustomizePopupWindowSize;
      }

      private void PopupControl_CustomizePopupWindowSize(object sender, DevExpress.ExpressApp.Web.Controls.CustomizePopupWindowSizeEventArgs e)
      {
         if (Window.View.Id == "Employee_LookupListView" || Window.View.Id == "Contact_LookupListView" || Window.View.Id == "TemporaryScheduleParticipant_LookupListView")
         {
            e.ShowPopupMode = DevExpress.ExpressApp.Web.Controls.ShowPopupMode.Centered;
            e.PopupTemplateType = DevExpress.ExpressApp.Web.Controls.PopupTemplateType.FindDialog;
            e.Height = 500;
            e.Width = 700;
            e.Handled = true;
         }
      }

      protected override void OnDeactivated()
      {
         // Unsubscribe from previously subscribed events and release other references and resources.
         base.OnDeactivated();
         ((WebApplication)Application).PopupWindowManager.PopupShowing -= PopupWindowManager_PopupShowing;
      }
   }
}
