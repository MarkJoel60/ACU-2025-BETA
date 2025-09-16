// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheRemovedOriginalsCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

internal sealed class PXCacheRemovedOriginalsCollection
{
  private readonly Dictionary<IBqlTable, BqlTablePair> _inner = new Dictionary<IBqlTable, BqlTablePair>((IEqualityComparer<IBqlTable>) PXWeakKeyComparer.Comparer);

  internal BqlTablePair this[IBqlTable key]
  {
    set
    {
      Dictionary<IBqlTable, BqlTablePair> inner = this._inner;
      if (!(key is PXWeakReference key1))
        key1 = new PXWeakReference(key);
      BqlTablePair bqlTablePair = value;
      inner[(IBqlTable) key1] = bqlTablePair;
    }
  }

  internal bool TryGetValue(IBqlTable key, out BqlTablePair value)
  {
    return this._inner.TryGetValue(key, out value);
  }

  internal void Restore(PXCacheOriginalCollection originals)
  {
    foreach (KeyValuePair<IBqlTable, BqlTablePair> keyValuePair in this._inner)
    {
      if (((WeakReference) keyValuePair.Key).Target is IBqlTable target)
        originals[target] = keyValuePair.Value;
    }
  }
}
