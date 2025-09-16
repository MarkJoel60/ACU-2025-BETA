// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARSalesPrice2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXHidden]
public class ARSalesPrice2 : ARSalesPrice
{
  public new abstract class custPriceClassID : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    ARSalesPrice2.custPriceClassID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARSalesPrice2.inventoryID>
  {
  }

  public new abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice2.curyID>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARSalesPrice2.uOM>
  {
  }

  public new abstract class breakQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARSalesPrice2.breakQty>
  {
  }

  public new abstract class salesPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARSalesPrice2.salesPrice>
  {
  }
}
