// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLineS
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.PO;

/// <summary>Read-only class for selector</summary>
[PXProjection(typeof (Select2<POReceiptLine, LeftJoin<POLine, On<POLine.orderType, Equal<POReceiptLine.pOType>, And<POLine.orderNbr, Equal<POReceiptLine.pONbr>, And<POLine.lineNbr, Equal<POReceiptLine.pOLineNbr>>>>, LeftJoin<POOrder, On<POOrder.orderType, Equal<POReceiptLine.pOType>, And<POOrder.orderNbr, Equal<POReceiptLine.pONbr>>>, LeftJoin<POReceipt, On<POReceiptLine.FK.Receipt>>>>>), Persistent = false)]
[PXCacheName("Purchase Receipt Line")]
[Serializable]
public class POReceiptLineS : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAPTranSource,
  ISortOrder
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected 
  #nullable disable
  string _ReceiptNbr;
  protected string _ReceiptType;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected string _LineType;
  protected int? _InventoryID;
  protected int? _VendorID;
  protected DateTime? _ReceiptDate;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected string _UOM;
  protected Decimal? _ReceiptQty;
  protected Decimal? _BaseReceiptQty;
  protected string _CuryID;
  protected long? _OrderCuryInfoID;
  protected long? _ReceiptCuryInfoID;
  protected Decimal? _CuryOrderUnitCost;
  protected Decimal? _OrderUnitCost;
  protected Decimal? _CuryExtCost;
  protected Decimal? _ExtCost;
  protected Decimal? _OrderDiscPct;
  protected Decimal? _ReceiptDiscPct;
  protected Decimal? _CuryReceiptDiscAmt;
  protected Decimal? _UnbilledQty;
  protected Decimal? _BaseUnbilledQty;
  protected Decimal? _GroupDiscountRate;
  protected Decimal? _DocumentDiscountRate;
  protected string _TaxCategoryID;
  protected int? _ExpenseAcctID;
  protected int? _ExpenseSubID;
  protected int? _POAccrualAcctID;
  protected int? _POAccrualSubID;
  protected string _TranDesc;
  protected string _TaxID;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected string _POType;
  protected string _PONbr;
  protected int? _POLineNbr;
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

  [Branch(null, null, true, true, true, BqlField = typeof (POReceiptLine.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POLine.orderType))]
  public virtual string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (POReceiptLine.receiptNbr))]
  [PXUIField(DisplayName = "Receipt Nbr.")]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (POReceiptLine.receiptType))]
  [PXUIField(DisplayName = "Receipt Type")]
  public virtual string ReceiptType
  {
    get => this._ReceiptType;
    set => this._ReceiptType = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (POReceiptLine.lineNbr))]
  [PXUIField]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(BqlField = typeof (POReceiptLine.sortOrder))]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POReceiptLine.lineType))]
  [POLineType.List]
  [PXUIField(DisplayName = "Line Type")]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDBBool(BqlField = typeof (POReceiptLine.isStockItem))]
  public virtual bool? IsStockItem { get; set; }

  [Inventory(Filterable = true, BqlField = typeof (POReceiptLine.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBBool(BqlField = typeof (POReceiptLine.accrueCost))]
  public virtual bool? AccrueCost { get; set; }

  [Vendor]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBDate(BqlField = typeof (POReceiptLine.receiptDate))]
  public virtual DateTime? ReceiptDate
  {
    get => this._ReceiptDate;
    set => this._ReceiptDate = value;
  }

  [SubItem(typeof (POReceiptLineS.inventoryID), BqlField = typeof (POReceiptLine.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [SiteAvail(typeof (POReceiptLineS.inventoryID), typeof (POReceiptLineS.subItemID), typeof (POReceiptLineS.costCenterID), BqlField = typeof (POReceiptLine.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [INUnit(typeof (POReceiptLineS.inventoryID), BqlField = typeof (POReceiptLine.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (POReceiptLineS.uOM), typeof (POReceiptLineS.baseReceiptQty), HandleEmptyKey = true, BqlField = typeof (POReceiptLine.receiptQty))]
  [PXUIField]
  public virtual Decimal? ReceiptQty
  {
    get => this._ReceiptQty;
    set => this._ReceiptQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (POReceiptLine.baseReceiptQty))]
  public virtual Decimal? BaseReceiptQty
  {
    get => this._BaseReceiptQty;
    set => this._BaseReceiptQty = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (POOrder.curyID))]
  [PXUIField]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceipt.ReturnInventoryCostMode" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (POReceipt.returnInventoryCostMode))]
  [PXUIField]
  public virtual string ReturnInventoryCostMode { get; set; }

  [PXDBLong(BqlField = typeof (POLine.curyInfoID))]
  public virtual long? OrderCuryInfoID
  {
    get => this._OrderCuryInfoID;
    set => this._OrderCuryInfoID = value;
  }

  [PXDBLong(BqlField = typeof (POReceiptLine.curyInfoID))]
  public virtual long? ReceiptCuryInfoID
  {
    get => this._ReceiptCuryInfoID;
    set => this._ReceiptCuryInfoID = value;
  }

  [PXDBDecimal(6, BqlField = typeof (POLine.curyUnitCost))]
  [PXUIField]
  public virtual Decimal? CuryOrderUnitCost
  {
    get => this._CuryOrderUnitCost;
    set => this._CuryOrderUnitCost = value;
  }

  [PXDBDecimal(6, BqlField = typeof (POLine.unitCost))]
  public virtual Decimal? OrderUnitCost
  {
    get => this._OrderUnitCost;
    set => this._OrderUnitCost = value;
  }

  [PXDBCurrency(typeof (POReceiptLineS.orderCuryInfoID), typeof (POReceiptLineS.extCost), BqlField = typeof (POLine.curyExtCost))]
  [PXUIField]
  public virtual Decimal? CuryExtCost
  {
    get => this._CuryExtCost;
    set => this._CuryExtCost = value;
  }

  [PXDBBaseCury(BqlField = typeof (POLine.extCost))]
  public virtual Decimal? ExtCost
  {
    get => this._ExtCost;
    set => this._ExtCost = value;
  }

  [PXDBCurrency(typeof (POReceiptLineS.orderCuryInfoID), typeof (POReceiptLineS.orderLineAmt), BqlField = typeof (POLine.curyLineAmt))]
  public virtual Decimal? CuryOrderLineAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (POLine.lineAmt))]
  public virtual Decimal? OrderLineAmt { get; set; }

  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0, BqlField = typeof (POLine.discPct))]
  [PXUIField(DisplayName = "Discount Percent")]
  public virtual Decimal? OrderDiscPct
  {
    get => this._OrderDiscPct;
    set => this._OrderDiscPct = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (POLine.completePOLine))]
  public virtual string CompletePOLine { get; set; }

  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0, BqlField = typeof (POReceiptLine.discPct))]
  [PXUIField(DisplayName = "Discount Percent")]
  public virtual Decimal? ReceiptDiscPct
  {
    get => this._ReceiptDiscPct;
    set => this._ReceiptDiscPct = value;
  }

  [PXDBCurrency(typeof (POReceiptLineS.orderCuryInfoID), typeof (POReceiptLineS.orderDiscAmt), BqlField = typeof (POLine.curyDiscAmt))]
  public virtual Decimal? CuryOrderDiscAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (POLine.discAmt))]
  public virtual Decimal? OrderDiscAmt { get; set; }

  [PXDBCurrency(typeof (POReceiptLineS.receiptCuryInfoID), typeof (POReceiptLineS.receiptDiscAmt), BqlField = typeof (POReceiptLine.curyDiscAmt))]
  public virtual Decimal? CuryReceiptDiscAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (POReceiptLine.discAmt))]
  public virtual Decimal? ReceiptDiscAmt { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POReceiptLine.curyUnitCost))]
  [PXUIField]
  public virtual Decimal? CuryReceiptUnitCost { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POReceiptLine.unitCost))]
  public virtual Decimal? ReceiptUnitCost { get; set; }

  [PXDBBaseCury(BqlField = typeof (POReceiptLine.curyExtCost))]
  public virtual Decimal? CuryReceiptExtCost { get; set; }

  [PXDBBaseCury(BqlField = typeof (POReceiptLine.extCost))]
  public virtual Decimal? ReceiptExtCost { get; set; }

  [PXDBQuantity(typeof (POReceiptLineS.uOM), typeof (POReceiptLineS.baseUnbilledQty), HandleEmptyKey = true, BqlField = typeof (POReceiptLine.unbilledQty))]
  [PXUIField(DisplayName = "Unbilled Qty.", Enabled = false)]
  public virtual Decimal? UnbilledQty
  {
    get => this._UnbilledQty;
    set => this._UnbilledQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (POReceiptLine.baseUnbilledQty))]
  public virtual Decimal? BaseUnbilledQty
  {
    get => this._BaseUnbilledQty;
    set => this._BaseUnbilledQty = value;
  }

  [INUnit(typeof (POReceiptLineS.inventoryID), BqlField = typeof (POLine.uOM))]
  public virtual string OrderUOM { get; set; }

  [PXDBQuantity(typeof (POReceiptLineS.orderUOM), typeof (POReceiptLineS.baseOrderQty), BqlField = typeof (POLine.orderQty))]
  [PXUIField(DisplayName = "Ordered Qty.")]
  public virtual Decimal? OrderQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POLine.baseOrderQty))]
  public virtual Decimal? BaseOrderQty { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0, BqlField = typeof (POLine.retainagePct))]
  [PXUIField(DisplayName = "Retainage Percent", FieldClass = "Retainage")]
  public virtual Decimal? RetainagePct { get; set; }

  [PXDBCurrency(typeof (POReceiptLineS.orderCuryInfoID), typeof (POReceiptLineS.retainageAmt), BqlField = typeof (POLine.curyRetainageAmt))]
  [PXUIField(DisplayName = "Retainage Amount", FieldClass = "Retainage")]
  public virtual Decimal? CuryRetainageAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (POLine.retainageAmt))]
  public virtual Decimal? RetainageAmt { get; set; }

  [PXDBCurrency(typeof (POReceiptLineS.orderCuryInfoID), typeof (POReceiptLineS.unbilledAmt), BqlField = typeof (POLine.curyUnbilledAmt))]
  [PXUIField(DisplayName = "Unbilled Amount", Enabled = false)]
  public virtual Decimal? CuryUnbilledAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (POLine.unbilledAmt))]
  public virtual Decimal? UnbilledAmt { get; set; }

  [PXDBDecimal(18, BqlField = typeof (POLine.groupDiscountRate))]
  public virtual Decimal? GroupDiscountRate
  {
    get => this._GroupDiscountRate;
    set => this._GroupDiscountRate = value;
  }

  [PXDBDecimal(18, BqlField = typeof (POLine.documentDiscountRate))]
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

  [PXDBInt(BqlField = typeof (POReceiptLine.expenseAcctID))]
  public virtual int? ExpenseAcctID
  {
    get => this._ExpenseAcctID;
    set => this._ExpenseAcctID = value;
  }

  [PXDBInt(BqlField = typeof (POReceiptLine.expenseSubID))]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  [PXDBInt(BqlField = typeof (POReceiptLine.pOAccrualAcctID))]
  public virtual int? POAccrualAcctID
  {
    get => this._POAccrualAcctID;
    set => this._POAccrualAcctID = value;
  }

  [PXDBInt(BqlField = typeof (POReceiptLine.pOAccrualSubID))]
  public virtual int? POAccrualSubID
  {
    get => this._POAccrualSubID;
    set => this._POAccrualSubID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (POReceiptLine.tranDesc))]
  [PXUIField]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [PXDBString(60, IsUnicode = true, BqlField = typeof (POLine.taxID))]
  [PXUIField(DisplayName = "Tax ID", Visible = false)]
  public virtual string TaxID
  {
    get => this._TaxID;
    set => this._TaxID = value;
  }

  [PXDBInt(BqlField = typeof (POReceiptLine.projectID))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBInt(BqlField = typeof (POReceiptLine.taskID))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBInt(BqlField = typeof (POReceiptLine.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (POReceiptLine.pOAccrualType))]
  public virtual string POAccrualType { get; set; }

  [PXDBGuid(false, BqlField = typeof (POReceiptLine.pOAccrualRefNoteID))]
  public virtual Guid? POAccrualRefNoteID { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.pOAccrualLineNbr))]
  public virtual int? POAccrualLineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POReceiptLine.pOType))]
  [POOrderType.List]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public virtual string POType
  {
    get => this._POType;
    set => this._POType = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POReceiptLine.pONbr))]
  [PXUIField(DisplayName = "Order Nbr.")]
  public virtual string PONbr
  {
    get => this._PONbr;
    set => this._PONbr = value;
  }

  [PXDBInt(BqlField = typeof (POReceiptLine.pOLineNbr))]
  [PXUIField(DisplayName = "PO Line Nbr.")]
  public virtual int? POLineNbr
  {
    get => this._POLineNbr;
    set => this._POLineNbr = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (POLine.discountID))]
  public virtual string DiscountID
  {
    get => this._DiscountID;
    set => this._DiscountID = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (POLine.discountSequenceID))]
  public virtual string DiscountSequenceID
  {
    get => this._DiscountSequenceID;
    set => this._DiscountSequenceID = value;
  }

  [PXDBBool(BqlField = typeof (POReceiptLine.allowEditUnitCost))]
  public virtual bool? AllowEditUnitCost { get; set; }

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

  [PXDBString(1, BqlField = typeof (POReceiptLine.dropshipExpenseRecording))]
  public virtual string DropshipExpenseRecording { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.costCenterID))]
  public virtual int? CostCenterID { get; set; }

  protected bool IsDirectReceipt => !this.OrderQty.HasValue;

  protected bool UseAmountsFromReceipt => this.AllowEditUnitCost.GetValueOrDefault();

  string IAPTranSource.OrigUOM
  {
    get
    {
      return !this.IsDirectReceipt && !this.UseAmountsFromReceipt && !(this.ReturnInventoryCostMode == "O") ? this.OrderUOM : this.UOM;
    }
  }

  public virtual Decimal? OrigQty
  {
    get
    {
      return !this.IsDirectReceipt && !this.UseAmountsFromReceipt && !(this.ReturnInventoryCostMode == "O") ? this.OrderQty : this.ReceiptQty;
    }
  }

  public virtual Decimal? BaseOrigQty
  {
    get
    {
      return !this.IsDirectReceipt && !this.UseAmountsFromReceipt && !(this.ReturnInventoryCostMode == "O") ? this.BaseOrderQty : this.BaseReceiptQty;
    }
  }

  Decimal? IAPTranSource.BillQty => this.UnbilledQty;

  Decimal? IAPTranSource.BaseBillQty => this.BaseUnbilledQty;

  long? IAPTranSource.CuryInfoID
  {
    get
    {
      return !this.IsDirectReceipt && !this.UseAmountsFromReceipt && !(this.ReturnInventoryCostMode == "O") ? this.OrderCuryInfoID : this.ReceiptCuryInfoID;
    }
  }

  Decimal? IAPTranSource.DiscPct
  {
    get
    {
      return !this.UseAmountsFromReceipt && !(this.ReturnInventoryCostMode == "O") ? this.OrderDiscPct : this.ReceiptDiscPct;
    }
  }

  Decimal? IAPTranSource.CuryDiscAmt
  {
    get
    {
      return !this.UseAmountsFromReceipt && !(this.ReturnInventoryCostMode == "O") ? this.CuryOrderDiscAmt : this.CuryReceiptDiscAmt;
    }
  }

  Decimal? IAPTranSource.DiscAmt
  {
    get
    {
      return !this.UseAmountsFromReceipt && !(this.ReturnInventoryCostMode == "O") ? this.OrderDiscAmt : this.ReceiptDiscAmt;
    }
  }

  Decimal? IAPTranSource.CuryLineAmt
  {
    get
    {
      return !this.IsDirectReceipt && !this.UseAmountsFromReceipt && !(this.ReturnInventoryCostMode == "O") ? this.CuryOrderLineAmt : this.CuryReceiptExtCost;
    }
  }

  Decimal? IAPTranSource.LineAmt
  {
    get
    {
      return !this.IsDirectReceipt && !this.UseAmountsFromReceipt && !(this.ReturnInventoryCostMode == "O") ? this.OrderLineAmt : this.ReceiptExtCost;
    }
  }

  Decimal? IAPTranSource.CuryUnitCost
  {
    get
    {
      return !this.IsDirectReceipt && !this.UseAmountsFromReceipt && !(this.ReturnInventoryCostMode == "O") ? this.CuryOrderUnitCost : this.CuryReceiptUnitCost;
    }
  }

  Decimal? IAPTranSource.UnitCost
  {
    get
    {
      return !this.IsDirectReceipt && !this.UseAmountsFromReceipt && !(this.ReturnInventoryCostMode == "O") ? this.OrderUnitCost : this.ReceiptUnitCost;
    }
  }

  bool IAPTranSource.IsReturn => this.ReceiptType == "RN";

  bool IAPTranSource.IsPartiallyBilled
  {
    get
    {
      Decimal? baseOrigQty = this.BaseOrigQty;
      Decimal? baseUnbilledQty = this.BaseUnbilledQty;
      return !(baseOrigQty.GetValueOrDefault() == baseUnbilledQty.GetValueOrDefault() & baseOrigQty.HasValue == baseUnbilledQty.HasValue);
    }
  }

  bool IAPTranSource.AggregateWithExistingTran => this.POAccrualType == "O";

  public virtual Decimal? CuryBilledAmt { get; set; }

  public virtual Decimal? BilledAmt { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.IAPTranSource.BilledQty" />
  public virtual Decimal? BilledQty { get; set; }

  public virtual bool CompareReferenceKey(PX.Objects.AP.APTran aTran)
  {
    if (aTran.POAccrualType == this.POAccrualType)
    {
      Guid? accrualRefNoteId1 = aTran.POAccrualRefNoteID;
      Guid? accrualRefNoteId2 = this.POAccrualRefNoteID;
      if ((accrualRefNoteId1.HasValue == accrualRefNoteId2.HasValue ? (accrualRefNoteId1.HasValue ? (accrualRefNoteId1.GetValueOrDefault() == accrualRefNoteId2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
      {
        int? poAccrualLineNbr1 = aTran.POAccrualLineNbr;
        int? poAccrualLineNbr2 = this.POAccrualLineNbr;
        return poAccrualLineNbr1.GetValueOrDefault() == poAccrualLineNbr2.GetValueOrDefault() & poAccrualLineNbr1.HasValue == poAccrualLineNbr2.HasValue;
      }
    }
    return false;
  }

  public virtual void SetReferenceKeyTo(PX.Objects.AP.APTran aTran)
  {
    bool flag = this.POAccrualType == "O";
    aTran.POAccrualType = this.POAccrualType;
    aTran.POAccrualRefNoteID = this.POAccrualRefNoteID;
    aTran.POAccrualLineNbr = this.POAccrualLineNbr;
    aTran.ReceiptType = flag ? (string) null : this.ReceiptType;
    aTran.ReceiptNbr = flag ? (string) null : this.ReceiptNbr;
    aTran.ReceiptLineNbr = flag ? new int?() : this.LineNbr;
    aTran.SubItemID = flag ? new int?() : this.SubItemID;
    aTran.POOrderType = this.POType;
    aTran.PONbr = this.PONbr;
    aTran.POLineNbr = this.POLineNbr;
  }

  public class PK : 
    PrimaryKeyOf<POReceiptLineS>.By<POReceiptLineS.receiptType, POReceiptLineS.receiptNbr, POReceiptLineS.lineNbr>
  {
    public static POReceiptLineS Find(
      PXGraph graph,
      string receiptType,
      string receiptNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (POReceiptLineS) PrimaryKeyOf<POReceiptLineS>.By<POReceiptLineS.receiptType, POReceiptLineS.receiptNbr, POReceiptLineS.lineNbr>.FindBy(graph, (object) receiptType, (object) receiptNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.branchID>
    {
    }

    public class Receipt : 
      PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.receiptType, POReceiptLineS.receiptNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.inventoryID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.vendorID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.siteID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.curyID>
    {
    }

    public class OrderCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.orderCuryInfoID>
    {
    }

    public class ReceiptCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.receiptCuryInfoID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.taxCategoryID>
    {
    }

    public class ExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.expenseAcctID>
    {
    }

    public class ExpenseSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.expenseSubID>
    {
    }

    public class AccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.pOAccrualAcctID>
    {
    }

    public class AccrualSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.pOAccrualSubID>
    {
    }

    public class Tax : 
      PrimaryKeyOf<PX.Objects.TX.Tax>.By<PX.Objects.TX.Tax.taxID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.taxID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.projectID, POReceiptLineS.taskID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.costCodeID>
    {
    }

    public class AccrualStatus : 
      PrimaryKeyOf<POAccrualStatus>.By<POAccrualStatus.refNoteID, POAccrualStatus.lineNbr, POAccrualStatus.type>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.pOAccrualRefNoteID, POReceiptLineS.pOAccrualLineNbr, POReceiptLineS.pOAccrualType>
    {
    }

    public class Order : 
      PrimaryKeyOf<POOrder>.By<POOrder.orderType, POOrder.orderNbr>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.pOType, POReceiptLineS.pONbr>
    {
    }

    public class OrderLine : 
      PrimaryKeyOf<POLine>.By<POLine.orderType, POLine.orderNbr, POLine.lineNbr>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.pOType, POReceiptLineS.pONbr, POReceiptLineS.pOLineNbr>
    {
    }

    public class Discount : 
      PrimaryKeyOf<APDiscount>.By<APDiscount.discountID, APDiscount.bAccountID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.discountID, POReceiptLineS.vendorID>
    {
    }

    public class DiscountSequence : 
      PrimaryKeyOf<VendorDiscountSequence>.By<VendorDiscountSequence.vendorID, VendorDiscountSequence.discountID, VendorDiscountSequence.discountSequenceID>.ForeignKeyOf<POReceiptLineS>.By<POReceiptLineS.vendorID, POReceiptLineS.discountID, POReceiptLineS.discountSequenceID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineS.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.branchID>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineS.orderType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineS.receiptNbr>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineS.receiptType>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.sortOrder>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineS.lineType>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineS.isStockItem>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.inventoryID>
  {
  }

  public abstract class accrueCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineS.accrueCost>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.vendorID>
  {
  }

  public abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineS.receiptDate>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.siteID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineS.uOM>
  {
  }

  public abstract class receiptQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineS.receiptQty>
  {
  }

  public abstract class baseReceiptQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.baseReceiptQty>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineS.curyID>
  {
  }

  public abstract class returnInventoryCostMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineS.returnInventoryCostMode>
  {
  }

  public abstract class orderCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    POReceiptLineS.orderCuryInfoID>
  {
  }

  public abstract class receiptCuryInfoID : 
    BqlType<
    #nullable enable
    IBqlLong, long>.Field<
    #nullable disable
    POReceiptLineS.receiptCuryInfoID>
  {
  }

  public abstract class curyOrderUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.curyOrderUnitCost>
  {
  }

  public abstract class orderUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.orderUnitCost>
  {
  }

  public abstract class curyExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineS.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineS.extCost>
  {
  }

  public abstract class curyOrderLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.curyOrderLineAmt>
  {
  }

  public abstract class orderLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.orderLineAmt>
  {
  }

  public abstract class orderDiscPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.orderDiscPct>
  {
  }

  public abstract class completePOLine : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineS.completePOLine>
  {
  }

  public abstract class receiptDiscPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.receiptDiscPct>
  {
  }

  public abstract class curyOrderDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.curyOrderDiscAmt>
  {
  }

  public abstract class orderDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.orderDiscAmt>
  {
  }

  public abstract class curyReceiptDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.curyReceiptDiscAmt>
  {
  }

  public abstract class receiptDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.receiptDiscAmt>
  {
  }

  public abstract class curyReceiptUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.curyReceiptUnitCost>
  {
  }

  public abstract class receiptUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.receiptUnitCost>
  {
  }

  public abstract class curyReceiptExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.curyReceiptExtCost>
  {
  }

  public abstract class receiptExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.receiptExtCost>
  {
  }

  public abstract class unbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineS.unbilledQty>
  {
  }

  public abstract class baseUnbilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.baseUnbilledQty>
  {
  }

  public abstract class orderUOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineS.orderUOM>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineS.orderQty>
  {
  }

  public abstract class baseOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.baseOrderQty>
  {
  }

  public abstract class retainagePct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.retainagePct>
  {
  }

  public abstract class curyRetainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.curyRetainageAmt>
  {
  }

  public abstract class retainageAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.retainageAmt>
  {
  }

  public abstract class curyUnbilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.curyUnbilledAmt>
  {
  }

  public abstract class unbilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineS.unbilledAmt>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineS.documentDiscountRate>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineS.taxCategoryID>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.expenseAcctID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.expenseSubID>
  {
  }

  public abstract class pOAccrualAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.pOAccrualAcctID>
  {
  }

  public abstract class pOAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.pOAccrualSubID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineS.tranDesc>
  {
  }

  public abstract class taxID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineS.taxID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.costCodeID>
  {
  }

  public abstract class pOAccrualType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineS.pOAccrualType>
  {
  }

  public abstract class pOAccrualRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POReceiptLineS.pOAccrualRefNoteID>
  {
  }

  public abstract class pOAccrualLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLineS.pOAccrualLineNbr>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineS.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineS.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.pOLineNbr>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineS.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineS.discountSequenceID>
  {
  }

  public abstract class allowEditUnitCost : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLineS.allowEditUnitCost>
  {
  }

  public abstract class dRTermStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineS.dRTermStartDate>
  {
  }

  public abstract class dRTermEndDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineS.dRTermEndDate>
  {
  }

  public abstract class dropshipExpenseRecording : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineS.dropshipExpenseRecording>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineS.costCenterID>
  {
  }
}
