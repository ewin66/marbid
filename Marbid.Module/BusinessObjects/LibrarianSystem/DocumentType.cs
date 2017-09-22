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
using DevExpress.ExpressApp.Editors;

namespace Marbid.Module.BusinessObjects.LibrarianSystem
{
  [DefaultClassOptions]
  [DefaultProperty("Name")]
  [NavigationItem("Librarian System")]
  public class DocumentType : BaseObject
  {
    public DocumentType(Session session)
        : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
    }
    // Fields...
    private string _Description;
    private string _Name;

    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    public string Name
    {
      get
      {
        return _Name;
      }
      set
      {
        SetPropertyValue("Name", ref _Name, value);
      }
    }

    

    [Size(SizeAttribute.Unlimited)]
    [EditorAlias(EditorAliases.HtmlPropertyEditor)]
    public string Description
    {
      get
      {
        return _Description;
      }
      set
      {
        SetPropertyValue("Description", ref _Description, value);
      }
    }
  }
}