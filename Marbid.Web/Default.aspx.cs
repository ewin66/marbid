using System;
using System.Collections.Generic;
using System.Web.UI;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web.Templates.ActionContainers;
using Marbid.Web;
using Marbid.Module.Web.Controllers;
using Marbid.Module.Web;

public partial class Default : BaseXafPage, IHintPlaceHolderWeb
{
  protected override ContextActionsMenu CreateContextActionsMenu()
  {
    return new ContextActionsMenu(this, "Edit", "RecordEdit", "ObjectsCreation", "ListView", "Reports");
  }
  public Control HintPlaceHolder
  {
    get
    {
      return TemplateContent is MareinDefaultVerticalTemplateContent
          ? ((MareinDefaultVerticalTemplateContent)TemplateContent).HintPlaceHolder : null;
    }
  }
  public override Control InnerContentPlaceHolder
  {
    get
    {
      return Content;
    }
  }
}
