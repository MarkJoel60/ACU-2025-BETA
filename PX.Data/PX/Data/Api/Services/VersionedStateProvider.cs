// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Services.VersionedStateProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Api.Services;

public class VersionedStateProvider : IVersionedStateProvider
{
  public void Save(PXGraph graph)
  {
    graph.VersionedState.SaveCurrentVersion();
    graph.ShouldSaveVersionModified = false;
  }

  public void Cancel(PXGraph graph)
  {
    graph.VersionedState.DiscardCurrentVersion();
    graph.ShouldSaveVersionModified = false;
  }

  public void StartAction(PXCache cache, bool isInsert = false)
  {
    cache.Graph.ShouldSaveVersionModified = false;
    if (cache.Graph.IsInVersionModifiedState)
      cache.Graph.VersionedState.SaveCurrentVersion();
    cache.Graph.ShouldSaveVersionModified = true;
    if (!isInsert)
      return;
    cache.SaveInsertState();
  }

  public bool CheckStateModified(PXCache cache, object record) => cache.IfModifiedVersion(record);

  public bool SupportOnSmartPanels() => false;
}
