//using DevExpress.ExpressApp.Editors;
//using DevExpress.ExpressApp.Web.Templates;
//using DevExpress.Web;
//using Marbid.Module.CustomInterface;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DevExpress.ExpressApp.Utils;
//using DevExpress.ExpressApp.Model;
//using DevExpress.ExpressApp;

//namespace Marbid.Module.Web.Editors
//{
//  public class CardListEditorClickEventArgs : EventArgs
//  {
//    public IPictureItem ItemClicked;
//  }


//  [ListEditor(typeof(IPictureItem))]
//  public class ASPxCardListView : ListEditor
//  {
//    private ASPxCardListViewControl control;
//    private object focusedObject;
//    protected override object CreateControlsCore()
//    {
//      control = new ASPxCardListViewControl();
//      control.ID = "CardListEditor_control";
//      control.OnClick += new EventHandler<CardListEditorClickEventArgs>(control_OnClick);
//      return control;
//    }
//    private void control_OnClick(object sender, CardListEditorClickEventArgs e)
//    {
//      this.FocusedObject = e.ItemClicked;
//      OnSelectionChanged();
//      OnProcessSelectedItem();
//    }
//    protected override void AssignDataSourceToControl(Object dataSource)
//    {
//      if (control != null)
//      {
//        control.DataSource = dataSource;//ListHelper.GetList(dataSource);
//      }
//    }
//    protected override void OnSelectionChanged()
//    {
//      base.OnSelectionChanged();
//    }
//    public ASPxCardListView(IModelListView info) : base(info) { }
//    public override IList GetSelectedObjects()
//    {
//      List<object> selectedObjects = new List<object>();
//      if (FocusedObject != null)
//      {
//        selectedObjects.Add(FocusedObject);
//      }
//      return selectedObjects;
//    }

//    public override void SaveModel()
//    {
//    }
//    public override object FocusedObject
//    {
//      get
//      {
//        return focusedObject;
//      }
//      set
//      {
//        focusedObject = value;
//      }
//    }
//    public override DevExpress.ExpressApp.Templates.IContextMenuTemplate ContextMenuTemplate
//    {
//      get { return null; }
//    }
//    public override bool AllowEdit
//    {
//      get
//      {
//        return false;
//      }
//      set
//      {
//      }
//    }
//    public override void Refresh()
//    {
//      if (control != null) control.Refresh();
//    }
//    public override SelectionType SelectionType
//    {
//      get { return SelectionType.TemporarySelection; }
//    }
//  }




//  // ListViewControl
//  public class ASPxCardListViewControl : ASPxCardView, IXafCallbackHandler
//  {
//    private Dictionary<string, System.Drawing.Image> images = new Dictionary<string, System.Drawing.Image>();
//    public event EventHandler<CardListEditorClickEventArgs> OnClick;
//    private void RaiseItemClick(IPictureItem item)
//    {
//      ASPxCardView aSPxCard = new ASPxCardView();
//      if (OnClick != null)
//      {
//        CardListEditorClickEventArgs args = new CardListEditorClickEventArgs();
//        args.ItemClicked = item;
//        OnClick(this, args);
//      }
//    }
//    private IPictureItem FindItemByID(string ID)
//    {
//      if (DataSource == null)
//        return null;
//      IList dataSource = ListHelper.GetList(DataSource);
//      foreach (IPictureItem item in dataSource)
//      {
//        if (item.ID == ID)
//          return item;
//      }
//      return null;
//    }
//    private XafCallbackManager CallbackManager
//    {
//      get { return Page != null ? ((ICallbackManagerHolder)Page).CallbackManager : null; }
//    }
//    protected override void OnInit(EventArgs e)
//    {
//      base.OnInit(e);
//      Refresh();
//    }
//    public void Refresh()
//    {
//      //Columns.Add(new CardViewColumn("Title"));
//      //Columns.Add(new CardViewBinaryImageColumn("Image"));
//      Columns.Clear();
//      Columns.Add(new CardViewColumn("Title"));
//      Columns.Add(new CardViewBinaryImageColumn("Image"));
//      DataBind();
//    }

//    public void ProcessAction(string parameter)
//    {
//      IPictureItem item = FindItemByID(parameter);
//      if (item != null)
//      {
//        RaiseItemClick(item);
//      }
//    }
//  }
//}
