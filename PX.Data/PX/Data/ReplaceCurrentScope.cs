// Decompiled with JetBrains decompiler
// Type: PX.Data.ReplaceCurrentScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class ReplaceCurrentScope : IDisposable
{
  private List<PXCache> _caches;
  private List<object> _currents;

  public ReplaceCurrentScope(IEnumerable<KeyValuePair<PXCache, object>> caches)
  {
    this._caches = new List<PXCache>();
    this._currents = new List<object>();
    foreach (KeyValuePair<PXCache, object> cach in caches)
    {
      this._caches.Add(cach.Key);
      this._currents.Add(cach.Key.Current);
      cach.Key.Current = cach.Value;
    }
  }

  void IDisposable.Dispose()
  {
    for (int index = 0; index < this._caches.Count; ++index)
      this._caches[index].Current = this._currents[index];
  }
}
