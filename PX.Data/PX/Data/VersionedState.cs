// Decompiled with JetBrains decompiler
// Type: PX.Data.VersionedState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

internal class VersionedState
{
  private PXGraph graph;

  public VersionedState(PXGraph aGraph) => this.graph = aGraph;

  public void CreateNewVersion(PXCache cache = null)
  {
    if (!this.graph.IsMobile)
      return;
    EnumerableExtensions.ForEach<KeyValuePair<System.Type, PXCache>>((IEnumerable<KeyValuePair<System.Type, PXCache>>) this.graph.Caches, (System.Action<KeyValuePair<System.Type, PXCache>>) (_ => _.Value.CreateNewVersion()));
    cache?.SaveInsertState();
    this.graph.IsInVersionModifiedState = true;
  }

  public void DiscardCurrentVersion()
  {
    if (!this.graph.IsMobile)
      return;
    EnumerableExtensions.ForEach<KeyValuePair<System.Type, PXCache>>((IEnumerable<KeyValuePair<System.Type, PXCache>>) this.graph.Caches, (System.Action<KeyValuePair<System.Type, PXCache>>) (_ => _.Value.DiscardCurrentVersion()));
    this.graph.IsInVersionModifiedState = false;
  }

  public void SaveCurrentVersion()
  {
    if (!this.graph.IsMobile)
      return;
    EnumerableExtensions.ForEach<KeyValuePair<System.Type, PXCache>>((IEnumerable<KeyValuePair<System.Type, PXCache>>) this.graph.Caches, (System.Action<KeyValuePair<System.Type, PXCache>>) (_ => _.Value.SaveCurrentVersion()));
    this.graph.IsInVersionModifiedState = false;
  }
}
