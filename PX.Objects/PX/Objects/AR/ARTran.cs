// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR.Standalone;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.DR.Descriptor;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.GL.DAC.Abstract;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.RelatedItems;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;
using PX.Objects.TX;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// A line of an accounts receivable invoice or memo. The record
/// contains such information as the inventory item name, price and quantity,
/// line discounts, and tax category. Entities of this type are edited
/// on the Invoices and Memos (AR301000) form, which corresponds
/// to the <see cref="T:PX.Objects.AR.ARInvoiceEntry" /> graph.
/// </summary>
[DebuggerDisplay("LineType={LineType} TranAmt={CuryTranAmt}")]
[PXCacheName("AR Transactions")]
[Serializable]
public class ARTran : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IHasMinGrossProfit,
  IDocumentLine,
  IDocumentTran,
  ISortOrder,
  ILSPrimary,
  ILSMaster,
  IItemPlanMaster,
  IAccountable,
  ISubstitutableLine,
  IItemPlanSource
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected 
  #nullable disable
  string _TranType;
  protected string _RefNbr;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected string _SOOrderType;
  protected string _SOOrderNbr;
  protected int? _SOOrderLineNbr;
  protected string _SOOrderLineOperation;
  protected int? _SOOrderSortOrder;
  protected string _SOShipmentType;
  protected string _SOShipmentNbr;
  protected int? _SOShipmentLineNbr;
  protected int? _CustomerID;
  protected string _LineType;
  protected bool? _IsFree;
  protected int? _ProjectID;
  protected string _PMDeltaOption;
  protected DateTime? _ExpenseDate;
  protected long? _CuryInfoID;
  protected bool? _ManualPrice;
  protected string _TaxID;
  protected string _DeferredCode;
  protected int? _SiteID;
  protected string _UOM;
  protected Decimal? _BaseQty;
  protected Decimal? _UnitCost;
  protected Decimal? _TranCost;
  protected Decimal? _TranCostOrig;
  protected bool? _IsTranCostFinal;
  protected Decimal? _CuryExtPrice;
  protected Decimal? _ExtPrice;
  protected bool? _CalculateDiscountsOnImport;
  protected Decimal? _DiscPct;
  protected Decimal? _CuryDiscAmt;
  protected Decimal? _DiscAmt;
  protected bool? _ManualDisc;
  protected ushort[] _DiscountsAppliedToLine;
  protected Decimal? _OrigGroupDiscountRate;
  protected Decimal? _OrigDocumentDiscountRate;
  protected Decimal? _GroupDiscountRate;
  protected Decimal? _DocumentDiscountRate;
  protected Decimal? _CuryTranAmt;
  protected Decimal? _TranAmt;
  protected bool? _AccrueCost;
  protected string _CostBasis;
  protected Decimal? _CuryAccruedCost;
  protected Decimal? _AccruedCost;
  protected string _OrigDocType;
  protected string _TranClass;
  protected string _DrCr;
  protected DateTime? _OrigInvoiceDate;
  protected string _FinPeriodID;
  protected string _TranDesc;
  protected bool? _Released;
  protected int? _SalesPersonID;
  protected int? _EmployeeID;
  protected Decimal? _CommnPct;
  protected Decimal? _CuryCommnAmt;
  protected Decimal? _CommnAmt;
  protected int? _DefScheduleID;
  protected string _TaxCategoryID;
  protected string _ReasonCode;
  protected int? _AccountID;
  protected int? _SubID;
  protected int? _ExpenseAccrualAccountID;
  protected int? _ExpenseAccrualSubID;
  protected int? _ExpenseAccountID;
  protected int? _ExpenseSubID;
  protected int? _TaskID;
  protected int? _CostCodeID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected Guid? _NoteID;
  protected bool? _Commissionable;
  protected bool? _FreezeManualDisc;
  protected string _DiscountID;
  protected string _DiscountSequenceID;
  protected DateTime? _DRTermStartDate;
  protected DateTime? _DRTermEndDate;
  protected Decimal? _CuryUnitPriceDR;
  protected Decimal? _DiscPctDR;
  protected Decimal? _GroupDiscountAmount;
  protected Decimal? _GrossSalesAmount;
  protected Decimal? _NetSalesAmount;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [Branch(typeof (ARRegister.branchID), null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (ARRegister.docType))]
  [PXUIField]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (ARRegister.refNbr))]
  [PXUIField]
  [PXParent(typeof (Select<ARRegister, Where<ARRegister.docType, Equal<Current<ARTran.tranType>>, And<ARRegister.refNbr, Equal<Current<ARTran.refNbr>>>>>))]
  [PXParent(typeof (Select<PX.Objects.SO.SOInvoice, Where<PX.Objects.SO.SOInvoice.docType, Equal<Current<ARTran.tranType>>, And<PX.Objects.SO.SOInvoice.refNbr, Equal<Current<ARTran.refNbr>>>>>))]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXLineNbr(typeof (ARRegister.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType>), CacheGlobal = true)]
  public virtual string SOOrderType
  {
    get => this._SOOrderType;
    set => this._SOOrderType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<ARTran.sOOrderType>>>>))]
  [CopyLinkToSOInvoice(typeof (PX.Objects.SO.SOInvoice.tranCntr), typeof (ARTran.curyExtPrice), new System.Type[] {typeof (ARTran.sOOrderType), typeof (ARTran.sOOrderNbr)}, new System.Type[] {typeof (PX.Objects.SO.SOInvoice.sOOrderType), typeof (PX.Objects.SO.SOInvoice.sOOrderNbr)})]
  public virtual string SOOrderNbr
  {
    get => this._SOOrderNbr;
    set => this._SOOrderNbr = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Order Line Nbr", Visible = false, Enabled = false)]
  public virtual int? SOOrderLineNbr
  {
    get => this._SOOrderLineNbr;
    set => this._SOOrderLineNbr = value;
  }

  [PXDBString(1, IsFixed = true, InputMask = ">a")]
  public virtual string SOOrderLineOperation
  {
    get => this._SOOrderLineOperation;
    set => this._SOOrderLineOperation = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Order Sort Order", Visible = false, Enabled = false)]
  [PXDefault]
  public virtual int? SOOrderSortOrder
  {
    get => this._SOOrderSortOrder;
    set => this._SOOrderSortOrder = value;
  }

  [PXDBShort]
  public virtual short? SOOrderLineSign { get; set; }

  [PXDBString(1, IsFixed = true)]
  public virtual string SOShipmentType
  {
    get => this._SOShipmentType;
    set => this._SOShipmentType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Shipment Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.Navigate.SOOrderShipment.shipmentNbr, Where<PX.Objects.SO.Navigate.SOOrderShipment.orderType, Equal<Current<ARTran.sOOrderType>>, And<PX.Objects.SO.Navigate.SOOrderShipment.orderNbr, Equal<Current<ARTran.sOOrderNbr>>, And<PX.Objects.SO.Navigate.SOOrderShipment.shipmentType, Equal<Current<ARTran.sOShipmentType>>>>>>))]
  public virtual string SOShipmentNbr
  {
    get => this._SOShipmentNbr;
    set => this._SOShipmentNbr = value;
  }

  /// <summary>
  /// Number of the group of shipment lines referencing the same <see cref="T:PX.Objects.SO.SOLine">sales order line</see> which
  /// are invoiced together in the single <see cref="T:PX.Objects.AR.ARTran">invoice line</see>.
  /// </summary>
  [PXDBInt]
  public virtual int? SOShipmentLineGroupNbr { get; set; }

  [PXDBInt]
  public virtual int? SOShipmentLineNbr
  {
    get => this._SOShipmentLineNbr;
    set => this._SOShipmentLineNbr = value;
  }

  [Customer]
  [PXDBDefault(typeof (ARRegister.customerID))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Line Type", Visible = false, Enabled = false)]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXBool]
  public virtual bool? IsFree
  {
    get => new bool?(this._IsFree.GetValueOrDefault());
    set => this._IsFree = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Project")]
  [PXSelector(typeof (PMProject.contractID), SubstituteKey = typeof (PMProject.contractCD), ValidateValue = false)]
  [PXDefault(typeof (ARInvoice.projectID))]
  [PXForeignReference(typeof (Field<ARTran.projectID>.IsRelatedTo<PMProject.contractID>))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDefault("C")]
  [PXDBString]
  public virtual string PMDeltaOption
  {
    get => this._PMDeltaOption;
    set => this._PMDeltaOption = value;
  }

  [PXDBDate]
  public virtual DateTime? ExpenseDate
  {
    get => this._ExpenseDate;
    set => this._ExpenseDate = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (ARRegister.curyInfoID), Required = true)]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Price", Visible = false)]
  public virtual bool? ManualPrice
  {
    get => this._ManualPrice;
    set => this._ManualPrice = value;
  }

  [PXDBShort]
  [PXDefault(typeof (short0))]
  [PXUIField(DisplayName = "Multiplier")]
  public virtual short? InvtMult { get; set; }

  [PXDBBool]
  [PXUIField]
  public virtual bool? IsStockItem { get; set; }

  [ARTranInventoryItem(Filterable = true)]
  [PXForeignReference(typeof (Field<ARTran.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  [ConvertedInventoryItem(typeof (ARTran.isStockItem))]
  public virtual int? InventoryID { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax ID", Visible = false)]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Deferral Code")]
  [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID, Where<DRDeferredCode.accountType, Equal<DeferredAccountType.income>>>))]
  [PXRestrictor(typeof (Where<DRDeferredCode.active, Equal<True>>), "The {0} deferral code is deactivated on the Deferral Codes (DR202000) form.", new System.Type[] {typeof (DRDeferredCode.deferredCodeID)})]
  [PXDefault(typeof (Search2<PX.Objects.IN.InventoryItem.deferredCode, InnerJoin<DRDeferredCode, On<DRDeferredCode.deferredCodeID, Equal<PX.Objects.IN.InventoryItem.deferredCode>>>, Where<DRDeferredCode.accountType, Equal<DeferredAccountType.income>, And<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<ARTran.inventoryID>>>>>))]
  public virtual string DeferredCode
  {
    get => this._DeferredCode;
    set => this._DeferredCode = value;
  }

  [PXDBInt]
  [PXDimensionSelector("INSITE", typeof (PX.Objects.IN.INSite.siteID), typeof (PX.Objects.IN.INSite.siteCD))]
  [PXForeignReference(typeof (Field<ARTran.siteID>.IsRelatedTo<PX.Objects.IN.INSite.siteID>))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDefault(typeof (Selector<ARTran.inventoryID, PX.Objects.IN.InventoryItem.salesUnit>))]
  [INUnit(typeof (ARTran.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (ARTran.uOM), typeof (ARTran.baseQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? Qty { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseQty
  {
    get => this._BaseQty;
    set => this._BaseQty = value;
  }

  [PXPriceCost]
  [PXDBCalced(typeof (Switch<Case<Where<ARTran.qty, NotEqual<decimal0>>, Div<ARTran.tranCost, ARTran.qty>>, decimal0>), typeof (Decimal))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Cost")]
  public virtual Decimal? TranCost
  {
    get => this._TranCost;
    set => this._TranCost = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Orig. Ext. Cost")]
  public virtual Decimal? TranCostOrig
  {
    get => this._TranCostOrig;
    set => this._TranCostOrig = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsTranCostFinal
  {
    get => this._IsTranCostFinal;
    set => this._IsTranCostFinal = value;
  }

  bool? ILSMaster.IsIntercompany => new bool?(false);

  /// <exclude />
  [PXDBInt]
  [PXDefault(typeof (CostCenter.freeStock))]
  public int? CostCenterID { get; set; }

  [PXDBCurrencyPriceCost(typeof (ARTran.curyInfoID), typeof (ARTran.unitPrice))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitPrice { get; set; }

  [PXDBPriceCost]
  public virtual Decimal? UnitPrice { get; set; }

  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.extPrice))]
  [PXUIField(DisplayName = "Ext. Price")]
  [PXFormula(typeof (Mult<ARTran.qty, ARTran.curyUnitPrice>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExtPrice
  {
    get => this._CuryExtPrice;
    set => this._CuryExtPrice = value;
  }

  [PXDBBaseCury(typeof (ARTran.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtPrice
  {
    get => this._ExtPrice;
    set => this._ExtPrice = value;
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
  public virtual Decimal? DiscPct
  {
    get => this._DiscPct;
    set => this._DiscPct = value;
  }

  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.discAmt))]
  [PXUIField(DisplayName = "Discount Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscAmt
  {
    get => this._CuryDiscAmt;
    set => this._CuryDiscAmt = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt
  {
    get => this._DiscAmt;
    set => this._DiscAmt = value;
  }

  [ManualDiscountMode(typeof (ARTran.curyDiscAmt), typeof (ARTran.curyTranAmt), typeof (ARTran.discPct), typeof (ARTran.freezeManualDisc), DiscountFeatureType.CustomerDiscount)]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ManualDisc
  {
    get => this._ManualDisc;
    set => this._ManualDisc = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the system does not need to calculate discounts, because they are
  /// already calculated.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Automatic Discounts Disabled", Visible = false, Enabled = false)]
  public virtual bool? AutomaticDiscountsDisabled { get; set; }

  [PXDBPackedIntegerArray]
  public virtual ushort[] DiscountsAppliedToLine
  {
    get => this._DiscountsAppliedToLine;
    set => this._DiscountsAppliedToLine = value;
  }

  [PXDBInt]
  public virtual int? OrigLineNbr { get; set; }

  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? OrigGroupDiscountRate
  {
    get => this._OrigGroupDiscountRate;
    set => this._OrigGroupDiscountRate = value;
  }

  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? OrigDocumentDiscountRate
  {
    get => this._OrigDocumentDiscountRate;
    set => this._OrigDocumentDiscountRate = value;
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

  /// <summary>
  /// Indicates (if selected) that the automatic line discounts are not applied to this line
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ignore Automatic Line Discounts", Visible = false)]
  public virtual bool? SkipLineDiscounts { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? RetainagePct { get; set; }

  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.retainageAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryRetainageAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageAmt { get; set; }

  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.tranAmt))]
  [PXUIField]
  [PXFormula(typeof (Sub<ARTran.curyExtPrice, Add<ARTran.curyDiscAmt, ARTran.curyRetainageAmt>>))]
  [PXFormula(null, typeof (CountCalc<ARSalesPerTran.refCntr>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranAmt
  {
    get => this._CuryTranAmt;
    set => this._CuryTranAmt = value;
  }

  [PXDBBaseCury(typeof (ARTran.branchID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TranAmt
  {
    get => this._TranAmt;
    set => this._TranAmt = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that cost will be processed using expense accrual account.
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (BqlOperand<IsNull<Selector<ARTran.inventoryID, PX.Objects.IN.InventoryItem.postToExpenseAccount>, PX.Objects.IN.InventoryItem.postToExpenseAccount.purchases>, IBqlString>.IsEqual<PX.Objects.IN.InventoryItem.postToExpenseAccount.sales>))]
  [PXUIField(DisplayName = "Accrue Cost", Enabled = false, Visible = false)]
  public virtual bool? AccrueCost
  {
    get => this._AccrueCost;
    set => this._AccrueCost = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (IsNull<Selector<ARTran.inventoryID, PX.Objects.IN.InventoryItem.costBasis>, CostBasisOption.undefinedCostBasis>))]
  [CostBasisOption.List]
  public virtual string CostBasis
  {
    get => this._CostBasis;
    set => this._CostBasis = value;
  }

  [PXString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Cost Based On", Enabled = false, Visible = false)]
  [CostBasisOption.List]
  [PXFormula(typeof (IIf<BqlOperand<ARTran.costBasis, IBqlString>.IsEqual<CostBasisOption.undefinedCostBasis>, Null, ARTran.costBasis>))]
  public virtual string CostBasisNull { get; set; }

  [PXInt]
  [PXSelector(typeof (Search<InventoryItemCurySettings.inventoryID, Where<InventoryItemCurySettings.inventoryID, Equal<BqlField<ARTran.inventoryID, IBqlInt>.FromCurrent>, And<InventoryItemCurySettings.curyID, Equal<Current<AccessInfo.baseCuryID>>>>>), ValidateValue = false)]
  [PXFormula(typeof (ARTran.inventoryID))]
  public int? CuryInventoryID { get; set; }

  [PXDBCurrencyPriceCost(typeof (ARTran.curyInfoID), typeof (ARTran.accruedCost))]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where<ARTran.accrueCost, Equal<True>, And<ARTran.costBasis, Equal<CostBasisOption.percentOfSalesPrice>>>, Mult<ARTran.curyTranAmt, Div<Selector<ARTran.inventoryID, PX.Objects.IN.InventoryItem.percentOfSalesPrice>, decimal100>>, Case<Where<ARTran.accrueCost, Equal<True>, And<ARTran.costBasis, Equal<CostBasisOption.priceMarkupPercent>>>, Div<Mult<ARTran.curyTranAmt, decimal100>, Add<decimal100, Selector<ARTran.inventoryID, PX.Objects.IN.InventoryItem.markupPct>>>>>, decimal0>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryAccruedCost
  {
    get => this._CuryAccruedCost;
    set => this._CuryAccruedCost = value;
  }

  [PXDBBaseCury]
  [PXFormula(typeof (Switch<Case<Where<ARTran.accrueCost, Equal<True>, And<ARTran.costBasis, Equal<CostBasisOption.standardCost>>>, Mult<ARTran.baseQty, Selector<ARTran.curyInventoryID, InventoryItemCurySettings.stdCost>>>, decimal0>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AccruedCost
  {
    get => this._AccruedCost;
    set => this._AccruedCost = value;
  }

  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Net Amount", Enabled = false, FieldClass = "PaymentsByLines")]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxableAmt { get; set; }

  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.taxAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryTaxAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxAmt { get; set; }

  /// <summary>
  /// The line amount included into line balance.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AR.ARRegister.CuryID" />)
  /// </summary>
  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.origTaxableAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Taxable Amount", Enabled = false, FieldClass = "PaymentsByLines")]
  public virtual Decimal? CuryOrigTaxableAmt { get; set; }

  /// <summary>
  /// The line amount included into line balance.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigTaxableAmt { get; set; }

  /// <summary>
  /// The amount of tax included into line balance.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AR.ARRegister.CuryID" />)
  /// </summary>
  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.origTaxAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryOrigTaxAmt { get; set; }

  /// <summary>
  /// The amount of tax included into line balance.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigTaxAmt { get; set; }

  /// <summary>
  /// The line amount that is subject to retained tax.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AR.ARRegister.CuryID" />)
  /// </summary>
  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.retainedTaxableAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Retained Taxable Amount", Enabled = false, FieldClass = "PaymentsByLines")]
  public virtual Decimal? CuryRetainedTaxableAmt { get; set; }

  /// <summary>
  /// The line amount that is subject to retained tax.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainedTaxableAmt { get; set; }

  /// <summary>
  /// The amount of retained tax (VAT) associated with the line.
  /// (Presented in the currency of the document, see <see cref="P:PX.Objects.AR.ARRegister.CuryID" />)
  /// </summary>
  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.retainedTaxAmt), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryRetainedTaxAmt { get; set; }

  /// <summary>
  /// The amount of retained tax (VAT) associated with the line.
  /// (Presented in the base currency of the company, see <see cref="P:PX.Objects.GL.Company.BaseCuryID" />)
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainedTaxAmt { get; set; }

  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.cashDiscBal), BaseCalc = false)]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryCashDiscBal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CashDiscBal { get; set; }

  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.origRetainageAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryOrigRetainageAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigRetainageAmt { get; set; }

  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.retainageBal), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryRetainageBal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? RetainageBal { get; set; }

  /// <summary>The type of the original (source) document.</summary>
  /// <value>
  /// Corresponds to the <see cref="!:DocType" /> field.
  /// </value>
  [PXDBString(3, IsFixed = true)]
  [ARDocType.List]
  [PXUIField]
  public virtual string OrigDocType
  {
    get => this._OrigDocType;
    set => this._OrigDocType = value;
  }

  /// <summary>
  /// The reference number of the original (source) document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.AR.ARTran.RefNbr" /> field.
  /// </value>
  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField]
  public virtual string OrigRefNbr { get; set; }

  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.origTranAmt), BaseCalc = false)]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrigTranAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigTranAmt { get; set; }

  [PXDBCurrency(typeof (ARTran.curyInfoID), typeof (ARTran.tranBal), BaseCalc = false)]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTranBal { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Balance", Enabled = false)]
  public virtual Decimal? TranBal { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("")]
  public virtual string TranClass
  {
    get => this._TranClass;
    set => this._TranClass = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (ARInvoice.drCr))]
  public virtual string DrCr
  {
    get => this._DrCr;
    set => this._DrCr = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (ARRegister.docDate))]
  [PXUIField(DisplayName = "Document Date", Visible = false)]
  public virtual DateTime? TranDate { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Original Invoice date")]
  public virtual DateTime? OrigInvoiceDate
  {
    get => this._OrigInvoiceDate;
    set => this._OrigInvoiceDate = value;
  }

  [PX.Objects.GL.FinPeriodID(null, typeof (ARTran.branchID), null, null, null, null, true, false, null, typeof (ARTran.tranPeriodID), typeof (ARRegister.tranPeriodID), true, true)]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  /// <summary>
  /// When set, on persist checks, that the document has the corresponded <see cref="P:PX.Objects.AR.ARTran.Released" /> original value.
  /// When not set, on persist checks, that <see cref="P:PX.Objects.AR.ARTran.Released" /> value is not changed.
  /// Throws an error otherwise.
  /// </summary>
  [PXDBRestrictionBool(typeof (ARTran.released))]
  public virtual bool? ReleasedToVerify { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [SalesPerson]
  [PXParent(typeof (Select<ARSalesPerTran, Where<ARSalesPerTran.docType, Equal<Current<ARTran.tranType>>, And<ARSalesPerTran.refNbr, Equal<Current<ARTran.refNbr>>, And<ARSalesPerTran.salespersonID, Equal<Current2<ARTran.salesPersonID>>, And<Current<ARTran.commissionable>, Equal<True>>>>>>), LeaveChildren = true, ParentCreate = true)]
  [PXDefault(typeof (ARRegister.salesPersonID))]
  [PXFormula(typeof (Switch<Case<Where<ARTran.lineType, Equal<SOLineType.freight>>, Null>, ARTran.salesPersonID>))]
  [PXForeignReference(typeof (Field<ARTran.salesPersonID>.IsRelatedTo<SalesPerson.salesPersonID>))]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  [PXDBInt]
  [PXDefault(typeof (Search<EPEmployee.bAccountID, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnPct
  {
    get => this._CommnPct;
    set => this._CommnPct = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryCommnAmt
  {
    get => this._CuryCommnAmt;
    set => this._CuryCommnAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CommnAmt
  {
    get => this._CommnAmt;
    set => this._CommnAmt = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Original Deferral Schedule")]
  [PXSelector(typeof (Search<DRSchedule.scheduleID, Where<DRSchedule.bAccountID, Equal<Current<ARInvoice.customerID>>, And<Where2<Where<Current<ARTran.tranType>, NotEqual<ARDocType.creditMemo>, And<Current<ARTran.tranType>, NotEqual<ARDocType.debitMemo>>>, Or<Where<DRSchedule.docType, NotEqual<Current<ARTran.tranType>>, And<Where<Current<ARTran.tranType>, Equal<ARDocType.creditMemo>, Or<Current<ARTran.tranType>, Equal<ARDocType.debitMemo>>>>>>>>>>), SubstituteKey = typeof (DRSchedule.scheduleNbr))]
  public virtual int? DefScheduleID
  {
    get => this._DefScheduleID;
    set => this._DefScheduleID = value;
  }

  [PXDBBool]
  [PXDefault(false, typeof (ARInvoice.disableAutomaticTaxCalculation))]
  [PXUIField(DisplayName = "Disable Automatic Tax Calculation", Visible = false, Enabled = false)]
  public virtual bool? DisableAutomaticTaxCalculation { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Tax Category")]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Selector<ARTran.inventoryID, PX.Objects.IN.InventoryItem.taxCategoryID>))]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  [PXDefault("0", typeof (ARInvoice.avalaraCustomerUsageType))]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string AvalaraCustomerUsageType { get; set; }

  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.sales>, Or<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.issue>>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXUIField(DisplayName = "Reason Code")]
  [PXForeignReference(typeof (Field<ARTran.reasonCode>.IsRelatedTo<PX.Objects.CS.ReasonCode.reasonCodeID>))]
  public virtual string ReasonCode
  {
    get => this._ReasonCode;
    set => this._ReasonCode = value;
  }

  [PXString]
  [PXFormula(typeof (Switch<Case<Where<Current2<ARPayment.refNbr>, IsNotNull>, ControlAccountModule.any, Case<Where<Current<ARPayment.refNbr>, Equal<Current<ARTran.refNbr>>>, ControlAccountModule.any, Case<Where<Current<ARInvoice.masterRefNbr>, IsNotNull, And<Current<ARInvoice.installmentNbr>, IsNotNull>>, ControlAccountModule.any, Case<Where<Current<ARInvoice.isRetainageDocument>, Equal<True>>, ControlAccountModule.aR>>>>, Empty>))]
  public virtual string AllowControlAccountForModule { get; set; }

  [Account(typeof (ARTran.branchID))]
  [PXDefault(typeof (Coalesce<Search<PX.Objects.IN.InventoryItem.salesAcctID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<ARTran.inventoryID>>>>, Search<PX.Objects.CR.Location.cSalesAcctID, Where<PX.Objects.CR.Location.locationID, Equal<Current<ARRegister.customerLocationID>>>>>))]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(typeof (ARTran.accountID), typeof (ARTran.branchID), true)]
  [PXDefault(typeof (Selector<ARTran.inventoryID, PX.Objects.IN.InventoryItem.salesSubID>))]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [Account(typeof (ARTran.branchID))]
  [PXDefault]
  public virtual int? ExpenseAccrualAccountID
  {
    get => this._ExpenseAccrualAccountID;
    set => this._ExpenseAccrualAccountID = value;
  }

  [SubAccount(typeof (ARTran.expenseAccrualAccountID), typeof (ARTran.branchID), true)]
  [PXDefault]
  public virtual int? ExpenseAccrualSubID
  {
    get => this._ExpenseAccrualSubID;
    set => this._ExpenseAccrualSubID = value;
  }

  [Account(typeof (ARTran.branchID))]
  [PXDefault]
  public virtual int? ExpenseAccountID
  {
    get => this._ExpenseAccountID;
    set => this._ExpenseAccountID = value;
  }

  [SubAccount(typeof (ARTran.expenseAccountID), typeof (ARTran.branchID), true)]
  [PXDefault]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  [PXDefault(typeof (Coalesce<Search<PMAccountTask.taskID, Where<PMAccountTask.projectID, Equal<Current<ARTran.projectID>>, And<PMAccountTask.accountID, Equal<Current<ARTran.accountID>>>>>, Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<ARTran.projectID>>, And<PMTask.isDefault, Equal<True>, And<PMTask.type, NotEqual<ProjectTaskType.cost>>>>>>))]
  [ActiveProjectTask(typeof (ARTran.projectID), "AR", DisplayName = "Project Task", AlwaysVisibleIfProjectType = true)]
  [PXForeignReference(typeof (CompositeKey<Field<ARTran.projectID>.IsRelatedTo<PMTask.projectID>, Field<ARTran.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [CostCode(typeof (ARTran.accountID), typeof (ARTran.taskID), "I")]
  [PXForeignReference(typeof (Field<ARTran.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

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

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBBool]
  [PXFormula(typeof (Switch<Case<Where<ARTran.inventoryID, IsNotNull>, Selector<ARTran.inventoryID, PX.Objects.IN.InventoryItem.commisionable>>, True>))]
  [PXUIField(DisplayName = "Commissionable")]
  public bool? Commissionable
  {
    get => this._Commissionable;
    set => this._Commissionable = value;
  }

  /// <summary>
  /// Reference Date. May be an original expense date that is billed to the customer.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Expense Date", Visible = false)]
  public virtual DateTime? Date { get; set; }

  /// <exclude />
  [PXSelector(typeof (Search<CRCase.caseCD>))]
  [PXDBString(15)]
  [PXUIField(DisplayName = "Case ID", Visible = false, Enabled = false)]
  public virtual string CaseCD { get; set; }

  [PXBool]
  public bool? RequireINUpdate { get; set; }

  [PXBool]
  public virtual bool? FreezeManualDisc
  {
    get => this._FreezeManualDisc;
    set => this._FreezeManualDisc = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, Equal<DiscountType.LineDiscount>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouse>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomer>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndCustomerPrice>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventory>, And<ARDiscount.applicableTo, NotEqual<DiscountTarget.warehouseAndInventoryPrice>>>>>>>>))]
  [PXUIField(DisplayName = "Discount Code", Visible = true, Enabled = true)]
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

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Term Start Date")]
  public DateTime? DRTermStartDate
  {
    get => this._DRTermStartDate;
    set => this._DRTermStartDate = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Term End Date")]
  public DateTime? DRTermEndDate
  {
    get => this._DRTermEndDate;
    set => this._DRTermEndDate = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the <see cref="P:PX.Objects.AR.ARTran.DRTermStartDate" /> and <see cref="P:PX.Objects.AR.ARTran.DRTermEndDate" />
  /// fields are enabled and should be filled for the line.
  /// </summary>
  /// <value>
  /// The value of this field is set by the <see cref="T:PX.Objects.AR.ARInvoiceEntry" /> and <see cref="T:PX.Objects.AR.ARCashSaleEntry" /> graphs
  /// based on the settings of the <see cref="P:PX.Objects.AR.ARTran.InventoryID">item</see> and the <see cref="P:PX.Objects.AR.ARTran.DeferredCode">Deferral Code</see> selected
  /// for the line. In other contexts it is not populated.
  /// See the attribute on the <see cref="M:PX.Objects.AR.ARInvoiceEntry.ARTran_RequiresTerms_CacheAttached(PX.Data.PXCache)" /> handler for details.
  /// </value>
  [PXBool]
  public virtual bool? RequiresTerms { get; set; }

  [PXUIField(DisplayName = "Unit Price for DR", Visible = false)]
  [PXDBDecimal(typeof (Search<CommonSetup.decPlPrcCst>))]
  public virtual Decimal? CuryUnitPriceDR
  {
    get => this._CuryUnitPriceDR;
    set => this._CuryUnitPriceDR = value;
  }

  [PXUIField(DisplayName = "Discount Percent for DR", Visible = false)]
  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0)]
  public virtual Decimal? DiscPctDR
  {
    get => this._DiscPctDR;
    set => this._DiscPctDR = value;
  }

  [PXBool]
  [DRTerms.VerifyResidual(typeof (ARTran.inventoryID), typeof (ARTran.deferredCode), typeof (ARTran.curyUnitPriceDR), typeof (ARTran.curyExtPrice))]
  public virtual bool? ItemHasResidual { get; set; }

  [PXUIField(DisplayName = "Group Discount Amount", Enabled = false)]
  [PXDecimal]
  [PXDBCalced(typeof (Add<Mult<ARTran.tranAmt, Sub<decimal1, ARTran.groupDiscountRate>>, Mult<ARTran.tranAmt, Sub<decimal1, ARTran.origGroupDiscountRate>>>), typeof (Decimal))]
  public virtual Decimal? GroupDiscountAmount
  {
    get => this._GroupDiscountAmount;
    set => this._GroupDiscountAmount = value;
  }

  [PXUIField(DisplayName = "Document Discount Amount", Enabled = false)]
  [PXDecimal]
  [PXDBCalced(typeof (Add<Mult<Sub<ARTran.tranAmt, ARTran.groupDiscountAmount>, Sub<decimal1, ARTran.documentDiscountRate>>, Mult<Sub<ARTran.tranAmt, ARTran.groupDiscountAmount>, Sub<decimal1, ARTran.origDocumentDiscountRate>>>), typeof (Decimal))]
  public virtual Decimal? DocumentDiscountAmount { get; set; }

  [PXUIField(DisplayName = "Gross Sales Amount", Enabled = false)]
  [PXDecimal]
  [PXDBCalced(typeof (Add<ARTran.discAmt, Switch<Case<Where<ARTran.taxableAmt, Equal<decimal0>>, ARTran.tranAmt>, ARTran.taxableAmt>>), typeof (Decimal))]
  public virtual Decimal? GrossSalesAmount
  {
    get => this._GrossSalesAmount;
    set => this._GrossSalesAmount = value;
  }

  [PXUIField(DisplayName = "Cost", Enabled = false)]
  [PXDecimal]
  [PXDBCalced(typeof (Mult<Switch<Case<Where<ARTran.drCr, Equal<PX.Objects.GL.DrCr.debit>>, Minus<decimal1>>, decimal1>, Switch<Case<Where<ARTran.isTranCostFinal, Equal<False>>, ARTran.tranCostOrig>, ARTran.tranCost>>), typeof (Decimal))]
  public virtual Decimal? Cost { get; set; }

  [PXUIField(DisplayName = "Net Sales Amount", Enabled = false)]
  [PXDecimal]
  [PXDBCalced(typeof (Mult<Switch<Case<Where<ARTran.drCr, Equal<PX.Objects.GL.DrCr.debit>>, Minus<decimal1>>, decimal1>, Switch<Case<Where<ARTran.taxableAmt, Equal<decimal0>>, Sub<Sub<Sub<ARTran.grossSalesAmount, ARTran.discAmt>, ARTran.groupDiscountAmount>, ARTran.documentDiscountAmount>>, ARTran.taxableAmt>>), typeof (Decimal))]
  public virtual Decimal? NetSalesAmount
  {
    get => this._NetSalesAmount;
    set => this._NetSalesAmount = value;
  }

  [PXUIField(DisplayName = "Margin", Enabled = false)]
  [PXDecimal]
  [PXDBCalced(typeof (Sub<ARTran.netSalesAmount, ARTran.cost>), typeof (Decimal), CastToPrecision = 25, CastToScale = 9)]
  public virtual Decimal? Margin { get; set; }

  [PXUIField(DisplayName = "Margin Percent", Enabled = false)]
  [PXDecimal]
  [PXDBCalced(typeof (Switch<Case<Where<ARTran.netSalesAmount, NotEqual<decimal0>>, Mult<Div<ARTran.margin, ARTran.netSalesAmount>, decimal100>>, decimal0>), typeof (Decimal))]
  public virtual Decimal? MarginPercent { get; set; }

  string IDocumentLine.Module => "AR";

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<ARTran.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [SubItem(typeof (ARTran.inventoryID), typeof (LeftJoin<INSiteStatusByCostCenter, On<INSiteStatusByCostCenter.subItemID, Equal<INSubItem.subItemID>, And<INSiteStatusByCostCenter.inventoryID, Equal<Optional<ARTran.inventoryID>>, And<INSiteStatusByCostCenter.siteID, Equal<Optional<ARTran.siteID>>, And<INSiteStatusByCostCenter.costCenterID, Equal<CostCenter.freeStock>>>>>>))]
  [PXFormula(typeof (Default<ARTran.inventoryID>))]
  public virtual int? SubItemID { get; set; }

  [LocationAvail(typeof (ARTran.inventoryID), typeof (ARTran.subItemID), typeof (CostCenter.freeStock), typeof (ARTran.siteID), typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTran.tranType, In3<ARDocType.invoice, ARDocType.debitMemo, ARDocType.cashSale>>>>>.And<BqlOperand<ARTran.invtMult, IBqlShort>.IsIn<short1, shortMinus1>>>), typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARTran.tranType, In3<ARDocType.creditMemo, ARDocType.cashReturn>>>>>.And<BqlOperand<ARTran.invtMult, IBqlShort>.IsIn<short1, shortMinus1>>>), typeof (Where<False>))]
  [PXDefault]
  public virtual int? LocationID { get; set; }

  [SOInvoiceLineSplittingExtension.ARLotSerialNbr(typeof (ARTran.inventoryID), typeof (ARTran.subItemID), typeof (ARTran.locationID))]
  public virtual string LotSerialNbr { get; set; }

  [SOInvoiceLineSplittingExtension.ARExpireDate(typeof (ARTran.inventoryID))]
  public virtual DateTime? ExpireDate { get; set; }

  [PXDecimal(6)]
  [PXFormula(typeof (decimal0))]
  public virtual Decimal? UnassignedQty { get; set; }

  [PXDBLong(IsImmutable = true)]
  public virtual long? PlanID { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXUIField(DisplayName = "Orig. Inv. Type", Enabled = false)]
  [ARDocType.List]
  public virtual string OrigInvoiceType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Orig. Inv. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOInvoice.refNbr, Where<PX.Objects.SO.SOInvoice.docType, Equal<Current<ARTran.origInvoiceType>>>>))]
  public virtual string OrigInvoiceNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Orig. Inv. Line Nbr.", Enabled = false, Visible = false)]
  public virtual int? OrigInvoiceLineNbr { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Inventory Doc. Type", Enabled = false)]
  [INDocType.List]
  public virtual string InvtDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Inventory Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<Current<ARTran.invtDocType>>>>))]
  public virtual string InvtRefNbr { get; set; }

  /// <exclude />
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? InvtReleased { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the invoice is a cancellation invoice (credit memo).
  /// </summary>
  [PXDBBool]
  [PXDefault(typeof (ARRegister.isCancellation))]
  public virtual bool? IsCancellation { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the invoice was canceled or corrected.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Canceled { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Substitution Required")]
  public virtual bool? SubstitutionRequired { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual string BlanketType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Blanket SO Ref. Nbr.", Enabled = false, Visible = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<ARTran.blanketType>>>>), ValidateValue = false)]
  public virtual string BlanketNbr { get; set; }

  [PXDBInt]
  public virtual int? BlanketLineNbr { get; set; }

  [PXDBInt]
  public virtual int? BlanketSplitLineNbr { get; set; }

  /// <exclude />
  public bool? SkipLineDiscountsBuffer { get; set; }

  /// <summary>
  /// Determines whether the <i>View Deferrals</i> grid action should be enabled or disabled on the <see cref="T:PX.Objects.AR.ARInvoiceEntry">AR301000</see> screen
  /// </summary>
  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<Exists<Select<DRSchedule, Where<DRSchedule.module, Equal<BatchModule.moduleAR>, And<DRSchedule.docType, Equal<ARTran.tranType>, And<DRSchedule.refNbr, Equal<ARTran.refNbr>, And<DRSchedule.lineNbr, Equal<ARTran.lineNbr>>>>>>>>, True, Case<Where<ARTran.deferredCode, IsNotNull>, True>>, False>), typeof (bool))]
  [PXUIField]
  public virtual bool? CanViewDeferralSchedule { get; set; }

  public class PK : PrimaryKeyOf<ARTran>.By<ARTran.tranType, ARTran.refNbr, ARTran.lineNbr>
  {
    public static ARTran Find(
      PXGraph graph,
      string tranType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (ARTran) PrimaryKeyOf<ARTran>.By<ARTran.tranType, ARTran.refNbr, ARTran.lineNbr>.FindBy(graph, (object) tranType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Document : 
      PrimaryKeyOf<ARRegister>.By<ARRegister.docType, ARRegister.refNbr>.ForeignKeyOf<ARTran>.By<ARTran.tranType, ARTran.refNbr>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<ARInvoice>.By<ARInvoice.docType, ARInvoice.refNbr>.ForeignKeyOf<ARTran>.By<ARTran.tranType, ARTran.refNbr>
    {
    }

    public class Payment : 
      PrimaryKeyOf<ARPayment>.By<ARPayment.docType, ARPayment.refNbr>.ForeignKeyOf<ARTran>.By<ARTran.tranType, ARTran.refNbr>
    {
    }

    public class CashSale : 
      PrimaryKeyOf<ARCashSale>.By<ARCashSale.docType, ARCashSale.refNbr>.ForeignKeyOf<ARTran>.By<ARTran.tranType, ARTran.refNbr>
    {
    }

    public class SOInvoice : 
      PrimaryKeyOf<PX.Objects.SO.SOInvoice>.By<PX.Objects.SO.SOInvoice.docType, PX.Objects.SO.SOInvoice.refNbr>.ForeignKeyOf<ARTran>.By<ARTran.tranType, ARTran.refNbr>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARTran>.By<ARTran.branchID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<ARTran>.By<ARTran.curyInfoID>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<ARTran>.By<ARTran.taxID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<ARTran>.By<ARTran.taxCategoryID>
    {
    }

    public class SOOrder : 
      PrimaryKeyOf<PX.Objects.SO.SOOrder>.By<PX.Objects.SO.SOOrder.orderType, PX.Objects.SO.SOOrder.orderNbr>.ForeignKeyOf<ARTran>.By<ARTran.sOOrderType, ARTran.sOOrderNbr>
    {
    }

    public class SOOrderLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<ARTran>.By<ARTran.sOOrderType, ARTran.sOOrderNbr, ARTran.sOOrderLineNbr>
    {
    }

    public class SOOrderShipment : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderShipment>.By<PX.Objects.SO.SOOrderShipment.shipmentType, PX.Objects.SO.SOOrderShipment.shipmentNbr, PX.Objects.SO.SOOrderShipment.orderType, PX.Objects.SO.SOOrderShipment.orderNbr>.ForeignKeyOf<ARTran>.By<ARTran.sOShipmentType, ARTran.sOShipmentNbr, ARTran.sOOrderType, ARTran.sOOrderNbr>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARTran>.By<ARTran.customerID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<ARTran>.By<ARTran.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<ARTran>.By<ARTran.subItemID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARTran>.By<ARTran.accountID>
    {
    }

    public class Subaccount : PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARTran>.By<ARTran.subID>
    {
    }

    public class ExpenseAccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARTran>.By<ARTran.expenseAccrualAccountID>
    {
    }

    public class ExpenseAccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARTran>.By<ARTran.expenseAccrualSubID>
    {
    }

    public class ExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARTran>.By<ARTran.expenseAccountID>
    {
    }

    public class ExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARTran>.By<ARTran.expenseSubID>
    {
    }

    public class DeferralCode : 
      PrimaryKeyOf<DRDeferredCode>.By<DRDeferredCode.deferredCodeID>.ForeignKeyOf<ARTran>.By<ARTran.deferredCode>
    {
    }

    public class Site : PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<ARTran>.By<ARTran.siteID>
    {
    }

    [Obsolete("This foreign key is obsolete and is going to be removed in 2021R1. Use SOOrder instead.")]
    public class Order : ARTran.FK.SOOrder
    {
    }

    public class BlanketOrderLink : 
      PrimaryKeyOf<SOBlanketOrderLink>.By<SOBlanketOrderLink.blanketType, SOBlanketOrderLink.blanketNbr, SOBlanketOrderLink.orderType, SOBlanketOrderLink.orderNbr>.ForeignKeyOf<ARTran>.By<ARTran.blanketType, ARTran.blanketNbr, ARTran.sOOrderType, ARTran.sOOrderNbr>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.branchID>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.sortOrder>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.sOOrderNbr>
  {
  }

  public abstract class sOOrderLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.sOOrderLineNbr>
  {
  }

  public abstract class sOOrderLineOperation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTran.sOOrderLineOperation>
  {
  }

  public abstract class soOrderSortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.soOrderSortOrder>
  {
  }

  public abstract class sOOrderLineSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARTran.sOOrderLineSign>
  {
  }

  public abstract class sOShipmentType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.sOShipmentType>
  {
  }

  public abstract class sOShipmentNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.sOShipmentNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.AR.ARTran.SOShipmentLineGroupNbr" />
  public abstract class sOShipmentLineGroupNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARTran.sOShipmentLineGroupNbr>
  {
  }

  public abstract class sOShipmentLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.sOShipmentLineNbr>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.customerID>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.lineType>
  {
  }

  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.isFree>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.projectID>
  {
  }

  public abstract class pMDeltaOption : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.pMDeltaOption>
  {
    public const string CompleteLine = "C";
    public const string BillLater = "U";
  }

  public abstract class expenseDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARTran.expenseDate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARTran.curyInfoID>
  {
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.manualPrice>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  ARTran.invtMult>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.isStockItem>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.inventoryID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.taxID>
  {
  }

  public abstract class deferredCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.deferredCode>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.siteID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.baseQty>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.unitCost>
  {
  }

  /// <summary>
  /// TranCost is calculated as BaseQty * UnitCost for non-stock items and Qty * Unit Cost for stock items.
  /// It could also contain a sum of costs of stock and non-stock items (non stock kit with stock components)
  /// </summary>
  public abstract class tranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.tranCost>
  {
  }

  public abstract class tranCostOrig : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.tranCostOrig>
  {
  }

  public abstract class isTranCostFinal : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.isTranCostFinal>
  {
  }

  /// <exclude />
  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.costCenterID>
  {
  }

  public abstract class curyUnitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyUnitPrice>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.unitPrice>
  {
  }

  public abstract class curyExtPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyExtPrice>
  {
  }

  public abstract class extPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.extPrice>
  {
  }

  public abstract class calculateDiscountsOnImport : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARTran.calculateDiscountsOnImport>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.discPct>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.discAmt>
  {
  }

  public abstract class manualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.manualDisc>
  {
  }

  public abstract class automaticDiscountsDisabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARTran.automaticDiscountsDisabled>
  {
  }

  public abstract class discountsAppliedToLine : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    ARTran.discountsAppliedToLine>
  {
  }

  public abstract class origLineNbr : IBqlField, IBqlOperand
  {
  }

  public abstract class origGroupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.origGroupDiscountRate>
  {
  }

  public abstract class origDocumentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.origDocumentDiscountRate>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.documentDiscountRate>
  {
  }

  public abstract class skipLineDiscounts : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.skipLineDiscounts>
  {
  }

  public abstract class retainagePct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.retainagePct>
  {
  }

  public abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.curyRetainageAmt>
  {
  }

  public abstract class retainageAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.retainageAmt>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.tranAmt>
  {
  }

  public abstract class accrueCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.accrueCost>
  {
  }

  public abstract class costBasis : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.costBasis>
  {
  }

  public abstract class costBasisNull : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.costBasisNull>
  {
  }

  public abstract class curyInventoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.curyInventoryID>
  {
  }

  public abstract class curyAccruedCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyAccruedCost>
  {
  }

  public abstract class accruedCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.accruedCost>
  {
  }

  public abstract class curyTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyTaxableAmt>
  {
  }

  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.taxableAmt>
  {
  }

  public abstract class curyTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyTaxAmt>
  {
  }

  public abstract class taxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.taxAmt>
  {
  }

  public abstract class curyOrigTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.curyOrigTaxableAmt>
  {
  }

  public abstract class origTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.origTaxableAmt>
  {
  }

  public abstract class curyOrigTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyOrigTaxAmt>
  {
  }

  public abstract class origTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.origTaxAmt>
  {
  }

  public abstract class curyRetainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.curyRetainedTaxableAmt>
  {
  }

  public abstract class retainedTaxableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.retainedTaxableAmt>
  {
  }

  public abstract class curyRetainedTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.curyRetainedTaxAmt>
  {
  }

  public abstract class retainedTaxAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.retainedTaxAmt>
  {
  }

  public abstract class curyCashDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyCashDiscBal>
  {
  }

  public abstract class cashDiscBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.cashDiscBal>
  {
  }

  public abstract class curyOrigRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.curyOrigRetainageAmt>
  {
  }

  public abstract class origRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.origRetainageAmt>
  {
  }

  public abstract class curyRetainageBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.curyRetainageBal>
  {
  }

  public abstract class retainageBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.retainageBal>
  {
  }

  public abstract class origDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.origDocType>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.origRefNbr>
  {
  }

  public abstract class curyOrigTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyOrigTranAmt>
  {
  }

  public abstract class origTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.origTranAmt>
  {
  }

  public abstract class curyTranBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyTranBal>
  {
  }

  public abstract class tranBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.tranBal>
  {
  }

  public abstract class tranClass : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.tranClass>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.drCr>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARTran.tranDate>
  {
  }

  public abstract class origInvoiceDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARTran.origInvoiceDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.tranPeriodID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.tranDesc>
  {
  }

  public abstract class releasedToVerify : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.releasedToVerify>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.released>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.salesPersonID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.employeeID>
  {
  }

  public abstract class commnPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.commnPct>
  {
  }

  public abstract class curyCommnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyCommnAmt>
  {
  }

  public abstract class commnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.commnAmt>
  {
  }

  public abstract class defScheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.defScheduleID>
  {
  }

  public abstract class disableAutomaticTaxCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARTran.disableAutomaticTaxCalculation>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.taxCategoryID>
  {
  }

  public abstract class avalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTran.avalaraCustomerUsageType>
  {
  }

  public abstract class reasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.reasonCode>
  {
  }

  public abstract class allowControlAccountForModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTran.allowControlAccountForModule>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.subID>
  {
  }

  public abstract class expenseAccrualAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARTran.expenseAccrualAccountID>
  {
  }

  public abstract class expenseAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.expenseAccrualSubID>
  {
  }

  public abstract class expenseAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.expenseAccountID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.expenseSubID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.costCodeID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARTran.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARTran.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARTran.noteID>
  {
  }

  public abstract class commissionable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.commissionable>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARTran.date>
  {
  }

  /// <exclude />
  public abstract class caseCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.caseCD>
  {
  }

  public abstract class requireINUpdate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.requireINUpdate>
  {
  }

  public abstract class freezeManualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.freezeManualDisc>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARTran.discountSequenceID>
  {
  }

  public abstract class dRTermStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARTran.dRTermStartDate>
  {
  }

  public abstract class dRTermEndDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARTran.dRTermEndDate>
  {
  }

  public abstract class requiresTerms : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.requiresTerms>
  {
  }

  public abstract class curyUnitPriceDR : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.curyUnitPriceDR>
  {
  }

  public abstract class discPctDR : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.discPctDR>
  {
  }

  public abstract class itemHasResidual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.itemHasResidual>
  {
  }

  public abstract class groupDiscountAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.groupDiscountAmount>
  {
  }

  public abstract class documentDiscountAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.documentDiscountAmount>
  {
  }

  public abstract class grossSalesAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARTran.grossSalesAmount>
  {
  }

  public abstract class cost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.cost>
  {
  }

  public abstract class netSalesAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.netSalesAmount>
  {
  }

  public abstract class margin : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.margin>
  {
  }

  public abstract class marginPercent : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.marginPercent>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.subItemID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.locationID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.lotSerialNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARTran.expireDate>
  {
  }

  public abstract class unassignedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARTran.unassignedQty>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ARTran.planID>
  {
  }

  public abstract class origInvoiceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.origInvoiceType>
  {
  }

  public abstract class origInvoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.origInvoiceNbr>
  {
  }

  public abstract class origInvoiceLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.origInvoiceLineNbr>
  {
  }

  public abstract class invtDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.invtDocType>
  {
  }

  public abstract class invtRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.invtRefNbr>
  {
  }

  public abstract class invtReleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.invtReleased>
  {
  }

  /// <exclude />
  public abstract class isCancellation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.isCancellation>
  {
  }

  /// <exclude />
  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARTran.canceled>
  {
  }

  public abstract class substitutionRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARTran.substitutionRequired>
  {
  }

  public abstract class blanketType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.blanketType>
  {
  }

  public abstract class blanketNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARTran.blanketNbr>
  {
  }

  public abstract class blanketLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.blanketLineNbr>
  {
  }

  public abstract class blanketSplitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARTran.blanketSplitLineNbr>
  {
  }

  public abstract class canViewDeferralSchedule : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARTran.canViewDeferralSchedule>
  {
  }
}
