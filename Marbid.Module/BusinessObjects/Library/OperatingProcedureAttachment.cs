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

namespace Marbid.Module.BusinessObjects.Library
{
    [DefaultClassOptions, NavigationItem(false)]
    [CreatableItem(false)]
    [ImageName("BO_FileAttachment")]
    public class OperatingProcedureAttachment : BaseObject
    {
        private Marbid.Module.BusinessObjects.Library.StandardOperatingProcedure _standardOperatingProcedure;
        public OperatingProcedureAttachment(DevExpress.Xpo.Session session)
          : base(session)
        {
        }
        [Association("OperatingProcedureAttachments-StandardOperatingProcedure")]
        public Marbid.Module.BusinessObjects.Library.StandardOperatingProcedure StandardOperatingProcedure
        {
            get
            {
                return _standardOperatingProcedure;
            }
            set
            {
                SetPropertyValue("StandardOperatingProcedure", ref _standardOperatingProcedure, value);
            }
        }
    }
}
