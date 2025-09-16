// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProformaLineType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.PM;

public static class PMProformaLineType
{
  public const 
  #nullable disable
  string Progressive = "P";
  public const string Transaction = "T";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "P", "T" }, new string[2]
      {
        "Progressive",
        "Transactions"
      })
    {
    }
  }

  public class progressive : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMProformaLineType.progressive>
  {
    public progressive()
      : base("P")
    {
    }
  }

  public class transaction : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMProformaLineType.transaction>
  {
    public transaction()
      : base("T")
    {
    }
  }
}
