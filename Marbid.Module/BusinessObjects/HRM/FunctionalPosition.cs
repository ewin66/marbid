using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace Marbid.Module.BusinessObjects.HRM
{
  [DefaultClassOptions]
  [DevExpress.Persistent.Base.ImageNameAttribute("BO_Employee")]
  public class FunctionalPosition : BaseObject
  {
    private System.String _description;
    private System.String _name;
    public FunctionalPosition(DevExpress.Xpo.Session session)
      : base(session)
    {
    }
    public System.String Name
    {
      get
      {
        return _name;
      }
      set
      {
        SetPropertyValue("Name", ref _name, value);
      }
    }
    [DevExpress.Xpo.SizeAttribute(1000)]
    public System.String Description
    {
      get
      {
        return _description;
      }
      set
      {
        SetPropertyValue("Description", ref _description, value);
      }
    }
    [DevExpress.Xpo.AssociationAttribute("Employees-FunctionalPosition")]
    public XPCollection<Marbid.Module.BusinessObjects.Administration.Employee> Employees
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.Administration.Employee>("Employees");
      }
    }
  }
}