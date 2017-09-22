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
using Marbid.Module.BusinessObjects.Administration;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.SystemModule;

namespace Marbid.Module.BusinessObjects.General
{
  [DefaultClassOptions]
  [ImageName("BO_Note")]
  [Appearance("NotesDisabled", TargetItems = "CreateDate,Owner", Enabled = false)]
  [FileAttachment("Attachment")]
  //[ListViewFilter("Hide System Notes", "[Owner.Oid] = CurrentUserId() or [AssignedTo.Oid] = CurrentUserId()", "All My Tasks", "Show only my tasks", 0, false)]
  //[ListViewFilter("Show System Notes", "([Owner.Oid] = CurrentUserId() or [AssignedTo.Oid] = CurrentUserId()) and [Status] <> 'Completed'", "All My Pending Tasks", "Show only my pending tasks", 1, false)]
  public class Notes : BaseObject
  {
    private System.Boolean _isSystemNote;
    private System.Boolean _isPrivate;
    private Marbid.Module.BusinessObjects.Administration.Employee _owner;
    private System.DateTime _createDate;
    private System.String _note;
    public Notes(DevExpress.Xpo.Session session)
      : base(session)
    {
    }
    public override void AfterConstruction()
    {
      base.AfterConstruction();
      Owner = Session.GetObjectByKey<Employee>(Session.GetKeyValue(SecuritySystem.CurrentUser));
      CreateDate = DateTime.Now;
      IsPrivate = true;
    }
    [Size(SizeAttribute.Unlimited)]
    [EditorAlias(EditorAliases.HtmlPropertyEditor)]
    public System.String Note
    {
      get
      {
        return _note;
      }
      set
      {
        SetPropertyValue("Note", ref _note, value);
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
    [DevExpress.Xpo.AssociationAttribute("Notes-Owner")]
    public Marbid.Module.BusinessObjects.Administration.Employee Owner
    {
      get
      {
        return _owner;
      }
      set
      {
        SetPropertyValue("Owner", ref _owner, value);
      }
    }
    [CaptionsForBoolValues("Private", "Public")]
    public System.Boolean IsPrivate
    {
      get
      {
        return _isPrivate;
      }
      set
      {
        SetPropertyValue("IsPrivate", ref _isPrivate, value);
      }
    }
    [CaptionsForBoolValues("System", "User")]
    public System.Boolean IsSystemNote
    {
      get
      {
        return _isSystemNote;
      }
      set
      {
        SetPropertyValue("IsSystemNote", ref _isSystemNote, value);
      }
    }
    FileData attachment;
    [VisibleInListView(false)]
    public FileData Attachment
    {
      get { return attachment; }
      set { SetPropertyValue("Attachment", ref attachment, value); }
    }
  }
}
