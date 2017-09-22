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
using System.IO;
using Marbid.Module.CustomCodes;
using Marbid.Module.BusinessObjects.Administration;
using Marbid.Module.BusinessObjects.LibrarianSystem;

namespace Marbid.Module.Controllers
{
   // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
   public partial class DocumentViewController : ViewController
   {
      public DocumentViewController()
      {
         InitializeComponent();
         // Target required Views (via the TargetXXX properties) and create their Actions.
         TargetObjectType = typeof(Marbid.Module.BusinessObjects.LibrarianSystem.Document);
         TargetViewType = ViewType.DetailView;
      }
      protected override void OnActivated()
      {
         base.OnActivated();
         // Perform various tasks depending on the target View.
      }
      protected override void OnViewControlsCreated()
      {
         base.OnViewControlsCreated();
         // Access and customize the target View control.
      }
      protected override void OnDeactivated()
      {
         // Unsubscribe from previously subscribed events and release other references and resources.
         base.OnDeactivated();
      }

      private void ExtractTextFromPDFAction_Execute(object sender, SimpleActionExecuteEventArgs e)
      {
         Marbid.Module.BusinessObjects.LibrarianSystem.Document doc = (Marbid.Module.BusinessObjects.LibrarianSystem.Document)View.CurrentObject;
         if (doc.Attachment != null)
         {
            using (MemoryStream ms = new MemoryStream())
            {
               doc.Attachment.SaveToStream(ms);
               using (PdfHandling handling = new PdfHandling(ms))
               {
                  doc.Excerpt = handling.DocumentText;
               }
            }
         }

         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
         {
            View.ObjectSpace.CommitChanges();
         }
      }

      private void VerifyAction_Execute(object sender, SimpleActionExecuteEventArgs e)
      {
         Marbid.Module.BusinessObjects.LibrarianSystem.Document doc = (Marbid.Module.BusinessObjects.LibrarianSystem.Document)View.CurrentObject;
         doc.IsVerified = true;
         doc.VerifiedBy = View.ObjectSpace.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
         doc.VerifyDate = DateTime.Now;

         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
         {
            View.ObjectSpace.CommitChanges();
         }
      }

      private void DocumentRequestCheckOutAction_Execute_1(object sender, SimpleActionExecuteEventArgs e)
      {
         Checking checking = ObjectSpace.CreateObject<Checking>();
         checking.Document = (Document)View.CurrentObject;
         e.ShowViewParameters.CreatedView = Application.CreateDetailView(ObjectSpace, "Checkout_DetailView", false, checking);
         e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
         DialogController dialogController = Application.CreateController<DialogController>();
         dialogController.SaveOnAccept = false;
         e.ShowViewParameters.Controllers.Add(dialogController);
         dialogController.Accepting += DialogController_Accepting;
         ((DetailView)e.ShowViewParameters.CreatedView).ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
      }

      private void DialogController_Accepting(object sender, DialogControllerAcceptingEventArgs e)
      {
         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
         {
            View.ObjectSpace.CommitChanges();
         }
      }
   }
}
