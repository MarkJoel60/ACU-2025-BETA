// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.FreightAmountSourceAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

public class FreightAmountSourceAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string ShipmentBased = "S";
  public const string OrderBased = "O";

  public FreightAmountSourceAttribute()
    : base(new Tuple<string, string>[2]
    {
      PXStringListAttribute.Pair("S", "Shipment"),
      PXStringListAttribute.Pair("O", "Sales Order")
    })
  {
  }

  public class shipmentBased : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FreightAmountSourceAttribute.shipmentBased>
  {
    public shipmentBased()
      : base("S")
    {
    }
  }

  public class orderBased : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FreightAmountSourceAttribute.orderBased>
  {
    public orderBased()
      : base("O")
    {
    }
  }
}
