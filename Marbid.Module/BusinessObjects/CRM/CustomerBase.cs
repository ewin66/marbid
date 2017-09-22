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
using Marbid.Module.CustomCodes;
using Marbid.Module.BusinessObjects.Administration;
namespace Marbid.Module.BusinessObjects.CRM
{
  [DefaultClassOptions]
  [DefaultProperty("DisplayName")]
  [NavigationItem(false)]
  public class CustomerBase : BaseObject
  {
    private System.String _faximile2;
    private System.String _faximile1;
    private System.String _phone2;
    private System.String _phone1;
    private System.String _postalCode;
    private System.String _website;
    private System.String _email;
    private System.String _linkedIn;
    private System.String _twitter;
    private System.String _facebook;
    private Marbid.Module.BusinessObjects.Administration.Employee _lastUpdateBy;
    private System.DateTime _lastUpdate;
    private MediaDataObject _logo;
    private Marbid.Module.BusinessObjects.Administration.SubRegion _subRegion;
    private Marbid.Module.BusinessObjects.Administration.Region _region;
    private Marbid.Module.BusinessObjects.Administration.Province _province;
    private System.String _address;
    private System.DateTime _anniversary;
    private Marbid.Module.BusinessObjects.Administration.Employee _createdBy;
    private System.DateTime _createDate;
    private System.String _displayName;
    public CustomerBase(DevExpress.Xpo.Session session)
      : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      CreateDate = DateTime.Now;
      CreatedBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
    }
    protected override void OnSaving()
    {
      base.OnSaving();
      LastUpdateBy = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
      LastUpdate = DateTime.Now;
    }
    public System.String DisplayName
    {
      get
      {
        return _displayName;
      }
      set
      {
        SetPropertyValue("DisplayName", ref _displayName, value);
      }
    }
    public System.DateTime CreateDate
    {
      get
      {
        return _createDate;
      }
      set
      {
        SetPropertyValue("CreateDate", ref _createDate, value);
      }
    }
    public Marbid.Module.BusinessObjects.Administration.Employee CreatedBy
    {
      get
      {
        return _createdBy;
      }
      set
      {
        SetPropertyValue("CreatedBy", ref _createdBy, value);
      }
    }
    public Marbid.Module.BusinessObjects.Administration.Employee LastUpdateBy
    {
      get
      {
        return _lastUpdateBy;
      }
      set
      {
        SetPropertyValue("LastUpdateBy", ref _lastUpdateBy, value);
      }
    }
    public System.DateTime LastUpdate
    {
      get
      {
        return _lastUpdate;
      }
      set
      {
        SetPropertyValue("LastUpdate", ref _lastUpdate, value);
      }
    }
    public System.DateTime Anniversary
    {
      get
      {
        return _anniversary;
      }
      set
      {
        SetPropertyValue("Anniversary", ref _anniversary, value);
      }
    }
    [DevExpress.Xpo.SizeAttribute(1000)]
    public System.String Address
    {
      get
      {
        return _address;
      }
      set
      {
        SetPropertyValue("Address", ref _address, value);
      }
    }
    [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
    public Marbid.Module.BusinessObjects.Administration.Province Province
    {
      get
      {
        return _province;
      }
      set
      {
        SetPropertyValue("Province", ref _province, value);
      }
    }
    [DevExpress.Persistent.Base.DataSourcePropertyAttribute("Province.Regions")]
    [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
    public Marbid.Module.BusinessObjects.Administration.Region Region
    {
      get
      {
        return _region;
      }
      set
      {
        SetPropertyValue("Region", ref _region, value);
      }
    }
    [DevExpress.Persistent.Base.DataSourcePropertyAttribute("Region.SubRegions")]
    public Marbid.Module.BusinessObjects.Administration.SubRegion SubRegion
    {
      get
      {
        return _subRegion;
      }
      set
      {
        SetPropertyValue("SubRegion", ref _subRegion, value);
      }
    }
    [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorFixedHeight = 200, DetailViewImageEditorFixedWidth = 200)]
    public MediaDataObject Logo
    {
      get { return _logo; }
      set { SetPropertyValue("Photo", ref _logo, value); }
    }
    [DevExpress.Xpo.SizeAttribute(200)]
    public System.String Facebook
    {
      get
      {
        return _facebook;
      }
      set
      {
        SetPropertyValue("Facebook", ref _facebook, value);
      }
    }
    public System.String Twitter
    {
      get
      {
        return _twitter;
      }
      set
      {
        SetPropertyValue("Twitter", ref _twitter, value);
      }
    }
    public System.String LinkedIn
    {
      get
      {
        return _linkedIn;
      }
      set
      {
        SetPropertyValue("LinkedIn", ref _linkedIn, value);
      }
    }
    public System.String Email
    {
      get
      {
        return _email;
      }
      set
      {
        SetPropertyValue("Email", ref _email, value);
      }
    }
    public System.String Website
    {
      get
      {
        return _website;
      }
      set
      {
        SetPropertyValue("Website", ref _website, value);
      }
    }
    [DevExpress.Xpo.AssociationAttribute("Notes-Customer")]
    [DevExpress.Xpo.AggregatedAttribute]
    public XPCollection<Marbid.Module.BusinessObjects.CRM.CustomerNote> Notes
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.CRM.CustomerNote>("Notes");
      }
    }
    [Association("Documents-Customer"), DevExpress.Xpo.Aggregated]
    public XPCollection<Marbid.Module.BusinessObjects.CRM.CustomerDocument> Documents
    {
      get
      {
        return GetCollection<Marbid.Module.BusinessObjects.CRM.CustomerDocument>("Documents");
      }
    }
    public System.String PostalCode
    {
      get
      {
        return _postalCode;
      }
      set
      {
        SetPropertyValue("PostalCode", ref _postalCode, value);
      }
    }
    public System.String Phone1
    {
      get
      {
        return _phone1;
      }
      set
      {
        SetPropertyValue("Phone1", ref _phone1, value);
      }
    }
    public System.String Phone2
    {
      get
      {
        return _phone2;
      }
      set
      {
        SetPropertyValue("Phone2", ref _phone2, value);
      }
    }
    public System.String Faximile1
    {
      get
      {
        return _faximile1;
      }
      set
      {
        SetPropertyValue("Faximile1", ref _faximile1, value);
      }
    }
    public System.String Faximile2
    {
      get
      {
        return _faximile2;
      }
      set
      {
        SetPropertyValue("Faximile2", ref _faximile2, value);
      }
    }
    [NonPersistent]
    public int Age
    {
      get
      {
        if (Anniversary != null)
        {
          Calculator calc = new Calculator();
          return calc.Age(Anniversary, DateTime.Now);
        }
        else
        {
          return 0;
        }
      }
    }
  }
}
