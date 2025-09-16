// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractDetailType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CT;

public static class ContractDetailType
{
  public const 
  #nullable disable
  string Setup = "S";
  public const string Renewal = "R";
  public const string Billing = "B";
  public const string UsagePrice = "U";
  public const string Reinstallment = "I";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[5]{ "S", "R", "B", "U", "I" }, new string[5]
      {
        "Setup",
        "Renewal",
        "Billing",
        "Usage",
        "Re-Installment"
      })
    {
    }
  }

  public class ContractDetailSetup : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ContractDetailType.ContractDetailSetup>
  {
    public ContractDetailSetup()
      : base("S")
    {
    }
  }

  public class ContractDetailRenewal : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ContractDetailType.ContractDetailRenewal>
  {
    public ContractDetailRenewal()
      : base("R")
    {
    }
  }

  public class ContractDetail : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ContractDetailType.ContractDetail>
  {
    public ContractDetail()
      : base("B")
    {
    }
  }

  public class ContractDetailUsagePrice : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ContractDetailType.ContractDetailUsagePrice>
  {
    public ContractDetailUsagePrice()
      : base("U")
    {
    }
  }

  public class ContractDetailReinstallment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ContractDetailType.ContractDetailReinstallment>
  {
    public ContractDetailReinstallment()
      : base("I")
    {
    }
  }
}
