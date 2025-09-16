// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeOrderStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class ChangeOrderStatus
{
  public const 
  #nullable disable
  string OnHold = "H";
  public const string PendingApproval = "A";
  public const string Open = "O";
  public const string Closed = "C";
  public const string Rejected = "R";
  public const string Canceled = "L";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[6]{ "H", "A", "O", "C", "R", "L" }, new string[6]
      {
        "On Hold",
        "Pending Approval",
        "Open",
        "Closed",
        "Rejected",
        "Canceled"
      })
    {
    }
  }

  public class onHold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ChangeOrderStatus.onHold>
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
    ChangeOrderStatus.pendingApproval>
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
  ChangeOrderStatus.open>
  {
    public open()
      : base("O")
    {
    }
  }

  public class closed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ChangeOrderStatus.closed>
  {
    public closed()
      : base("C")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ChangeOrderStatus.rejected>
  {
    public rejected()
      : base("R")
    {
    }
  }

  public class canceled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ChangeOrderStatus.canceled>
  {
    public canceled()
      : base("L")
    {
    }
  }
}
