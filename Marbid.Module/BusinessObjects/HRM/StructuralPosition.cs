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
  [DevExpress.Persistent.Base.ImageNameAttribute("BO_Position")]
  public class StructuralPosition : BaseObject
  {
    private Marbid.Module.BusinessObjects.HRM.RankGroup _rankGroup;
    private System.Int16 _index;
    private System.String _description;
    private System.String _name;
    public StructuralPosition(DevExpress.Xpo.Session session)
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
    [DevExpress.Xpo.AssociationAttribute("Employees-StructuralPosition")]
    public XPCollection<Marbid.Module.BusinessObjects.Administration.Employee> Employees
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.Administration.Employee>("Employees");
      }
    }
    public System.Int16 Index
    {
      get
      {
        return _index;
      }
      set
      {
        SetPropertyValue("Index", ref _index, value);
      }
    }
    [DevExpress.Xpo.AssociationAttribute("StructuralPositions-RankGroup")]
    [RuleRequiredField]
    public Marbid.Module.BusinessObjects.HRM.RankGroup RankGroup
    {
      get
      {
        return _rankGroup;
      }
      set
      {
        SetPropertyValue("RankGroup", ref _rankGroup, value);
      }
    }
  }
}
