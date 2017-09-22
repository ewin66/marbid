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
using DevExpress.ExpressApp.Scheduler.Web;
using DevExpress.Persistent.Base.General;
using Marbid.Module.BusinessObjects;
using Marbid.Module.BusinessObjects.CRM;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using DevExpress.ExpressApp.Security;

namespace Marbid.Module.Web.Controllers
{
  // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
  public partial class SchedulerInfoViewController : ViewController<ListView>
  {
    public SchedulerInfoViewController()
    {
      InitializeComponent();
      // Target required Views (via the TargetXXX properties) and create their Actions.
      TargetObjectType = typeof(Schedule);
      //TargetViewType = ViewType.ListView;
    }
    protected override void OnActivated()
    {
      base.OnActivated();
      // Perform various tasks depending on the target View.
    }
    ASPxSchedulerListEditor listEditor;
    protected override void OnViewControlsCreated()
    {
      base.OnViewControlsCreated();
      listEditor = View.Editor as ASPxSchedulerListEditor;
      if (listEditor != null)
      {
        ASPxScheduler scheduler = (ASPxScheduler)listEditor.SchedulerControl;
        scheduler.InitAppointmentDisplayText -= scheduler_InitAppointmentDisplayText;
        scheduler.InitAppointmentDisplayText += scheduler_InitAppointmentDisplayText;
        scheduler.OptionsCustomization.AllowAppointmentDrag = UsedAppointmentType.None;
        scheduler.OptionsCustomization.AllowAppointmentResize = UsedAppointmentType.None;

      }
      // Access and customize the target View control.
      //editor = ((ListView)View).Editor as ASPxSchedulerListEditor;
      //if (editor != null)
      //{
      //    editor.SchedulerControl.AppointmentViewInfoCustomizing += new DevExpress.Web.ASPxScheduler.AppointmentViewInfoCustomizingEventHandler(SchedulerControl_AppointmentViewInfoCustomizing);
      //}

    }

    private void scheduler_InitAppointmentDisplayText(object sender, AppointmentDisplayTextEventArgs e)
    {
      Appointment appointment = e.Appointment;
      if (appointment.IsRecurring)
        appointment = e.Appointment.RecurrencePattern;
      Schedule obj = (Schedule)listEditor.SourceObjectHelper.GetSourceObject(appointment);
      string ScheduleLocation;
      if (obj.MeetingRoom != null)
        ScheduleLocation = obj.MeetingRoom.Name;
      else
        ScheduleLocation = "Out of Office";
      e.Text = string.Format("{0}-({1})", obj.Subject, ScheduleLocation);
      e.Description = string.Format("{0} Participant(s): {1}", obj.Description, obj.Participants);
    }

    protected override void OnDeactivated()
    {
      base.OnDeactivated();
      if (listEditor != null && listEditor.SchedulerControl != null)
      {
        ASPxScheduler scheduler = (ASPxScheduler)listEditor.SchedulerControl;
        scheduler.InitAppointmentDisplayText -= scheduler_InitAppointmentDisplayText;
      }
    }

    void SchedulerControl_AppointmentViewInfoCustomizing(object sender, DevExpress.Web.ASPxScheduler.AppointmentViewInfoCustomizingEventArgs e)
    {
      //e.ViewInfo.Appointment.Description += "tambahanaeiutusfdkf";
    }
  }
}
