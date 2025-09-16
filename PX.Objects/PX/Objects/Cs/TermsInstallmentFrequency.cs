// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.TermsInstallmentFrequency
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CS;

public class TermsInstallmentFrequency
{
  public const 
  #nullable disable
  string Weekly = "W";
  public const string Monthly = "M";
  public const string SemiMonthly = "B";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "W", "M", "B" }, new string[3]
      {
        "Weekly",
        "Monthly",
        "Semi-Monthly"
      })
    {
    }
  }

  public class weekly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsInstallmentFrequency.weekly>
  {
    public weekly()
      : base("W")
    {
    }
  }

  public class monthly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  TermsInstallmentFrequency.monthly>
  {
    public monthly()
      : base("M")
    {
    }
  }

  public class semiMonthly : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    TermsInstallmentFrequency.semiMonthly>
  {
    public semiMonthly()
      : base("B")
    {
    }
  }
}
