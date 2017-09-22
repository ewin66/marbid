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

namespace Marbid.Module.BusinessObjects.HRM
{
    [DefaultClassOptions]
    [NavigationItem(false)]
    [CreatableItem(false)]
    public class TaskNote : Marbid.Module.BusinessObjects.General.Notes
    {
        private Marbid.Module.BusinessObjects.HRM.Task _task;
        public TaskNote(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [DevExpress.Xpo.AssociationAttribute("TaskNotes-Task"), DevExpress.Xpo.Aggregated]
        [DevExpress.Persistent.Base.VisibleInDetailViewAttribute(false)]
        [DevExpress.Persistent.Base.VisibleInLookupListViewAttribute(false)]
        [System.ComponentModel.BrowsableAttribute(false)]
        [RuleRequiredField()]
        public Marbid.Module.BusinessObjects.HRM.Task Task
        {
            get
            {
                return _task;
            }
            set
            {
                SetPropertyValue("Task", ref _task, value);
            }
        }
    }
}
