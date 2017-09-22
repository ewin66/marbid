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
using DevExpress.ExpressApp.CloneObject;
using Marbid.Module.BusinessObjects.CRM;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace Marbid.Module.Controllers
{
   // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
   public partial class CloneScheduleViewController : ObjectViewController
   {
      public CloneScheduleViewController()
      {
         InitializeComponent();
         // Target required Views (via the TargetXXX properties) and create their Actions.
         TargetObjectType = typeof(Schedule);
      }
      protected override void OnActivated()
      {
         base.OnActivated();
         // Perform various tasks depending on the target View.
         var cloneObjectController = Frame.GetController<CloneObjectViewController>();
         cloneObjectController.CustomCloneObject += CloneObjectController_CustomCloneObject;
      }

      private void CloneObjectController_CustomCloneObject(object sender, CustomCloneObjectEventArgs e)
      {
         var cloner = new MyCloner();
         e.TargetObjectSpace = e.CreateDefaultTargetObjectSpace();
         object objectFromTargetObjectSpace = e.TargetObjectSpace.GetObject(e.SourceObject);
         e.ClonedObject = cloner.CloneTo(objectFromTargetObjectSpace, e.TargetType);

         if (e.ClonedObject.GetType() == typeof(Schedule))
         {
            Schedule clonedSchedule = (Schedule)e.ClonedObject;
            clonedSchedule.CreateDate = DateTime.Now;
            clonedSchedule.ModifyDate = DateTime.Now;
            clonedSchedule.IsCarAssigned = false;
            clonedSchedule.AssignedCar = null;
            clonedSchedule.AssignedDriver = null;
            clonedSchedule.CarAssignedBy = null;
            clonedSchedule.CarAssignedDate = DateTime.MinValue;
            clonedSchedule.CarAssignmentNote = null;
            if (clonedSchedule.MinutesOfMeetings.Count > 0)
            {
               foreach (MinutesOfMeeting mom in clonedSchedule.MinutesOfMeetings)
               {
                  mom.Delete();
               }
            }
         }
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
   }

   public class MyCloner : Cloner
   {
      public override void CopyMemberValue(XPMemberInfo memberInfo, IXPSimpleObject sourceObject, IXPSimpleObject targetObject)
      {
         if (!memberInfo.IsAssociation)
         {
            base.CopyMemberValue(memberInfo, sourceObject, targetObject);
         }
      }
   }
}
