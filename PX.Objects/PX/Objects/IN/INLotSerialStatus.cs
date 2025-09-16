// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INLotSerialStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Lot/Serial Status")]
[INLotSerialStatusProjection]
public class INLotSerialStatus : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IStatus,
  ILotSerial
{
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected 
  #nullable disable
  string _LotSerialNbr;
  protected Decimal? _QtyFSSrvOrdBooked;
  protected Decimal? _QtyFSSrvOrdAllocated;
  protected Decimal? _QtyFSSrvOrdPrepared;
  protected Decimal? _QtyOnHand;
  protected Decimal? _QtyAvail;
  protected Decimal? _QtyNotAvail;
  protected Decimal? _QtyExpired;
  protected Decimal? _QtyHardAvail;
  protected Decimal? _QtyActual;
  protected Decimal? _QtyInTransit;
  protected Decimal? _QtyInTransitToSO;
  protected Decimal? _QtyPOPrepared;
  protected Decimal? _QtyPOOrders;
  protected Decimal? _QtyPOReceipts;
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
  protected Decimal? _QtyFixedFSSrvOrd;
  protected Decimal? _QtyPOFixedFSSrvOrd;
  protected Decimal? _QtyPOFixedFSSrvOrdPrepared;
  protected Decimal? _QtyPOFixedFSSrvOrdReceipts;
  protected Decimal? _QtyProdFixedPurchase;
  protected Decimal? _QtyProdFixedProduction;
  protected Decimal? _QtyProdFixedProdOrdersPrepared;
  protected Decimal? _QtyProdFixedProdOrders;
  protected Decimal? _QtyProdFixedSalesOrdersPrepared;
  protected Decimal? _QtyProdFixedSalesOrders;
  protected Decimal? _QtySOFixed;
  protected Decimal? _QtyPOFixedOrders;
  protected Decimal? _QtyPOFixedPrepared;
  protected Decimal? _QtyPOFixedReceipts;
  protected Decimal? _QtySODropShip;
  protected Decimal? _QtyPODropShipOrders;
  protected Decimal? _QtyPODropShipPrepared;
  protected Decimal? _QtyPODropShipReceipts;
  protected DateTime? _ExpireDate;
  protected DateTime? _ReceiptDate;
  protected string _LotSerTrack;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [StockItem(IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.subItemID))]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Site(IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.siteID))]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (INLotSerialStatus.siteID), IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.locationID))]
  [PXDefault]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDefault]
  [PX.Objects.IN.LotSerialNbr(IsKey = true, BqlField = typeof (INLotSerialStatusByCostCenter.lotSerialNbr))]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyFSSrvOrdBooked))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdBooked
  {
    get => this._QtyFSSrvOrdBooked;
    set => this._QtyFSSrvOrdBooked = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyFSSrvOrdAllocated))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdAllocated
  {
    get => this._QtyFSSrvOrdAllocated;
    set => this._QtyFSSrvOrdAllocated = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyFSSrvOrdPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdPrepared
  {
    get => this._QtyFSSrvOrdPrepared;
    set => this._QtyFSSrvOrdPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyOnHand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Hand")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available")]
  public virtual Decimal? QtyAvail
  {
    get => this._QtyAvail;
    set => this._QtyAvail = value;
  }

  [PXDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
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

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyHardAvail))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Hard Available")]
  public virtual Decimal? QtyHardAvail
  {
    get => this._QtyHardAvail;
    set => this._QtyHardAvail = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyActual))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Available for Issue")]
  public virtual Decimal? QtyActual
  {
    get => this._QtyActual;
    set => this._QtyActual = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyInTransit))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyInTransit
  {
    get => this._QtyInTransit;
    set => this._QtyInTransit = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyInTransitToSO))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyInTransitToSO
  {
    get => this._QtyInTransitToSO;
    set => this._QtyInTransitToSO = value;
  }

  public Decimal? QtyINReplaned
  {
    get => new Decimal?(0M);
    set
    {
    }
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOPrepared
  {
    get => this._QtyPOPrepared;
    set => this._QtyPOPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOOrders
  {
    get => this._QtyPOOrders;
    set => this._QtyPOOrders = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOReceipts
  {
    get => this._QtyPOReceipts;
    set => this._QtyPOReceipts = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySOBackOrdered))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOBackOrdered
  {
    get => this._QtySOBackOrdered;
    set => this._QtySOBackOrdered = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySOPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOPrepared
  {
    get => this._QtySOPrepared;
    set => this._QtySOPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySOBooked))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOBooked
  {
    get => this._QtySOBooked;
    set => this._QtySOBooked = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySOShipped))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOShipped
  {
    get => this._QtySOShipped;
    set => this._QtySOShipped = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySOShipping))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOShipping
  {
    get => this._QtySOShipping;
    set => this._QtySOShipping = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyINIssues))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Inventory Issues")]
  public virtual Decimal? QtyINIssues
  {
    get => this._QtyINIssues;
    set => this._QtyINIssues = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyINReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Inventory Receipts")]
  public virtual Decimal? QtyINReceipts
  {
    get => this._QtyINReceipts;
    set => this._QtyINReceipts = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyINAssemblyDemand))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty Demanded by Kit Assembly")]
  public virtual Decimal? QtyINAssemblyDemand
  {
    get => this._QtyINAssemblyDemand;
    set => this._QtyINAssemblyDemand = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyINAssemblySupply))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyInTransitToProduction))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyProductionSupplyPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty Production Supply Prepared")]
  public virtual Decimal? QtyProductionSupplyPrepared
  {
    get => this._QtyProductionSupplyPrepared;
    set => this._QtyProductionSupplyPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production Supply.
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyProductionSupply))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOFixedProductionPrepared))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOFixedProductionOrders))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyProductionDemandPrepared))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyProductionDemand))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyProductionAllocated))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySOFixedProduction))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On SO to Production")]
  public virtual Decimal? QtySOFixedProduction
  {
    get => this._QtySOFixedProduction;
    set => this._QtySOFixedProduction = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyFixedFSSrvOrd))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyFixedFSSrvOrd
  {
    get => this._QtyFixedFSSrvOrd;
    set => this._QtyFixedFSSrvOrd = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOFixedFSSrvOrd))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedFSSrvOrd
  {
    get => this._QtyPOFixedFSSrvOrd;
    set => this._QtyPOFixedFSSrvOrd = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOFixedFSSrvOrdPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedFSSrvOrdPrepared
  {
    get => this._QtyPOFixedFSSrvOrdPrepared;
    set => this._QtyPOFixedFSSrvOrdPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOFixedFSSrvOrdReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedFSSrvOrdReceipts
  {
    get => this._QtyPOFixedFSSrvOrdReceipts;
    set => this._QtyPOFixedFSSrvOrdReceipts = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production to Purchase.
  /// </summary>
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyProdFixedPurchase))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyProdFixedProduction))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyProdFixedProdOrdersPrepared))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyProdFixedProdOrders))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyProdFixedSalesOrdersPrepared))]
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
  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyProdFixedSalesOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty On Production for SO", Enabled = false)]
  public virtual Decimal? QtyProdFixedSalesOrders
  {
    get => this._QtyProdFixedSalesOrders;
    set => this._QtyProdFixedSalesOrders = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySOFixed))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOFixed
  {
    get => this._QtySOFixed;
    set => this._QtySOFixed = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOFixedOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedOrders
  {
    get => this._QtyPOFixedOrders;
    set => this._QtyPOFixedOrders = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOFixedPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedPrepared
  {
    get => this._QtyPOFixedPrepared;
    set => this._QtyPOFixedPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPOFixedReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPOFixedReceipts
  {
    get => this._QtyPOFixedReceipts;
    set => this._QtyPOFixedReceipts = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtySODropShip))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySODropShip
  {
    get => this._QtySODropShip;
    set => this._QtySODropShip = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPODropShipOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipOrders
  {
    get => this._QtyPODropShipOrders;
    set => this._QtyPODropShipOrders = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPODropShipPrepared))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipPrepared
  {
    get => this._QtyPODropShipPrepared;
    set => this._QtyPODropShipPrepared = value;
  }

  [PXDBQuantity(BqlField = typeof (INLotSerialStatusByCostCenter.qtyPODropShipReceipts))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyPODropShipReceipts
  {
    get => this._QtyPODropShipReceipts;
    set => this._QtyPODropShipReceipts = value;
  }

  [PXDBDate(BqlField = typeof (INItemLotSerial.expireDate))]
  [PXUIField(DisplayName = "Expiry Date")]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXDBDate(BqlField = typeof (INLotSerialStatusByCostCenter.receiptDate))]
  [PXDefault]
  public virtual DateTime? ReceiptDate
  {
    get => this._ReceiptDate;
    set => this._ReceiptDate = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (INLotSerialStatusByCostCenter.lotSerTrack))]
  [PXDefault]
  public virtual string LotSerTrack
  {
    get => this._LotSerTrack;
    set => this._LotSerTrack = value;
  }

  [PXDBLastModifiedByScreenID(BqlField = typeof (INLotSerialStatusByCostCenter.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime(BqlField = typeof (INLotSerialStatusByCostCenter.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>
  {
    public static INLotSerialStatus Find(
      PXGraph graph,
      int? inventoryID,
      int? subItemID,
      int? siteID,
      int? locationID,
      string lotSerialNbr,
      PKFindOptions options = 0)
    {
      return (INLotSerialStatus) PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.FindBy(graph, (object) inventoryID, (object) subItemID, (object) siteID, (object) locationID, (object) lotSerialNbr, options);
    }
  }

  public static class FK
  {
    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INLotSerialStatus>.By<INLotSerialStatus.locationID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INLotSerialStatus>.By<INLotSerialStatus.subItemID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID>
    {
    }

    public class ItemLotSerial : 
      PrimaryKeyOf<INItemLotSerial>.By<INItemLotSerial.inventoryID, INItemLotSerial.lotSerialNbr>.ForeignKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.lotSerialNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INLotSerialStatus>.By<INLotSerialStatus.siteID>
    {
    }
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLotSerialStatus.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLotSerialStatus.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLotSerialStatus.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INLotSerialStatus.locationID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerialStatus.lotSerialNbr>
  {
  }

  public abstract class qtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyFSSrvOrdBooked>
  {
  }

  public abstract class qtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyFSSrvOrdAllocated>
  {
  }

  public abstract class qtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyFSSrvOrdPrepared>
  {
  }

  public abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INLotSerialStatus.qtyOnHand>
  {
  }

  public abstract class qtyAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INLotSerialStatus.qtyAvail>
  {
  }

  public abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyNotAvail>
  {
  }

  public abstract class qtyExpired : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyExpired>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyHardAvail>
  {
  }

  public abstract class qtyActual : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INLotSerialStatus.qtyActual>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyInTransit>
  {
  }

  public abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyInTransitToSO>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPOPrepared>
  {
  }

  public abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPOOrders>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPOReceipts>
  {
  }

  public abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtySOBackOrdered>
  {
  }

  public abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtySOPrepared>
  {
  }

  public abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtySOBooked>
  {
  }

  public abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtySOShipped>
  {
  }

  public abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtySOShipping>
  {
  }

  public abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyINIssues>
  {
  }

  public abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyINReceipts>
  {
  }

  public abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyINAssemblyDemand>
  {
  }

  public abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyINAssemblySupply>
  {
  }

  public abstract class qtyInTransitToProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyInTransitToProduction>
  {
  }

  public abstract class qtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyProductionSupplyPrepared>
  {
  }

  public abstract class qtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyProductionSupply>
  {
  }

  public abstract class qtyPOFixedProductionPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPOFixedProductionPrepared>
  {
  }

  public abstract class qtyPOFixedProductionOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPOFixedProductionOrders>
  {
  }

  public abstract class qtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyProductionDemandPrepared>
  {
  }

  public abstract class qtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyProductionDemand>
  {
  }

  public abstract class qtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyProductionAllocated>
  {
  }

  public abstract class qtySOFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtySOFixedProduction>
  {
  }

  public abstract class qtyFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPOFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPOFixedFSSrvOrdPrepared>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPOFixedFSSrvOrdReceipts>
  {
  }

  public abstract class qtyProdFixedPurchase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyProdFixedPurchase>
  {
  }

  public abstract class qtyProdFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyProdFixedProduction>
  {
  }

  public abstract class qtyProdFixedProdOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyProdFixedProdOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedProdOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyProdFixedProdOrders>
  {
  }

  public abstract class qtyProdFixedSalesOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyProdFixedSalesOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedSalesOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyProdFixedSalesOrders>
  {
  }

  public abstract class qtySOFixed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtySOFixed>
  {
  }

  public abstract class qtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPOFixedOrders>
  {
  }

  public abstract class qtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPOFixedPrepared>
  {
  }

  public abstract class qtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPOFixedReceipts>
  {
  }

  public abstract class qtySODropShip : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtySODropShip>
  {
  }

  public abstract class qtyPODropShipOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPODropShipOrders>
  {
  }

  public abstract class qtyPODropShipPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPODropShipPrepared>
  {
  }

  public abstract class qtyPODropShipReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INLotSerialStatus.qtyPODropShipReceipts>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerialStatus.expireDate>
  {
  }

  public abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerialStatus.receiptDate>
  {
  }

  public abstract class lotSerTrack : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerialStatus.lotSerTrack>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INLotSerialStatus.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INLotSerialStatus.lastModifiedDateTime>
  {
  }
}
