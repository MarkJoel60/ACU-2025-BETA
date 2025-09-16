// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceiptLineReturn
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select2<POReceiptLine, InnerJoin<POReceipt, On<POReceiptLine.FK.Receipt>>, Where<POReceipt.receiptType, Equal<POReceiptType.poreceipt>, And<POReceipt.released, Equal<True>, And<Sub<POReceiptLine.baseReceiptQty, POReceiptLine.baseReturnedQty>, Greater<decimal0>>>>>), Persistent = false)]
[PXHidden]
[Serializable]
public class POReceiptLineReturn : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IPOReturnLineSource
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _CuryID;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [Branch(null, null, true, true, true, BqlField = typeof (POReceiptLine.branchID))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POReceiptLine.pOType))]
  [POOrderType.List]
  [PXUIField(DisplayName = "Order Type", Enabled = false)]
  public virtual string POType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POReceiptLine.pONbr))]
  [PXUIField(DisplayName = "Order Nbr.", Enabled = false)]
  [PX.Objects.PO.PO.RefNbr(typeof (Search<POOrder.orderNbr>), Filterable = true)]
  public virtual string PONbr { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.pOLineNbr))]
  [PXUIField(DisplayName = "PO Line Nbr.", Enabled = false)]
  public virtual int? POLineNbr { get; set; }

  [PXDBString(2, IsFixed = true, IsKey = true, BqlField = typeof (POReceipt.receiptType))]
  [POReceiptType.List]
  [PXUIField(DisplayName = "Receipt Type", Enabled = false)]
  public virtual string ReceiptType { get; set; }

  /// <inheritdoc cref="T:PX.Objects.PO.POReceiptLine.lotSerialNbrRequiredForDropship" />
  [PXDBBool(BqlField = typeof (POReceiptLine.lotSerialNbrRequiredForDropship))]
  public virtual bool? LotSerialNbrRequiredForDropship { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (POReceipt.receiptNbr))]
  [POReceiptType.RefNbr(typeof (Search<POReceipt.receiptNbr, Where<BqlOperand<POReceipt.receiptType, IBqlString>.IsEqual<BqlField<POReceiptLineReturn.receiptType, IBqlString>.FromCurrent>>>), Filterable = true)]
  [PXUIField(DisplayName = "Receipt Nbr.", Enabled = false)]
  public virtual string ReceiptNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (POReceiptLine.lineNbr))]
  [PXUIField(DisplayName = "Line Nbr.", Enabled = false)]
  public virtual int? LineNbr { get; set; }

  [VendorActive(Enabled = false, DescriptionField = typeof (PX.Objects.AP.Vendor.acctName), CacheGlobal = true, Filterable = true, BqlField = typeof (POReceipt.vendorID))]
  public virtual int? VendorID { get; set; }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POReceipt.vendorID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr), Enabled = false, BqlField = typeof (POReceipt.vendorLocationID))]
  public virtual int? VendorLocationID { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (POReceipt.curyID))]
  [PXUIField]
  public virtual string CuryID { get; set; }

  [PXDBDate(BqlField = typeof (POReceipt.receiptDate))]
  [PXUIField(DisplayName = "Date", Enabled = false)]
  public virtual DateTime? ReceiptDate { get; set; }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (POReceipt.invoiceNbr))]
  [PXUIField(DisplayName = "Vendor Ref.", Enabled = false)]
  public virtual string InvoiceNbr { get; set; }

  [Site(Enabled = false, BqlField = typeof (POReceiptLine.siteID))]
  public virtual int? SiteID { get; set; }

  [PXDBBool(BqlField = typeof (POReceiptLine.isStockItem))]
  public virtual bool? IsStockItem { get; set; }

  [Inventory(Enabled = false, BqlField = typeof (POReceiptLine.inventoryID))]
  public virtual int? InventoryID { get; set; }

  [PXDBBool(BqlField = typeof (POReceiptLine.accrueCost))]
  public virtual bool? AccrueCost { get; set; }

  [INUnit(typeof (POReceiptLine.inventoryID), Enabled = false, BqlField = typeof (POReceiptLine.uOM))]
  public virtual string UOM { get; set; }

  [PXDBQuantity(BqlField = typeof (POReceiptLine.receiptQty))]
  [PXUIField(DisplayName = "Receipt Qty.", Enabled = false)]
  public virtual Decimal? ReceiptQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POReceiptLine.baseReceiptQty))]
  public virtual Decimal? BaseReceiptQty { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POReceiptLine.lineType))]
  public virtual string LineType { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.locationID))]
  public virtual int? LocationID { get; set; }

  [PXDBString(100, IsUnicode = true, BqlField = typeof (POReceiptLine.lotSerialNbr))]
  public virtual string LotSerialNbr { get; set; }

  [PXDBDate(BqlField = typeof (POReceiptLine.expireDate))]
  public virtual DateTime? ExpireDate { get; set; }

  [PXDBQuantity(BqlField = typeof (POReceiptLine.baseReturnedQty))]
  public virtual Decimal? BaseReturnedQty { get; set; }

  [PXQuantity]
  [PXUIField(DisplayName = "Returned Qty.", Enabled = false)]
  public virtual Decimal? ReturnedQty { get; set; }

  [PXDBLong(BqlField = typeof (POReceipt.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.expenseAcctID))]
  public virtual int? ExpenseAcctID { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.expenseSubID))]
  public virtual int? ExpenseSubID { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.pOAccrualAcctID))]
  public virtual int? POAccrualAcctID { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.pOAccrualSubID))]
  public virtual int? POAccrualSubID { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.taskID))]
  public virtual int? TaskID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (POReceiptLine.tranDesc))]
  public virtual string TranDesc { get; set; }

  [PXDBBool(BqlField = typeof (POReceiptLine.allowEditUnitCost))]
  public virtual bool? AllowEditUnitCost { get; set; }

  [PXDBBool(BqlField = typeof (POReceiptLine.manualPrice))]
  public virtual bool? ManualPrice { get; set; }

  [PXDBDecimal(6, BqlField = typeof (POReceiptLine.discPct))]
  public virtual Decimal? DiscPct { get; set; }

  [PXDBCurrency(typeof (POReceiptLineReturn.curyInfoID), typeof (POReceiptLineReturn.discAmt), BqlField = typeof (POReceiptLine.curyDiscAmt))]
  public virtual Decimal? CuryDiscAmt { get; set; }

  [PXDBBaseCury(BqlField = typeof (POReceiptLine.discAmt))]
  public virtual Decimal? DiscAmt { get; set; }

  [PXDBCurrency(typeof (POReceiptLineReturn.curyInfoID), typeof (POReceiptLineReturn.tranCost), BqlField = typeof (POReceiptLine.curyTranCost))]
  public virtual Decimal? CuryTranCost { get; set; }

  [PXDBBaseCury(BqlField = typeof (POReceiptLine.tranCost))]
  public virtual Decimal? TranCost { get; set; }

  [PXDBCurrency(typeof (POReceiptLineReturn.curyInfoID), typeof (POReceiptLineReturn.extCost), BqlField = typeof (POReceiptLine.curyExtCost))]
  public virtual Decimal? CuryExtCost { get; set; }

  [PXDBBaseCury(BqlField = typeof (POReceiptLine.extCost))]
  public virtual Decimal? ExtCost { get; set; }

  [PXDBBaseCury(BqlField = typeof (POReceiptLine.tranCostFinal))]
  public virtual Decimal? TranCostFinal { get; set; }

  [PXDBCurrency(typeof (POReceiptLineReturn.curyInfoID), typeof (POReceiptLineReturn.unitCost), BqlField = typeof (POReceiptLine.curyUnitCost))]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXDBPriceCost(BqlField = typeof (POReceiptLine.unitCost))]
  public virtual Decimal? UnitCost { get; set; }

  [PXDBString(1, BqlField = typeof (POReceiptLine.dropshipExpenseRecording))]
  public virtual string DropshipExpenseRecording { get; set; }

  [PXDBBool(BqlField = typeof (POReceiptLine.isSpecialOrder))]
  [PXDefault]
  public virtual bool? IsSpecialOrder { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.costCenterID))]
  [PXDefault]
  public virtual int? CostCenterID { get; set; }

  [PXDBDecimal(6)]
  [PXDefault]
  public virtual Decimal? TranUnitCost { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineReturn.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.branchID>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineReturn.pOType>
  {
  }

  public abstract class pONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineReturn.pONbr>
  {
  }

  public abstract class pOLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.pOLineNbr>
  {
  }

  public abstract class receiptType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineReturn.receiptType>
  {
  }

  public abstract class lotSerialNbrRequiredForDropship : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLineReturn.lotSerialNbrRequiredForDropship>
  {
  }

  public abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineReturn.receiptNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.lineNbr>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.vendorID>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLineReturn.vendorLocationID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineReturn.curyID>
  {
  }

  public abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineReturn.receiptDate>
  {
  }

  public abstract class invoiceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineReturn.invoiceNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.siteID>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineReturn.isStockItem>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.inventoryID>
  {
  }

  public abstract class accrueCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineReturn.accrueCost>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineReturn.uOM>
  {
  }

  public abstract class receiptQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineReturn.receiptQty>
  {
  }

  public abstract class baseReceiptQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineReturn.baseReceiptQty>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineReturn.lineType>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.subItemID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.locationID>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineReturn.lotSerialNbr>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceiptLineReturn.expireDate>
  {
  }

  public abstract class baseReturnedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineReturn.baseReturnedQty>
  {
  }

  public abstract class returnedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineReturn.returnedQty>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POReceiptLineReturn.curyInfoID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.costCodeID>
  {
  }

  public abstract class expenseAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLineReturn.expenseAcctID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.expenseSubID>
  {
  }

  public abstract class pOAccrualAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLineReturn.pOAccrualAcctID>
  {
  }

  public abstract class pOAccrualSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceiptLineReturn.pOAccrualSubID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.taskID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceiptLineReturn.tranDesc>
  {
  }

  public abstract class allowEditUnitCost : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLineReturn.allowEditUnitCost>
  {
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceiptLineReturn.manualPrice>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineReturn.discPct>
  {
  }

  public abstract class curyDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineReturn.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineReturn.discAmt>
  {
  }

  public abstract class curyTranCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineReturn.curyExtCost>
  {
  }

  public abstract class tranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineReturn.tranCost>
  {
  }

  public abstract class curyExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineReturn.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineReturn.extCost>
  {
  }

  public abstract class tranCostFinal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineReturn.tranCostFinal>
  {
  }

  public abstract class curyUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineReturn.curyUnitCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceiptLineReturn.unitCost>
  {
  }

  public abstract class dropshipExpenseRecording : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceiptLineReturn.dropshipExpenseRecording>
  {
  }

  public abstract class isSpecialOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceiptLineReturn.isSpecialOrder>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceiptLineReturn.costCenterID>
  {
  }

  public abstract class tranUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceiptLineReturn.tranUnitCost>
  {
  }
}
