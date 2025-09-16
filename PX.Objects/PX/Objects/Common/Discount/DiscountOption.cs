// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.DiscountOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.Common.Discount;

public static class DiscountOption
{
  public const 
  #nullable disable
  string Percent = "P";
  public const string Amount = "A";
  public const string FreeItem = "F";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("P", "Percent"),
        PXStringListAttribute.Pair("A", "Amount"),
        PXStringListAttribute.Pair("F", "Free Item")
      })
    {
    }
  }

  public class PercentDiscount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountOption.PercentDiscount>
  {
    public PercentDiscount()
      : base("P")
    {
    }
  }

  public class AmountDiscount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DiscountOption.AmountDiscount>
  {
    public AmountDiscount()
      : base("A")
    {
    }
  }

  public class FreeItemDiscount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DiscountOption.FreeItemDiscount>
  {
    public FreeItemDiscount()
      : base("F")
    {
    }
  }
}
