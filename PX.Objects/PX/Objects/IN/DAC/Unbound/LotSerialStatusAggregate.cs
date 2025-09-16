// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Unbound.LotSerialStatusAggregate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN.DAC.Unbound;

[PXHidden]
public class LotSerialStatusAggregate : 
  PXBqlTable,
  LocationStatusAggregate.ITable,
  SiteStatusAggregate.ITable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IStatus,
  ICostStatus
{
  [PXInt(IsKey = true)]
  public virtual int? InventoryID { get; set; }

  [PXInt(IsKey = true)]
  public virtual int? SubItemID { get; set; }

  [PXInt(IsKey = true)]
  public virtual int? SiteID { get; set; }

  [PXInt(IsKey = true)]
  public virtual int? LocationID { get; set; }

  [PXString(100, IsUnicode = true, IsKey = true)]
  public virtual 
  #nullable disable
  string LotSerialNbr { get; set; }

  [PXInt(IsKey = true)]
  public virtual int? CostCenterID { get; set; }

  [PXString(1)]
  public virtual string CostLayerType { get; set; }

  [PXString(IsUnicode = true)]
  public virtual string SiteCD { get; set; }

  [PXString(IsUnicode = true)]
  public virtual string SubItemCD { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string LocationCD { get; set; }

  [PXDate]
  public virtual DateTime? ExpireDate { get; set; }

  [PXString(6, IsUnicode = true)]
  public virtual string BaseUnit { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyOnHand { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyAvail { get; set; }

  [PXDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyNotAvail { get; set; }

  [PXDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyExpired { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyHardAvail { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyActual { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyInTransit { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyInTransitToSO { get; set; }

  public Decimal? QtyINReplaned
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOPrepared { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOOrders { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOReceipts { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyFSSrvOrdBooked { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyFSSrvOrdAllocated { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyFSSrvOrdPrepared { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOBackOrdered { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOPrepared { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOBooked { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOShipped { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOShipping { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyINIssues { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyINReceipts { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyINAssemblyDemand { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyINAssemblySupply { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity In Transit to Production.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyInTransitToProduction { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity Production Supply Prepared.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyProductionSupplyPrepared { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity Production Supply.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyProductionSupply { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Purchase for Prod. Prepared.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedProductionPrepared { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Purchase for Production.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedProductionOrders { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production Demand Prepared.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyProductionDemandPrepared { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production Demand.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyProductionDemand { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production Allocated.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyProductionAllocated { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On SO to Production.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOFixedProduction { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyFixedFSSrvOrd { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedFSSrvOrd { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedFSSrvOrdPrepared { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedFSSrvOrdReceipts { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production to Purchase.
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyProdFixedPurchase { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production to Production
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyProdFixedProduction { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for Prod. Prepared
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyProdFixedProdOrdersPrepared { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for Production
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyProdFixedProdOrders { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for SO Prepared
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyProdFixedSalesOrdersPrepared { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for SO
  /// </summary>
  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyProdFixedSalesOrders { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOFixed { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedOrders { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedPrepared { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedReceipts { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySODropShip { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipOrders { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipPrepared { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipReceipts { get; set; }

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalCost { get; set; }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitCost { get; set; }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LotSerialStatusAggregate.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LotSerialStatusAggregate.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LotSerialStatusAggregate.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LotSerialStatusAggregate.locationID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LotSerialStatusAggregate.lotSerialNbr>
  {
  }

  public abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LotSerialStatusAggregate.costCenterID>
  {
  }

  public abstract class costLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LotSerialStatusAggregate.costLayerType>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LotSerialStatusAggregate.siteCD>
  {
  }

  public abstract class subItemCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LotSerialStatusAggregate.subItemCD>
  {
  }

  public abstract class locationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LotSerialStatusAggregate.locationCD>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LotSerialStatusAggregate.expireDate>
  {
  }

  public abstract class baseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LotSerialStatusAggregate.baseUnit>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyOnHand>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyAvail>
  {
  }

  public abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyNotAvail>
  {
  }

  public abstract class qtyExpired : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyExpired>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyHardAvail>
  {
  }

  public abstract class qtyActual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyActual>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyInTransit>
  {
  }

  public abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyInTransitToSO>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPOPrepared>
  {
  }

  public abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPOOrders>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPOReceipts>
  {
  }

  public abstract class qtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyFSSrvOrdBooked>
  {
  }

  public abstract class qtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyFSSrvOrdAllocated>
  {
  }

  public abstract class qtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyFSSrvOrdPrepared>
  {
  }

  public abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtySOBackOrdered>
  {
  }

  public abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtySOPrepared>
  {
  }

  public abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtySOBooked>
  {
  }

  public abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtySOShipped>
  {
  }

  public abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtySOShipping>
  {
  }

  public abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyINIssues>
  {
  }

  public abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyINReceipts>
  {
  }

  public abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyINAssemblyDemand>
  {
  }

  public abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyINAssemblySupply>
  {
  }

  public abstract class qtyInTransitToProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyInTransitToProduction>
  {
  }

  public abstract class qtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyProductionSupplyPrepared>
  {
  }

  public abstract class qtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyProductionSupply>
  {
  }

  public abstract class qtyPOFixedProductionPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPOFixedProductionPrepared>
  {
  }

  public abstract class qtyPOFixedProductionOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPOFixedProductionOrders>
  {
  }

  public abstract class qtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyProductionDemandPrepared>
  {
  }

  public abstract class qtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyProductionDemand>
  {
  }

  public abstract class qtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyProductionAllocated>
  {
  }

  public abstract class qtySOFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtySOFixedProduction>
  {
  }

  public abstract class qtyFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPOFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPOFixedFSSrvOrdPrepared>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPOFixedFSSrvOrdReceipts>
  {
  }

  public abstract class qtyProdFixedPurchase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyProdFixedPurchase>
  {
  }

  public abstract class qtyProdFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyProdFixedProduction>
  {
  }

  public abstract class qtyProdFixedProdOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyProdFixedProdOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedProdOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyProdFixedProdOrders>
  {
  }

  public abstract class qtyProdFixedSalesOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyProdFixedSalesOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedSalesOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyProdFixedSalesOrders>
  {
  }

  public abstract class qtySOFixed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtySOFixed>
  {
  }

  public abstract class qtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPOFixedOrders>
  {
  }

  public abstract class qtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPOFixedPrepared>
  {
  }

  public abstract class qtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPOFixedReceipts>
  {
  }

  public abstract class qtySODropShip : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtySODropShip>
  {
  }

  public abstract class qtyPODropShipOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPODropShipOrders>
  {
  }

  public abstract class qtyPODropShipPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPODropShipPrepared>
  {
  }

  public abstract class qtyPODropShipReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.qtyPODropShipReceipts>
  {
  }

  public abstract class totalCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.totalCost>
  {
  }

  public abstract class unitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LotSerialStatusAggregate.unitCost>
  {
  }
}
