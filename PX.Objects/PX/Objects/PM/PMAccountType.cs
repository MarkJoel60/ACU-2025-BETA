// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMAccountType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
public static class PMAccountType
{
  public const 
  #nullable disable
  string OffBalance = "O";
  public const string All = "X";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("A", "Asset"),
        PXStringListAttribute.Pair("L", "Liability"),
        PXStringListAttribute.Pair("I", "Income"),
        PXStringListAttribute.Pair("E", "Expense"),
        PXStringListAttribute.Pair("O", "Off-Balance")
      })
    {
    }
  }

  public class FilterListAttribute : PXStringListAttribute
  {
    public FilterListAttribute()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("X", "All"),
        PXStringListAttribute.Pair("A", "Asset"),
        PXStringListAttribute.Pair("L", "Liability"),
        PXStringListAttribute.Pair("I", "Income"),
        PXStringListAttribute.Pair("E", "Expense"),
        PXStringListAttribute.Pair("O", "Off-Balance")
      })
    {
    }
  }

  public class offBalance : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMAccountType.offBalance>
  {
    public offBalance()
      : base("O")
    {
    }
  }

  public class all : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMAccountType.all>
  {
    public all()
      : base("X")
    {
    }
  }
}
