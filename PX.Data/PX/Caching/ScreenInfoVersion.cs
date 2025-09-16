// Decompiled with JetBrains decompiler
// Type: PX.Caching.ScreenInfoVersion
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable disable
namespace PX.Caching;

[PXInternalUseOnly]
[PXHidden]
public sealed class ScreenInfoVersion : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(36, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  public string ScreenInfoId { get; set; }

  [PXDBInt]
  [PXDefault]
  public int? Version { get; set; }

  public abstract class screenInfoId : 
    BqlType<IBqlString, string>.Field<ScreenInfoVersion.screenInfoId>
  {
  }

  public abstract class version : BqlType<IBqlInt, int>.Field<ScreenInfoVersion.version>
  {
  }
}
