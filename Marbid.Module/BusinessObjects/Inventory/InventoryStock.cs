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
    public class InventoryStock : BaseObject
    {
        private Marbid.Module.BusinessObjects.Inventory.LogisticPurchasing _logisticPurchasing;
        private System.String _brand;
        private System.DateTime _createDate;
        private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
        private System.Boolean _isPosted;
        private System.Decimal _price;
        private Marbid.Module.BusinessObjects.Inventory.InventoryItem _inventoryItem;
        private System.String _note;
        private System.Int16 _quantity;
        private System.DateTime _purchaseDate;
        public InventoryStock(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CreatedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
            PurchaseDate = DateTime.Now;
            CreateDate = DateTime.Now;
        }
        [RuleRequiredField("", DefaultContexts.Save)]
        public System.DateTime PurchaseDate
        {
            get
            {
                return _purchaseDate;
            }
            set
            {
                SetPropertyValue("PurchaseDate", ref _purchaseDate, value);
            }
        }
        public System.String Brand
        {
            get
            {
                return _brand;
            }
            set
            {
                SetPropertyValue("Brand", ref _brand, value);
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
        [DevExpress.Xpo.SizeAttribute(200)]
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
        [DevExpress.Xpo.AssociationAttribute("InventoryStocks-InventoryItem")]
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
                    oldInventoryItem.UpdateTotalQuantity(true);
                }
            }
        }
        [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
        public System.Decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                SetPropertyValue("Price", ref _price, value);
            }
        }
        public System.Boolean IsPosted
        {
            get
            {
                return _isPosted;
            }
            set
            {
                SetPropertyValue("IsPosted", ref _isPosted, value);
            }
        }
        public Marbid.Module.BusinessObjects.Administration.Employee CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                SetPropertyValue("CreatedBy", ref _createdBy, value);
            }
        }
        [Action(ConfirmationMessage = "Once Inventory Stock is mark as posted, it cannot be modified. Are you sure want to continue?", TargetObjectsCriteria = "CreatedBy.Oid = CurrentUserId() AND IsCurrentUserInRole('Logistic') AND IsPosted = False AND IsNewObject(This) = False", Caption = "Post", ToolTip = "Mark this item as posted, this action cannot be undone!", AutoCommit = true)]
        public void MarkPosted()
        {
            IsPosted = true;
            Save();
        }
        public System.DateTime CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                SetPropertyValue("CreateDate", ref _createDate, value);
            }
        }
        [DevExpress.Xpo.AssociationAttribute("PurchaseItems-LogisticPurchasing")]
        [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
        public Marbid.Module.BusinessObjects.Inventory.LogisticPurchasing LogisticPurchasing
        {
            get
            {
                return _logisticPurchasing;
            }
            set
            {
                LogisticPurchasing oldLogisticPurchasing = LogisticPurchasing;
                SetPropertyValue("LogisticPurchasing", ref _logisticPurchasing, value);
                if (!IsLoading && !IsSaving && oldLogisticPurchasing != _logisticPurchasing)
                {
                    oldLogisticPurchasing = oldLogisticPurchasing ?? _logisticPurchasing;
                    oldLogisticPurchasing.UpdateTotalPurchase(true);
                }
            }
        }
    }
}
