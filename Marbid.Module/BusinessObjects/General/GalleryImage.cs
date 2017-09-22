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
using Marbid.Module.CustomInterface;
using System.Drawing;
using System.IO;
using DevExpress.Xpo.Metadata;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Marbid.Module.BusinessObjects.General
{
   [DefaultClassOptions]
   [NavigationItem(false)]
   [CreatableItem(false)]
   [ImageName("image")]
   [DefaultProperty("Title")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class GalleryImage : BaseObject, IPictureItem
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public GalleryImage(Session session)
          : base(session)
      {
      }
      public override void AfterConstruction()
      {
         base.AfterConstruction();
         // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
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
      Gallery gallery;
      [Association("Gallery-Images")]
      public Gallery Gallery
      {
         get
         {
            return gallery;
         }
         set
         {
            SetPropertyValue("Gallery", ref gallery, value);
         }
      }
      string title;
      [Size(SizeAttribute.Unlimited)]
      [ModelDefault("RowCount", "1")]
      [RuleRequiredField]
      public string Title
      {
         get
         {
            return title;
         }
         set
         {
            SetPropertyValue("Title", ref title, value);
         }
      }

      Image image;
      [RuleRequiredField]
      [Size(SizeAttribute.Unlimited), ValueConverter(typeof(ImageValueConverter))]
      [VisibleInListView(false)]
      public Image Image
      {
         get
         {
            return image;
         }
         set
         {
            SetPropertyValue("Image", ref image, value);
         }
      }
      [VisibleInDetailView(false), VisibleInListView(true)]
      [Persistent]
      [Size(SizeAttribute.Unlimited), ValueConverter(typeof(ImageValueConverter))]
      Image thumbnail;
      public Image Thumbnail
      {
         get
         {
            return thumbnail;
         }
         set
         {
            SetPropertyValue("Thumbnail", ref thumbnail, value);
         }
      }
      public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
      {
         var ratioX = (double)maxWidth / image.Width;
         var ratioY = (double)maxHeight / image.Height;
         var ratio = Math.Min(ratioX, ratioY);

         var newWidth = (int)(image.Width * ratio);
         var newHeight = (int)(image.Height * ratio);

         var newImage = new Bitmap(newWidth, newHeight);

         using (var graphics = Graphics.FromImage(newImage))
            graphics.DrawImage(image, 0, 0, newWidth, newHeight);

         return newImage;
      }


      Image IPictureItem.Image
      {
         get { return Thumbnail; }
      }
      string IPictureItem.Text
      {
         get { return Title; }
      }
      string IPictureItem.ID
      {
         get { return Oid.ToString(); }
      }
   }
}