// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.VendorTaxPeriodType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.TX;

public class VendorTaxPeriodType
{
  public const 
  #nullable disable
  string Monthly = "M";
  public const string SemiMonthly = "B";
  public const string Quarterly = "Q";
  public const string Yearly = "Y";
  public const string FiscalPeriod = "F";
  public const string BiMonthly = "E";
  public const string SemiAnnually = "H";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[7]
      {
        "B",
        "M",
        "E",
        "Q",
        "H",
        "Y",
        "F"
      }, new string[7]
      {
        "Half a Month",
        "Month",
        "Two Months",
        "Quarter",
        "Half a Year",
        "Year",
        "Financial Period"
      })
    {
    }
  }

  public class monthly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VendorTaxPeriodType.monthly>
  {
    public monthly()
      : base("M")
    {
    }
  }

  public class semiMonthly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VendorTaxPeriodType.semiMonthly>
  {
    public semiMonthly()
      : base("B")
    {
    }
  }

  public class quarterly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VendorTaxPeriodType.quarterly>
  {
    public quarterly()
      : base("Q")
    {
    }
  }

  public class yearly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VendorTaxPeriodType.yearly>
  {
    public yearly()
      : base("Y")
    {
    }
  }

  public class fiscalPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VendorTaxPeriodType.fiscalPeriod>
  {
    public fiscalPeriod()
      : base("F")
    {
    }
  }

  public class biMonthly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VendorTaxPeriodType.biMonthly>
  {
    public biMonthly()
      : base("E")
    {
    }
  }

  public class semiAnnually : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VendorTaxPeriodType.semiAnnually>
  {
    public semiAnnually()
      : base("H")
    {
    }
  }
}
