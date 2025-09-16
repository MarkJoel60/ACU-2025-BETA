// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INAmountOptionASC606
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public static class INAmountOptionASC606
{
  public const 
  #nullable disable
  string FairValue = "F";
  public const string Residual = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[2]
      {
        PXStringListAttribute.Pair("F", "Fair Value"),
        PXStringListAttribute.Pair("R", "Residual")
      })
    {
    }
  }

  public class fairValue : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAmountOptionASC606.fairValue>
  {
    public fairValue()
      : base("F")
    {
    }
  }

  public class residual : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INAmountOptionASC606.residual>
  {
    public residual()
      : base("R")
    {
    }
  }
}
