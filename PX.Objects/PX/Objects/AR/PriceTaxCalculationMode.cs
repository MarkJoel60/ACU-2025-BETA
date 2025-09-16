// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.PriceTaxCalculationMode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

public class PriceTaxCalculationMode
{
  public const 
  #nullable disable
  string Net = "N";
  public const string Gross = "G";
  public const string AllModes = "A";
  public const string Undefined = "U";

  public class ListWithAllAttribute : PXStringListAttribute
  {
    public ListWithAllAttribute()
      : base(new string[4]{ "A", "G", "N", "U" }, new string[4]
      {
        "All Modes",
        "Gross",
        "Net",
        "Not Set"
      })
    {
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "G", "N", "U" }, new string[3]
      {
        "Gross",
        "Net",
        "Not Set"
      })
    {
    }
  }

  public class gross : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceTaxCalculationMode.gross>
  {
    public gross()
      : base("G")
    {
    }
  }

  public class net : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceTaxCalculationMode.net>
  {
    public net()
      : base("N")
    {
    }
  }

  public class allModes : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceTaxCalculationMode.allModes>
  {
    public allModes()
      : base("A")
    {
    }
  }

  public class undefined : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PriceTaxCalculationMode.undefined>
  {
    public undefined()
      : base("U")
    {
    }
  }
}
