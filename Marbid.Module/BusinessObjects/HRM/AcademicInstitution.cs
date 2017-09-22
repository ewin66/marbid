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
  [ImageName("school")]
  [DefaultClassOptions]
  public class AcademicInstitution : CRM.Organization
  {
    public AcademicInstitution(Session session)
        : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
    }
    private Marbid.Module.BusinessObjects.EducationInstitutionType _institutionType;
    public Marbid.Module.BusinessObjects.EducationInstitutionType InstitutionType
    {
      get
      {
        return _institutionType;
      }
      set
      {
        SetPropertyValue("InstitutionType", ref _institutionType, value);
      }
    }
  }
}