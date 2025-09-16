// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPlanningMethod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INPlanningMethod
{
  public const 
  #nullable disable
  string None = "N";
  public const string DRP = "D";
  public const string MRP = "M";
  public const string InventoryReplenishment = "R";

  public class List : PXStringListAttribute
  {
    public List()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("N", "None"),
        PXStringListAttribute.Pair("D", "DRP"),
        PXStringListAttribute.Pair("M", "MRP"),
        PXStringListAttribute.Pair("R", "Inventory Replenishment")
      })
    {
    }
  }

  /// <summary>Contract level InventoryReplenishment</summary>
  public class inventoryReplenishment : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INPlanningMethod.inventoryReplenishment>
  {
    public inventoryReplenishment()
      : base("R")
    {
    }
  }

  /// <summary>Contract level None</summary>
  public class none : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanningMethod.none>
  {
    public none()
      : base("N")
    {
    }
  }

  /// <summary>Contract level DRP</summary>
  public class dRP : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanningMethod.dRP>
  {
    public dRP()
      : base("D")
    {
    }
  }

  /// <summary>Contract level MRP</summary>
  public class mRP : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanningMethod.mRP>
  {
    public mRP()
      : base("M")
    {
    }
  }
}
