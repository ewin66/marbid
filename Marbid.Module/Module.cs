using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Notifications;
using DevExpress.ExpressApp.Scheduler;
using DevExpress.Data.Filtering;
using Marbid.Module.BusinessObjects.CRM;
using Marbid.Module.ModelExtender;

namespace Marbid.Module
{
  // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppModuleBasetopic.aspx.
  public sealed partial class MarbidModule : ModuleBase
  {
    public MarbidModule()
    {
      InitializeComponent();
      BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction;
    }
    public override void ExtendModelInterfaces(ModelInterfaceExtenders extenders)
    {
      base.ExtendModelInterfaces(extenders);
      extenders.Add<IModelView, IViewHintModel>();
    }
    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
    {
      ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
      return new ModuleUpdater[] { updater };
    }
    public override void Setup(XafApplication application)
    {
      base.Setup(application);
      // Manage various aspects of the application UI and behavior at the module level.
      application.LoggedOn += Application_LoggedOn;
    }

    private void Application_LoggedOn(object sender, LogonEventArgs e)
    {
      SchedulerModuleBase schedulerModule = Application.Modules.FindModule<SchedulerModuleBase>();
      NotificationsProvider notificationsProvider = schedulerModule.NotificationsProvider;
      notificationsProvider.CustomizeNotificationCollectionCriteria += NotificationsProvider_CustomizeNotificationCollectionCriteria;
    }

    private void NotificationsProvider_CustomizeNotificationCollectionCriteria(object sender, DevExpress.Persistent.Base.General.CustomizeCollectionCriteriaEventArgs e)
    {
      if (e.Type == typeof(Schedule))
      {
        e.Criteria = CriteriaOperator.Parse("[CreatedBy.Oid] = CurrentUserId()");
      }
    }

    public override void CustomizeTypesInfo(ITypesInfo typesInfo)
    {
      base.CustomizeTypesInfo(typesInfo);  
      CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
    }
  }
}
