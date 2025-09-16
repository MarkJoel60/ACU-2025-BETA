// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.CRReferenceAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public sealed class CRReferenceAttribute : PXViewExtensionAttribute
{
  private readonly BqlCommand _bAccountCommand;
  private readonly BqlCommand _contactCommand;
  private PXView _bAccountView;
  private PXView _contactView;

  private string BAccountRefFieldName
  {
    get
    {
      return !(this.BAccountRefField != (System.Type) null) ? EntityHelper.GetIDField(this._bAccountView.Cache) : this.BAccountRefField.Name;
    }
  }

  public System.Type BAccountRefField { get; set; }

  private string ContactRefFieldName
  {
    get
    {
      return !(this.ContactRefField != (System.Type) null) ? EntityHelper.GetIDField(this._contactView.Cache) : this.ContactRefField.Name;
    }
  }

  public System.Type ContactRefField { get; set; }

  public bool Persistent { get; set; }

  public CRReferenceAttribute(System.Type bAccountSelect, System.Type contactSelect = null)
  {
    this.Persistent = false;
    if (bAccountSelect == (System.Type) null)
      throw new ArgumentNullException(nameof (bAccountSelect));
    if (typeof (IBqlSelect).IsAssignableFrom(bAccountSelect))
      this._bAccountCommand = BqlCommand.CreateInstance(new System.Type[1]
      {
        bAccountSelect
      });
    else
      this.BAccountRefField = typeof (IBqlField).IsAssignableFrom(bAccountSelect) ? bAccountSelect : throw new PXArgumentException("sel", PXMessages.LocalizeFormatNoPrefixNLA("The {0} select expression is incorrect.", new object[1]
      {
        (object) bAccountSelect.Name
      }));
    if (contactSelect != (System.Type) null && typeof (IBqlSelect).IsAssignableFrom(contactSelect))
    {
      this._contactCommand = BqlCommand.CreateInstance(new System.Type[1]
      {
        contactSelect
      });
    }
    else
    {
      if (!typeof (IBqlField).IsAssignableFrom(contactSelect))
        return;
      this.ContactRefField = contactSelect;
    }
  }

  public virtual void ViewCreated(PXGraph graph, string viewName)
  {
    if (this._bAccountCommand != null)
      this._bAccountView = new PXView(graph, true, this._bAccountCommand);
    // ISSUE: method pointer
    graph.FieldDefaulting.AddHandler<CRActivity.bAccountID>(new PXFieldDefaulting((object) this, __methodptr(BAccountID_FieldDefaulting)));
    // ISSUE: method pointer
    graph.FieldDefaulting.AddHandler<CRPMTimeActivity.bAccountID>(new PXFieldDefaulting((object) this, __methodptr(BAccountID_FieldDefaulting)));
    // ISSUE: method pointer
    graph.FieldDefaulting.AddHandler<CRSMEmail.bAccountID>(new PXFieldDefaulting((object) this, __methodptr(BAccountID_FieldDefaulting)));
    if (this._contactCommand != null || this.ContactRefField != (System.Type) null)
    {
      if (this._contactCommand != null)
        this._contactView = new PXView(graph, true, this._contactCommand);
      // ISSUE: method pointer
      graph.FieldDefaulting.AddHandler<CRActivity.contactID>(new PXFieldDefaulting((object) this, __methodptr(ContactID_FieldDefaulting)));
      // ISSUE: method pointer
      graph.FieldDefaulting.AddHandler<CRPMTimeActivity.contactID>(new PXFieldDefaulting((object) this, __methodptr(ContactID_FieldDefaulting)));
      // ISSUE: method pointer
      graph.FieldDefaulting.AddHandler<CRSMEmail.contactID>(new PXFieldDefaulting((object) this, __methodptr(ContactID_FieldDefaulting)));
    }
    if (!this.Persistent)
      return;
    graph.Views.Caches.Remove(typeof (CRActivity));
    graph.Views.Caches.Add(typeof (CRActivity));
    // ISSUE: method pointer
    graph.RowPersisting.AddHandler(typeof (CRActivity), new PXRowPersisting((object) this, __methodptr(RowPersisting)));
    graph.Views.Caches.Remove(typeof (CRPMTimeActivity));
    graph.Views.Caches.Add(typeof (CRPMTimeActivity));
    // ISSUE: method pointer
    graph.RowPersisting.AddHandler(typeof (CRPMTimeActivity), new PXRowPersisting((object) this, __methodptr(RowPersisting)));
    graph.Views.Caches.Remove(typeof (CRSMEmail));
    graph.Views.Caches.Add(typeof (CRSMEmail));
    // ISSUE: method pointer
    graph.RowPersisting.AddHandler(typeof (CRSMEmail), new PXRowPersisting((object) this, __methodptr(RowPersisting)));
  }

  private int? GetBAccIDRef(PXCache sender)
  {
    if (this._bAccountView != null)
    {
      object record = this._bAccountView.SelectSingle(Array.Empty<object>());
      return CRReferenceAttribute.GetRecordValue(sender, record, this.BAccountRefFieldName);
    }
    return this.BAccountRefField != (System.Type) null ? CRReferenceAttribute.CetCurrentValue(sender, this.BAccountRefField) : new int?();
  }

  private int? GetContactIDRef(PXCache sender)
  {
    if (this._contactView != null)
    {
      object record = this._contactView.SelectSingle(Array.Empty<object>());
      return CRReferenceAttribute.GetRecordValue(sender, record, this.ContactRefFieldName);
    }
    return this.ContactRefFieldName != null ? CRReferenceAttribute.CetCurrentValue(sender, this.ContactRefField) : new int?();
  }

  private static int? GetRecordValue(PXCache sender, object record, string field)
  {
    return record == null ? new int?() : (int?) sender.Graph.Caches[record.GetType()].GetValue(record, field);
  }

  private static int? CetCurrentValue(PXCache sender, System.Type field)
  {
    if (field == (System.Type) null)
      return new int?();
    PXCache cach = sender.Graph.Caches[field.DeclaringType];
    return cach.Current == null ? new int?() : cach.GetValue(cach.Current, field.Name) as int?;
  }

  private void BAccountID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.GetBAccIDRef(sender);
  }

  private void ContactID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this.GetContactIDRef(sender);
  }

  private void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (this._bAccountView != null || this.BAccountRefField != (System.Type) null)
      sender.SetValue(e.Row, typeof (CRActivity.bAccountID).Name, (object) this.GetBAccIDRef(this._bAccountView?.Cache ?? sender.Graph.Caches[this.BAccountRefField.DeclaringType]));
    if (this._contactView == null && !(this.ContactRefField != (System.Type) null))
      return;
    sender.SetValue(e.Row, typeof (CRActivity.contactID).Name, (object) this.GetContactIDRef(this._contactView?.Cache ?? sender.Graph.Caches[this.ContactRefField.DeclaringType]));
  }
}
