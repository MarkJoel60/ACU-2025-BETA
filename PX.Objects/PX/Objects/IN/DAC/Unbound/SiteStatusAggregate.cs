// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Unbound.SiteStatusAggregate
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
public class SiteStatusAggregate : 
  PXBqlTable,
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

  [PXDecimal]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyINReplaned { get; set; }

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

  public interface ITable : IBqlTable, IBqlTableSystemDataStorage, IStatus, ICostStatus
  {
    int? InventoryID { get; }

    int? SubItemID { get; }

    string SubItemCD { get; }

    int? SiteID { get; }

    string SiteCD { get; }

    int? CostCenterID { get; }

    string CostLayerType { get; }

    string BaseUnit { get; }

    Decimal? UnitCost { get; set; }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteStatusAggregate.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteStatusAggregate.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteStatusAggregate.siteID>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SiteStatusAggregate.costCenterID>
  {
  }

  public abstract class excludeRecord : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SiteStatusAggregate.excludeRecord>
  {
  }

  public abstract class costLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SiteStatusAggregate.costLayerType>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteStatusAggregate.siteCD>
  {
  }

  public abstract class subItemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteStatusAggregate.subItemCD>
  {
  }

  public abstract class baseUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SiteStatusAggregate.baseUnit>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyOnHand>
  {
  }

  public abstract class qtyAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SiteStatusAggregate.qtyAvail>
  {
  }

  public abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyNotAvail>
  {
  }

  public abstract class qtyExpired : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyExpired>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyHardAvail>
  {
  }

  public abstract class qtyActual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyActual>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyInTransit>
  {
  }

  public abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyInTransitToSO>
  {
  }

  public abstract class qtyINReplaned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyINReplaned>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPOPrepared>
  {
  }

  public abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPOOrders>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPOReceipts>
  {
  }

  public abstract class qtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyFSSrvOrdBooked>
  {
  }

  public abstract class qtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyFSSrvOrdAllocated>
  {
  }

  public abstract class qtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyFSSrvOrdPrepared>
  {
  }

  public abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtySOBackOrdered>
  {
  }

  public abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtySOPrepared>
  {
  }

  public abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtySOBooked>
  {
  }

  public abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtySOShipped>
  {
  }

  public abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtySOShipping>
  {
  }

  public abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyINIssues>
  {
  }

  public abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyINReceipts>
  {
  }

  public abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyINAssemblyDemand>
  {
  }

  public abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyINAssemblySupply>
  {
  }

  public abstract class qtyInTransitToProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyInTransitToProduction>
  {
  }

  public abstract class qtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyProductionSupplyPrepared>
  {
  }

  public abstract class qtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyProductionSupply>
  {
  }

  public abstract class qtyPOFixedProductionPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPOFixedProductionPrepared>
  {
  }

  public abstract class qtyPOFixedProductionOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPOFixedProductionOrders>
  {
  }

  public abstract class qtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyProductionDemandPrepared>
  {
  }

  public abstract class qtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyProductionDemand>
  {
  }

  public abstract class qtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyProductionAllocated>
  {
  }

  public abstract class qtySOFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtySOFixedProduction>
  {
  }

  public abstract class qtyFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPOFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPOFixedFSSrvOrdPrepared>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPOFixedFSSrvOrdReceipts>
  {
  }

  public abstract class qtyProdFixedPurchase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyProdFixedPurchase>
  {
  }

  public abstract class qtyProdFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyProdFixedProduction>
  {
  }

  public abstract class qtyProdFixedProdOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyProdFixedProdOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedProdOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyProdFixedProdOrders>
  {
  }

  public abstract class qtyProdFixedSalesOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyProdFixedSalesOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedSalesOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyProdFixedSalesOrders>
  {
  }

  public abstract class qtySOFixed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtySOFixed>
  {
  }

  public abstract class qtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPOFixedOrders>
  {
  }

  public abstract class qtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPOFixedPrepared>
  {
  }

  public abstract class qtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPOFixedReceipts>
  {
  }

  public abstract class qtySODropShip : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtySODropShip>
  {
  }

  public abstract class qtyPODropShipOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPODropShipOrders>
  {
  }

  public abstract class qtyPODropShipPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPODropShipPrepared>
  {
  }

  public abstract class qtyPODropShipReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.qtyPODropShipReceipts>
  {
  }

  public abstract class totalCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SiteStatusAggregate.totalCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SiteStatusAggregate.unitCost>
  {
  }
}
