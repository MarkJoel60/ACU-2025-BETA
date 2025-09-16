// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPackageOption
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INPackageOption
{
  public const 
  #nullable disable
  string Manual = "N";
  public const string Weight = "W";
  public const string Quantity = "Q";
  public const string WeightAndVolume = "V";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("N", "Manual"),
        PXStringListAttribute.Pair("W", "By Weight"),
        PXStringListAttribute.Pair("Q", "By Quantity"),
        PXStringListAttribute.Pair("V", "By Weight & Volume")
      })
    {
    }
  }

  public class manual : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPackageOption.manual>
  {
    public manual()
      : base("N")
    {
    }
  }

  public class weight : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPackageOption.weight>
  {
    public weight()
      : base("W")
    {
    }
  }

  public class quantity : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPackageOption.quantity>
  {
    public quantity()
      : base("Q")
    {
    }
  }

  public class weightAndVolume : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INPackageOption.weightAndVolume>
  {
    public weightAndVolume()
      : base("V")
    {
    }
  }
}
