// Decompiled with JetBrains decompiler
// Type: PX.Api.SYWhatToShow
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYWhatToShow : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(true)]
  public virtual bool? ShowHidden { get; set; }

  [PXBool]
  public virtual bool? NeedToRefresh { get; set; }

  public abstract class showHidden : BqlType<IBqlBool, bool>.Field<
  #nullable disable
  SYWhatToShow.showHidden>
  {
  }

  public abstract class needToRefresh : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYWhatToShow.needToRefresh>
  {
  }
}
