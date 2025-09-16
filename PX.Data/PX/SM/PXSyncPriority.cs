// Decompiled with JetBrains decompiler
// Type: PX.SM.PXSyncPriority
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

public static class PXSyncPriority
{
  public const 
  #nullable disable
  string AcumaticaCode = "A";
  public const string ExchangeCode = "E";
  public const string LastUpdatedCode = "L";
  public const string KeepBothCode = "B";

  [Flags]
  public enum Directions
  {
    Acumatica = 1,
    Exchange = 2,
    LastUpdated = Exchange | Acumatica, // 0x00000003
    KeepBoth = 4,
  }

  public class acumatica : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXSyncPriority.acumatica>
  {
    public acumatica()
      : base("A")
    {
    }
  }

  public class exchange : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXSyncPriority.exchange>
  {
    public exchange()
      : base("E")
    {
    }
  }

  public class lastUpdated : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXSyncPriority.lastUpdated>
  {
    public lastUpdated()
      : base("L")
    {
    }
  }

  public class noPriority : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXSyncPriority.noPriority>
  {
    public noPriority()
      : base("B")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "A", "E", "L", "B" }, new string[4]
      {
        "System",
        "Exchange",
        "Last Updated",
        "Keep Both"
      })
    {
    }
  }
}
