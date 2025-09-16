// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.WatchType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CT;

public static class WatchType
{
  public const 
  #nullable disable
  string All = "A";
  public const string ContractRenewed = "R";
  public const string ContractExpired = "X";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "A", "R", "X" }, new string[3]
      {
        "All",
        "Contract Renewed",
        "Contract Expired"
      })
    {
    }
  }

  public class WatchAll : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  WatchType.WatchAll>
  {
    public WatchAll()
      : base("A")
    {
    }
  }

  public class WatchContractRenewed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WatchType.WatchContractRenewed>
  {
    public WatchContractRenewed()
      : base("R")
    {
    }
  }

  public class WatchContractExpired : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    WatchType.WatchContractExpired>
  {
    public WatchContractExpired()
      : base("X")
    {
    }
  }
}
