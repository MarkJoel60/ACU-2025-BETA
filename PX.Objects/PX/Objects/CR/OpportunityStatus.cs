// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OpportunityStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// Statuses for <see cref="T:PX.Objects.CR.CROpportunity.status" /> used by default in system workflow.
/// Values could be changed and extended by workflow.
/// </summary>
public class OpportunityStatus
{
  public const 
  #nullable disable
  string New = "N";
  public const string Open = "O";
  public const string Won = "W";
  public const string Lost = "L";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new (string, string)[4]
      {
        ("N", "New"),
        ("O", "Open"),
        ("W", "Won"),
        ("L", "Lost")
      })
    {
    }
  }

  public class @new : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  OpportunityStatus.@new>
  {
    public @new()
      : base("N")
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  OpportunityStatus.open>
  {
    public open()
      : base("O")
    {
    }
  }

  public class won : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  OpportunityStatus.won>
  {
    public won()
      : base("W")
    {
    }
  }

  public class lost : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  OpportunityStatus.lost>
  {
    public lost()
      : base("L")
    {
    }
  }
}
