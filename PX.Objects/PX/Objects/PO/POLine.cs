// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.Matrix.Interfaces;
using PX.Objects.PM;
using PX.Objects.RQ;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName("PO Line")]
[Serializable]
public class POLine : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ISortOrder,
  ITaxableDetail,
  IMatrixItemLine,
  ICommitmentSource,
  IItemPlanPOSource,
  IItemPlanSource,
  IItemPlanMaster,
  IProjectLine
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected int? _InventoryID;
  protected string _LineType;
  protected bool? _ProcessNonStockAsServiceViaPR;
  protected bool? _AccrueCost;
  protected long? _PlanID;
  protected string _POType;
  protected string _PONbr;
  protected int? _POLineNbr;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected int? _ShipToBAccountID;
  protected int? _ShipToLocationID;
  protected DateTime? _OrderDate;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected string _LotSerialNbr;
  protected string _BLType;
  protected string _BLOrderNbr;
  protected int? _BLLineNbr;
  protected string _RQReqNbr;
  protected int? _RQReqLineNbr;
  protected string _UOM;
  protected Decimal? _OrderQty;
  protected Decimal? _BaseOrderQty;
  protected Decimal? _ReceivedQty;
  protected Decimal? _BaseReceivedQty;
  protected long? _CuryInfoID;
  protected Decimal? _CuryUnitCost;
  protected Decimal? _UnitCost;
  protected bool? _CalculateDiscountsOnImport;
  protected Decimal? _DiscPct;
  protected Decimal? _CuryDiscAmt;
  protected Decimal? _DiscAmt;
  protected bool? _ManualPrice;
  protected bool? _ManualDisc;
  protected Decimal? _CuryLineAmt;
  protected Decimal? _LineAmt;
  protected Decimal? _CuryDiscCost;
  protected Decimal? _DiscCost;
  protected Decimal? _CuryExtCost;
  protected Decimal? _ExtCost;
  protected ushort[] _DiscountsAppliedToLine;
  protected Decimal? _GroupDiscountRate;
  protected Decimal? _DocumentDiscountRate;
  protected string _TaxCategoryID;
  protected string _TaxID;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected int? _ExpenseAcctID;
  protected int? _ExpenseSubID;
  protected string _AlternateID;
  protected string _TranDesc;
  protected Decimal? _UnitWeight;
  protected Decimal? _UnitVolume;
  protected Decimal? _ExtWeight;
  protected Decimal? _ExtVolume;
  protected int? _CostCodeID;
  protected Guid? _CommitmentID;
  protected string _ReasonCode;
  protected Guid? _NoteID;
  protected Decimal? _RcptQtyMin;
  protected Decimal? _RcptQtyMax;
  protected Decimal? _RcptQtyThreshold;
  protected string _RcptQtyAction;
  protected string _ReceiptStatus;
  protected DateTime? _DRTermStartDate;
  protected DateTime? _DRTermEndDate;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected DateTime? _RequestedDate;
  protected DateTime? _PromisedDate;
  protected bool? _Cancelled;
  protected string _CompletePOLine;
  protected bool? _AllowComplete;
  protected string _DiscountID;
  protected string _DiscountSequenceID;
  protected bool? _HasInclusiveTaxes;
  protected bool? _AllowEditUnitCostInPR;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [Branch(typeof (POOrder.branchID), null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXParent(typeof (POLine.FK.Order))]
  [PXUIField]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXLineNbr(typeof (POOrder.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(1)]
  [PXDefault(typeof (POOrder.dropshipReceiptProcessing))]
  [PXUIField(DisplayName = "Drop-Ship Receipt Processing", Enabled = false)]
  public virtual string DropshipReceiptProcessing { get; set; }

  [PXDBString(1)]
  [PXDefault(typeof (POOrder.dropshipExpenseRecording))]
  [PXUIField(DisplayName = "Record Drop-Ship Expenses", Enabled = false)]
  public virtual string DropshipExpenseRecording { get; set; }

  [PXUIField(DisplayName = "Line Order", Visible = false, Enabled = false)]
  [PXDBInt]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBBool]
  [PXUIField]
  public virtual bool? IsStockItem { get; set; }

  [PXDefault]
  [POLineInventoryItem(Filterable = true)]
  [PXForeignReference(typeof (POLine.FK.InventoryItem))]
  [ConvertedInventoryItem(typeof (POLine.isStockItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault("SV")]
  [PXDBString(2, IsFixed = true)]
  [POLineTypeList2(typeof (POLine.orderType), typeof (POLine.inventoryID))]
  [PXUIField(DisplayName = "Line Type")]
  [PXUnboundFormula(typeof (Switch<Case<Where<POLine.lineType, In3<POLineType.goodsForDropShip, POLineType.nonStockForDropShip>, And<POLine.completed, Equal<False>>>, int1>, int0>), typeof (SumCalc<POOrder.dropShipOpenLinesCntr>), ValidateAggregateCalculation = true)]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that non-stock item will be processed as service via Purchase Receipt.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (Switch<Case<Where2<FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>, And<Selector<POLine.inventoryID, PX.Objects.IN.InventoryItem.nonStockReceiptAsService>, Equal<True>>>, True>, False>))]
  [PXUIField(DisplayName = "Process as Service via Purchase Receipt", Enabled = false, Visible = false)]
  public virtual bool? ProcessNonStockAsServiceViaPR
  {
    get => this._ProcessNonStockAsServiceViaPR;
    set => this._ProcessNonStockAsServiceViaPR = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that cost will be processed using expense accrual account.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (Switch<Case<Where<POLine.orderType, Equal<POOrderType.projectDropShip>>, False, Case<Where<POLine.orderType, Equal<POOrderType.regularSubcontract>>, False, Case<Where<BqlOperand<IsNull<Selector<POLine.inventoryID, PX.Objects.IN.InventoryItem.postToExpenseAccount>, PX.Objects.IN.InventoryItem.postToExpenseAccount.purchases>, IBqlString>.IsEqual<PX.Objects.IN.InventoryItem.postToExpenseAccount.purchases>>, False>>>, True>))]
  [PXUIField(DisplayName = "Accrue Cost", Enabled = false, Visible = false)]
  public virtual bool? AccrueCost
  {
    get => this._AccrueCost;
    set => this._AccrueCost = value;
  }

  [PXDBLong(IsImmutable = true)]
  [PXUIField(DisplayName = "Plan ID", Visible = false, Enabled = false)]
  public virtual long? PlanID
  {
    get => this._PlanID;
    set => this._PlanID = value;
  }

  [PXBool]
  public virtual bool? ClearPlanID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [POOrderType.List]
  [PXUIField(DisplayName = "Blanket PO Type", Enabled = false)]
  public virtual string POType
  {
    get => this._POType;
    set => this._POType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Blanket PO Nbr.", Enabled = false)]
  [PX.Objects.PO.PO.RefNbr(typeof (Search2<POOrder.orderNbr, InnerJoinSingleTable<PX.Objects.AP.Vendor, On<POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>, Where<POOrder.orderType, Equal<Current<POLine.pOType>>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>, OrderBy<Desc<POOrder.orderNbr>>>), Filterable = true)]
  public virtual string PONbr
  {
    get => this._PONbr;
    set => this._PONbr = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Blanket PO Line Nbr.", Enabled = false)]
  public virtual int? POLineNbr
  {
    get => this._POLineNbr;
    set => this._POLineNbr = value;
  }

  [POVendor]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXInt]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [PXInt]
  public virtual int? ShipToBAccountID
  {
    get => this._ShipToBAccountID;
    set => this._ShipToBAccountID = value;
  }

  [PXInt]
  public virtual int? ShipToLocationID
  {
    get => this._ShipToLocationID;
    set => this._ShipToLocationID = value;
  }

  [PXDBDate]
  [PXDefault]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POLine.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [SubItem(typeof (POLine.inventoryID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [POSiteAvail(typeof (POLine.inventoryID), typeof (POLine.subItemID), typeof (POLine.costCenterID), DocumentBranchType = typeof (POOrder.branchID))]
  [PXDefault(typeof (Coalesce<Search<POOrder.siteID, Where<POOrder.orderType, Equal<Current<POOrder.orderType>>, And<POOrder.orderNbr, Equal<Current<POOrder.orderNbr>>, And<POOrder.shipDestType, Equal<POShippingDestination.site>>>>>, Search<LocationBranchSettings.vSiteID, Where<LocationBranchSettings.locationID, Equal<Current<POOrder.vendorLocationID>>, And<LocationBranchSettings.bAccountID, Equal<Current<POOrder.vendorID>>, And<LocationBranchSettings.branchID, Equal<Current<POOrder.branchID>>>>>>, Search<PX.Objects.CR.Location.vSiteID, Where<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>>>>, Search<InventoryItemCurySettings.dfltSiteID, Where<InventoryItemCurySettings.inventoryID, Equal<Current<POLine.inventoryID>>, And<InventoryItemCurySettings.curyID, EqualBaseCuryID<Current2<POOrder.branchID>>>>>>))]
  [PXForeignReference(typeof (POLine.FK.Site))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Lot Serial Number", Visible = false)]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  public virtual string BLType
  {
    get => this._BLType;
    set => this._BLType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string BLOrderNbr
  {
    get => this._BLOrderNbr;
    set => this._BLOrderNbr = value;
  }

  [PXDBInt]
  public virtual int? BLLineNbr
  {
    get => this._BLLineNbr;
    set => this._BLLineNbr = value;
  }

  [PXDBString(15, IsUnicode = true)]
  public virtual string RQReqNbr
  {
    get => this._RQReqNbr;
    set => this._RQReqNbr = value;
  }

  [PXDBInt]
  public virtual int? RQReqLineNbr
  {
    get => this._RQReqLineNbr;
    set => this._RQReqLineNbr = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POLine.inventoryID>>>>))]
  [INUnit(typeof (POLine.inventoryID), DisplayName = "UOM")]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (POLine.uOM), typeof (POLine.baseOrderQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (SumCalc<POOrder.orderQty>), ValidateAggregateCalculation = true)]
  [PXUIField]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBDecimal]
  public virtual Decimal? OrigOrderQty { get; set; }

  [PXUIField(DisplayName = "Base Order Qty.", Visible = false, Enabled = false)]
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOrderQty
  {
    get => this._BaseOrderQty;
    set => this._BaseOrderQty = value;
  }

  [PXDBQuantity(typeof (POLine.uOM), typeof (POLine.baseOrderedQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Orders", Enabled = false)]
  public virtual Decimal? OrderedQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOrderedQty { get; set; }

  [PXDBQuantity(typeof (POLine.uOM), typeof (POLine.baseReceivedQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ReceivedQty
  {
    get => this._ReceivedQty;
    set => this._ReceivedQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? BaseReceivedQty
  {
    get => this._BaseReceivedQty;
    set => this._BaseReceivedQty = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (POOrder.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrencyPriceCost(typeof (POLine.curyInfoID), typeof (POLine.unitCost))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitCost
  {
    get => this._CuryUnitCost;
    set => this._CuryUnitCost = value;
  }

  [PXDBPriceCost]
  [PXDefault(typeof (Search<INItemCost.lastCost, Where<INItemCost.inventoryID, Equal<Current<POLine.inventoryID>>, And<INItemCost.curyID, EqualBaseCuryID<Current2<POLine.branchID>>>>>))]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Calculate automatic discounts on import")]
  public virtual bool? CalculateDiscountsOnImport
  {
    get => this._CalculateDiscountsOnImport;
    set => this._CalculateDiscountsOnImport = value;
  }

  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIVerify]
  public virtual Decimal? DiscPct
  {
    get => this._DiscPct;
    set => this._DiscPct = value;
  }

  [PXDBCurrency(typeof (POLine.curyInfoID), typeof (POLine.discAmt))]
  [PXUIField(DisplayName = "Discount Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscAmt
  {
    get => this._CuryDiscAmt;
    set => this._CuryDiscAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt
  {
    get => this._DiscAmt;
    set => this._DiscAmt = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Cost", Visible = false)]
  public virtual bool? ManualPrice
  {
    get => this._ManualPrice;
    set => this._ManualPrice = value;
  }

  [ManualDiscountMode(typeof (POLine.curyDiscAmt), typeof (POLine.curyExtCost), typeof (POLine.discPct), DiscountFeatureType.VendorDiscount)]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ManualDisc
  {
    get => this._ManualDisc;
    set => this._ManualDisc = value;
  }

  [PXDBCurrency(typeof (POLine.curyInfoID), typeof (POLine.lineAmt))]
  [PXUIField(DisplayName = "Ext. Cost")]
  [PXFormula(typeof (Mult<POLine.orderQty, POLine.curyUnitCost>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIVerify]
  public virtual Decimal? CuryLineAmt
  {
    get => this._CuryLineAmt;
    set => this._CuryLineAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineAmt
  {
    get => this._LineAmt;
    set => this._LineAmt = value;
  }

  [PXDBPriceCostCalced(typeof (Switch<Case<Where<POLine.discPct, NotEqual<decimal0>>, Mult<POLine.curyUnitCost, Sub<decimal1, Div<POLine.discPct, decimal100>>>, Case<Where<POLine.orderQty, Equal<decimal0>>, decimal0, Case<Where<POLine.manualPrice, Equal<False>>, POLine.curyUnitCost>>>, Div<Sub<POLine.curyLineAmt, POLine.curyDiscAmt>, POLine.orderQty>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  [PXFormula(typeof (Switch<Case<Where<POLine.discPct, NotEqual<decimal0>>, Mult<POLine.curyUnitCost, Sub<decimal1, Div<POLine.discPct, decimal100>>>, Case<Where<POLine.manualPrice, Equal<False>>, POLine.curyUnitCost>>, Div<Sub<POLine.curyLineAmt, POLine.curyDiscAmt>, NullIf<POLine.orderQty, decimal0>>>))]
  [PXCurrencyPriceCost(typeof (POLine.curyInfoID), typeof (POLine.discCost))]
  [PXUIField(DisplayName = "Disc. Unit Cost", Enabled = false, Visible = false)]
  public virtual Decimal? CuryDiscCost
  {
    get => this._CuryDiscCost;
    set => this._CuryDiscCost = value;
  }

  [PXPriceCost]
  [PXDBPriceCostCalced(typeof (Switch<Case<Where<POLine.orderQty, Equal<decimal0>>, decimal0>, Div<POLine.lineAmt, POLine.orderQty>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  [PXFormula(typeof (Div<Row<POLine.lineAmt>.WithDependency<POLine.curyLineAmt>, NullIf<POLine.orderQty, decimal0>>))]
  public virtual Decimal? DiscCost
  {
    get => this._DiscCost;
    set => this._DiscCost = value;
  }

  [DBRetainagePercent(typeof (POOrder.retainageApply), typeof (POOrder.defRetainagePct), typeof (Sub<Current<POLine.curyLineAmt>, Current<POLine.curyDiscAmt>>), typeof (POLine.curyRetainageAmt), typeof (POLine.retainagePct))]
  public virtual Decimal? RetainagePct { get; set; }

  [DBRetainageAmount(typeof (POLine.curyInfoID), typeof (Sub<POLine.curyLineAmt, POLine.curyDiscAmt>), typeof (POLine.curyRetainageAmt), typeof (POLine.retainageAmt), typeof (POLine.retainagePct))]
  public virtual Decimal? CuryRetainageAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageAmt { get; set; }

  [PXDBCurrency(typeof (POLine.curyInfoID), typeof (POLine.extCost))]
  [PXUIField]
  [PXFormula(typeof (Sub<POLine.curyLineAmt, Add<POLine.curyDiscAmt, POLine.curyRetainageAmt>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIVerify]
  public virtual Decimal? CuryExtCost
  {
    get => this._CuryExtCost;
    set => this._CuryExtCost = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? ExtCost
  {
    get => this._ExtCost;
    set => this._ExtCost = value;
  }

  [PXDBDecimal]
  public virtual Decimal? OrigExtCost { get; set; }

  [PXDBPackedIntegerArray]
  public virtual ushort[] DiscountsAppliedToLine
  {
    get => this._DiscountsAppliedToLine;
    set => this._DiscountsAppliedToLine = value;
  }

  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? GroupDiscountRate
  {
    get => this._GroupDiscountRate;
    set => this._GroupDiscountRate = value;
  }

  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? DocumentDiscountRate
  {
    get => this._DocumentDiscountRate;
    set => this._DocumentDiscountRate = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.taxCategoryID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POLine.inventoryID>>>>))]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax ID", Visible = false)]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [POProjectDefault(typeof (POLine.lineType), AccountType = typeof (POLine.expenseAcctID))]
  [PXRestrictor(typeof (Where<PX.Objects.CT.Contract.isCancelled, Equal<False>>), "The {0} project or contract is canceled.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInPO, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase]
  [PXForeignReference(typeof (POLine.FK.Project))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault(typeof (Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<POLine.projectID>>, And<PMTask.isDefault, Equal<True>>>>))]
  [ActiveOrInPlanningProjectTask(typeof (POLine.projectID), "PO", DisplayName = "Project Task")]
  [PXForeignReference(typeof (POLine.FK.Task))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [Account(typeof (POLine.branchID))]
  [PXDefault]
  public virtual int? ExpenseAcctID
  {
    get => this._ExpenseAcctID;
    set => this._ExpenseAcctID = value;
  }

  [SubAccount(typeof (POLine.expenseAcctID), typeof (POLine.branchID), false)]
  [PXDefault]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  [Account(typeof (POLine.branchID), DescriptionField = typeof (PX.Objects.GL.Account.description), DisplayName = "Accrual Account", Filterable = false, ControlAccountForModule = "PO")]
  [PXDefault]
  public virtual int? POAccrualAcctID { get; set; }

  [SubAccount(typeof (POLine.pOAccrualAcctID), typeof (POLine.branchID), false, DisplayName = "Accrual Sub.", Filterable = true)]
  [PXDefault]
  public virtual int? POAccrualSubID { get; set; }

  [AlternativeItem(INPrimaryAlternateType.VPN, typeof (POLine.vendorID), typeof (POLine.inventoryID), typeof (POLine.subItemID), typeof (POLine.uOM))]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<PX.Objects.IN.InventoryItem.baseWeight, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POLine.inventoryID>>, And<PX.Objects.IN.InventoryItem.baseWeight, IsNotNull>>>))]
  [PXUIField(DisplayName = "Unit Weight")]
  public virtual Decimal? UnitWeight
  {
    get => this._UnitWeight;
    set => this._UnitWeight = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<PX.Objects.IN.InventoryItem.baseVolume, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<POLine.inventoryID>>, And<PX.Objects.IN.InventoryItem.baseVolume, IsNotNull>>>))]
  [PXUIField(DisplayName = "Unit Volume")]
  public virtual Decimal? UnitVolume
  {
    get => this._UnitVolume;
    set => this._UnitVolume = value;
  }

  [PXDBDecimal(6)]
  [PXUIField]
  [PXFormula(typeof (Mult<Row<POLine.baseOrderQty>.WithDependencies<POLine.uOM, POLine.orderQty>, POLine.unitWeight>), typeof (SumCalc<POOrder.orderWeight>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtWeight
  {
    get => this._ExtWeight;
    set => this._ExtWeight = value;
  }

  [PXDBDecimal(6)]
  [PXUIField]
  [PXFormula(typeof (Mult<Row<POLine.baseOrderQty>.WithDependencies<POLine.uOM, POLine.orderQty>, POLine.unitVolume>), typeof (SumCalc<POOrder.orderVolume>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtVolume
  {
    get => this._ExtVolume;
    set => this._ExtVolume = value;
  }

  [CostCode(typeof (POLine.expenseAcctID), typeof (POLine.taskID), "E", InventoryField = typeof (POLine.inventoryID), VendorField = typeof (POLine.vendorID), ProjectField = typeof (POLine.projectID), UseNewDefaulting = true, DescriptionField = typeof (PMCostCode.description))]
  [PXForeignReference(typeof (POLine.FK.CostCode))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDBGuid(false)]
  public virtual Guid? CommitmentID
  {
    get => this._CommitmentID;
    set => this._CommitmentID = value;
  }

  [PXDBString(20, IsUnicode = true)]
  public virtual string ReasonCode
  {
    get => this._ReasonCode;
    set => this._ReasonCode = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vRcptQtyMin, Where<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>>>>))]
  [PXUIField(DisplayName = "Min. Receipt (%)")]
  public virtual Decimal? RcptQtyMin
  {
    get => this._RcptQtyMin;
    set => this._RcptQtyMin = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vRcptQtyMax, Where<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>>>>))]
  [PXUIField(DisplayName = "Max. Receipt (%)")]
  public virtual Decimal? RcptQtyMax
  {
    get => this._RcptQtyMax;
    set => this._RcptQtyMax = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 999.0)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vRcptQtyThreshold, Where<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>>>>))]
  [PXUIField(DisplayName = "Complete On (%)")]
  public virtual Decimal? RcptQtyThreshold
  {
    get => this._RcptQtyThreshold;
    set => this._RcptQtyThreshold = value;
  }

  [PXDBString(1, IsFixed = true)]
  [POReceiptQtyAction.List]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vRcptQtyAction, Where<PX.Objects.CR.Location.locationID, Equal<Current<POOrder.vendorLocationID>>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<POOrder.vendorID>>>>>))]
  [PXUIField(DisplayName = "Receipt Action")]
  public virtual string RcptQtyAction
  {
    get => this._RcptQtyAction;
    set => this._RcptQtyAction = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("")]
  public virtual string ReceiptStatus
  {
    get => this._ReceiptStatus;
    set => this._ReceiptStatus = value;
  }

  [PXDBCurrency(typeof (POLine.curyInfoID), typeof (POLine.bLOrderedCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Received Cost", Visible = false)]
  public virtual Decimal? CuryBLOrderedCost { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BLOrderedCost { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public DateTime? DRTermStartDate
  {
    get => this._DRTermStartDate;
    set => this._DRTermStartDate = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public DateTime? DRTermEndDate
  {
    get => this._DRTermEndDate;
    set => this._DRTermEndDate = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the <see cref="P:PX.Objects.PO.POLine.DRTermStartDate" /> and <see cref="P:PX.Objects.PO.POLine.DRTermEndDate" />
  /// fields are enabled for the line.
  /// </summary>
  /// <value>
  /// The value of this field is set by the <see cref="T:PX.Objects.PO.POOrderEntry" /> graph based on the settings of the
  /// <see cref="P:PX.Objects.PO.POLine.InventoryID">item</see> selected for the line. In other contexts it is not populated.
  /// See the attribute on the <see cref="M:PX.Objects.PO.POOrderEntry.POLine_ItemRequiresTerms_CacheAttached(PX.Data.PXCache)" /> handler for details.
  /// </value>
  [PXBool]
  public virtual bool? ItemRequiresTerms { get; set; }

  [PXUIField]
  [PX.Objects.SO.SOOrderStatus.List]
  [PXString(1, IsFixed = true)]
  public virtual string SOOrderStatus { get; set; }

  [PXString(2, IsFixed = true)]
  public virtual string SOOrderType { get; set; }

  [PXUIField(DisplayName = "Sales Order Nbr.", Enabled = false, FieldClass = "DropShipments")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<POLine.sOOrderType>>>>))]
  [PXDependsOnFields(new System.Type[] {typeof (POLine.lineType)})]
  [PXString(15, IsUnicode = true)]
  public virtual string SOOrderNbr { get; set; }

  [PXUIField(DisplayName = "Sales Order Line Nbr.", Enabled = false, FieldClass = "DropShipments")]
  [PXDependsOnFields(new System.Type[] {typeof (POLine.lineType)})]
  [PXInt]
  public virtual int? SOLineNbr { get; set; }

  [PXBool]
  [PXUIField]
  public virtual bool? SOLinkActive { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Special Order", FieldClass = "SpecialOrders", Enabled = false)]
  [PXUnboundFormula(typeof (IIf<Where<POLine.isSpecialOrder, Equal<True>>, int1, int0>), typeof (SumCalc<POOrder.specialLineCntr>))]
  public virtual bool? IsSpecialOrder { get; set; }

  [PXDBInt]
  [PXDefault(typeof (CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
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

  [PXDBQuantity(typeof (POLine.uOM), typeof (POLine.baseCompletedQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Received Qty.", Enabled = false)]
  public virtual Decimal? CompletedQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseCompletedQty { get; set; }

  [PXDBQuantity(typeof (POLine.uOM), typeof (POLine.baseBilledQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billed Qty.", Enabled = false)]
  public virtual Decimal? BilledQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseBilledQty { get; set; }

  [PXDBCurrency(typeof (POLine.curyInfoID), typeof (POLine.billedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billed Amount", Enabled = false)]
  public virtual Decimal? CuryBilledAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledAmt { get; set; }

  [PXDBQuantity(typeof (POLine.uOM), typeof (POLine.baseOpenQty), HandleEmptyKey = true)]
  [PXFormula(typeof (Switch<Case<Where<POLine.completed, Equal<True>, Or<POLine.cancelled, Equal<True>>>, decimal0>, Maximum<Sub<POLine.orderQty, Maximum<POLine.completedQty, POLine.orderedQty>>, decimal0>>), typeof (SumCalc<POOrder.openOrderQty>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
  public virtual Decimal? OpenQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOpenQty { get; set; }

  [PXDBQuantity(typeof (POLine.uOM), typeof (POLine.baseUnbilledQty), HandleEmptyKey = true)]
  [PXFormula(typeof (Switch<Case<Where<POLine.closed, Equal<True>, Or<POLine.cancelled, Equal<True>>>, decimal0, Case<Where<POLine.orderQty, GreaterEqual<decimal0>>, Maximum<Sub<Maximum<POLine.orderQty, POLine.completedQty>, POLine.billedQty>, decimal0>>>, Minimum<Sub<Minimum<POLine.orderQty, POLine.completedQty>, POLine.billedQty>, decimal0>>), typeof (SumCalc<POOrder.unbilledOrderQty>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Qty.", Enabled = false)]
  public virtual Decimal? UnbilledQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseUnbilledQty { get; set; }

  [PXDBCurrency(typeof (POLine.curyInfoID), typeof (POLine.unbilledAmt))]
  [PXFormula(typeof (Switch<Case<Where<POLine.closed, Equal<True>, Or<POLine.cancelled, Equal<True>>>, decimal0, Case<Where<POLine.curyLineAmt, GreaterEqual<decimal0>>, Maximum<Sub<Sub<POLine.curyLineAmt, POLine.curyDiscAmt>, POLine.curyBilledAmt>, decimal0>>>, Minimum<Sub<Sub<POLine.curyLineAmt, POLine.curyDiscAmt>, POLine.curyBilledAmt>, decimal0>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Amount", Enabled = false)]
  public virtual Decimal? CuryUnbilledAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledAmt { get; set; }

  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(typeof (Maximum<Sub<POLine.orderQty, POLine.receivedQty>, decimal0>))]
  public virtual Decimal? LeftToReceiveQty { get; set; }

  [PXDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LeftToReceiveBaseQty
  {
    [PXDependsOnFields(new System.Type[] {typeof (POLine.baseOrderQty), typeof (POLine.baseReceivedQty)})] get
    {
      Decimal? baseOrderQty = this._BaseOrderQty;
      Decimal? baseReceivedQty = this._BaseReceivedQty;
      return !(baseOrderQty.HasValue & baseReceivedQty.HasValue) ? new Decimal?() : new Decimal?(baseOrderQty.GetValueOrDefault() - baseReceivedQty.GetValueOrDefault());
    }
  }

  [PXQuantity]
  [PXFormula(typeof (POLine.openQty))]
  [PXUIField(DisplayName = "Blanket Open Qty.", Enabled = false)]
  public virtual Decimal? NonOrderedQty { get; set; }

  [PXDBQuantity(typeof (POLine.uOM), typeof (POLine.baseReqPrepaidQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ReqPrepaidQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseReqPrepaidQty { get; set; }

  [PXQuantity]
  [PXFormula(typeof (Switch<Case<Where<POLine.orderQty, GreaterEqual<decimal0>>, Minimum<POLine.orderQty, POLine.reqPrepaidQty>>, Maximum<POLine.orderQty, POLine.reqPrepaidQty>>))]
  [PXUIField(DisplayName = "Prepaid Qty.", Enabled = false)]
  public virtual Decimal? DisplayReqPrepaidQty { get; set; }

  [PXDBCurrency(typeof (POLine.curyInfoID), typeof (POLine.reqPrepaidAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Prepaid Amount", Enabled = false)]
  public virtual Decimal? CuryReqPrepaidAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ReqPrepaidAmt { get; set; }

  [PXDBDate]
  [PXDefault(typeof (POOrder.orderDate))]
  [PXUIField(DisplayName = "Requested")]
  public virtual DateTime? RequestedDate
  {
    get => this._RequestedDate;
    set => this._RequestedDate = value;
  }

  [PXDBDate]
  [PXDefault(typeof (POOrder.expectedDate))]
  [PXUIField(DisplayName = "Promised")]
  public virtual DateTime? PromisedDate
  {
    get => this._PromisedDate;
    set => this._PromisedDate = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? Cancelled
  {
    get => this._Cancelled;
    set => this._Cancelled = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<POLine.lineType, In3<POLineType.nonStockForDropShip, POLineType.nonStockForSalesOrder>>, CompletePOLineTypes.quantity>, IsNull<Selector<POLine.inventoryID, PX.Objects.IN.InventoryItem.completePOLine>, CompletePOLineTypes.amount>>))]
  [PXUIField(DisplayName = "Close PO Line", Enabled = false, Visible = false)]
  [CompletePOLineTypes.List]
  public virtual string CompletePOLine
  {
    get => this._CompletePOLine;
    set => this._CompletePOLine = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  [PXUnboundFormula(typeof (Switch<Case<Where<POLine.lineType, NotEqual<POLineType.description>, And<POLine.completed, Equal<False>>>, int1>, int0>), typeof (SumCalc<POOrder.linesToCompleteCntr>), ValidateAggregateCalculation = true)]
  public virtual bool? Completed { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  [PXUnboundFormula(typeof (Switch<Case<Where<POLine.lineType, NotEqual<POLineType.description>, And<POLine.closed, Equal<False>>>, int1>, int0>), typeof (SumCalc<POOrder.linesToCloseCntr>), ValidateAggregateCalculation = true)]
  public virtual bool? Closed { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXFormula(typeof (Switch<Case<Where<Current<POOrder.pOAccrualType>, Equal<PX.Objects.PO.POAccrualType.order>, Or<Where<POLine.lineType, NotEqual<POLineType.service>>>>, Current<POOrder.pOAccrualType>>, Switch<Case<Where2<Where2<Not<FeatureInstalled<FeaturesSet.pOReceiptsWithoutInventory>>, And<Where<Current<POSetup.addServicesFromNormalPOtoPR>, Equal<True>, And<Current<POLine.orderType>, Equal<POOrderType.regularOrder>, Or<Current<POSetup.addServicesFromDSPOtoPR>, Equal<True>, And<Current<POLine.orderType>, Equal<POOrderType.dropShip>>>>>>>, Or<Current<POLine.processNonStockAsServiceViaPR>, Equal<True>>>, PX.Objects.PO.POAccrualType.receipt>, PX.Objects.PO.POAccrualType.order>>))]
  [PX.Objects.PO.POAccrualType.List]
  [PXUIField(DisplayName = "Billing Based On", Enabled = false)]
  public virtual string POAccrualType { get; set; }

  [PXDBGuid(false)]
  [PXDefault(typeof (POOrder.noteID))]
  public virtual Guid? OrderNoteID { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? AllowComplete
  {
    get => this._AllowComplete;
    set => this._AllowComplete = value;
  }

  [PXBool]
  [PXUIField]
  public virtual bool? IsKit { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<APDiscount.discountID, Where<APDiscount.bAccountID, Equal<Current<POLine.vendorID>>, And<APDiscount.type, Equal<DiscountType.LineDiscount>>>>))]
  [PXUIField(DisplayName = "Discount Code", Visible = true, Enabled = false)]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Discount Sequence", Visible = false, Enabled = false)]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXBool]
  [PXFormula(typeof (False))]
  public virtual bool? OrderedQtyAltered { get; set; }

  [PXString(6, IsUnicode = true)]
  public virtual string OverridenUOM { get; set; }

  [PXDecimal]
  public virtual Decimal? OverridenQty { get; set; }

  [PXDecimal]
  public virtual Decimal? BaseOverridenQty { get; set; }

  Decimal? ITaxableDetail.CuryTranAmt => this.CuryExtCost;

  Decimal? ITaxableDetail.TranAmt => this.ExtCost;

  [PXDecimal(4)]
  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public virtual Decimal? CuryReceivedCost
  {
    get => new Decimal?();
    set
    {
    }
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? HasInclusiveTaxes
  {
    get => this._HasInclusiveTaxes;
    set => this._HasInclusiveTaxes = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Editable Unit Cost in Receipt", Visible = false, Enabled = false)]
  [PXDefault(false)]
  public virtual bool? AllowEditUnitCostInPR
  {
    get => this._AllowEditUnitCostInPR;
    set => this._AllowEditUnitCostInPR = value;
  }

  [PXBool]
  [PXFormula(typeof (IIf<Where<POLine.lineType, In3<POLineType.goodsForSalesOrder, POLineType.nonStockForSalesOrder, POLineType.goodsForManufacturing, POLineType.nonStockForManufacturing, POLineType.goodsForServiceOrder, POLineType.nonStockForServiceOrder, POLineType.goodsForDropShip, POLineType.nonStockForDropShip, POLineType.goodsForReplenishment>>, True, False>))]
  public virtual bool? ViewDemandEnabled { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? SODeleted { get; set; }

  Decimal? IMatrixItemLine.Qty
  {
    get => this.OrderQty;
    set => this.OrderQty = value;
  }

  public class PK : PrimaryKeyOf<POLine>.By<POLine.orderType, POLine.orderNbr, POLine.lineNbr>
  {
    public static POLine Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (POLine) PrimaryKeyOf<POLine>.By<POLine.orderType, POLine.orderNbr, POLine.lineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<POLine>.By<POLine.branchID>
    {
    }

    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POLine>.By<POLine.orderType, POLine.orderNbr>
    {
    }

    public class BlanketOrder : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POLine>.By<POLine.pOType, POLine.pONbr>
    {
    }

    public class BlanketLine : 
      PrimaryKeyOf<POLine>.By<POLine.orderType, POLine.orderNbr, POLine.lineNbr>.ForeignKeyOf<POLine>.By<POLine.pOType, POLine.pONbr, POLine.pOLineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POLine>.By<POLine.inventoryID>
    {
    }

    public class Site : PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<POLine>.By<POLine.siteID>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<POLine>.By<POLine.inventoryID, POLine.subItemID, POLine.siteID>
    {
    }

    public class Requisition : 
      PrimaryKeyOf<RQRequisition>.By<RQRequisition.reqNbr>.ForeignKeyOf<POLine>.By<POLine.rQReqNbr>
    {
    }

    public class RequisitionLine : 
      PrimaryKeyOf<RQRequisitionLine>.By<RQRequisitionLine.reqNbr, RQRequisitionLine.lineNbr>.ForeignKeyOf<POLine>.By<POLine.rQReqNbr, POLine.rQReqLineNbr>
    {
    }

    public class ItemPlan : 
      PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.ForeignKeyOf<POLine>.By<POLine.planID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<POLine>.By<POLine.vendorID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POLine>.By<POLine.curyInfoID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<POLine>.By<POLine.taxCategoryID>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<POLine>.By<POLine.taxID>
    {
    }

    public class ExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<POLine>.By<POLine.expenseAcctID>
    {
    }

    public class ExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<POLine>.By<POLine.expenseSubID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<POLine>.By<POLine.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<POLine>.By<POLine.projectID, POLine.taskID>
    {
    }

    public class AccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<POLine>.By<POLine.pOAccrualAcctID>
    {
    }

    public class AccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<POLine>.By<POLine.pOAccrualSubID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<POLine>.By<POLine.costCodeID>
    {
    }

    public class ReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<POLine>.By<POLine.reasonCode>
    {
    }

    public class Discount : 
      PrimaryKeyOf<APDiscount>.By<APDiscount.discountID, APDiscount.bAccountID>.ForeignKeyOf<POLine>.By<POLine.discountID, POLine.vendorID>
    {
    }

    public class DiscountSequence : 
      PrimaryKeyOf<VendorDiscountSequence>.By<VendorDiscountSequence.vendorID, VendorDiscountSequence.discountID, VendorDiscountSequence.discountSequenceID>.ForeignKeyOf<POLine>.By<POLine.vendorID, POLine.discountID, POLine.discountSequenceID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.branchID>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.lineNbr>
  {
  }

  public abstract class dropshipReceiptProcessing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLine.dropshipReceiptProcessing>
  {
  }

  public abstract class dropshipExpenseRecording : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLine.dropshipExpenseRecording>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.sortOrder>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.isStockItem>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.inventoryID>
  {
    public class InventoryBaseUnitRule : 
      PX.Objects.IN.InventoryItem.baseUnit.PreventEditIfExists<Select<POLine, Where<POLine.inventoryID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>, And2<Where2<SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<POLineType.goodsForInventory, POLineType.goodsForDropShip, POLineType.goodsForSalesOrder, POLineType.goodsForServiceOrder, POLineType.goodsForReplenishment, POLineType.goodsForManufacturing, POLineType.goodsForProject>>.AsStrings.Contains<POLine.lineType>, Or<SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<POLineType.nonStock, POLineType.nonStockForDropShip, POLineType.nonStockForSalesOrder, POLineType.nonStockForServiceOrder, POLineType.service, POLineType.nonStockForManufacturing, POLineType.nonStockForProject>>.AsStrings.Contains<POLine.lineType>>>, And<POLine.completed, NotEqual<True>>>>>>
    {
    }
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.lineType>
  {
  }

  public abstract class processNonStockAsServiceViaPR : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POLine.processNonStockAsServiceViaPR>
  {
  }

  public abstract class accrueCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.accrueCost>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POLine.planID>
  {
  }

  public abstract class clearPlanID : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.clearPlanID>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.pOLineNbr>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.vendorID>
  {
  }

  public abstract class vendorLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.vendorLocationID>
  {
  }

  public abstract class shipToBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.shipToBAccountID>
  {
  }

  public abstract class shipToLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.shipToLocationID>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLine.orderDate>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.siteID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.lotSerialNbr>
  {
  }

  public abstract class bLType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.bLType>
  {
  }

  public abstract class bLOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.bLOrderNbr>
  {
  }

  public abstract class bLLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.bLLineNbr>
  {
  }

  public abstract class rQReqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.rQReqNbr>
  {
  }

  public abstract class rQReqLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.rQReqLineNbr>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.uOM>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.orderQty>
  {
  }

  public abstract class origOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.origOrderQty>
  {
  }

  public abstract class baseOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.baseOrderQty>
  {
  }

  public abstract class orderedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.orderedQty>
  {
  }

  public abstract class baseOrderedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.baseOrderedQty>
  {
  }

  public abstract class receivedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.receivedQty>
  {
  }

  public abstract class baseReceivedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.baseReceivedQty>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POLine.curyInfoID>
  {
  }

  public abstract class curyUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.curyUnitCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.unitCost>
  {
  }

  public abstract class calculateDiscountsOnImport : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POLine.calculateDiscountsOnImport>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.discPct>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.discAmt>
  {
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.manualPrice>
  {
  }

  public abstract class manualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.manualDisc>
  {
  }

  public abstract class curyLineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.curyLineAmt>
  {
  }

  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.lineAmt>
  {
  }

  public abstract class curyDiscCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.curyDiscCost>
  {
  }

  public abstract class discCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.discCost>
  {
  }

  public abstract class retainagePct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.retainagePct>
  {
  }

  public abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine.curyRetainageAmt>
  {
  }

  public abstract class retainageAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.retainageAmt>
  {
  }

  public abstract class curyExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.extCost>
  {
  }

  public abstract class origExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.origExtCost>
  {
  }

  public abstract class discountsAppliedToLine : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    POLine.discountsAppliedToLine>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine.documentDiscountRate>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.taxCategoryID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.taxID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.taskID>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.expenseAcctID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.expenseSubID>
  {
  }

  public abstract class pOAccrualAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.pOAccrualAcctID>
  {
  }

  public abstract class pOAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.pOAccrualSubID>
  {
  }

  public abstract class alternateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.alternateID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.tranDesc>
  {
  }

  public abstract class unitWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.unitWeight>
  {
  }

  public abstract class unitVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.unitVolume>
  {
  }

  public abstract class extWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.extWeight>
  {
  }

  public abstract class extVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.extVolume>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.costCodeID>
  {
  }

  public abstract class commitmentID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLine.commitmentID>
  {
  }

  public abstract class reasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.reasonCode>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLine.noteID>
  {
  }

  public abstract class rcptQtyMin : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.rcptQtyMin>
  {
  }

  public abstract class rcptQtyMax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.rcptQtyMax>
  {
  }

  public abstract class rcptQtyThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine.rcptQtyThreshold>
  {
  }

  public abstract class rcptQtyAction : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.rcptQtyAction>
  {
  }

  public abstract class receiptStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.receiptStatus>
  {
  }

  public abstract class curyBLOrderedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine.curyBLOrderedCost>
  {
  }

  public abstract class bLOrderedCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.bLOrderedCost>
  {
  }

  public abstract class dRTermStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLine.dRTermStartDate>
  {
  }

  public abstract class dRTermEndDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLine.dRTermEndDate>
  {
  }

  public abstract class itemRequiresTerms : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.itemRequiresTerms>
  {
  }

  public abstract class sOOrderStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.sOOrderStatus>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.sOOrderNbr>
  {
  }

  public abstract class sOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.sOLineNbr>
  {
  }

  public abstract class sOLinkActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.sOLinkActive>
  {
  }

  public abstract class isSpecialOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.isSpecialOrder>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine.costCenterID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POLine.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLine.lastModifiedDateTime>
  {
  }

  public abstract class completedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.completedQty>
  {
  }

  public abstract class baseCompletedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine.baseCompletedQty>
  {
  }

  public abstract class billedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.billedQty>
  {
  }

  public abstract class baseBilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.baseBilledQty>
  {
  }

  public abstract class curyBilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.curyBilledAmt>
  {
  }

  public abstract class billedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.billedAmt>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.openQty>
  {
  }

  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.baseOpenQty>
  {
  }

  public abstract class unbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.unbilledQty>
  {
  }

  public abstract class baseUnbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.baseUnbilledQty>
  {
  }

  public abstract class curyUnbilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.curyUnbilledAmt>
  {
  }

  public abstract class unbilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.unbilledAmt>
  {
  }

  public abstract class leftToReceiveQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine.leftToReceiveQty>
  {
  }

  public abstract class leftToReceiveBaseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine.leftToReceiveBaseQty>
  {
  }

  public abstract class nonOrderedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.nonOrderedQty>
  {
  }

  public abstract class reqPrepaidQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.reqPrepaidQty>
  {
  }

  public abstract class baseReqPrepaidQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine.baseReqPrepaidQty>
  {
  }

  public abstract class displayReqPrepaidQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine.displayReqPrepaidQty>
  {
  }

  public abstract class curyReqPrepaidAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine.curyReqPrepaidAmt>
  {
  }

  public abstract class reqPrepaidAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine.reqPrepaidAmt>
  {
  }

  public abstract class requestedDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLine.requestedDate>
  {
  }

  public abstract class promisedDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLine.promisedDate>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.cancelled>
  {
  }

  public abstract class completePOLine : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.completePOLine>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.completed>
  {
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.closed>
  {
  }

  public abstract class pOAccrualType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.pOAccrualType>
  {
  }

  public abstract class orderNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLine.orderNoteID>
  {
  }

  public abstract class allowComplete : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.allowComplete>
  {
  }

  public abstract class isKit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.isKit>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLine.discountSequenceID>
  {
  }

  public abstract class orderedQtyAltered : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.orderedQtyAltered>
  {
  }

  public abstract class overridenUOM : IBqlField, IBqlOperand
  {
  }

  public abstract class overridenQty : IBqlField, IBqlOperand
  {
  }

  public abstract class baseOverridenQty : IBqlField, IBqlOperand
  {
  }

  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public abstract class curyReceivedCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine.curyReceivedCost>
  {
  }

  public abstract class hasInclusiveTaxes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.hasInclusiveTaxes>
  {
  }

  public abstract class allowEditUnitCostInPR : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POLine.allowEditUnitCostInPR>
  {
  }

  public abstract class viewDemandEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.viewDemandEnabled>
  {
  }

  public abstract class sODeleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine.sODeleted>
  {
  }
}
