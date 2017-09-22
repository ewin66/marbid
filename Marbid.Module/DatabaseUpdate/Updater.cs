using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using Marbid.Module.BusinessObjects.Administration;
using Marbid.Module.BusinessObjects.CRM;

namespace Marbid.Module.DatabaseUpdate
{
  // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
  public class Updater : ModuleUpdater
  {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion)
    {
    }
    public override void UpdateDatabaseAfterUpdateSchema()
    {
      base.UpdateDatabaseAfterUpdateSchema();
      //string name = "MyName";
      //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
      //if(theObject == null) {
      //    theObject = ObjectSpace.CreateObject<DomainObject1>();
      //    theObject.Name = name;
      //}
      Employee sampleUser = ObjectSpace.FindObject<Employee>(new BinaryOperator("UserName", "Norman"));
      if (sampleUser == null)
      {
        sampleUser = ObjectSpace.CreateObject<Employee>();
        sampleUser.UserName = "User";
        sampleUser.SetPassword("");
      }
      MarbidRole defaultRole = CreateDefaultRole();
      sampleUser.MarbidRoles.Add(defaultRole);

      Employee userAdmin = ObjectSpace.FindObject<Employee>(new BinaryOperator("UserName", "Admin"));
      if (userAdmin == null)
      {
        userAdmin = ObjectSpace.CreateObject<Employee>();
        userAdmin.UserName = "SAdmin";
        // Set a password if the standard authentication type is used
        userAdmin.SetPassword("");
      }
      // If a role with the Administrators name doesn't exist in the database, create this role
      MarbidRole adminRole = ObjectSpace.FindObject<MarbidRole>(new BinaryOperator("Name", "Administrators"));
      if (adminRole == null)
      {
        adminRole = ObjectSpace.CreateObject<MarbidRole>();
        adminRole.Name = "Administrators";
      }
      adminRole.IsAdministrative = true;
      userAdmin.MarbidRoles.Add(adminRole);

      MarbidRole carManagerRole = ObjectSpace.FindObject<MarbidRole>(new BinaryOperator("Name", "Car Manager"));
      if (carManagerRole == null)
      {
        carManagerRole = ObjectSpace.CreateObject<MarbidRole>();
        carManagerRole.Name = "Car Manager";
        carManagerRole.IsAdministrative = false;
      }

      if (ObjectSpace.GetObjectsCount(typeof(SystemSetting), null) == 0)
      {
        SystemSetting ss = ObjectSpace.CreateObject<SystemSetting>();
        ss.MaximumOfficeLeaveDays = 3;
        ss.YearlyOfficeLeaveDays = 14;
        ss.DriverManagerRole = adminRole;
        ss.HRRole = adminRole;
      }

      ObjectSpace.CommitChanges(); //This line persists created object(s).
    }
    public override void UpdateDatabaseBeforeUpdateSchema()
    {
      base.UpdateDatabaseBeforeUpdateSchema();
      //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
      //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
      //}
    }
    private MarbidRole CreateDefaultRole()
    {
      MarbidRole defaultRole = ObjectSpace.FindObject<MarbidRole>(new BinaryOperator("Name", "Default"));
      if (defaultRole == null)
      {
        defaultRole = ObjectSpace.CreateObject<MarbidRole>();
        defaultRole.Name = "Default";

        defaultRole.AddObjectPermission<Employee>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
        defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
        defaultRole.AddMemberPermission<Employee>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
        defaultRole.AddMemberPermission<Employee>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
        defaultRole.AddTypePermissionsRecursively<Employee>(SecurityOperations.Read, SecurityPermissionState.Allow);
        defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
        defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
        defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
        defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);        
      }
      return defaultRole;
    }
  }
}
