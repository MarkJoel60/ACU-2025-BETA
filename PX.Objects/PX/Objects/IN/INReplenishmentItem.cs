// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INReplenishmentItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.SQLTree;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP.Standalone;
using PX.Objects.PO;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.IN;

[INReplenishmentProjection]
public class INReplenishmentItem : PX.Objects.IN.S.INItemSite
{
  protected int? _SubItemID;
  protected 
  #nullable disable
  string _Descr;
  protected string _PreferredVendorName;
  protected int? _DefaultVendorLocationID;
  protected int? _ItemClassID;
  protected string _DemandCalculation;
  protected string _VendorClassID;
  protected Decimal? _PurchaseERQ;
  protected bool? _InclQtySOBackOrdered;
  protected bool? _InclQtyFSSrvOrdPrepared;
  protected bool? _InclQtyFSSrvOrdBooked;
  protected bool? _InclQtyFSSrvOrdAllocated;
  protected bool? _InclQtySOPrepared;
  protected bool? _InclQtySOBooked;
  protected bool? _InclQtySOShipped;
  protected bool? _InclQtySOShipping;
  protected bool? _InclQtyINIssues;
  protected bool? _InclQtyINAssemblyDemand;
  protected bool? _InclQtyProductionSupplyPrepared;
  protected bool? _InclQtyProductionSupply;
  protected bool? _InclQtyProductionDemandPrepared;
  protected bool? _InclQtyProductionDemand;
  protected bool? _InclQtyProductionAllocated;
  protected Decimal? _QtyProcess;
  protected Decimal? _QtyOnHand;
  protected Decimal? _QtyPOPrepared;
  protected Decimal? _QtyPOOrders;
  protected Decimal? _QtyPOReceipts;
  protected Decimal? _QtyInTransit;
  protected Decimal? _QtyINReceipts;
  protected Decimal? _QtyINAssemblySupply;
  protected Decimal? _QtyInTransitToProduction;
  protected Decimal? _QtyProductionSupplyPrepared;
  protected Decimal? _QtyProductionSupply;
  protected Decimal? _QtyPOFixedProductionPrepared;
  protected Decimal? _QtyPOFixedProductionOrders;
  protected Decimal? _QtyFSSrvOrdPrepared;
  protected Decimal? _QtyFSSrvOrdBooked;
  protected Decimal? _QtyFSSrvOrdAllocated;
  protected Decimal? _QtySOBackOrdered;
  protected Decimal? _QtySOPrepared;
  protected Decimal? _QtySOBooked;
  protected Decimal? _QtySOShipped;
  protected Decimal? _QtySOShipping;
  protected Decimal? _QtyINIssues;
  protected Decimal? _QtyINAssemblyDemand;
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
  protected Decimal? _QtyReplenishment;
  protected Decimal? _QtyHardDemand;
  protected Decimal? _QtyDemand;
  protected Decimal? _QtyProcessInt;
  protected int? _DefaultSubItemID;

  [StockItem(DirtyRead = true, DisplayName = "Inventory ID")]
  [PXParent(typeof (INReplenishmentItem.FK.InventoryItem))]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [InventoryRaw(IsKey = true, BqlField = typeof (InventoryItem.inventoryCD), DisplayName = "")]
  public virtual string InventoryCD { get; set; }

  [Site]
  [PXParent(typeof (INReplenishmentItem.FK.Site))]
  public override int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [SiteRaw(true, IsKey = true, BqlField = typeof (INSite.siteCD))]
  public virtual string SiteCD { get; set; }

  [SubItem(BqlField = typeof (INSubItem.subItemID))]
  [PXDefault]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [SubItemRaw(IsKey = true, BqlField = typeof (INSubItem.subItemCD))]
  public virtual string SubItemCD { get; set; }

  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true, BqlField = typeof (InventoryItem.descr), IsProjection = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [Site(DisplayName = "Source Warehouse", DescriptionField = typeof (INSite.descr))]
  public override int? ReplenishmentSourceSiteID
  {
    get => this._ReplenishmentSourceSiteID;
    set => this._ReplenishmentSourceSiteID = value;
  }

