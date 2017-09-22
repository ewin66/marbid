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
using Marbid.Module.BusinessObjects.CRM;
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;
using System.Collections;
using Marbid.Module.BusinessObjects.Administration;
using DevExpress.ExpressApp.Xpo;

namespace Marbid.Module.Controllers
{
   // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.

   public partial class ScheduleAddParticipantViewController : ViewController
   {
      public ScheduleAddParticipantViewController()
      {
         InitializeComponent();
         // Target required Views (via the TargetXXX properties) and create their Actions.
         TargetObjectType = typeof(Schedule);
      }
      protected override void OnActivated()
      {
         base.OnActivated();
         // Perform various tasks depending on the target View.
         //AddParticipantAction.Active.SetItemValue("Security", SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(Schedule), "OurParticipants", ObjectSpace.GetObjectHandle(View.CurrentObject), SecurityOperations.Write)));
         //RemoveParticipantAction.Active.SetItemValue("Security", SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(Schedule), "OurParticipants", ObjectSpace.GetObjectHandle(View.CurrentObject), SecurityOperations.Write)));
         //LockScheduleAction.Active.SetItemValue("Security", SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(Schedule), "IsLocked", ObjectSpace.GetObjectHandle(View.CurrentObject), SecurityOperations.Write)));
         //AddNewMOMAction.Active.SetItemValue("Security", SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(Schedule), "MinutesOfMeetings", ObjectSpace.GetObjectHandle(View.CurrentObject), SecurityOperations.Write)));

         //AddParticipantAction.Enabled.SetItemValue("IsCurrentObjectNew", !View.ObjectSpace.IsNewObject(((DetailView)View).CurrentObject));
         //RemoveParticipantAction.Enabled.SetItemValue("IsCurrentObjectNew", !View.ObjectSpace.IsNewObject(((DetailView)View).CurrentObject));
         //LockScheduleAction.Enabled.SetItemValue("IsCurrentObjectNew", !View.ObjectSpace.IsNewObject(((DetailView)View).CurrentObject));
         //AddNewMOMAction.Enabled.SetItemValue("IsCurrentObjectNew", !View.ObjectSpace.IsNewObject(((DetailView)View).CurrentObject));
         //AddParticipantAction.CustomizeTemplate += CustomizeTemplate;
         //AddInternalParticipantAction.CustomizeTemplate += CustomizeTemplate;
      }
      private void CustomizeTemplate(object sender, CustomizeTemplateEventArgs e)
      {
         if (e.Template != null)
         {
            ((ILookupPopupFrameTemplate)e.Template).IsSearchEnabled = true;
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

      private void AddParticipantAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
      {
         Schedule currentSchedule = (Schedule)View.CurrentObject;
         View.ObjectSpace.SetModified(currentSchedule);
         foreach (People emp in e.PopupWindow.View.SelectedObjects)
         {
            People nEmp = ObjectSpace.GetObjectByKey<People>(emp.Oid);
            //currentSchedule.ScheduleParticipants.Add(nEmp);
            ScheduleParticipant partic = ObjectSpace.CreateObject<ScheduleParticipant>();
            partic.Participant = nEmp;
            currentSchedule.ScheduleParticipants.Add(partic);
         }
         currentSchedule.UpdateParticipants(true);
         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
         {
            View.ObjectSpace.CommitChanges();
         }
      }

      private void AddParticipantAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
      {
         Schedule currentSchedule = (Schedule)View.CurrentObject;
         IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(Contact));
         string participantListId = Application.FindLookupListViewId(typeof(Contact));
         CollectionSourceBase collectionSource = Application.CreateCollectionSource(objectSpace, typeof(Contact), participantListId);
         ListView view = Application.CreateListView(participantListId, collectionSource, true);
         view.CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("[Organization] = ?", objectSpace.GetObject<Organization>(currentSchedule.Organization));
         e.Context = TemplateContext.FindPopupWindow;
         e.View = view;
      }

      private void RemoveParticipantAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
      {
         Schedule currentSchedule = (Schedule)View.CurrentObject;
         View.ObjectSpace.SetModified(currentSchedule);

         ArrayList arrayList = new ArrayList();

         foreach (TemporaryScheduleParticipant temp in e.PopupWindow.View.SelectedObjects)
         {
            foreach (ScheduleParticipant partic in currentSchedule.ScheduleParticipants)
            {
               if (partic.Participant.Oid == temp.Participant.Oid)
               {
                  arrayList.Add(partic);
               }
            }
         }
         foreach (ScheduleParticipant toDelete in arrayList)
         {
            currentSchedule.ScheduleParticipants.Remove(toDelete);
         }

         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
         {
            View.ObjectSpace.CommitChanges();
         }
      }

      private void RemoveParticipantAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
      {
         //string participantListId = Application.FindLookupListViewId(typeof(ScheduleParticipant));
         //IObjectSpace objectSpace = Application.CreateObjectSpace();
         //Schedule master = (Schedule)objectSpace.GetObject(View.CurrentObject);
         //CollectionSource cs = new CollectionSource(objectSpace, typeof(ScheduleParticipant));
         //cs.Criteria["ScheduleParticipantFilter"] = new InOperator("Oid", master.ScheduleParticipants);
         //e.View = Application.CreateListView(participantListId, cs, false);

         //IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(ScheduleParticipant));
         //Schedule currentSchedule = (Schedule)objectSpace.GetObject(View.CurrentObject);
         //string participantListId = Application.FindLookupListViewId(typeof(ScheduleParticipant));
         //CollectionSourceBase collectionSource = Application.CreatePropertyCollectionSource(objectSpace, typeof(Schedule), currentSchedule, XafTypesInfo.Instance.FindTypeInfo(typeof(Schedule)).FindMember("ScheduleParticipants"), participantListId);
         //e.View = Application.CreateListView(participantListId, collectionSource, false);


         IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(TemporaryScheduleParticipant));
         string participantListId = Application.FindLookupListViewId(typeof(TemporaryScheduleParticipant));
         CollectionSource collectionSource = new CollectionSource(objectSpace, typeof(TemporaryScheduleParticipant));
         Schedule currentSchedule = (Schedule)View.CurrentObject;

         foreach (ScheduleParticipant detail in currentSchedule.ScheduleParticipants)
         {
            TemporaryScheduleParticipant tempParticipant = objectSpace.CreateObject<TemporaryScheduleParticipant>();
            tempParticipant.Participant = objectSpace.GetObjectByKey<People>(detail.Participant.Oid);
            collectionSource.Add(tempParticipant);
         }
         
         
         e.View = Application.CreateListView(participantListId, collectionSource, false);
         e.View.Caption = "Remove Participant";
         e.Context = TemplateContext.FindPopupWindow;
         //var listView = Application.CreateListView(ObjectSpace, typeof(ScheduleParticipant), false);
         //if (this.View is ListView)
         //{
         //   foreach (object obj in ((ListView)this.View).CollectionSource.List)
         //   {
         //      listView.CollectionSource.List.Add(obj);
         //   }
         //}
         //if (((Schedule)View.CurrentObject).ScheduleParticipants.Count > 0)
         //{
         //   foreach(object detail in ((Schedule)View.CurrentObject).ScheduleParticipants)
         //   {
         //      listView.CollectionSource.Add(detail);
         //   }
         //}
         //e.View = listView;
         //e.Context = TemplateContext.LookupWindow;
         //e.DialogController.SaveOnAccept = false;
      }

