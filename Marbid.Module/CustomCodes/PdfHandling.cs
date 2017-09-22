//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
using DevExpress.Pdf;
using DevExpress.Pdf.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marbid.Module.CustomCodes
{
   public class PdfHandling : IDisposable
   {
      PdfDocumentProcessor processor = new PdfDocumentProcessor();
      public PdfHandling(MemoryStream memoryStream)
      {
         processor.LoadDocument(memoryStream);
      }

      public int PageCount
      {
         get
         {
            return processor.Document.Pages.Count;
         }
      }

      public string DocumentText
      {
         get
         {
            return processor.Text;
         }
      }

      public void Dispose()
      {
         if (processor != null)
            processor = null;
      }
   }
}
