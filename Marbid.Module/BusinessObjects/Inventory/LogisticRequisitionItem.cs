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
using Marbid.Module.BusinessObjects.Administration;

namespace Marbid.Module.BusinessObjects.Inventory
{
   [DefaultClassOptions]
   [DevExpress.Persistent.Base.ImageNameAttribute("InventoryItems")]
   [NavigationItem(false)]
   public class LogisticRequisitionItem : BaseObject
   {
      private Marbid.Module.BusinessObjects.Administration.Employee _deliveryBy;
      private System.DateTime _deliveryDate;
      private System.Int16 _fulfilled;
      private Marbid.Module.BusinessObjects.Inventory.InventoryItem _inventoryItem;
      private Marbid.Module.BusinessObjects.Inventory.LogisticRequisition _logisticRequisition;
      private System.Int16 _quantity;
      private System.String _note;
      public LogisticRequisitionItem(DevExpress.Xpo.Session session)
        : base(session)
      {
      }
      [DevExpress.Xpo.AssociationAttribute("Items-LogisticRequisition")]
      public Marbid.Module.BusinessObjects.Inventory.LogisticRequisition LogisticRequisition
      {
         get
         {
            return _logisticRequisition;
         }
         set
         {
            LogisticRequisition oldLogisticRequisition = LogisticRequisition;
            SetPropertyValue("LogisticRequisition", ref _logisticRequisition, value);
            if (!IsLoading && !IsSaving && oldLogisticRequisition != _logisticRequisition)
            {
               oldLogisticRequisition = oldLogisticRequisition ?? _logisticRequisition;
               oldLogisticRequisition.UpdateAuthorizationLevel(true);
            }
         }
      }

      [RuleRequiredField("", DefaultContexts.Save)]
      [DevExpress.Xpo.AssociationAttribute("LogisticRequisitionItems-InventoryItem")]
      [DataSourceCriteria("IsActive=True")]
      public Marbid.Module.BusinessObjects.Inventory.InventoryItem InventoryItem
      {
         get
         {
            return _inventoryItem;
         }
         set
         {
            InventoryItem oldInventoryItem = InventoryItem;
            SetPropertyValue("InventoryItem", ref _inventoryItem, value);
            if (!IsLoading && !IsSaving && oldInventoryItem != _inventoryItem)
            {
               oldInventoryItem = oldInventoryItem ?? _inventoryItem;
               oldInventoryItem.UpdateTotalFulfillment(true);
            }
         }
      }
      public System.String Note
      {
         get
         {
            return _note;
         }
         set
         {
            SetPropertyValue("Note", ref _note, value);
         }
      }
      [RuleValueComparison("", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)]
      public System.Int16 Quantity
      {
         get
         {
            return _quantity;
         }
         set
         {
            SetPropertyValue("Quantity", ref _quantity, value);
         }
      }
      public System.Int16 Fulfilled
      {
         get
         {
            return _fulfilled;
         }
         set
         {
            InventoryItem oldInventoryItem = InventoryItem;
            SetPropertyValue("Fulfilled", ref _fulfilled, value);
            if (!IsLoading && !IsSaving && oldInventoryItem != _inventoryItem)
            {
               oldInventoryItem = oldInventoryItem ?? _inventoryItem;
               oldInventoryItem.UpdateTotalFulfillment(true);
            }
         }
      }
      public System.DateTime DeliveryDate
      {
         get
         {
            return _deliveryDate;
         }
         set
         {
            SetPropertyValue("DeliveryDate", ref _deliveryDate, value);
         }
      }
      public Marbid.Module.BusinessObjects.Administration.Employee DeliveryBy
      {
         get
         {
            return _deliveryBy;
         }
         set
         {
            SetPropertyValue("DeliveryBy", ref _deliveryBy, value);
         }
      }
      [Action(AutoCommit = true, Caption = "Deliver All", ConfirmationMessage = "This action will set all items as delivered, are you sure want to continue?")]
      public void DeliverAll(DeliverAllParameters param)
      {
         this.Fulfilled = this.Quantity;
         this.DeliveryDate = param.DeliveryDate;
         this.DeliveryBy = Session.FindObject<Employee>(CriteriaOperator.Parse("[Oid] = ?", param.DeliveredBy.Oid));
      }
   }
   [DomainComponent]
   public class DeliverAllParameters
   {

      [RuleRequiredField]
      public Employee DeliveredBy { get; set; }
      [RuleRequiredField]
      public DateTime DeliveryDate { get; set; }
   }
}
