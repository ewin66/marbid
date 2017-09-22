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
   [DevExpress.Persistent.Base.ImageNameAttribute("BO_Invoice")]
   public class CustomerFinancialInfo : BaseObject
   {
      private System.Double _claimRecovery;
      private System.Double _reinsurancePremium;
      private System.Double _profitLoss;
      private Marbid.Module.BusinessObjects.CRM.Company _company;
      private System.Double _totalAsset;
      private System.Int16 _accountingQuarter;
      private System.Int16 _accountingYear;
      private System.Double _equity;
      private System.Double _solvabilityRatio;
      private System.Double _grossClaim;
      private System.Double _grossPremiumIncome;
      public CustomerFinancialInfo(DevExpress.Xpo.Session session)
        : base(session)
      {
      }
      [DevExpress.Xpo.AssociationAttribute("FinancialInfo-Company")]
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
      [RuleRange("", DefaultContexts.Save, 1950, 2100)]
      public System.Int16 AccountingYear
      {
         get
         {
            return _accountingYear;
         }
         set
         {
            SetPropertyValue("AccountingYear", ref _accountingYear, value);
         }
      }
      [RuleRange("", DefaultContexts.Save, 0, 4)]
      public System.Int16 AccountingQuarter
      {
         get
         {
            return _accountingQuarter;
         }
         set
         {
            SetPropertyValue("AccountingQuarter", ref _accountingQuarter, value);
         }
      }
      public System.Double GrossPremiumIncome
      {
         get
         {
            return _grossPremiumIncome;
         }
         set
         {
            SetPropertyValue("GrossPremiumIncome", ref _grossPremiumIncome, value);
         }
      }
      public System.Double ReinsurancePremium
      {
         get
         {
            return _reinsurancePremium;
         }
         set
         {
            SetPropertyValue("ReinsurancePremium", ref _reinsurancePremium, value);
         }
      }
      public System.Double GrossClaim
      {
         get
         {
            return _grossClaim;
         }
         set
         {
            SetPropertyValue("GrossClaim", ref _grossClaim, value);
         }
      }
      public System.Double SolvabilityRatio
      {
         get
         {
            return _solvabilityRatio;
         }
         set
         {
            SetPropertyValue("SolvabilityRatio", ref _solvabilityRatio, value);
         }
      }
      public System.Double Equity
      {
         get
         {
            return _equity;
         }
         set
         {
            SetPropertyValue("Equity", ref _equity, value);
         }
      }
      public System.Double TotalAsset
      {
         get
         {
            return _totalAsset;
         }
         set
         {
            SetPropertyValue("TotalAsset", ref _totalAsset, value);
         }
      }
      public System.Double ProfitLoss
      {
         get
         {
            return _profitLoss;
         }
         set
         {
            SetPropertyValue("ProfitLoss", ref _profitLoss, value);
         }
      }
      public System.Double ClaimRecovery
      {
         get
         {
            return _claimRecovery;
         }
         set
         {
            SetPropertyValue("ClaimRecovery", ref _claimRecovery, value);
         }
      }
      double premiumReserve;
      public double PremiumReserve
      {
         get
         {
            return premiumReserve;
         }
         set
         {
            SetPropertyValue("PremiumReserve", ref premiumReserve, value);
         }
      }
      double claimReserve;
      public double ClaimReserve
      {
         get
         {
            return claimReserve;
         }
         set
         {
            SetPropertyValue("ClaimReserve", ref claimReserve, value);
         }
      }
      double commission;
      public double Commission
      {
         get
         {
            return commission;
         }
         set
         {
            SetPropertyValue("Commission", ref commission, value);
         }
      }
      double reinsuranceCommission;
      public double ReinsuranceCommission
      {
         get
         {
            return reinsuranceCommission;
         }
         set
         {
            SetPropertyValue("ReinsuranceCommission", ref reinsuranceCommission, value);
         }
      }
      double underwritingIncome;
      public double UnderwritingIncome
      {
         get
         {
            return underwritingIncome;
         }
         set
         {
            SetPropertyValue("UnderwritingIncome", ref underwritingIncome, value);
         }
      }
      double underwritingExpenses;
      public double UnderwritingExpenses
      {
         get
         {
            return underwritingExpenses;
         }
         set
         {
            SetPropertyValue("UnderwritingExpenses", ref underwritingExpenses, value);
         }
      }
      double underwritingResult;
      public double UnderwritingResult
      {
         get
         {
            return underwritingResult;
         }
         set
         {
            SetPropertyValue("UnderwritingResult", ref underwritingResult, value);
         }
      }

   }
}
