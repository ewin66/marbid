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

namespace Marbid.Module.BusinessObjects.CRM
{
   [DefaultClassOptions]
   [DevExpress.Persistent.Base.ImageNameAttribute("BO_Customer")]
   [NavigationItem(false)]
   public class CompanyBoardOfDirectors : BaseObject
   {
      private System.Boolean _isCurrent;
      private System.DateTime _periodTo;
      private System.DateTime _periodFrom;
      private Marbid.Module.BusinessObjects.CRM.Company _company;
      private Marbid.Module.BusinessObjects.CRM.Contact _name;
      private System.String _title;
      public CompanyBoardOfDirectors(DevExpress.Xpo.Session session)
        : base(session)
      {
      }
      public Marbid.Module.BusinessObjects.CRM.Contact Name
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
      public System.String Title
      {
         get
         {
            return _title;
         }
         set
         {
            SetPropertyValue("Title", ref _title, value);
         }
      }
      public System.DateTime PeriodFrom
      {
         get
         {
            return _periodFrom;
         }
         set
         {
            SetPropertyValue("PeriodFrom", ref _periodFrom, value);
         }
      }
      public System.DateTime PeriodTo
      {
         get
         {
            return _periodTo;
         }
         set
         {
            SetPropertyValue("PeriodTo", ref _periodTo, value);
         }
      }
      public System.Boolean IsCurrent
      {
         get
         {
            return _isCurrent;
         }
         set
         {
            SetPropertyValue("IsCurrent", ref _isCurrent, value);
         }
      }
      [DevExpress.Xpo.AssociationAttribute("BoardOfDirectors-Company")]
      public Marbid.Module.BusinessObjects.CRM.Company Company
      {
         get
         {
            return _company;
         }
         set
         {
            SetPropertyValue("Company", ref _company, value);
         }
      }
   }
}
