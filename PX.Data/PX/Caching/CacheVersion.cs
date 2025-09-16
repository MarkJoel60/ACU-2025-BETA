// Decompiled with JetBrains decompiler
// Type: PX.Caching.CacheVersion
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Caching;

[PXInternalUseOnly]
[PXHidden]
public sealed class CacheVersion : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(1024 /*0x0400*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public 
  #nullable disable
  string CacheName { get; set; }

  [PXDBInt]
  [PXDefault]
  public int? Version { get; set; }

  public abstract class cacheName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CacheVersion.cacheName>
  {
  }

  public abstract class version : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CacheVersion.version>
  {
  }
}
