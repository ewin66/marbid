using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Editors;
using System;
using DevExpress.ExpressApp.Model;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Pdf;
using System.Collections.Generic;
using System.Web.UI;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;

namespace Marbid.Module.Web.Editors
{
   [PropertyEditor(typeof(FileData), false)]
   public class PdfPropertyEditor : ASPxPropertyEditor
   {
      public PdfPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model)
      {
         DocumentProcessor = new PdfDocumentProcessor();
      }

      public ASPxDataView DataView { get; private set; }

      private PdfDocumentProcessor DocumentProcessor { get; set; }

      protected override WebControl CreateViewModeControlCore()
      {
         DataView = new ASPxDataView();
         DataView.SettingsTableLayout.ColumnCount = 1;
         DataView.SettingsTableLayout.RowsPerPage = 1;
         DataView.PagerSettings.ShowNumericButtons = true;
         DataView.PagerSettings.AllButton.Visible = true;
         DataView.ItemStyle.Paddings.Padding = new Unit(0, UnitType.Pixel);
         DataView.ItemTemplate = new DocumentItemTemplate(this);
         DataView.Width = Unit.Percentage(100);
         return DataView;
      }

      protected override WebControl CreateEditModeControlCore()
      {
         return new Panel();
      }

      protected override void ReadViewModeValueCore()
      {
         IFileData fileData = PropertyValue as IFileData;
         if (fileData != null && fileData.FileName.ToLower().Contains(".pdf"))
         {
            using (MemoryStream stream = new MemoryStream())
            {
               fileData.SaveToStream(stream);
               //LoadDocument(stream);
               DocumentProcessor.LoadDocument(stream, true);
               BindDataView();
            }
         }
      }

      private void BindDataView()
      {
         if (DocumentProcessor.Document != null)
         {
            List<PdfPageItem> data = new List<PdfPageItem>();
            for (int pageNumber = 1; pageNumber <= DocumentProcessor.Document.Pages.Count; pageNumber++)
            {
               data.Add(new PdfPageItem()
               {
                  PageNumber = pageNumber
               });
            }
            DataView.DataSource = data;
            DataView.DataBind();
         }
      }

      void image_DataBinding(object sender, EventArgs e)
      {
         ASPxBinaryImage image = sender as ASPxBinaryImage;
         DataViewItemTemplateContainer container = image.NamingContainer as DataViewItemTemplateContainer;
         int pageNumber = (int)container.EvalDataItem("PageNumber");

         using (Bitmap bitmap = DocumentProcessor.CreateBitmap(pageNumber, 900))
         {
            using (MemoryStream stream = new MemoryStream())
            {
               bitmap.Save(stream, ImageFormat.Png);
               image.ContentBytes = stream.ToArray();
            }
         }
      }

      private class PdfPageItem
      {
         public int PageNumber { get; set; }
      }

      private class DocumentItemTemplate : ITemplate
      {
         private PdfPropertyEditor Owner;

         public DocumentItemTemplate(PdfPropertyEditor owner)
         {
            this.Owner = owner;
         }

         #region ITemplate Members

         void ITemplate.InstantiateIn(Control container)
         {
            var image = new ASPxBinaryImage();
            image.DataBinding += Owner.image_DataBinding;
            container.Controls.Add(image);
         }
         #endregion
      }

   }
}