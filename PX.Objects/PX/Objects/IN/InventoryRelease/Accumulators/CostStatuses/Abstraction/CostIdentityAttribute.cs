// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses.Abstraction.CostIdentityAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses.Abstraction;

public class CostIdentityAttribute : PXDBLongIdentityAttribute
{
  protected long? _KeyToAbort;
  protected Type[] _ChildTypes;

  public CostIdentityAttribute(params Type[] ChildTypes) => this._ChildTypes = ChildTypes;

  public virtual void RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    if (e.TranStatus == null)
    {
      this._KeyToAbort = (long?) cache.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal);
      base.RowPersisted(cache, e);
      this.ConfirmKeyForChildren(cache, e.Row, PXDBOperationExt.Command(e.Operation) == 2);
    }
    else
    {
      if (e.TranStatus != 2 || !this._KeyToAbort.HasValue)
        return;
      this.RollbackKeyForChildren(cache, e.Row);
      this._KeyToAbort = new long?();
      base.RowPersisted(cache, e);
    }
  }

  private void ConfirmKeyForChildren(PXCache cache, object row, bool isNewRow)
  {
    long? keyToAbort = this._KeyToAbort;
    long num = 0;
    if (!(keyToAbort.GetValueOrDefault() < num & keyToAbort.HasValue))
      return;
    long? to = isNewRow ? new long?(Convert.ToInt64((object) PXDatabase.SelectIdentity())) : this.SelectAccumIdentity(cache, row);
    this.ChangeKeyForChildren(cache, this._KeyToAbort, to);
  }

  private long? SelectAccumIdentity(PXCache cache, object row)
  {
    List<PXDataField> pxDataFieldList = new List<PXDataField>()
    {
      new PXDataField(((PXEventSubscriberAttribute) this)._FieldName)
    };
    foreach (string key in (IEnumerable<string>) cache.Keys)
      pxDataFieldList.Add((PXDataField) new PXDataFieldValue(key, cache.GetValue(row, key)));
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(cache.BqlTable, pxDataFieldList.ToArray()))
    {
      if (pxDataRecord != null)
        return pxDataRecord.GetInt64(0);
    }
    return new long?();
  }

  private void RollbackKeyForChildren(PXCache cache, object row)
  {
    this.ChangeKeyForChildren(cache, (long?) cache.GetValue(row, ((PXEventSubscriberAttribute) this)._FieldOrdinal), this._KeyToAbort);
  }

  private void ChangeKeyForChildren(PXCache cache, long? from, long? to)
  {
    foreach (Type childType in this._ChildTypes)
    {
      PXCache cach = cache.Graph.Caches[BqlCommand.GetItemType(childType)];
      foreach (object obj in cach.Inserted)
      {
        long? nullable1 = (long?) cach.GetValue(obj, childType.Name);
        long? nullable2 = from;
        if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          cach.SetValue(obj, childType.Name, (object) to);
      }
    }
  }
}
