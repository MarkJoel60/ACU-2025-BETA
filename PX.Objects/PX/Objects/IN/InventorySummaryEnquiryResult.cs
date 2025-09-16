// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventorySummaryEnquiryResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.IN;

public class InventorySummaryEnquiryResult : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IStatus,
  ICostStatus
{
  public const int TotalLocationID = -1;
  public const int EmptyLocationID = -3;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _LocationID;
  protected Decimal? _QtyAvail;
  protected Decimal? _QtyHardAvail;
  protected Decimal? _QtyActual;
  protected Decimal? _QtyNotAvail;
  protected Decimal? _QtyFSSrvOrdPrepared;
  protected Decimal? _QtyFSSrvOrdBooked;
  protected Decimal? _QtyFSSrvOrdAllocated;
  protected Decimal? _QtySOPrepared;
  protected Decimal? _QtySOBooked;
  protected Decimal? _QtySOShipped;
  protected Decimal? _QtySOShipping;
  protected Decimal? _QtySOBackOrdered;
  protected Decimal? _QtyINAssemblyDemand;
  protected Decimal? _QtyINIssues;
  protected Decimal? _QtyINReceipts;
  protected Decimal? _QtyInTransit;
  protected Decimal? _QtyInTransitToSO;
  protected Decimal? _QtyPOPrepared;
  protected Decimal? _QtyPOOrders;
  protected Decimal? _QtyPOReceipts;
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
  protected Decimal? _QtyOnHand;
  protected Decimal? _QtyExpired;
  protected 
  #nullable disable
  string _BaseUnit;
  protected Decimal? _Qty;
  protected Decimal? _UnitCost;
  protected Decimal? _TotalCost;
  protected string _LotSerialNbr;
  protected DateTime? _ExpireDate;

  [PXDBInt(IsKey = true)]
  [PXUIField]
  public virtual int? GridLineNbr { get; set; }

  [Inventory]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Site]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (InventorySummaryEnquiryResult.siteID))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  /// <exclude />
  [PXDBString(1)]
  [PX.Objects.IN.CostLayerType.List]
  [PXUIField(DisplayName = "Cost Layer Type", FieldClass = "CostLayerType")]
  public virtual string CostLayerType { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyAvail
  {
    get => this._QtyAvail;
    set => this._QtyAvail = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyHardAvail
  {
    get => this._QtyHardAvail;
    set => this._QtyHardAvail = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyActual
  {
    get => this._QtyActual;
    set => this._QtyActual = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyNotAvail
  {
    get => this._QtyNotAvail;
    set => this._QtyNotAvail = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyFSSrvOrdPrepared
  {
    get => this._QtyFSSrvOrdPrepared;
    set => this._QtyFSSrvOrdPrepared = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyFSSrvOrdBooked
  {
    get => this._QtyFSSrvOrdBooked;
    set => this._QtyFSSrvOrdBooked = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyFSSrvOrdAllocated
  {
    get => this._QtyFSSrvOrdAllocated;
    set => this._QtyFSSrvOrdAllocated = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtySOPrepared
  {
    get => this._QtySOPrepared;
    set => this._QtySOPrepared = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtySOBooked
  {
    get => this._QtySOBooked;
    set => this._QtySOBooked = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtySOShipped
  {
    get => this._QtySOShipped;
    set => this._QtySOShipped = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtySOShipping
  {
    get => this._QtySOShipping;
    set => this._QtySOShipping = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtySOBackOrdered
  {
    get => this._QtySOBackOrdered;
    set => this._QtySOBackOrdered = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyINAssemblyDemand
  {
    get => this._QtyINAssemblyDemand;
    set => this._QtyINAssemblyDemand = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyINIssues
  {
    get => this._QtyINIssues;
    set => this._QtyINIssues = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyINReceipts
  {
    get => this._QtyINReceipts;
    set => this._QtyINReceipts = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyInTransit
  {
    get => this._QtyInTransit;
    set => this._QtyInTransit = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
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

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyPOPrepared
  {
    get => this._QtyPOPrepared;
    set => this._QtyPOPrepared = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyPOOrders
  {
    get => this._QtyPOOrders;
    set => this._QtyPOOrders = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyPOReceipts
  {
    get => this._QtyPOReceipts;
    set => this._QtyPOReceipts = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "FS to Purchase", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFixedFSSrvOrd
  {
    get => this._QtyFixedFSSrvOrd;
    set => this._QtyFixedFSSrvOrd = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase for FS", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyPOFixedFSSrvOrd
  {
    get => this._QtyPOFixedFSSrvOrd;
    set => this._QtyPOFixedFSSrvOrd = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase for FS Prepared", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyPOFixedFSSrvOrdPrepared
  {
    get => this._QtyPOFixedFSSrvOrdPrepared;
    set => this._QtyPOFixedFSSrvOrdPrepared = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Receipts for FS", FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyPOFixedFSSrvOrdReceipts
  {
    get => this._QtyPOFixedFSSrvOrdReceipts;
    set => this._QtyPOFixedFSSrvOrdReceipts = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "SO to Purchase", Visible = false)]
  public virtual Decimal? QtySOFixed
  {
    get => this._QtySOFixed;
    set => this._QtySOFixed = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase for SO", Visible = false)]
  public virtual Decimal? QtyPOFixedOrders
  {
    get => this._QtyPOFixedOrders;
    set => this._QtyPOFixedOrders = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase for SO Prepared", Visible = false)]
  public virtual Decimal? QtyPOFixedPrepared
  {
    get => this._QtyPOFixedPrepared;
    set => this._QtyPOFixedPrepared = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Receipts for SO", Visible = false)]
  public virtual Decimal? QtyPOFixedReceipts
  {
    get => this._QtyPOFixedReceipts;
    set => this._QtyPOFixedReceipts = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "SO to Drop-Ship", Visible = false)]
  public virtual Decimal? QtySODropShip
  {
    get => this._QtySODropShip;
    set => this._QtySODropShip = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop-Ship for SO", Visible = false)]
  public virtual Decimal? QtyPODropShipOrders
  {
    get => this._QtyPODropShipOrders;
    set => this._QtyPODropShipOrders = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop-Ship for SO Prepared", Visible = false)]
  public virtual Decimal? QtyPODropShipPrepared
  {
    get => this._QtyPODropShipPrepared;
    set => this._QtyPODropShipPrepared = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop-Ship for SO Receipts", Visible = false)]
  public virtual Decimal? QtyPODropShipReceipts
  {
    get => this._QtyPODropShipReceipts;
    set => this._QtyPODropShipReceipts = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyINAssemblySupply
  {
    get => this._QtyINAssemblySupply;
    set => this._QtyINAssemblySupply = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transit to Production")]
  public virtual Decimal? QtyInTransitToProduction
  {
    get => this._QtyInTransitToProduction;
    set => this._QtyInTransitToProduction = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production Supply Prepared", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyProductionSupplyPrepared
  {
    get => this._QtyProductionSupplyPrepared;
    set => this._QtyProductionSupplyPrepared = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production Supply", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyProductionSupply
  {
    get => this._QtyProductionSupply;
    set => this._QtyProductionSupply = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase for Prod. Prepared", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyPOFixedProductionPrepared
  {
    get => this._QtyPOFixedProductionPrepared;
    set => this._QtyPOFixedProductionPrepared = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase for Production", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyPOFixedProductionOrders
  {
    get => this._QtyPOFixedProductionOrders;
    set => this._QtyPOFixedProductionOrders = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production Demand Prepared", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyProductionDemandPrepared
  {
    get => this._QtyProductionDemandPrepared;
    set => this._QtyProductionDemandPrepared = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production Demand", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyProductionDemand
  {
    get => this._QtyProductionDemand;
    set => this._QtyProductionDemand = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production Allocated", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyProductionAllocated
  {
    get => this._QtyProductionAllocated;
    set => this._QtyProductionAllocated = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "SO to Production", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtySOFixedProduction
  {
    get => this._QtySOFixedProduction;
    set => this._QtySOFixedProduction = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production to Purchase.
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production to Purchase", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyProdFixedPurchase
  {
    get => this._QtyProdFixedPurchase;
    set => this._QtyProdFixedPurchase = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production to Production
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production to Production", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyProdFixedProduction
  {
    get => this._QtyProdFixedProduction;
    set => this._QtyProdFixedProduction = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for Prod. Prepared
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production for Prod. Prepared", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyProdFixedProdOrdersPrepared
  {
    get => this._QtyProdFixedProdOrdersPrepared;
    set => this._QtyProdFixedProdOrdersPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for Production
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production for Production", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyProdFixedProdOrders
  {
    get => this._QtyProdFixedProdOrders;
    set => this._QtyProdFixedProdOrders = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for SO Prepared
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production for SO Prepared", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyProdFixedSalesOrdersPrepared
  {
    get => this._QtyProdFixedSalesOrdersPrepared;
    set => this._QtyProdFixedSalesOrdersPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for SO
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production for SO", FieldClass = "MANUFACTURING")]
  public virtual Decimal? QtyProdFixedSalesOrders
  {
    get => this._QtyProdFixedSalesOrders;
    set => this._QtyProdFixedSalesOrders = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyExpired
  {
    get => this._QtyExpired;
    set => this._QtyExpired = value;
  }

  [INUnit]
  public virtual string BaseUnit
  {
    get => this._BaseUnit;
    set => this._BaseUnit = value;
  }

  [PXQuantity]
  public virtual Decimal? Qty
  {
    [PXDependsOnFields(new Type[] {typeof (InventorySummaryEnquiryResult.qtyOnHand), typeof (InventorySummaryEnquiryResult.qtyInTransit)})] get
    {
      Decimal? qtyOnHand = this.QtyOnHand;
      Decimal? qtyInTransit = this.QtyInTransit;
      return !(qtyOnHand.HasValue & qtyInTransit.HasValue) ? new Decimal?() : new Decimal?(qtyOnHand.GetValueOrDefault() + qtyInTransit.GetValueOrDefault());
    }
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Estimated Unit Cost")]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Estimated Total Cost")]
  public virtual Decimal? TotalCost
  {
    get => this._TotalCost;
    set => this._TotalCost = value;
  }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Lot/Serial Number", Visible = false)]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Expiration Date", Visible = false)]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXBool]
  public virtual bool? IsTotal { get; set; }

  /// <exclude />
  public string ControlTimeStamp { get; set; }

  public abstract class gridLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.gridLineNbr>
  {
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.inventoryID>
  {
  }

  public abstract class subItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventorySummaryEnquiryResult.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.locationID>
  {
  }

  /// <exclude />
  public abstract class costLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.costLayerType>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyAvail>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyHardAvail>
  {
  }

  public abstract class qtyActual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyActual>
  {
  }

  public abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyNotAvail>
  {
  }

  public abstract class qtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyFSSrvOrdPrepared>
  {
  }

  public abstract class qtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyFSSrvOrdBooked>
  {
  }

  public abstract class qtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyFSSrvOrdAllocated>
  {
  }

  public abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtySOPrepared>
  {
  }

  public abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtySOBooked>
  {
  }

  public abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtySOShipped>
  {
  }

  public abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtySOShipping>
  {
  }

  public abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtySOBackOrdered>
  {
  }

  public abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyINAssemblyDemand>
  {
  }

  public abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyINIssues>
  {
  }

  public abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyINReceipts>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyInTransit>
  {
  }

  public abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyInTransitToSO>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPOPrepared>
  {
  }

  public abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPOOrders>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPOReceipts>
  {
  }

  public abstract class qtyFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPOFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPOFixedFSSrvOrdPrepared>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPOFixedFSSrvOrdReceipts>
  {
  }

  public abstract class qtySOFixed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtySOFixed>
  {
  }

  public abstract class qtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPOFixedOrders>
  {
  }

  public abstract class qtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPOFixedPrepared>
  {
  }

  public abstract class qtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPOFixedReceipts>
  {
  }

  public abstract class qtySODropShip : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtySODropShip>
  {
  }

  public abstract class qtyPODropShipOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPODropShipOrders>
  {
  }

  public abstract class qtyPODropShipPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPODropShipPrepared>
  {
  }

  public abstract class qtyPODropShipReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPODropShipReceipts>
  {
  }

  public abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyINAssemblySupply>
  {
  }

  public abstract class qtyInTransitToProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyInTransitToProduction>
  {
  }

  public abstract class qtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyProductionSupplyPrepared>
  {
  }

  public abstract class qtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyProductionSupply>
  {
  }

  public abstract class qtyPOFixedProductionPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPOFixedProductionPrepared>
  {
  }

  public abstract class qtyPOFixedProductionOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyPOFixedProductionOrders>
  {
  }

  public abstract class qtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyProductionDemandPrepared>
  {
  }

  public abstract class qtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyProductionDemand>
  {
  }

  public abstract class qtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyProductionAllocated>
  {
  }

  public abstract class qtySOFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtySOFixedProduction>
  {
  }

  public abstract class qtyProdFixedPurchase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyProdFixedPurchase>
  {
  }

  public abstract class qtyProdFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyProdFixedProduction>
  {
  }

  public abstract class qtyProdFixedProdOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyProdFixedProdOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedProdOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyProdFixedProdOrders>
  {
  }

  public abstract class qtyProdFixedSalesOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyProdFixedSalesOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedSalesOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyProdFixedSalesOrders>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyOnHand>
  {
  }

  public abstract class qtyExpired : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.qtyExpired>
  {
  }

  public abstract class baseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.baseUnit>
  {
  }

  public abstract class unitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.unitCost>
  {
  }

  public abstract class totalCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.totalCost>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.lotSerialNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.expireDate>
  {
  }

  public abstract class isTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventorySummaryEnquiryResult.isTotal>
  {
  }
}
