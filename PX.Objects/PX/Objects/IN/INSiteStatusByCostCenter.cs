// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteStatusByCostCenter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Site Status by Cost Center")]
public class INSiteStatusByCostCenter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IStatus
{
  [Inventory(IsKey = true)]
  [PXDefault]
  [ConvertedInventoryItem(true)]
  public virtual int? InventoryID { get; set; }

  [SubItem(IsKey = true)]
  [PXDefault]
  public virtual int? SubItemID { get; set; }

  [Site(IsKey = true)]
  [PXDefault]
  public virtual int? SiteID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? CostCenterID { get; set; }

  [PXExistance]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Not Available")]
  public virtual Decimal? QtyNotAvail { get; set; }

  [PXDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyExpired { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available for Shipping")]
  public virtual Decimal? QtyHardAvail { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available for Issue")]
  public virtual Decimal? QtyActual { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. In-Transit")]
  public virtual Decimal? QtyInTransit { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. In Transit to SO")]
  public virtual Decimal? QtyInTransitToSO { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. PO Prepared")]
  public virtual Decimal? QtyPOPrepared { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Purchase Orders")]
  public virtual Decimal? QtyPOOrders { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Purchase Receipts")]
  public virtual Decimal? QtyPOReceipts { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. FS Booked", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdBooked { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. FS Allocated", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdAllocated { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. FS Prepared", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdPrepared { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. SO Backordered")]
  public virtual Decimal? QtySOBackOrdered { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. SO Prepared")]
  public virtual Decimal? QtySOPrepared { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. SO Booked")]
  public virtual Decimal? QtySOBooked { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. SO Shipped")]
  public virtual Decimal? QtySOShipped { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. SO Shipping")]
  public virtual Decimal? QtySOShipping { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Inventory Issues")]
  public virtual Decimal? QtyINIssues { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Inventory Receipts")]
  public virtual Decimal? QtyINReceipts { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty Demanded by Kit Assembly")]
  public virtual Decimal? QtyINAssemblyDemand { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Kit Assembly")]
  public virtual Decimal? QtyINAssemblySupply { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity In Transit to Production.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty In Transit to Production")]
  public virtual Decimal? QtyInTransitToProduction { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity Production Supply Prepared.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty Production Supply Prepared")]
  public virtual Decimal? QtyProductionSupplyPrepared { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity Production Supply.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production Supply")]
  public virtual Decimal? QtyProductionSupply { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Purchase for Prod. Prepared.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Purchase for Prod. Prepared")]
  public virtual Decimal? QtyPOFixedProductionPrepared { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Purchase for Production.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Purchase for Production")]
  public virtual Decimal? QtyPOFixedProductionOrders { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production Demand Prepared.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production Demand Prepared")]
  public virtual Decimal? QtyProductionDemandPrepared { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production Demand.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production Demand")]
  public virtual Decimal? QtyProductionDemand { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production Allocated.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production Allocated")]
  public virtual Decimal? QtyProductionAllocated { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On SO to Production.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On SO to Production")]
  public virtual Decimal? QtySOFixedProduction { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production to Purchase.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production to Purchase", Enabled = false)]
  public virtual Decimal? QtyProdFixedPurchase { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production to Production
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production to Production", Enabled = false)]
  public virtual Decimal? QtyProdFixedProduction { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for Prod. Prepared
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production for Prod. Prepared", Enabled = false)]
  public virtual Decimal? QtyProdFixedProdOrdersPrepared { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for Production
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production for Production", Enabled = false)]
  public virtual Decimal? QtyProdFixedProdOrders { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for SO Prepared
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production for SO Prepared", Enabled = false)]
  public virtual Decimal? QtyProdFixedSalesOrdersPrepared { get; set; }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for SO
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production for SO Prepared", Enabled = false)]
  public virtual Decimal? QtyProdFixedSalesOrders { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Replaned")]
  public virtual Decimal? QtyINReplaned { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyFixedFSSrvOrd { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedFSSrvOrd { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedFSSrvOrdPrepared { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedFSSrvOrdReceipts { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOFixed { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedOrders { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedPrepared { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedReceipts { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySODropShip { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipOrders { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipPrepared { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipReceipts { get; set; }

  [PXDBTimestamp]
  public virtual 
  #nullable disable
  byte[] tstamp { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<INSiteStatusByCostCenter>.By<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>
  {
    public static INSiteStatusByCostCenter Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      int? costCenterID,
      PKFindOptions options = 0)
    {
      return (INSiteStatusByCostCenter) PrimaryKeyOf<INSiteStatusByCostCenter>.By<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) costCenterID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INSiteStatusByCostCenter>.By<INSiteStatusByCostCenter.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INSiteStatusByCostCenter>.By<INSiteStatusByCostCenter.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INSiteStatusByCostCenter>.By<INSiteStatusByCostCenter.siteID>
    {
    }

    public class ItemSite : 
      PrimaryKeyOf<INItemSite>.By<INItemSite.inventoryID, INItemSite.siteID>.ForeignKeyOf<INSiteStatusByCostCenter>.By<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.siteID>
    {
    }

    public class CostCenter : 
      PrimaryKeyOf<INCostCenter>.By<INCostCenter.costCenterID>.ForeignKeyOf<INSiteStatusByCostCenter>.By<INSiteStatusByCostCenter.costCenterID>
    {
    }
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INSiteStatusByCostCenter.inventoryID>
  {
    public class InventoryBaseUnitRule : 
      InventoryItem.baseUnit.PreventEditIfExists<Select<INSiteStatusByCostCenter, Where<INSiteStatusByCostCenter.inventoryID, Equal<Current<InventoryItem.inventoryID>>, And<Where<INSiteStatusByCostCenter.qtyOnHand, NotEqual<decimal0>, Or<INSiteStatusByCostCenter.qtyAvail, NotEqual<decimal0>>>>>>>
    {
    }
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusByCostCenter.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatusByCostCenter.siteID>
  {
  }

  public abstract class costCenterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INSiteStatusByCostCenter.costCenterID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INSiteStatusByCostCenter.active>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyOnHand>
  {
  }

  public abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyNotAvail>
  {
  }

  public abstract class qtyExpired : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyExpired>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyAvail>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyHardAvail>
  {
  }

  public abstract class qtyActual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyActual>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyInTransit>
  {
  }

  public abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyInTransitToSO>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPOPrepared>
  {
  }

  public abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPOOrders>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPOReceipts>
  {
  }

  public abstract class qtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyFSSrvOrdBooked>
  {
  }

  public abstract class qtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyFSSrvOrdAllocated>
  {
  }

  public abstract class qtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyFSSrvOrdPrepared>
  {
  }

  public abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtySOBackOrdered>
  {
  }

  public abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtySOPrepared>
  {
  }

  public abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtySOBooked>
  {
  }

  public abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtySOShipped>
  {
  }

  public abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtySOShipping>
  {
  }

  public abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyINIssues>
  {
  }

  public abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyINReceipts>
  {
  }

  public abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyINAssemblyDemand>
  {
  }

  public abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyINAssemblySupply>
  {
  }

  public abstract class qtyInTransitToProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyInTransitToProduction>
  {
  }

  public abstract class qtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyProductionSupplyPrepared>
  {
  }

  public abstract class qtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyProductionSupply>
  {
  }

  public abstract class qtyPOFixedProductionPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPOFixedProductionPrepared>
  {
  }

  public abstract class qtyPOFixedProductionOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPOFixedProductionOrders>
  {
  }

  public abstract class qtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyProductionDemandPrepared>
  {
  }

  public abstract class qtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyProductionDemand>
  {
  }

  public abstract class qtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyProductionAllocated>
  {
  }

  public abstract class qtySOFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtySOFixedProduction>
  {
  }

  public abstract class qtyProdFixedPurchase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyProdFixedPurchase>
  {
  }

  public abstract class qtyProdFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyProdFixedProduction>
  {
  }

  public abstract class qtyProdFixedProdOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyProdFixedProdOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedProdOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyProdFixedProdOrders>
  {
  }

  public abstract class qtyProdFixedSalesOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyProdFixedSalesOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedSalesOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyProdFixedSalesOrders>
  {
  }

  public abstract class qtyINReplaned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyINReplaned>
  {
  }

  public abstract class qtyFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPOFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPOFixedFSSrvOrdPrepared>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPOFixedFSSrvOrdReceipts>
  {
  }

  public abstract class qtySOFixed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtySOFixed>
  {
  }

  public abstract class qtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPOFixedOrders>
  {
  }

  public abstract class qtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPOFixedPrepared>
  {
  }

  public abstract class qtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPOFixedReceipts>
  {
  }

  public abstract class qtySODropShip : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtySODropShip>
  {
  }

  public abstract class qtyPODropShipOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPODropShipOrders>
  {
  }

  public abstract class qtyPODropShipPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPODropShipPrepared>
  {
  }

  public abstract class qtyPODropShipReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatusByCostCenter.qtyPODropShipReceipts>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    INSiteStatusByCostCenter.Tstamp>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatusByCostCenter.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSiteStatusByCostCenter.lastModifiedDateTime>
  {
  }
}
