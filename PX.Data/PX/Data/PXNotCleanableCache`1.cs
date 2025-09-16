// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNotCleanableCache`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.EP;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class PXNotCleanableCache<TNode>(PXGraph graph) : PXCache<TNode>(graph) where TNode : class, IBqlTable, new()
{
  public PXNotCleanableCache(PXGraph graph, PXCache source)
    : this(graph)
  {
    foreach (object obj in source.Cached)
    {
      this.Insert(obj);
      this.SetStatus(obj, source.GetStatus(obj));
    }
  }

  public override void Clear()
  {
  }

  public static PXNotCleanableCache<TNode> Attach(PXGraph graph)
  {
    PXNotCleanableCache<TNode> cache = new PXNotCleanableCache<TNode>(graph);
    cache.Load();
    graph.Caches[typeof (TNode)] = (PXCache) cache;
    graph.SynchronizeByItemType((PXCache) cache);
    return cache;
  }

  public static PXNotCleanableCache<TNode> Attach(PXGraph graph, PXCache cache)
  {
    PXNotCleanableCache<TNode> cache1 = new PXNotCleanableCache<TNode>(graph, cache);
    cache1.Load();
    graph.Caches[typeof (TNode)] = (PXCache) cache1;
    graph.SynchronizeByItemType((PXCache) cache1);
    return cache1;
  }
}
