// Decompiled with JetBrains decompiler
// Type: PX.SM.PXCacheEqualityComparer`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

internal class PXCacheEqualityComparer<T> : IEqualityComparer<T>
{
  public PXGraph Graph = new PXGraph();

  bool IEqualityComparer<T>.Equals(T x, T y)
  {
    return this.Graph.Caches[x.GetType()].ObjectsEqual((object) x, (object) y);
  }

  int IEqualityComparer<T>.GetHashCode(T obj) => this.Graph.Caches[obj.GetType()].GetHashCode();
}
