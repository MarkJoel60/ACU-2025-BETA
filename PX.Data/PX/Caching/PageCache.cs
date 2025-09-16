// Decompiled with JetBrains decompiler
// Type: PX.Caching.PageCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Caching;

/// <summary>
/// A marker type used in the <see cref="T:PX.Caching.ICacheVersion`1" /> and <see cref="T:PX.Caching.ICacheControl`1" />
/// services for the client-side page caching.
/// </summary>
[PXInternalUseOnly]
public sealed class PageCache
{
  private PageCache()
  {
  }
}
