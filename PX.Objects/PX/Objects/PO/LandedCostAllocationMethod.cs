// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCostAllocationMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO;

public static class LandedCostAllocationMethod
{
  public const 
  #nullable disable
  string ByQuantity = "Q";
  public const string ByCost = "C";
  public const string ByWeight = "W";
  public const string ByVolume = "V";
  public const string None = "N";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("Q", "By Quantity"),
        PXStringListAttribute.Pair("C", "By Cost"),
        PXStringListAttribute.Pair("W", "By Weight"),
        PXStringListAttribute.Pair("V", "By Volume"),
        PXStringListAttribute.Pair("N", "None")
      })
    {
    }
  }

  public class byQuantity : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    LandedCostAllocationMethod.byQuantity>
  {
    public byQuantity()
      : base("Q")
    {
    }
  }

  public class byCost : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LandedCostAllocationMethod.byCost>
  {
    public byCost()
      : base("C")
    {
    }
  }

  public class byWeight : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LandedCostAllocationMethod.byWeight>
  {
    public byWeight()
      : base("W")
    {
    }
  }

  public class byVolume : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LandedCostAllocationMethod.byVolume>
  {
    public byVolume()
      : base("V")
    {
    }
  }

  public class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  LandedCostAllocationMethod.none>
  {
    public none()
      : base("N")
    {
    }
  }
}
