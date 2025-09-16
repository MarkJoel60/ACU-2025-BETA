// Decompiled with JetBrains decompiler
// Type: PX.Caching.ICacheControl
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Caching;

/// <summary>
/// An abstraction used to control cache invalidation for a particular cache.
/// Every dependent cache must be registered in such a way that it can be resolved using this interface.
/// Cache must not implement this interface directly, but use <see cref="T:PX.Caching.ICacheControlledBy`1" /> instead.
/// </summary>
[PXInternalUseOnly]
public interface ICacheControl
{
  void InvalidateCache();
}
