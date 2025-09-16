// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.PXHashSet`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public class PXHashSet<T>(PXGraph graph) : HashSet<T>((IEqualityComparer<T>) new PXHashSet<T>.Comparer<T>(graph))
  where T : class, IBqlTable
{
  public List<T> ToList() => new List<T>((IEnumerable<T>) this);

  public class Comparer<TT> : IEqualityComparer<TT> where TT : T
  {
    protected PXCache _cache;

    public Comparer(PXGraph graph) => this._cache = graph.Caches[typeof (TT)];

    public bool Equals(TT a, TT b) => this._cache.ObjectsEqual((object) a, (object) b);

    public int GetHashCode(TT a) => this._cache.GetObjectHashCode((object) a);
  }
}
