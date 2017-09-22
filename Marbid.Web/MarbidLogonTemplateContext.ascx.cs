﻿using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Web.Controls;
using DevExpress.ExpressApp.Web.Templates;
namespace Marbid.Web
{
   public partial class MarbidLogonTemplateContext : TemplateContent, IXafPopupWindowControlContainer, IHeaderImageControlContainer
   {
      public override void SetStatus(ICollection<string> statusMessages)
      {
      }
      public override IActionContainer DefaultContainer
      {
         get { return null; }
      }
      public override object ViewSiteControl
      {
         get { return viewSiteControl; }
      }
      public XafPopupWindowControl XafPopupWindowControl
      {
         get { return PopupWindowControl; }
      }
      public override void BeginUpdate()
      {
         base.BeginUpdate();
         this.PopupActions.BeginUpdate();
      }
      public override void EndUpdate()
      {
         this.PopupActions.EndUpdate();
         base.EndUpdate();
      }
      public ThemedImageControl HeaderImageControl { get { return TIC; } }

      protected void Page_Load(object sender, EventArgs e)
      {

      }
   }
}
