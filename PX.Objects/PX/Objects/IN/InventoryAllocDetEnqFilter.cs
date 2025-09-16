// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryAllocDetEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class InventoryAllocDetEnqFilter : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IQtyAllocated,
  IQtyAllocatedBase
{
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _SubItemCD;
  protected int? _SiteID;
  protected int? _LocationID;
  protected string _LotSerialNbr;
  protected Decimal? _QtyOnHand;
  protected Decimal? _QtyTotalAddition;
  protected Decimal? _QtyPOPrepared;
  protected bool? _InclQtyPOPrepared;
  protected Decimal? _QtyPOOrders;
  protected bool? _InclQtyPOOrders;
  protected Decimal? _QtyPOReceipts;
  protected bool? _InclQtyPOReceipts;
  protected Decimal? _QtyINReceipts;
  protected bool? _InclQtyINReceipts;
  protected Decimal? _QtyINIssues;
  protected bool? _InclQtyINIssues;
  protected Decimal? _QtyInTransit;
  protected bool? _InclQtyInTransit;
  protected Decimal? _QtyInTransitToSO;
  protected Decimal? _QtyTotalDeduction;
  protected Decimal? _QtyNotAvail;
  protected Decimal? _QtyExpired;
  protected Decimal? _QtyFSSrvOrdPrepared;
  protected bool? _InclQtyFSSrvOrdPrepared;
  protected Decimal? _QtySOPrepared;
  protected bool? _InclQtySOPrepared;
  protected Decimal? _QtyFSSrvOrdBooked;
  protected bool? _InclQtyFSSrvOrdBooked;
  protected Decimal? _QtySOBooked;
  protected bool? _InclQtySOBooked;
  protected Decimal? _QtyFSSrvOrdAllocated;
  protected bool? _InclQtyFSSrvOrdAllocated;
  protected Decimal? _QtySOShipping;
  protected bool? _InclQtySOShipping;
  protected Decimal? _QtySOShipped;
  protected bool? _InclQtySOShipped;
  protected Decimal? _QtyINAssemblySupply;
  protected bool? _InclQtyINAssemblySupply;
  protected Decimal? _QtyINAssemblyDemand;
  protected bool? _InclQtyINAssemblyDemand;
  protected Decimal? _QtyInTransitToProduction;
  protected Decimal? _QtyProductionSupplyPrepared;
  protected bool? _InclQtyProductionSupplyPrepared;
  protected Decimal? _QtyProductionSupply;
  protected bool? _InclQtyProductionSupply;
  protected Decimal? _QtyPOFixedProductionPrepared;
  protected Decimal? _QtyPOFixedProductionOrders;
  protected Decimal? _QtyProductionDemandPrepared;
  protected bool? _InclQtyProductionDemandPrepared;
  protected Decimal? _QtyProductionDemand;
  protected bool? _InclQtyProductionDemand;
  protected Decimal? _QtyProductionAllocated;
  protected bool? _InclQtyProductionAllocated;
  protected Decimal? _QtySOFixedProduction;
  protected Decimal? _QtyProdFixedPurchase;
  protected Decimal? _QtyProdFixedProduction;
  protected Decimal? _QtyProdFixedProdOrdersPrepared;
  protected Decimal? _QtyProdFixedProdOrders;
  protected Decimal? _QtyProdFixedSalesOrdersPrepared;
  protected Decimal? _QtyProdFixedSalesOrders;
  protected Decimal? _QtyINReplaned;
  protected bool? _InclQtyINReplaned;
  protected bool? _InclQtyPOFixedReceipt;
  protected bool? _InclQtySOReverse;
  protected Decimal? _QtySOBackOrdered;
  protected bool? _InclQtySOBackOrdered;
  protected Decimal? _QtyAvail;
  protected Decimal? _QtyHardAvail;
  protected Decimal? _QtyActual;
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
  protected string _BaseUnit;
  protected bool? _NegQty;
  protected bool? _InclQtyAvail;
  protected string _Label;
  protected string _Label2;

  [PXDefault]
  [AnyInventory(typeof (Search<InventoryItem.inventoryID, Where<InventoryItem.stkItem, NotEqual<boolFalse>, And<Where<Match<Current<AccessInfo.userName>>>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItemRawExt(typeof (InventoryAllocDetEnqFilter.inventoryID), DisplayName = "Subitem")]
  public virtual string SubItemCD
  {
    get => this._SubItemCD;
    set => this._SubItemCD = value;
  }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubItemCDWildcard
  {
    get => SubCDUtils.CreateSubCDWildcard(this._SubItemCD, "INSUBITEM");
  }

  [Site(DescriptionField = typeof (INSite.descr), DisplayName = "Warehouse")]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [Location(typeof (InventoryAllocDetEnqFilter.siteID), KeepEntry = false, DescriptionField = typeof (INLocation.descr), DisplayName = "Location")]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PX.Objects.IN.LotSerialNbr]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBString(100, IsUnicode = true)]
  public virtual string LotSerialNbrWildcard
  {
    get
    {
      ISqlDialect sqlDialect = PXDatabase.Provider.SqlDialect;
      return sqlDialect.WildcardAnything + this._LotSerialNbr + sqlDialect.WildcardAnything;
    }
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "On Hand", Enabled = false)]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [InventoryAllocationField(IsAddition = true, IsTotal = true)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Addition", Enabled = false)]
  public virtual Decimal? QtyTotalAddition
  {
    get => this._QtyTotalAddition;
    set => this._QtyTotalAddition = value;
  }

  [InventoryAllocationField(IsAddition = true, InclQtyFieldName = "InclQtyPOPrepared", SortOrder = 10)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase Prepared", Enabled = false)]
  public virtual Decimal? QtyPOPrepared
  {
    get => this._QtyPOPrepared;
    set => this._QtyPOPrepared = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyPOPrepared
  {
    get => this._InclQtyPOPrepared;
    set => this._InclQtyPOPrepared = value;
  }

  [InventoryAllocationField(IsAddition = true, InclQtyFieldName = "InclQtyPOOrders", SortOrder = 20)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase Orders", Enabled = false)]
  public virtual Decimal? QtyPOOrders
  {
    get => this._QtyPOOrders;
    set => this._QtyPOOrders = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyPOOrders
  {
    get => this._InclQtyPOOrders;
    set => this._InclQtyPOOrders = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyFixedSOPO { get; set; }

  [InventoryAllocationField(IsAddition = true, InclQtyFieldName = "InclQtyPOReceipts", SortOrder = 30)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "PO Receipts", Enabled = false)]
  public virtual Decimal? QtyPOReceipts
  {
    get => this._QtyPOReceipts;
    set => this._QtyPOReceipts = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyPOReceipts
  {
    get => this._InclQtyPOReceipts;
    set => this._InclQtyPOReceipts = value;
  }

  [InventoryAllocationField(IsAddition = true, InclQtyFieldName = "InclQtyINReceipts", SortOrder = 40)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "IN Receipts [*]", Enabled = false)]
  public virtual Decimal? QtyINReceipts
  {
    get => this._QtyINReceipts;
    set => this._QtyINReceipts = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyINReceipts
  {
    get => this._InclQtyINReceipts;
    set => this._InclQtyINReceipts = value;
  }

  [InventoryAllocationField(IsAddition = false, InclQtyFieldName = "InclQtyINIssues", SortOrder = 190)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "IN Issues [**]", Enabled = false)]
  public virtual Decimal? QtyINIssues
  {
    get => this._QtyINIssues;
    set => this._QtyINIssues = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyINIssues
  {
    get => this._InclQtyINIssues;
    set => this._InclQtyINIssues = value;
  }

  [InventoryAllocationField(IsAddition = true, InclQtyFieldName = "InclQtyInTransit", SortOrder = 50)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "In-Transit [**]", Enabled = false)]
  public virtual Decimal? QtyInTransit
  {
    get => this._QtyInTransit;
    set => this._QtyInTransit = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyInTransit
  {
    get => this._InclQtyInTransit;
    set => this._InclQtyInTransit = value;
  }

  [InventoryAllocationField(IsAddition = false, SortOrder = 60)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "In-Transit to SO", Enabled = false)]
  public virtual Decimal? QtyInTransitToSO
  {
    get => this._QtyInTransitToSO;
    set => this._QtyInTransitToSO = value;
  }

  [InventoryAllocationField(IsAddition = false, IsTotal = true)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Deduction", Enabled = false)]
  public virtual Decimal? QtyTotalDeduction
  {
    get => this._QtyTotalDeduction;
    set => this._QtyTotalDeduction = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "On Loc. Not Available", Enabled = false)]
  public virtual Decimal? QtyNotAvail
  {
    get => this._QtyNotAvail;
    set => this._QtyNotAvail = value;
  }

  /// <summary>Quantity on Hand on Not Available Locations</summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyOnHandNotAvail { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Expired [*]", Enabled = false)]
  public virtual Decimal? QtyExpired
  {
    get => this._QtyExpired;
    set => this._QtyExpired = value;
  }

  [InventoryAllocationField(IsAddition = false, InclQtyFieldName = "InclQtyFSSrvOrdPrepared", SortOrder = 200)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "FS Prepared [**]", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdPrepared
  {
    get => this._QtyFSSrvOrdPrepared;
    set => this._QtyFSSrvOrdPrepared = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual bool? InclQtyFSSrvOrdPrepared
  {
    get => this._InclQtyFSSrvOrdPrepared;
    set => this._InclQtyFSSrvOrdPrepared = value;
  }

  [InventoryAllocationField(IsAddition = false, InclQtyFieldName = "InclQtySOPrepared", SortOrder = 140)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "SO Prepared", Enabled = false)]
  public virtual Decimal? QtySOPrepared
  {
    get => this._QtySOPrepared;
    set => this._QtySOPrepared = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtySOPrepared
  {
    get => this._InclQtySOPrepared;
    set => this._InclQtySOPrepared = value;
  }

  [InventoryAllocationField(IsAddition = false, InclQtyFieldName = "InclQtyFSSrvOrdBooked", SortOrder = 210)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "FS Booked [**]", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdBooked
  {
    get => this._QtyFSSrvOrdBooked;
    set => this._QtyFSSrvOrdBooked = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual bool? InclQtyFSSrvOrdBooked
  {
    get => this._InclQtyFSSrvOrdBooked;
    set => this._InclQtyFSSrvOrdBooked = value;
  }

  [InventoryAllocationField(IsAddition = false, InclQtyFieldName = "InclQtySOBooked", SortOrder = 150)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "SO Booked [**]", Enabled = false)]
  public virtual Decimal? QtySOBooked
  {
    get => this._QtySOBooked;
    set => this._QtySOBooked = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtySOBooked
  {
    get => this._InclQtySOBooked;
    set => this._InclQtySOBooked = value;
  }

  [InventoryAllocationField(IsAddition = false, InclQtyFieldName = "InclQtyFSSrvOrdAllocated", SortOrder = 220)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "FS Allocated [**]", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFSSrvOrdAllocated
  {
    get => this._QtyFSSrvOrdAllocated;
    set => this._QtyFSSrvOrdAllocated = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual bool? InclQtyFSSrvOrdAllocated
  {
    get => this._InclQtyFSSrvOrdAllocated;
    set => this._InclQtyFSSrvOrdAllocated = value;
  }

  [InventoryAllocationField(IsAddition = false, InclQtyFieldName = "InclQtySOShipping", SortOrder = 160 /*0xA0*/)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "SO Allocated [**]", Enabled = false)]
  public virtual Decimal? QtySOShipping
  {
    get => this._QtySOShipping;
    set => this._QtySOShipping = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtySOShipping
  {
    get => this._InclQtySOShipping;
    set => this._InclQtySOShipping = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOShippingReverse { get; set; }

  [InventoryAllocationField(IsAddition = false, InclQtyFieldName = "InclQtySOShipped", SortOrder = 170)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "SO Shipped [**]", Enabled = false)]
  public virtual Decimal? QtySOShipped
  {
    get => this._QtySOShipped;
    set => this._QtySOShipped = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtySOShipped
  {
    get => this._InclQtySOShipped;
    set => this._InclQtySOShipped = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtySOShippedReverse { get; set; }

  [InventoryAllocationField(IsAddition = true, InclQtyFieldName = "InclQtyINAssemblySupply", SortOrder = 70)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Kit Assembly Supply", Enabled = false)]
  public virtual Decimal? QtyINAssemblySupply
  {
    get => this._QtyINAssemblySupply;
    set => this._QtyINAssemblySupply = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyINAssemblySupply
  {
    get => this._InclQtyINAssemblySupply;
    set => this._InclQtyINAssemblySupply = value;
  }

  [InventoryAllocationField(IsAddition = false, InclQtyFieldName = "InclQtyINAssemblyDemand", SortOrder = 230)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Kit Assembly Demand", Enabled = false)]
  public virtual Decimal? QtyINAssemblyDemand
  {
    get => this._QtyINAssemblyDemand;
    set => this._QtyINAssemblyDemand = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyINAssemblyDemand
  {
    get => this._InclQtyINAssemblyDemand;
    set => this._InclQtyINAssemblyDemand = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "In Transit to Production")]
  public virtual Decimal? QtyInTransitToProduction
  {
    get => this._QtyInTransitToProduction;
    set => this._QtyInTransitToProduction = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production Supply Prepared")]
  public virtual Decimal? QtyProductionSupplyPrepared
  {
    get => this._QtyProductionSupplyPrepared;
    set => this._QtyProductionSupplyPrepared = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyProductionSupplyPrepared
  {
    get => this._InclQtyProductionSupplyPrepared;
    set => this._InclQtyProductionSupplyPrepared = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production Supply")]
  public virtual Decimal? QtyProductionSupply
  {
    get => this._QtyProductionSupply;
    set => this._QtyProductionSupply = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyProductionSupply
  {
    get => this._InclQtyProductionSupply;
    set => this._InclQtyProductionSupply = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase for Prod. Prepared")]
  public virtual Decimal? QtyPOFixedProductionPrepared
  {
    get => this._QtyPOFixedProductionPrepared;
    set => this._QtyPOFixedProductionPrepared = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase for Production")]
  public virtual Decimal? QtyPOFixedProductionOrders
  {
    get => this._QtyPOFixedProductionOrders;
    set => this._QtyPOFixedProductionOrders = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production Demand Prepared")]
  public virtual Decimal? QtyProductionDemandPrepared
  {
    get => this._QtyProductionDemandPrepared;
    set => this._QtyProductionDemandPrepared = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyProductionDemandPrepared
  {
    get => this._InclQtyProductionDemandPrepared;
    set => this._InclQtyProductionDemandPrepared = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production Demand")]
  public virtual Decimal? QtyProductionDemand
  {
    get => this._QtyProductionDemand;
    set => this._QtyProductionDemand = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyProductionDemand
  {
    get => this._InclQtyProductionDemand;
    set => this._InclQtyProductionDemand = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production Allocated")]
  public virtual Decimal? QtyProductionAllocated
  {
    get => this._QtyProductionAllocated;
    set => this._QtyProductionAllocated = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyProductionAllocated
  {
    get => this._InclQtyProductionAllocated;
    set => this._InclQtyProductionAllocated = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "SO to Production")]
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
  [PXUIField(DisplayName = "Production to Purchase")]
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
  [PXUIField(DisplayName = "Production to Production")]
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
  [PXUIField(DisplayName = "Production for Prod. Prepared")]
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
  [PXUIField(DisplayName = "Production for Production")]
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
  [PXUIField(DisplayName = "Production for SO Prepared")]
  public virtual Decimal? QtyProdFixedSalesOrdersPrepared
  {
    get => this._QtyProdFixedSalesOrdersPrepared;
    set => this._QtyProdFixedSalesOrdersPrepared = value;
  }

  /// <summary>
  /// Production / Manufacturing
  /// Specifies the quantity On Production for SO Prepared
  /// </summary>
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Production for SO")]
  public virtual Decimal? QtyProdFixedSalesOrders
  {
    get => this._QtyProdFixedSalesOrders;
    set => this._QtyProdFixedSalesOrders = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "In Replanned", Enabled = false)]
  public virtual Decimal? QtyINReplaned
  {
    get => this._QtyINReplaned;
    set => this._QtyINReplaned = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyINReplaned
  {
    get => this._InclQtyINReplaned;
    set => this._InclQtyINReplaned = value;
  }

  [PXBool]
  [PXDefault(typeof (False))]
  public virtual bool? InclQtyPOFixedReceipt
  {
    get => this._InclQtyPOFixedReceipt;
    set => this._InclQtyPOFixedReceipt = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtySOReverse
  {
    get => this._InclQtySOReverse;
    set => this._InclQtySOReverse = value;
  }

  [InventoryAllocationField(IsAddition = false, InclQtyFieldName = "InclQtySOBackOrdered", SortOrder = 180)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "SO Back Ordered [**]", Enabled = false)]
  public virtual Decimal? QtySOBackOrdered
  {
    get => this._QtySOBackOrdered;
    set => this._QtySOBackOrdered = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtySOBackOrdered
  {
    get => this._InclQtySOBackOrdered;
    set => this._InclQtySOBackOrdered = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Available", Enabled = false)]
  public virtual Decimal? QtyAvail
  {
    get => this._QtyAvail;
    set => this._QtyAvail = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Available for Shipping", Enabled = false)]
  public virtual Decimal? QtyHardAvail
  {
    get => this._QtyHardAvail;
    set => this._QtyHardAvail = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Available for Issue", Enabled = false)]
  public virtual Decimal? QtyActual
  {
    get => this._QtyActual;
    set => this._QtyActual = value;
  }

  [InventoryAllocationField(IsAddition = true, SortOrder = 260)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "FS to Purchase", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyFixedFSSrvOrd
  {
    get => this._QtyFixedFSSrvOrd;
    set => this._QtyFixedFSSrvOrd = value;
  }

  [InventoryAllocationField(IsAddition = true, SortOrder = 290)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase for FS", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyPOFixedFSSrvOrd
  {
    get => this._QtyPOFixedFSSrvOrd;
    set => this._QtyPOFixedFSSrvOrd = value;
  }

  [InventoryAllocationField(IsAddition = true, SortOrder = 280)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase for FS Prepared", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyPOFixedFSSrvOrdPrepared
  {
    get => this._QtyPOFixedFSSrvOrdPrepared;
    set => this._QtyPOFixedFSSrvOrdPrepared = value;
  }

  [InventoryAllocationField(IsAddition = true, SortOrder = 300)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Receipts for FS", Enabled = false, FieldClass = "SERVICEMANAGEMENT")]
  public virtual Decimal? QtyPOFixedFSSrvOrdReceipts
  {
    get => this._QtyPOFixedFSSrvOrdReceipts;
    set => this._QtyPOFixedFSSrvOrdReceipts = value;
  }

  [InventoryAllocationField(IsAddition = false, InclQtyFieldName = "InclQtySOFixed", SortOrder = 240 /*0xF0*/)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "SO to Purchase", Enabled = false)]
  public virtual Decimal? QtySOFixed
  {
    get => this._QtySOFixed;
    set => this._QtySOFixed = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtySOFixed { get; set; }

  [InventoryAllocationField(IsAddition = true, InclQtyFieldName = "InclQtyPOFixedOrders", SortOrder = 90)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase for SO", Enabled = false)]
  public virtual Decimal? QtyPOFixedOrders
  {
    get => this._QtyPOFixedOrders;
    set => this._QtyPOFixedOrders = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyPOFixedOrders { get; set; }

  [InventoryAllocationField(IsAddition = true, InclQtyFieldName = "InclQtyPOFixedPrepared", SortOrder = 80 /*0x50*/)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Purchase for SO Prepared", Enabled = false)]
  public virtual Decimal? QtyPOFixedPrepared
  {
    get => this._QtyPOFixedPrepared;
    set => this._QtyPOFixedPrepared = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyPOFixedPrepared { get; set; }

  [InventoryAllocationField(IsAddition = true, InclQtyFieldName = "InclQtyPOFixedReceipts", SortOrder = 100)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Receipts for SO", Enabled = false)]
  public virtual Decimal? QtyPOFixedReceipts
  {
    get => this._QtyPOFixedReceipts;
    set => this._QtyPOFixedReceipts = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = " ", Enabled = false)]
  public virtual bool? InclQtyPOFixedReceipts { get; set; }

  [InventoryAllocationField(IsAddition = false, SortOrder = 250)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "SO to Drop-Ship", Enabled = false)]
  public virtual Decimal? QtySODropShip
  {
    get => this._QtySODropShip;
    set => this._QtySODropShip = value;
  }

  [InventoryAllocationField(IsAddition = true, SortOrder = 120)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop-Ship for SO", Enabled = false)]
  public virtual Decimal? QtyPODropShipOrders
  {
    get => this._QtyPODropShipOrders;
    set => this._QtyPODropShipOrders = value;
  }

  [InventoryAllocationField(IsAddition = true, SortOrder = 110)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop-Ship for SO Prepared", Enabled = false)]
  public virtual Decimal? QtyPODropShipPrepared
  {
    get => this._QtyPODropShipPrepared;
    set => this._QtyPODropShipPrepared = value;
  }

  [InventoryAllocationField(IsAddition = true, SortOrder = 130)]
  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Drop-Ship for SO Receipts", Enabled = false)]
  public virtual Decimal? QtyPODropShipReceipts
  {
    get => this._QtyPODropShipReceipts;
    set => this._QtyPODropShipReceipts = value;
  }

  [PXDefault("")]
  [INUnit(DisplayName = "Base Unit", Enabled = false)]
  public virtual string BaseUnit
  {
    get => this._BaseUnit;
    set => this._BaseUnit = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? NegQty
  {
    get => this._NegQty;
    set => this._NegQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? InclQtyAvail
  {
    get => this._InclQtyAvail;
    set => this._InclQtyAvail = value;
  }

  [PXString]
  [PXUIField]
  [PXDefault("[*]  Except Location Not Available")]
  public virtual string Label
  {
    get => this._Label;
    set => this._Label = value;
  }

  [PXString]
  [PXUIField]
  [PXDefault("[**] Except Expired and  Loc. Not Available")]
  public virtual string Label2
  {
    get => this._Label2;
    set => this._Label2 = value;
  }

  public abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inventoryID>
  {
  }

  public abstract class subItemCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.subItemCD>
  {
  }

  public abstract class subItemCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.subItemCDWildcard>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryAllocDetEnqFilter.siteID>
  {
  }

  public abstract class locationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.locationID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.lotSerialNbr>
  {
  }

  public abstract class lotSerialNbrWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.lotSerialNbrWildcard>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyOnHand>
  {
  }

  public abstract class qtyTotalAddition : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyTotalAddition>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPOPrepared>
  {
  }

  public abstract class inclQtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyPOPrepared>
  {
  }

  public abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPOOrders>
  {
  }

  public abstract class inclQtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyPOOrders>
  {
  }

  public abstract class inclQtyFixedSOPO : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyFixedSOPO>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPOReceipts>
  {
  }

  public abstract class inclQtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyPOReceipts>
  {
  }

  public abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyINReceipts>
  {
  }

  public abstract class inclQtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyINReceipts>
  {
  }

  public abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyINIssues>
  {
  }

  public abstract class inclQtyINIssues : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyINIssues>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyInTransit>
  {
  }

  public abstract class inclQtyInTransit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyInTransit>
  {
  }

  public abstract class qtyInTransitToSO : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyInTransitToSO>
  {
  }

  public abstract class qtyTotalDeduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyTotalDeduction>
  {
  }

  public abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyNotAvail>
  {
  }

  public abstract class qtyOnHandNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyOnHandNotAvail>
  {
  }

  public abstract class qtyExpired : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyExpired>
  {
  }

  public abstract class qtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyFSSrvOrdPrepared>
  {
  }

  public abstract class inclQtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyFSSrvOrdPrepared>
  {
  }

  public abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtySOPrepared>
  {
  }

  public abstract class inclQtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtySOPrepared>
  {
  }

  public abstract class qtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyFSSrvOrdBooked>
  {
  }

  public abstract class inclQtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyFSSrvOrdBooked>
  {
  }

  public abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtySOBooked>
  {
  }

  public abstract class inclQtySOBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtySOBooked>
  {
  }

  public abstract class qtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyFSSrvOrdAllocated>
  {
  }

  public abstract class inclQtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyFSSrvOrdAllocated>
  {
  }

  public abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtySOShipping>
  {
  }

  public abstract class inclQtySOShipping : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtySOShipping>
  {
  }

  public abstract class qtySOShippingReverse : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtySOShippingReverse>
  {
  }

  public abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtySOShipped>
  {
  }

  public abstract class inclQtySOShipped : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtySOShipped>
  {
  }

  public abstract class qtySOShippedReverse : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtySOShippedReverse>
  {
  }

  public abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyINAssemblySupply>
  {
  }

  public abstract class inclQtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyINAssemblySupply>
  {
  }

  public abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyINAssemblyDemand>
  {
  }

  public abstract class inclQtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyINAssemblyDemand>
  {
  }

  public abstract class qtyInTransitToProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyInTransitToProduction>
  {
  }

  public abstract class qtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyProductionSupplyPrepared>
  {
  }

  public abstract class inclQtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyProductionSupplyPrepared>
  {
  }

  public abstract class qtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyProductionSupply>
  {
  }

  public abstract class inclQtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyProductionSupply>
  {
  }

  public abstract class qtyPOFixedProductionPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPOFixedProductionPrepared>
  {
  }

  public abstract class qtyPOFixedProductionOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPOFixedProductionOrders>
  {
  }

  public abstract class qtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyProductionDemandPrepared>
  {
  }

  public abstract class inclQtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyProductionDemandPrepared>
  {
  }

  public abstract class qtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyProductionDemand>
  {
  }

  public abstract class inclQtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyProductionDemand>
  {
  }

  public abstract class qtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyProductionAllocated>
  {
  }

  public abstract class inclQtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyProductionAllocated>
  {
  }

  public abstract class qtySOFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtySOFixedProduction>
  {
  }

  public abstract class qtyProdFixedPurchase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyProdFixedPurchase>
  {
  }

  public abstract class qtyProdFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyProdFixedProduction>
  {
  }

  public abstract class qtyProdFixedProdOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyProdFixedProdOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedProdOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyProdFixedProdOrders>
  {
  }

  public abstract class qtyProdFixedSalesOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyProdFixedSalesOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedSalesOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyProdFixedSalesOrders>
  {
  }

  public abstract class qtyINReplaned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyINReplaned>
  {
  }

  public abstract class inclQtyINReplaned : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyINReplaned>
  {
  }

  public abstract class inclQtyPOFixedReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyPOFixedReceipt>
  {
  }

  public abstract class inclQtySOReverse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtySOReverse>
  {
  }

  public abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtySOBackOrdered>
  {
  }

  public abstract class inclQtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtySOBackOrdered>
  {
  }

  public abstract class qtyAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyAvail>
  {
  }

  public abstract class qtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyHardAvail>
  {
  }

  public abstract class qtyActual : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyActual>
  {
  }

  public abstract class qtyFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrd : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPOFixedFSSrvOrd>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPOFixedFSSrvOrdPrepared>
  {
  }

  public abstract class qtyPOFixedFSSrvOrdReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPOFixedFSSrvOrdReceipts>
  {
  }

  public abstract class qtySOFixed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtySOFixed>
  {
  }

  public abstract class inclQtySOFixed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtySOFixed>
  {
  }

  public abstract class qtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPOFixedOrders>
  {
  }

  public abstract class inclQtyPOFixedOrders : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyPOFixedOrders>
  {
  }

  public abstract class qtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPOFixedPrepared>
  {
  }

  public abstract class inclQtyPOFixedPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyPOFixedPrepared>
  {
  }

  public abstract class qtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPOFixedReceipts>
  {
  }

  public abstract class inclQtyPOFixedReceipts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyPOFixedReceipts>
  {
  }

  public abstract class qtySODropShip : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtySODropShip>
  {
  }

  public abstract class qtyPODropShipOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPODropShipOrders>
  {
  }

  public abstract class qtyPODropShipPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPODropShipPrepared>
  {
  }

  public abstract class qtyPODropShipReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.qtyPODropShipReceipts>
  {
  }

  public abstract class baseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.baseUnit>
  {
  }

  public abstract class negQty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InventoryAllocDetEnqFilter.negQty>
  {
  }

  public abstract class inclQtyAvail : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    InventoryAllocDetEnqFilter.inclQtyAvail>
  {
  }

  public abstract class label : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryAllocDetEnqFilter.label>
  {
  }

  public abstract class label2 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InventoryAllocDetEnqFilter.label2>
  {
  }
}
