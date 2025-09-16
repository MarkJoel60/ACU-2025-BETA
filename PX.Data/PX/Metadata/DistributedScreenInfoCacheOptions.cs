// Decompiled with JetBrains decompiler
// Type: PX.Metadata.DistributedScreenInfoCacheOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Metadata;

/// <remarks>
/// Made <see langword="public" /> only because <see cref="T:PX.Metadata.DistributedScreenInfoCache" /> is <see langword="public" />
/// </remarks>
[PXInternalUseOnly]
public class DistributedScreenInfoCacheOptions
{
  public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromDays(7.0);
}
