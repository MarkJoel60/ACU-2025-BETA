// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.CSTaxTermsDiscount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.TX;

public static class CSTaxTermsDiscount
{
  public const 
  #nullable disable
  string ToTaxableAmount = "X";
  public const string ToPromtPayment = "P";
  public const string ToTaxAmount = "T";
  public const string AdjustTax = "A";
  public const string NoAdjust = "N";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "X", "P", "N" }, new string[3]
      {
        "Reduces Taxable Amount",
        "Reduces Taxable Amount on Early Payment",
        "Does Not Affect Taxable Amount"
      })
    {
    }
  }

  public class toPromtPayment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CSTaxTermsDiscount.toPromtPayment>
  {
    public toPromtPayment()
      : base("P")
    {
    }
  }
}
