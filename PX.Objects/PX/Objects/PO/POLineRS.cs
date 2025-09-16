// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLineRS
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
using PX.Objects.Common.Discount;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.PO;

/// <exclude />
[PXProjection(typeof (Select2<POLine, InnerJoin<POOrder, On<POLine.orderType, Equal<POOrder.orderType>, And<POLine.orderNbr, Equal<POOrder.orderNbr>>>, LeftJoin<POAccrualStatus, On<POAccrualStatus.type, Equal<PX.Objects.PO.POAccrualType.order>, And<POAccrualStatus.refNoteID, Equal<POLine.orderNoteID>, And<POAccrualStatus.lineNbr, Equal<POLine.lineNbr>>>>>>>), Persistent = false)]
[PXCacheName("PO Line")]
[Serializable]
public class POLineRS : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAPTranSource, ISortOrder
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
  protected string _Status;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected DateTime? _OrderDate;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected string _LotSerialNbr;
  protected string _UOM;
  protected Decimal? _OrderQty;
  protected Decimal? _BaseOrderQty;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected Decimal? _CuryUnitCost;
  protected Decimal? _UnitCost;
  protected Decimal? _DiscPct;
  protected Decimal? _CuryDiscAmt;
  protected Decimal? _DiscAmt;
  protected Decimal? _CuryLineAmt;
  protected Decimal? _LineAmt;
  protected Decimal? _CuryExtCost;
  protected Decimal? _ExtCost;
  protected Decimal? _GroupDiscountRate;
  protected Decimal? _DocumentDiscountRate;
  protected string _TaxCategoryID;
  protected string _TaxID;
  protected int? _ExpenseAcctID;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected int? _ExpenseSubID;
  protected string _TranDesc;
  protected int? _CostCodeID;
  protected bool? _Cancelled;
  protected string _DiscountID;
  protected string _DiscountSequenceID;
  protected DateTime? _DRTermStartDate;
  protected DateTime? _DRTermEndDate;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [Branch(null, null, true, true, true, BqlField = typeof (POLine.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (POLine.orderType))]
  [PXUIField(DisplayName = "Order Type")]
  [POOrderType.List]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (POLine.orderNbr))]
  [PXUIField(DisplayName = "Order Nbr.")]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (POLine.lineNbr))]
  [PXUIField]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXUIField(DisplayName = "Line Order", Visible = false, Enabled = false)]
  [PXDBInt(BqlField = typeof (POLine.sortOrder))]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBBool(BqlField = typeof (POLine.isStockItem))]
  public virtual bool? IsStockItem { get; set; }

  [POLineInventoryItem(Filterable = true, BqlField = typeof (POLine.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBBool(BqlField = typeof (POLine.accrueCost))]
  public virtual bool? AccrueCost { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POLine.lineType))]
  [POLineTypeList2(typeof (POLine.orderType), typeof (POLine.inventoryID))]
  [PXUIField(DisplayName = "Line Type")]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (POOrder.status))]
  [PXUIField]
  [POOrderStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [POVendor(BqlField = typeof (POLine.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  /// <summary>
  /// A reference to the <see cref="T:PX.Objects.AP.Vendor" />.
  /// </summary>
  /// <value>
  /// An integer identifier of the vendor, whom the AP bill will belong to.
  /// </value>
  [PXFormula(typeof (Validate<POLineRS.curyID>))]
  [POOrderPayToVendor(CacheGlobal = true, Filterable = true, BqlField = typeof (POOrder.payToVendorID))]
  [PXForeignReference(typeof (Field<POLineRS.payToVendorID>.IsRelatedTo<PX.Objects.AP.Vendor.bAccountID>))]
  public virtual int? PayToVendorID { get; set; }

  [LocationActive(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Location.bAccountID, Equal<BqlField<POLineRS.vendorID, IBqlInt>.FromCurrent>>>>>.And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (POOrder.taxZoneID))]
  public virtual string TaxZoneID { get; set; }

  [PXDBDate(BqlField = typeof (POLine.orderDate))]
  [PXUIField(DisplayName = "Order Date", Enabled = false)]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [SubItem(typeof (POLineRS.inventoryID), BqlField = typeof (POLine.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [POSiteAvail(typeof (POLineRS.inventoryID), typeof (POLineRS.subItemID), typeof (POLineRS.costCenterID), BqlField = typeof (POLine.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(100, IsUnicode = true, BqlField = typeof (POLine.lotSerialNbr))]
  [PXUIField(DisplayName = "Lot Serial Number", Visible = false)]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [INUnit(typeof (POLine.inventoryID), DisplayName = "UOM", BqlField = typeof (POLine.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (POLineRS.uOM), typeof (POLineRS.baseOrderQty), BqlField = typeof (POLine.orderQty))]
  [PXUIField]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXUIField(DisplayName = "Base Order Qty.", Visible = false, Enabled = false)]
  [PXDBDecimal(6, BqlField = typeof (POLine.baseOrderQty))]
  public virtual Decimal? BaseOrderQty
  {
    get => this._BaseOrderQty;
    set => this._BaseOrderQty = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (POOrder.curyID))]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBLong(BqlField = typeof (POLine.curyInfoID))]
  [CurrencyInfo]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrencyPriceCost(typeof (POLineRS.curyInfoID), typeof (POLineRS.unitCost), BqlField = typeof (POLine.curyUnitCost))]
  [PXUIField]
  public virtual Decimal? CuryUnitCost
  {
    get => this._CuryUnitCost;
    set => this._CuryUnitCost = value;
  }

  [PXDBPriceCost(BqlField = typeof (POLine.unitCost))]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0, BqlField = typeof (POLine.discPct))]
  public virtual Decimal? DiscPct
  {
    get => this._DiscPct;
    set => this._DiscPct = value;
  }

  [PXDBCurrency(typeof (POLineRS.curyInfoID), typeof (POLineRS.discAmt), BqlField = typeof (POLine.curyDiscAmt))]
  [PXUIField(DisplayName = "Discount Amount")]
  public virtual Decimal? CuryDiscAmt
  {
    get => this._CuryDiscAmt;
    set => this._CuryDiscAmt = value;
  }

  [PXDBDecimal(4, BqlField = typeof (POLine.discAmt))]
  public virtual Decimal? DiscAmt
  {
    get => this._DiscAmt;
    set => this._DiscAmt = value;
  }

  [PXDBCurrency(typeof (POLineRS.curyInfoID), typeof (POLineRS.lineAmt), BqlField = typeof (POLine.curyLineAmt))]
  [PXUIField(DisplayName = "Ext. Cost")]
  public virtual Decimal? CuryLineAmt
  {
    get => this._CuryLineAmt;
    set => this._CuryLineAmt = value;
  }

  [PXDBDecimal(4, BqlField = typeof (POLine.lineAmt))]
  public virtual Decimal? LineAmt
  {
    get => this._LineAmt;
    set => this._LineAmt = value;
  }

  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0, BqlField = typeof (POLine.retainagePct))]
  public virtual Decimal? RetainagePct { get; set; }

  [PXDBDecimal(BqlTable = typeof (POLine))]
  public virtual Decimal? RcptQtyThreshold { get; set; }

  [PXDBCurrency(typeof (POLineRS.curyInfoID), typeof (POLineRS.retainageAmt), BqlField = typeof (POLine.curyRetainageAmt))]
  public virtual Decimal? CuryRetainageAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (POLine.retainageAmt))]
  public virtual Decimal? RetainageAmt { get; set; }

  [PXDBCurrency(typeof (POLineRS.curyInfoID), typeof (POLineRS.extCost), MinValue = 0.0, BqlField = typeof (POLine.curyExtCost))]
  [PXUIField]
  public virtual Decimal? CuryExtCost
  {
    get => this._CuryExtCost;
    set => this._CuryExtCost = value;
  }

  [PXDBBaseCury(BqlField = typeof (POLine.extCost))]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? ExtCost
  {
    get => this._ExtCost;
    set => this._ExtCost = value;
  }

  [PXDBDecimal(6, BqlField = typeof (POLine.groupDiscountRate))]
  public virtual Decimal? GroupDiscountRate
  {
    get => this._GroupDiscountRate;
    set => this._GroupDiscountRate = value;
  }

  [PXDBDecimal(6, BqlField = typeof (POLine.documentDiscountRate))]
  public virtual Decimal? DocumentDiscountRate
  {
    get => this._DocumentDiscountRate;
    set => this._DocumentDiscountRate = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POLine.taxCategoryID))]
  [PXUIField]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (POLine.taxID))]
  [PXUIField(DisplayName = "Tax ID", Visible = false)]
  [PXSelector(typeof (PX.Objects.TX.Tax.taxID), DescriptionField = typeof (PX.Objects.TX.Tax.descr))]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [Account(typeof (POLineRS.branchID))]
  public virtual int? ExpenseAcctID
  {
    get => this._ExpenseAcctID;
    set => this._ExpenseAcctID = value;
  }

  [ProjectBase(BqlField = typeof (POLine.projectID))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [ActiveOrInPlanningProjectTask(typeof (POLineRS.projectID), "PO", DisplayName = "Project Task", BqlField = typeof (POLine.taskID))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [SubAccount(typeof (POLineRS.expenseAcctID), typeof (POLineRS.branchID), false)]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  [Account(typeof (POLineRS.branchID), DescriptionField = typeof (PX.Objects.GL.Account.description), DisplayName = "Accrual Account", Filterable = false, BqlField = typeof (POLine.pOAccrualAcctID))]
  public virtual int? POAccrualAcctID { get; set; }

  [SubAccount(typeof (POLineRS.pOAccrualAcctID), typeof (POLineRS.branchID), false, DisplayName = "Accrual Sub.", Filterable = true, BqlField = typeof (POLine.pOAccrualSubID))]
  [PXDefault]
  public virtual int? POAccrualSubID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (POLine.tranDesc))]
  [PXUIField]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [CostCode(typeof (POLineRS.expenseAcctID), typeof (POLineRS.taskID), "E", BqlField = typeof (POLine.costCodeID))]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [PXDBBool(BqlField = typeof (POLine.cancelled))]
  [PXUIField]
  public virtual bool? Cancelled
  {
    get => this._Cancelled;
    set => this._Cancelled = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (POLine.completePOLine))]
  public virtual string CompletePOLine { get; set; }

  [PXDBBool(BqlField = typeof (POLine.closed))]
  [PXUIField]
  public virtual bool? Closed { get; set; }

  /// <summary>
  /// A flag that indicates (if set to <c>true</c>) that a PO Line is fully billed.
  /// </summary>
  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<POLine.completedQty, Greater<POLine.billedQty>>, False>, Switch<Case<Where<POLine.completePOLine, Equal<CompletePOLineTypes.quantity>>, Switch<Case<Where<POLine.orderQty, LessEqual<decimal0>, Or<Div<Mult<POLine.orderQty, POLine.rcptQtyThreshold>, decimal100>, Greater<POLine.billedQty>>>, False>, True>>, Switch<Case<Where<POLine.curyExtCost, Greater<decimal0>, And<Div<Mult<Add<POLine.curyExtCost, POLine.curyRetainageAmt>, POLine.rcptQtyThreshold>, decimal100>, Greater<POLine.curyBilledAmt>>>, False>, Switch<Case<Where<POLine.curyExtCost, Less<decimal0>, And<Div<Mult<Add<POLine.curyExtCost, POLine.curyRetainageAmt>, POLine.rcptQtyThreshold>, decimal100>, Less<POLine.curyBilledAmt>>>, False>, True>>>>), typeof (bool))]
  public virtual bool? Billed { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (POLine.pOAccrualType))]
  public virtual string POAccrualType { get; set; }

  [PXDBGuid(false, BqlField = typeof (POLine.orderNoteID))]
  public virtual Guid? OrderNoteID { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (POLine.discountID))]
  [PXSelector(typeof (Search<APDiscount.discountID, Where<APDiscount.bAccountID, Equal<Current<POLineRS.vendorID>>, And<APDiscount.type, Equal<DiscountType.LineDiscount>>>>))]
  [PXUIField(DisplayName = "Discount Code", Visible = true, Enabled = false)]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (POLine.discountSequenceID))]
  [PXUIField(DisplayName = "Discount Sequence", Visible = false, Enabled = false)]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBGuid(false, BqlField = typeof (POAccrualStatus.refNoteID))]
  public virtual Guid? RefNoteID { get; set; }

  [PXDBString(6, IsUnicode = true, BqlField = typeof (POAccrualStatus.billedUOM))]
  public virtual string BilledUOM { get; set; }

  [PXDBQuantity(BqlField = typeof (POAccrualStatus.billedQty))]
  [PXUIField(DisplayName = "Billed Qty.", Enabled = false)]
  public virtual Decimal? BilledQty { get; set; }

  [PXDBCalced(typeof (IsNull<POAccrualStatus.baseBilledQty, decimal0>), typeof (Decimal))]
  [PXQuantity]
  public virtual Decimal? BaseBilledQty { get; set; }

  [PXDBCalced(typeof (IsNull<POAccrualStatus.curyBilledAmt, decimal0>), typeof (Decimal))]
  [PXBaseCury]
  [PXUIField(DisplayName = "Billed Amount", Enabled = false)]
  public virtual Decimal? CuryBilledAmt { get; set; }

  [PXDBCalced(typeof (IsNull<POAccrualStatus.billedAmt, decimal0>), typeof (Decimal))]
  [PXBaseCury]
  public virtual Decimal? BilledAmt { get; set; }

  [PXDBCalced(typeof (IsNull<POAccrualStatus.curyBilledCost, decimal0>), typeof (Decimal))]
  [PXBaseCury]
  public virtual Decimal? CuryBilledCost { get; set; }

  [PXDBCalced(typeof (IsNull<POAccrualStatus.billedCost, decimal0>), typeof (Decimal))]
  [PXBaseCury]
  public virtual Decimal? BilledCost { get; set; }

  [PXDBCalced(typeof (IsNull<POAccrualStatus.curyBilledDiscAmt, decimal0>), typeof (Decimal))]
  [PXBaseCury]
  public virtual Decimal? CuryBilledDiscAmt { get; set; }

  [PXDBCalced(typeof (IsNull<POAccrualStatus.billedDiscAmt, decimal0>), typeof (Decimal))]
  [PXBaseCury]
  public virtual Decimal? BilledDiscAmt { get; set; }

  [PXDBString(6, IsUnicode = true, BqlField = typeof (POAccrualStatus.receivedUOM))]
  public virtual string ReceivedUOM { get; set; }

  [PXDBQuantity(BqlField = typeof (POAccrualStatus.receivedQty))]
  public virtual Decimal? ReceivedQty { get; set; }

  [PXDBCalced(typeof (IsNull<POAccrualStatus.baseReceivedQty, decimal0>), typeof (Decimal))]
  [PXQuantity]
  public virtual Decimal? BaseReceivedQty { get; set; }

  [PXDBCalced(typeof (IsNull<POAccrualStatus.receivedCost, decimal0>), typeof (Decimal))]
  [PXBaseCury]
  public virtual Decimal? ReceivedCost { get; set; }

  [PXDBCalced(typeof (IsNull<POAccrualStatus.pPVAmt, decimal0>), typeof (Decimal))]
  [PXBaseCury]
  public virtual Decimal? PPVAmt { get; set; }

  [PXDBCurrency(typeof (POLineRS.curyInfoID), typeof (POLineRS.orderBilledAmt), BqlField = typeof (POLine.curyBilledAmt))]
  public virtual Decimal? CuryOrderBilledAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (POLine.billedAmt))]
  public virtual Decimal? OrderBilledAmt { get; set; }

  [PXDBQuantity(typeof (POLineRS.uOM), typeof (POLineRS.baseOrderBilledQty), HandleEmptyKey = true, BqlField = typeof (POLine.billedQty))]
  public virtual Decimal? OrderBilledQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POLine.baseBilledQty))]
  public virtual Decimal? BaseOrderBilledQty { get; set; }

  [PXDBQuantity(typeof (POLineRS.uOM), typeof (POLineRS.baseUnbilledQty), HandleEmptyKey = true, BqlField = typeof (POLine.unbilledQty))]
  [PXUIField(DisplayName = "Unbilled Qty.", Enabled = false)]
  public virtual Decimal? UnbilledQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POLine.baseUnbilledQty))]
  public virtual Decimal? BaseUnbilledQty { get; set; }

  [PXDBCurrency(typeof (POLineRS.curyInfoID), typeof (POLineRS.unbilledAmt), BqlField = typeof (POLine.curyUnbilledAmt))]
  [PXUIField(DisplayName = "Unbilled Amount", Enabled = false)]
  public virtual Decimal? CuryUnbilledAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (POLine.unbilledAmt))]
  public virtual Decimal? UnbilledAmt { get; set; }

  [PXDBQuantity(BqlField = typeof (POLine.reqPrepaidQty))]
  public virtual Decimal? ReqPrepaidQty { get; set; }

  [PXDBCurrency(typeof (POLineRS.curyInfoID), typeof (POLineRS.reqPrepaidAmt), BqlField = typeof (POLine.curyReqPrepaidAmt))]
  public virtual Decimal? CuryReqPrepaidAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (POLine.reqPrepaidAmt))]
  public virtual Decimal? ReqPrepaidAmt { get; set; }

  [PXDBDate(BqlField = typeof (POLine.dRTermStartDate))]
  public DateTime? DRTermStartDate
  {
    get => this._DRTermStartDate;
    set => this._DRTermStartDate = value;
  }

  [PXDBDate(BqlField = typeof (POLine.dRTermEndDate))]
  public DateTime? DRTermEndDate
  {
    get => this._DRTermEndDate;
    set => this._DRTermEndDate = value;
  }

  [PXDBString(1, BqlField = typeof (POLine.dropshipExpenseRecording))]
  public virtual string DropshipExpenseRecording { get; set; }

  [PXDBInt(BqlField = typeof (POLine.costCenterID))]
  public virtual int? CostCenterID { get; set; }

  string IAPTranSource.OrigUOM => this.UOM;

  Decimal? IAPTranSource.OrigQty => this.OrderQty;

  Decimal? IAPTranSource.BaseOrigQty => this.BaseOrderQty;

  Decimal? IAPTranSource.BillQty
  {
    get
    {
      if (!this.RefNoteID.HasValue)
        return this.OrderQty;
      Decimal? billQty = this.ReceivedQty;
      Decimal num1 = 0M;
      Decimal? nullable1;
      if (!(billQty.GetValueOrDefault() == num1 & billQty.HasValue) && !(this.ReceivedUOM == this.UOM))
      {
        billQty = new Decimal?();
        nullable1 = billQty;
      }
      else
        nullable1 = this.ReceivedQty;
      Decimal? nullable2 = nullable1;
      if (!nullable2.HasValue)
      {
        billQty = new Decimal?();
        return billQty;
      }
      billQty = this.BilledQty;
      Decimal num2 = 0M;
      Decimal? nullable3;
      if (!(billQty.GetValueOrDefault() == num2 & billQty.HasValue) && !(this.BilledUOM == this.UOM))
      {
        billQty = new Decimal?();
        nullable3 = billQty;
      }
      else
        nullable3 = this.BilledQty;
      Decimal? nullable4 = nullable3;
      if (!nullable4.HasValue)
      {
        billQty = new Decimal?();
        return billQty;
      }
      Decimal? orderQty = this.OrderQty;
      Decimal? nullable5 = nullable2;
      billQty = orderQty.GetValueOrDefault() < nullable5.GetValueOrDefault() & orderQty.HasValue & nullable5.HasValue ? nullable2 : this.OrderQty;
      Decimal? nullable6 = nullable4;
      Decimal? nullable7 = billQty.HasValue & nullable6.HasValue ? new Decimal?(billQty.GetValueOrDefault() - nullable6.GetValueOrDefault()) : new Decimal?();
      nullable6 = nullable7;
      Decimal num3 = 0M;
      return !(nullable6.GetValueOrDefault() < num3 & nullable6.HasValue) ? nullable7 : new Decimal?(0M);
    }
  }

  Decimal? IAPTranSource.BaseBillQty
  {
    get
    {
      if (!this.RefNoteID.HasValue)
        return this.BaseOrderQty;
      Decimal? baseOrderQty = this.BaseOrderQty;
      Decimal? nullable1 = this.BaseReceivedQty;
      Decimal? nullable2 = baseOrderQty.GetValueOrDefault() < nullable1.GetValueOrDefault() & baseOrderQty.HasValue & nullable1.HasValue ? this.BaseReceivedQty : this.BaseOrderQty;
      Decimal? nullable3 = this.BaseBilledQty;
      Decimal? nullable4;
      if (!(nullable2.HasValue & nullable3.HasValue))
      {
        nullable1 = new Decimal?();
        nullable4 = nullable1;
      }
      else
        nullable4 = new Decimal?(nullable2.GetValueOrDefault() - nullable3.GetValueOrDefault());
      Decimal? nullable5 = nullable4;
      nullable3 = nullable5;
      Decimal num = 0M;
      return !(nullable3.GetValueOrDefault() < num & nullable3.HasValue) ? nullable5 : new Decimal?(0M);
    }
  }

  bool IAPTranSource.IsPartiallyBilled
  {
    [PXDependsOnFields(new System.Type[] {typeof (POLineRS.baseBilledQty), typeof (POLineRS.curyBilledCost)})] get
    {
      Decimal? baseBilledQty = this.BaseBilledQty;
      Decimal num1 = 0M;
      if (!(baseBilledQty.GetValueOrDefault() == num1 & baseBilledQty.HasValue))
        return true;
      Decimal? curyBilledCost = this.CuryBilledCost;
      Decimal num2 = 0M;
      return !(curyBilledCost.GetValueOrDefault() == num2 & curyBilledCost.HasValue);
    }
  }

  Guid? IAPTranSource.POAccrualRefNoteID
  {
    get => !(this.POAccrualType == "O") ? new Guid?() : this.OrderNoteID;
  }

  int? IAPTranSource.POAccrualLineNbr => !(this.POAccrualType == "O") ? new int?() : this.LineNbr;

  public virtual bool CompareReferenceKey(PX.Objects.AP.APTran aTran)
  {
    if (aTran.POAccrualType == this.POAccrualType)
    {
      Guid? accrualRefNoteId1 = aTran.POAccrualRefNoteID;
      Guid? accrualRefNoteId2 = ((IAPTranSource) this).POAccrualRefNoteID;
      if ((accrualRefNoteId1.HasValue == accrualRefNoteId2.HasValue ? (accrualRefNoteId1.HasValue ? (accrualRefNoteId1.GetValueOrDefault() == accrualRefNoteId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        int? poAccrualLineNbr1 = aTran.POAccrualLineNbr;
        int? poAccrualLineNbr2 = ((IAPTranSource) this).POAccrualLineNbr;
        return poAccrualLineNbr1.GetValueOrDefault() == poAccrualLineNbr2.GetValueOrDefault() & poAccrualLineNbr1.HasValue == poAccrualLineNbr2.HasValue;
      }
    }
    return false;
  }

  public virtual void SetReferenceKeyTo(PX.Objects.AP.APTran aTran)
  {
    aTran.POAccrualType = this.POAccrualType;
    aTran.PONbr = this.OrderNbr;
    aTran.POOrderType = this.OrderType;
    aTran.POLineNbr = this.LineNbr;
    aTran.POAccrualRefNoteID = ((IAPTranSource) this).POAccrualRefNoteID;
    aTran.POAccrualLineNbr = ((IAPTranSource) this).POAccrualLineNbr;
  }

  public virtual bool IsReturn => false;

  public virtual bool AggregateWithExistingTran => false;

  public virtual bool? AllowEditUnitCost => new bool?(false);

  public class PK : 
    PrimaryKeyOf<POLineRS>.By<POLineRS.orderType, POLineRS.orderNbr, POLineRS.lineNbr>
  {
    public static POLineRS Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (POLineRS) PrimaryKeyOf<POLineRS>.By<POLineRS.orderType, POLineRS.orderNbr, POLineRS.lineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<POLineRS>.By<POLineRS.branchID>
    {
    }

    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POLineRS>.By<POLineRS.orderType, POLineRS.orderNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POLineRS>.By<POLineRS.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<POLineRS>.By<POLineRS.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<POLineRS>.By<POLineRS.siteID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<POLineRS>.By<POLineRS.vendorID>
    {
    }

    public class PayToVendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<POLineRS>.By<POLineRS.payToVendorID>
    {
    }

    public class VendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<POLineRS>.By<POLineRS.vendorID, POLineRS.vendorLocationID>
    {
    }

    public class TaxZone : 
      PrimaryKeyOf<PX.Objects.TX.TaxZone>.By<PX.Objects.TX.TaxZone.taxZoneID>.ForeignKeyOf<POLineRS>.By<POLineRS.taxZoneID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<POLineRS>.By<POLineRS.curyID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POLineRS>.By<POLineRS.curyInfoID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<POLineRS>.By<POLineRS.taxCategoryID>
    {
    }

    public class Tax : PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<POLineRS>.By<POLineRS.taxID>
    {
    }

    public class ExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<POLineRS>.By<POLineRS.expenseAcctID>
    {
    }

    public class ExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<POLineRS>.By<POLineRS.expenseSubID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<POLineRS>.By<POLineRS.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<POLineRS>.By<POLineRS.projectID, POLineRS.taskID>
    {
    }

    public class AccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<POLineRS>.By<POLineRS.pOAccrualAcctID>
    {
    }

    public class AccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<POLineRS>.By<POLineRS.pOAccrualSubID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<POLineRS>.By<POLineRS.costCodeID>
    {
    }

    public class Discount : 
      PrimaryKeyOf<APDiscount>.By<APDiscount.discountID, APDiscount.bAccountID>.ForeignKeyOf<POLineRS>.By<POLineRS.discountID, POLineRS.vendorID>
    {
    }

    public class DiscountSequence : 
      PrimaryKeyOf<VendorDiscountSequence>.By<VendorDiscountSequence.vendorID, VendorDiscountSequence.discountID, VendorDiscountSequence.discountSequenceID>.ForeignKeyOf<POLineRS>.By<POLineRS.vendorID, POLineRS.discountID, POLineRS.discountSequenceID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLineRS.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.branchID>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.sortOrder>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLineRS.isStockItem>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.inventoryID>
  {
  }

  public abstract class accrueCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLineRS.accrueCost>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.lineType>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.status>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.vendorID>
  {
  }

  public abstract class payToVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.payToVendorID>
  {
  }

  public abstract class vendorLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.vendorLocationID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.taxZoneID>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLineRS.orderDate>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.siteID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.lotSerialNbr>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.uOM>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.orderQty>
  {
  }

  public abstract class baseOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.baseOrderQty>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POLineRS.curyInfoID>
  {
  }

  public abstract class curyUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.curyUnitCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.unitCost>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.discPct>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.discAmt>
  {
  }

  public abstract class curyLineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.curyLineAmt>
  {
  }

  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.lineAmt>
  {
  }

  public abstract class retainagePct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.retainagePct>
  {
  }

  public abstract class rcptQtyThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineRS.rcptQtyThreshold>
  {
  }

  public abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineRS.curyRetainageAmt>
  {
  }

  public abstract class retainageAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.retainageAmt>
  {
  }

  public abstract class curyExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.extCost>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineRS.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineRS.documentDiscountRate>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.taxCategoryID>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.taxID>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.expenseAcctID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.taskID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.expenseSubID>
  {
  }

  public abstract class pOAccrualAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.pOAccrualAcctID>
  {
  }

  public abstract class pOAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.pOAccrualSubID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.tranDesc>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.costCodeID>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLineRS.cancelled>
  {
  }

  public abstract class completePOLine : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.completePOLine>
  {
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLineRS.closed>
  {
  }

  public abstract class billed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLineRS.billed>
  {
  }

  public abstract class pOAccrualType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.pOAccrualType>
  {
  }

  public abstract class orderNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLineRS.orderNoteID>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLineRS.discountSequenceID>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLineRS.refNoteID>
  {
  }

  public abstract class billedUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.billedUOM>
  {
  }

  public abstract class billedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.billedQty>
  {
  }

  public abstract class baseBilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.baseBilledQty>
  {
  }

  public abstract class curyBilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.curyBilledAmt>
  {
  }

  public abstract class billedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.billedAmt>
  {
  }

  public abstract class curyBilledCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.curyBilledCost>
  {
  }

  public abstract class billedCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.billedCost>
  {
  }

  public abstract class curyBilledDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineRS.curyBilledDiscAmt>
  {
  }

  public abstract class billedDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.billedDiscAmt>
  {
  }

  public abstract class receivedUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLineRS.receivedUOM>
  {
  }

  public abstract class receivedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.receivedQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineRS.baseReceivedQty>
  {
  }

  public abstract class receivedCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.receivedCost>
  {
  }

  public abstract class pPVAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.pPVAmt>
  {
  }

  public abstract class curyOrderBilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineRS.curyOrderBilledAmt>
  {
  }

  public abstract class orderBilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.orderBilledAmt>
  {
  }

  public abstract class orderBilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.orderBilledQty>
  {
  }

  public abstract class baseOrderBilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineRS.baseOrderBilledQty>
  {
  }

  public abstract class unbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.unbilledQty>
  {
  }

  public abstract class baseUnbilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineRS.baseUnbilledQty>
  {
  }

  public abstract class curyUnbilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineRS.curyUnbilledAmt>
  {
  }

  public abstract class unbilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.unbilledAmt>
  {
  }

  public abstract class reqPrepaidQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.reqPrepaidQty>
  {
  }

  public abstract class curyReqPrepaidAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLineRS.curyReqPrepaidAmt>
  {
  }

  public abstract class reqPrepaidAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLineRS.reqPrepaidAmt>
  {
  }

  public abstract class dRTermStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLineRS.dRTermStartDate>
  {
  }

  public abstract class dRTermEndDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLineRS.dRTermEndDate>
  {
  }

  public abstract class dropshipExpenseRecording : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLineRS.dropshipExpenseRecording>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLineRS.costCenterID>
  {
  }
}