      private void LockScheduleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
      {
         IObjectSpace objectSpace = Application.CreateObjectSpace();
         Schedule currentSchedule = (Schedule)View.CurrentObject;
         View.ObjectSpace.SetModified(currentSchedule);
         currentSchedule.IsLocked = true;

         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
         {
            View.ObjectSpace.CommitChanges();
         }
      }

      private void AddNewMOMAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
      {
         MinutesOfMeeting meeting = ObjectSpace.CreateObject<MinutesOfMeeting>();
         meeting.Schedule = ObjectSpace.GetObject<Schedule>((Schedule)View.CurrentObject);
         e.View = Application.CreateDetailView(ObjectSpace, meeting, false);
         ((DetailView)e.View).ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
      }

      private void AddNewMOMAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
      {
         Schedule currentSchedule = (Schedule)View.CurrentObject;
         View.ObjectSpace.SetModified(currentSchedule);
         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
         {
            View.ObjectSpace.CommitChanges();
         }
      }

      private void AddInternalParticipantAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
      {
         Schedule currentSchedule = (Schedule)View.CurrentObject;
         View.ObjectSpace.SetModified(currentSchedule);
         foreach (Employee emp in e.PopupWindow.View.SelectedObjects)
         {
            Employee nEmp = ObjectSpace.GetObjectByKey<Employee>(emp.Oid);
            ScheduleParticipant partic = ObjectSpace.CreateObject<ScheduleParticipant>();
            partic.Participant = (People)nEmp;
            currentSchedule.ScheduleParticipants.Add(partic);
         }
         currentSchedule.UpdateParticipants(true);
         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
         {
            View.ObjectSpace.CommitChanges();
         }
      }

      private void AddInternalParticipantAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
      {
         IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(Employee));
         string participantListId = Application.FindLookupListViewId(typeof(Employee));
         CollectionSourceBase collectionSource = Application.CreateCollectionSource(objectSpace, typeof(Employee), participantListId);
         ListView view = Application.CreateListView(participantListId, collectionSource, true);
         view.CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("[IsActive] = ?", true);
         e.Context = TemplateContext.FindPopupWindow;
         e.View = view;
      }

      private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
      {
         //IObjectSpace objectSpace = Application.CreateObjectSpace();
         MinutesOfMeeting meeting = ObjectSpace.CreateObject<MinutesOfMeeting>();
         meeting.Schedule = (Schedule)View.CurrentObject;
         e.ShowViewParameters.CreatedView = Application.CreateDetailView(ObjectSpace, meeting, false);
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

      private void AssignCarAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
      {
         Schedule currentSchedule = (Schedule)View.CurrentObject;
         Employee currentUser = ObjectSpace.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
         View.ObjectSpace.SetModified(currentSchedule);
         currentSchedule.AssignedCar = ObjectSpace.GetObjectByKey<Car>(((AssignCarParametersObject)e.PopupWindow.View.CurrentObject).DesignatedCar.Oid);
         currentSchedule.AssignedDriver = ObjectSpace.GetObjectByKey<Driver>(((AssignCarParametersObject)e.PopupWindow.View.CurrentObject).DesignatedDriver.Oid);
         currentSchedule.CarAssignmentNote = ((AssignCarParametersObject)e.PopupWindow.View.CurrentObject).Comment;
         currentSchedule.CarAssignedBy = currentUser;
         currentSchedule.CarAssignedDate = DateTime.Now;
         currentSchedule.IsCarAssigned = true;

         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
         {
            View.ObjectSpace.CommitChanges();
         }
      }

      private void AssignCarAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
      {
         IObjectSpace objectSpace = Application.CreateObjectSpace();
         e.View = Application.CreateDetailView(Application.CreateObjectSpace(), AssignCarParametersObject.CreateAssignCarParameterObject());
         ((DetailView)e.View).ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
      }
   }
}
