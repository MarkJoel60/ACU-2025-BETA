// Decompiled with JetBrains decompiler
// Type: PX.Caching.CacheControlAggregator`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Caching;

internal class CacheControlAggregator<TCacheName> : ICacheControl<TCacheName>, ICacheControl
{
  private readonly IEnumerable<ICacheControlledBy<TCacheName>> _dependentCaches;

  public CacheControlAggregator(
    IEnumerable<ICacheControlledBy<TCacheName>> dependentCaches)
  {
    this._dependentCaches = dependentCaches;
  }

  public void InvalidateCache()
  {
    foreach (ICacheControl dependentCach in this._dependentCaches)
      dependentCach.InvalidateCache();
  }
}
