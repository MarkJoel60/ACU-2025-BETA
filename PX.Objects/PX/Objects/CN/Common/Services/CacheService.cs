// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Services.CacheService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CN.Common.Services;

public class CacheService : ICacheService
{
  public object GetValueOriginal<T>(PXCache cache, object record) where T : IBqlField
  {
    return cache.GetValueOriginal<T>(record);
  }
}
