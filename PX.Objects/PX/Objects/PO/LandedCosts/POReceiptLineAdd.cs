// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCosts.POReceiptLineAdd
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO.LandedCosts;

[PXProjection(typeof (Select2<PX.Objects.PO.POReceiptLine, InnerJoin<PX.Objects.PO.POReceipt, On<PX.Objects.PO.POReceiptLine.FK.Receipt>, LeftJoin<PX.Objects.PO.POLine, On<PX.Objects.PO.POLine.orderType, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<PX.Objects.PO.POLine.orderNbr, Equal<PX.Objects.PO.POReceiptLine.pONbr>, And<PX.Objects.PO.POLine.lineNbr, Equal<PX.Objects.PO.POReceiptLine.pOLineNbr>>>>>>>), Persistent = false)]
[PXCacheName("Purchase Receipt Line")]
[Serializable]
public class POReceiptLineAdd : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
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
  protected int? _SubItemID;
  protected int? _SiteID;
  protected string _UOM;
  protected Decimal? _ReceiptQty;
  protected Decimal? _BaseReceiptQty;
  protected long? _CuryInfoID;
  protected Decimal? _UnitCost;
  protected Decimal? _UnitWeight;
  protected int? _ExpenseAcctID;
  protected int? _ExpenseSubID;
  protected int? _POAccrualAcctID;
  protected int? _POAccrualSubID;
  protected string _TranDesc;
  protected int? _ProjectID;
  protected int? _TaskID;
  protected string _POType;
  protected string _PONbr;
  protected int? _POLineNbr;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [Branch(null, null, true, true, true, BqlField = typeof (PX.Objects.PO.POReceiptLine.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.PO.POReceiptLine.receiptNbr))]
  [PXUIField(DisplayName = "Receipt Nbr.")]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.receiptType))]
  [PXUIField(DisplayName = "Receipt Type")]
  public virtual string ReceiptType
  {
    get => this._ReceiptType;
    set => this._ReceiptType = value;
  }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POReceipt.invoiceNbr))]
  [PXUIField(DisplayName = "Vendor Ref.")]
  public virtual string InvoiceNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.lineNbr))]
  [PXUIField]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POReceiptLine.sortOrder))]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POReceipt.released))]
  [PXUIField(DisplayName = "Released")]
  public virtual bool? Released { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.lineType))]
  [POLineType.List]
  [PXUIField(DisplayName = "Line Type")]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POReceiptLine.isStockItem))]
  public virtual bool? IsStockItem { get; set; }

  [Inventory(Filterable = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [Vendor]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBDate(BqlField = typeof (PX.Objects.PO.POReceiptLine.receiptDate))]
  public virtual DateTime? ReceiptDate { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.PO.POReceipt.lastModifiedDateTime))]
  public virtual DateTime? ReceiptLastModifiedDateTime { get; set; }

  [SubItem(typeof (POReceiptLineAdd.inventoryID), BqlField = typeof (PX.Objects.PO.POReceiptLine.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Site(BqlField = typeof (PX.Objects.PO.POReceiptLine.siteID))]
  [PXDefault]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [INUnit(typeof (POReceiptLineAdd.inventoryID), BqlField = typeof (PX.Objects.PO.POReceiptLine.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (POReceiptLineAdd.uOM), typeof (POReceiptLineAdd.baseReceiptQty), HandleEmptyKey = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.receiptQty))]
  [PXUIField]
  public virtual Decimal? ReceiptQty
  {
    get => this._ReceiptQty;
    set => this._ReceiptQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.PO.POReceiptLine.baseReceiptQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseReceiptQty
  {
    get => this._BaseReceiptQty;
    set => this._BaseReceiptQty = value;
  }

  public virtual Decimal? BaseQty
  {
    get => this._BaseReceiptQty;
    set => this._BaseReceiptQty = value;
  }

  [PXDBLong(BqlField = typeof (PX.Objects.PO.POReceiptLine.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.PO.POReceiptLine.unitCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBBaseCury(BqlField = typeof (PX.Objects.PO.POReceiptLine.tranCostFinal))]
  [PXDefault]
  [PXUIField(DisplayName = "Final IN Ext. Cost", Enabled = false)]
  public virtual Decimal? TranCostFinal { get; set; }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.PO.POReceiptLine.unitWeight))]
  [PXUIField(DisplayName = "Unit Weight")]
  public virtual Decimal? UnitWeight { get; set; }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.PO.POReceiptLine.unitVolume))]
  [PXUIField(DisplayName = "Unit Volume")]
  public virtual Decimal? UnitVolume { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POReceiptLine.expenseAcctID))]
  public virtual int? ExpenseAcctID
  {
    get => this._ExpenseAcctID;
    set => this._ExpenseAcctID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POReceiptLine.expenseSubID))]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POReceiptLine.pOAccrualAcctID))]
  public virtual int? POAccrualAcctID
  {
    get => this._POAccrualAcctID;
    set => this._POAccrualAcctID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POReceiptLine.pOAccrualSubID))]
  public virtual int? POAccrualSubID
  {
    get => this._POAccrualSubID;
    set => this._POAccrualSubID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.tranDesc))]
  [PXUIField]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POReceiptLine.projectID))]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POReceiptLine.taskID))]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POReceiptLine.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.pOType))]
  [POOrderType.List]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public virtual string POType
  {
    get => this._POType;
    set => this._POType = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POReceiptLine.pONbr))]
  [PXUIField(DisplayName = "Order Nbr.")]
  public virtual string PONbr
  {
    get => this._PONbr;
    set => this._PONbr = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POReceiptLine.pOLineNbr))]
  [PXUIField(DisplayName = "PO Line Nbr.")]
  public virtual int? POLineNbr
  {
    get => this._POLineNbr;
    set => this._POLineNbr = value;
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceipt.IsUnderCorrection" />
  [PXDBBool(BqlField = typeof (PX.Objects.PO.POReceipt.isUnderCorrection))]
  public virtual bool? IsUnderCorrection { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceipt.Canceled" />
  [PXDBBool(BqlField = typeof (PX.Objects.PO.POReceipt.canceled))]
  public virtual bool? Canceled { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineAdd.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.branchID>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineAdd.receiptNbr>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineAdd.receiptType>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineAdd.invoiceNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.sortOrder>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineAdd.released>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineAdd.lineType>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineAdd.isStockItem>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.inventoryID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.vendorID>
  {
  }

  public abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineAdd.receiptDate>
  {
  }

  public abstract class receiptLastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineAdd.receiptLastModifiedDateTime>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.siteID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineAdd.uOM>
  {
  }

  public abstract class receiptQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineAdd.receiptQty>
  {
  }

  public abstract class baseReceiptQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineAdd.baseReceiptQty>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POReceiptLineAdd.curyInfoID>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineAdd.unitCost>
  {
  }

  public abstract class tranCostFinal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineAdd.tranCostFinal>
  {
  }

  public abstract class unitWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineAdd.unitWeight>
  {
  }

  public abstract class unitVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineAdd.unitVolume>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.expenseAcctID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.expenseSubID>
  {
  }

  public abstract class pOAccrualAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLineAdd.pOAccrualAcctID>
  {
  }

  public abstract class pOAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.pOAccrualSubID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineAdd.tranDesc>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.costCodeID>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineAdd.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineAdd.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineAdd.pOLineNbr>
  {
  }

  public abstract class isUnderCorrection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLineAdd.isUnderCorrection>
  {
  }

  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineAdd.canceled>
  {
  }
}
