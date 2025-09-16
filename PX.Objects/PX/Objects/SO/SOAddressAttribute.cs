// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOAddressAttribute
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

public abstract class SOAddressAttribute : AddressAttribute
{
  private BqlCommand _DuplicateSelect = BqlCommand.CreateInstance(new Type[1]
  {
    typeof (Select<SOAddress, Where<SOAddress.customerID, Equal<PX.Data.Required<SOAddress.customerID>>, And<SOAddress.customerAddressID, Equal<PX.Data.Required<SOAddress.customerAddressID>>, And<SOAddress.revisionID, Equal<PX.Data.Required<SOAddress.revisionID>>, And<SOAddress.isDefaultAddress, Equal<boolTrue>>>>>>)
  });

  public SOAddressAttribute(Type AddressIDType, Type IsDefaultAddressType, Type SelectType)
    : base(AddressIDType, IsDefaultAddressType, SelectType)
  {
  }

  public override void Record_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 2 && ((SOAddress) e.Row).IsDefaultAddress.GetValueOrDefault())
    {
      PXView view = sender.Graph.TypedViews.GetView(this._DuplicateSelect, true);
      view.Clear();
      SOAddress soAddress = (SOAddress) view.SelectSingle(new object[3]
      {
        (object) ((SOAddress) e.Row).CustomerID,
        (object) ((SOAddress) e.Row).CustomerAddressID,
        (object) ((SOAddress) e.Row).RevisionID
      });
      if (soAddress != null)
      {
        this._KeyToAbort = sender.GetValue(e.Row, this._RecordID);
        object key = sender.Graph.Caches[typeof (SOAddress)].GetValue((object) soAddress, this._RecordID);
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
            if (((SOAddress) obj1).IsDefaultAddress.GetValueOrDefault())
            {
              PXView view = sender.Graph.TypedViews.GetView(this._DuplicateSelect, true);
              view.Clear();
              SOAddress soAddress = (SOAddress) view.SelectSingle(new object[3]
              {
                (object) ((SOAddress) obj1).CustomerID,
                (object) ((SOAddress) obj1).CustomerAddressID,
                (object) ((SOAddress) obj1).RevisionID
              });
              if (soAddress != null)
              {
                this._KeyToAbort = sender.GetValue(e.Row, this._FieldOrdinal);
                object obj2 = sender.Graph.Caches[typeof (SOAddress)].GetValue((object) soAddress, this._RecordID);
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
