// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCommitmentStatus
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
public static class PMCommitmentStatus
{
  public const 
  #nullable disable
  string Open = "O";
  public const string Closed = "C";
  public const string Canceled = "X";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "O", "C", "X" }, new string[3]
      {
        "Open",
        "Closed",
        "Canceled"
      })
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMCommitmentStatus.open>
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
  PMCommitmentStatus.closed>
  {
    public closed()
      : base("C")
    {
    }
  }

  public class canceled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMCommitmentStatus.canceled>
  {
    public canceled()
      : base("X")
    {
    }
  }
}
