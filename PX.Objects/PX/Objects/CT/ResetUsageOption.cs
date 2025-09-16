// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ResetUsageOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CT;

public static class ResetUsageOption
{
  public const 
  #nullable disable
  string Never = "N";
  public const string OnBilling = "B";
  public const string OnRenewal = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "N", "B", "R" }, new string[3]
      {
        "Never",
        "On Billing",
        "On Renewal"
      })
    {
    }
  }

  public class ListForProjectAttribute : PXStringListAttribute
  {
    public ListForProjectAttribute()
      : base(new string[2]{ "N", "B" }, new string[2]
      {
        "Never",
        "On Billing"
      })
    {
    }
  }

  public class onBilling : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ResetUsageOption.onBilling>
  {
    public onBilling()
      : base("B")
    {
    }
  }

  public class onRenewal : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ResetUsageOption.onRenewal>
  {
    public onRenewal()
      : base("R")
    {
    }
  }
}
