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
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using Marbid.Module.BusinessObjects.CRM;

namespace Marbid.Module.BusinessObjects.Administration
{
  [NavigationItem(false), CreatableItem(false), DefaultProperty("FullName")]
  [CurrentUserDisplayImage("Photo")]
  [ImageName("BO_Contact")]
  public class People : BaseObject
  { 
    public People(Session session)
        : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
    }
    private Marbid.Module.BusinessObjects.MaritalStatus _maritalStatus;
    private Marbid.Module.BusinessObjects.Religion _religion;
    private System.String _homePhone;
    private Marbid.Module.BusinessObjects.Administration.SubRegion _subRegion;
    private Marbid.Module.BusinessObjects.Administration.Region _region;
    private Marbid.Module.BusinessObjects.Administration.Province _province;
    private System.String _address;
    private MediaDataObject _photo;
    private Marbid.Module.BusinessObjects.Gender _gender;
    private System.String _personalEmail;
    private System.String _corporateEmail;
    private System.String _mobilePhone2;
    private System.String _mobilePhone1;
    private System.String _officeExtension;
    private System.DateTime _birthDate;
    private System.String _lastName;
    private System.String _firstName;

    //[Persistent]
    [PersistentAlias("Concat(IsNull([FirstName],''), ' ', IsNull([LastName],''))")]
    public string FullName
    {
      get
      {
        //return ObjectFormatter.Format(displayNameFormat, this, EmptyEntriesMode.RemoveDelimiterWhenEntryIsEmpty);
        return (string)EvaluateAlias("FullName");
      }
    }
    [RuleRequiredField]
    [VisibleInLookupListView(false)]
    public System.String FirstName
    {
      get
      {
        return _firstName;
      }
      set
      {
        SetPropertyValue("FirstName", ref _firstName, value);
      }
    }
    [VisibleInLookupListView(false)]
    public System.String LastName
    {
      get
      {
        return _lastName;
      }
      set
      {
        SetPropertyValue("LastName", ref _lastName, value);
      }
    }
    [VisibleInLookupListView(false)]
    public System.DateTime BirthDate
    {
      get
      {
        return _birthDate;
      }
      set
      {
        SetPropertyValue("BirthDate", ref _birthDate, value);
      }
    }

    [VisibleInLookupListView(false)]
    public System.String MobilePhone1
    {
      get
      {
        return _mobilePhone1;
      }
      set
      {
        SetPropertyValue("MobilePhone1", ref _mobilePhone1, value);
      }
    }
    [VisibleInLookupListView(false)]
    public System.String MobilePhone2
    {
      get
      {
        return _mobilePhone2;
      }
      set
      {
        SetPropertyValue("MobilePhone2", ref _mobilePhone2, value);
      }
    }
    [VisibleInLookupListView(false)]
    [RuleRegularExpression("", DefaultContexts.Save, @"(((http|https|ftp)\://)?[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;amp;%\$#\=~])*)|([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6})")]
    public System.String CorporateEmail
    {
      get
      {
        return _corporateEmail;
      }
      set
      {
        SetPropertyValue("CorporateEmail", ref _corporateEmail, value);
      }
    }
    [RuleRegularExpression("", DefaultContexts.Save, @"(((http|https|ftp)\://)?[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;amp;%\$#\=~])*)|([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6})")]
    [VisibleInLookupListView(false)]
    public System.String PersonalEmail
    {
      get
      {
        return _personalEmail;
      }
      set
      {
        SetPropertyValue("PersonalEmail", ref _personalEmail, value);
      }
    }

    [DevExpress.Xpo.SizeAttribute(5)]
    [VisibleInLookupListView(false)]
    public System.String OfficeExtension
    {
      get
      {
        return _officeExtension;
      }
      set
      {
        SetPropertyValue("OfficeExtension", ref _officeExtension, value);
      }
    }
    [RuleRequiredField]
    [VisibleInLookupListView(false)]
    public Marbid.Module.BusinessObjects.Gender Gender
    {
      get
      {
        return _gender;
      }
      set
      {
        SetPropertyValue("Gender", ref _gender, value);
      }
    }
    [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorFixedHeight = 200, DetailViewImageEditorFixedWidth = 200)]
    public MediaDataObject Photo
    {
      get { return _photo; }
      set { SetPropertyValue("Photo", ref _photo, value); }
    }

    [Size(SizeAttribute.Unlimited)]
    [VisibleInLookupListView(false)]
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
    [VisibleInLookupListView(false)]
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
    [DevExpress.Persistent.Base.ImmediatePostDataAttribute]
    [DevExpress.Persistent.Base.DataSourcePropertyAttribute("Province.Regions")]
    [VisibleInLookupListView(false)]
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
    [VisibleInLookupListView(false)]
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

    [VisibleInLookupListView(false)]
    public System.String HomePhone
    {
      get
      {
        return _homePhone;
      }
      set
      {
        SetPropertyValue("HomePhone", ref _homePhone, value);
      }
    }
    [RuleRequiredField(TargetCriteria = "Religion > 0")]
    [VisibleInLookupListView(false)]
    public Marbid.Module.BusinessObjects.Religion Religion
    {
      get
      {
        return _religion;
      }
      set
      {
        SetPropertyValue("Religion", ref _religion, value);
      }
    }
    [RuleRequiredField(TargetCriteria = "MaritalStatus > 0")]
    [VisibleInLookupListView(false)]
    public Marbid.Module.BusinessObjects.MaritalStatus MaritalStatus
    {
      get
      {
        return _maritalStatus;
      }
      set
      {
        SetPropertyValue("MaritalStatus", ref _maritalStatus, value);
      }
    }

    string placeOfBirth;
    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    public string PlaceOfBirth
    {
      get
      {
        return placeOfBirth;
      }
      set
      {
        SetPropertyValue("PlaceOfBirth", ref placeOfBirth, value);
      }
    }

    [NonPersistent]
    [VisibleInDetailView(false), VisibleInListView(false)]
    public string UserType
    {
      get
      {
        return this.GetType().Name;
      }
    }
    private XPCollection<Schedule> schedules;
    public XPCollection<Schedule> Schedules
    {
      get
      {
        if (schedules == null)
        {
          schedules = new XPCollection<Schedule>(Session);
          schedules.Criteria = CriteriaOperator.Parse("[ScheduleParticipants][[Participant.Oid] = ?]", Oid);
        }
        return schedules;
      }
    }
    string position;
    [Size(SizeAttribute.DefaultStringMappingFieldSize)]
    public string Position
    {
      get
      {
        return position;
      }
      set
      {
        SetPropertyValue("Position", ref position, value);
      }
    }
  }
}