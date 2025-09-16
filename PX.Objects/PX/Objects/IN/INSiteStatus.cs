// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Site Status")]
[INSiteStatusProjection]
public class INSiteStatus : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IStatus
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected Decimal? _QtyOnHand;
  protected Decimal? _QtyNotAvail;
  protected Decimal? _QtyExpired;
  protected Decimal? _QtyAvail;
  protected Decimal? _QtyHardAvail;
  protected Decimal? _QtyActual;
  protected Decimal? _QtyInTransit;
  protected Decimal? _QtyInTransitToSO;
  protected Decimal? _QtyPOPrepared;
  protected Decimal? _QtyPOOrders;
  protected Decimal? _QtyPOReceipts;
  protected Decimal? _QtyFSSrvOrdBooked;
  protected Decimal? _QtyFSSrvOrdAllocated;
  protected Decimal? _QtyFSSrvOrdPrepared;
  protected Decimal? _QtySOBackOrdered;
  protected Decimal? _QtySOPrepared;
  protected Decimal? _QtySOBooked;
  protected Decimal? _QtySOShipped;
  protected Decimal? _QtySOShipping;
  protected Decimal? _QtyINIssues;
  protected Decimal? _QtyINReceipts;
  protected Decimal? _QtyINAssemblyDemand;
  protected Decimal? _QtyINAssemblySupply;
  protected Decimal? _QtyInTransitToProduction;
  protected Decimal? _QtyProductionSupplyPrepared;
  protected Decimal? _QtyProductionSupply;
  protected Decimal? _QtyPOFixedProductionPrepared;
  protected Decimal? _QtyPOFixedProductionOrders;
  protected Decimal? _QtyProductionDemandPrepared;
  protected Decimal? _QtyProductionDemand;
  protected Decimal? _QtyProductionAllocated;
  protected Decimal? _QtySOFixedProduction;
  protected Decimal? _QtyProdFixedPurchase;
  protected Decimal? _QtyProdFixedProduction;
  protected Decimal? _QtyProdFixedProdOrdersPrepared;
  protected Decimal? _QtyProdFixedProdOrders;
  protected Decimal? _QtyProdFixedSalesOrdersPrepared;
  protected Decimal? _QtyProdFixedSalesOrders;
  protected Decimal? _QtyINReplaned;
  protected Decimal? _QtyFixedFSSrvOrd;
  protected Decimal? _QtyPOFixedFSSrvOrd;
  protected Decimal? _QtyPOFixedFSSrvOrdPrepared;
  protected Decimal? _QtyPOFixedFSSrvOrdReceipts;
  protected Decimal? _QtySOFixed;
  protected Decimal? _QtyPOFixedOrders;
  protected Decimal? _QtyPOFixedPrepared;
  protected Decimal? _QtyPOFixedReceipts;
  protected Decimal? _QtySODropShip;
  protected Decimal? _QtyPODropShipOrders;
  protected Decimal? _QtyPODropShipPrepared;
  protected Decimal? _QtyPODropShipReceipts;
  protected 
  #nullable disable
  string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [Inventory(IsKey = true, BqlField = typeof (INSiteStatusByCostCenter.inventoryID))]
  [PXDefault]
  [ConvertedInventoryItem(true)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(IsKey = true, BqlField = typeof (INSiteStatusByCostCenter.subItemID))]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Site(IsKey = true, BqlField = typeof (INSiteStatusByCostCenter.siteID))]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyNotAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Not Available")]
  public virtual Decimal? QtyNotAvail
  {
    get => this._QtyNotAvail;
    set => this._QtyNotAvail = value;
  }

  [PXDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyExpired
  {
    get => this._QtyExpired;
    set => this._QtyExpired = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail
  {
    get => this._QtyAvail;
    set => this._QtyAvail = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyHardAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Hard Available")]
  public virtual Decimal? QtyHardAvail
  {
    get => this._QtyHardAvail;
    set => this._QtyHardAvail = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyActual))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available for Issue")]
  public virtual Decimal? QtyActual
  {
    get => this._QtyActual;
    set => this._QtyActual = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyInTransit))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. In-Transit")]
  public virtual Decimal? QtyInTransit
  {
    get => this._QtyInTransit;
    set => this._QtyInTransit = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyInTransitToSO))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. In Transit to SO")]
  public virtual Decimal? QtyInTransitToSO
  {
    get => this._QtyInTransitToSO;
    set => this._QtyInTransitToSO = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. PO Prepared")]
  public virtual Decimal? QtyPOPrepared
  {
    get => this._QtyPOPrepared;
    set => this._QtyPOPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Purchase Orders")]
  public virtual Decimal? QtyPOOrders
  {
    get => this._QtyPOOrders;
    set => this._QtyPOOrders = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Purchase Receipts")]
  public virtual Decimal? QtyPOReceipts
  {
    get => this._QtyPOReceipts;
    set => this._QtyPOReceipts = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyFSSrvOrdBooked))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. FS Booked", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdBooked
  {
    get => this._QtyFSSrvOrdBooked;
    set => this._QtyFSSrvOrdBooked = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyFSSrvOrdAllocated))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. FS Allocated", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdAllocated
  {
    get => this._QtyFSSrvOrdAllocated;
    set => this._QtyFSSrvOrdAllocated = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyFSSrvOrdPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. FS Prepared", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdPrepared
  {
    get => this._QtyFSSrvOrdPrepared;
    set => this._QtyFSSrvOrdPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtySOBackOrdered))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. SO Backordered")]
  public virtual Decimal? QtySOBackOrdered
  {
    get => this._QtySOBackOrdered;
    set => this._QtySOBackOrdered = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtySOPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. SO Prepared")]
  public virtual Decimal? QtySOPrepared
  {
    get => this._QtySOPrepared;
    set => this._QtySOPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtySOBooked))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. SO Booked")]
  public virtual Decimal? QtySOBooked
  {
    get => this._QtySOBooked;
    set => this._QtySOBooked = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtySOShipped))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. SO Shipped")]
  public virtual Decimal? QtySOShipped
  {
    get => this._QtySOShipped;
    set => this._QtySOShipped = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtySOShipping))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. SO Shipping")]
  public virtual Decimal? QtySOShipping
  {
    get => this._QtySOShipping;
    set => this._QtySOShipping = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyINIssues))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Inventory Issues")]
  public virtual Decimal? QtyINIssues
  {
    get => this._QtyINIssues;
    set => this._QtyINIssues = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyINReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Inventory Receipts")]
  public virtual Decimal? QtyINReceipts
  {
    get => this._QtyINReceipts;
    set => this._QtyINReceipts = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyINAssemblyDemand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty Demanded by Kit Assembly")]
  public virtual Decimal? QtyINAssemblyDemand
  {
    get => this._QtyINAssemblyDemand;
    set => this._QtyINAssemblyDemand = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyINAssemblySupply))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Kit Assembly")]
  public virtual Decimal? QtyINAssemblySupply
  {
    get => this._QtyINAssemblySupply;
    set => this._QtyINAssemblySupply = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity In Transit to Production.
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyInTransitToProduction))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty In Transit to Production")]
  public virtual Decimal? QtyInTransitToProduction
  {
    get => this._QtyInTransitToProduction;
    set => this._QtyInTransitToProduction = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity Production Supply Prepared.
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyProductionSupplyPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty Production Supply Prepared")]
  public virtual Decimal? QtyProductionSupplyPrepared
  {
    get => this._QtyProductionSupplyPrepared;
    set => this._QtyProductionSupplyPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity Production Supply.
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyProductionSupply))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production Supply")]
  public virtual Decimal? QtyProductionSupply
  {
    get => this._QtyProductionSupply;
    set => this._QtyProductionSupply = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Purchase for Prod. Prepared.
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOFixedProductionPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Purchase for Prod. Prepared")]
  public virtual Decimal? QtyPOFixedProductionPrepared
  {
    get => this._QtyPOFixedProductionPrepared;
    set => this._QtyPOFixedProductionPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Purchase for Production.
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOFixedProductionOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Purchase for Production")]
  public virtual Decimal? QtyPOFixedProductionOrders
  {
    get => this._QtyPOFixedProductionOrders;
    set => this._QtyPOFixedProductionOrders = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production Demand Prepared.
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyProductionDemandPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production Demand Prepared")]
  public virtual Decimal? QtyProductionDemandPrepared
  {
    get => this._QtyProductionDemandPrepared;
    set => this._QtyProductionDemandPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production Demand.
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyProductionDemand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production Demand")]
  public virtual Decimal? QtyProductionDemand
  {
    get => this._QtyProductionDemand;
    set => this._QtyProductionDemand = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production Allocated.
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyProductionAllocated))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production Allocated")]
  public virtual Decimal? QtyProductionAllocated
  {
    get => this._QtyProductionAllocated;
    set => this._QtyProductionAllocated = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On SO to Production.
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtySOFixedProduction))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On SO to Production")]
  public virtual Decimal? QtySOFixedProduction
  {
    get => this._QtySOFixedProduction;
    set => this._QtySOFixedProduction = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production to Purchase.
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyProdFixedPurchase))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production to Purchase", Enabled = false)]
  public virtual Decimal? QtyProdFixedPurchase
  {
    get => this._QtyProdFixedPurchase;
    set => this._QtyProdFixedPurchase = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production to Production
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyProdFixedProduction))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production to Production", Enabled = false)]
  public virtual Decimal? QtyProdFixedProduction
  {
    get => this._QtyProdFixedProduction;
    set => this._QtyProdFixedProduction = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for Prod. Prepared
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyProdFixedProdOrdersPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production for Prod. Prepared", Enabled = false)]
  public virtual Decimal? QtyProdFixedProdOrdersPrepared
  {
    get => this._QtyProdFixedProdOrdersPrepared;
    set => this._QtyProdFixedProdOrdersPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for Production
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyProdFixedProdOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production for Production", Enabled = false)]
  public virtual Decimal? QtyProdFixedProdOrders
  {
    get => this._QtyProdFixedProdOrders;
    set => this._QtyProdFixedProdOrders = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for SO Prepared
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyProdFixedSalesOrdersPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production for SO Prepared", Enabled = false)]
  public virtual Decimal? QtyProdFixedSalesOrdersPrepared
  {
    get => this._QtyProdFixedSalesOrdersPrepared;
    set => this._QtyProdFixedSalesOrdersPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for SO
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyProdFixedSalesOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production for SO Prepared", Enabled = false)]
  public virtual Decimal? QtyProdFixedSalesOrders
  {
    get => this._QtyProdFixedSalesOrders;
    set => this._QtyProdFixedSalesOrders = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyINReplaned))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Replanned")]
  public virtual Decimal? QtyINReplaned
  {
    get => this._QtyINReplaned;
    set => this._QtyINReplaned = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyFixedFSSrvOrd))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyFixedFSSrvOrd
  {
    get => this._QtyFixedFSSrvOrd;
    set => this._QtyFixedFSSrvOrd = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOFixedFSSrvOrd))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedFSSrvOrd
  {
    get => this._QtyPOFixedFSSrvOrd;
    set => this._QtyPOFixedFSSrvOrd = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOFixedFSSrvOrdPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedFSSrvOrdPrepared
  {
    get => this._QtyPOFixedFSSrvOrdPrepared;
    set => this._QtyPOFixedFSSrvOrdPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOFixedFSSrvOrdReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedFSSrvOrdReceipts
  {
    get => this._QtyPOFixedFSSrvOrdReceipts;
    set => this._QtyPOFixedFSSrvOrdReceipts = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtySOFixed))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOFixed
  {
    get => this._QtySOFixed;
    set => this._QtySOFixed = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOFixedOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedOrders
  {
    get => this._QtyPOFixedOrders;
    set => this._QtyPOFixedOrders = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOFixedPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedPrepared
  {
    get => this._QtyPOFixedPrepared;
    set => this._QtyPOFixedPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPOFixedReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedReceipts
  {
    get => this._QtyPOFixedReceipts;
    set => this._QtyPOFixedReceipts = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtySODropShip))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySODropShip
  {
    get => this._QtySODropShip;
    set => this._QtySODropShip = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPODropShipOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipOrders
  {
    get => this._QtyPODropShipOrders;
    set => this._QtyPODropShipOrders = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPODropShipPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipPrepared
  {
    get => this._QtyPODropShipPrepared;
    set => this._QtyPODropShipPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INSiteStatusByCostCenter.qtyPODropShipReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipReceipts
  {
    get => this._QtyPODropShipReceipts;
    set => this._QtyPODropShipReceipts = value;
  }

  [PXDBLastModifiedByScreenID(BqlField = typeof (INSiteStatusByCostCenter.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime(BqlField = typeof (INSiteStatusByCostCenter.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>
  {
    public static INSiteStatus Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      PKFindOptions options = 0)
    {
      return (INSiteStatus) PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INSiteStatus>.By<INSiteStatus.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INSiteStatus>.By<INSiteStatus.siteID>
    {
    }

    public class ItemSite : 
      PrimaryKeyOf<INItemSite>.By<INItemSite.inventoryID, INItemSite.siteID>.ForeignKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.siteID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatus.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatus.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INSiteStatus.siteID>
  {
  }

  public abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatus.qtyOnHand>
  {
  }

  public abstract class qtyNotAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatus.qtyNotAvail>
  {
  }

  public abstract class qtyExpired : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatus.qtyExpired>
  {
  }

  public abstract class qtyAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatus.qtyAvail>
  {
  }

  public abstract class qtyHardAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatus.qtyHardAvail>
  {
  }

  public abstract class qtyActual : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatus.qtyActual>
  {
  }

  public abstract class qtyInTransit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatus.qtyInTransit>
  {
  }

  public abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyInTransitToSO>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPOPrepared>
  {
  }

  public abstract class qtyPOOrders : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatus.qtyPOOrders>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPOReceipts>
  {
  }

  public abstract class qtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyFSSrvOrdBooked>
  {
  }

  public abstract class qtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyFSSrvOrdAllocated>
  {
  }

  public abstract class qtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyFSSrvOrdPrepared>
  {
  }

  public abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtySOBackOrdered>
  {
  }

  public abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtySOPrepared>
  {
  }

  public abstract class qtySOBooked : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatus.qtySOBooked>
  {
  }

  public abstract class qtySOShipped : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatus.qtySOShipped>
  {
  }

  public abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtySOShipping>
  {
  }

  public abstract class qtyINIssues : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatus.qtyINIssues>
  {
  }

  public abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyINReceipts>
  {
  }

  public abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyINAssemblyDemand>
  {
  }

  public abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyINAssemblySupply>
  {
  }

  public abstract class qtyInTransitToProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyInTransitToProduction>
  {
  }

  public abstract class qtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyProductionSupplyPrepared>
  {
  }

  public abstract class qtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyProductionSupply>
  {
  }

  public abstract class qtyPOFixedProductionPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPOFixedProductionPrepared>
  {
  }

  public abstract class qtyPOFixedProductionOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPOFixedProductionOrders>
  {
  }

  public abstract class qtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyProductionDemandPrepared>
  {
  }

  public abstract class qtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyProductionDemand>
  {
  }

  public abstract class qtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyProductionAllocated>
  {
  }

  public abstract class qtySOFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtySOFixedProduction>
  {
  }

  public abstract class qtyProdFixedPurchase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyProdFixedPurchase>
  {
  }

  public abstract class qtyProdFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyProdFixedProduction>
  {
  }

  public abstract class qtyProdFixedProdOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyProdFixedProdOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedProdOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyProdFixedProdOrders>
  {
  }

  public abstract class qtyProdFixedSalesOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyProdFixedSalesOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedSalesOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyProdFixedSalesOrders>
  {
  }

  public abstract class qtyINReplaned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyINReplaned>
  {
  }

  public abstract class qtyFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPOFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPOFixedFSSrvOrdPrepared>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPOFixedFSSrvOrdReceipts>
  {
  }

  public abstract class qtySOFixed : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INSiteStatus.qtySOFixed>
  {
  }

  public abstract class qtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPOFixedOrders>
  {
  }

  public abstract class qtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPOFixedPrepared>
  {
  }

  public abstract class qtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPOFixedReceipts>
  {
  }

  public abstract class qtySODropShip : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtySODropShip>
  {
  }

  public abstract class qtyPODropShipOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPODropShipOrders>
  {
  }

  public abstract class qtyPODropShipPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPODropShipPrepared>
  {
  }

  public abstract class qtyPODropShipReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INSiteStatus.qtyPODropShipReceipts>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INSiteStatus.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INSiteStatus.lastModifiedDateTime>
  {
  }
}
