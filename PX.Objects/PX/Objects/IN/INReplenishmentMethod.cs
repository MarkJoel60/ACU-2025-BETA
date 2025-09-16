// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReplenishmentMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INReplenishmentMethod
{
  public const 
  #nullable disable
  string None = "N";
  public const string MinMax = "M";
  public const string FixedReorder = "F";

  public class List : PXStringListAttribute
  {
    public List()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("N", "None"),
        PXStringListAttribute.Pair("M", "Min./Max."),
        PXStringListAttribute.Pair("F", "Fixed Reorder Qty")
      })
    {
    }
  }

  public class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INReplenishmentMethod.none>
  {
    public none()
      : base("N")
    {
    }
  }

  public class minMax : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INReplenishmentMethod.minMax>
  {
    public minMax()
      : base("M")
    {
    }
  }

  public class fixedReorder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INReplenishmentMethod.fixedReorder>
  {
    public fixedReorder()
      : base("F")
    {
    }
  }
}
