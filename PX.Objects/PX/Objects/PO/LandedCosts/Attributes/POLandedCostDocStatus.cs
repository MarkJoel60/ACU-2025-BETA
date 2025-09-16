// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.Attributes.POLandedCostDocStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO.LandedCosts.Attributes;

public class POLandedCostDocStatus
{
  public const 
  #nullable disable
  string Initial = "_";
  public const string Hold = "H";
  public const string Balanced = "B";
  public const string Released = "R";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("H", "On Hold"),
        PXStringListAttribute.Pair("B", "Balanced"),
        PXStringListAttribute.Pair("R", "Released")
      })
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLandedCostDocStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class balanced : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLandedCostDocStatus.balanced>
  {
    public balanced()
      : base("B")
    {
    }
  }

  public class released : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  POLandedCostDocStatus.released>
  {
    public released()
      : base("R")
    {
    }
  }
}
