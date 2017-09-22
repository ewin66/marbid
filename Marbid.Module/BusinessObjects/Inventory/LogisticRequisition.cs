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
using Marbid.Module.BusinessObjects.HRM;

namespace Marbid.Module.BusinessObjects.Inventory
{
   [DefaultClassOptions]
   [DevExpress.Persistent.Base.ImageNameAttribute("InventoryRequisition")]
   [DefaultProperty("RequisitionCode")]
   public class LogisticRequisition : BaseObject
   {
      private DateTime _CreateDate;
      private System.String _requisitionCode;
      private System.DateTime _requestDate;
      private Marbid.Module.BusinessObjects.RequisitonStatus _status;
      private System.DateTime _authorizationDate;
      private System.String _authorizationNote;
      private System.String _requestNote;
      private Marbid.Module.BusinessObjects.Administration.Employee _authorizedBy;
      private Marbid.Module.BusinessObjects.Administration.Employee _requestBy;
      public LogisticRequisition(DevExpress.Xpo.Session session)
        : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         RequestDate = DateTime.Now;
         RequestBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
         Status = RequisitonStatus.Pending;
         RequisitionCode = string.Format("R{0}", RequestDate.ToString("yyMMddHHmmssfff"));
         CreateDate = DateTime.Now;
      }
      public Marbid.Module.BusinessObjects.Administration.Employee RequestBy
      {
         get
         {
            return _requestBy;
         }
         set
         {
            SetPropertyValue("RequestBy", ref _requestBy, value);
         }
      }
      public System.DateTime RequestDate
      {
         get
         {
            return _requestDate;
         }
         set
         {
            SetPropertyValue("RequestDate", ref _requestDate, value);
         }
      }
      public System.String RequisitionCode
      {
         get
         {
            return _requisitionCode;
         }
         set
         {
            SetPropertyValue("RequisitionCode", ref _requisitionCode, value);
         }
      }
      public Marbid.Module.BusinessObjects.Administration.Employee AuthorizedBy
      {
         get
         {
            return _authorizedBy;
         }
         set
         {
            SetPropertyValue("AuthorizedBy", ref _authorizedBy, value);
         }
      }
      [RuleRequiredField("", DefaultContexts.Save)]
      [DevExpress.Xpo.AssociationAttribute("Items-LogisticRequisition")]
      [DevExpress.Xpo.Aggregated]
      public XPCollection<Marbid.Module.BusinessObjects.Inventory.LogisticRequisitionItem> Items
      {
         get
         {
            return GetCollection<Marbid.Module.BusinessObjects.Inventory.LogisticRequisitionItem>("Items");
         }
      }
      [DevExpress.Xpo.SizeAttribute(5000)]
      public System.String RequestNote
      {
         get
         {
            return _requestNote;
         }
         set
         {
            SetPropertyValue("RequestNote", ref _requestNote, value);
         }
      }
      [DevExpress.Xpo.SizeAttribute(5000)]
      public System.String AuthorizationNote
      {
         get
         {
            return _authorizationNote;
         }
         set
         {
            SetPropertyValue("AuthorizationNote", ref _authorizationNote, value);
         }
      }
      public System.DateTime AuthorizationDate
      {
         get
         {
            return _authorizationDate;
         }
         set
         {
            SetPropertyValue("AuthorizationDate", ref _authorizationDate, value);
         }
      }
      public Marbid.Module.BusinessObjects.RequisitonStatus Status
      {
         get
         {
            return _status;
         }
         set
         {
            SetPropertyValue("Status", ref _status, value);
         }
      }

