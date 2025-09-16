// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemSite
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.TM;
using System;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.IN;

[PXPrimaryGraph(typeof (INItemSiteMaint))]
[PXCacheName]
[PXProjection(typeof (Select2<INItemSite, InnerJoin<INSite, On<INItemSite.FK.Site>, LeftJoin<INItemStats, On<INItemStats.FK.ItemSite>>>>), new System.Type[] {typeof (INItemSite)})]
[PXGroupMask(typeof (InnerJoin<InventoryItem, On<InventoryItem.inventoryID, Equal<INItemSite.inventoryID>, And<Match<InventoryItem, Current<AccessInfo.userName>>>>, InnerJoin<INSite, On<INSite.siteID, Equal<INItemSite.siteID>, And<Match<INSite, Current<AccessInfo.userName>>>>>>))]
[Serializable]
public class INItemSite : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _InventoryID;
  protected int? _SiteID;
  protected 
  #nullable disable
  string _SiteStatus;
  protected bool? _OverrideInvtAcctSub;
  protected int? _InvtAcctID;
  protected int? _InvtSubID;
  protected string _ValMethod;
  protected int? _DfltShipLocationID;
  protected int? _DfltReceiptLocationID;
  protected string _DfltSalesUnit;
  protected string _DfltPurchaseUnit;
  protected Decimal? _LastStdCost;
  protected Decimal? _PendingStdCost;
  protected DateTime? _PendingStdCostDate;
  protected Decimal? _StdCost;
  protected DateTime? _StdCostDate;
  protected Decimal? _LastBasePrice;
  protected Decimal? _PendingBasePrice;
  protected DateTime? _PendingBasePriceDate;
  protected Decimal? _BasePrice;
  protected DateTime? _BasePriceDate;
  protected DateTime? _LastCostDate;
  protected Decimal? _LastCost;
  protected Decimal? _AvgCost;
  protected Decimal? _MinCost;
  protected Decimal? _MaxCost;
  protected Decimal? _TranUnitCost;
  protected bool? _PreferredVendorOverride;
  protected int? _PreferredVendorID;
  protected int? _PreferredVendorLocationID;
  protected int? _ProductWorkgroupID;
  protected int? _ProductManagerID;
  protected int? _PriceWorkgroupID;
  protected int? _PriceManagerID;
  protected bool? _IsDefault;
  protected bool? _StdCostOverride;
  protected bool? _BasePriceOverride;
  protected bool? _Commissionable;
  protected bool? _ABCCodeOverride;
  protected string _ABCCodeID;
  protected bool? _ABCCodeIsFixed;
  protected bool? _MovementClassOverride;
  protected string _MovementClassID;
  protected bool? _MovementClassIsFixed;
  protected Guid? _NoteID;
  protected bool? _POCreate;
  protected string _POSource;
  protected Decimal? _MarkupPct;
  protected bool? _MarkupPctOverride;
  protected Decimal? _RecPrice;
  protected bool? _RecPriceOverride;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected string _ReplenishmentClassID;
  protected bool? _ReplenishmentPolicyOverride;
  protected string _ReplenishmentPolicyID;
  protected string _ReplenishmentSource;
  protected string _ReplenishmentMethod;
  protected int? _ReplenishmentSourceSiteID;
  protected bool? _MaxShelfLifeOverride;
  protected int? _MaxShelfLife;
  protected bool? _LaunchDateOverride;
  protected DateTime? _LaunchDate;
  protected bool? _TerminationDateOverride;
  protected DateTime? _TerminationDate;
  protected bool? _ServiceLevelOverride;
  protected Decimal? _ServiceLevel;
  protected bool? _SafetyStockOverride;
  protected Decimal? _SafetyStock;
  protected bool? _MinQtyOverride;
  protected Decimal? _MinQty;
  protected bool? _MaxQtyOverride;
  protected Decimal? _MaxQty;
  protected bool? _TransferERQOverride;
  protected Decimal? _TransferERQ;
  protected bool? _SubItemOverride;
  protected Decimal? _SafetyStockSuggested;
  protected Decimal? _MinQtySuggested;
  protected Decimal? _MaxQtySuggested;
  protected Decimal? _ESSmoothingConstantL;
  protected bool? _ESSmoothingConstantLOverride;
  protected Decimal? _ESSmoothingConstantT;
  protected bool? _ESSmoothingConstantTOverride;
  protected Decimal? _ESSmoothingConstantS;
  protected bool? _ESSmoothingConstantSOverride;
  protected bool? _AutoFitModel;
  protected Decimal? _DemandPerDayAverage;
  protected Decimal? _DemandPerDayMSE;
  protected Decimal? _DemandPerDayMAD;
  protected Decimal? _LeadTimeAverage;
  protected Decimal? _LeadTimeMSE;
  protected DateTime? _LastForecastDate;
  protected string _ForecastModelType;
  protected string _ForecastPeriodType;
  protected DateTime? _LastFCApplicationDate;
  protected byte[] _tstamp;

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [StockItem(IsKey = true, DirtyRead = true, DisplayName = "Inventory ID", TabOrder = 1)]
  [PXParent(typeof (INItemSite.FK.InventoryItem))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [Site(IsKey = true, TabOrder = 2)]
  [PXDefault]
  [PXParent(typeof (INItemSite.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBBool(BqlField = typeof (INSite.active))]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("AC")]
  [PXUIField]
  [PXStringList(new string[] {"AC", "IN"}, new string[] {"Active", "Inactive"})]
  public virtual string SiteStatus
  {
    get => this._SiteStatus;
    set => this._SiteStatus = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Inventory Account/Sub.")]
  public virtual bool? OverrideInvtAcctSub
  {
    get => this._OverrideInvtAcctSub;
    set => this._OverrideInvtAcctSub = value;
  }

  [Account]
  [PXUIRequired(typeof (Where<INItemSite.overrideInvtAcctSub, Equal<True>>))]
  [PXUIEnabled(typeof (Where<INItemSite.overrideInvtAcctSub, Equal<True>>))]
  [PXDefault]
  [PXForeignReference(typeof (INItemSite.FK.InventoryAccount))]
  public virtual int? InvtAcctID
  {
    get => this._InvtAcctID;
    set => this._InvtAcctID = value;
  }

  [SubAccount(typeof (INItemSite.invtAcctID))]
  [PXUIRequired(typeof (Where<INItemSite.overrideInvtAcctSub, Equal<True>>))]
  [PXUIEnabled(typeof (Where<INItemSite.overrideInvtAcctSub, Equal<True>>))]
  [PXDefault]
  [PXForeignReference(typeof (INItemSite.FK.InventorySubaccount))]
  public virtual int? InvtSubID
  {
    get => this._InvtSubID;
    set => this._InvtSubID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (Search<InventoryItem.valMethod, Where<InventoryItem.inventoryID, Equal<Current<INItemSite.inventoryID>>>>))]
  public virtual string ValMethod
  {
    get => this._ValMethod;
    set => this._ValMethod = value;
  }

  [PXRestrictor(typeof (Where<INLocation.active, Equal<True>>), "Location is not Active.", new System.Type[] {})]
  [Location(typeof (INItemSite.siteID), DisplayName = "Default Issue From", DescriptionField = typeof (INLocation.descr))]
  [PXForeignReference(typeof (INItemSite.FK.DefaultShipLocation))]
  public virtual int? DfltShipLocationID
  {
    get => this._DfltShipLocationID;
    set => this._DfltShipLocationID = value;
  }

  [PXRestrictor(typeof (Where<INLocation.active, Equal<True>>), "Location is not Active.", new System.Type[] {})]
  [Location(typeof (INItemSite.siteID), DisplayName = "Default Receipt To", DescriptionField = typeof (INLocation.descr))]
  [PXForeignReference(typeof (INItemSite.FK.DefaultReceiptLocation))]
  public virtual int? DfltReceiptLocationID
  {
    get => this._DfltReceiptLocationID;
    set => this._DfltReceiptLocationID = value;
  }

  /// <summary>
  /// The location of the warehouse to be used by default to put away the selected stock item.
  /// </summary>
  [PXRestrictor(typeof (Where<INLocation.active, Equal<True>>), "Location is not Active.", new System.Type[] {})]
  [Location(typeof (INItemSite.siteID), DisplayName = "Default Putaway To", DescriptionField = typeof (INLocation.descr))]
  [PXForeignReference(typeof (INItemSite.FK.DefaultPutawayLocation))]
  public virtual int? DfltPutawayLocationID { get; set; }

  [INUnit(null, typeof (InventoryItem.baseUnit), DisplayName = "Sales Unit")]
  [PXDefault(typeof (Search<InventoryItem.salesUnit, Where<InventoryItem.inventoryID, Equal<Current<INItemSite.inventoryID>>>>))]
  public virtual string DfltSalesUnit
  {
    get => this._DfltSalesUnit;
    set => this._DfltSalesUnit = value;
  }

  [INUnit(null, typeof (InventoryItem.baseUnit), DisplayName = "Purchase Unit")]
  [PXDefault(typeof (Search<InventoryItem.purchaseUnit, Where<InventoryItem.inventoryID, Equal<Current<INItemSite.inventoryID>>>>))]
  public virtual string DfltPurchaseUnit
  {
    get => this._DfltPurchaseUnit;
    set => this._DfltPurchaseUnit = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Cost", Enabled = false)]
  [CurySymbol(null, null, null, null, typeof (INItemSite.siteID), null, null, true, true)]
  public virtual Decimal? LastStdCost
  {
    get => this._LastStdCost;
    set => this._LastStdCost = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Cost")]
  [CurySymbol(null, null, null, null, typeof (INItemSite.siteID), null, null, true, true)]
  public virtual Decimal? PendingStdCost
  {
    get => this._PendingStdCost;
    set => this._PendingStdCost = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Pending Cost Date")]
  public virtual DateTime? PendingStdCostDate
  {
    get => this._PendingStdCostDate;
    set => this._PendingStdCostDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? PendingStdCostReset { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Current Cost", Enabled = false)]
  [CurySymbol(null, null, null, null, typeof (INItemSite.siteID), null, null, true, true)]
  public virtual Decimal? StdCost
  {
    get => this._StdCost;
    set => this._StdCost = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Effective Date", Enabled = false)]
  [PXFormula(typeof (Switch<Case<Where<INItemSite.lastStdCost, NotEqual<decimal0>>, Current<AccessInfo.businessDate>>, INItemSite.stdCostDate>))]
  public virtual DateTime? StdCostDate
  {
    get => this._StdCostDate;
    set => this._StdCostDate = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Price", Enabled = false)]
  public virtual Decimal? LastBasePrice
  {
    get => this._LastBasePrice;
    set => this._LastBasePrice = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Pending Price")]
  public virtual Decimal? PendingBasePrice
  {
    get => this._PendingBasePrice;
    set => this._PendingBasePrice = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Pending Price Date", Enabled = false)]
  public virtual DateTime? PendingBasePriceDate
  {
    get => this._PendingBasePriceDate;
    set => this._PendingBasePriceDate = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Current Price", Enabled = false)]
  public virtual Decimal? BasePrice
  {
    get => this._BasePrice;
    set => this._BasePrice = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Effective Date", Enabled = false)]
  public virtual DateTime? BasePriceDate
  {
    get => this._BasePriceDate;
    set => this._BasePriceDate = value;
  }

  [PXDate]
  public virtual DateTime? LastCostDate
  {
    get => this._LastCostDate;
    set => this._LastCostDate = value;
  }

  [PXDBPriceCost(BqlField = typeof (INItemStats.lastCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Cost", Enabled = true)]
  [CurySymbol(null, null, null, null, typeof (INItemSite.siteID), null, null, true, true)]
  public virtual Decimal? LastCost
  {
    get => this._LastCost;
    set => this._LastCost = value;
  }

  [PXPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Average Cost", Enabled = false)]
  [PXDBPriceCostCalced(typeof (Switch<Case<Where<INItemStats.qtyOnHand, Equal<decimal0>>, decimal0>, Div<INItemStats.totalCost, INItemStats.qtyOnHand>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  [CurySymbol(null, null, null, null, typeof (INItemSite.siteID), null, null, true, true)]
  public virtual Decimal? AvgCost
  {
    get => this._AvgCost;
    set => this._AvgCost = value;
  }

  [PXDBPriceCost(BqlField = typeof (INItemStats.minCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Min. Cost", Enabled = false)]
  [CurySymbol(null, null, null, null, typeof (INItemSite.siteID), null, null, true, true)]
  public virtual Decimal? MinCost
  {
    get => this._MinCost;
    set => this._MinCost = value;
  }

  [PXDBPriceCost(BqlField = typeof (INItemStats.maxCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Max. Cost", Enabled = false)]
  [CurySymbol(null, null, null, null, typeof (INItemSite.siteID), null, null, true, true)]
  public virtual Decimal? MaxCost
  {
    get => this._MaxCost;
    set => this._MaxCost = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<INItemSite.valMethod, Equal<INValMethod.standard>>, INItemSite.stdCost, Case<Where2<Where<INItemSite.valMethod, Equal<INValMethod.average>, And<INSite.avgDefaultCost, Equal<INSite.avgDefaultCost.averageCost>, Or<INItemSite.valMethod, Equal<INValMethod.fIFO>, And<INSite.fIFODefaultCost, Equal<INSite.avgDefaultCost.averageCost>>>>>, And<INItemStats.qtyOnHand, NotEqual<decimal0>, And<Div<INItemStats.totalCost, INItemStats.qtyOnHand>, GreaterEqual<decimal0>>>>, Div<INItemStats.totalCost, INItemStats.qtyOnHand>, Case<Where<INItemStats.lastCostDate, GreaterEqual<INItemStats.dateAfterMinDate>>, INItemStats.lastCost>>>, Null>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  public virtual Decimal? TranUnitCost
  {
    get => this._TranUnitCost;
    set => this._TranUnitCost = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Preferred Vendor")]
  public virtual bool? PreferredVendorOverride
  {
    get => this._PreferredVendorOverride;
    set => this._PreferredVendorOverride = value;
  }

  [VendorNonEmployeeActiveOrHoldPayments(DisplayName = "Preferred Vendor", Required = false, DescriptionField = typeof (PX.Objects.AP.Vendor.acctName))]
  [PXForeignReference(typeof (INItemSite.FK.PreferredVendor))]
  public virtual int? PreferredVendorID
  {
    get => this._PreferredVendorID;
    set => this._PreferredVendorID = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<INItemSite.preferredVendorID>>>))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.defLocationID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<INItemSite.preferredVendorID>>>>))]
  [PXFormula(typeof (Default<INItemSite.preferredVendorID>))]
  public virtual int? PreferredVendorLocationID
  {
    get => this._PreferredVendorLocationID;
    set => this._PreferredVendorLocationID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Product Manager")]
  public virtual bool? ProductManagerOverride { get; set; }

  [PXDBInt]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Product Workgroup")]
  [PXDefault(typeof (Parent<InventoryItem.productWorkgroupID>))]
  [PXUIEnabled(typeof (Where<INItemSite.productManagerOverride, Equal<True>>))]
  public virtual int? ProductWorkgroupID
  {
    get => this._ProductWorkgroupID;
    set => this._ProductWorkgroupID = value;
  }

  [Owner(typeof (INItemSite.productWorkgroupID), DisplayName = "Product Manager")]
  [PXDefault(typeof (Parent<InventoryItem.productManagerID>))]
  [PXUIEnabled(typeof (Where<INItemSite.productManagerOverride, Equal<True>>))]
  public virtual int? ProductManagerID
  {
    get => this._ProductManagerID;
    set => this._ProductManagerID = value;
  }

  [PXDBInt]
  [PXCompanyTreeSelector]
  [PXUIField(DisplayName = "Price Workgroup")]
  public virtual int? PriceWorkgroupID
  {
    get => this._PriceWorkgroupID;
    set => this._PriceWorkgroupID = value;
  }

  [Owner(typeof (INItemSite.priceWorkgroupID), DisplayName = "Price Manager")]
  public virtual int? PriceManagerID
  {
    get => this._PriceManagerID;
    set => this._PriceManagerID = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Default")]
  public virtual bool? IsDefault
  {
    get => this._IsDefault;
    set => this._IsDefault = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Std. Cost")]
  public virtual bool? StdCostOverride
  {
    get => this._StdCostOverride;
    set => this._StdCostOverride = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Price Override")]
  public virtual bool? BasePriceOverride
  {
    get => this._BasePriceOverride;
    set => this._BasePriceOverride = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Commissionable
  {
    get => this._Commissionable;
    set => this._Commissionable = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "ABC Code Override")]
  public virtual bool? ABCCodeOverride
  {
    get => this._ABCCodeOverride;
    set => this._ABCCodeOverride = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField]
  [PXSelector(typeof (INABCCode.aBCCodeID), DescriptionField = typeof (INABCCode.descr))]
  public virtual string ABCCodeID
  {
    get => this._ABCCodeID;
    set => this._ABCCodeID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Fixed ABC Code")]
  public virtual bool? ABCCodeIsFixed
  {
    get => this._ABCCodeIsFixed;
    set => this._ABCCodeIsFixed = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Movement Class Override")]
  public virtual bool? MovementClassOverride
  {
    get => this._MovementClassOverride;
    set => this._MovementClassOverride = value;
  }

  [PXDBString(1)]
  [PXUIField]
  [PXSelector(typeof (INMovementClass.movementClassID), DescriptionField = typeof (INMovementClass.descr))]
  public virtual string MovementClassID
  {
    get => this._MovementClassID;
    set => this._MovementClassID = value;
  }

  [PXDBString(1)]
  public virtual string PendingMovementClassID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Fixed Movement Class")]
  public virtual bool? MovementClassIsFixed
  {
    get => this._MovementClassIsFixed;
    set => this._MovementClassIsFixed = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public virtual string PendingMovementClassPeriodID { get; set; }

  [PXDBDate]
  public virtual DateTime? PendingMovementClassUpdateDate { get; set; }

  [PXNote(DescriptionField = typeof (INItemSite.siteID), Selector = typeof (INItemSite.siteID))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<INItemSite.replenishmentSource, Equal<INReplenishmentSource.purchaseToOrder>, Or<INItemSite.replenishmentSource, Equal<INReplenishmentSource.dropShipToOrder>>>, boolTrue>, boolFalse>), typeof (bool))]
  [PXUIField(DisplayName = "Mark fo PO", Enabled = false)]
  public virtual bool? POCreate
  {
    get => this._POCreate;
    set => this._POCreate = new bool?(value.GetValueOrDefault());
  }

  [PXDBCalced(typeof (Switch<Case<Where<INItemSite.replenishmentSource, Equal<INReplenishmentSource.dropShipToOrder>>, INReplenishmentSource.dropShipToOrder, Case<Where<INItemSite.replenishmentSource, Equal<INReplenishmentSource.purchaseToOrder>>, INReplenishmentSource.purchaseToOrder>>, INReplenishmentSource.none>), typeof (string))]
  public virtual string POSource
  {
    get => this._POSource;
    set => this._POSource = value ?? "N";
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Markup %", Enabled = false)]
  public virtual Decimal? MarkupPct
  {
    get => this._MarkupPct;
    set => this._MarkupPct = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Markup %")]
  public virtual bool? MarkupPctOverride
  {
    get => this._MarkupPctOverride;
    set => this._MarkupPctOverride = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "MSRP", Enabled = false)]
  [CurySymbol(null, null, null, null, typeof (INItemSite.siteID), null, null, true, true)]
  public virtual Decimal? RecPrice
  {
    get => this._RecPrice;
    set => this._RecPrice = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Price")]
  public virtual bool? RecPriceOverride
  {
    get => this._RecPriceOverride;
    set => this._RecPriceOverride = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField]
  [PXSelector(typeof (Search<INReplenishmentClass.replenishmentClassID>), DescriptionField = typeof (INReplenishmentClass.descr))]
  public virtual string ReplenishmentClassID
  {
    get => this._ReplenishmentClassID;
    set => this._ReplenishmentClassID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Replenishment Settings")]
  public virtual bool? ReplenishmentPolicyOverride
  {
    get => this._ReplenishmentPolicyOverride;
    set => this._ReplenishmentPolicyOverride = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Seasonality")]
  [PXSelector(typeof (Search<INReplenishmentPolicy.replenishmentPolicyID>), DescriptionField = typeof (INReplenishmentPolicy.descr))]
  public virtual string ReplenishmentPolicyID
  {
    get => this._ReplenishmentPolicyID;
    set => this._ReplenishmentPolicyID = value;
  }

  /// <summary>Replenishment source</summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Replenishment Source", FieldClass = "InvPlanning")]
  [INReplenishmentSource.List]
  public virtual string ReplenishmentSource
  {
    get => this._ReplenishmentSource;
    set => this._ReplenishmentSource = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Replenishment Method", Enabled = false)]
  [INReplenishmentMethod.List]
  public virtual string ReplenishmentMethod
  {
    get => this._ReplenishmentMethod;
    set => this._ReplenishmentMethod = value;
  }

  [Site(DisplayName = "Replenishment Warehouse", DescriptionField = typeof (INSite.descr))]
  [PXForeignReference(typeof (INItemSite.FK.ReplenishmentSourceSite))]
  public virtual int? ReplenishmentSourceSiteID
  {
    get => this._ReplenishmentSourceSiteID;
    set => this._ReplenishmentSourceSiteID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? MaxShelfLifeOverride
  {
    get => this._MaxShelfLifeOverride;
    set => this._MaxShelfLifeOverride = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Max. Shelf Life (Days)")]
  public virtual int? MaxShelfLife
  {
    get => this._MaxShelfLife;
    set => this._MaxShelfLife = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? LaunchDateOverride
  {
    get => this._LaunchDateOverride;
    set => this._LaunchDateOverride = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Launch Date")]
  public virtual DateTime? LaunchDate
  {
    get => this._LaunchDate;
    set => this._LaunchDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? TerminationDateOverride
  {
    get => this._TerminationDateOverride;
    set => this._TerminationDateOverride = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Termination Date")]
  public virtual DateTime? TerminationDate
  {
    get => this._TerminationDate;
    set => this._TerminationDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? ServiceLevelOverride
  {
    get => this._ServiceLevelOverride;
    set => this._ServiceLevelOverride = value;
  }

  [PXDBDecimal(6, MinValue = 0.500001, MaxValue = 0.999999)]
  [PXUIField(DisplayName = "Service Level", Visible = true)]
  public virtual Decimal? ServiceLevel
  {
    get => this._ServiceLevel;
    set => this._ServiceLevel = value;
  }

  [PXDecimal(4, MinValue = 50.0001, MaxValue = 99.9999)]
  [PXUIField(DisplayName = "Service Level (%)", Visible = true)]
  public virtual Decimal? ServiceLevelPct
  {
    [PXDependsOnFields(new System.Type[] {typeof (INItemSite.serviceLevel)})] get
    {
      Decimal? serviceLevel = this._ServiceLevel;
      Decimal num = 100.0M;
      return !serviceLevel.HasValue ? new Decimal?() : new Decimal?(serviceLevel.GetValueOrDefault() * num);
    }
    set
    {
      Decimal? nullable = value;
      Decimal num = 100.0M;
      this._ServiceLevel = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() / num) : new Decimal?();
    }
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? SafetyStockOverride
  {
    get => this._SafetyStockOverride;
    set => this._SafetyStockOverride = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Safety Stock")]
  public virtual Decimal? SafetyStock
  {
    get => this._SafetyStock;
    set => this._SafetyStock = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? MinQtyOverride
  {
    get => this._MinQtyOverride;
    set => this._MinQtyOverride = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Reorder Point")]
  public virtual Decimal? MinQty
  {
    get => this._MinQty;
    set => this._MinQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? MaxQtyOverride
  {
    get => this._MaxQtyOverride;
    set => this._MaxQtyOverride = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Max Qty.")]
  public virtual Decimal? MaxQty
  {
    get => this._MaxQty;
    set => this._MaxQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? TransferERQOverride
  {
    get => this._TransferERQOverride;
    set => this._TransferERQOverride = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Transfer ERQ")]
  public virtual Decimal? TransferERQ
  {
    get => this._TransferERQ;
    set => this._TransferERQ = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override", FieldClass = "INSUBITEM")]
  public virtual bool? SubItemOverride
  {
    get => this._SubItemOverride;
    set => this._SubItemOverride = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Safety Stock Suggested", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? SafetyStockSuggested
  {
    get => this._SafetyStockSuggested;
    set => this._SafetyStockSuggested = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Reorder Point Suggested", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? MinQtySuggested
  {
    get => this._MinQtySuggested;
    set => this._MinQtySuggested = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Max Qty Suggested")]
  public virtual Decimal? MaxQtySuggested
  {
    get => this._MaxQtySuggested;
    set => this._MaxQtySuggested = value;
  }

  [PXDBDecimal(9, MinValue = 0.0, MaxValue = 1.0)]
  [PXUIField(DisplayName = "Level Smoothing Constant")]
  public virtual Decimal? ESSmoothingConstantL
  {
    get => this._ESSmoothingConstantL;
    set => this._ESSmoothingConstantL = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? ESSmoothingConstantLOverride
  {
    get => this._ESSmoothingConstantLOverride;
    set => this._ESSmoothingConstantLOverride = value;
  }

  [PXDBDecimal(9)]
  [PXUIField(DisplayName = "Trend Smoothing Constant")]
  public virtual Decimal? ESSmoothingConstantT
  {
    get => this._ESSmoothingConstantT;
    set => this._ESSmoothingConstantT = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? ESSmoothingConstantTOverride
  {
    get => this._ESSmoothingConstantTOverride;
    set => this._ESSmoothingConstantTOverride = value;
  }

  [PXDBDecimal(9)]
  [PXUIField(DisplayName = "Seasonality Smoothing Constant")]
  public virtual Decimal? ESSmoothingConstantS
  {
    get => this._ESSmoothingConstantS;
    set => this._ESSmoothingConstantS = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override")]
  public virtual bool? ESSmoothingConstantSOverride
  {
    get => this._ESSmoothingConstantSOverride;
    set => this._ESSmoothingConstantSOverride = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? AutoFitModel
  {
    get => this._AutoFitModel;
    set => this._AutoFitModel = value;
  }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Daily Demand Forecast", Enabled = false)]
  public virtual Decimal? DemandPerDayAverage
  {
    get => this._DemandPerDayAverage;
    set => this._DemandPerDayAverage = value;
  }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Daily Demand Forecast Error(MSE)", Enabled = false)]
  public virtual Decimal? DemandPerDayMSE
  {
    get => this._DemandPerDayMSE;
    set => this._DemandPerDayMSE = value;
  }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Daily Forecast Error(MAD)", Enabled = false)]
  public virtual Decimal? DemandPerDayMAD
  {
    get => this._DemandPerDayMAD;
    set => this._DemandPerDayMAD = value;
  }

  [PXDecimal(6)]
  [PXUIField(DisplayName = "Daily Demand Forecast Error(STDEV)", Enabled = false)]
  public virtual Decimal? DemandPerDaySTDEV
  {
    [PXDependsOnFields(new System.Type[] {typeof (INItemSite.demandPerDayMSE)})] get
    {
      return !this._DemandPerDayMSE.HasValue ? this._DemandPerDayMSE : new Decimal?((Decimal) Math.Sqrt((double) this._DemandPerDayMSE.Value));
    }
    set
    {
    }
  }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Lead Time Average")]
  public virtual Decimal? LeadTimeAverage
  {
    get => this._LeadTimeAverage;
    set => this._LeadTimeAverage = value;
  }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Lead Time Deviation")]
  public virtual Decimal? LeadTimeMSE
  {
    get => this._LeadTimeMSE;
    set => this._LeadTimeMSE = value;
  }

  [PXDecimal(6)]
  [PXUIField(DisplayName = "Lead Time STDEV")]
  public virtual Decimal? LeadTimeSTDEV
  {
    [PXDependsOnFields(new System.Type[] {typeof (INItemSite.leadTimeMSE)})] get
    {
      return !this._LeadTimeMSE.HasValue ? this._LeadTimeMSE : new Decimal?((Decimal) Math.Sqrt((double) this._LeadTimeMSE.Value));
    }
    set
    {
    }
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Forecast Date", Enabled = false)]
  public virtual DateTime? LastForecastDate
  {
    get => this._LastForecastDate;
    set => this._LastForecastDate = value;
  }

  [PXDBString(3, IsFixed = true)]
  [DemandForecastModelType.List]
  [PXUIField(DisplayName = "Demand Forecast Model Used")]
  [PXFormula(typeof (Default<INItemRep.forecastModelType>))]
  public virtual string ForecastModelType
  {
    get => this._ForecastModelType;
    set => this._ForecastModelType = value;
  }

  [PXDBString(2, IsFixed = true)]
  [DemandPeriodType.List]
  [PXUIField(DisplayName = "Forecast Period Type Used")]
  public virtual string ForecastPeriodType
  {
    get => this._ForecastPeriodType;
    set => this._ForecastPeriodType = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Forecast Results Application Date", Enabled = false)]
  public virtual DateTime? LastFCApplicationDate
  {
    get => this._LastFCApplicationDate;
    set => this._LastFCApplicationDate = value;
  }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Country Of Origin")]
  [Country]
  [PXDefault(typeof (Search<InventoryItem.countryOfOrigin, Where<InventoryItem.inventoryID, Equal<Current<INItemSite.inventoryID>>>>))]
  [PXFormula(typeof (Default<InventoryItem.inventoryID>))]
  public virtual string CountryOfOrigin { get; set; }

  /// <summary>Planning method</summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Planning Method", FieldClass = "InvPlanning")]
  [PXDefault(typeof (Search<InventoryItem.planningMethod, Where<InventoryItem.inventoryID, Equal<Current<INItemSite.inventoryID>>>>))]
  [INPlanningMethod.List]
  public string PlanningMethod { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<INItemSite>.By<INItemSite.inventoryID, INItemSite.siteID>
  {
    public static INItemSite Find(
      PXGraph graph,
      int? inventoryID,
      int? siteID,
      PKFindOptions options = 0)
    {
      return (INItemSite) PrimaryKeyOf<INItemSite>.By<INItemSite.inventoryID, INItemSite.siteID>.FindBy(graph, (object) inventoryID, (object) siteID, options);
    }
  }

  public static class FK
  {
    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INItemSite>.By<INItemSite.inventoryID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemSite>.By<INItemSite.siteID>
    {
    }

    public class InventoryAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INItemSite>.By<INItemSite.invtAcctID>
    {
    }

    public class InventorySubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INItemSite>.By<INItemSite.invtSubID>
    {
    }

    public class ReplenishmentSourceSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INItemSite>.By<INItemSite.replenishmentSourceSiteID>
    {
    }

    public class DefaultShipLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INItemSite>.By<INItemSite.dfltShipLocationID>
    {
    }

    public class DefaultReceiptLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INItemSite>.By<INItemSite.dfltReceiptLocationID>
    {
    }

    public class DefaultPutawayLocation : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INItemSite>.By<INItemSite.dfltPutawayLocationID>
    {
    }

    public class PreferredVendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<INItemSite>.By<INItemSite.preferredVendorID>
    {
    }

    public class PreferredVendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<INItemSite>.By<INItemSite.preferredVendorID, INItemSite.preferredVendorLocationID>
    {
    }

    public class ABCCode : 
      PrimaryKeyOf<INABCCode>.By<INABCCode.aBCCodeID>.ForeignKeyOf<INItemSite>.By<INItemSite.aBCCodeID>
    {
    }

    public class MovementClass : 
      PrimaryKeyOf<INMovementClass>.By<INMovementClass.movementClassID>.ForeignKeyOf<INItemSite>.By<INItemSite.movementClassID>
    {
    }

    public class PendingMovementClass : 
      PrimaryKeyOf<INMovementClass>.By<INMovementClass.movementClassID>.ForeignKeyOf<INItemSite>.By<INItemSite.pendingMovementClassID>
    {
    }

    public class ReplenishmentClass : 
      PrimaryKeyOf<INReplenishmentClass>.By<INReplenishmentClass.replenishmentClassID>.ForeignKeyOf<INItemSite>.By<INItemSite.replenishmentClassID>
    {
    }

    public class ReplenishmentPolicy : 
      PrimaryKeyOf<INReplenishmentPolicy>.By<INReplenishmentPolicy.replenishmentPolicyID>.ForeignKeyOf<INItemSite>.By<INItemSite.replenishmentPolicyID>
    {
    }

    public class ProductWorkgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<INItemSite>.By<INItemSite.productWorkgroupID>
    {
    }

    public class ProductManager : 
      PrimaryKeyOf<PX.Objects.CR.Standalone.EPEmployee>.By<PX.Objects.CR.Standalone.EPEmployee.bAccountID>.ForeignKeyOf<INItemSite>.By<INItemSite.productManagerID>
    {
    }

    public class PriceWorkgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<INItemSite>.By<INItemSite.priceWorkgroupID>
    {
    }

    public class PriceManager : 
      PrimaryKeyOf<PX.Objects.CR.Standalone.EPEmployee>.By<PX.Objects.CR.Standalone.EPEmployee.bAccountID>.ForeignKeyOf<INItemSite>.By<INItemSite.priceManagerID>
    {
    }

    public class CountryOfOrigin : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<INItemSite>.By<INItemSite.countryOfOrigin>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.selected>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.siteID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.active>
  {
  }

  public abstract class siteStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSite.siteStatus>
  {
  }

  public abstract class overrideInvtAcctSub : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.overrideInvtAcctSub>
  {
  }

  public abstract class invtAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.invtAcctID>
  {
    /// <exclude />
    public class WarnIfNonZeroCostLayerExists : 
      WarnIfNonZeroCostLayerExists<INItemSite.invtAcctID, INItemSiteMaint>
    {
      public static bool IsActive() => true;

      protected override bool FilterBySiteID => true;

      protected override void OnPreventEdit(GetEditPreventingReasonArgs e)
      {
        if (e.NewValue == null)
          ((CancelEventArgs) e).Cancel = true;
        base.OnPreventEdit(e);
      }

      protected override bool AccountIDChanged(
        GetEditPreventingReasonArgs e,
        out int? originalAccountID,
        out int? newAccountID)
      {
        INItemSite row = (INItemSite) e.Row;
        bool? valueOriginal = (bool?) e.Cache.GetValueOriginal<INItemSite.overrideInvtAcctSub>((object) row);
        bool? nullable1 = valueOriginal;
        bool flag1 = false;
        bool? nullable2;
        if (nullable1.GetValueOrDefault() == flag1 & nullable1.HasValue)
        {
          nullable2 = row.OverrideInvtAcctSub;
          bool flag2 = false;
          if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
          {
            ref int? local1 = ref originalAccountID;
            ref int? local2 = ref newAccountID;
            int? nullable3 = new int?();
            int? nullable4;
            int? nullable5 = nullable4 = nullable3;
            local2 = nullable4;
            int? nullable6 = nullable5;
            local1 = nullable6;
            return false;
          }
        }
        nullable2 = valueOriginal;
        bool? overrideInvtAcctSub = row.OverrideInvtAcctSub;
        if (nullable2.GetValueOrDefault() == overrideInvtAcctSub.GetValueOrDefault() & nullable2.HasValue == overrideInvtAcctSub.HasValue)
          return base.AccountIDChanged(e, out originalAccountID, out newAccountID);
        ref int? local3 = ref originalAccountID;
        bool? nullable7 = valueOriginal;
        bool flag3 = false;
        int? nullable8 = nullable7.GetValueOrDefault() == flag3 & nullable7.HasValue ? this.GetDefaultAccountID(e) : (int?) e.Cache.GetValueOriginal<INItemSite.invtAcctID>(e.Row);
        local3 = nullable8;
        ref int? local4 = ref newAccountID;
        nullable7 = row.OverrideInvtAcctSub;
        bool flag4 = false;
        int? nullable9 = nullable7.GetValueOrDefault() == flag4 & nullable7.HasValue ? this.GetDefaultAccountID(e) : (int?) e.NewValue;
        local4 = nullable9;
        int? nullable10 = originalAccountID;
        int? nullable11 = newAccountID;
        return !(nullable10.GetValueOrDefault() == nullable11.GetValueOrDefault() & nullable10.HasValue == nullable11.HasValue);
      }

      private int? GetDefaultAccountID(GetEditPreventingReasonArgs e)
      {
        INItemSite row = (INItemSite) e.Row;
        InventoryItem inventoryItem = InventoryItem.PK.Find(e.Graph, row.InventoryID);
        INSite site = INSite.PK.Find(e.Graph, row.SiteID);
        INPostClass postclass = INPostClass.PK.Find(e.Graph, inventoryItem.PostClassID);
        INItemSite copy = (INItemSite) e.Cache.CreateCopy((object) row);
        INItemSiteMaint.DefaultInvtAcctSub(e.Graph, copy, inventoryItem, site, postclass);
        return copy.InvtAcctID;
      }

      protected virtual void _(
        PX.Data.Events.FieldUpdated<INItemSite, INItemSite.overrideInvtAcctSub> e)
      {
        this.GetEditPreventingReason(new GetEditPreventingReasonArgs(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<INItemSite, INItemSite.overrideInvtAcctSub>>) e).Cache, typeof (INItemSite.invtAcctID), (object) e.Row, (object) e.Row.InvtAcctID, false));
      }
    }
  }

  public abstract class invtSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.invtSubID>
  {
  }

  public abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSite.valMethod>
  {
  }

  public abstract class dfltShipLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSite.dfltShipLocationID>
  {
  }

  public abstract class dfltReceiptLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSite.dfltReceiptLocationID>
  {
  }

  public abstract class dfltPutawayLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSite.dfltPutawayLocationID>
  {
  }

  public abstract class dfltSalesUnit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSite.dfltSalesUnit>
  {
  }

  public abstract class dfltPurchaseUnit : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.dfltPurchaseUnit>
  {
  }

  public abstract class lastStdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.lastStdCost>
  {
  }

  public abstract class pendingStdCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.pendingStdCost>
  {
  }

  public abstract class pendingStdCostDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.pendingStdCostDate>
  {
  }

  public abstract class pendingStdCostReset : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.pendingStdCostReset>
  {
  }

  public abstract class stdCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.stdCost>
  {
  }

  public abstract class stdCostDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemSite.stdCostDate>
  {
  }

  public abstract class lastBasePrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.lastBasePrice>
  {
  }

  public abstract class pendingBasePrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.pendingBasePrice>
  {
  }

  public abstract class pendingBasePriceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.pendingBasePriceDate>
  {
  }

  public abstract class basePrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.basePrice>
  {
  }

  public abstract class basePriceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.basePriceDate>
  {
  }

  public abstract class lastCostDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemSite.lastCostDate>
  {
  }

  public abstract class lastCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.lastCost>
  {
  }

  public abstract class avgCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.avgCost>
  {
  }

  public abstract class minCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.minCost>
  {
  }

  public abstract class maxCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.maxCost>
  {
  }

  public abstract class tranUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.tranUnitCost>
  {
  }

  public abstract class preferredVendorOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.preferredVendorOverride>
  {
  }

  public abstract class preferredVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.preferredVendorID>
  {
    public class PreventEditBAccountVOrgBAccountID<TGraph> : 
      PreventEditBAccountRestrictToBase<PX.Objects.CR.BAccount.vOrgBAccountID, TGraph, INItemSite, SelectFromBase<INItemSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
      #nullable enable
      INItemSite.preferredVendorOverride, 
      #nullable disable
      Equal<True>>>>>.And<BqlOperand<
      #nullable enable
      INItemSite.preferredVendorID, IBqlInt>.IsEqual<
      #nullable disable
      BqlField<
      #nullable enable
      PX.Objects.CR.BAccount.bAccountID, IBqlInt>.FromCurrent>>>>
      where TGraph : 
      #nullable disable
      PXGraph
    {
      protected override string GetErrorMessage(
        PX.Objects.CR.BAccount baccount,
        INItemSite document,
        string documentBaseCurrency)
      {
        InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<TGraph>) this).Base, document.InventoryID);
        INSite inSite = INSite.PK.Find((PXGraph) ((PXGraphExtension<TGraph>) this).Base, document.SiteID);
        return PXMessages.LocalizeFormatNoPrefix("A branch with the base currency other than {0} cannot be associated with the {1} vendor because {1} is specified as the preferred vendor in the {3} warehouse details of the {2} item.", new object[4]
        {
          (object) documentBaseCurrency,
          (object) baccount.AcctCD,
          (object) inventoryItem?.InventoryCD,
          (object) inSite?.SiteCD
        });
      }

      protected override string GetBaseCurrency(INItemSite document)
      {
        return INSite.PK.Find((PXGraph) ((PXGraphExtension<TGraph>) this).Base, document.SiteID).BaseCuryID;
      }
    }

    public class PreventEditBAccountVOrgBAccountIDOnVendorMaint : 
      INItemSite.preferredVendorID.PreventEditBAccountVOrgBAccountID<VendorMaint>
    {
      public static bool IsActive()
      {
        return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
      }
    }

    public class PreventEditBAccountVOrgBAccountIDOnCustomerMaint : 
      INItemSite.preferredVendorID.PreventEditBAccountVOrgBAccountID<CustomerMaint>
    {
      public static bool IsActive()
      {
        return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
      }
    }
  }

  public abstract class preferredVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSite.preferredVendorLocationID>
  {
  }

  public abstract class productManagerOverride : IBqlField, IBqlOperand
  {
  }

  public abstract class productWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSite.productWorkgroupID>
  {
  }

  public abstract class productManagerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.productManagerID>
  {
  }

  public abstract class priceWorkgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.priceWorkgroupID>
  {
  }

  public abstract class priceManagerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.priceManagerID>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.isDefault>
  {
  }

  public abstract class stdCostOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.stdCostOverride>
  {
  }

  public abstract class basePriceOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.basePriceOverride>
  {
  }

  public abstract class commissionable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.commissionable>
  {
  }

  public abstract class aBCCodeOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.aBCCodeOverride>
  {
  }

  public abstract class aBCCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSite.aBCCodeID>
  {
  }

  public abstract class aBCCodeIsFixed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.aBCCodeIsFixed>
  {
  }

  public abstract class movementClassOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.movementClassOverride>
  {
  }

  public abstract class movementClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.movementClassID>
  {
  }

  public abstract class pendingMovementClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.movementClassID>
  {
  }

  public abstract class movementClassIsFixed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.movementClassIsFixed>
  {
  }

  public abstract class pendingMovementClassPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.pendingMovementClassPeriodID>
  {
  }

  public abstract class pendingMovementClassUpdateDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.pendingMovementClassUpdateDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemSite.noteID>
  {
  }

  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.pOCreate>
  {
  }

  public abstract class pOSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSite.pOSource>
  {
  }

  public abstract class markupPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.markupPct>
  {
  }

  public abstract class markupPctOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.markupPctOverride>
  {
  }

  public abstract class recPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.recPrice>
  {
  }

  public abstract class recPriceOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.recPriceOverride>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemSite.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INItemSite.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.lastModifiedDateTime>
  {
  }

  public abstract class replenishmentClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.replenishmentClassID>
  {
  }

  public abstract class replenishmentPolicyOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.replenishmentPolicyOverride>
  {
  }

  public abstract class replenishmentPolicyID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.replenishmentPolicyID>
  {
  }

  public abstract class replenishmentSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.replenishmentSource>
  {
  }

  public abstract class replenishmentMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.replenishmentMethod>
  {
  }

  public abstract class replenishmentSourceSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemSite.replenishmentSourceSiteID>
  {
  }

  public abstract class maxShelfLifeOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.maxShelfLifeOverride>
  {
  }

  public abstract class maxShelfLife : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemSite.maxShelfLife>
  {
  }

  public abstract class launchDateOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.launchDateOverride>
  {
  }

  public abstract class launchDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INItemSite.launchDate>
  {
  }

  public abstract class terminationDateOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.terminationDateOverride>
  {
  }

  public abstract class terminationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.terminationDate>
  {
  }

  public abstract class serviceLevelOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.serviceLevelOverride>
  {
  }

  public abstract class serviceLevel : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.serviceLevel>
  {
  }

  public abstract class serviceLevelPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.serviceLevelPct>
  {
  }

  public abstract class safetyStockOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.safetyStockOverride>
  {
  }

  public abstract class safetyStock : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.safetyStock>
  {
  }

  public abstract class minQtyOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.minQtyOverride>
  {
  }

  public abstract class minQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.minQty>
  {
  }

  public abstract class maxQtyOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.maxQtyOverride>
  {
  }

  public abstract class maxQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.maxQty>
  {
  }

  public abstract class transferERQOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.transferERQOverride>
  {
  }

  public abstract class transferERQ : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.transferERQ>
  {
  }

  public abstract class subItemOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.subItemOverride>
  {
  }

  public abstract class safetyStockSuggested : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.safetyStockSuggested>
  {
  }

  public abstract class minQtySuggested : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.minQtySuggested>
  {
  }

  public abstract class maxQtySuggested : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.maxQtySuggested>
  {
  }

  public abstract class eSSmoothingConstantL : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.eSSmoothingConstantL>
  {
  }

  public abstract class eSSmoothingConstantLOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.eSSmoothingConstantLOverride>
  {
  }

  public abstract class eSSmoothingConstantT : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.eSSmoothingConstantT>
  {
  }

  public abstract class eSSmoothingConstantTOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.eSSmoothingConstantTOverride>
  {
  }

  public abstract class eSSmoothingConstantS : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.eSSmoothingConstantS>
  {
  }

  public abstract class eSSmoothingConstantSOverride : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INItemSite.eSSmoothingConstantSOverride>
  {
  }

  public abstract class autoFitModel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INItemSite.autoFitModel>
  {
  }

  public abstract class demandPerDayAverage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.demandPerDayAverage>
  {
  }

  public abstract class demandPerDayMSE : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.demandPerDayMSE>
  {
  }

  public abstract class demandPerDayMAD : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.demandPerDayMAD>
  {
  }

  public abstract class demandPerDaySTDEV : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.demandPerDaySTDEV>
  {
  }

  public abstract class leadTimeAverage : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INItemSite.leadTimeAverage>
  {
  }

  public abstract class leadTimeMSE : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.leadTimeMSE>
  {
  }

  public abstract class leadTimeSTDEV : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INItemSite.leadTimeSTDEV>
  {
  }

  public abstract class lastForecastDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.lastForecastDate>
  {
  }

  public abstract class forecastModelType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.forecastModelType>
  {
  }

  public abstract class forecastPeriodType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.forecastPeriodType>
  {
  }

  public abstract class lastFCApplicationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemSite.lastFCApplicationDate>
  {
  }

  public abstract class countryOfOrigin : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemSite.countryOfOrigin>
  {
  }

  public abstract class planningMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemSite.planningMethod>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemSite.Tstamp>
  {
  }
}
