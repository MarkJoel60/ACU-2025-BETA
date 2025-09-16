// Decompiled with JetBrains decompiler
// Type: PX.SM.PXEmailSyncDirection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

public static class PXEmailSyncDirection
{
  public const 
  #nullable disable
  string ImportCode = "I";
  public const string ExportCode = "E";
  public const string FullCode = "F";

  public static string Parse(PXEmailSyncDirection.Directions direct)
  {
    switch (direct)
    {
      case PXEmailSyncDirection.Directions.Import:
        return "I";
      case PXEmailSyncDirection.Directions.Export:
        return "E";
      case PXEmailSyncDirection.Directions.Full:
        return "F";
      default:
        throw new PXException("Unknown sync direction.");
    }
  }

  public static PXEmailSyncDirection.Directions Parse(string code)
  {
    switch (code)
    {
      case "I":
        return PXEmailSyncDirection.Directions.Import;
      case "E":
        return PXEmailSyncDirection.Directions.Export;
      case "F":
        return PXEmailSyncDirection.Directions.Full;
      default:
        throw new PXException("Unknown sync direction.");
    }
  }

  [Flags]
  public enum Directions
  {
    Import = 1,
    Export = 2,
    Full = Export | Import, // 0x00000003
  }

  public class import : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXEmailSyncDirection.import>
  {
    public import()
      : base("I")
    {
    }
  }

  public class export : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXEmailSyncDirection.export>
  {
    public export()
      : base("E")
    {
    }
  }

  public class full : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PXEmailSyncDirection.full>
  {
    public full()
      : base("F")
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "F", "I", "E" }, new string[3]
      {
        "Bidirectional",
        "Exchange->System",
        "System->Exchange"
      })
    {
    }
  }
}
