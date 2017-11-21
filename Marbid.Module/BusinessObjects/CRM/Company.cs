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
   [DefaultProperty("DisplayName")]
   [NavigationItem("CRM")]
   public class Company : Organization
   {
      private System.Boolean _inBusiness;
      private System.DateTime _inCooperationSince;
      private System.String _briefHistory;
      private System.String _liraCode;
      private Marbid.Module.BusinessObjects.BusinessRole _businessRole;
      private Marbid.Module.BusinessObjects.CorporationType _corporationType;
      private System.Boolean _isPublicCompany;
      private System.String _stockTicker;
      private System.Boolean _isWatchList;
      private System.String _coreBelieve;
      private System.String _coreValue;
      private System.String _mission;
      private System.String _vision;
      private System.String _iDXCode;
      private System.Int32 _endeavourCode;
      private System.String _reinsCode;
      private System.Int16 _branchCount;
      private System.Int16 _employeeCount;
      private Marbid.Module.BusinessObjects.CRM.InsuranceDivision _mainBusiness;
      public Company(DevExpress.Xpo.Session session)
        : base(session)
      {
      }
      public System.String ReinsCode
      {
         get
         {
            return _reinsCode;
         }
         set
         {
            SetPropertyValue("ReinsCode", ref _reinsCode, value);
         }
      }
      public System.Int32 EndeavourCode
      {
         get
         {
            return _endeavourCode;
         }
         set
         {
            SetPropertyValue("EndeavourCode", ref _endeavourCode, value);
         }
      }
      public System.String LiraCode
      {
         get
         {
            return _liraCode;
         }
         set
         {
            SetPropertyValue("LiraCode", ref _liraCode, value);
         }
      }
      public System.String IDXCode
      {
         get
         {
            return _iDXCode;
         }
         set
         {
            SetPropertyValue("IDXCode", ref _iDXCode, value);
         }
      }
      [Size(SizeAttribute.Unlimited)]
      public System.String Vision
      {
         get
         {
            return _vision;
         }
         set
         {
            SetPropertyValue("Vision", ref _vision, value);
         }
      }
      [Size(SizeAttribute.Unlimited)]
      public System.String Mission
      {
         get
         {
            return _mission;
         }
         set
         {
            SetPropertyValue("Mission", ref _mission, value);
         }
      }
      [Size(SizeAttribute.Unlimited)]
      public System.String CoreValue
      {
         get
         {
            return _coreValue;
         }
         set
         {
            SetPropertyValue("CoreValue", ref _coreValue, value);
         }
      }
      [Size(SizeAttribute.Unlimited)]
      public System.String CoreBelieve
      {
         get
         {
            return _coreBelieve;
         }
         set
         {
            SetPropertyValue("CoreBelieve", ref _coreBelieve, value);
         }
      }
      public System.Int16 EmployeeCount
      {
         get
         {
            return _employeeCount;
         }
         set
         {
            SetPropertyValue("EmployeeCount", ref _employeeCount, value);
         }
      }
      public System.Int16 BranchCount
      {
         get
         {
            return _branchCount;
         }
         set
         {
            SetPropertyValue("BranchCount", ref _branchCount, value);
         }
      }
      [DevExpress.Xpo.AssociationAttribute("FinancialInfo-Company")]
      public XPCollection<Marbid.Module.BusinessObjects.CRM.CustomerFinancialInfo> FinancialInfo
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.CRM.CustomerFinancialInfo>("FinancialInfo");
         }
      }
      public System.String StockTicker
      {
         get
         {
            return _stockTicker;
         }
         set
         {
            SetPropertyValue("StockTicker", ref _stockTicker, value);
         }
      }
      public Marbid.Module.BusinessObjects.CorporationType CorporationType
      {
         get
         {
            return _corporationType;
         }
         set
         {
            SetPropertyValue("CorporationType", ref _corporationType, value);
         }
      }
      public Marbid.Module.BusinessObjects.BusinessRole BusinessRole
      {
         get
         {
            return _businessRole;
         }
         set
         {
            SetPropertyValue("BusinessRole", ref _businessRole, value);
         }
      }
      public System.Boolean IsWatchList
      {
         get
         {
            return _isWatchList;
         }
         set
         {
            SetPropertyValue("IsWatchList", ref _isWatchList, value);
         }
      }
      public System.Boolean IsPublicCompany
      {
         get
         {
            return _isPublicCompany;
         }
         set
         {
            SetPropertyValue("IsPublicCompany", ref _isPublicCompany, value);
         }
      }
      Pefindo pefindo;
      [ModelDefault("Caption", "Pefindo")]
      public Pefindo Pefindo
      {
         get
         {
            return pefindo;
         }
         set
         {
            SetPropertyValue("Pefindo", ref pefindo, value);
         }
      }
      FitchRatings fitchRatings;
      [ModelDefault("Caption", "FitchRatings")]
      public FitchRatings FitchRatings
      {
         get
         {
            return fitchRatings;
         }
         set
         {
            SetPropertyValue("FitchRatings", ref fitchRatings, value);
         }
      }
      StandardPoor standardPoor;
      [ModelDefault("Caption", "Standard & Poor")]
      public StandardPoor StandardPoor
      {
         get
         {
            return standardPoor;
         }
         set
         {
            SetPropertyValue("StandardPoor", ref standardPoor, value);
         }
      }

      Moody moody;
      [ModelDefault("Caption", "Moody's")]
      public Moody Moody
      {
         get
         {
            return moody;
         }
         set
         {
            SetPropertyValue("Moody", ref moody, value);
         }
      }

      AMBest aMBest;
      [ModelDefault("Caption", "AM Best")]
      public AMBest AMBest
      {
         get
         {
            return aMBest;
         }
         set
         {
            SetPropertyValue("AMBest", ref aMBest, value);
         }
      }

      [DevExpress.Xpo.AssociationAttribute("BoardOfDirectors-Company")]
      public XPCollection<Marbid.Module.BusinessObjects.CRM.CompanyBoardOfDirectors> BoardOfDirectors
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.CRM.CompanyBoardOfDirectors>("BoardOfDirectors");
         }
      }
      [DevExpress.Xpo.AssociationAttribute("BoardOfCommissioners-Company")]
      public XPCollection<Marbid.Module.BusinessObjects.CRM.CompanyBoardOfCommissioners> BoardOfCommissioners
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.CRM.CompanyBoardOfCommissioners>("BoardOfCommissioners");
         }
      }
      [DevExpress.Xpo.SizeAttribute(5000)]
      public System.String BriefHistory
      {
         get
         {
            return _briefHistory;
         }
         set
         {
            SetPropertyValue("BriefHistory", ref _briefHistory, value);
         }
      }
      [Association("Company-BusinessTransactionSummary")]
      public XPCollection<BusinessTransactionSummary> BusinessTransactionSummary
      {
         get
         {
            return GetCollection<BusinessTransactionSummary>("BusinessTransactionSummary");
         }
      }
        [Association("Company-BankAccounts")]
        public XPCollection<BankAccount> BankAccounts
        {
            get
            {
                return GetCollection<BankAccount>("BankAccounts");
            }
        }
        [DevExpress.Persistent.Base.ToolTipAttribute("First known recorded transaction (might not be accurate)")]
      public System.DateTime InCooperationSince
      {
         get
         {
            return _inCooperationSince;
         }
         set
         {
            SetPropertyValue("InCooperationSince", ref _inCooperationSince, value);
         }
      }
      [DevExpress.Xpo.AssociationAttribute("InsuranceDivisions-Companies")]
      public InsuranceDivision MainBusiness
      {
         get
         {
            return _mainBusiness;
         }
         set
         {
            SetPropertyValue("MainBusiness", ref _mainBusiness, value);
         }
      }
      public System.Boolean InBusiness
      {
         get
         {
            return _inBusiness;
         }
         set
         {
            SetPropertyValue("InBusiness", ref _inBusiness, value);
         }
      }
   }
}
