// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.BreakdownType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Common.Discount;

public static class BreakdownType
{
  public const 
  #nullable disable
  string Quantity = "Q";
  public const string Amount = "A";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("Q", "Quantity"),
        PXStringListAttribute.Pair("A", "Amount")
      })
    {
    }
  }

  public class QuantityBreakdown : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    BreakdownType.QuantityBreakdown>
  {
    public QuantityBreakdown()
      : base("Q")
    {
    }
  }

  public class AmountBreakdown : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BreakdownType.AmountBreakdown>
  {
    public AmountBreakdown()
      : base("A")
    {
    }
  }
}
