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
using Marbid.Module.BusinessObjects;
using DevExpress.Xpo;
using Marbid.Module.BusinessObjects.Administration;
namespace Marbid.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class FeedbackReplyViewController : ViewController
    {
        public FeedbackReplyViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(Feedback);
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

        private void replyFeedbackPopupWindowShowAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Feedback currentFeedback = (Feedback)View.CurrentObject;
            View.ObjectSpace.SetModified(currentFeedback);
            Employee currentUser = ObjectSpace.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);

            string currentDescription = currentFeedback.Description;
            currentDescription += string.Format("<BR><HR><BR>Reply From: {0}<BR>Reply Date: {1}<BR>Reply Content:<BR>{2}", currentUser.FullName, DateTime.Now, ((ReplyParametersObject)e.PopupWindow.View.CurrentObject).Reply);
            currentFeedback.Description = currentDescription;
            currentFeedback.LastDescription = ((ReplyParametersObject)e.PopupWindow.View.CurrentObject).Reply;
            currentFeedback.LastDescriptionEmployee = currentUser;
            currentFeedback.LastDescriptionDate = DateTime.Now;

            if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
            {
                View.ObjectSpace.CommitChanges();
            }

        }

        private void replyFeedbackPopupWindowShowAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace();
            e.View = Application.CreateDetailView(Application.CreateObjectSpace(), ReplyParametersObject.CreateReplyParametersObject());
            ((DetailView)e.View).ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
        }
    }
}
