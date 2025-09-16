// Decompiled with JetBrains decompiler
// Type: PX.Data.PXViewInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable enable
namespace PX.Data;

[PXInternalUseOnly]
public class PXViewInfo : PXInfo
{
  public readonly PXCacheInfo Cache;

  public BqlCommand? BqlCommand { get; private set; }

  public bool HasInferredDisplayName { get; private set; }

  public PXViewInfo(string viewName, string displayName, PXCacheInfo cache)
    : base(viewName, displayName ?? $"{cache.DisplayName}({viewName})")
  {
    this.Cache = cache;
    this.HasInferredDisplayName = displayName == null;
  }

  public PXViewInfo(string viewName, string? displayName, PXCacheInfo cache, BqlCommand? command)
    : base(viewName, displayName ?? $"{cache.DisplayName}({viewName})")
  {
    this.Cache = cache;
    this.BqlCommand = command;
    this.HasInferredDisplayName = displayName == null;
  }
}
