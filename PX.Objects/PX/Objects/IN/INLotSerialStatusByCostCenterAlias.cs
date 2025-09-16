// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerialStatusByCostCenterAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R2.")]
[PXHidden]
public class INLotSerialStatusByCostCenterAlias : INLotSerialStatusByCostCenter
{
  [PXQuantity]
  [PXFormula(typeof (IsNull<INLotSerialStatusByCostCenterAlias.qtyAvail, decimal0>))]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvailNotNull { get; set; }

  public new abstract class inventoryID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    INLotSerialStatusByCostCenterAlias.inventoryID>
  {
  }

  public new abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLotSerialStatusByCostCenterAlias.subItemID>
  {
  }

  public new abstract class siteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLotSerialStatusByCostCenterAlias.siteID>
  {
  }

  public new abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLotSerialStatusByCostCenterAlias.locationID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerialStatusByCostCenterAlias.lotSerialNbr>
  {
  }

  public new abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostCenterAlias.qtyAvail>
  {
  }

  public abstract class qtyAvailNotNull : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusByCostCenterAlias.qtyAvailNotNull>
  {
  }
}
