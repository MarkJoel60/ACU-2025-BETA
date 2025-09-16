// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerialStatusInTransit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXHidden]
public class INLotSerialStatusInTransit : INLotSerialStatusByCostCenter
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  public new abstract class inventoryID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    INLotSerialStatusInTransit.inventoryID>
  {
  }

  public new abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLotSerialStatusInTransit.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLotSerialStatusInTransit.siteID>
  {
  }

  public new abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INLotSerialStatusInTransit.locationID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerialStatusInTransit.lotSerialNbr>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyOnHand>
  {
  }

  public new abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyAvail>
  {
  }

  public new abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyNotAvail>
  {
  }

  public new abstract class qtyExpired : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyExpired>
  {
  }

  public new abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyHardAvail>
  {
  }

  public new abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyInTransit>
  {
  }

  public new abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyInTransitToSO>
  {
  }

  public new abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyPOPrepared>
  {
  }

  public new abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyPOOrders>
  {
  }

  public new abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyPOReceipts>
  {
  }

  public new abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtySOBackOrdered>
  {
  }

  public new abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtySOPrepared>
  {
  }

  public new abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtySOBooked>
  {
  }

  public new abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtySOShipped>
  {
  }

  public new abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtySOShipping>
  {
  }

  public new abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyINIssues>
  {
  }

  public new abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyINReceipts>
  {
  }

  public new abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyINAssemblyDemand>
  {
  }

  public new abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyINAssemblySupply>
  {
  }

  public new abstract class qtyInTransitToProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyInTransitToProduction>
  {
  }

  public new abstract class qtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyProductionSupplyPrepared>
  {
  }

  public new abstract class qtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyProductionSupply>
  {
  }

  public new abstract class qtyPOFixedProductionPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyPOFixedProductionPrepared>
  {
  }

  public new abstract class qtyPOFixedProductionOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyPOFixedProductionOrders>
  {
  }

  public new abstract class qtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyProductionDemandPrepared>
  {
  }

  public new abstract class qtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyProductionDemand>
  {
  }

  public new abstract class qtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyProductionAllocated>
  {
  }

  public new abstract class qtySOFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtySOFixedProduction>
  {
  }

  public new abstract class qtyProdFixedPurchase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyProdFixedPurchase>
  {
  }

  public new abstract class qtyProdFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyProdFixedProduction>
  {
  }

  public new abstract class qtyProdFixedProdOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyProdFixedProdOrdersPrepared>
  {
  }

  public new abstract class qtyProdFixedProdOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyProdFixedProdOrders>
  {
  }

  public new abstract class qtyProdFixedSalesOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyProdFixedSalesOrdersPrepared>
  {
  }

  public new abstract class qtyProdFixedSalesOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyProdFixedSalesOrders>
  {
  }

  public new abstract class qtySOFixed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtySOFixed>
  {
  }

  public new abstract class qtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyPOFixedOrders>
  {
  }

  public new abstract class qtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyPOFixedPrepared>
  {
  }

  public new abstract class qtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyPOFixedReceipts>
  {
  }

  public new abstract class qtySODropShip : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtySODropShip>
  {
  }

  public new abstract class qtyPODropShipOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyPODropShipOrders>
  {
  }

  public new abstract class qtyPODropShipPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyPODropShipPrepared>
  {
  }

  public new abstract class qtyPODropShipReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatusInTransit.qtyPODropShipReceipts>
  {
  }

  public new abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerialStatusInTransit.expireDate>
  {
  }

  public new abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerialStatusInTransit.receiptDate>
  {
  }

  public new abstract class lotSerTrack : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerialStatusInTransit.lotSerTrack>
  {
  }

  public new abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    INLotSerialStatusInTransit.Tstamp>
  {
  }
}
