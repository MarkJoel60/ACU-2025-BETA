// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.CostProjectionStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.CN.ProjectAccounting;

[ExcludeFromCodeCoverage]
public static class CostProjectionStatus
{
  public const 
  #nullable disable
  string OnHold = "H";
  public const string PendingApproval = "A";
  public const string Open = "O";
  public const string Released = "C";
  public const string Rejected = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[5]{ "H", "A", "O", "C", "R" }, new string[5]
      {
        "On Hold",
        "Pending Approval",
        "Open",
        "Released",
        "Rejected"
      })
    {
    }
  }

  public class onHold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CostProjectionStatus.onHold>
  {
    public onHold()
      : base("H")
    {
    }
  }

  public class pendingApproval : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CostProjectionStatus.pendingApproval>
  {
    public pendingApproval()
      : base("A")
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CostProjectionStatus.open>
  {
    public open()
      : base("O")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CostProjectionStatus.rejected>
  {
    public rejected()
      : base("R")
    {
    }
  }

  public class released : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CostProjectionStatus.released>
  {
    public released()
      : base("C")
    {
    }
  }
}
