// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLineType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.SO;

public class SOLineType
{
  public const 
  #nullable disable
  string Inventory = "GI";
  public const string NonInventory = "GN";
  public const string MiscCharge = "MI";
  public const string Freight = "FR";
  public const string Discount = "DS";
  public const string Reallocation = "RA";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[3]
      {
        PXStringListAttribute.Pair("GI", "Goods for Inventory"),
        PXStringListAttribute.Pair("GN", "Non-Inventory Goods"),
        PXStringListAttribute.Pair("MI", "Misc. Charge")
      })
    {
    }
  }

  public class inventory : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOLineType.inventory>
  {
    public inventory()
      : base("GI")
    {
    }
  }

  public class nonInventory : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOLineType.nonInventory>
  {
    public nonInventory()
      : base("GN")
    {
    }
  }

  public class miscCharge : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOLineType.miscCharge>
  {
    public miscCharge()
      : base("MI")
    {
    }
  }

  public class freight : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOLineType.freight>
  {
    public freight()
      : base("FR")
    {
    }
  }

  public class discount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOLineType.discount>
  {
    public discount()
      : base("DS")
    {
    }
  }

  public class reallocation : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  SOLineType.reallocation>
  {
    public reallocation()
      : base("RA")
    {
    }
  }
}
