// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction.StatusAccumulatorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;

public class StatusAccumulatorAttribute : PXAccumulatorAttribute
{
  protected Dictionary<object, bool> _persisted;
  protected PXAccumulatorCollection _columns;
  protected bool _InternalCall;

  protected virtual TAccumulator Aggregate<TAccumulator>(
    PXCache cache,
    TAccumulator a,
    TAccumulator b)
    where TAccumulator : class, IBqlTable, new()
  {
    TAccumulator copy = (TAccumulator) cache.CreateCopy((object) a);
    foreach (KeyValuePair<string, PXAccumulatorItem> column in (Dictionary<string, PXAccumulatorItem>) this._columns)
    {
      if (column.Value.CurrentUpdateBehavior == 1)
      {
        object obj1 = cache.GetValue((object) a, column.Key);
        object obj2 = cache.GetValue((object) b, column.Key);
        object obj3 = (object) null;
        if (obj1 is Decimal num1)
          obj3 = (object) (num1 + (Decimal) obj2);
        if (obj1 is double num2)
          obj3 = (object) (num2 + (double) obj2);
        if (obj1 is long num3)
          obj3 = (object) (num3 + (long) obj2);
        if (obj1 is int num4)
          obj3 = (object) (num4 + (int) obj2);
        if (obj1 is short num5)
          obj3 = (object) ((int) num5 + (int) (short) obj2);
        cache.SetValue((object) copy, column.Key, obj3);
      }
      else if (column.Value.CurrentUpdateBehavior == null)
      {
        object obj = cache.GetValue((object) b, column.Key);
        cache.SetValue((object) copy, column.Key, obj);
      }
      else if (column.Value.CurrentUpdateBehavior == 4)
      {
        object obj4 = cache.GetValue((object) a, column.Key);
        object obj5 = cache.GetValue((object) b, column.Key);
        if (obj4 == null)
          obj4 = obj5;
        object obj6 = obj4;
        cache.SetValue((object) copy, column.Key, obj6);
      }
    }
    return copy;
  }

  public virtual void ResetPersisted(object row)
  {
    if (this._persisted == null || !this._persisted.ContainsKey(row))
      return;
    this._persisted.Remove(row);
  }

  public virtual object Insert(PXCache cache, object row)
  {
    object copy = cache.CreateCopy(row);
    PXAccumulatorCollection accumulatorCollection = new PXAccumulatorCollection(cache, row);
    this._InternalCall = true;
    base.PrepareInsert(cache, row, accumulatorCollection);
    foreach (KeyValuePair<string, PXAccumulatorItem> keyValuePair in (Dictionary<string, PXAccumulatorItem>) accumulatorCollection)
    {
      if (keyValuePair.Value.CurrentUpdateBehavior == 1)
        cache.SetValue(copy, keyValuePair.Key, (object) null);
    }
    object key = base.Insert(cache, copy);
    if (key == null || !this._persisted.ContainsKey(key))
      return key;
    foreach (string field in (List<string>) cache.Fields)
    {
      if (cache.GetValue(copy, field) == null)
      {
        object obj;
        if (cache.RaiseFieldDefaulting(field, copy, ref obj))
          cache.RaiseFieldUpdating(field, copy, ref obj);
        cache.SetValue(copy, field, obj);
      }
    }
    return copy;
  }

  public virtual void CacheAttached(PXCache cache)
  {
    base.CacheAttached(cache);
    this._persisted = new Dictionary<object, bool>();
    PXGraph.RowPersistedEvents rowPersisted = cache.Graph.RowPersisted;
    Type itemType = cache.GetItemType();
    StatusAccumulatorAttribute accumulatorAttribute = this;
    // ISSUE: virtual method pointer
    PXRowPersisted pxRowPersisted = new PXRowPersisted((object) accumulatorAttribute, __vmethodptr(accumulatorAttribute, RowPersisted));
    rowPersisted.AddHandler(itemType, pxRowPersisted);
    // ISSUE: method pointer
    cache.Graph.OnClear += new PXGraphClearDelegate((object) this, __methodptr(Graph_OnClear));
  }

  private void Graph_OnClear(PXGraph graph, PXClearOption option)
  {
    this._persisted = new Dictionary<object, bool>();
  }

  protected virtual bool PrepareInsert(PXCache cache, object row, PXAccumulatorCollection columns)
  {
    this._columns = columns;
    if (!base.PrepareInsert(cache, row, columns) || !cache.IsKeysFilled(row))
      return false;
    foreach (string field in (List<string>) cache.Fields)
    {
      if (cache.Keys.IndexOf(field) < 0 && field.StartsWith("Usr", StringComparison.InvariantCultureIgnoreCase))
      {
        object obj = cache.GetValue(row, field);
        columns.Update(field, obj, obj != null ? (PXDataFieldAssign.AssignBehavior) 0 : (PXDataFieldAssign.AssignBehavior) 4);
      }
    }
    return true;
  }

  public virtual bool PersistInserted(PXCache cache, object row)
  {
    this._InternalCall = false;
    if (!base.PersistInserted(cache, row))
      return false;
    this._persisted.Add(row, true);
    return true;
  }

  public virtual void RowPersisted(PXCache cache, PXRowPersistedEventArgs e)
  {
    if (PXDBOperationExt.Command(e.Operation) != 2 || !EnumerableExtensions.IsIn<PXTranStatus>(e.TranStatus, (PXTranStatus) 1, (PXTranStatus) 2) || !this._persisted.ContainsKey(e.Row))
      return;
    this._persisted.Remove(e.Row);
  }

  public virtual bool IsZero(IStatus a) => a.IsZero();
}
