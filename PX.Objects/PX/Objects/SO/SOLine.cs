// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.DR;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.Attributes;
using PX.Objects.IN.Matrix.Interfaces;
using PX.Objects.IN.RelatedItems;
using PX.Objects.PM;
using PX.Objects.SO.DAC.Projections;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <summary>Represents sales order details.</summary>
/// <remarks>
/// The records of this type are created and edited on the <i>Sales Orders (SO301000)</i> form (corresponds to the
/// <see cref="T:PX.Objects.SO.SOOrderEntry" /> graph).
/// </remarks>
[PXCacheName("Sales Order Line")]
[Serializable]
public class SOLine : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSPrimary,
  ILSMaster,
  IItemPlanMaster,
  IHasMinGrossProfit,
  ISortOrder,
  IMatrixItemLine,
  ISubstitutableLine
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected string _Behavior;
  protected string _Operation;
  protected string _ShipComplete;
  protected bool? _Completed;
  protected bool? _OpenLine;
  protected int? _CustomerID;
  protected DateTime? _OrderDate;
  protected DateTime? _CancelDate;
  protected DateTime? _RequestDate;
  protected DateTime? _ShipDate;
  protected string _InvoiceNbr;
  protected DateTime? _InvoiceDate;
  protected short? _InvtMult;
  protected bool? _ManualPrice;
  protected int? _InventoryID;
  protected string _LineType;
  protected int? _SubItemID;
  protected string _TranType;
  protected string _PlanType;
  protected bool? _RequireReasonCode;
  protected bool? _RequireShipping;
  protected bool? _RequireAllocation;
  protected bool? _RequireLocation;
  protected int? _SiteID;
  protected int? _LocationID;
  protected string _LotSerialNbr;
  protected string _OrigOrderType;
  protected string _OrigOrderNbr;
  protected int? _OrigLineNbr;
  protected string _UOM;
  protected Decimal? _ClosedQty;
  protected Decimal? _BaseClosedQty;
  protected Decimal? _OrderQty;
  protected Decimal? _BaseOrderQty;
  protected Decimal? _UnassignedQty;
  protected Decimal? _ShippedQty;
  protected Decimal? _BaseShippedQty;
  protected Decimal? _OpenQty;
  protected Decimal? _BaseOpenQty;
  protected Decimal? _BilledQty;
  protected Decimal? _BaseBilledQty;
  protected Decimal? _UnbilledQty;
  protected Decimal? _BaseUnbilledQty;
  protected Decimal? _CompleteQtyMin;
  protected Decimal? _CompleteQtyMax;
  protected long? _CuryInfoID;
  protected Decimal? _CuryUnitPrice;
  protected Decimal? _UnitPrice;
  protected Decimal? _UnitCost;
  protected Decimal? _CuryUnitCost;
  protected Decimal? _CuryExtPrice;
  protected Decimal? _ExtPrice;
  protected Decimal? _CuryExtCost;
  protected Decimal? _ExtCost;
  protected string _TaxZoneID;
  protected string _TaxCategoryID;
  protected string _AlternateID;
  protected Decimal? _CommnPct;
  protected Decimal? _CuryCommnAmt;
  protected Decimal? _CommnAmt;
  protected string _TranDesc;
  protected Decimal? _UnitWeigth;
  protected Decimal? _UnitVolume;
  protected Decimal? _ExtWeight;
  protected Decimal? _ExtVolume;
  protected bool? _IsFree;
  protected bool? _CalculateDiscountsOnImport;
  protected Decimal? _DiscPct;
  protected Decimal? _CuryDiscAmt;
  protected Decimal? _DiscAmt;
  protected bool? _ManualDisc;
  protected bool? _FreezeManualDisc;
  protected bool? _SkipDisc;
  protected Decimal? _CuryLineAmt;
  protected Decimal? _LineAmt;
  protected Decimal? _CuryOpenAmt;
  protected Decimal? _OpenAmt;
  protected Decimal? _CuryBilledAmt;
  protected Decimal? _BilledAmt;
  protected Decimal? _CuryUnbilledAmt;
  protected Decimal? _UnbilledAmt;
  protected Decimal? _CuryDiscPrice;
  protected Decimal? _DiscPrice;
  protected ushort[] _DiscountsAppliedToLine;
  protected Decimal? _GroupDiscountRate;
  protected Decimal? _DocumentDiscountRate;
  protected Decimal? _AvgCost;
  protected int? _ProjectID;
  protected string _ReasonCode;
  protected int? _SalesPersonID;
  protected int? _SalesAcctID;
  protected int? _SalesSubID;
  protected int? _TaskID;
  protected int? _CostCodeID;
  protected Guid? _NoteID;
  protected bool? _Commissionable;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  protected bool? _AutoCreateIssueLine;
  protected DateTime? _ExpireDate;
  protected string _DiscountID;
  protected string _DiscountSequenceID;
  protected string _POSource;
  protected bool? _POCreated;
  protected int? _VendorID;
  protected int? _POSiteID;
  protected DateTime? _DRTermStartDate;
  protected DateTime? _DRTermEndDate;
  protected Decimal? _CuryUnitPriceDR;
  protected Decimal? _DiscPctDR;
  protected int? _DefScheduleID;
  protected bool? _IsCut;

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.GL.Branch">branch</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.Branch" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.GL.Branch.branchID" /> field.
  /// </value>
  [Branch(typeof (SOOrder.branchID), null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  /// <summary>
  /// The type of the sales order in which this line item is listed.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.Order" />. The field is a part of the identifier of the sales order
  /// <see cref="T:PX.Objects.SO.SOOrder" />.<see cref="T:PX.Objects.SO.SOOrder.orderType" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.OrderType" />. The field is a part of the identifier of the parent order type
  /// <see cref="T:PX.Objects.SO.SOOrderType" />.<see cref="T:PX.Objects.SO.SOOrderType.orderType" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.OrderTypeOperation" />. The field is a part of the identifier of the operation (issues,
  /// receipts) of a particular order types
  /// <see cref="T:PX.Objects.SO.SOOrderTypeOperation" />.<see cref="T:PX.Objects.SO.SOOrderTypeOperation.orderType" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.BlanketOrderLink" />. The field is a part of the identifier of the linked blanket sales
  /// order <see cref="T:PX.Objects.SO.SOBlanketOrderLink" />.<see cref="T:PX.Objects.SO.SOBlanketOrderLink.orderType" /></item>
  /// </list>
  /// </remarks>
  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (SOOrder.orderType))]
  [PXUIField(DisplayName = "Order Type", Visible = false, Enabled = false)]
  [PXSelector(typeof (Search<SOOrderType.orderType>), CacheGlobal = true)]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  /// <summary>
  /// The reference number of the sales order in which this line item is listed.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.Order" />. The field is a part of the identifier of the sales order
  /// <see cref="T:PX.Objects.SO.SOOrder" />.<see cref="T:PX.Objects.SO.SOOrder.orderNbr" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.BlanketOrderLink" />. The field is a part of the identifier of the linked blanket sales
  /// order <see cref="T:PX.Objects.SO.SOBlanketOrderLink" />.<see cref="T:PX.Objects.SO.SOBlanketOrderLink.orderNbr" /></item>
  /// </list>
  /// </remarks>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (SOOrder.orderNbr))]
  [PXParent(typeof (SOLine.FK.Order))]
  [PXUIField(DisplayName = "Order Nbr.", Visible = false, Enabled = false)]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  /// <summary>The line number of the document.</summary>
  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (SOOrder.lineCntr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  /// <summary>The order number of the document line.</summary>
  /// <remarks>
  /// The system regenerates this number automatically when lines are reordered.
  /// </remarks>
  [PXUIField(DisplayName = "Line Order", Visible = false, Enabled = false)]
  [PXDBInt]
  [PXDefault]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  /// <summary>
  /// The behavior which is defined by the <see cref="P:PX.Objects.SO.SOLine.OrderType" /> of the sales order in which this line item is
  /// listed.
  /// </summary>
  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault(typeof (Search<SOOrderType.behavior, Where<SOOrderType.orderType, Equal<Current<SOLine.orderType>>>>))]
  public virtual string Behavior
  {
    get => this._Behavior;
    set => this._Behavior = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (SOOrderType.defaultOperation))]
  [PXSelectorMarker(typeof (Search<SOOrderTypeOperation.operation, Where<SOOrderTypeOperation.orderType, Equal<Current<SOLine.orderType>>>>))]
  public virtual string DefaultOperation { get; set; }

  /// <summary>
  /// The part of the identifier of the <see cref="T:PX.Objects.SO.SOOrderTypeOperation">operation</see> to be performed in
  /// inventory to fulfill the order.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.OrderTypeOperation" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.SO.SOOrderTypeOperation.operation" /> field.
  /// </value>
  /// <remarks>
  /// An order of the RR or RM type includes lines with the Receipt operation and lines with the
  /// <see cref="F:PX.Objects.SO.SOOperation.Issue">Issue</see> operation. Orders of other return types include only lines with
  /// the <see cref="F:PX.Objects.SO.SOOperation.Receipt">Receipt</see> operation.
  /// </remarks>
  [PXDBString(1, IsFixed = true, InputMask = ">a")]
  [PXUIField]
  [PXDefault(typeof (SOOrderType.defaultOperation))]
  [SOOperation.List]
  [PXSelectorMarker(typeof (Search<SOOrderTypeOperation.operation, Where<SOOrderTypeOperation.orderType, Equal<Current<SOLine.orderType>>>>))]
  [PXFormula(typeof (Switch<Case<Where<SOLine.orderQty, Less<decimal0>, And<SOLine.defaultOperation, Equal<SOOperation.issue>, Or<SOLine.orderQty, Equal<decimal0>, And<SOLine.invoiceNbr, IsNotNull>>>>, SOOperation.receipt, Case<Where<SOLine.orderQty, Less<decimal0>, And<SOLine.defaultOperation, Equal<SOOperation.receipt>>>, SOOperation.issue>>, SOLine.defaultOperation>))]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBShort]
  [PXFormula(typeof (IIf<BqlOperand<SOLine.operation, IBqlString>.IsEqual<SOLine.defaultOperation>, short1, shortMinus1>))]
  [PXDefault]
  public virtual short? LineSign { get; set; }

  /// <summary>The way the line item should be shipped.</summary>
  /// <value>
  /// The field can have one of the values listed in <see cref="T:PX.Objects.SO.SOShipComplete" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (SOOrder.shipComplete))]
  [SOShipComplete.List]
  [PXUIField(DisplayName = "Shipping Rule")]
  public virtual string ShipComplete
  {
    get => this._ShipComplete;
    set => this._ShipComplete = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the line is completed.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Completed", Enabled = true)]
  public virtual bool? Completed
  {
    get => this._Completed;
    set => this._Completed = value;
  }

  /// <summary>
  /// The identifier of the open detail line of the parent order.
  /// </summary>
  [PXDBBool]
  [PXFormula(typeof (Switch<Case<Where2<Where<SOLine.requireShipping, Equal<True>, And<SOLine.lineType, NotEqual<SOLineType.miscCharge>, Or<SOLine.behavior, Equal<SOBehavior.bL>>>>, And<SOLine.completed, NotEqual<True>, And<SOLine.orderQty, NotEqual<decimal0>>>>, True>, False>))]
  [DirtyFormula(typeof (Switch<Case<Where<SOLine.openLine, Equal<True>, And<Where<SOLine.isFree, NotEqual<True>, Or<SOLine.manualDisc, Equal<True>>>>>, int1>, int0>), typeof (SumCalc<SOOrder.openLineCntr>), true, SkipZeroUpdates = false, ValidateAggregateCalculation = true)]
  [PXFormula(null, typeof (CountCalc<BlanketSOLine.childLineCntr>))]
  [DirtyFormula(typeof (Switch<Case<Where<SOLine.openLine, Equal<True>, And<Where<SOLine.isFree, NotEqual<True>, Or<SOLine.manualDisc, Equal<True>>>>>, int1>, int0>), typeof (SumCalc<BlanketSOLine.openChildLineCntr>), true, SkipZeroUpdates = false)]
  [PXFormula(null, typeof (CountCalc<BlanketSOLineSplit.childLineCntr>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOLine.cancelled, Equal<False>>, int1>, int0>), typeof (SumCalc<BlanketSOLineSplit.effectiveChildLineCntr>))]
  [DirtyFormula(typeof (Switch<Case<Where<SOLine.openLine, Equal<True>, And<Where<SOLine.isFree, NotEqual<True>, Or<SOLine.manualDisc, Equal<True>>>>>, int1>, int0>), typeof (SumCalc<BlanketSOLineSplit.openChildLineCntr>), true, SkipZeroUpdates = false)]
  [PXUIField(DisplayName = "Open Line", Enabled = false)]
  public virtual bool? OpenLine
  {
    get => this._OpenLine;
    set => this._OpenLine = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether a <see cref="T:PX.Objects.SO.SOLine" /> was created by copying.
  /// This flag can be used to mark copied sales order lines if they need to be distinguished
  /// from the lines that are not created by copying.
  /// </summary>
  [PXBool]
  public virtual bool? IsBeingCopied { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AR.Customer">customer</see> of the original sales order.
  /// </summary>
  /// <remarks>
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.Customer" /> foreign key. The field is a part of the identifier
  /// of the reason code <see cref="T:PX.Objects.AR.Customer" />.<see cref="T:PX.Objects.AR.Customer.bAccountID" />.
  /// </remarks>
  [PXDBInt]
  [PXDefault(typeof (SOOrder.customerID))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  /// <summary>The customer location.</summary>
  /// <value>
  /// By default, the system copies to it the value of the <see cref="T:PX.Objects.SO.SOOrder.customerLocationID">Location</see>
  /// field.
  /// </value>
  /// <remarks>
  /// This field is available only for blanket sales orders. This field cannot be empty.
  /// </remarks>
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOOrder.customerID>>, And<MatchWithBranch<PX.Objects.CR.Location.cBranchID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), DisplayName = "Ship-To Location")]
  [PXDefault(typeof (SOOrder.customerLocationID))]
  [PXForeignReference(typeof (CompositeKey<Field<SOLine.customerID>.IsRelatedTo<PX.Objects.CR.Location.bAccountID>, Field<SOLine.customerLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>>))]
  public virtual int? CustomerLocationID { get; set; }

  /// <summary>
  /// The date of the sales order in which this line item is listed.
  /// </summary>
  [PXDBDate]
  [PXDBDefault(typeof (SOOrder.orderDate))]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  /// <summary>
  /// The expiration date of the order in which this line item is listed.
  /// </summary>
  [PXDBDate]
  [PXDefault(typeof (SOOrder.cancelDate))]
  [PXUIField]
  public virtual DateTime? CancelDate
  {
    get => this._CancelDate;
    set => this._CancelDate = value;
  }

  /// <summary>
  /// The date when the customer wants to receive the goods.
  /// </summary>
  /// <value>
  /// The default value is specified in the <see cref="T:PX.Objects.SO.SOOrder.requestDate">Requested On</see> field of the order.
  /// </value>
  [PXDBDate]
  [PXDefault(typeof (SOOrder.requestDate))]
  [PXUIField]
  public virtual DateTime? RequestDate
  {
    get => this._RequestDate;
    set => this._RequestDate = value;
  }

  /// <summary>The date when the item should be shipped.</summary>
  /// <value>
  /// By default, this date is calculated as a date that is earlier than the
  /// <see cref="T:PX.Objects.SO.SOLine.requestDate">Requested On date</see> by the number of lead days but not earlier than the
  /// <see cref="T:PX.Data.AccessInfo.businessDate">current business date</see>.
  /// </value>
  [PXDBDate]
  [PXFormula(typeof (IIf<Where<SOLine.requestDate, Equal<Parent<SOOrder.requestDate>>>, Parent<SOOrder.shipDate>, DateMinusDaysNotLessThenDate<SOLine.requestDate, IsNull<Selector<Current<SOOrder.customerLocationID>, PX.Objects.CR.Location.cLeadTime>, decimal0>, Parent<SOOrder.orderDate>>>))]
  [PXUIField]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
  }

  /// <summary>
  /// Type of the Invoice to which the return SO line is applied.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.Invoice" />. The field is a part of the identifier of the Invoice
  /// <see cref="T:PX.Objects.SO.SOInvoice" />.<see cref="T:PX.Objects.SO.SOInvoice.docType" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.InvoiceLine" />. The field is a part of the identifier of the Invoice Line
  /// <see cref="T:PX.Objects.AR.ARTran" />.<see cref="T:PX.Objects.AR.ARTran.tranType" /></item>
  /// </list>
  /// </remarks>
  [PXDBString(3, IsFixed = true)]
  [PXUIField]
  [ARDocType.List]
  public virtual string InvoiceType { get; set; }

  /// <summary>
  /// Number of the Invoice to which the return SO line is applied.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.Invoice" />. The field is a part of the identifier of the Invoice
  /// <see cref="T:PX.Objects.SO.SOInvoice" />.<see cref="T:PX.Objects.SO.SOInvoice.refNbr" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.InvoiceLine" />. The field is a part of the identifier of the Invoice Line
  /// <see cref="T:PX.Objects.AR.ARTran" />.<see cref="T:PX.Objects.AR.ARTran.refNbr" /></item>
  /// </list>
  /// </remarks>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  public virtual string InvoiceNbr
  {
    get => this._InvoiceNbr;
    set => this._InvoiceNbr = value;
  }

  /// <summary>
  /// Number of the Invoice line to which the return SO line is applied.
  /// </summary>
  /// <remarks>
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.InvoiceLine" /> foreign key. The field is a part of the
  /// identifier of the Invoice Line <see cref="T:PX.Objects.AR.ARTran" />.<see cref="T:PX.Objects.AR.ARTran.lineNbr" />.
  /// </remarks>
  [PXDBInt]
  [PXUIField(DisplayName = "Invoice Line Nbr.", Enabled = false, Visible = false)]
  public virtual int? InvoiceLineNbr { get; set; }

  /// <summary>
  /// Date of the Invoice line to which the return SO line is applied.
  /// </summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Original Sale Date")]
  public virtual DateTime? InvoiceDate
  {
    get => this._InvoiceDate;
    set => this._InvoiceDate = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrderTypeOperation.invtMult">Inventory Multiplier</see> of the
  /// <see cref="T:PX.Objects.SO.SOLine.operation">operation</see> of the line.
  /// </summary>
  [PXDBShort]
  [PXDefault]
  [PXFormula(typeof (Selector<SOLine.defaultOperation, SOOrderTypeOperation.invtMult>))]
  [PXUIField(DisplayName = "Inventory Multiplier")]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the unit price in this line has been corrected or specified manually.
  /// </summary>
  /// <remarks>
  /// If the field value is <see langword="false" /> then the system updates the
  /// <see cref="T:PX.Objects.SO.SOLine.unitPrice">unit price</see> in the document line with the current price (if one is specified).
  /// If the field value is <see langword="true" /> then <see cref="T:PX.Objects.SO.SOLine.customerID">customer ID</see> is changed in
  /// the sales order or return order, the system does not update unit prices in the line.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Price")]
  public virtual bool? ManualPrice
  {
    get => this._ManualPrice;
    set => this._ManualPrice = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the Inventory Item of the line is a stock item.
  /// </summary>
  [PXDBBool]
  [PXUIField]
  public virtual bool? IsStockItem { get; set; }

  /// <summary>
  /// The inventory ID of the Inventory Item to be sold or returned.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.InventoryItem" />. The field is the identifier of the Stock Item or Non-Stock Item
  /// <see cref="T:PX.Objects.IN.InventoryItem" />.<see cref="T:PX.Objects.IN.InventoryItem.inventoryID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.SiteStatus" />. The field is a part of the identifier of the warehouse container
  /// <see cref="T:PX.Objects.IN.INSiteStatus" />.<see cref="T:PX.Objects.IN.INSiteStatus.inventoryID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.LocationStatus" />. The field is a part of the identifier of the Location inventory item
  /// status <see cref="T:PX.Objects.IN.INSiteStatus" />.<see cref="T:PX.Objects.IN.INLocationStatus.inventoryID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.LotSerialStatus" />. The field is a part of the identifier of the Location inventory
  /// item status by Lot Serial numbers
  /// <see cref="T:PX.Objects.IN.INLotSerialStatus" />.<see cref="T:PX.Objects.IN.INLotSerialStatus.inventoryID" /></item>
  /// </list>
  /// </remarks>
  [SOLineInventoryItem(Filterable = true)]
  [PXDefault]
  [PXForeignReference(typeof (SOLine.FK.InventoryItem))]
  [ConvertedInventoryItem(typeof (SOLine.isStockItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>The type of the line.</summary>
  /// <value>
  /// The field can have one of the values listed in <see cref="T:PX.Objects.SO.SOLineType" />.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [SOLineType.List]
  [PXUIField(DisplayName = "Line Type", Visible = false, Enabled = false)]
  [PXFormula(typeof (Selector<SOLine.inventoryID, Switch<Case<Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>, Or<PX.Objects.IN.InventoryItem.kitItem, Equal<True>, And<PX.Objects.IN.InventoryItem.nonStockShip, Equal<True>>>>, SOLineType.inventory, Case<Where<PX.Objects.IN.InventoryItem.nonStockShip, Equal<True>>, SOLineType.nonInventory>>, SOLineType.miscCharge>>))]
  [PXFormula(null, typeof (CountCalc<SOOrderSite.lineCntr>), ValidateAggregateCalculation = true)]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the item is a kit.
  /// </summary>
  [PXBool]
  [PXUIField]
  [PXFormula(typeof (Selector<SOLine.inventoryID, PX.Objects.IN.InventoryItem.kitItem>))]
  public virtual bool? IsKit { get; set; }

  /// <summary>
  /// Represents a Subitem (or subitem code), which is used to indicate the particular size, color, or other
  /// variation of the inventory item.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.SubItem" />. The field is the identifier of the Subitem
  /// <see cref="T:PX.Objects.IN.INSubItem" />.<see cref="T:PX.Objects.IN.INSubItem.subItemID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.SiteStatus" />. The field is a part of the identifier of the warehouse container
  /// <see cref="T:PX.Objects.IN.INSiteStatus" />.<see cref="T:PX.Objects.IN.INSiteStatus.subItemID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.LocationStatus" />. The field is a part of the identifier of the Location inventory item
  /// status <see cref="T:PX.Objects.IN.INSiteStatus" />.<see cref="T:PX.Objects.IN.INLocationStatus.subItemID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.LotSerialStatus" />. The field is a part of the identifier of the Location inventory
  /// item status by Lot Serial numbers
  /// <see cref="T:PX.Objects.IN.INLotSerialStatus" />.<see cref="T:PX.Objects.IN.INLotSerialStatus.subItemID" /></item>
  /// </list>
  /// </remarks>
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [PXFormula(typeof (Default<SOLine.inventoryID>))]
  [SubItem(typeof (SOLine.inventoryID))]
  [SubItemStatusVeryfier(typeof (SOLine.inventoryID), typeof (SOLine.siteID), new string[] {"IN", "NS"})]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrderTypeOperation.iNDocType">Inventory Transaction Type</see> of the
  /// <see cref="T:PX.Objects.SO.SOLine.operation">operation</see> of the line.
  /// </summary>
  [PXFormula(typeof (Selector<SOLine.operation, SOOrderTypeOperation.iNDocType>))]
  [PXString(3, IsFixed = true)]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  /// <inheritdoc cref="!:ILSPrimary.TranDate" />
  public virtual DateTime? TranDate => this._OrderDate;

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrderTypeOperation.orderPlanType">Order Plan Type</see> of the
  /// <see cref="T:PX.Objects.SO.SOLine.operation">operation</see> of the line.
  /// </summary>
  [PXFormula(typeof (Selector<SOLine.operation, SOOrderTypeOperation.orderPlanType>))]
  [PXString(2, IsFixed = true)]
  public virtual string PlanType
  {
    get => this._PlanType;
    set => this._PlanType = value;
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  [PXDBString(2, IsFixed = true)]
  [PXSelector(typeof (Search<INPlanType.planType>), CacheGlobal = true)]
  public virtual string OrigPlanType { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrderTypeOperation.requireReasonCode">Require Reason Code</see> of the
  /// <see cref="T:PX.Objects.SO.SOLine.operation">operation</see> of the line.
  /// </summary>
  [PXFormula(typeof (Selector<SOLine.operation, SOOrderTypeOperation.requireReasonCode>))]
  [PXBool]
  public virtual bool? RequireReasonCode
  {
    get => this._RequireReasonCode;
    set => this._RequireReasonCode = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrderType.requireShipping">Process Shipments</see> of the
  /// <see cref="T:PX.Objects.SO.SOLine.orderType">order type</see> of the line.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Selector<SOLine.orderType, SOOrderType.requireShipping>))]
  public virtual bool? RequireShipping
  {
    get => this._RequireShipping;
    set => this._RequireShipping = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrderType.requireAllocation">Require Stock Allocation</see> of the
  /// <see cref="T:PX.Objects.SO.SOLine.orderType">order type</see> of the line.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Selector<SOLine.orderType, SOOrderType.requireAllocation>))]
  public virtual bool? RequireAllocation
  {
    get => this._RequireAllocation;
    set => this._RequireAllocation = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrderType.requireLocation">Require Location</see> of the
  /// <see cref="T:PX.Objects.SO.SOLine.orderType">order type</see> of the line.
  /// </summary>
  [PXBool]
  [PXFormula(typeof (Selector<SOLine.orderType, SOOrderType.requireLocation>))]
  public virtual bool? RequireLocation
  {
    get => this._RequireLocation;
    set => this._RequireLocation = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INSiteStatus.qtyAvail">Quantity Available</see> of the item of the line.
  /// </summary>
  [PXDecimal(6)]
  public Decimal? LineQtyAvail { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INSiteStatus.qtyHardAvail">Quantity Hard Available</see> of the item of the line.
  /// </summary>
  [PXDecimal(6)]
  public Decimal? LineQtyHardAvail { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.INSite">warehouse</see> from which the specified quantity of the
  /// Inventory Item should be delivered.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.Site" />. The field is identifier of the Warehouse
  /// <see cref="T:PX.Objects.IN.INSite" />.<see cref="T:PX.Objects.IN.INSite.siteID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.SiteStatus" />. The field is a part of the identifier of the Warehouse inventory item
  /// status <see cref="T:PX.Objects.IN.INSiteStatus" />.<see cref="T:PX.Objects.IN.INSiteStatus.siteID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.LocationStatus" />. The field is a part of the identifier of the Location inventory item
  /// status <see cref="T:PX.Objects.IN.INLocationStatus" />.<see cref="T:PX.Objects.IN.INLocationStatus.siteID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.LotSerialStatus" />. The field is a part of the identifier of the Location inventory
  /// item status by Lot Serial numbers
  /// <see cref="T:PX.Objects.IN.INLotSerialStatus" />.<see cref="T:PX.Objects.IN.INLotSerialStatus.siteID" /></item>
  /// </list>
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.warehouse">Multiple Warehouses</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [SOSiteAvail(DocumentBranchType = typeof (SOOrder.branchID))]
  [PXParent(typeof (Select<SOOrderSite, Where<SOOrderSite.orderType, Equal<Current<SOLine.orderType>>, And<SOOrderSite.orderNbr, Equal<Current<SOLine.orderNbr>>, And<SOOrderSite.siteID, Equal<Current2<SOLine.siteID>>>>>>), LeaveChildren = true, ParentCreate = true)]
  [PXDefault]
  [PXUIRequired(typeof (IIf<Where<SOLine.lineType, NotEqual<SOLineType.miscCharge>>, True, False>))]
  [InterBranchRestrictor(typeof (Where2<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<SOOrder.branchID>>, Or<Current<SOOrder.behavior>, Equal<SOBehavior.qT>>>))]
  [PXUnboundFormula(typeof (IIf<Where<BqlOperand<SOLine.openLine, IBqlBool>.IsEqual<True>>, int1, int0>), typeof (SumCalc<SOOrderSite.openLineCntr>), SkipZeroUpdates = false, ValidateAggregateCalculation = true)]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  /// <summary>
  /// The identifier of the location of the original sales order.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.Location" />. The field is the identifier of the Location
  /// <see cref="T:PX.Objects.IN.INLocation"></see>.<see cref="T:PX.Objects.IN.INLocation.locationID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.LocationStatus" />. The field is a part of the identifier of the Location inventory item
  /// status <see cref="T:PX.Objects.SO.SOOrder" />.<see cref="T:PX.Objects.IN.INLocationStatus.locationID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.LotSerialStatus" />. The field is a part of the identifier of the Location inventory
  /// item status by Lot Serial numbers
  /// <see cref="T:PX.Objects.SO.SOLine.FK.LotSerialStatus" />.<see cref="T:PX.Objects.IN.INLotSerialStatus.locationID" /></item>
  /// </list>
  /// </remarks>
  [SOLocationAvail(typeof (SOLine.inventoryID), typeof (SOLine.subItemID), typeof (SOLine.costCenterID), typeof (SOLine.siteID), typeof (SOLine.tranType), typeof (SOLine.invtMult))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  /// <summary>The lot or serial number of the item for returns.</summary>
  /// <remarks>
  /// <para>The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.LotSerialStatus" /> foreign key. The field is a part of the
  /// identifier of the Location inventory item status by Lot Serial numbers
  /// <see cref="T:PX.Objects.SO.SOLine" />.<see cref="T:PX.Objects.SO.SOLine.locationID" />.</para>
  /// <para>This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.lotSerialTracking">Lot and Serial Tracking</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.</para>
  /// </remarks>
  [INLotSerialNbr(typeof (SOLine.inventoryID), typeof (SOLine.subItemID), typeof (SOLine.locationID), typeof (SOLine.costCenterID))]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.SO.SOOrderType">type</see> of the original order.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.OriginalOrderType" />. The field is the identifier of the Order type
  /// <see cref="T:PX.Objects.SO.SOOrderType"></see>.<see cref="T:PX.Objects.SO.SOOrderType.orderType" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.OriginalOrder" />. The field is a part of the identifier of the Sales order
  /// <see cref="T:PX.Objects.SO.SOOrder" />.<see cref="T:PX.Objects.SO.SOOrder.orderType" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.OriginalOrderLine" />. The field is a part of the identifier of the Sales order line
  /// <see cref="T:PX.Objects.SO.SOLine" />.<see cref="T:PX.Objects.SO.SOLine.orderType" /></item>
  /// </list>
  /// </remarks>
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Orig. Order Type", Enabled = false)]
  public virtual string OrigOrderType
  {
    get => this._OrigOrderType;
    set => this._OrigOrderType = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.SO.SOOrder">reference number</see> of the original sales order.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.OriginalOrder" />. The field is a part of the identifier of the Sales order
  /// <see cref="T:PX.Objects.SO.SOOrder" />.<see cref="T:PX.Objects.SO.SOOrder.orderNbr" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.OriginalOrderLine" />. The field is a part of the identifier of the Sales order line
  /// <see cref="T:PX.Objects.SO.SOLine" />.<see cref="T:PX.Objects.SO.SOLine.orderNbr" /></item>
  /// </list>
  /// </remarks>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Orig. Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOLine.origOrderType>>>>), ValidateValue = false)]
  public virtual string OrigOrderNbr
  {
    get => this._OrigOrderNbr;
    set => this._OrigOrderNbr = value;
  }

  /// <summary>
  /// The part of the identifier of the original <see cref="T:PX.Objects.SO.SOLine">line</see>.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.SO.SOLine.lineNbr" /> field.
  /// </value>
  /// <remarks>
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.OriginalOrderLine" /> foreign key.
  /// </remarks>
  [PXDBInt]
  public virtual int? OrigLineNbr
  {
    get => this._OrigLineNbr;
    set => this._OrigLineNbr = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.AR.ARTran.sOShipmentType">Shipment Type</see> of the original line of the Accounts Receivable
  /// invoice or memo.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  public virtual string OrigShipmentType { get; set; }

  /// <summary>
  /// The unit of measure (UOM) used for the item with this <see cref="T:PX.Objects.SO.SOLine.inventoryID">inventory ID</see>.
  /// </summary>
  [INUnit(typeof (SOLine.inventoryID), DisplayName = "UOM")]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  /// <summary>
  /// The unit of measure (UOM) of the original line of the invoice.
  /// </summary>
  [PXDBString(6, IsUnicode = true, InputMask = ">aaaaaa")]
  public virtual string InvoiceUOM { get; set; }

  /// <summary>
  /// The closed quantity of the item, calculated as the subtraction of the
  /// <see cref="T:PX.Objects.SO.SOLine.openQty">quantity of the item to be shipped</see> from the
  /// <see cref="T:PX.Objects.SO.SOLine.orderQty">quantity of the item sold</see>.
  /// </summary>
  [PXDBCalced(typeof (Sub<SOLine.orderQty, SOLine.openQty>), typeof (Decimal))]
  [PXQuantity(typeof (SOLine.uOM), typeof (SOLine.baseClosedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ClosedQty
  {
    get => this._ClosedQty;
    set => this._ClosedQty = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.closedQty">closed quantity</see> of the item, expressed in the base unit of measure.
  /// </summary>
  [PXDBCalced(typeof (Sub<SOLine.baseOrderQty, SOLine.baseOpenQty>), typeof (Decimal))]
  [PXQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseClosedQty
  {
    get => this._BaseClosedQty;
    set => this._BaseClosedQty = value;
  }

  /// <summary>
  /// The quantity of the item sold in the <see cref="T:PX.Objects.SO.SOLine.uOM">unit of measure</see>.
  /// </summary>
  [PXDBQuantity(typeof (SOLine.uOM), typeof (SOLine.baseOrderQty), InventoryUnitType.SalesUnit)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOLine.operation, Equal<SOLine.defaultOperation>, And<SOLine.lineType, NotEqual<SOLineType.miscCharge>>>, SOLine.orderQty>, decimal0>), typeof (SumCalc<SOOrder.orderQty>), ValidateAggregateCalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOLine.lineType, NotEqual<SOLineType.miscCharge>>, SOLine.orderQty>, decimal0>), typeof (SumCalc<SOBlanketOrderLink.orderedQty>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOLine.cancelled, Equal<False>>, SOLine.orderQty>, decimal0>), typeof (SumCalc<BlanketSOLine.qtyOnOrders>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOLine.cancelled, Equal<False>>, SOLine.orderQty>, decimal0>), typeof (SumCalc<BlanketSOLineSplit.qtyOnOrders>))]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OrderQty" />
  public virtual Decimal? Qty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  /// <summary>
  /// The quantity of the item sold, expressed in the base unit of measure.
  /// </summary>
  /// <remarks>
  /// This quantity is used for calculating discounts if the
  /// <see cref="T:PX.Objects.AR.ARSetup.applyQuantityDiscountBy">Base UOM</see> field is <see langword="true" />.
  /// </remarks>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Order Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseOrderQty
  {
    get => this._BaseOrderQty;
    set => this._BaseOrderQty = value;
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BaseOrderQty" />
  public virtual Decimal? BaseQty
  {
    get => this._BaseOrderQty;
    set => this._BaseOrderQty = value;
  }

  /// <summary>
  /// This will store the initial Order Qty of the SO to the verification process while updating the SOLine/SOLineSplits
  /// </summary>
  [PXDBCalced(typeof (SOLine.orderQty), typeof (Decimal))]
  [PXDecimal]
  public virtual Decimal? VerifyOrderQty { get; set; }

  /// <summary>
  /// Contains the difference between the line quantity and the quantity on child split lines, for Inventory
  /// Items with lot or serial number.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnassignedQty
  {
    get => this._UnassignedQty;
    set => this._UnassignedQty = value;
  }

  /// <summary>
  /// The quantity of the stock item being prepared for shipment and already shipped for this order.
  /// </summary>
  [PXDBQuantity(typeof (SOLine.uOM), typeof (SOLine.baseShippedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Shipments", Enabled = false)]
  public virtual Decimal? ShippedQty
  {
    get => this._ShippedQty;
    set => this._ShippedQty = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.shippedQty">quantity</see> of the stock item being prepared for shipment and already shipped
  /// for this order, expressed in the base unit of measure.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseShippedQty
  {
    get => this._BaseShippedQty;
    set => this._BaseShippedQty = value;
  }

  /// <summary>The quantity of the item to be shipped.</summary>
  /// <remarks>
  /// That is, the total quantity minus the quantity shipped according to closed shipment documents.
  /// </remarks>
  [PXDBQuantity(typeof (SOLine.uOM), typeof (SOLine.baseOpenQty))]
  [PXFormula(typeof (Switch<Case<Where2<Where<SOLine.requireShipping, Equal<True>, And<SOLine.lineType, NotEqual<SOLineType.miscCharge>, Or<SOLine.behavior, Equal<SOBehavior.bL>>>>, And<SOLine.completed, NotEqual<True>>>, Switch<Case<Where<SOLine.lineSign, Equal<shortMinus1>>, Minimum<Sub<SOLine.orderQty, SOLine.closedQty>, decimal0>>, Maximum<Sub<SOLine.orderQty, SOLine.closedQty>, decimal0>>>, decimal0>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOLine.lineType, NotEqual<SOLineType.miscCharge>>, BqlOperand<SOLine.openQty, IBqlDecimal>.Multiply<SOLine.lineSign>>, decimal0>), typeof (SumCalc<SOOrder.openOrderQty>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.openQty">quantity of the item to be shipped</see>, expressed in the base unit of measure.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Open Qty.")]
  public virtual Decimal? BaseOpenQty
  {
    get => this._BaseOpenQty;
    set => this._BaseOpenQty = value;
  }

  /// <summary>
  /// The quantity of the blanket sales order line that have not been shipped yet in child orders.
  /// </summary>
  /// <remarks>
  /// This field is available only for blanket sales orders.
  /// </remarks>
  [PXQuantity]
  [PXFormula(typeof (Switch<Case<Where<SOLine.lineType, NotEqual<SOLineType.miscCharge>>, SOLine.openQty>, decimal0>))]
  [PXUIField(DisplayName = "Unshipped Qty.", Enabled = false, FieldClass = "DISTINV")]
  public virtual Decimal? UnshippedQty { get; set; }

  /// <summary>
  /// The quantity of stock and non-stock items that were billed.
  /// </summary>
  [PXDBQuantity(typeof (SOLine.uOM), typeof (SOLine.baseBilledQty), MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Billed Quantity", Enabled = false)]
  public virtual Decimal? BilledQty
  {
    get => this._BilledQty;
    set => this._BilledQty = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.billedQty">quantity</see> of stock and non-stock items that were billed, expressed in the
  /// base unit of measure.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseBilledQty
  {
    get => this._BaseBilledQty;
    set => this._BaseBilledQty = value;
  }

  /// <summary>
  /// The quantity of stock and non-stock items that were not yet billed.
  /// </summary>
  [PXDBQuantityWithOrigQty(typeof (SOLine.uOM), typeof (SOLine.baseUnbilledQty), typeof (SOLine.shippedQty), typeof (SOLine.baseShippedQty))]
  [PXFormula(typeof (Switch<Case<Where<SOLine.requireShipping, Equal<True>, And<SOLine.lineType, NotEqual<SOLineType.miscCharge>, And<SOLine.completed, Equal<True>>>>, Sub<SOLine.shippedQty, SOLine.billedQty>>, Sub<SOLine.orderQty, SOLine.billedQty>>))]
  [PXUnboundFormula(typeof (BqlOperand<SOLine.unbilledQty, IBqlDecimal>.Multiply<SOLine.lineSign>), typeof (SumCalc<SOOrder.unbilledOrderQty>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unbilled Quantity", Enabled = false)]
  public virtual Decimal? UnbilledQty
  {
    get => this._UnbilledQty;
    set => this._UnbilledQty = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.unbilledQty">quantity</see> of stock and non-stock items that were not yet billed, expressed
  /// in the base unit of measure.
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseUnbilledQty
  {
    get => this._BaseUnbilledQty;
    set => this._BaseUnbilledQty = value;
  }

  /// <summary>
  /// The minimum percentage of goods shipped (with respect to the ordered quantity) for the system to mark the
  /// order as completely shipped.
  /// </summary>
  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "100.0", typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>), SourceField = typeof (PX.Objects.IN.InventoryItem.undershipThreshold), CacheGlobal = true)]
  [PXUIField(DisplayName = "Undership Threshold (%)")]
  public virtual Decimal? CompleteQtyMin
  {
    get => this._CompleteQtyMin;
    set => this._CompleteQtyMin = value;
  }

  /// <summary>
  /// The maximum percentage of goods shipped (with respect to the ordered quantity) allowed by the customer.
  /// </summary>
  [PXDBDecimal(2, MinValue = 100.0, MaxValue = 999.0)]
  [PXDefault(TypeCode.Decimal, "100.0", typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>), SourceField = typeof (PX.Objects.IN.InventoryItem.overshipThreshold), CacheGlobal = true)]
  [PXUIField(DisplayName = "Overship Threshold (%)")]
  public virtual Decimal? CompleteQtyMax
  {
    get => this._CompleteQtyMax;
    set => this._CompleteQtyMax = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.CM.CurrencyInfo">currency and exchange rate information</see>.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.CurrencyInfo" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.CM.CurrencyInfo.curyInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo(typeof (SOOrder.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  /// <summary>The type of the item price of the line.</summary>
  /// <value>
  /// The field can have one of the values listed in <see cref="T:PX.Objects.AR.PriceTypes" />.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PriceTypes.List]
  [PXUIField(DisplayName = "Price Type", Visible = false, Enabled = false)]
  public virtual string PriceType { get; set; }

  /// <inheritdoc cref="T:PX.Objects.AR.ARSalesPrice.isPromotionalPrice" />
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Promotional Price", Visible = false, Enabled = false)]
  public virtual bool? IsPromotionalPrice { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.unitPrice">unit price</see> of the item (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (SOLine.curyInfoID), typeof (SOLine.unitPrice))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitPrice
  {
    get => this._CuryUnitPrice;
    set => this._CuryUnitPrice = value;
  }

  /// <summary>The unit price of the item.</summary>
  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price", Enabled = false)]
  public virtual Decimal? UnitPrice
  {
    get => this._UnitPrice;
    set => this._UnitPrice = value;
  }

  /// <summary>
  /// The unit cost at which the item being returned was issued from inventory when it was sold.
  /// </summary>
  /// <value>
  /// For the return lines added with a link to an original invoice, this is the cost specified in the inventory
  /// issue transaction that was generated on release of the original invoice. For the return lines not linked
  /// to an invoice, the unit cost specified in this column depends on the items valuation method and the
  /// settings of the warehouse specified in the line.
  /// </value>
  /// <remarks>
  /// This field is available for orders of the CM, RC, RR, RM or CR type.
  /// </remarks>
  [PXDBPriceCost]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.unitCost">unit cost</see> at which the item being returned was issued from inventory when
  /// it was sold (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (SOLine.curyInfoID), typeof (SOLine.unitCost), BaseCalc = false, KeepResultValue = true)]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitCost
  {
    get => this._CuryUnitCost;
    set => this._CuryUnitCost = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.extPrice">extended price</see> of the item line (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOLine.curyInfoID), typeof (SOLine.extPrice))]
  [PXUIField(DisplayName = "Ext. Price")]
  [PXFormula(typeof (Mult<SOLine.orderQty, SOLine.curyUnitPrice>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExtPrice
  {
    get => this._CuryExtPrice;
    set => this._CuryExtPrice = value;
  }

  /// <summary>
  /// The extended price, which the system calculates as the <see cref="T:PX.Objects.SO.SOLine.unitPrice">unit price</see> multiplied
  /// by the <see cref="T:PX.Objects.SO.SOLine.orderQty">quantity</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtPrice
  {
    get => this._ExtPrice;
    set => this._ExtPrice = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.extCost">extended cost</see> of the item line (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOLine.curyInfoID), typeof (SOLine.extCost), BaseCalc = false, KeepResultValue = true)]
  [PXUIField(DisplayName = "Extended Cost")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExtCost
  {
    get => this._CuryExtCost;
    set => this._CuryExtCost = value;
  }

  /// <summary>
  /// The extended cost, which the system calculates as the <see cref="T:PX.Objects.SO.SOLine.unitCost">unit cost</see> multiplied
  /// by the <see cref="T:PX.Objects.SO.SOLine.orderQty">quantity</see>.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Mult<SOLine.orderQty, SOLine.unitCost>))]
  public virtual Decimal? ExtCost
  {
    get => this._ExtCost;
    set => this._ExtCost = value;
  }

  /// <summary>
  /// The tax zone associated with the <see cref="T:PX.Objects.SO.SOLine.customerLocationID">customer location</see>.
  /// </summary>
  /// <value>
  /// If no tax zone is specified for this <see cref="T:PX.Objects.SO.SOLine.customerLocationID">customer location</see>, the system
  /// inserts into this field the tax zone assigned to the selling branch.
  /// </value>
  /// <remarks>
  /// This field is available only for blanket sales orders.
  /// </remarks>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (SOOrder.taxZoneID))]
  [PXUIField(DisplayName = "Tax Zone")]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxZone.isExternal, Equal<False>, Or<PX.Objects.TX.TaxZone.taxZoneID, Equal<Current<SOOrder.taxZoneID>>>>), "The external tax provider cannot be manually selected on the blanket sales order line. To calculate taxes via an external tax provider, select a tax zone in the Customer Tax Zone box on the Financial tab.", new System.Type[] {})]
  public virtual string TaxZoneID
  {
    get => this._TaxZoneID;
    set => this._TaxZoneID = value;
  }

  /// <summary>The tax category of the goods mentioned in this line.</summary>
  /// <remarks>
  /// <para>The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.TaxCategory" /> foreign key.
  /// The field is the identifier of the tax category
  /// <see cref="T:PX.Objects.TX.TaxCategory" />.<see cref="T:PX.Objects.TX.TaxCategory.taxCategoryID" />.</para>
  /// <para>This field is not available for orders of the TR type.</para>
  /// </remarks>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [SOTax(typeof (SOOrder), typeof (SOTax), typeof (SOTaxTran), typeof (SOOrder.taxCalcMode), TaxCalc = TaxCalc.ManualLineCalc, CuryTaxableAmtField = typeof (SOLine.curyTaxableAmt), Inventory = typeof (SOLine.inventoryID), UOM = typeof (SOLine.uOM), LineQty = typeof (SOLine.orderQty))]
  [SOOpenTax(typeof (SOOrder), typeof (SOTax), typeof (SOTaxTran), TaxCalc = TaxCalc.ManualLineCalc, Inventory = typeof (SOLine.inventoryID), UOM = typeof (SOLine.uOM), LineQty = typeof (SOLine.openQty))]
  [SOUnbilledTax(typeof (SOOrder), typeof (SOTax), typeof (SOTaxTran), TaxCalc = TaxCalc.ManualLineCalc, Inventory = typeof (SOLine.inventoryID), UOM = typeof (SOLine.uOM), LineQty = typeof (SOLine.unbilledQty))]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.taxCategoryID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>))]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  /// <summary>
  /// The entity usage type of the customer location if sales to this location are tax-exempt.
  /// </summary>
  /// <value>
  /// By default, the system inserts the <see cref="T:PX.Objects.SO.SOOrder.avalaraCustomerUsageType">entity usage type</see>.
  /// </value>
  /// <remarks>
  /// This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.avalaraTax">External Tax Calculation Integration</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// This field is not available for transfer orders.
  /// </remarks>
  [PXDefault("0", typeof (SOOrder.avalaraCustomerUsageType))]
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Tax Exemption Type")]
  [TXAvalaraCustomerUsageType.List]
  public virtual string AvalaraCustomerUsageType { get; set; }

  /// <summary>
  /// The alternate ID for the item, such as the barcode or the inventory ID used by the customer.
  /// </summary>
  [AlternativeItem(INPrimaryAlternateType.CPN, typeof (SOLine.customerID), typeof (SOLine.inventoryID), typeof (SOLine.subItemID), typeof (SOLine.uOM))]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  /// <summary>The default commission percentage of the salesperson.</summary>
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  public virtual Decimal? CommnPct
  {
    get => this._CommnPct;
    set => this._CommnPct = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.commnAmt">default commission amount</see> of the salesperson
  /// (in the currency of the document).
  /// </summary>
  [PXDBDecimal(4)]
  public virtual Decimal? CuryCommnAmt
  {
    get => this._CuryCommnAmt;
    set => this._CuryCommnAmt = value;
  }

  /// <summary>The default commission amount of the salesperson.</summary>
  [PXDBDecimal(4)]
  public virtual Decimal? CommnAmt
  {
    get => this._CommnAmt;
    set => this._CommnAmt = value;
  }

  /// <summary>The description provided for the stock item.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Line Description")]
  [PXLocalizableDefault(typeof (Search<PX.Objects.IN.InventoryItem.descr, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>), typeof (PX.Objects.AR.Customer.localeName))]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  /// <summary>The unit weight of the item.</summary>
  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<PX.Objects.IN.InventoryItem.baseWeight, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>))]
  [PXUIField(DisplayName = "Unit Weight")]
  public virtual Decimal? UnitWeigth
  {
    get => this._UnitWeigth;
    set => this._UnitWeigth = value;
  }

  /// <summary>The unit volume of the item.</summary>
  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<PX.Objects.IN.InventoryItem.baseVolume, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<SOLine.inventoryID>>>>))]
  public virtual Decimal? UnitVolume
  {
    get => this._UnitVolume;
    set => this._UnitVolume = value;
  }

  /// <summary>
  /// The extended weight, which the system calculates as the <see cref="T:PX.Objects.SO.SOLine.unitWeigth">unit weight</see> multiplied
  /// by the <see cref="T:PX.Objects.SO.SOLine.orderQty">quantity</see>.
  /// </summary>
  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Mult<Abs<Row<SOLine.baseOrderQty>.WithDependency<SOLine.orderQty>>, SOLine.unitWeigth>), typeof (SumCalc<SOOrder.orderWeight>))]
  [PXUIField(DisplayName = "Ext. Weight")]
  public virtual Decimal? ExtWeight
  {
    get => this._ExtWeight;
    set => this._ExtWeight = value;
  }

  /// <summary>
  /// The extended volume, which the system calculates as the <see cref="T:PX.Objects.SO.SOLine.unitVolume">unit volume</see> multiplied
  /// by the <see cref="T:PX.Objects.SO.SOLine.orderQty">quantity</see>.
  /// </summary>
  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Mult<Abs<Row<SOLine.baseOrderQty>.WithDependency<SOLine.orderQty>>, SOLine.unitVolume>), typeof (SumCalc<SOOrder.orderVolume>))]
  [PXUIField(DisplayName = "Ext. Volume")]
  public virtual Decimal? ExtVolume
  {
    get => this._ExtVolume;
    set => this._ExtVolume = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the inventory item specified in the row is a free item.
  /// </summary>
  /// <remarks>
  /// If the field value is <see langword="true" /> then the system updates the
  /// <see cref="T:PX.Objects.SO.SOLine.unitPrice">Unit Price</see>, <see cref="T:PX.Objects.SO.SOLine.discPct">Discount Percent</see>,
  /// <see cref="T:PX.Objects.SO.SOLine.discAmt">Discount Amount</see>, and  <see cref="T:PX.Objects.SO.SOLine.extPrice">Ext. Price</see> amounts with 0 and
  /// set <see cref="T:PX.Objects.SO.SOLine.manualDisc">Manual Discount</see> field to <see langword="true" />.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Free Item")]
  public virtual bool? IsFree
  {
    get => this._IsFree;
    set => this._IsFree = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the line discounts will be calculated automaticly.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Calculate automatic discounts on import")]
  public virtual bool? CalculateDiscountsOnImport
  {
    get => this._CalculateDiscountsOnImport;
    set => this._CalculateDiscountsOnImport = value;
  }

  /// <summary>The percent of the line-level discount.</summary>
  /// <remarks>
  /// If the <see cref="T:PX.Objects.SO.SOLine.manualDisc">Manual Discount</see> field value is <see langword="true" />, it indicates
  /// that the percent of the discount is specified by the line discount that has been applied manually, or has
  /// been entered manually or calculated based on the <see cref="T:PX.Objects.SO.SOLine.discAmt">discount amount</see> of the line.
  /// </remarks>
  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscPct
  {
    get => this._DiscPct;
    set => this._DiscPct = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.discAmt">amount</see> of the line-level discount of the line
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOLine.curyInfoID), typeof (SOLine.discAmt))]
  [PXUIField(DisplayName = "Discount Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscAmt
  {
    get => this._CuryDiscAmt;
    set => this._CuryDiscAmt = value;
  }

  /// <summary>The amount of the line-level discount of the line.</summary>
  /// <remarks>
  /// If the <see cref="T:PX.Objects.SO.SOLine.manualDisc">Manual Discount</see> field value is <see langword="true" />, it indicates
  /// that the amount of the discount is specified by the line discount that has been applied manually, or has
  /// been entered manually or calculated based on the <see cref="T:PX.Objects.SO.SOLine.discPct">discount percent</see> of the line.
  /// </remarks>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt
  {
    get => this._DiscAmt;
    set => this._DiscAmt = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the discount has been applied manually.
  /// </summary>
  [ManualDiscountMode(typeof (SOLine.curyDiscAmt), typeof (SOLine.discPct), DiscountFeatureType.CustomerDiscount)]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ManualDisc
  {
    get => this._ManualDisc;
    set => this._ManualDisc = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the system will not recalculate
  /// the discount of the <see cref="F:PX.Objects.Common.Discount.DiscountType.Line">line</see> type.
  /// </summary>
  [PXBool]
  public virtual bool? FreezeManualDisc
  {
    get => this._FreezeManualDisc;
    set => this._FreezeManualDisc = value;
  }

  [PXBool]
  public virtual bool? SkipDisc
  {
    get => this._SkipDisc;
    set => this._SkipDisc = value;
  }

  /// <summary>
  /// Indicates (if selected) that the automatic line discounts are not applied to this line.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ignore Automatic Line Discounts", Visible = false)]
  public virtual bool? SkipLineDiscounts { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the system does not need to calculate discounts, because they are
  /// already calculated.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Automatic Discounts Disabled", Visible = false, Enabled = false)]
  public virtual bool? AutomaticDiscountsDisabled { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the system does not need to calculate taxes, because they are
  /// already calculated.
  /// </summary>
  [PXDBBool]
  [PXDefault(false, typeof (SOOrder.disableAutomaticTaxCalculation))]
  [PXUIField(DisplayName = "Disable Automatic Tax Calculation", Visible = false, Enabled = false)]
  public virtual bool? DisableAutomaticTaxCalculation { get; set; }

  /// <summary>
  /// The<see cref="T:PX.Objects.SO.SOLine.lineAmt"> amount of the line</see>, which the system calculates as the extended price minus
  /// the line-level discount (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOLine.curyInfoID), typeof (SOLine.lineAmt))]
  [PXUIField(DisplayName = "Amount", Enabled = false)]
  [PXFormula(typeof (Sub<SOLine.curyExtPrice, SOLine.curyDiscAmt>))]
  [PXFormula(null, typeof (CountCalc<SOSalesPerTran.refCntr>))]
  [PXFormula(null, typeof (SumCalc<SOBlanketOrderLink.curyOrderedAmt>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryLineAmt
  {
    get => this._CuryLineAmt;
    set => this._CuryLineAmt = value;
  }

  /// <summary>
  /// The amount of the line, which the system calculates as the extended price minus the line-level discount.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineAmt
  {
    get => this._LineAmt;
    set => this._LineAmt = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.openAmt">amount</see> of the stock items not yet shipped according to the sales order
  /// (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOLine.curyInfoID), typeof (SOLine.openAmt))]
  [PXFormula(typeof (BqlFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<SOLine.lineType, IBqlString>.IsNotEqual<SOLineType.miscCharge>>, SOLine.openQty>>, decimal0>, IBqlDecimal>.Multiply<BqlOperand<SOLine.curyLineAmt, IBqlDecimal>.Divide<BqlOperand<SOLine.orderQty, IBqlDecimal>.When<BqlOperand<SOLine.orderQty, IBqlDecimal>.IsNotEqual<decimal0>>.Else<decimal1>>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Open Amount")]
  public virtual Decimal? CuryOpenAmt
  {
    get => this._CuryOpenAmt;
    set => this._CuryOpenAmt = value;
  }

  /// <summary>
  /// The amount of the stock items not yet shipped according to the line.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OpenAmt
  {
    get => this._OpenAmt;
    set => this._OpenAmt = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.billedAmt">sum of amounts</see> of the payments or prepayments that have been applied to
  /// the AR invoice generated for the line of the order (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOLine.curyInfoID), typeof (SOLine.billedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryBilledAmt
  {
    get => this._CuryBilledAmt;
    set => this._CuryBilledAmt = value;
  }

  /// <summary>
  /// The sum of amounts of the payments or prepayments that have been applied to the AR invoice generated for
  /// the line of the order.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledAmt
  {
    get => this._BilledAmt;
    set => this._BilledAmt = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.unbilledAmt">unbilled amount</see> for a stock item with the
  /// <see cref="T:PX.Objects.SO.SOLineType.inventory">Goods for Inventory</see> line type or a non-stock item with the
  /// <see cref="T:PX.Objects.SO.SOLineType.nonInventory">Non-Inventory Goods</see> line type (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOLine.curyInfoID), typeof (SOLine.unbilledAmt))]
  [PXFormula(typeof (BqlFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<SOLine.orderQty, IBqlDecimal>.IsNotEqual<decimal0>>, SOLine.unbilledQty>>, decimal1>, IBqlDecimal>.Multiply<BqlOperand<SOLine.curyLineAmt, IBqlDecimal>.Divide<BqlOperand<SOLine.orderQty, IBqlDecimal>.When<BqlOperand<SOLine.orderQty, IBqlDecimal>.IsNotEqual<decimal0>>.Else<decimal1>>>))]
  [PXUIField(DisplayName = "Unbilled Amount", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnbilledAmt
  {
    get => this._CuryUnbilledAmt;
    set => this._CuryUnbilledAmt = value;
  }

  /// <summary>
  /// The unbilled amount for a stock item with the <see cref="T:PX.Objects.SO.SOLineType.inventory">Goods for Inventory</see>
  /// line type or a non-stock item with the <see cref="T:PX.Objects.SO.SOLineType.nonInventory">Non-Inventory Goods</see> line
  /// type.
  /// </summary>
  /// <value>
  /// Calculated as the quantity in the sales order minus the quantity in the invoice or invoices generated for
  /// this order, multiplied by the discounted unit price in the order. The unbilled amount for a non-stock item
  /// with the <see cref="T:PX.Objects.SO.SOLineType.miscCharge">Misc. Charge</see> line type is calculated as the line amount
  /// minus the line discount (if applicable), and minus the line amount in the invoice or invoices generated for
  /// this order.
  /// </value>
  /// <remarks>This field is not available for transfer orders.</remarks>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledAmt
  {
    get => this._UnbilledAmt;
    set => this._UnbilledAmt = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.discPrice">unit price, which has been recalculated</see> after the application of discounts
  /// (in the currency of the document).
  /// </summary>
  [PXDBPriceCostCalced(typeof (Sub<SOLine.curyUnitPrice, BqlOperand<SOLine.curyUnitPrice, IBqlDecimal>.Multiply<BqlOperand<SOLine.discPct, IBqlDecimal>.Divide<decimal100>>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  [PXFormula(typeof (Sub<SOLine.curyUnitPrice, Round<BqlOperand<SOLine.curyUnitPrice, IBqlDecimal>.Multiply<BqlOperand<SOLine.discPct, IBqlDecimal>.Divide<decimal100>>, Current<CommonSetup.decPlPrcCst>>>))]
  [PXCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (SOLine.curyInfoID), typeof (SOLine.discPrice))]
  [PXUIField(DisplayName = "Disc. Unit Price", Enabled = false, Visible = false)]
  public virtual Decimal? CuryDiscPrice
  {
    get => this._CuryDiscPrice;
    set => this._CuryDiscPrice = value;
  }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOLine.unitPrice">unit price</see> that has been recalculated after the application of discounts.
  /// </summary>
  [PXDecimal(4)]
  [PXDBPriceCostCalced(typeof (Sub<SOLine.unitPrice, BqlOperand<SOLine.unitPrice, IBqlDecimal>.Multiply<BqlOperand<SOLine.discPct, IBqlDecimal>.Divide<decimal100>>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscPrice
  {
    get => this._DiscPrice;
    set => this._DiscPrice = value;
  }

  /// <summary>
  /// Array of line numbers of discounts applied to the line.
  /// </summary>
  [PXDBPackedIntegerArray]
  public virtual ushort[] DiscountsAppliedToLine
  {
    get => this._DiscountsAppliedToLine;
    set => this._DiscountsAppliedToLine = value;
  }

  /// <summary>
  /// The rate of all discounts of the <see cref="F:PX.Objects.Common.Discount.DiscountType.Group">group</see> type applied to the line.
  /// </summary>
  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? GroupDiscountRate
  {
    get => this._GroupDiscountRate;
    set => this._GroupDiscountRate = value;
  }

  /// <summary>
  /// The rate of all discounts of the <see cref="F:PX.Objects.Common.Discount.DiscountType.Document">document</see> type applied to the line.
  /// </summary>
  [PXDBDecimal(18)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? DocumentDiscountRate
  {
    get => this._DocumentDiscountRate;
    set => this._DocumentDiscountRate = value;
  }

  /// <summary>Average cost of the Inventory Item of the line.</summary>
  [PXPriceCost]
  [PXUIField(DisplayName = "Average Cost", Enabled = false, Visible = false)]
  public virtual Decimal? AvgCost
  {
    get => this._AvgCost;
    set => this._AvgCost = value;
  }

  /// <summary>The source of material for a transaction line.</summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("F")]
  [InventorySourceType.List(false)]
  [PXUIField(DisplayName = "Inventory Source", FieldClass = "InventorySource")]
  public virtual string InventorySource { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.PM.PMProject">project</see>.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.Project" />. The field is the identifier of the Project
  /// <see cref="T:PX.Objects.PM.PMProject" />.<see cref="T:PX.Objects.PM.PMProject.contractCD" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.Task" />. The field is a part of the identifier of the Project Task
  /// <see cref="T:PX.Objects.PM.PMTask" />.<see cref="T:PX.Objects.PM.PMTask.projectID" /></item>
  /// </list>
  /// </remarks>
  [PXDBInt]
  [PXDefault(typeof (SOOrder.projectID))]
  [PXForeignReference(typeof (SOLine.FK.Project))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  /// <summary>
  /// The reason code to be used for creation or cancellation of the order, if applicable.
  /// </summary>
  /// <remarks>
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.ReasonCode" /> foreign key. The field is the identifier of the
  /// reason code <see cref="T:PX.Objects.CS.ReasonCode" />.<see cref="T:PX.Objects.CS.ReasonCode.reasonCodeID" />.
  /// </remarks>
  [PXDBString(20, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<Current<SOLine.tranType>, Equal<INTranType.transfer>, And<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.transfer>, Or<Current<SOLine.tranType>, NotEqual<INTranType.transfer>, And<PX.Objects.CS.ReasonCode.usage, In3<ReasonCodeUsages.sales, ReasonCodeUsages.issue>>>>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXDefault]
  [PXUIField(DisplayName = "Reason Code")]
  [PXForeignReference(typeof (SOLine.FK.ReasonCode))]
  public virtual string ReasonCode
  {
    get => this._ReasonCode;
    set => this._ReasonCode = value;
  }

  /// <summary>
  /// The salesperson associated with the sale of the line item.
  /// </summary>
  /// <remarks>
  /// <para>The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.SalesPerson" /> foreign key. The field is a part of the
  /// identifier of the salesperson
  /// <see cref="T:PX.Objects.AR.SalesPerson" />.<see cref="T:PX.Objects.AR.SalesPerson.salesPersonID" />.</para>
  /// <para>This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.commissions">Commissions</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// This field is not available for orders of the TR type.</para>
  /// </remarks>
  [SalesPerson]
  [PXParent(typeof (Select<SOSalesPerTran, Where<SOSalesPerTran.orderType, Equal<Current<SOLine.orderType>>, And<SOSalesPerTran.orderNbr, Equal<Current<SOLine.orderNbr>>, And<SOSalesPerTran.salespersonID, Equal<Current2<SOLine.salesPersonID>>>>>>), LeaveChildren = true, ParentCreate = true)]
  [PXDefault(typeof (SOOrder.salesPersonID))]
  [PXForeignReference(typeof (SOLine.FK.SalesPerson))]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  /// <summary>
  /// The account associated with the sale of the line item.
  /// </summary>
  /// <remarks>
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.SalesAccount" /> foreign key. The field is the identifier of the
  /// Sales Account <see cref="T:PX.Objects.GL.Account" />.<see cref="T:PX.Objects.GL.Account.accountID" />.
  /// </remarks>
  [PXDefault]
  [Account(typeof (SOLine.branchID), Visible = false)]
  public virtual int? SalesAcctID
  {
    get => this._SalesAcctID;
    set => this._SalesAcctID = value;
  }

  /// <summary>
  /// The subaccount associated with the sale of the line item.
  /// </summary>
  /// <remarks>
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.SalesSubaccount" /> foreign key. The field is the identifier of
  /// the Sales subaccount <see cref="T:PX.Objects.GL.Sub" />.<see cref="T:PX.Objects.GL.Sub.subID" />.
  /// </remarks>
  [PXFormula(typeof (Default<SOLine.branchID>))]
  [PXDefault]
  [SOLineSubAccount(typeof (SOLine.salesAcctID), typeof (SOLine.branchID), false)]
  public virtual int? SalesSubID
  {
    get => this._SalesSubID;
    set => this._SalesSubID = value;
  }

  /// <summary>
  /// The task of the project with which this document is associated.
  /// </summary>
  /// <remarks>
  /// <para>The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.Task" /> foreign key. The field is a part of the identifier
  /// of the Project Task <see cref="T:PX.Objects.PM.PMTask" />.<see cref="T:PX.Objects.PM.PMTask.projectID" />.</para>
  /// <para>This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.projectAccounting">Project Accounting</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form and the integration of the Projects
  /// submodule with Sales Orders has been enabled.</para>
  /// </remarks>
  [PXDefault(typeof (Coalesce<Search<PMAccountTask.taskID, Where<PMAccountTask.projectID, Equal<Current<SOLine.projectID>>, And<PMAccountTask.accountID, Equal<Current<SOLine.salesAcctID>>>>>, Search<PMTask.taskID, Where<PMTask.projectID, Equal<Current<SOLine.projectID>>, And<PMTask.isDefault, Equal<True>>>>>))]
  [ActiveOrInPlanningProjectTask(typeof (SOLine.projectID), "SO", DisplayName = "Project Task")]
  [PXForeignReference(typeof (SOLine.FK.Task))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  /// <summary>
  /// The cost code with which this document is associated to track project costs and revenue.
  /// </summary>
  /// <remarks>
  /// <para>The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.CostCode" /> foreign key. The field is the identifier of
  /// the cost code <see cref="T:PX.Objects.PM.PMCostCode" />.<see cref="T:PX.Objects.PM.PMCostCode.costCodeID" />.</para>
  /// <para>This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.costCodes">Cost Codes</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form in addition to the integration of the
  /// Projects submodule with Sales Orders.</para>
  /// </remarks>
  [CostCode(typeof (SOLine.salesAcctID), typeof (SOLine.taskID), "I", DescriptionField = typeof (PMCostCode.description))]
  [PXForeignReference(typeof (Field<SOLine.costCodeID>.IsRelatedTo<PMCostCode.costCodeID>))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the line is subjected to a sales commission.
  /// </summary>
  /// <remarks>
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.commissions">Commissions</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// This field is not available for orders of the TR type.
  /// </remarks>
  [PXDBBool]
  [PXFormula(typeof (Switch<Case<Where<SOLine.inventoryID, IsNotNull>, Selector<SOLine.inventoryID, PX.Objects.IN.InventoryItem.commisionable>>, True>))]
  [PXUIField(DisplayName = "Commissionable")]
  public bool? Commissionable
  {
    get => this._Commissionable;
    set => this._Commissionable = value;
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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the line of the Issue type will
  /// be created automatically for each order line of the Receipt type if the order is of the RR type.
  /// </summary>
  [PXDBBool]
  [PXFormula(typeof (IsNull<Selector<SOLine.operation, SOOrderTypeOperation.autoCreateIssueLine>, False>))]
  [PXUIField]
  public virtual bool? AutoCreateIssueLine
  {
    get => this._AutoCreateIssueLine;
    set => this._AutoCreateIssueLine = value;
  }

  /// <summary>
  /// The expiration date for the item with the specified lot number.
  /// </summary>
  /// <remarks>
  /// This field is available only for only orders of the RR type.
  /// </remarks>
  [INExpireDate(typeof (SOLine.inventoryID))]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the line is a drop-ship which was created by the previous
  /// realization version.
  /// </summary>
  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? IsLegacyDropShip { get; set; }

  /// <summary>The code of the discount of the line.</summary>
  /// <remarks>
  /// <para> The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.Discount" />. The field is the identifier of the Discount
  /// <see cref="T:PX.Objects.AR.ARDiscount" />.<see cref="T:PX.Objects.AR.ARDiscount.discountID" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.DiscountSequence" />. The field is a part of the identifier of the Discount Sequence
  /// <see cref="T:PX.Objects.AR.DiscountSequence" />.<see cref="T:PX.Objects.AR.DiscountSequence.discountID" /></item>
  /// </list></para>
  /// <para>This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.customerDiscounts">Customer Discounts</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.</para>
  /// </remarks>
  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, Equal<DiscountType.LineDiscount>>>))]
  [PXUIField(DisplayName = "Discount Code", Visible = true, Enabled = true)]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  /// <summary>The identifier of the discount sequence of the line.</summary>
  /// <remarks>
  /// <para>The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.DiscountSequence" /> foreign key. The field is a part of
  /// the identifier of the Discount Sequence
  /// <see cref="T:PX.Objects.AR.DiscountSequence" />.<see cref="T:PX.Objects.AR.DiscountSequence.discountSequenceID" />.</para>
  /// <para>This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.customerDiscounts">Customer Discounts</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.</para>
  /// </remarks>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Discount Sequence", Visible = false, Enabled = false)]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the order line was marked for purchasing (if it has not been shipped
  /// completely) and the line will be available for adding to a purchase order.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Mark for PO")]
  public virtual bool? POCreate { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the line is a part of the special order.
  /// </summary>
  /// <remarks>
  /// A special order is a customer order for goods that a company does not normally keep in stock (due to their
  /// nature, specific components, dimensions, attributes, etc.) or for goods that have been acquired for a
  /// specific job only at a special purchase cost from a vendor. The special-ordered items must maintain their
  /// cost from purchase to sale and are not included in inventory cost calculations.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Special Order", FieldClass = "SpecialOrders")]
  [PXFormula(typeof (IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOLine.behavior, In3<SOBehavior.sO, SOBehavior.rM, SOBehavior.qT>>>>>.And<BqlOperand<SOLine.operation, IBqlString>.IsEqual<SOOperation.issue>>, Selector<SOLine.inventoryID, PX.Objects.IN.InventoryItem.isSpecialOrderItem>, False>))]
  [PXUnboundFormula(typeof (IIf<Where<SOLine.isSpecialOrder, Equal<True>>, int1, int0>), typeof (SumCalc<SOOrder.specialLineCntr>))]
  public virtual bool? IsSpecialOrder { get; set; }

  [PXBool]
  public virtual bool? OrigIsSpecialOrder { get; set; }

  [PXDBInt]
  [PXDefault(typeof (CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Update Cost On PO", FieldClass = "SpecialOrders")]
  public virtual bool? IsCostUpdatedOnPO { get; set; }

  /// <summary>
  /// The purchase order source to be used to fulfill this line.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in <see cref="T:PX.Objects.IN.INReplenishmentSource" />.
  /// </value>
  [PXDBString]
  [PXDefault(typeof (Switch<Case<Where<Current<SOLine.pOCreate>, NotEqual<True>>, Null, Case<Where<FeatureInstalled<FeaturesSet.sOToPOLink>>, INReplenishmentSource.purchaseToOrder, Case<Where<FeatureInstalled<FeaturesSet.dropShipments>>, INReplenishmentSource.dropShipToOrder>>>>))]
  [INReplenishmentSource.SOList]
  [PXUIField(DisplayName = "PO Source")]
  public virtual string POSource
  {
    get => this._POSource;
    set => this._POSource = value;
  }

  /// <summary>
  /// A Boolean value that indicates whether the line was added to a purchase order.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? POCreated
  {
    get => this._POCreated;
    set => this._POCreated = value;
  }

  /// <summary>
  /// The status of the drop-ship purchase order to which the sales order line is linked.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in <see cref="T:PX.Objects.PO.POOrderStatus" />.
  /// </value>
  [PXUIField(DisplayName = "Drop-Ship PO Status", Enabled = false, FieldClass = "DropShipments")]
  [PX.Objects.PO.POOrderStatus.List]
  [PXString(2, IsFixed = true)]
  public virtual string POOrderStatus { get; set; }

  /// <summary>
  /// The type of the drop-ship purchase order to which the sales order line is linked.
  /// </summary>
  /// <value>
  /// The field can have one of the values listed in <see cref="T:PX.Objects.PO.POOrderType" />.
  /// </value>
  [PX.Objects.PO.POOrderType.List]
  [PXString(2, IsFixed = true)]
  public virtual string POOrderType { get; set; }

  /// <summary>
  /// The number of the drop-ship purchase order to which the sales order line is linked.
  /// </summary>
  [PXUIField(DisplayName = "Drop-Ship PO Nbr.", Enabled = false, FieldClass = "DropShipments")]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<SOLine.pOOrderType>>>>))]
  [PXString(15, IsUnicode = true, InputMask = "")]
  public virtual string POOrderNbr { get; set; }

  /// <summary>
  /// The number of the drop-ship purchase order line to which the sales order line is linked.
  /// </summary>
  [PXUIField(DisplayName = "Drop-Ship PO Line Nbr.", Enabled = false, FieldClass = "DropShipments")]
  [PXInt]
  public virtual int? POLineNbr { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the line has an active link to a line of the drop-ship purchase
  /// order.
  /// </summary>
  [PXUIField]
  [PXBool]
  public virtual bool? POLinkActive { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the
  /// <see cref="T:PX.Objects.SO.SOLine.pOCreate">Mark for PO</see> field is <see langword="true" /> and the
  /// <see cref="T:PX.Objects.SO.SOLine.operation">Operation</see> field is equal to the <see cref="T:PX.Objects.SO.SOOperation.issue" /> value of the
  /// line.
  /// </summary>
  [PXFormula(typeof (Switch<Case<Where<SOLine.pOCreate, Equal<True>, And<SOLine.operation, Equal<SOOperation.issue>>>, True>, False>))]
  [PXUIField]
  [PXBool]
  public virtual bool? IsPOLinkAllowed { get; set; }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.AP.Vendor">Vendor</see> of the sales order.
  /// </summary>
  /// <remarks>
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.Vendor" /> foreign key. The field is a part of the identifier of
  /// the Vendor <see cref="T:PX.Objects.AP.Vendor" />.<see cref="T:PX.Objects.AP.Vendor.bAccountID" />.
  /// </remarks>
  [Vendor(typeof (Search2<BAccountR.bAccountID, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.CR.BAccount.isBranch, Equal<True>, And<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>>, Where<PX.Objects.AP.Vendor.type, NotEqual<BAccountType.employeeType>>>))]
  [PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.vStatus, IsNull, Or<BqlOperand<PX.Objects.AP.Vendor.vStatus, IBqlString>.IsIn<VendorStatus.active, VendorStatus.oneTime, VendorStatus.holdPayments>>>), "The vendor status is '{0}'.", new System.Type[] {typeof (PX.Objects.AP.Vendor.vStatus)})]
  [PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.bAccountID, NotEqual<Current<SOOrder.customerID>>, And<Where<PX.Objects.GL.Branch.branchID, IsNull, Or<PX.Objects.GL.Branch.branchID, NotEqual<Current<SOOrder.branchID>>>>>>), "The vendor cannot be specified because either it has been extended from the branch of the sales order or it coincides with the customer of this order.", new System.Type[] {})]
  [PXFormula(typeof (Default<SOLine.siteID>))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.INSite">destination warehouse</see> for the items to be purchased.
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.POSite" /> foreign key.
  /// </summary>
  /// <value>
  /// The value of this field corresponds to the value of the <see cref="T:PX.Objects.IN.INSite.siteID" /> field.
  /// </value>
  [Site(DisplayName = "Purchase Warehouse", Required = true)]
  [PXForeignReference(typeof (SOLine.FK.POSite))]
  public virtual int? POSiteID
  {
    get => this._POSiteID;
    set => this._POSiteID = value;
  }

  /// <summary>
  /// The date when the process of deferred revenue recognition should start for the selected item.
  /// </summary>
  /// <remarks>
  /// This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.defferedRevenue">Deferred Revenue Management</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Term Start Date")]
  public DateTime? DRTermStartDate
  {
    get => this._DRTermStartDate;
    set => this._DRTermStartDate = value;
  }

  /// <summary>
  /// The date when the process of deferred revenue recognition should finish for the selected item.
  /// </summary>
  /// <remarks>
  /// This field is available only if the
  /// <see cref="T:PX.Objects.CS.FeaturesSet.defferedRevenue">Deferred Revenue Management</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Term End Date")]
  public DateTime? DRTermEndDate
  {
    get => this._DRTermEndDate;
    set => this._DRTermEndDate = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the <see cref="P:PX.Objects.SO.SOLine.DRTermStartDate" /> and <see cref="P:PX.Objects.SO.SOLine.DRTermEndDate" />
  /// fields are enabled for the line.
  /// </summary>
  /// <value>
  /// The value of this field is set by the <see cref="T:PX.Objects.SO.SOOrderEntry" /> graph based on the settings of the
  /// <see cref="P:PX.Objects.SO.SOLine.InventoryID">item</see> selected for the line. In other contexts it is not populated. See the
  /// attribute on the <see cref="M:PX.Objects.SO.SOOrderEntry.SOLine_ItemRequiresTerms_CacheAttached(PX.Data.PXCache)" /> handler for details.
  /// </value>
  [PXBool]
  public virtual bool? ItemRequiresTerms { get; set; }

  /// <inheritdoc cref="T:PX.Objects.AR.ARTran.itemHasResidual" />
  [PXBool]
  [DRTerms.VerifyResidual(typeof (SOLine.inventoryID), null, typeof (SOLine.curyUnitPriceDR), typeof (SOLine.curyExtPrice))]
  public virtual bool? ItemHasResidual { get; set; }

  /// <inheritdoc cref="T:PX.Objects.AR.ARTran.curyUnitPriceDR" />
  [PXUIField(DisplayName = "Unit Price for DR", Visible = false)]
  [PXDBDecimal(typeof (Search<CommonSetup.decPlPrcCst>))]
  public virtual Decimal? CuryUnitPriceDR
  {
    get => this._CuryUnitPriceDR;
    set => this._CuryUnitPriceDR = value;
  }

  /// <inheritdoc cref="T:PX.Objects.AR.ARTran.discPctDR" />
  [PXUIField(DisplayName = "Discount Percent for DR", Visible = false)]
  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0)]
  public virtual Decimal? DiscPctDR
  {
    get => this._DiscPctDR;
    set => this._DiscPctDR = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.DR.DRSchedule">deferred revenue or deferred expense schedule</see> of the
  /// sales order.
  /// </summary>
  /// <remarks>
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.DefaultShedule" /> foreign key. The field is a part of the
  /// identifier of the deferred revenue or deferred expense schedule
  /// <see cref="T:PX.Objects.DR.DRSchedule" />.<see cref="T:PX.Objects.DR.DRSchedule.scheduleID" />.
  /// </remarks>
  [PXDBInt]
  public virtual int? DefScheduleID
  {
    get => this._DefScheduleID;
    set => this._DefScheduleID = value;
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  [PXBool]
  [PXFormula(typeof (False))]
  public virtual bool? IsCut
  {
    get => this._IsCut;
    set => this._IsCut = value;
  }

  /// <summary>
  /// The number of the Intercompany purchase order line to which the sales order line is linked.
  /// </summary>
  /// <remarks>
  /// This field is available only if the <see cref="T:PX.Objects.CS.FeaturesSet.interBranch">Inter-Branch Transactions</see>
  /// feature is enabled on the Enable/Disable Features (CS100000) form.
  /// </remarks>
  [PXDBInt]
  public virtual int? IntercompanyPOLineNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.ILSMaster.IsIntercompany" />
  bool? ILSMaster.IsIntercompany => new bool?(false);

  /// <summary>
  /// A Boolean value that indicates whether the current item has to be replaced with the related item that is
  /// specified on the Non-Stock Items (IN202000) or Stock Items (IN202500) form.
  /// </summary>
  /// <remarks>
  /// Shipment for the original item cannot be created if this field is <see langword="true" />.
  /// </remarks>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Substitution Required")]
  public virtual bool? SubstitutionRequired { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOOrder.orderType">order type</see> of the blanket sales order from which the child
  /// order has been generated.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.BlanketOrder" />. The field is a part of the identifier of the blanket sales order
  /// <see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOOrder" />.<see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOOrder.orderType" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.BlanketLine" />. The field is a part of the identifier of the blanket sales order line
  /// <see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLine" />.<see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLine.orderType" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.BlanketSplit" />. The field is a part of the identifier of the blanket sales order split
  /// line <see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLineSplit" />.<see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLineSplit.orderType" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.BlanketOrderLink" />. The field is a part of the identifier of the blanket sales order
  /// link <see cref="T:PX.Objects.SO.SOBlanketOrderLink" />.<see cref="T:PX.Objects.SO.SOBlanketOrderLink.blanketType" /></item>
  /// </list>
  /// </remarks>
  [PXDBString(2, IsFixed = true)]
  public virtual string BlanketType { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.SOOrder.orderNbr">reference number</see> of the blanket sales order from which the child
  /// order has been generated.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.BlanketOrder" />. The field is a part of the identifier of the blanket sales order
  /// <see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOOrder" />.<see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOOrder.orderNbr" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.BlanketLine" />. The field is a part of the identifier of the blanket sales order line
  /// <see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLine" />.<see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLine.orderNbr" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.BlanketSplit" />. The field is a part of the identifier of the blanket sales order split
  /// line <see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLineSplit" />.<see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLineSplit.orderNbr" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.BlanketOrderLink" />. The field is a part of the identifier of the blanket sales order
  /// link <see cref="T:PX.Objects.SO.SOBlanketOrderLink" />.<see cref="T:PX.Objects.SO.SOBlanketOrderLink.blanketNbr" /></item>
  /// </list>
  /// </remarks>
  [PXDBString(15, IsUnicode = true)]
  [PXParent(typeof (SOLine.FK.BlanketOrder))]
  [PXUIField(DisplayName = "Blanket SO Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOLine.blanketType>>>>), ValidateValue = false)]
  [PXParent(typeof (SOLine.FK.BlanketOrderLink))]
  [PXUnboundFormula(typeof (Switch<Case<Where<SOLine.blanketNbr, IsNotNull>, int1>, int0>), typeof (SumCalc<SOOrder.blanketLineCntr>))]
  public virtual string BlanketNbr { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLine.lineNbr">blanket line number</see> of the blanket sales order from which the
  /// child order has been generated.
  /// </summary>
  /// <remarks>
  /// The field is included in the following foreign keys:
  /// <list type="bullet">
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.BlanketLine" />. The field is a part of the identifier of the blanket sales order line
  /// <see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLine" />.<see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLine.lineNbr" /></item>
  /// <item><see cref="T:PX.Objects.SO.SOLine.FK.BlanketSplit" />. The field is a part of the identifier of the blanket sales order split
  /// line <see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLineSplit" />.<see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLineSplit.lineNbr" /></item>
  /// </list>
  /// </remarks>
  [PXDBInt]
  [PXParent(typeof (SOLine.FK.BlanketLine))]
  public virtual int? BlanketLineNbr { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLine.lineNbr">blanket split line number</see> of the blanket sales order from which
  /// the child order has been generated.
  /// </summary>
  /// <remarks>
  /// The field is included in the <see cref="T:PX.Objects.SO.SOLine.FK.BlanketSplit" /> foreign key. The field is a part of the
  /// identifier of the blanket sales order split line
  /// <see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLineSplit" />.<see cref="T:PX.Objects.SO.DAC.Projections.BlanketSOLineSplit.lineNbr" />.
  /// </remarks>
  [PXDBInt]
  [PXParent(typeof (SOLine.FK.BlanketSplit))]
  public virtual int? BlanketSplitLineNbr { get; set; }

  /// <summary>
  /// The quantity of a stock or non-stock item in a blanket sales order line distributed among child orders
  /// that are generated for this line.
  /// </summary>
  [PXDBQuantity(typeof (SOLine.uOM), typeof (SOLine.baseQtyOnOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty. On Orders", Enabled = false)]
  public virtual Decimal? QtyOnOrders { get; set; }

  /// <summary>
  /// The quantity of the item sold, expressed in the base unit of measure in a blanket sales order line,
  /// distributed among child orders that are generated for this line.
  /// </summary>
  [PXDBDecimal(6, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseQtyOnOrders { get; set; }

  /// <summary>
  /// The customer order number that the system inserts into the
  /// <see cref="T:PX.Objects.SO.SOOrder.customerOrderNbr">Customer Order Nbr.</see> field for a generated child order.
  /// </summary>
  /// <remarks>
  /// This field is available only for blanket sales orders.
  /// </remarks>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Customer Order Nbr.")]
  public virtual string CustomerOrderNbr { get; set; }

  /// <summary>
  /// The date on which a child order should be generated for the line of the blanket sales order.
  /// </summary>
  /// <value>
  /// By default, the system inserts the <see cref="T:PX.Data.AccessInfo.businessDate">current business date</see> to this
  /// field. The value in this field can be empty. The value cannot be earlier than the
  /// <see cref="T:PX.Objects.SO.SOOrder.orderDate">date of the blanket sales order</see> and later than the
  /// <see cref="T:PX.Objects.SO.SOOrder.cancelDate">expiration date of the blanket sales order</see>.
  /// </value>
  /// <remarks>
  /// This field is available only for blanket sales orders.
  /// </remarks>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Sched. Order Date")]
  public virtual DateTime? SchedOrderDate { get; set; }

  /// <summary>
  /// The planned date of the shipment for a child order generated from this line.
  /// </summary>
  /// <value>
  /// The date in this field cannot be earlier than the
  /// <see cref="T:PX.Objects.SO.SOOrder.orderDate">date of the blanket sales order</see> and the
  /// <see cref="T:PX.Objects.SO.SOLine.schedOrderDate">Sched. Order Date</see>. The value in this field cannot be later than the
  /// <see cref="T:PX.Objects.SO.SOOrder.cancelDate">expiration date of the blanket sales order</see>.
  /// </value>
  /// <remarks>
  /// This field is available only for blanket sales orders.
  /// </remarks>
  [PXDBDate]
  [PXUIField(DisplayName = "Sched. Shipment Date", FieldClass = "DISTINV")]
  public virtual DateTime? SchedShipDate { get; set; }

  /// <summary>The planned date for creation of a purchase order.</summary>
  /// <value>
  /// By default, the system inserts the <see cref="T:PX.Data.AccessInfo.businessDate">current business date</see> to this
  /// field.
  /// </value>
  /// <remarks>
  /// This field is available only for blanket sales orders.
  /// </remarks>
  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "PO Creation Date", FieldClass = "DISTINV")]
  public virtual DateTime? POCreateDate { get; set; }

  /// <summary>
  /// The quantity of a stock or non-stock item in a blanket sales order line that has not been transferred to
  /// child orders.
  /// </summary>
  /// <value>
  /// This value is calculated as the difference between the line quantity and the quantity on child orders.
  /// </value>
  /// <remarks>
  /// This field is available only for blanket sales orders.
  /// </remarks>
  [PXQuantity]
  [PXDBCalced(typeof (Switch<Case<Where<SOLine.behavior, Equal<SOBehavior.bL>, And<SOLine.lineType, NotEqual<SOLineType.miscCharge>, And<SOLine.completed, Equal<False>>>>, Sub<SOLine.orderQty, SOLine.qtyOnOrders>>, decimal0>), typeof (Decimal))]
  [PXFormula(typeof (Switch<Case<Where<SOLine.behavior, Equal<SOBehavior.bL>, And<SOLine.lineType, NotEqual<SOLineType.miscCharge>, And<SOLine.completed, Equal<False>>>>, Sub<SOLine.orderQty, SOLine.qtyOnOrders>>, decimal0>), typeof (SumCalc<SOOrder.blanketOpenQty>))]
  [PXDefault]
  [PXUIField(DisplayName = "Blanket Open Qty.", Enabled = false, FieldClass = "DISTINV")]
  public virtual Decimal? BlanketOpenQty { get; set; }

  /// <summary>
  /// The identifier of the detail line of the parent order.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ChildLineCntr { get; set; }

  /// <summary>
  /// The identifier of the unshipped detail line of the parent order.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? OpenChildLineCntr { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the order was cancelled.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Cancelled { get; set; }

  /// <summary>
  /// The ship via code that represents the carrier and its service to be used for shipping the ordered goods.
  /// </summary>
  /// <remarks>
  /// This field is available only for blanket sales orders. This field cannot be empty.
  /// </remarks>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Ship Via", FieldClass = "DISTINV")]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), new System.Type[] {typeof (PX.Objects.CS.Carrier.carrierID), typeof (PX.Objects.CS.Carrier.description), typeof (PX.Objects.CS.Carrier.isCommonCarrier), typeof (PX.Objects.CS.Carrier.confirmationRequired), typeof (PX.Objects.CS.Carrier.packageRequired)}, DescriptionField = typeof (PX.Objects.CS.Carrier.description), CacheGlobal = true)]
  [PXDefault(typeof (SOOrder.shipVia))]
  public virtual string ShipVia { get; set; }

  /// <summary>
  /// The point at which the ownership of the goods passes to the customer.
  /// </summary>
  /// <remarks>
  /// This field is available only for blanket sales orders. This field cannot be empty.
  /// </remarks>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "FOB", FieldClass = "DISTINV")]
  [PXSelector(typeof (Search<PX.Objects.CS.FOBPoint.fOBPointID>), DescriptionField = typeof (PX.Objects.CS.FOBPoint.description), CacheGlobal = true)]
  [PXDefault(typeof (SOOrder.fOBPoint))]
  public virtual string FOBPoint { get; set; }

  /// <summary>The shipping terms used for the customer.</summary>
  /// <remarks>
  /// This field is available only for blanket sales orders. This field cannot be empty.
  /// </remarks>
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Terms", FieldClass = "DISTINV")]
  [PXSelector(typeof (PX.Objects.CS.ShipTerms.shipTermsID), DescriptionField = typeof (PX.Objects.CS.ShipTerms.description), CacheGlobal = true)]
  [PXDefault(typeof (SOOrder.shipTermsID))]
  public virtual string ShipTermsID { get; set; }

  /// <summary>
  /// The identification of the shipping zone of the customer to be used to calculate the freight.
  /// </summary>
  /// <remarks>
  /// This field is available only for blanket sales orders. This field cannot be empty.
  /// </remarks>
  [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXUIField(DisplayName = "Shipping Zone", FieldClass = "DISTINV")]
  [PXSelector(typeof (PX.Objects.CS.ShippingZone.zoneID), DescriptionField = typeof (PX.Objects.CS.ShippingZone.description), CacheGlobal = true)]
  [PXDefault(typeof (SOOrder.shipZoneID))]
  public virtual string ShipZoneID { get; set; }

  /// <exclude />
  public bool? SkipLineDiscountsBuffer { get; set; }

  /// <summary>
  /// The line amount that is subject to tax (in the currency of the document).
  /// </summary>
  [PXDBCurrency(typeof (SOLine.curyInfoID), typeof (SOLine.taxableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxableAmt { get; set; }

  /// <summary>The line amount that is subject to tax.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxableAmt { get; set; }

  /// <summary>
  /// The line amount without the tax and with the applied group and document discounts (in the currency of the document).
  /// </summary>
  /// <remarks>
  /// The value has the negative sign for the receipt lines.
  /// </remarks>
  [PXDBCurrency(typeof (SOLine.curyInfoID), typeof (SOLine.netSales))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryNetSales { get; set; }

  /// <summary>
  /// The line amount without the tax and with the applied group and document discounts.
  /// </summary>
  /// <remarks>
  /// The value has the negative sign for the receipt lines.
  /// </remarks>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? NetSales { get; set; }

  /// <summary>
  /// The line's estimated margin amount (in the currency of the document).
  /// </summary>
  /// <remarks>
  /// The value is not available for the transfer order.
  /// The value is empty for the line with zero <see cref="P:PX.Objects.SO.SOLine.CuryExtCost">extended cost</see>
  /// and for the line marked for drop-ship or blanket for drop-ship.
  /// </remarks>
  [PXDBCurrency(typeof (SOLine.curyInfoID), typeof (SOLine.marginAmt))]
  [PXUIField(DisplayName = "Est. Margin Amount", Enabled = false)]
  public virtual Decimal? CuryMarginAmt { get; set; }

  /// <summary>The line's estimated margin amount.</summary>
  /// <remarks>
  /// The value is not available for the transfer order.
  /// The value is empty for the line with zero <see cref="P:PX.Objects.SO.SOLine.ExtCost">extended cost</see>
  /// and for the line marked for drop-ship or blanket for drop-ship.
  /// </remarks>
  [PXDBDecimal(4)]
  public virtual Decimal? MarginAmt { get; set; }

  /// <summary>The line's estimated margin percent.</summary>
  /// <remarks>
  /// The value is not available for the transfer order.
  /// The value is empty for the line with zero <see cref="P:PX.Objects.SO.SOLine.CuryExtCost">extended cost</see>
  /// and for the line marked for drop-ship or blanket for drop-ship.
  /// </remarks>
  [PXDBDecimal(2)]
  [PXUIField(DisplayName = "Est. Margin (%)", Enabled = false)]
  public virtual Decimal? MarginPct { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the <see cref="T:PX.Objects.SO.SOLine" /> record was orchestrated.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Orchestrated", Enabled = false, FieldClass = "OrderOrchestration")]
  public bool? IsOrchestratedLine { get; set; }

  /// <summary>
  /// The original number of the <see cref="T:PX.Objects.SO.SOLine" /> record that it had before the orchestration.
  /// </summary>
  [PXDefault]
  [PXDBInt]
  public int? OrchestrationOriginalLineNbr { get; set; }

  /// <summary>
  /// The identifier of the original <see cref="T:PX.Objects.IN.INSite">warehouse</see>, which the line had before the order orchestration.
  /// </summary>
  [PXDefault]
  [PXDBInt]
  public virtual int? OrchestrationOriginalSiteID { get; set; }

  /// <summary>
  /// The ID of the orchestration plan (see <see cref="T:PX.Objects.SO.SOOrchestrationPlan" />) associated with this <see cref="T:PX.Objects.SO.SOLine" /> record.
  /// </summary>
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Orchestration Plan", Enabled = false, FieldClass = "OrderOrchestration")]
  [PXSelector(typeof (SOOrchestrationPlan.planID))]
  public string OrchestrationPlanID { get; set; }

  public static SOLine FromSOLineSplit(SOLineSplit item)
  {
    return new SOLine()
    {
      OrderType = item.OrderType,
      OrderNbr = item.OrderNbr,
      LineNbr = item.LineNbr,
      Behavior = item.Behavior,
      Operation = item.Operation,
      InventoryID = item.InventoryID,
      SiteID = item.SiteID,
      SubItemID = item.SubItemID,
      LocationID = item.LocationID,
      LotSerialNbr = item.LotSerialNbr,
      Qty = item.Qty,
      OpenQty = item.Qty,
      BaseOpenQty = item.BaseQty,
      UOM = item.UOM,
      OrderDate = item.OrderDate,
      BaseQty = item.BaseQty,
      InvtMult = item.InvtMult,
      PlanType = item.PlanType,
      ShipDate = item.ShipDate,
      RequireAllocation = item.RequireAllocation,
      RequireLocation = item.RequireLocation,
      RequireShipping = item.RequireShipping,
      ProjectID = item.ProjectID,
      TaskID = item.TaskID,
      CostCenterID = item.CostCenterID,
      LineType = item.LineType,
      ShipComplete = item.ShipComplete,
      ShippedQty = item.ShippedQty,
      BaseShippedQty = item.BaseShippedQty,
      TranType = item.TranType,
      POCreate = item.POCreate,
      POSource = item.POSource
    };
  }

  public class PK : PrimaryKeyOf<SOLine>.By<SOLine.orderType, SOLine.orderNbr, SOLine.lineNbr>
  {
    public static SOLine Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (SOLine) PrimaryKeyOf<SOLine>.By<SOLine.orderType, SOLine.orderNbr, SOLine.lineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<SOLine>.By<SOLine.branchID>
    {
    }

    public class Order : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOLine>.By<SOLine.orderType, SOLine.orderNbr>
    {
    }

    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOLine>.By<SOLine.orderType>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<SOLine>.By<SOLine.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<SOLine>.By<SOLine.subItemID>
    {
    }

    public class OriginalOrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOLine>.By<SOLine.origOrderType>
    {
    }

    public class OriginalOrder : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOLine>.By<SOLine.origOrderType, SOLine.origOrderNbr>
    {
    }

    public class OriginalOrderLine : 
      PrimaryKeyOf<SOLine>.By<SOLine.orderType, SOLine.orderNbr, SOLine.lineNbr>.ForeignKeyOf<SOLine>.By<SOLine.origOrderType, SOLine.origOrderNbr, SOLine.origLineNbr>
    {
    }

    public class POSite : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOLine>.By<SOLine.pOSiteID>
    {
    }

    public class OrderTypeOperation : 
      PrimaryKeyOf<SOOrderTypeOperation>.By<SOOrderTypeOperation.orderType, SOOrderTypeOperation.operation>.ForeignKeyOf<SOLine>.By<SOLine.orderType, SOLine.operation>
    {
    }

    public class Site : PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOLine>.By<SOLine.siteID>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<SOLine>.By<SOLine.inventoryID, SOLine.subItemID, SOLine.siteID>
    {
    }

    public class SiteStatusByCostCenter : 
      PrimaryKeyOf<INSiteStatusByCostCenter>.By<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>.ForeignKeyOf<SOLine>.By<SOLine.inventoryID, SOLine.subItemID, SOLine.siteID, SOLine.costCenterID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<SOLine>.By<SOLine.locationID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<SOLine>.By<SOLine.inventoryID, SOLine.subItemID, SOLine.siteID, SOLine.locationID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<SOLine>.By<SOLine.inventoryID, SOLine.subItemID, SOLine.siteID, SOLine.locationID, SOLine.lotSerialNbr>
    {
    }

    public class ReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<SOLine>.By<SOLine.reasonCode>
    {
    }

    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<SOLine>.By<SOLine.customerID>
    {
    }

    public class Invoice : 
      PrimaryKeyOf<SOInvoice>.By<SOInvoice.docType, SOInvoice.refNbr>.ForeignKeyOf<SOLine>.By<SOLine.invoiceType, SOLine.invoiceNbr>
    {
    }

    public class InvoiceLine : 
      PrimaryKeyOf<PX.Objects.AR.ARTran>.By<PX.Objects.AR.ARTran.tranType, PX.Objects.AR.ARTran.refNbr, PX.Objects.AR.ARTran.lineNbr>.ForeignKeyOf<SOLine>.By<SOLine.invoiceType, SOLine.invoiceNbr, SOLine.invoiceLineNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<SOLine>.By<SOLine.curyInfoID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<SOLine>.By<SOLine.taxCategoryID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<SOLine>.By<SOLine.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<SOLine>.By<SOLine.projectID, SOLine.taskID>
    {
    }

    public class SalesPerson : 
      PrimaryKeyOf<PX.Objects.AR.SalesPerson>.By<PX.Objects.AR.SalesPerson.salesPersonID>.ForeignKeyOf<SOLine>.By<SOLine.salesPersonID>
    {
    }

    public class SalesAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<SOLine>.By<SOLine.salesAcctID>
    {
    }

    public class SalesSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<SOLine>.By<SOLine.salesSubID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<SOLine>.By<SOLine.costCodeID>
    {
    }

    public class Discount : 
      PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>.ForeignKeyOf<SOLine>.By<SOLine.discountID>
    {
    }

    public class DiscountSequence : 
      PrimaryKeyOf<PX.Objects.AR.DiscountSequence>.By<PX.Objects.AR.DiscountSequence.discountID, PX.Objects.AR.DiscountSequence.discountSequenceID>.ForeignKeyOf<SOLine>.By<SOLine.discountID, SOLine.discountSequenceID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<SOLine>.By<SOLine.vendorID>
    {
    }

    public class DefaultShedule : 
      PrimaryKeyOf<DRSchedule>.By<DRSchedule.scheduleID>.ForeignKeyOf<SOLine>.By<SOLine.defScheduleID>
    {
    }

    public class BlanketOrder : 
      PrimaryKeyOf<BlanketSOOrder>.By<BlanketSOOrder.orderType, BlanketSOOrder.orderNbr>.ForeignKeyOf<SOLine>.By<SOLine.blanketType, SOLine.blanketNbr>
    {
    }

    public class BlanketLine : 
      PrimaryKeyOf<BlanketSOLine>.By<BlanketSOLine.orderType, BlanketSOLine.orderNbr, BlanketSOLine.lineNbr>.ForeignKeyOf<SOLine>.By<SOLine.blanketType, SOLine.blanketNbr, SOLine.blanketLineNbr>
    {
    }

    public class BlanketSplit : 
      PrimaryKeyOf<BlanketSOLineSplit>.By<BlanketSOLineSplit.orderType, BlanketSOLineSplit.orderNbr, BlanketSOLineSplit.lineNbr, BlanketSOLineSplit.splitLineNbr>.ForeignKeyOf<SOLine>.By<SOLine.blanketType, SOLine.blanketNbr, SOLine.blanketLineNbr, SOLine.blanketSplitLineNbr>
    {
    }

    public class BlanketOrderLink : 
      PrimaryKeyOf<SOBlanketOrderLink>.By<SOBlanketOrderLink.blanketType, SOBlanketOrderLink.blanketNbr, SOBlanketOrderLink.orderType, SOBlanketOrderLink.orderNbr>.ForeignKeyOf<SOLine>.By<SOLine.blanketType, SOLine.blanketNbr, SOLine.orderType, SOLine.orderNbr>
    {
    }
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BranchID" />
  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.branchID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OrderType" />
  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.orderType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OrderNbr" />
  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.orderNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.LineNbr" />
  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.lineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.SortOrder" />
  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.sortOrder>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.Behavior" />
  public abstract class behavior : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.behavior>
  {
  }

  public abstract class defaultOperation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.defaultOperation>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.Operation" />
  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.operation>
  {
  }

  public abstract class lineSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOLine.lineSign>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ShipComplete" />
  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.shipComplete>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.Completed" />
  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.completed>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OpenLine" />
  public abstract class openLine : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.openLine>
  {
  }

  public abstract class isBeingCopied : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.isBeingCopied>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CustomerID" />
  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.customerID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CustomerLocationID" />
  public abstract class customerLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.customerLocationID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OrderDate" />
  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLine.orderDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CancelDate" />
  public abstract class cancelDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLine.cancelDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.RequestDate" />
  public abstract class requestDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLine.requestDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ShipDate" />
  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLine.shipDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.InvoiceType" />
  public abstract class invoiceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.invoiceType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.InvoiceNbr" />
  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.invoiceNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.InvoiceLineNbr" />
  public abstract class invoiceLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.invoiceLineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.InvoiceDate" />
  public abstract class invoiceDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLine.invoiceDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.InvtMult" />
  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOLine.invtMult>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ManualPrice" />
  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.manualPrice>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.IsStockItem" />
  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.isStockItem>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.InventoryID" />
  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.inventoryID>
  {
    public class InventoryBaseUnitRule : 
      PX.Objects.IN.InventoryItem.baseUnit.PreventEditIfExists<Select<SOLine, Where<SOLine.inventoryID, Equal<Current<PX.Objects.IN.InventoryItem.inventoryID>>, And<SOLine.lineType, In3<SOLineType.inventory, SOLineType.nonInventory>, And<SOLine.completed, NotEqual<True>>>>>>
    {
    }
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.lineType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.IsKit" />
  public abstract class isKit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.isKit>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.SubItemID" />
  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.subItemID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.TranType" />
  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.tranType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.PlanType" />
  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.planType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.PlanType" />
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  public abstract class origPlanType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.origPlanType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.RequireReasonCode" />
  public abstract class requireReasonCode : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.requireReasonCode>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.RequireShipping" />
  public abstract class requireShipping : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.requireShipping>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.RequireAllocation" />
  public abstract class requireAllocation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.requireAllocation>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.RequireLocation" />
  public abstract class requireLocation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.requireLocation>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.LineQtyAvail" />
  public abstract class lineQtyAvail : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.lineQtyAvail>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.LineQtyHardAvail" />
  public abstract class lineQtyHardAvail : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine.lineQtyHardAvail>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.SiteID" />
  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.siteID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.LocationID" />
  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.locationID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.LotSerialNbr" />
  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.lotSerialNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OrigOrderType" />
  public abstract class origOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.origOrderType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OrigOrderNbr" />
  public abstract class origOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.origOrderNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OrigLineNbr" />
  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.origLineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OrigShipmentType" />
  public abstract class origShipmentType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.origShipmentType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.UOM" />
  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.uOM>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.InvoiceUOM" />
  public abstract class invoiceUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.invoiceUOM>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ClosedQty" />
  public abstract class closedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.closedQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BaseClosedQty" />
  public abstract class baseClosedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.baseClosedQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OrderQty" />
  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.orderQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BaseOrderQty" />
  public abstract class baseOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.baseOrderQty>
  {
  }

  public abstract class verifyOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.verifyOrderQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.UnassignedQty" />
  public abstract class unassignedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.unassignedQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ShippedQty" />
  public abstract class shippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.shippedQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ShippedQty" />
  public abstract class baseShippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.baseShippedQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OpenQty" />
  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.openQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BaseOpenQty" />
  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.baseOpenQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.UnshippedQty" />
  public abstract class unshippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.unshippedQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BilledQty" />
  public abstract class billedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.billedQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BaseBilledQty" />
  public abstract class baseBilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.baseBilledQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.UnbilledQty" />
  public abstract class unbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.unbilledQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BaseUnbilledQty" />
  public abstract class baseUnbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.baseUnbilledQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CompleteQtyMin" />
  public abstract class completeQtyMin : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.completeQtyMin>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CompleteQtyMax" />
  public abstract class completeQtyMax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.completeQtyMax>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryInfoID" />
  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOLine.curyInfoID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.PriceType" />
  public abstract class priceType : IBqlField, IBqlOperand
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.IsPromotionalPrice" />
  public abstract class isPromotionalPrice : IBqlField, IBqlOperand
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryUnitPrice" />
  public abstract class curyUnitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyUnitPrice>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.UnitPrice" />
  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.unitPrice>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.UnitCost" />
  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.unitCost>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryUnitCost" />
  public abstract class curyUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyUnitCost>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryExtPrice" />
  public abstract class curyExtPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyExtPrice>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ExtPrice" />
  public abstract class extPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.extPrice>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryExtCost" />
  public abstract class curyExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyExtCost>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ExtCost" />
  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.extCost>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.TaxZoneID" />
  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.taxZoneID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.TaxCategoryID" />
  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.taxCategoryID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.AvalaraCustomerUsageType" />
  public abstract class avalaraCustomerUsageType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLine.avalaraCustomerUsageType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.AlternateID" />
  public abstract class alternateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.alternateID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CommnPct" />
  public abstract class commnPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.commnPct>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryCommnAmt" />
  public abstract class curyCommnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyCommnAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CommnAmt" />
  public abstract class commnAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.commnAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.TranDesc" />
  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.tranDesc>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.UnitWeigth" />
  public abstract class unitWeigth : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.unitWeigth>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.UnitVolume" />
  public abstract class unitVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.unitVolume>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ExtWeight" />
  public abstract class extWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.extWeight>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ExtVolume" />
  public abstract class extVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.extVolume>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.IsFree" />
  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.isFree>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CalculateDiscountsOnImport" />
  public abstract class calculateDiscountsOnImport : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOLine.calculateDiscountsOnImport>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DiscPct" />
  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.discPct>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryDiscAmt" />
  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyDiscAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DiscAmt" />
  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.discAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ManualDisc" />
  public abstract class manualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.manualDisc>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.FreezeManualDisc" />
  public abstract class freezeManualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.freezeManualDisc>
  {
  }

  public abstract class skipDisc : IBqlField, IBqlOperand
  {
  }

  public abstract class skipLineDiscounts : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.skipLineDiscounts>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.AutomaticDiscountsDisabled" />
  public abstract class automaticDiscountsDisabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOLine.automaticDiscountsDisabled>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DisableAutomaticTaxCalculation" />
  public abstract class disableAutomaticTaxCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOLine.disableAutomaticTaxCalculation>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryLineAmt" />
  public abstract class curyLineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyLineAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.LineAmt" />
  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.lineAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryOpenAmt" />
  public abstract class curyOpenAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyOpenAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OpenAmt" />
  public abstract class openAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.openAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryBilledAmt" />
  public abstract class curyBilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyBilledAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BilledAmt" />
  public abstract class billedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.billedAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryUnbilledAmt" />
  public abstract class curyUnbilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyUnbilledAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.UnbilledAmt" />
  public abstract class unbilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.unbilledAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryDiscPrice" />
  public abstract class curyDiscPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyDiscPrice>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DiscPrice" />
  public abstract class discPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.discPrice>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DiscountsAppliedToLine" />
  public abstract class discountsAppliedToLine : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLine.discountsAppliedToLine>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DiscountsAppliedToLine" />
  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine.groupDiscountRate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DocumentDiscountRate" />
  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine.documentDiscountRate>
  {
  }

  public abstract class avgCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.avgCost>
  {
  }

  public abstract class inventorySource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.inventorySource>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ProjectID" />
  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.projectID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ReasonCode" />
  public abstract class reasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.reasonCode>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.SalesPersonID" />
  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.salesPersonID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.SalesAcctID" />
  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.salesAcctID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.SalesSubID" />
  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.salesSubID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.TaskID" />
  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.taskID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CostCodeID" />
  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.costCodeID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOLine.noteID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.Commissionable" />
  public abstract class commissionable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.commissionable>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOLine.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOLine.Tstamp>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.AutoCreateIssueLine" />
  public abstract class autoCreateIssueLine : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOLine.autoCreateIssueLine>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ExpireDate" />
  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLine.expireDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.IsLegacyDropShip" />
  public abstract class isLegacyDropShip : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.isLegacyDropShip>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DiscountID" />
  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.discountID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DiscountSequenceID" />
  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLine.discountSequenceID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.POCreate" />
  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.pOCreate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.IsSpecialOrder" />
  public abstract class isSpecialOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.isSpecialOrder>
  {
  }

  public abstract class origIsSpecialOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.origIsSpecialOrder>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.costCenterID>
  {
  }

  public abstract class isCostUpdatedOnPO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.isCostUpdatedOnPO>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.POSource" />
  public abstract class pOSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.pOSource>
  {
    public sealed class SOLineBlanketPOSourceExtension : PXCacheExtension<SOLine>
    {
      public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.blanketPO>();

      [PXMergeAttributes]
      [PXDBString]
      [INReplenishmentSource.SOListWithBlankets]
      public string POSource { get; set; }

      public abstract class pOSource : IBqlField, IBqlOperand
      {
      }
    }
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.POCreated" />
  public abstract class pOCreated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.pOCreated>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.POOrderStatus" />
  public abstract class pOOrderStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.pOOrderStatus>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.POOrderType" />
  public abstract class pOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.pOOrderType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.POOrderNbr" />
  public abstract class pOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.pOOrderNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.POLineNbr" />
  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.pOLineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.POLinkActive" />
  public abstract class pOLinkActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.pOLinkActive>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.IsPOLinkAllowed" />
  public abstract class isPOLinkAllowed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.isPOLinkAllowed>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.VendorID" />
  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.vendorID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.POSiteID" />
  public abstract class pOSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.pOSiteID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DRTermStartDate" />
  public abstract class dRTermStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOLine.dRTermStartDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DRTermEndDate" />
  public abstract class dRTermEndDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLine.dRTermEndDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ItemRequiresTerms" />
  public abstract class itemRequiresTerms : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.itemRequiresTerms>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ItemHasResidual" />
  public abstract class itemHasResidual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.itemHasResidual>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryUnitPriceDR" />
  public abstract class curyUnitPriceDR : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyUnitPriceDR>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DiscPctDR" />
  public abstract class discPctDR : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.discPctDR>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DefScheduleID" />
  public abstract class defScheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.defScheduleID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.IsCut" />
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R2.")]
  public abstract class isCut : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.isCut>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.IntercompanyPOLineNbr" />
  public abstract class intercompanyPOLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOLine.intercompanyPOLineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.SubstitutionRequired" />
  public abstract class substitutionRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOLine.substitutionRequired>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BlanketType" />
  public abstract class blanketType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.blanketType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BlanketNbr" />
  public abstract class blanketNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.blanketNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BlanketLineNbr" />
  public abstract class blanketLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.blanketLineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BlanketSplitLineNbr" />
  public abstract class blanketSplitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.blanketSplitLineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.QtyOnOrders" />
  public abstract class qtyOnOrders : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.qtyOnOrders>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BaseQtyOnOrders" />
  public abstract class baseQtyOnOrders : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.baseQtyOnOrders>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CustomerOrderNbr" />
  public abstract class customerOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.customerOrderNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.SchedOrderDate" />
  public abstract class schedOrderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLine.schedOrderDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.SchedShipDate" />
  public abstract class schedShipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLine.schedShipDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.POCreateDate" />
  public abstract class pOCreateDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLine.pOCreateDate>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BlanketOpenQty" />
  public abstract class blanketOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.blanketOpenQty>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ChildLineCntr" />
  public abstract class childLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.childLineCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.OpenChildLineCntr" />
  public abstract class openChildLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine.openChildLineCntr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.Cancelled" />
  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.cancelled>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ShipVia" />
  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.shipVia>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.FOBPoint" />
  public abstract class fOBPoint : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.fOBPoint>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ShipTermsID" />
  public abstract class shipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.shipTermsID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ShipZoneID" />
  public abstract class shipZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine.shipZoneID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryTaxableAmt" />
  public abstract class curyTaxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyTaxableAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.TaxableAmt" />
  public abstract class taxableAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.taxableAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryNetSales" />
  public abstract class curyNetSales : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyNetSales>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.NetSales" />
  public abstract class netSales : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.netSales>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryMarginAmt" />
  public abstract class curyMarginAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.curyMarginAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.MarginAmt" />
  public abstract class marginAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.marginAmt>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.MarginPct" />
  public abstract class marginPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine.marginPct>
  {
  }

  public abstract class isOrchestratedLine : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine.isOrchestratedLine>
  {
  }

  public abstract class orchestrationOriginalLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOLine.orchestrationOriginalLineNbr>
  {
  }

  public abstract class orchestrationOriginalSiteID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOLine.orchestrationOriginalSiteID>
  {
  }

  public abstract class orchestrationPlanID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLine.orchestrationPlanID>
  {
  }
}
