using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.SystemModule;
using System.Web.UI;
using DevExpress.ExpressApp.Templates;
using Marbid.Module.BusinessObjects.Administration;
using DevExpress.Data.Filtering;

namespace Marbid.Module.Web.Controllers
{
   public interface IHintPlaceHolderWeb : IFrameTemplate
   {
      Control HintPlaceHolder
      {
         get;
      }
   }
   public class InfoPanelViewControllerWeb : CustomizeTemplateViewControllerBase<IHintPlaceHolderWeb>
   {
      LiteralControl literal;
      protected override void AddControlsToTemplateCore(IHintPlaceHolderWeb template)
      {
         if (literal == null) literal = new LiteralControl();
         if (template.HintPlaceHolder != null)
         {
            template.HintPlaceHolder.Controls.Add(literal);
         }
      }
      protected override void RemoveControlsFromTemplateCore(IHintPlaceHolderWeb template)
      {
         if (template.HintPlaceHolder != null)
         {
            template.HintPlaceHolder.Controls.Remove(literal);
            literal = null;
         }
      }
      protected override void UpdateControls(View view)
      {
         UpdateControls();
      }
      protected override void UpdateControls(object currentObject)
      {
         UpdateControls();
      }
      void UpdateControls()
      {
         literal.Text = "";
         literal.Visible = false;
         IObjectSpace objectSpace = Application.CreateObjectSpace();
         ViewItemHints hints = objectSpace.FindObject<ViewItemHints>(CriteriaOperator.Parse("ViewID = ?", View.Id));
         if (hints != null)
         {
            literal.Text += hints.Hint;
            
            literal.Visible = true;
         }
      }
   }
}

