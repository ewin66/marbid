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
using Marbid.Module.BusinessObjects.HRM;
using System.Collections;
using DevExpress.Xpo;
using Marbid.Module.BusinessObjects.Administration;
namespace Marbid.Module.Controllers
{
  // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
  public partial class AnnouncementViewController : ViewController
  {
    public AnnouncementViewController()
    {
      InitializeComponent();
      // Target required Views (via the TargetXXX properties) and create their Actions.
      TargetObjectType = typeof(Announcement);
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

    private void AddRecipientAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
    {
      IObjectSpace objectSpace = Application.CreateObjectSpace();
      e.View = Application.CreateListView("Employee_LookupListView", new CollectionSource(objectSpace, typeof(Employee)), true);
    }

    private void AddRecipientAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
    {
      //Schedule currentSchedule = (Schedule)View.CurrentObject;
      Announcement currentAnnouncement = (Announcement)View.CurrentObject;

      View.ObjectSpace.SetModified(currentAnnouncement);

      foreach (Employee emp in e.PopupWindow.View.SelectedObjects)
      {
        Employee nEmp = ObjectSpace.GetObjectByKey<Employee>(emp.Oid);
        AnnouncementRecepients nRec = new AnnouncementRecepients(currentAnnouncement.Session);
        nRec.Employee = nEmp;
        nRec.Email = nEmp.CorporateEmail;
        currentAnnouncement.AnnouncementRecepients.Add(nRec);
      }

      if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
      {
        View.ObjectSpace.CommitChanges();
      }
    }

    private void AddRecepientAllAction_Execute(object sender, SimpleActionExecuteEventArgs e)
    {
      Announcement currentAnnouncement = (Announcement)View.CurrentObject;
      View.ObjectSpace.SetModified(currentAnnouncement);

      ICollection employees;
      DevExpress.Xpo.Metadata.XPClassInfo employeeClass;
      DevExpress.Data.Filtering.CriteriaOperator criteria;
      DevExpress.Xpo.SortingCollection sortProps;
      DevExpress.Xpo.Session session;
      DevExpress.Xpo.Generators.CollectionCriteriaPatcher patcher;

      session = currentAnnouncement.Session;
      employeeClass = session.GetClassInfo(typeof(Employee));
      criteria = CriteriaOperator.Parse("IsActive = true");
      sortProps = new SortingCollection(null);
      sortProps.Add(new SortProperty("FullName", DevExpress.Xpo.DB.SortingDirection.Ascending));
      patcher = new DevExpress.Xpo.Generators.CollectionCriteriaPatcher(false, session.TypesManager);
      employees = session.GetObjects(employeeClass, criteria, sortProps, 0, false, false);
      foreach (Employee employee in employees)
      {
        if (employee.IsActive == true)
        {
          AnnouncementRecepients nRec = new AnnouncementRecepients(currentAnnouncement.Session);
          nRec.Employee = employee;
          nRec.Email = employee.CorporateEmail;
          currentAnnouncement.AnnouncementRecepients.Add(nRec);
        }

      }

      if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
      {
        View.ObjectSpace.CommitChanges();
      }
    }

    private void AddRecipientByRankGroupAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
    {
      Announcement currentAnnouncement = (Announcement)View.CurrentObject;
      View.ObjectSpace.SetModified(currentAnnouncement);
      foreach (RankGroup selected in e.PopupWindow.View.SelectedObjects)
      {

        ICollection tempEmployee;
        DevExpress.Xpo.Metadata.XPClassInfo employeeClass;
        DevExpress.Data.Filtering.CriteriaOperator criteria;
        DevExpress.Xpo.SortingCollection sortProps;
        DevExpress.Xpo.Session session;
        DevExpress.Xpo.Generators.CollectionCriteriaPatcher patcher;

        session = currentAnnouncement.Session;
        //session.ConnectionString = XpoDefault.ConnectionString;

        // Obtain the persistent object class info required by the GetObjects method 
        employeeClass = session.GetClassInfo(typeof(Employee));
        // Create criteria to get objects 
        criteria = CriteriaOperator.Parse("StructuralPosition.RankGroup.Oid = ? And IsActive = true", selected.Oid);
        // Create a sort list if objects must be processed in a specific order 
        sortProps = new SortingCollection(null);
        sortProps.Add(new SortProperty("FirstName", DevExpress.Xpo.DB.SortingDirection.Ascending));

        // Create criteria patcher to filter out the objects marked as "deleted"  
        // and to support loading of inherited objects of a given base persistent class 
        patcher = new DevExpress.Xpo.Generators.CollectionCriteriaPatcher(false, session.TypesManager);

        // Call GetObjects 


        //tempEmployee = session.GetObjects(employeeClass, criteria, sortProps, 0, patcher, selectDeleted:false, force: false);
        tempEmployee = session.GetObjects(employeeClass, criteria, sortProps, 0, 0, false, true);

        foreach (Employee emp in tempEmployee)
        {
          if (emp.IsActive == true)
          {
            Employee nEmp = ObjectSpace.GetObjectByKey<Employee>(emp.Oid);
            AnnouncementRecepients nRec = new AnnouncementRecepients(currentAnnouncement.Session);
            nRec.Employee = nEmp;
            nRec.Email = nEmp.CorporateEmail;
            currentAnnouncement.AnnouncementRecepients.Add(nRec);
          }
        }
      }
      if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
      {
        View.ObjectSpace.CommitChanges();
      }
    }

    private void AddRecipientByRankGroupAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
    {
      IObjectSpace os = Application.CreateObjectSpace();
      e.View = Application.CreateListView("RankGroup_LookupListView", new CollectionSource(os, typeof(RankGroup)), true);
    }
  }
}
