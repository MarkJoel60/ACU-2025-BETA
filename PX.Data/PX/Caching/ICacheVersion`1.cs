// Decompiled with JetBrains decompiler
// Type: PX.Caching.ICacheVersion`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Caching;

/// <summary>
/// An abstraction to work with cache version for a particular cache type marked by <typeparamref name="TCacheName" />.
/// </summary>
/// <typeparam name="TCacheName">A marker type used to distinguish different caches.</typeparam>
[PXInternalUseOnly]
public interface ICacheVersion<TCacheName>
{
  int Current { get; }

  void Increment();
}
