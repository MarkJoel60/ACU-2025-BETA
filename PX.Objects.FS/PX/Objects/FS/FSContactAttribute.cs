// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSContactAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.FS;

public abstract class FSContactAttribute : ContactAttribute, IPXRowUpdatedSubscriber
{
  private BqlCommand _DuplicateSelect = BqlCommand.CreateInstance(new System.Type[1]
  {
    typeof (Select<FSContact, Where<FSContact.bAccountID, Equal<PX.Data.Required<FSContact.bAccountID>>, And<FSContact.bAccountContactID, Equal<PX.Data.Required<FSContact.bAccountContactID>>, And<FSContact.revisionID, Equal<PX.Data.Required<FSContact.revisionID>>, And<FSContact.isDefaultContact, Equal<boolTrue>>>>>>)
  });

  public FSContactAttribute(System.Type AddressIDType, System.Type IsDefaultAddressType, System.Type SelectType)
    : base(AddressIDType, IsDefaultAddressType, SelectType)
  {
  }

  public override void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    bool isDirty = sender.IsDirty;
    base.RowInserted(sender, e);
    sender.IsDirty = isDirty;
  }

  public override void DefaultContact<TContact, TContactID>(
    PXCache sender,
    object documentRow,
    object contactRow)
  {
    PXView pxView = (PXView) null;
    object obj1 = (object) null;
    if (sender.GetValue<FSManufacturer.contactID>(documentRow) != null)
    {
      obj1 = sender.GetValue<FSManufacturer.contactID>(documentRow);
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select2<PX.Objects.CR.Contact, LeftJoin<FSContact, On<FSContact.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<FSContact.bAccountContactID, Equal<PX.Objects.CR.Contact.contactID>, And<FSContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<FSContact.isDefaultContact, Equal<boolTrue>>>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<PX.Data.Required<FSManufacturer.contactID>>>>)
      });
      pxView = sender.Graph.TypedViews.GetView(instance, false);
    }
    else if (sender.GetValue<FSManufacturer.locationID>(documentRow) != null)
    {
      obj1 = sender.GetValue<FSManufacturer.locationID>(documentRow);
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select2<PX.Objects.CR.Contact, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<PX.Data.Required<FSManufacturer.locationID>>>, LeftJoin<FSContact, On<FSContact.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<FSContact.bAccountContactID, Equal<PX.Objects.CR.Contact.contactID>, And<FSContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<FSContact.isDefaultContact, Equal<boolTrue>>>>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>)
      });
      pxView = sender.Graph.TypedViews.GetView(instance, false);
    }
    if (pxView != null)
    {
      int num1 = -1;
      int num2 = 0;
      bool flag = false;
      using (List<object>.Enumerator enumerator = pxView.Select(new object[1]
      {
        documentRow
      }, new object[1]{ obj1 }, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult current = (PXResult) enumerator.Current;
          flag = ContactAttribute.DefaultContact<TContact, TContactID>(sender, this.FieldName, documentRow, contactRow, (object) current);
        }
      }
      if (flag || this._Required)
        return;
      this.ClearRecord(sender, documentRow);
    }
    else
    {
      this.ClearRecord(sender, documentRow);
      if (!this._Required || sender.GetValue(documentRow, this._FieldOrdinal) != null)
        return;
      using (new ReadOnlyScope(new PXCache[1]
      {
        sender.Graph.Caches[this._RecordType]
      }))
      {
        object obj2 = sender.Graph.Caches[this._RecordType].Insert();
        object obj3 = sender.Graph.Caches[this._RecordType].GetValue(obj2, this._RecordID);
        sender.SetValue(documentRow, this._FieldOrdinal, obj3);
      }
    }
  }

  public override void Record_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 2 && ((FSContact) e.Row).IsDefaultContact.GetValueOrDefault())
    {
      PXView view = sender.Graph.TypedViews.GetView(this._DuplicateSelect, true);
      view.Clear();
      FSContact fsContact = (FSContact) view.SelectSingle(new object[3]
      {
        (object) ((FSContact) e.Row).BAccountID,
        (object) ((FSContact) e.Row).BAccountContactID,
        (object) ((FSContact) e.Row).RevisionID
      });
      if (fsContact != null)
      {
        this._KeyToAbort = sender.GetValue(e.Row, this._RecordID);
        object obj1 = sender.Graph.Caches[typeof (FSContact)].GetValue((object) fsContact, this._RecordID);
        PXCache cach = sender.Graph.Caches[this._ItemType];
        foreach (object obj2 in cach.Updated)
        {
          if (object.Equals(this._KeyToAbort, cach.GetValue(obj2, this._FieldOrdinal)))
            cach.SetValue(obj2, this._FieldOrdinal, obj1);
        }
        this._KeyToAbort = (object) null;
        ((CancelEventArgs) e).Cancel = true;
        return;
      }
    }
    base.Record_RowPersisting(sender, e);
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object objA = sender.GetValue(e.Row, this._FieldOrdinal);
    if (objA != null)
    {
      PXCache cach = sender.Graph.Caches[this._RecordType];
      if (Convert.ToInt32(objA) < 0)
      {
        foreach (object obj1 in cach.Inserted)
        {
          object objB = cach.GetValue(obj1, this._RecordID);
          if (object.Equals(objA, objB))
          {
            if (((FSContact) obj1).IsDefaultContact.GetValueOrDefault())
            {
              PXView view = sender.Graph.TypedViews.GetView(this._DuplicateSelect, true);
              view.Clear();
              FSContact fsContact = (FSContact) view.SelectSingle(new object[3]
              {
                (object) ((FSContact) obj1).BAccountID,
                (object) ((FSContact) obj1).BAccountContactID,
                (object) ((FSContact) obj1).RevisionID
              });
              if (fsContact != null)
              {
                this._KeyToAbort = sender.GetValue(e.Row, this._FieldOrdinal);
                object obj2 = sender.Graph.Caches[typeof (FSContact)].GetValue((object) fsContact, this._RecordID);
                sender.SetValue(e.Row, this._FieldOrdinal, obj2);
                break;
              }
              break;
            }
            break;
          }
        }
      }
    }
    base.RowPersisting(sender, e);
  }

  public virtual void RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!this._Required || sender.GetValue(e.Row, this._FieldOrdinal) != null)
      return;
    using (new ReadOnlyScope(new PXCache[1]
    {
      sender.Graph.Caches[this._RecordType]
    }))
    {
      object obj1 = sender.Graph.Caches[this._RecordType].Insert();
      object obj2 = sender.Graph.Caches[this._RecordType].GetValue(obj1, this._RecordID);
      sender.SetValue(e.Row, this._FieldOrdinal, obj2);
    }
  }
}
