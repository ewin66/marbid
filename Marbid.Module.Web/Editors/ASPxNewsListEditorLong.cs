using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Web;
using Marbid.Module.BusinessObjects.CRM;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp;
using System.Collections;
using System.Web.UI;
using DevExpress.ExpressApp.Web.Templates;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.ComponentModel;
using System.Data;

namespace Marbid.Module.Web.Editors
{
    public class ASPxNewsListEditorControlLong : Panel, INamingContainer, IXafCallbackHandler
    {
        private IList dataSource;
        private Dictionary<string, System.Drawing.Image> images = new Dictionary<string, System.Drawing.Image>();
        private XafCallbackManager CallbackManager
        {
            get { return Page != null ? ((ICallbackManagerHolder)Page).CallbackManager : null; }
        }
        private void ImageResourceHttpHandler_QueryImageInfo(object sender, ImageInfoEventArgs e)
        {
            if (e.Url.StartsWith("CLE"))
            {
                lock (images)
                {
                    if (images.ContainsKey(e.Url))
                    {
                        System.Drawing.Image image = images[e.Url];
                        e.ImageInfo = new DevExpress.ExpressApp.Utils.ImageInfo("", image, "");
                        images.Remove(e.Url);
                    }
                }
            }
        }
        public ASPxNewsListEditorControlLong()
        {
            ImageResourceHttpHandler.QueryImageInfo += new EventHandler<ImageInfoEventArgs>(ImageResourceHttpHandler_QueryImageInfo);
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Refresh();
        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Refresh();
        }

        private INewsItem FindItemByID(string ID)
        {
            if (dataSource == null)
                return null;

            foreach (INewsItem item in dataSource)
            {
                if (item.ID == ID)
                    return item;
            }
            return null;
        }

        public void Refresh()
        {
            this.Controls.Clear();
            if (Page != null)
            {
                //ArrayList list = new ArrayList(dataSource);
                //IEnumerable<INewsItem> list = new BindingList<INewsItem>();
                //foreach (INewsItem item in list)
                //{
                //    //string imageUrl = null;
                //    //if (item.CoverImage != null)
                //    //{
                //    //    string imageKey = "CLE_" + WebImageHelper.GetImageHash(item.CoverImage);
                //    //    imageUrl = ImageResourceHttpHandler.GetWebResourceUrl(imageKey);
                //    //    if (!images.ContainsKey(imageKey))
                //    //    {
                //    //        images.Add(imageKey, item.CoverImage);
                //    //    }
                //    //}

                //    string imageUrl = null;

                //    if (item.CoverImage != null)
                //    {
                //        System.Drawing.Image tempImage;
                //        byte[] image = null;
                //        using (var msi = new MemoryStream(item.CoverImage.MediaData))
                //        {
                //            tempImage = Marbid.Module.BusinessObjects.General.GalleryImage.ScaleImage(System.Drawing.Image.FromStream(msi), 100, 100);
                //        }

                //        MemoryStream ms = new MemoryStream();
                //        tempImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                //        image = ms.ToArray();
                //        string base64String = Convert.ToBase64String(image, 0, image.Length);
                //        imageUrl = "data:image/png;base64," + base64String;
                //    }

                //    string itemText = null;
                //    if (item.TextContent != null)
                //    {
                //        itemText = Regex.Replace(item.TextContent, @"<[^>]+>|&nbsp;", "").Trim();
                //    }
                //    NewsItem newsItem = new NewsItem(item.Title, itemText, item.URL, item.ID, imageUrl, item.Date);
                //    control.Items.Add(item.Title, itemText, item.URL, item.ID, imageUrl, item.Date);
                //}
                ASPxNewsControl control = new ASPxNewsControl();
                this.Controls.Add(control);

                IList<INewsItem> list = new List<INewsItem>();
                foreach (INewsItem item in dataSource)
                {
                    NewsItem newsItem = new NewsItem();
                    newsItem.Date = item.Date;
                    newsItem.HeaderText = item.Title;
                    newsItem.NavigateUrl = item.URL;
                    newsItem.Text = item.TextContent;
                    newsItem.Image.Url = item.CoverImage;
                    control.Items.Add(newsItem);
                }

                //control.DataSource = dataSource;

                //control.HeaderTextField = "{Binding Path=(CRM:INewsItem.Title)}";
                //control.NavigateUrlField = "URL";
                //control.ImageUrlField = "ImageURL";
                //control.TextField = "{Binding Path=(CRM:INewsItem.TextContent)}";
                control.ItemSettings.ImagePosition = HeadlineImagePosition.Left;
                control.ItemSettings.ShowImageAsLink = true;
                control.ItemSettings.ShowHeaderAsLink = true;
                control.EncodeHtml = true;
                control.ItemSettings.MaxLength = 350;
                control.ItemSettings.TailText = "Read More";
                control.ItemSettings.TailPosition = TailPosition.Inline;
                control.Theme = "Metropolis";
                control.ItemSettings.DateVerticalPosition = DateVerticalPosition.Header;
                control.RowPerPage = 10;
            }
        }

        #region IXafCallbackHandler Members
        public void ProcessAction(string parameter)
        {
            INewsItem item = FindItemByID(parameter);
            if (item != null)
            {
                //RaiseItemClick(item);
            }
        }
        #endregion
        //public event EventHandler<CustomListEditorClickEventArgs> OnClick;
        //private void RaiseItemClick(INewsItem item)
        //{
        //    if (OnClick != null)
        //    {
        //        NewsItemEventArgs args = new NewsItemEventArgs()
        //        OnClick(this, args);
        //    }
        //}

        public IList DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }
    }

    [ListEditor(typeof(INewsItem))]
    public class ASPxNewsListEditorLong : ListEditor
    {
        public ASPxNewsListEditorLong(IModelListView info) : base(info) { }
        private ASPxNewsListEditorControlLong control;
        protected override object CreateControlsCore()
        {
            control = new ASPxNewsListEditorControlLong();
            control.ID = "CustomListEditor_control";
            return control;
        }
        protected override void AssignDataSourceToControl(Object dataSource)
        {
            if (control != null)
            {
                IList ds = ListHelper.GetBindingList(dataSource);
                control.DataSource = ds;
            }
        }

        public override void Refresh()
        {
            if (control != null) control.Refresh();
        }

        public override void BreakLinksToControls()
        {
            control = null;
            base.BreakLinksToControls();
        }
        public override SelectionType SelectionType
        {
            get
            {
                return SelectionType.None;
            }
        }
        protected override void OnSelectionChanged()
        {
            base.OnSelectionChanged();
        }
        public override IList GetSelectedObjects()
        {
            List<object> selectedObjects = new List<object>();
            //if (FocusedObject != null)
            //{
            //    selectedObjects.Add(FocusedObject);
            //}
            return selectedObjects;
        }
    }
}
