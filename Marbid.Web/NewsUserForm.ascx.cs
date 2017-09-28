using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Marbid.Web
{
    public partial class NewsUserForm : System.Web.UI.UserControl, IComplexControl
    {
        private IObjectSpace objectSpace;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void IComplexControl.Setup(IObjectSpace objectSpace, XafApplication application)
        {
            this.objectSpace = objectSpace;
        }

        void IComplexControl.Refresh() { }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ASPxNewsControl1.DataSource = objectSpace.GetObjects<Marbid.Module.BusinessObjects.CRM.News>();
            ASPxNewsControl1.DataBind();
        }
    }
}