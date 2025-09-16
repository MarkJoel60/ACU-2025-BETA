// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.BillingType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CT;

public static class BillingType
{
  public const 
  #nullable disable
  string Statement = "S";
  public const string Quarterly = "Q";
  public const string Monthly = "M";
  public const string Annual = "A";
  public const string SemiAnnual = "6";
  public const string Weekly = "W";
  public const string OnDemand = "D";

  public class ListForProjectAttribute : PXStringListAttribute
  {
    public ListForProjectAttribute()
      : base(new string[5]{ "W", "M", "Q", "A", "D" }, new string[5]
      {
        "Week",
        "Month",
        "Quarter",
        "Year",
        "On Demand"
      })
    {
    }
  }

  public class ListForContractAttribute : PXStringListAttribute
  {
    public ListForContractAttribute()
      : base(new string[7]
      {
        "W",
        "M",
        "Q",
        "6",
        "A",
        "D",
        "S"
      }, new string[7]
      {
        "Week",
        "Month",
        "Quarter",
        "Half a Year",
        "Year",
        "On Demand",
        "Statement-Based"
      })
    {
    }
  }

  public class BillingStatement : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BillingType.BillingStatement>
  {
    public BillingStatement()
      : base("S")
    {
    }
  }

  public class BillingQuarterly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BillingType.BillingQuarterly>
  {
    public BillingQuarterly()
      : base("Q")
    {
    }
  }

  public class BillingMonthly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BillingType.BillingMonthly>
  {
    public BillingMonthly()
      : base("M")
    {
    }
  }

  public class BillingAnnual : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BillingType.BillingAnnual>
  {
    public BillingAnnual()
      : base("A")
    {
    }
  }

  public class BillingSemiAnnual : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    BillingType.BillingSemiAnnual>
  {
    public BillingSemiAnnual()
      : base("6")
    {
    }
  }

  public class BillingWeekly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BillingType.BillingWeekly>
  {
    public BillingWeekly()
      : base("W")
    {
    }
  }

  public class BillingOnDemand : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  BillingType.BillingOnDemand>
  {
    public BillingOnDemand()
      : base("D")
    {
    }
  }
}