  [VendorNonEmployeeActive(DisplayName = "Preferred Vendor ID", Required = false, DescriptionField = typeof (PX.Objects.AP.Vendor.acctName), BqlField = typeof (INItemSite.preferredVendorID))]
  public override int? PreferredVendorID
  {
    get => this._PreferredVendorID;
    set => this._PreferredVendorID = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<INReplenishmentItem.preferredVendorID>>>))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.defLocationID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<INReplenishmentItem.preferredVendorID>>>>))]
  [PXFormula(typeof (Default<INReplenishmentItem.preferredVendorID>))]
  public override int? PreferredVendorLocationID
  {
    get => this._PreferredVendorLocationID;
    set => this._PreferredVendorLocationID = value;
  }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (PX.Objects.AP.Vendor.acctName))]
  [PXDefault(typeof (Search<BAccountR.acctName, Where<BAccountR.bAccountID, Equal<Current<INReplenishmentItem.preferredVendorID>>>>))]
  [PXUIField]
  [PXFormula(typeof (Default<INReplenishmentItem.preferredVendorID>))]
  public virtual string PreferredVendorName
  {
    get => this._PreferredVendorName;
    set => this._PreferredVendorName = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<INReplenishmentItem.preferredVendorID>>>))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.defLocationID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<INReplenishmentItem.preferredVendorID>>>>))]
  [PXFormula(typeof (Default<INReplenishmentItem.preferredVendorID>))]
  public virtual int? DefaultVendorLocationID
  {
    get => this._DefaultVendorLocationID;
    set => this._DefaultVendorLocationID = value;
  }

  [PXDBInt(BqlField = typeof (InventoryItem.itemClassID))]
  [PXUIField]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID, Where<INItemClass.stkItem, Equal<boolTrue>>>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
  [PXDefault(typeof (Search<INSetup.dfltStkItemClassID>))]
  public virtual int? ItemClassID
  {
    get => this._ItemClassID;
    set => this._ItemClassID = value;
  }

  [INUnit(BqlField = typeof (InventoryItem.baseUnit))]
  public virtual string BaseUnit { get; set; }

  [INUnit(typeof (INReplenishmentItem.inventoryID), DisplayName = "Purchase UOM", BqlField = typeof (InventoryItem.purchaseUnit))]
  public virtual string PurchaseUnit { get; set; }

  [PXString(1, IsFixed = true, BqlTable = typeof (INItemClass))]
  [PXDBCalced(typeof (IsNull<INItemClass.demandCalculation, StringEmpty>), typeof (string))]
  [PXUIField(DisplayName = "Demand Calculation")]
  [PXDefault("I")]
  [INDemandCalculation.List]
  public virtual string DemandCalculation
  {
    get => this._DemandCalculation;
    set => this._DemandCalculation = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.AP.Vendor.vendorClassID))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.vendorClassID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<INReplenishmentItem.preferredVendorID>>>>))]
  [PXSelector(typeof (Search2<VendorClass.vendorClassID, LeftJoin<EPEmployeeClass, On<EPEmployeeClass.vendorClassID, Equal<VendorClass.vendorClassID>>>, Where<EPEmployeeClass.vendorClassID, IsNull>>), DescriptionField = typeof (VendorClass.descr), CacheGlobal = true)]
  [PXFormula(typeof (Default<INReplenishmentItem.preferredVendorID>))]
  [PXUIField(DisplayName = "Vendor Class")]
  public virtual string VendorClassID
  {
    get => this._VendorClassID;
    set => this._VendorClassID = value;
  }

  [PXDecimal]
  [PXDBCalced(typeof (IsNull<INItemSiteReplenishment.safetyStock, INItemSite.safetyStock>), typeof (Decimal))]
  [PXUIField(DisplayName = "Safety Stock")]
  public override Decimal? SafetyStock
  {
    get => this._SafetyStock;
    set => this._SafetyStock = value;
  }

  [PXDecimal]
  [PXDBCalced(typeof (IsNull<INItemSiteReplenishment.minQty, INItemSite.minQty>), typeof (Decimal))]
  [PXUIField(DisplayName = "Reorder Point")]
  public override Decimal? MinQty
  {
    get => this._MinQty;
    set => this._MinQty = value;
  }

  [PXDecimal]
  [PXDBCalced(typeof (IsNull<INItemSiteReplenishment.maxQty, INItemSite.maxQty>), typeof (Decimal))]
  [PXUIField(DisplayName = "Max. Qty.")]
  public override Decimal? MaxQty
  {
    get => this._MaxQty;
    set => this._MaxQty = value;
  }

  [PXDecimal]
  [PXDBCalced(typeof (IsNull<POVendorInventoryRepo.eRQ, POVendorInventoryRepo.nullERQ>), typeof (Decimal))]
  [PXUIField(DisplayName = "Purchase ERQ")]
  public virtual Decimal? PurchaseERQ
  {
    get => this._PurchaseERQ;
    set => this._PurchaseERQ = value;
  }

  [PXDecimal]
  [PXDBCalced(typeof (IsNull<INItemSiteReplenishment.transferERQ, INItemSite.transferERQ>), typeof (Decimal))]
  [PXUIField(DisplayName = "Transfer ERQ")]
  public override Decimal? TransferERQ
  {
    get => this._TransferERQ;
    set => this._TransferERQ = value;
  }

  [PXDBDate(BqlField = typeof (INItemSite.launchDate))]
  [PXUIField(DisplayName = "Launch Date")]
  public override DateTime? LaunchDate
  {
    get => this._LaunchDate;
    set => this._LaunchDate = value;
  }

  [PXDBDate(BqlField = typeof (INItemSite.terminationDate))]
  [PXUIField(DisplayName = "Termination Date")]
  public override DateTime? TerminationDate
  {
    get => this._TerminationDate;
    set => this._TerminationDate = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa", BqlField = typeof (INItemSite.replenishmentPolicyID))]
  [PXUIField(DisplayName = "Replenishment Policy ID")]
  [PXSelector(typeof (Search<INReplenishmentPolicy.replenishmentPolicyID>), DescriptionField = typeof (INReplenishmentPolicy.descr))]
  public override string ReplenishmentPolicyID
  {
    get => this._ReplenishmentPolicyID;
    set => this._ReplenishmentPolicyID = value;
  }

  [PXString(1, IsFixed = true)]
  [PXDBCalced(typeof (Switch<Case<Where<INItemSite.replenishmentSource, IsNull, Or<Where<INItemSite.replenishmentSource, NotEqual<INReplenishmentSource.purchased>, And<INItemSite.replenishmentSource, NotEqual<INReplenishmentSource.transfer>>>>>, INReplenishmentSource.none>, INItemSite.replenishmentSource>), typeof (string))]
  [PXUIField(DisplayName = "Replenishment Source")]
  [INReplenishmentSource.INPlanList]
  [PXDefault("P")]
  public override string ReplenishmentSource
  {
    get => this._ReplenishmentSource;
    set => this._ReplenishmentSource = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtySOBackOrdered))]
  [PXUIField(DisplayName = "Deduct Qty. on Back Orders")]
  public virtual bool? InclQtySOBackOrdered
  {
    get => this._InclQtySOBackOrdered;
    set => this._InclQtySOBackOrdered = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtyFSSrvOrdPrepared))]
  [PXUIField(DisplayName = "Deduct Qty. SrvOrd Prepared", FieldClass = "SERVICEMANAGEMENT")]
  public virtual bool? InclQtyFSSrvOrdPrepared
  {
    get => this._InclQtyFSSrvOrdPrepared;
    set => this._InclQtyFSSrvOrdPrepared = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtyFSSrvOrdBooked))]
  [PXUIField(DisplayName = "Deduct Qty. on Customer Service Orders", FieldClass = "SERVICEMANAGEMENT")]
  public virtual bool? InclQtyFSSrvOrdBooked
  {
    get => this._InclQtyFSSrvOrdBooked;
    set => this._InclQtyFSSrvOrdBooked = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtyFSSrvOrdAllocated))]
  [PXUIField(DisplayName = "Deduct Qty. SrvOrd Allocated", FieldClass = "SERVICEMANAGEMENT")]
  public virtual bool? InclQtyFSSrvOrdAllocated
  {
    get => this._InclQtyFSSrvOrdAllocated;
    set => this._InclQtyFSSrvOrdAllocated = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtySOPrepared))]
  [PXUIField(DisplayName = "Deduct Qty. on Sales Prepared")]
  public virtual bool? InclQtySOPrepared
  {
    get => this._InclQtySOPrepared;
    set => this._InclQtySOPrepared = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtySOBooked))]
  [PXUIField(DisplayName = "Deduct Qty. on Customer Orders")]
  public virtual bool? InclQtySOBooked
  {
    get => this._InclQtySOBooked;
    set => this._InclQtySOBooked = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtySOShipped))]
  [PXUIField(DisplayName = "Deduct Qty. Shipped")]
  public virtual bool? InclQtySOShipped
  {
    get => this._InclQtySOShipped;
    set => this._InclQtySOShipped = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtySOShipping))]
  [PXUIField(DisplayName = "Deduct Qty. Shipping")]
  public virtual bool? InclQtySOShipping
  {
    get => this._InclQtySOShipping;
    set => this._InclQtySOShipping = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtyINIssues))]
  [PXUIField(DisplayName = "Deduct Qty. on Issues")]
  public virtual bool? InclQtyINIssues
  {
    get => this._InclQtyINIssues;
    set => this._InclQtyINIssues = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtyINAssemblyDemand))]
  [PXUIField(DisplayName = "Deduct Qty. of Kit Assembly Demand")]
  public virtual bool? InclQtyINAssemblyDemand
  {
    get => this._InclQtyINAssemblyDemand;
    set => this._InclQtyINAssemblyDemand = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtyProductionSupplyPrepared))]
  [PXUIField(DisplayName = "Include Qty. of Production Supply Prepared")]
  public virtual bool? InclQtyProductionSupplyPrepared
  {
    get => this._InclQtyProductionSupplyPrepared;
    set => this._InclQtyProductionSupplyPrepared = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtyProductionSupply))]
  [PXUIField(DisplayName = "Include Qty. of Production Supply")]
  public virtual bool? InclQtyProductionSupply
  {
    get => this._InclQtyProductionSupply;
    set => this._InclQtyProductionSupply = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtyProductionDemandPrepared))]
  [PXUIField(DisplayName = "Deduct Qty. on Production Demand Prepared")]
  public virtual bool? InclQtyProductionDemandPrepared
  {
    get => this._InclQtyProductionDemandPrepared;
    set => this._InclQtyProductionDemandPrepared = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtyProductionDemand))]
  [PXUIField(DisplayName = "Deduct Qty. on Production Demand")]
  public virtual bool? InclQtyProductionDemand
  {
    get => this._InclQtyProductionDemand;
    set => this._InclQtyProductionDemand = value;
  }

  [PXDBBool(BqlField = typeof (INAvailabilityScheme.inclQtyProductionAllocated))]
  [PXUIField(DisplayName = "Deduct Qty. on Production Allocated")]
  public virtual bool? InclQtyProductionAllocated
  {
    get => this._InclQtyProductionAllocated;
    set => this._InclQtyProductionAllocated = value;
  }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. To Process")]
  public virtual Decimal? QtyProcess
  {
    get => this._QtyProcess;
    set => this._QtyProcess = value;
  }

  [PXBool]
  public virtual bool? QtyProcessRounded { get; set; }

  [PXDBBool(BqlField = typeof (InventoryItem.decimalBaseUnit))]
  public virtual bool? DecimalBaseUnit { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyOnHand, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. on Hand")]
  public virtual Decimal? QtyOnHand
  {
    get => this._QtyOnHand;
    set => this._QtyOnHand = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (Add<IsNull<INSiteStatusByCostCenter.qtyPOFixedPrepared, decimal0>, Add<IsNull<INSiteStatusByCostCenter.qtyPOFixedOrders, decimal0>, Sub<IsNull<INSiteStatusByCostCenter.qtyPOFixedReceipts, decimal0>, IsNull<INSiteStatusByCostCenter.qtySOFixed, decimal0>>>>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyFixedSOPODiff { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyNotAvail, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. Not Available")]
  public virtual Decimal? QtyNotAvail { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyPOPrepared, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyPOPrepared
  {
    get => this._QtyPOPrepared;
    set => this._QtyPOPrepared = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (Add<IIf<Where<INAvailabilityScheme.inclQtyFixedSOPO, Equal<True>, And<INReplenishmentItem.qtyFixedSOPODiff, Greater<decimal0>>>, INReplenishmentItem.qtyFixedSOPODiff, decimal0>, IsNull<INSiteStatusByCostCenter.qtyPOOrders, decimal0>>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyPOOrders
  {
    get => this._QtyPOOrders;
    set => this._QtyPOOrders = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyPOReceipts, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyPOReceipts
  {
    get => this._QtyPOReceipts;
    set => this._QtyPOReceipts = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyInTransit, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyInTransit
  {
    get => this._QtyInTransit;
    set => this._QtyInTransit = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyINReceipts, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyINReceipts
  {
    get => this._QtyINReceipts;
    set => this._QtyINReceipts = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyINAssemblySupply, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyINAssemblySupply
  {
    get => this._QtyINAssemblySupply;
    set => this._QtyINAssemblySupply = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyInTransitToProduction, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty In Transit to Production")]
  public virtual Decimal? QtyInTransitToProduction
  {
    get => this._QtyInTransitToProduction;
    set => this._QtyInTransitToProduction = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyProductionSupplyPrepared, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty Production Supply Prepared")]
  public virtual Decimal? QtyProductionSupplyPrepared
  {
    get => this._QtyProductionSupplyPrepared;
    set => this._QtyProductionSupplyPrepared = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyProductionSupply, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on Production Supply")]
  public virtual Decimal? QtyProductionSupply
  {
    get => this._QtyProductionSupply;
    set => this._QtyProductionSupply = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyPOFixedProductionPrepared, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on Purchase for Prod. Prepared")]
  public virtual Decimal? QtyPOFixedProductionPrepared
  {
    get => this._QtyPOFixedProductionPrepared;
    set => this._QtyPOFixedProductionPrepared = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyPOFixedProductionOrders, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on Purchase for Production")]
  public virtual Decimal? QtyPOFixedProductionOrders
  {
    get => this._QtyPOFixedProductionOrders;
    set => this._QtyPOFixedProductionOrders = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyFSSrvOrdPrepared, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyFSSrvOrdPrepared
  {
    get => this._QtyFSSrvOrdPrepared;
    set => this._QtyFSSrvOrdPrepared = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyFSSrvOrdBooked, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyFSSrvOrdBooked
  {
    get => this._QtyFSSrvOrdBooked;
    set => this._QtyFSSrvOrdBooked = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyFSSrvOrdAllocated, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyFSSrvOrdAllocated
  {
    get => this._QtyFSSrvOrdAllocated;
    set => this._QtyFSSrvOrdAllocated = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtySOBackOrdered, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtySOBackOrdered
  {
    get => this._QtySOBackOrdered;
    set => this._QtySOBackOrdered = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtySOPrepared, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtySOPrepared
  {
    get => this._QtySOPrepared;
    set => this._QtySOPrepared = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtySOBooked, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtySOBooked
  {
    get => this._QtySOBooked;
    set => this._QtySOBooked = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtySOShipped, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtySOShipped
  {
    get => this._QtySOShipped;
    set => this._QtySOShipped = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtySOShipping, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtySOShipping
  {
    get => this._QtySOShipping;
    set => this._QtySOShipping = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyINIssues, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyINIssues
  {
    get => this._QtyINIssues;
    set => this._QtyINIssues = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyINAssemblyDemand, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? QtyINAssemblyDemand
  {
    get => this._QtyINAssemblyDemand;
    set => this._QtyINAssemblyDemand = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyProductionDemandPrepared, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on Production Demand Prepared")]
  public virtual Decimal? QtyProductionDemandPrepared
  {
    get => this._QtyProductionDemandPrepared;
    set => this._QtyProductionDemandPrepared = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyProductionDemand, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on Production Demand")]
  public virtual Decimal? QtyProductionDemand
  {
    get => this._QtyProductionDemand;
    set => this._QtyProductionDemand = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyProductionAllocated, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on Production Allocated")]
  public virtual Decimal? QtyProductionAllocated
  {
    get => this._QtyProductionAllocated;
    set => this._QtyProductionAllocated = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtySOFixedProduction, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on SO to Production")]
  public virtual Decimal? QtySOFixedProduction
  {
    get => this._QtySOFixedProduction;
    set => this._QtySOFixedProduction = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyProdFixedPurchase, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on Production to Purchase")]
  public virtual Decimal? QtyProdFixedPurchase
  {
    get => this._QtyProdFixedPurchase;
    set => this._QtyProdFixedPurchase = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyProdFixedProduction, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on Production to Production")]
  public virtual Decimal? QtyProdFixedProduction
  {
    get => this._QtyProdFixedProduction;
    set => this._QtyProdFixedProduction = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyProdFixedProdOrdersPrepared, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on Production for Prod. Prepared")]
  public virtual Decimal? QtyProdFixedProdOrdersPrepared
  {
    get => this._QtyProdFixedProdOrdersPrepared;
    set => this._QtyProdFixedProdOrdersPrepared = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyProdFixedProdOrders, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on Production for Production")]
  public virtual Decimal? QtyProdFixedProdOrders
  {
    get => this._QtyProdFixedProdOrders;
    set => this._QtyProdFixedProdOrders = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyProdFixedSalesOrdersPrepared, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on Production for SO Prepared")]
  public virtual Decimal? QtyProdFixedSalesOrdersPrepared
  {
    get => this._QtyProdFixedSalesOrdersPrepared;
    set => this._QtyProdFixedSalesOrdersPrepared = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyProdFixedSalesOrders, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty on Production for SO")]
  public virtual Decimal? QtyProdFixedSalesOrders
  {
    get => this._QtyProdFixedSalesOrders;
    set => this._QtyProdFixedSalesOrders = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INSiteStatusByCostCenter.qtyINReplaned, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Replenishment Qty.")]
  public virtual Decimal? QtyINReplaned
  {
    get => this._QtyINReplaned;
    set => this._QtyINReplaned = value;
  }

  [PXDecimal]
  [PXDBCalced(typeof (Add<INReplenishmentItem.qtyPOPrepared, Add<INReplenishmentItem.qtyPOOrders, Add<INReplenishmentItem.qtyPOReceipts, Add<INReplenishmentItem.qtyInTransit, Add<INReplenishmentItem.qtyINReceipts, INReplenishmentItem.qtyINAssemblySupply>>>>>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. on Supply")]
  public virtual Decimal? QtyReplenishment
  {
    get => this._QtyReplenishment;
    set => this._QtyReplenishment = value;
  }

  [PXDecimal]
  [PXDBCalced(typeof (Add<INReplenishmentItem.qtySOShipped, Add<INReplenishmentItem.qtySOShipping, INReplenishmentItem.qtySOBackOrdered>>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. on Hard Demand")]
  public virtual Decimal? QtyHardDemand
  {
    get => this._QtyHardDemand;
    set => this._QtyHardDemand = value;
  }

  [PXDecimal]
  [PXDBCalced(typeof (Add<IIf<Where<INAvailabilityScheme.inclQtySOPrepared, Equal<True>>, INReplenishmentItem.qtySOPrepared, decimal0>, Add<IIf<Where<INAvailabilityScheme.inclQtyFSSrvOrdPrepared, Equal<True>>, INReplenishmentItem.qtyFSSrvOrdPrepared, decimal0>, Add<IIf<Where<INAvailabilityScheme.inclQtyFSSrvOrdBooked, Equal<True>>, INReplenishmentItem.qtyFSSrvOrdBooked, decimal0>, Add<IIf<Where<INAvailabilityScheme.inclQtyFSSrvOrdAllocated, Equal<True>>, INReplenishmentItem.qtyFSSrvOrdAllocated, decimal0>, Add<IIf<Where<INAvailabilityScheme.inclQtySOBooked, Equal<True>>, INReplenishmentItem.qtySOBooked, decimal0>, Add<IIf<Where<INAvailabilityScheme.inclQtySOShipping, Equal<True>>, INReplenishmentItem.qtySOShipping, decimal0>, Add<IIf<Where<INAvailabilityScheme.inclQtySOShipped, Equal<True>>, INReplenishmentItem.qtySOShipped, decimal0>, Add<IIf<Where<INAvailabilityScheme.inclQtySOBackOrdered, Equal<True>>, INReplenishmentItem.qtySOBackOrdered, decimal0>, Add<IIf<Where<INAvailabilityScheme.inclQtyINIssues, Equal<True>>, INReplenishmentItem.qtyINIssues, decimal0>, Add<IIf<Where<INAvailabilityScheme.inclQtyINAssemblyDemand, Equal<True>>, INReplenishmentItem.qtyINAssemblyDemand, decimal0>, Add<IIf<Where<INAvailabilityScheme.inclQtyProductionDemandPrepared, Equal<True>>, INReplenishmentItem.qtyProductionDemandPrepared, decimal0>, Add<IIf<Where<INAvailabilityScheme.inclQtyProductionDemand, Equal<True>>, INReplenishmentItem.qtyProductionDemand, decimal0>, IIf<Where<INAvailabilityScheme.inclQtyProductionAllocated, Equal<True>>, INReplenishmentItem.qtyProductionAllocated, decimal0>>>>>>>>>>>>>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. on Demand")]
  public virtual Decimal? QtyDemand
  {
    get => this._QtyDemand;
    set => this._QtyDemand = value;
  }

  [PXDecimal]
  [PXDBCalced(typeof (INReplenishmentItem.QtyProcessIntFormula), typeof (Decimal))]
  public virtual Decimal? QtyProcessInt
  {
    get => this._QtyProcessInt;
    set => this._QtyProcessInt = value;
  }

  [SubItem(BqlField = typeof (InventoryItem.defaultSubItemID))]
  public virtual int? DefaultSubItemID
  {
    get => this._DefaultSubItemID;
    set => this._DefaultSubItemID = value;
  }

  public new class PK : 
    PrimaryKeyOf<INReplenishmentItem>.By<INReplenishmentItem.inventoryCD, INReplenishmentItem.siteCD, INReplenishmentItem.subItemCD>
  {
    public static INReplenishmentItem Find(
      PXGraph graph,
      string inventoryID,
      string siteID,
      string subItemCD,
      PKFindOptions options = 0)
    {
      return (INReplenishmentItem) PrimaryKeyOf<INReplenishmentItem>.By<INReplenishmentItem.inventoryCD, INReplenishmentItem.siteCD, INReplenishmentItem.subItemCD>.FindBy(graph, (object) inventoryID, (object) siteID, (object) subItemCD, options);
    }
  }

  public new static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INReplenishmentItem>.By<INReplenishmentItem.inventoryID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INReplenishmentItem>.By<INReplenishmentItem.siteID>
    {
    }

    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<INReplenishmentItem>.By<INReplenishmentItem.itemClassID>
    {
    }

    public class PreferredVendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<INReplenishmentItem>.By<INReplenishmentItem.preferredVendorID>
    {
    }
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INReplenishmentItem.inventoryID>
  {
  }

  public abstract class inventoryCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentItem.inventoryCD>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentItem.siteID>
  {
  }

  public abstract class siteCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentItem.siteCD>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentItem.subItemID>
  {
  }

  public abstract class subItemCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentItem.subItemCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentItem.descr>
  {
  }

  public new abstract class replenishmentSourceSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INReplenishmentItem.replenishmentSourceSiteID>
  {
  }

  public new abstract class preferredVendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INReplenishmentItem.preferredVendorID>
  {
  }

  public new abstract class preferredVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INReplenishmentItem.preferredVendorLocationID>
  {
  }

  public abstract class preferredVendorName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentItem.preferredVendorName>
  {
  }

  public abstract class defaultVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INReplenishmentItem.defaultVendorLocationID>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INReplenishmentItem.itemClassID>
  {
  }

  public abstract class baseUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INReplenishmentItem.baseUnit>
  {
  }

  public abstract class purchaseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentItem.purchaseUnit>
  {
  }

  public abstract class demandCalculation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentItem.demandCalculation>
  {
  }

  public abstract class vendorClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentItem.vendorClassID>
  {
  }

  public new abstract class safetyStock : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.safetyStock>
  {
  }

  public new abstract class minQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INReplenishmentItem.minQty>
  {
  }

  public new abstract class maxQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INReplenishmentItem.maxQty>
  {
  }

  public abstract class purchaseERQ : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.purchaseERQ>
  {
  }

  public new abstract class transferERQ : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.transferERQ>
  {
  }

  public new abstract class launchDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentItem.launchDate>
  {
  }

  public new abstract class terminationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INReplenishmentItem.terminationDate>
  {
  }

  public new abstract class replenishmentPolicyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentItem.replenishmentPolicyID>
  {
  }

  public new abstract class replenishmentSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INReplenishmentItem.replenishmentSource>
  {
  }

  public abstract class inclQtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtySOBackOrdered>
  {
  }

  public abstract class inclQtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtyFSSrvOrdPrepared>
  {
  }

  public abstract class inclQtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtyFSSrvOrdBooked>
  {
  }

  public abstract class inclQtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtyFSSrvOrdAllocated>
  {
  }

  public abstract class inclQtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtySOPrepared>
  {
  }

  public abstract class inclQtySOBooked : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtySOBooked>
  {
  }

  public abstract class inclQtySOShipped : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtySOShipped>
  {
  }

  public abstract class inclQtySOShipping : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtySOShipping>
  {
  }

  public abstract class inclQtyINIssues : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtyINIssues>
  {
  }

  public abstract class inclQtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtyINAssemblyDemand>
  {
  }

  public abstract class inclQtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtyProductionSupplyPrepared>
  {
  }

  public abstract class inclQtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtyProductionSupply>
  {
  }

  public abstract class inclQtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtyProductionDemandPrepared>
  {
  }

  public abstract class inclQtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtyProductionDemand>
  {
  }

  public abstract class inclQtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.inclQtyProductionAllocated>
  {
  }

  public abstract class qtyProcess : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProcess>
  {
  }

  public abstract class qtyProcessRounded : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.qtyProcessRounded>
  {
  }

  public abstract class decimalBaseUnit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INReplenishmentItem.decimalBaseUnit>
  {
  }

  public abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyOnHand>
  {
  }

  public abstract class qtyFixedSOPODiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyFixedSOPODiff>
  {
  }

  public abstract class qtyNotAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyNotAvail>
  {
  }

  public abstract class qtyPOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyPOPrepared>
  {
  }

  public abstract class qtyPOOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyPOOrders>
  {
  }

  public abstract class qtyPOReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyPOReceipts>
  {
  }

  public abstract class qtyInTransit : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyInTransit>
  {
  }

  public abstract class qtyINReceipts : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyINReceipts>
  {
  }

  public abstract class qtyINAssemblySupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyINAssemblySupply>
  {
  }

  public abstract class qtyInTransitToProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyInTransitToProduction>
  {
  }

  public abstract class qtyProductionSupplyPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProductionSupplyPrepared>
  {
  }

  public abstract class qtyProductionSupply : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProductionSupply>
  {
  }

  public abstract class qtyPOFixedProductionPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyPOFixedProductionPrepared>
  {
  }

  public abstract class qtyPOFixedProductionOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyPOFixedProductionOrders>
  {
  }

  public abstract class qtyFSSrvOrdPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyFSSrvOrdPrepared>
  {
  }

  public abstract class qtyFSSrvOrdBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyFSSrvOrdBooked>
  {
  }

  public abstract class qtyFSSrvOrdAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyFSSrvOrdAllocated>
  {
  }

  public abstract class qtySOBackOrdered : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtySOBackOrdered>
  {
  }

  public abstract class qtySOPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtySOPrepared>
  {
  }

  public abstract class qtySOBooked : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtySOBooked>
  {
  }

  public abstract class qtySOShipped : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtySOShipped>
  {
  }

  public abstract class qtySOShipping : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtySOShipping>
  {
  }

  public abstract class qtyINIssues : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyINIssues>
  {
  }

  public abstract class qtyINAssemblyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyINAssemblyDemand>
  {
  }

  public abstract class qtyProductionDemandPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProductionDemandPrepared>
  {
  }

  public abstract class qtyProductionDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProductionDemand>
  {
  }

  public abstract class qtyProductionAllocated : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProductionAllocated>
  {
  }

  public abstract class qtySOFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtySOFixedProduction>
  {
  }

  public abstract class qtyProdFixedPurchase : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProdFixedPurchase>
  {
  }

  public abstract class qtyProdFixedProduction : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProdFixedProduction>
  {
  }

  public abstract class qtyProdFixedProdOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProdFixedProdOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedProdOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProdFixedProdOrders>
  {
  }

  public abstract class qtyProdFixedSalesOrdersPrepared : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProdFixedSalesOrdersPrepared>
  {
  }

  public abstract class qtyProdFixedSalesOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProdFixedSalesOrders>
  {
  }

  public abstract class qtyINReplaned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyINReplaned>
  {
  }

  public abstract class qtyReplenishment : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyReplenishment>
  {
  }

  public abstract class qtyHardDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyHardDemand>
  {
  }

  public abstract class qtyDemand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyDemand>
  {
  }

  public class effectiveOnHand : IBqlCreator, IBqlVerifier, IBqlOperand
  {
    private readonly IBqlCreator _formula = (IBqlCreator) new Add<Sub<IsNull<INReplenishmentItem.qtyOnHand, decimal0>, IsNull<INReplenishmentItem.qtyNotAvail, decimal0>>, Add<IsNull<INReplenishmentItem.qtyReplenishment, decimal0>, Sub<IsNull<INReplenishmentItem.qtyINReplaned, decimal0>, Switch<Case<Where<INReplenishmentItem.demandCalculation, Equal<INDemandCalculation.hardDemand>>, IsNull<INReplenishmentItem.qtyHardDemand, decimal0>>, IsNull<INReplenishmentItem.qtyDemand, decimal0>>>>>();

    public bool AppendExpression(
      ref SQLExpression exp,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      return this._formula.AppendExpression(ref exp, graph, info, selection);
    }

    public void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
      ((IBqlVerifier) this._formula).Verify(cache, item, pars, ref result, ref value);
    }
  }

  public class QtyProcessIntFormula : IBqlCreator, IBqlVerifier, IBqlOperand
  {
    private readonly IBqlCreator _formula = (IBqlCreator) new Switch<Case<Where<INReplenishmentItem.effectiveOnHand, Greater<IsNull<INReplenishmentItem.minQty, decimal0>>, Or<PX.Objects.IN.S.INItemSite.replenishmentMethod, IsNull, Or<PX.Objects.IN.S.INItemSite.replenishmentMethod, Equal<INReplenishmentMethod.none>>>>, decimal0, Case<Where<PX.Objects.IN.S.INItemSite.replenishmentMethod, Equal<INReplenishmentMethod.minMax>>, Sub<IsNull<INReplenishmentItem.maxQty, decimal0>, INReplenishmentItem.effectiveOnHand>, Case<Where<PX.Objects.IN.S.INItemSite.replenishmentMethod, NotEqual<INReplenishmentMethod.fixedReorder>>, decimal0, Case<Where<INReplenishmentItem.replenishmentSource, Equal<INReplenishmentSource.transfer>>, IsNull<INReplenishmentItem.transferERQ, decimal0>, Case<Where<INReplenishmentItem.replenishmentSource, Equal<INReplenishmentSource.purchased>>, IsNull<INReplenishmentItem.purchaseERQ, decimal0>>>>>>, decimal0>();

    public bool AppendExpression(
      ref SQLExpression exp,
      PXGraph graph,
      BqlCommandInfo info,
      BqlCommand.Selection selection)
    {
      return this._formula.AppendExpression(ref exp, graph, info, selection);
    }

    public void Verify(
      PXCache cache,
      object item,
      List<object> pars,
      ref bool? result,
      ref object value)
    {
      ((IBqlVerifier) this._formula).Verify(cache, item, pars, ref result, ref value);
    }
  }

  public abstract class qtyProcessInt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INReplenishmentItem.qtyProcessInt>
  {
  }

  public abstract class defaultSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INReplenishmentItem.defaultSubItemID>
  {
  }
}
