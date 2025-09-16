// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOContactAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.SO;

public abstract class SOContactAttribute : ContactAttribute
{
  private BqlCommand _DuplicateSelect = BqlCommand.CreateInstance(new Type[1]
  {
    typeof (Select<SOContact, Where<SOContact.customerID, Equal<PX.Data.Required<SOContact.customerID>>, And<SOContact.customerContactID, Equal<PX.Data.Required<SOContact.customerContactID>>, And<SOContact.revisionID, Equal<PX.Data.Required<SOContact.revisionID>>, And<SOContact.isDefaultContact, Equal<boolTrue>>>>>>)
  });

  public SOContactAttribute(Type AddressIDType, Type IsDefaultAddressType, Type SelectType)
    : base(AddressIDType, IsDefaultAddressType, SelectType)
  {
  }

  public override void Record_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 2 && ((SOContact) e.Row).IsDefaultContact.GetValueOrDefault())
    {
      PXView view = sender.Graph.TypedViews.GetView(this._DuplicateSelect, true);
      view.Clear();
      SOContact soContact = (SOContact) view.SelectSingle(new object[3]
      {
        (object) ((SOContact) e.Row).CustomerID,
        (object) ((SOContact) e.Row).CustomerContactID,
        (object) ((SOContact) e.Row).RevisionID
      });
      if (soContact != null)
      {
        this._KeyToAbort = sender.GetValue(e.Row, this._RecordID);
        object key = sender.Graph.Caches[typeof (SOContact)].GetValue((object) soContact, this._RecordID);
        PXCache cach = sender.Graph.Caches[this._ItemType];
        foreach (object obj in cach.Updated)
        {
          if (object.Equals(this._KeyToAbort, cach.GetValue(obj, this._FieldOrdinal)))
            cach.SetValue(obj, this._FieldOrdinal, key);
        }
        if (!this._persistedItems.ContainsKey(key))
          this._persistedItems.Add(key, this._KeyToAbort);
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
            if (((SOContact) obj1).IsDefaultContact.GetValueOrDefault())
            {
              PXView view = sender.Graph.TypedViews.GetView(this._DuplicateSelect, true);
              view.Clear();
              SOContact soContact = (SOContact) view.SelectSingle(new object[3]
              {
                (object) ((SOContact) obj1).CustomerID,
                (object) ((SOContact) obj1).CustomerContactID,
                (object) ((SOContact) obj1).RevisionID
              });
              if (soContact != null)
              {
                this._KeyToAbort = sender.GetValue(e.Row, this._FieldOrdinal);
                object obj2 = sender.Graph.Caches[typeof (SOContact)].GetValue((object) soContact, this._RecordID);
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
}
