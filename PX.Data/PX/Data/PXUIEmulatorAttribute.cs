// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUIEmulatorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace PX.Data;

public class PXUIEmulatorAttribute : PXDBInterceptorAttribute
{
  public BqlCommand _Command;

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this._Command = BqlCommand.CreateInstance(typeof (Select<>), sender.BqlTable);
    sender.SetupSlot<PXUIEmulatorAttribute.LastUpdated>((Func<PXUIEmulatorAttribute.LastUpdated>) (() => new PXUIEmulatorAttribute.LastUpdated()), (Func<PXUIEmulatorAttribute.LastUpdated, PXUIEmulatorAttribute.LastUpdated, PXUIEmulatorAttribute.LastUpdated>) ((item1, item2) =>
    {
      item1.Row = item2.Row;
      return item1;
    }), (Func<PXUIEmulatorAttribute.LastUpdated, PXUIEmulatorAttribute.LastUpdated>) (item => new PXUIEmulatorAttribute.LastUpdated()
    {
      Row = item.Row
    }));
  }

  public override BqlCommand GetRowCommand() => this._Command;

  public override BqlCommand GetTableCommand() => this._Command;

  protected virtual bool TryEmulate(
    PXCache sender,
    object row,
    Func<IDictionary, IDictionary, bool> process,
    out object result)
  {
    result = (object) null;
    bool? slot = PXContext.GetSlot<bool?>(this.GetType().FullName);
    bool flag = true;
    if (slot.GetValueOrDefault() == flag & slot.HasValue)
      return false;
    try
    {
      PXContext.SetSlot<bool?>(this.GetType().FullName, new bool?(true));
      Dictionary<string, object> dictionary1 = sender.ToDictionary(row);
      OrderedDictionary orderedDictionary1 = new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
      OrderedDictionary orderedDictionary2 = new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
      object row1 = sender.GetSlot<PXUIEmulatorAttribute.LastUpdated>(row, 0)?.Row;
      Dictionary<string, object> dictionary2 = row1 == null ? new Dictionary<string, object>() : sender.ToDictionary(row1);
      foreach (KeyValuePair<string, object> keyValuePair in dictionary1)
      {
        if (sender.Keys.Contains(keyValuePair.Key))
          orderedDictionary1[(object) keyValuePair.Key] = keyValuePair.Value;
        else if (keyValuePair.Value != null && (!dictionary2.ContainsKey(keyValuePair.Key) || !object.Equals(keyValuePair.Value, dictionary2[keyValuePair.Key])))
          orderedDictionary2[(object) keyValuePair.Key] = keyValuePair.Value;
      }
      if (row1 != null)
        sender.RestoreCopy(row, row1);
      if (process((IDictionary) orderedDictionary1, (IDictionary) orderedDictionary2))
        result = sender.Current;
      foreach (PXCache pxCache in sender.Graph.Caches.Values)
      {
        if (pxCache.Interceptor is PXUIEmulatorAttribute)
        {
          foreach (object obj in pxCache.Cached)
            pxCache.SetSlot<PXUIEmulatorAttribute.LastUpdated>(obj, 0, new PXUIEmulatorAttribute.LastUpdated()
            {
              Row = pxCache.CreateCopy(obj)
            });
        }
      }
      return true;
    }
    finally
    {
      PXContext.SetSlot<bool?>(this.GetType().FullName, new bool?());
    }
  }

  public override object Delete(PXCache sender, object row)
  {
    object result;
    return this.TryEmulate(sender, row, (Func<IDictionary, IDictionary, bool>) ((keys, values) => sender.Delete(keys, values) > 0), out result) ? result : base.Delete(sender, row);
  }

  public override object Insert(PXCache sender, object row)
  {
    object result;
    return this.TryEmulate(sender, row, (Func<IDictionary, IDictionary, bool>) ((keys, values) =>
    {
      foreach (DictionaryEntry key in keys)
        values[key.Key] = key.Value;
      return sender.Insert(values) > 0;
    }), out result) ? result : base.Insert(sender, row);
  }

  public override object Update(PXCache sender, object row)
  {
    object result;
    return this.TryEmulate(sender, row, (Func<IDictionary, IDictionary, bool>) ((keys, values) => sender.Update(keys, values) > 0), out result) ? result : base.Update(sender, row);
  }

  public override bool PersistInserted(PXCache sender, object row)
  {
    return sender.PersistInserted(row, true);
  }

  public override bool PersistUpdated(PXCache sender, object row)
  {
    return sender.PersistUpdated(row, true);
  }

  public override bool PersistDeleted(PXCache sender, object row)
  {
    return sender.PersistDeleted(row, true);
  }

  protected class LastUpdated
  {
    public object Row;
  }
}
