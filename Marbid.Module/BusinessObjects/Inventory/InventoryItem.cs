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

namespace Marbid.Module.BusinessObjects.Inventory
{
   [DefaultClassOptions]
   [DevExpress.Persistent.Base.ImageNameAttribute("InventoryItems")]
   public class InventoryItem : BaseObject
   {
      private System.Boolean _isActive;
      private System.String _itemCode;
      private System.String _description;
      private Marbid.Module.BusinessObjects.HRM.RankGroup _authorizationLevel;
      private Marbid.Module.BusinessObjects.Inventory.InventoryCategory _inventoryCategory;
      private System.String _inventoryName;
      private readonly static string displayNameFormat = "{InventoryCategory.CategoryCode}{ItemCode}";
      public InventoryItem(DevExpress.Xpo.Session session)
        : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
      }
      protected override void OnLoaded()
      {
         Reset();
         base.OnLoaded();
      }
      private void Reset()
      {
         _totalQuantity = null;
         _totalFulfillment = null;
      }
      [DevExpress.Xpo.SizeAttribute(3)]
      [RuleRequiredField("", DefaultContexts.Save)]
      //[RuleUniqueValue("", DefaultContexts.Save)]
      [RuleRegularExpression("", DefaultContexts.Save, @"[0-9]{3,3}")]
      [VisibleInListView(false)]
      [ImmediatePostData(true)]
      public System.String ItemCode
      {
         get
         {
            return _itemCode;
         }
         set
         {
            SetPropertyValue("ItemCode", ref _itemCode, value);
            OnChanged("Code");
         }
      }
      [VisibleInDetailView(false)]
      public string Code
      {
         get
         {
            return ObjectFormatter.Format(displayNameFormat, this, EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty);
         }
      }
      [RuleRequiredField("", DefaultContexts.Save)]
      public System.String InventoryName
      {
         get
         {
            return _inventoryName;
         }
         set
         {
            SetPropertyValue("InventoryName", ref _inventoryName, value);
         }
      }
      [DevExpress.Xpo.AssociationAttribute("InventoryItems-InventoryCategory")]
      [ImmediatePostData(true)]
      public Marbid.Module.BusinessObjects.Inventory.InventoryCategory InventoryCategory
      {
         get
         {
            return _inventoryCategory;
         }
         set
         {
            SetPropertyValue("InventoryCategory", ref _inventoryCategory, value);
         }
      }
      [RuleRequiredField("", DefaultContexts.Save)]
      public Marbid.Module.BusinessObjects.HRM.RankGroup AuthorizationLevel
      {
         get
         {
            return _authorizationLevel;
         }
         set
         {
            SetPropertyValue("AuthorizationLevel", ref _authorizationLevel, value);
         }
      }
      [DevExpress.Xpo.SizeAttribute(5000)]
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
      [DevExpress.Xpo.AssociationAttribute("InventoryStocks-InventoryItem")]
      public XPCollection<Marbid.Module.BusinessObjects.Inventory.InventoryStock> InventoryStocks
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.Inventory.InventoryStock>("InventoryStocks");
         }
      }
      [Persistent("TotalQuantity")]
      private Int16? _totalQuantity;
      [PersistentAlias("_totalQuantity")]
      public Int16? TotalQuantity
      {
         get
         {
            if (!IsLoading && !IsSaving && _totalQuantity == null)
               UpdateTotalQuantity(false);
            return _totalQuantity;
         }
      }
      public void UpdateTotalQuantity(bool forceChangeEvents)
      {
         Int16? oldTotalQuantity = _totalQuantity;
         Int16 tempTotal = 0;
         foreach (InventoryStock detail in InventoryStocks)
            if (detail.IsPosted == true)
               tempTotal += detail.Quantity;
         _totalQuantity = tempTotal;
         if (forceChangeEvents)
         {
            OnChanged("TotalQuantity", oldTotalQuantity, _totalQuantity);
            OnChanged("StockBalance");
         }
      }
      [Persistent("TotalFullfillment")]
      private Int16? _totalFulfillment;
      [PersistentAlias("_totalFulfillment")]
      public Int16? TotalFulfillment
      {
         get
         {
            if (!IsLoading && !IsSaving && _totalFulfillment == null)
            {
               UpdateTotalFulfillment(false);
            }
            return _totalFulfillment;
         }
      }
      public void UpdateTotalFulfillment(bool forceChangeEvents)
      {
         Int16? oldTotalFulfillment = _totalQuantity;
         Int16 tempTotal = 0;
         foreach (LogisticRequisitionItem detail in LogisticRequisitionItems)
            if (detail.LogisticRequisition.Status == RequisitonStatus.Completed)
               tempTotal += detail.Fulfilled;
         _totalFulfillment = tempTotal;
         if (forceChangeEvents)
         {
            OnChanged("TotalFulfillment", oldTotalFulfillment, _totalFulfillment);
            OnChanged("StockBalance");
         }
      }

      [Persistent("StockBalance")]
      private int _stockBalance;
      [PersistentAlias("TotalQuantity - TotalFulfillment")]
      public int StockBalance
      {
         get
         {
            object tempObject = EvaluateAlias("StockBalance");
            if (tempObject != null)
            {
               _stockBalance = (int)tempObject;
               return _stockBalance;
            }
            else
            {
               _stockBalance = 0;
               return _stockBalance;
            }

         }
      }
      [DevExpress.Xpo.AssociationAttribute("LogisticRequisitionItems-InventoryItem")]
      public XPCollection<Marbid.Module.BusinessObjects.Inventory.LogisticRequisitionItem> LogisticRequisitionItems
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.Inventory.LogisticRequisitionItem>("LogisticRequisitionItems");
         }
      }
      public System.Boolean IsActive
      {
         get
         {
            return _isActive;
         }
         set
         {
            SetPropertyValue("IsActive", ref _isActive, value);
         }
      }
   }
}
