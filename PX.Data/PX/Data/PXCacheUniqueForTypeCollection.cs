// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheUniqueForTypeCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

/// <summary>
/// This cache collection contains separate caches for each item type regardless of inheritance.
/// </summary>
[PXInternalUseOnly]
public class PXCacheUniqueForTypeCollection : PXCacheCollection
{
  public PXCacheUniqueForTypeCollection(PXGraph parent)
    : base(parent)
  {
  }

  public PXCacheUniqueForTypeCollection(PXGraph parent, int capacity)
    : base(parent, capacity)
  {
  }

  internal override bool IsSameCache(System.Type type) => false;
}
