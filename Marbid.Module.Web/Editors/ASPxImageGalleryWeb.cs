using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp;
using System.Collections;
using System.ComponentModel;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Model;
using System.Web.UI;
using System.IO;
using System.Web;
using DevExpress.Web;
using Marbid.Module.Web.Editors;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Web;
using Marbid.Module.CustomInterface;

namespace Marbid.Module.Web.Editors
{
  [ListEditor(typeof(IPictureItem), false)]
  public class ASPxImageGalleryWeb : ListEditor
  {
    public ASPxImageGalleryWeb(IModelListView info) : base(info) { }
    private ASPxImageGallery control;
    protected override object CreateControlsCore()
    {
      control = new ASPxImageGallery();
      control.ID = "CustomListEditor_control";
      control.Width = Unit.Percentage(100);
      return control;
    }
    protected override void AssignDataSourceToControl(Object dataSource)
    {
      if (control != null)
      {
        IList ds = ListHelper.GetList(dataSource);
        control.DataSource = ds;
        control.ImageContentBytesField = "ImageByte";
        control.SettingsFolder.ImageCacheFolder = "~\\thumb\\";
        control.DataBind();
      }
    }
    public override void Refresh()
    {
      if (control != null) control.DataBind();
    }

    public override SelectionType SelectionType
    {
      get { return SelectionType.TemporarySelection; }
    }
    public override IList GetSelectedObjects()
    {
      List<object> selectedObjects = new List<object>();
      if (FocusedObject != null)
      {
        selectedObjects.Add(FocusedObject);
      }
      return selectedObjects;
    }

    private object focusedObject;
    public override object FocusedObject
    {
      get
      {
        return focusedObject;
      }
      set
      {
        focusedObject = value;
      }
    }
  }
}
