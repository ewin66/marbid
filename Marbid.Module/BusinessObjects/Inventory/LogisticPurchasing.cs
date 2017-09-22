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
   [DevExpress.Persistent.Base.ImageNameAttribute("BO_Order")]
   [XafDefaultProperty("PurchaseCode")]
   public class LogisticPurchasing : BaseObject
   {
      private System.String _purchaseCode;
      private System.Boolean _isPosted;
      private System.String _note;
      private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
      private System.DateTime _createDate;
      private System.DateTime _purchaseDate;
      private System.String _purchaseFrom;
      public LogisticPurchasing(DevExpress.Xpo.Session session)
        : base(session)
      {
      }
      protected override void OnLoaded()
      {
         Reset();
         base.OnLoaded();
      }
      private void Reset()
      {
         _totalPurchase = null;
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         PurchaseDate = DateTime.Now;
         CreateDate = DateTime.Now;
         IsPosted = false;
         CreatedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
         PurchaseCode = string.Format("P{0}", CreateDate.ToString("yyMMddHHmmssfff"));
      }
      protected override void OnSaved()
      {
         base.OnSaved();
      }
      public System.String PurchaseCode
      {
         get
         {
            return _purchaseCode;
         }
         set
         {
            SetPropertyValue("PurchaseCode", ref _purchaseCode, value);
         }
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
      public System.String PurchaseFrom
      {
         get
         {
            return _purchaseFrom;
         }
         set
         {
            SetPropertyValue("PurchaseFrom", ref _purchaseFrom, value);
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
      [DevExpress.Xpo.SizeAttribute(5000)]
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
      [DevExpress.Xpo.AssociationAttribute("PurchaseItems-LogisticPurchasing")]
      [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
      public XPCollection<Marbid.Module.BusinessObjects.Inventory.InventoryStock> PurchaseItems
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.Inventory.InventoryStock>("PurchaseItems");
         }
      }
      public Boolean IsPosted
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
      [Action(ConfirmationMessage = "Once a purchas is mark as posted, it cannot be modified. Are you sure want to continue?", TargetObjectsCriteria = "IsCurrentUserInRole('Logistic') AND IsPosted = False AND IsNewObject(This) = False", Caption = "Post", ToolTip = "Mark this item as posted, this action cannot be undone!", AutoCommit = true)]
      public void MarkPosted()
      {
         IsPosted = true;
         foreach (InventoryStock detail in PurchaseItems)
         {
            detail.IsPosted = true;
            detail.Save();
         }
         Session.CommitTransaction();
         Session.Save(this);
      }
      [Action(ConfirmationMessage = "This purchase item will be unposted, Are you sure want to continue?", TargetObjectsCriteria = "IsCurrentUserInRole('Logistic') AND IsPosted = True AND IsNewObject(This) = False", Caption = "Unpost", ToolTip = "Mark this item as unposted.", AutoCommit = true)]
      public void MarkUnpost()
      {
         IsPosted = false;

         foreach (InventoryStock detail in PurchaseItems)
         {
            detail.IsPosted = false;
            detail.Save();
         }
         Session.CommitTransaction();
         Session.Save(this);
      }
      [Persistent("TotalPurchase")]
      private decimal? _totalPurchase;
      [PersistentAlias("_totalPurchase")]
      public decimal? TotalPurchase
      {
         get
         {
            if (!IsLoading && !IsSaving && _totalPurchase == null)
               UpdateTotalPurchase(false);
            return _totalPurchase;
         }
      }
      public void UpdateTotalPurchase(bool forceChangeEvent)
      {
         decimal? oldTotalPurchase = _totalPurchase;
         decimal tempTotalPurchase = 0;
         foreach (InventoryStock detail in PurchaseItems)
            tempTotalPurchase += detail.Price;
         _totalPurchase = tempTotalPurchase;
         if (forceChangeEvent)
         {
            OnChanged("TotalPurchase", oldTotalPurchase, _totalPurchase);
         }
      }
   }
}
