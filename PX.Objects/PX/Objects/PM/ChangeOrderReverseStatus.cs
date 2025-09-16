// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeOrderReverseStatus
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
public static class ChangeOrderReverseStatus
{
  public const 
  #nullable disable
  string None = "N";
  public const string Reversed = "X";
  public const string Reversal = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[3]{ "N", "X", "R" }, new string[3]
      {
        "None",
        "Reversed",
        "Reversing"
      })
    {
    }
  }

  public class reversed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ChangeOrderReverseStatus.reversed>
  {
    public reversed()
      : base("X")
    {
    }
  }

  public class reversal : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ChangeOrderReverseStatus.reversal>
  {
    public reversal()
      : base("R")
    {
    }
  }

  public class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ChangeOrderReverseStatus.none>
  {
    public none()
      : base("N")
    {
    }
  }
}
