// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.TermsInstallmentMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public class TermsInstallmentMethod
{
  public const 
  #nullable disable
  string EqualParts = "E";
  public const string AllTaxInFirst = "A";
  public const string SplitByPercents = "S";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "E", "A", "S" }, new string[3]
      {
        "Equal Parts",
        "Tax in First Installment",
        "Split by Percent in Table"
      })
    {
    }
  }

  public class equalParts : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsInstallmentMethod.equalParts>
  {
    public equalParts()
      : base("E")
    {
    }
  }

  public class allTaxInFirst : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    TermsInstallmentMethod.allTaxInFirst>
  {
    public allTaxInFirst()
      : base("A")
    {
    }
  }

  public class splitByPercents : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    TermsInstallmentMethod.splitByPercents>
  {
    public splitByPercents()
      : base("S")
    {
    }
  }
}