      public DateTime CreateDate
      {
         get
         {
            return _CreateDate;
         }
         set
         {
            SetPropertyValue("CreateDate", ref _CreateDate, value);
         }
      }
      [Persistent("AutorizationLevel")]
      private RankGroup _authorizationLevel;
      [PersistentAlias("_authorizationLevel")]
      [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
      public RankGroup AuthorizationLevel
      {
         get
         {
            if (!IsLoading && !IsSaving && _authorizationLevel == null)
               UpdateAuthorizationLevel(false);
            return _authorizationLevel;
         }
      }
      public void UpdateAuthorizationLevel(bool forceChangeEvents)
      {
         RankGroup oldAuthorizationlevel = _authorizationLevel;
         RankGroup tempRankGroup = Session.FindObject<RankGroup>(CriteriaOperator.Parse("[GroupIndex] <= ?", 0)); //(CriteriaOperator.Parse("[<RankGroup>].Min([GroupIndex]) <= ?", 0))
         foreach (LogisticRequisitionItem detail in Items)
         {
            if (detail.InventoryItem.AuthorizationLevel.GroupIndex > tempRankGroup.GroupIndex)
            {
               tempRankGroup = detail.InventoryItem.AuthorizationLevel;
            }
         }
         _authorizationLevel = tempRankGroup;
         if (forceChangeEvents)
         {
            OnChanged("AuthorizationLevel", oldAuthorizationlevel, _authorizationLevel);
         }
      }
      [Action(ConfirmationMessage = "Once approval is made you can not modify this requisition, are you sure want to continue?", TargetObjectsCriteria = "[RequestBy.Oid] = CurrentUserId() AND [Status] = 'Pending' AND IsNewObject(This) = False", AutoCommit = true, Caption = "Request Approval")]
      public void RequestApproval()
      {
         Status = RequisitonStatus.RequestApproval;
         Save();
      }
      //[Action(ConfirmationMessage = "Please make sure the requisition made is valid, are you sure you want to approve this requisition?", Caption = "Approval", ToolTip = "Approve or not Approve this requisition", AutoCommit = true, TargetObjectsCriteria = [Status])]
      //public void Approval(LogisticRequisitionApprovalParametersObject param)
      //{
      //  Status = (Marbid.Module.BusinessObjects.RequisitonStatus)param.Approve;
      //  AuthorizationNote = param.Comment;
      //  AuthorizedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
      //  AuthorizationDate = DateTime.Now;
      //}
      [Action(ConfirmationMessage = "Are you sure want to process this requisition?", TargetObjectsCriteria = "IsCurrentUserInRole('Logistic') AND Status = 'Approved'", Caption = "Process", ToolTip = "Mark this requisition as on process!", AutoCommit = true)]
      public void MarkProcess()
      {
         Status = RequisitonStatus.OnProcess;
         Save();
      }
      [Action(ConfirmationMessage = "Once requisition is mark as complete, it cannot be modified. Are you sure want to continue?", TargetObjectsCriteria = "IsCurrentUserInRole('Logistic') AND Status = 'OnProcess'", Caption = "Complete", ToolTip = "Mark this requisition as completed, this action cannot be undone!", AutoCommit = true)]
      public void MarkComlete()
      {
         Status = RequisitonStatus.Completed;
         foreach (LogisticRequisitionItem detail in Items)
         {
            detail.DeliveryDate = DateTime.Now;
            detail.DeliveryBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            detail.Fulfilled = detail.Quantity;
            detail.Save();
         }
         Save();
      }
   }
   [DomainComponent]
   public class LogisticRequisitionApprovalParametersObject
   {
      public static Type LogisticRequisitionApprovalParametersObjectType = typeof(LogisticRequisitionApprovalParametersObject);
      public static LogisticRequisitionApprovalParametersObject CreateLogisticRequisitionApprovalParametersObject()
      {
         return (LogisticRequisitionApprovalParametersObject)ReflectionHelper.CreateObject(LogisticRequisitionApprovalParametersObjectType);
      }
      public enum LogisticApproval
      {
         Undefined = 0,
         NotApproved = 2,
         Approved = 3
      }
      [RuleValueComparison(null, DefaultContexts.Save, ValueComparisonType.NotEquals, LogisticApproval.Undefined)]
      public LogisticApproval Approve { get; set; }
      [RuleRequiredField("", DefaultContexts.Save),
      SizeAttribute(5000)]
      public string Comment { get; set; }

   }
}
