// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CashDiscountBases
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public static class CashDiscountBases
{
  public const 
  #nullable disable
  string DocumentAmount = "DA";
  public const string DocumentAmountLessTaxes = "TA";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "DA", "TA" }, new string[2]
      {
        "Document Amount",
        "Document Amount Less Taxes"
      })
    {
    }
  }

  public class documentAmount : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CashDiscountBases.documentAmount>
  {
    public documentAmount()
      : base("DA")
    {
    }
  }

  public class documentAmountLessTaxes : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CashDiscountBases.documentAmountLessTaxes>
  {
    public documentAmountLessTaxes()
      : base("TA")
    {
    }
  }
}
