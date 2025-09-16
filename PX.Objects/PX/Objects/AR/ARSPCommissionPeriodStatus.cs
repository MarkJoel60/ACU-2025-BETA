// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSPCommissionPeriodStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AR;

public class ARSPCommissionPeriodStatus
{
  public const 
  #nullable disable
  string Prepared = "P";
  public const string Open = "N";
  public const string Closed = "C";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "P", "N", "C" }, new string[3]
      {
        "Prepared",
        "Open",
        "Closed"
      })
    {
    }
  }

  public class prepared : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARSPCommissionPeriodStatus.prepared>
  {
    public prepared()
      : base("P")
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARSPCommissionPeriodStatus.open>
  {
    public open()
      : base("N")
    {
    }
  }

  public class closed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARSPCommissionPeriodStatus.closed>
  {
    public closed()
      : base("C")
    {
    }
  }
}
