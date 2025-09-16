// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAddressAttribute
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

public abstract class FSAddressAttribute : AddressAttribute, IPXRowUpdatedSubscriber
{
  private BqlCommand _DuplicateSelect = BqlCommand.CreateInstance(new System.Type[1]
  {
    typeof (Select<FSAddress, Where<FSAddress.bAccountID, Equal<PX.Data.Required<FSAddress.bAccountID>>, And<FSAddress.bAccountAddressID, Equal<PX.Data.Required<FSAddress.bAccountAddressID>>, And<FSAddress.revisionID, Equal<PX.Data.Required<FSAddress.revisionID>>, And<FSAddress.isDefaultAddress, Equal<boolTrue>>>>>>)
  });

  public FSAddressAttribute(System.Type addressIDType, System.Type isDefaultAddressType, System.Type selectType)
    : base(addressIDType, isDefaultAddressType, selectType)
  {
  }

  public override void DefaultAddress<TAddress, TAddressID>(
    PXCache sender,
    object documentRow,
    object addressRow)
  {
    PXView pxView = (PXView) null;
    object obj1 = (object) null;
    if (sender.GetValue<FSManufacturer.contactID>(documentRow) != null)
    {
      obj1 = sender.GetValue<FSManufacturer.contactID>(documentRow);
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select2<PX.Objects.CR.Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<PX.Objects.CR.Address.addressID>>, LeftJoin<FSAddress, On<FSAddress.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<FSAddress.bAccountAddressID, Equal<PX.Objects.CR.Address.addressID>, And<FSAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<FSAddress.isDefaultAddress, Equal<boolTrue>>>>>>>, Where<PX.Objects.CR.Contact.contactID, Equal<PX.Data.Required<FSManufacturer.contactID>>>>)
      });
      pxView = sender.Graph.TypedViews.GetView(instance, false);
    }
    else if (sender.GetValue<FSManufacturer.locationID>(documentRow) != null)
    {
      obj1 = sender.GetValue<FSManufacturer.locationID>(documentRow);
      BqlCommand instance = BqlCommand.CreateInstance(new System.Type[1]
      {
        typeof (Select2<PX.Objects.CR.Address, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.defAddressID, Equal<PX.Objects.CR.Address.addressID>>, LeftJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.locationID, Equal<PX.Data.Required<FSManufacturer.locationID>>>, LeftJoin<FSAddress, On<FSAddress.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<FSAddress.bAccountAddressID, Equal<PX.Objects.CR.Address.addressID>, And<FSAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<FSAddress.isDefaultAddress, Equal<boolTrue>>>>>>>>, Where<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>)
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
          flag = AddressAttribute.DefaultAddress<TAddress, TAddressID>(sender, this.FieldName, documentRow, addressRow, (object) current);
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

  public override void RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    bool isDirty = sender.Graph.Caches[this._RecordType].IsDirty;
    base.RowInserted(sender, e);
    sender.Graph.Caches[this._RecordType].IsDirty = isDirty;
  }

  public override void Record_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 2 && ((FSAddress) e.Row).IsDefaultAddress.GetValueOrDefault())
    {
      PXView view = sender.Graph.TypedViews.GetView(this._DuplicateSelect, true);
      view.Clear();
      FSAddress fsAddress = (FSAddress) view.SelectSingle(new object[3]
      {
        (object) ((FSAddress) e.Row).BAccountID,
        (object) ((FSAddress) e.Row).BAccountAddressID,
        (object) ((FSAddress) e.Row).RevisionID
      });
      if (fsAddress != null)
      {
        this._KeyToAbort = sender.GetValue(e.Row, this._RecordID);
        object obj1 = sender.Graph.Caches[typeof (FSAddress)].GetValue((object) fsAddress, this._RecordID);
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
            if (((FSAddress) obj1).IsDefaultAddress.GetValueOrDefault())
            {
              PXView view = sender.Graph.TypedViews.GetView(this._DuplicateSelect, true);
              view.Clear();
              FSAddress fsAddress = (FSAddress) view.SelectSingle(new object[3]
              {
                (object) ((FSAddress) obj1).BAccountID,
                (object) ((FSAddress) obj1).BAccountAddressID,
                (object) ((FSAddress) obj1).RevisionID
              });
              if (fsAddress != null)
              {
                this._KeyToAbort = sender.GetValue(e.Row, this._FieldOrdinal);
                object obj2 = sender.Graph.Caches[typeof (FSAddress)].GetValue((object) fsAddress, this._RecordID);
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
