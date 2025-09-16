// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Unbound.LocationStatusAggregate
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
public class LocationStatusAggregate : 
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

  [PXInt(IsKey = true)]
  public virtual int? CostCenterID { get; set; }

  [PXBool(IsKey = true)]
  public virtual bool? ExcludeRecord { get; set; }

  [PXString(1)]
  public virtual 
  #nullable disable
  string CostLayerType { get; set; }

  [PXString(IsUnicode = true)]
  public virtual string SiteCD { get; set; }

  [PXString(IsUnicode = true)]
  public virtual string SubItemCD { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string LocationCD { get; set; }

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

  public interface ITable : 
    SiteStatusAggregate.ITable,
    IBqlTable,
    IBqlTableSystemDataStorage,
    IStatus,
    ICostStatus
  {
    int? LocationID { get; }

    string LocationCD { get; }
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationStatusAggregate.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationStatusAggregate.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationStatusAggregate.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LocationStatusAggregate.locationID>
  {
  }

  public abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LocationStatusAggregate.costCenterID>
  {
  }

  public abstract class excludeRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    LocationStatusAggregate.excludeRecord>
  {
  }

  public abstract class costLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationStatusAggregate.costLayerType>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LocationStatusAggregate.siteCD>
  {
  }

  public abstract class subItemCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationStatusAggregate.subItemCD>
  {
  }

  public abstract class locationCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationStatusAggregate.locationCD>
  {
  }

  public abstract class baseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LocationStatusAggregate.baseUnit>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyOnHand>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyAvail>
  {
  }

  public abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyNotAvail>
  {
  }

  public abstract class qtyExpired : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyExpired>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyHardAvail>
  {
  }

  public abstract class qtyActual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyActual>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyInTransit>
  {
  }

  public abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyInTransitToSO>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPOPrepared>
  {
  }

  public abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPOOrders>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPOReceipts>
  {
  }

  public abstract class qtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyFSSrvOrdBooked>
  {
  }

  public abstract class qtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyFSSrvOrdAllocated>
  {
  }

  public abstract class qtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyFSSrvOrdPrepared>
  {
  }

  public abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtySOBackOrdered>
  {
  }

  public abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtySOPrepared>
  {
  }

  public abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtySOBooked>
  {
  }

  public abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtySOShipped>
  {
  }

  public abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtySOShipping>
  {
  }

  public abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyINIssues>
  {
  }

  public abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyINReceipts>
  {
  }

  public abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyINAssemblyDemand>
  {
  }

  public abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyINAssemblySupply>
  {
  }

  public abstract class qtyInTransitToProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyInTransitToProduction>
  {
  }

  public abstract class qtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyProductionSupplyPrepared>
  {
  }

  public abstract class qtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyProductionSupply>
  {
  }

  public abstract class qtyPOFixedProductionPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPOFixedProductionPrepared>
  {
  }

  public abstract class qtyPOFixedProductionOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPOFixedProductionOrders>
  {
  }

  public abstract class qtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyProductionDemandPrepared>
  {
  }

  public abstract class qtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyProductionDemand>
  {
  }

  public abstract class qtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyProductionAllocated>
  {
  }

  public abstract class qtySOFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtySOFixedProduction>
  {
  }

  public abstract class qtyFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPOFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPOFixedFSSrvOrdPrepared>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPOFixedFSSrvOrdReceipts>
  {
  }

  public abstract class qtyProdFixedPurchase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyProdFixedPurchase>
  {
  }

  public abstract class qtyProdFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyProdFixedProduction>
  {
  }

  public abstract class qtyProdFixedProdOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyProdFixedProdOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedProdOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyProdFixedProdOrders>
  {
  }

  public abstract class qtyProdFixedSalesOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyProdFixedSalesOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedSalesOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyProdFixedSalesOrders>
  {
  }

  public abstract class qtySOFixed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtySOFixed>
  {
  }

  public abstract class qtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPOFixedOrders>
  {
  }

  public abstract class qtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPOFixedPrepared>
  {
  }

  public abstract class qtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPOFixedReceipts>
  {
  }

  public abstract class qtySODropShip : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtySODropShip>
  {
  }

  public abstract class qtyPODropShipOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPODropShipOrders>
  {
  }

  public abstract class qtyPODropShipPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPODropShipPrepared>
  {
  }

  public abstract class qtyPODropShipReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.qtyPODropShipReceipts>
  {
  }

  public abstract class totalCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.totalCost>
  {
  }

  public abstract class unitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LocationStatusAggregate.unitCost>
  {
  }
}
