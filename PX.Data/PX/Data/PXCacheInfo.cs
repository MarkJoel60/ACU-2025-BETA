// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCacheInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable enable
namespace PX.Data;

[PXInternalUseOnly]
public class PXCacheInfo : PXInfo
{
  public readonly System.Type CacheType;

  public PXCacheInfo(System.Type cacheType, string displayName)
    : base(cacheType.FullName, displayName)
  {
    this.CacheType = cacheType;
  }

  public PXCacheInfo(System.Type cacheType)
    : base(cacheType.FullName, (string) null)
  {
    this.CacheType = cacheType;
    if (cacheType.IsDefined(typeof (PXCacheNameAttribute), true))
      this.DisplayName = ((PXNameAttribute) cacheType.GetCustomAttributes(typeof (PXCacheNameAttribute), true)[0]).GetName();
    else
      this.DisplayName = cacheType.Name;
  }
}
