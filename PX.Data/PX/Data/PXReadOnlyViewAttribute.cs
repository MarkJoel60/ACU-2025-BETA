// Decompiled with JetBrains decompiler
// Type: PX.Data.PXReadOnlyViewAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// When attached to a data view in a graph, excludes the cache behind the view
/// from the collection of updatable caches.
/// The key consequence is that this cache is not persisted by the graph's
/// Persist() method and by the Save action.
/// The cache can still be persisted manually.
/// </summary>
public class PXReadOnlyViewAttribute : PXViewExtensionAttribute
{
  private string _viewName;

  /// <exclude />
  public override void ViewCreated(PXGraph graph, string viewName)
  {
    this._viewName = viewName;
    graph.Initialized += (PXGraphInitializedDelegate) (sender =>
    {
      PXCache cache = graph.Views[this._viewName].Cache;
      sender.Views.Caches.Remove(cache.GetItemType());
    });
  }
}
