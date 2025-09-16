// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProgressWorksheetStatus
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
public static class ProgressWorksheetStatus
{
  public const 
  #nullable disable
  string OnHold = "H";
  public const string PendingApproval = "A";
  public const string Open = "O";
  public const string Closed = "C";
  public const string Rejected = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[5]{ "H", "A", "O", "C", "R" }, new string[5]
      {
        "On Hold",
        "Pending Approval",
        "Open",
        "Closed",
        "Rejected"
      })
    {
    }
  }

  public class onHold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ProgressWorksheetStatus.onHold>
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
    ProgressWorksheetStatus.pendingApproval>
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
  ProgressWorksheetStatus.open>
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
  ProgressWorksheetStatus.closed>
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
  ProgressWorksheetStatus.rejected>
  {
    public rejected()
      : base("R")
    {
    }
  }
}
