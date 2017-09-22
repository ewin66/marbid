using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Marbid.Module.BusinessObjects.General;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.Persistent.BaseImpl;
using MetadataExtractor;

namespace Marbid.Module.Win.Controllers
{
   // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
   public partial class GalleryUpdloadControllerWin : ViewController
   {
      public GalleryUpdloadControllerWin()
      {
         InitializeComponent();
         // Target required Views (via the TargetXXX properties) and create their Actions.
         TargetObjectType = typeof(PhotoGallery);
         TargetViewType = ViewType.DetailView;
      }
      protected override void OnActivated()
      {
         base.OnActivated();
         // Perform various tasks depending on the target View.
      }
      protected override void OnViewControlsCreated()
      {
         base.OnViewControlsCreated();
         // Access and customize the target View control.
      }
      protected override void OnDeactivated()
      {
         // Unsubscribe from previously subscribed events and release other references and resources.
         base.OnDeactivated();
      }

      private void UploadFolderAction_Execute(object sender, SimpleActionExecuteEventArgs e)
      {
         FolderBrowserDialog browserDialog = new FolderBrowserDialog();
         if (browserDialog.ShowDialog() == DialogResult.OK)
         {
            DirectoryInfo directory = new DirectoryInfo(browserDialog.SelectedPath);
            foreach (FileInfo file in directory.GetFiles("*.jpg", SearchOption.TopDirectoryOnly))
            {
               //GalleryImage image = ObjectSpace.CreateObject<GalleryImage>();
               //image.Title = file.Name;
               //image.Image = Image.FromFile(file.FullName);
               //image.Thumbnail = GalleryImage.ScaleImage(image.Image, 200, 100);
               //image.Gallery = (Gallery)View.CurrentObject;
               //ObjectSpace.CommitChanges();
               //View.ObjectSpace.CommitChanges();
               InsertPhoto(file);
               //InsertImage(file);
            }
         }

         if (View is DetailView && ((DetailView)View).ViewEditMode == ViewEditMode.View)
         {
            View.ObjectSpace.CommitChanges();
         }
      }

      private void InsertImage (FileInfo file)
      {
         GalleryImage image = ObjectSpace.CreateObject<GalleryImage>();
         image.Title = file.Name;
         image.Image = Image.FromFile(file.FullName);
         image.Thumbnail = Marbid.Module.CustomCodes.CodeLibrary.ScaleImage(image.Image, 200, 100);
         image.Gallery = (Gallery)View.CurrentObject;
         View.ObjectSpace.CommitChanges();
      }

      private void InsertPhoto(FileInfo file)
      {
         Photo photo = ObjectSpace.CreateObject<Photo>();
         Image tempImage = Image.FromFile(file.FullName);
         photo.Title = file.Name;
         MediaDataObject media = ObjectSpace.CreateObject<MediaDataObject>();
         media.MediaData = ImageToByteArray(tempImage);
         photo.Image = media;
         photo.Thumbnail = Marbid.Module.CustomCodes.CodeLibrary.ScaleImage(tempImage, 250, 150);
         IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(file.FullName);
         foreach (var directory in directories)
            foreach (var tag in directory.Tags)
               photo.MetaData += string.Format("<strong>{0} - {1}:</strong> {2}<br/>", tag.DirectoryName, tag.Name, tag.Description);
         photo.Gallery = (PhotoGallery)View.CurrentObject;
         View.ObjectSpace.CommitChanges();
      }

      public byte[] ImageToByteArray(System.Drawing.Image imageIn)
      {
         using (var ms = new MemoryStream())
         {
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
         }
      }
   }
}
