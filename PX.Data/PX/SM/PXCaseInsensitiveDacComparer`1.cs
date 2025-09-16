// Decompiled with JetBrains decompiler
// Type: PX.SM.PXCaseInsensitiveDacComparer`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Serialization;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

internal class PXCaseInsensitiveDacComparer<T> : IEqualityComparer<T>
{
  public PXGraph Graph = new PXGraph();

  bool IEqualityComparer<T>.Equals(T x, T y)
  {
    if ((object) x == null)
      throw new ArgumentNullException(nameof (x));
    if ((object) y == null)
      throw new ArgumentNullException(nameof (y));
    PXCache cach = this.Graph.Caches[x.GetType()];
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    foreach (Tuple<object, object> tuple in PXCaseInsensitiveDacComparer<T>.GetKeyValues(x, cach).Zip<object, object, Tuple<object, object>>((IEnumerable<object>) PXCaseInsensitiveDacComparer<T>.GetKeyValues(y, cach), PXCaseInsensitiveDacComparer<T>.\u003C\u003EO.\u003C0\u003E__Create ?? (PXCaseInsensitiveDacComparer<T>.\u003C\u003EO.\u003C0\u003E__Create = new Func<object, object, Tuple<object, object>>(Tuple.Create<object, object>))))
    {
      if (!object.Equals(tuple.Item1, tuple.Item2))
        return false;
    }
    return true;
  }

  int IEqualityComparer<T>.GetHashCode(T obj)
  {
    PXCache cach = this.Graph.Caches[obj.GetType()];
    return PXHashCode.Generate((IList<object>) PXCaseInsensitiveDacComparer<T>.GetKeyValues(obj, cach));
  }

  private static List<object> GetKeyValues(T obj, PXCache cache)
  {
    return cache.Keys.Select<string, object>((Func<string, object>) (it =>
    {
      object keyValues = PXFieldState.UnwrapValue(cache.GetValue((object) obj, it));
      if (keyValues is string str2)
        keyValues = (object) str2.ToUpperInvariant();
      return keyValues;
    })).ToList<object>();
  }
}
