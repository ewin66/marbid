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
using DevExpress.ExpressApp.ConditionalAppearance;

namespace Marbid.Module.BusinessObjects.CRM
{
    [DefaultClassOptions]
    [ImageName("BO_Opportunity")]
    [NavigationItem("CRM")]
    [DefaultProperty("BeneficiaryName")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [Appearance("Disable", TargetItems = "CreatedBy,CreateDate,ModifiedBy,ModifyDate", Enabled = false)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class BankAccount : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public BankAccount(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            CreatedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            CreateDate = DateTime.Now;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            ModifiedBy = Session.GetObjectByKey<Employee>(SecuritySystem.CurrentUserId);
            ModifyDate = DateTime.Now;
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string BeneficiaryName { get; set; }

        [Association("Company-BankAccounts")]
        [RuleRequiredField]
        public Company Company { get; set; }

        [RuleRequiredField]
        public Bank Bank { get; set; }

        [RuleRequiredField]
        public Currency Currency { get; set; }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        [RuleUniqueValue]
        public string AccountNumber { get; set; }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string IBAN { get; set; }

        [VisibleInListView(false)]
        public Employee CreatedBy { get; set; }

        [VisibleInListView(false)]
        public DateTime CreateDate { get; set; }

        [VisibleInListView(false)]
        public Employee ModifiedBy { get; set; }

        [VisibleInListView(false)]
        public DateTime ModifyDate { get; set; }
    }
}