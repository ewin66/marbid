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
using DevExpress.ExpressApp.Editors;

namespace Marbid.Module.BusinessObjects.General
{
   [DefaultClassOptions]
   [CreatableItem(false)]
   [NavigationItem(false)]
   [ImageName("BO_Photo")]
   [DefaultProperty("Title")]
   //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
   //[Persistent("DatabaseTableName")]
   // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
   public class Photo : BaseObject, IPictureItem
   { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
      public Photo(Session session)
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

      string title;
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

      PhotoGallery gallery;
      [Association("Gallery-Photos")]
      public PhotoGallery Gallery
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

      MediaDataObject image;
      [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
      [ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PictureEdit, ImageSizeMode = ImageSizeMode.Zoom, DetailViewImageEditorFixedWidth = 1280)]
      public MediaDataObject Image
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
      string metaData;
      [Size(SizeAttribute.Unlimited)]
      [EditorAlias(EditorAliases.HtmlPropertyEditor)]
      public string MetaData
      {
         get
         {
            return metaData;
         }
         set
         {
            SetPropertyValue("MetaData", ref metaData, value);
         }
      }
      //MediaDataObject thumb;
      //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
      //public MediaDataObject Thumb
      //{
      //   get
      //   {
      //      return thumb;
      //   }
      //   set
      //   {
      //      SetPropertyValue("Thumb", ref thumb, value);
      //   }
      //}
      [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
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

      Image IPictureItem.Image
      {
         get
         {
            return Thumbnail;
         }
      }
      string IPictureItem.Text
      {
         get { return Title; }
      }
      string IPictureItem.ID
      {
         get { return Oid.ToString(); }
      }
      private static readonly ImageConverter _imageConverter = new ImageConverter();
      public static Bitmap GetImageFromByteArray(byte[] byteArray)
      {
         Bitmap bm = (Bitmap)_imageConverter.ConvertFrom(byteArray);

         if (bm != null && (bm.HorizontalResolution != (int)bm.HorizontalResolution ||
                            bm.VerticalResolution != (int)bm.VerticalResolution))
         {
            // Correct a strange glitch that has been observed in the test program when converting 
            //  from a PNG file image created by CopyImageToByteArray() - the dpi value "drifts" 
            //  slightly away from the nominal integer value
            bm.SetResolution((int)(bm.HorizontalResolution + 0.5f),
                             (int)(bm.VerticalResolution + 0.5f));
         }

         return bm;
      }
   }
}