// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCompletedPctMethod
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
public static class PMCompletedPctMethod
{
  public const 
  #nullable disable
  string Manual = "M";
  public const string ByQuantity = "Q";
  public const string ByAmount = "A";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("M", "Manual"),
        PXStringListAttribute.Pair("Q", "Budgeted Quantity"),
        PXStringListAttribute.Pair("A", "Budgeted Amount")
      })
    {
    }
  }

  public class manual : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMCompletedPctMethod.manual>
  {
    public manual()
      : base("M")
    {
    }
  }

  public class byQuantity : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMCompletedPctMethod.byQuantity>
  {
    public byQuantity()
      : base("Q")
    {
    }
  }

  public class byAmount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  PMCompletedPctMethod.byAmount>
  {
    public byAmount()
      : base("A")
    {
    }
  }
}
