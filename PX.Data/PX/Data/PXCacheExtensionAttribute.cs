// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheExtensionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public abstract class PXCacheExtensionAttribute : PXViewExtensionAttribute
{
  public sealed override void ViewCreated(PXGraph graph, string viewName)
  {
    System.Type itemType = graph.Views[viewName].Cache.GetItemType();
    PXCache cach = graph.Caches[itemType];
    this.PerformInjection(graph, cach, itemType);
  }

  public void CacheAttached(PXGraph graph, PXCache cache)
  {
    this.PerformInjection(graph, cache, cache.GetItemType());
  }

  private void PerformInjection(PXGraph graph, PXCache cache, System.Type itemType)
  {
    this.AddHandlers(cache);
  }

  protected abstract void AddHandlers(PXCache cache);
}
