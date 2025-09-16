// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAmountOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public static class INAmountOption
{
  public const 
  #nullable disable
  string Percentage = "P";
  public const string FixedAmt = "F";
  public const string Residual = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("P", "Percentage"),
        PXStringListAttribute.Pair("F", "Fixed Amount"),
        PXStringListAttribute.Pair("R", "Residual")
      })
    {
    }
  }

  public class percentage : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAmountOption.percentage>
  {
    public percentage()
      : base("P")
    {
    }
  }

  public class fixedAmt : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAmountOption.fixedAmt>
  {
    public fixedAmt()
      : base("F")
    {
    }
  }

  public class residual : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAmountOption.residual>
  {
    public residual()
      : base("R")
    {
    }
  }
}
