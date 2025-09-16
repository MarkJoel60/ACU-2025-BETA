// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.CostBasisOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class CostBasisOption
{
  public const 
  #nullable disable
  string StandardCost = "S";
  public const string PriceMarkupPercent = "M";
  public const string PercentOfSalesPrice = "P";
  public const string UndefinedCostBasis = "U";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("S", "Standard Cost"),
        PXStringListAttribute.Pair("M", "Markup %"),
        PXStringListAttribute.Pair("P", "Percentage of Sales Price")
      })
    {
    }
  }

  public class standardCost : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CostBasisOption.standardCost>
  {
    public standardCost()
      : base("S")
    {
    }
  }

  public class priceMarkupPercent : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CostBasisOption.priceMarkupPercent>
  {
    public priceMarkupPercent()
      : base("M")
    {
    }
  }

  public class percentOfSalesPrice : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CostBasisOption.percentOfSalesPrice>
  {
    public percentOfSalesPrice()
      : base("P")
    {
    }
  }

  public class undefinedCostBasis : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CostBasisOption.undefinedCostBasis>
  {
    public undefinedCostBasis()
      : base("U")
    {
    }
  }
}
