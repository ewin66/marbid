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
    [DevExpress.Persistent.Base.ImageNameAttribute("InventoryCategory")]
    public class InventoryCategory : BaseObject
    {
        private System.String _categoryCode;
        private System.Boolean _isReported;
        private System.String _description;
        private System.String _categoryName;
        public InventoryCategory(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            IsReported = true;
        }
        [DevExpress.Xpo.SizeAttribute(3)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRegularExpression("", DefaultContexts.Save, @"[0-9]{3,3}")]
        public System.String CategoryCode
        {
            get
            {
                return _categoryCode;
            }
            set
            {
                SetPropertyValue("CategoryCode", ref _categoryCode, value);
            }
        }
        [RuleRequiredField("", DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public System.String CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                SetPropertyValue("CategoryName", ref _categoryName, value);
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
        [DevExpress.Xpo.AssociationAttribute("InventoryItems-InventoryCategory")]
        public XPCollection<Marbid.Module.BusinessObjects.Inventory.InventoryItem> InventoryItems
        {
            get
            {
                return GetCollection<Marbid.Module.BusinessObjects.Inventory.InventoryItem>("InventoryItems");
            }
        }
        public System.Boolean IsReported
        {
            get
            {
                return _isReported;
            }
            set
            {
                SetPropertyValue("IsReported", ref _isReported, value);
            }
        }
    }
}
