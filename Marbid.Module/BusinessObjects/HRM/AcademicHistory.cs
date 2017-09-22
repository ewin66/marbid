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
  [ImageName("degree")]
  public class AcademicHistory : BaseObject
  { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
    public AcademicHistory(Session session)
        : base(session)
    {
    }
    private System.Drawing.Image _certificate;
    private Marbid.Module.BusinessObjects.Administration.Employee _employee;
    private System.DateTime _enrollment;
    private System.DateTime _graduation;
    private Marbid.Module.BusinessObjects.HRM.AcademicInstitution _institution;
    private Marbid.Module.BusinessObjects.HRM.Degree _degree;
    public override void AfterConstruction()
    {
      base.AfterConstruction();
    }
    public Marbid.Module.BusinessObjects.HRM.Degree Degree
    {
      get
      {
        return _degree;
      }
      set
      {
        SetPropertyValue("Degree", ref _degree, value);
      }
    }
    public Marbid.Module.BusinessObjects.HRM.AcademicInstitution Institution
    {
      get
      {
        return _institution;
      }
      set
      {
        SetPropertyValue("Institution", ref _institution, value);
      }
    }
    public System.DateTime Graduation
    {
      get
      {
        return _graduation;
      }
      set
      {
        SetPropertyValue("Graduation", ref _graduation, value);
      }
    }
    public System.DateTime Enrollment
    {
      get
      {
        return _enrollment;
      }
      set
      {
        SetPropertyValue("Enrollment", ref _enrollment, value);
      }
    }
    [DevExpress.Xpo.AssociationAttribute("AcademicHistory-Employee")]
    public Marbid.Module.BusinessObjects.Administration.Employee Employee
    {
      get
      {
        return _employee;
      }
      set
      {
        SetPropertyValue("Employee", ref _employee, value);
      }
    }
    [DevExpress.Xpo.ValueConverterAttribute(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
    public System.Drawing.Image Certificate
    {
      get
      {
        return _certificate;
      }
      set
      {
        SetPropertyValue("Certificate", ref _certificate, value);
      }
    }
  }
}