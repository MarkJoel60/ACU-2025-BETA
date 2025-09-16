// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LinkLineReceipt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select2<POReceiptLine, InnerJoin<POReceipt, On<POReceiptLine.FK.Receipt>, LeftJoin<POOrderReceiptLink, On<POOrderReceiptLink.receiptType, Equal<POReceiptLine.receiptType>, And<POOrderReceiptLink.receiptNbr, Equal<POReceiptLine.receiptNbr>, And<POOrderReceiptLink.pOType, Equal<POReceiptLine.pOType>, And<POOrderReceiptLink.pONbr, Equal<POReceiptLine.pONbr>>>>>, LeftJoin<PX.Objects.AP.APTran, On<PX.Objects.AP.APTran.released, NotEqual<True>, And<PX.Objects.AP.APTran.pOAccrualType, Equal<POReceiptLine.pOAccrualType>, And<PX.Objects.AP.APTran.pOAccrualRefNoteID, Equal<POReceiptLine.pOAccrualRefNoteID>, And<PX.Objects.AP.APTran.pOAccrualLineNbr, Equal<POReceiptLine.pOAccrualLineNbr>>>>>>>>, Where2<Where<POReceiptLine.pOType, In3<POOrderType.regularOrder, POOrderType.dropShip, POOrderType.projectDropShip>, Or<POReceiptLine.pOType, IsNull>>, And<POReceiptLine.unbilledQty, Greater<decimal0>, And<POReceipt.released, Equal<True>, And<PX.Objects.AP.APTran.refNbr, IsNull, And<POReceipt.canceled, Equal<False>, And<POReceipt.isUnderCorrection, Equal<False>>>>>>>>), Persistent = false)]
[Serializable]
public class LinkLineReceipt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _OrderLineNbr;
  protected int? _InventoryID;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected string _UOM;
  protected string _ReceiptType;
  protected string _ReceiptNbr;
  protected int? _ReceiptLineNbr;
  protected int? _ReceiptSortOrder;
  protected Decimal? _ReceiptQty;
  protected string _ReceiptCuryID;
  protected Decimal? _ReceiptUnbilledQty;
  protected Decimal? _ReceiptBaseUnbilledQty;
  protected int? _ReceiptSiteID;
  protected int? _ReceiptSubItemID;
  protected int? _POAccrualAcctID;
  protected int? _POAccrualSubID;
  protected int? _ReceiptExpenseAcctID;
  protected int? _ReceiptExpenseSubID;
  protected string _ReciptTranDesc;
  protected string _ReceiptVendorRefNbr;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (POReceiptLine.pOAccrualType))]
  public virtual string POAccrualType { get; set; }

  [PXDBGuid(false, BqlField = typeof (POReceiptLine.pOAccrualRefNoteID))]
  public virtual Guid? POAccrualRefNoteID { get; set; }

  [PXDBInt(BqlField = typeof (POReceiptLine.pOAccrualLineNbr))]
  public virtual int? POAccrualLineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (POReceiptLine.pOType))]
  [POOrderType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (POReceiptLine.pONbr))]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PXSelector(typeof (Search<POOrder.orderNbr, Where<POOrder.orderType, Equal<Current<LinkLineReceipt.orderType>>>>))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXUIField(DisplayName = "PO Line", Visible = false)]
  [PXDBInt(BqlField = typeof (POReceiptLine.pOLineNbr))]
  public virtual int? OrderLineNbr
  {
    get => this._OrderLineNbr;
    set => this._OrderLineNbr = value;
  }

  [POLineInventoryItem(Filterable = true, BqlField = typeof (POReceiptLine.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(BqlField = typeof (POReceipt.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt(BqlField = typeof (POReceipt.vendorLocationID))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [INUnit(typeof (LinkLineReceipt.inventoryID), DisplayName = "UOM", BqlField = typeof (POReceiptLine.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBString(2, IsFixed = true, IsKey = true, InputMask = "", BqlField = typeof (POReceiptLine.receiptType))]
  [PXDefault("RT")]
  [POReceiptType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual string ReceiptType
  {
    get => this._ReceiptType;
    set => this._ReceiptType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (POReceiptLine.receiptNbr))]
  [PXSelector(typeof (Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<Current<LinkLineReceipt.receiptType>>>>), ValidateValue = false)]
  [PXUIField]
  [PXUIVerify]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [PXUIField(DisplayName = "PO Receipt Line", Visible = false)]
  [PXDBInt(IsKey = true, BqlField = typeof (POReceiptLine.lineNbr))]
  [PXDefault(1)]
  public virtual int? ReceiptLineNbr
  {
    get => this._ReceiptLineNbr;
    set => this._ReceiptLineNbr = value;
  }

  [PXDBInt(BqlField = typeof (POReceiptLine.sortOrder))]
  public virtual int? ReceiptSortOrder
  {
    get => this._ReceiptSortOrder;
    set => this._ReceiptSortOrder = value;
  }

  [PXDBQuantity(BqlField = typeof (POReceiptLine.receiptQty))]
  [PXUIField]
  public virtual Decimal? ReceiptQty
  {
    get => this._ReceiptQty;
    set => this._ReceiptQty = value;
  }

  [PXUIField(DisplayName = "Order Currency")]
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (POOrderReceiptLink.curyID))]
  public virtual string ReceiptCuryID
  {
    get => this._ReceiptCuryID;
    set => this._ReceiptCuryID = value;
  }

  [PXDBQuantity(typeof (LinkLineReceipt.uOM), typeof (LinkLineReceipt.receiptBaseUnbilledQty), HandleEmptyKey = true, BqlField = typeof (POReceiptLine.unbilledQty))]
  [PXUIField(DisplayName = "Unbilled Qty.", Enabled = false)]
  public virtual Decimal? ReceiptUnbilledQty
  {
    get => this._ReceiptUnbilledQty;
    set => this._ReceiptUnbilledQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (POReceiptLine.baseUnbilledQty))]
  public virtual Decimal? ReceiptBaseUnbilledQty
  {
    get => this._ReceiptBaseUnbilledQty;
    set => this._ReceiptBaseUnbilledQty = value;
  }

  [Site(BqlField = typeof (POReceiptLine.siteID))]
  public virtual int? ReceiptSiteID
  {
    get => this._ReceiptSiteID;
    set => this._ReceiptSiteID = value;
  }

  [SubItem(typeof (LinkLineReceipt.inventoryID), BqlField = typeof (POReceiptLine.subItemID))]
  public virtual int? ReceiptSubItemID
  {
    get => this._ReceiptSubItemID;
    set => this._ReceiptSubItemID = value;
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

  [PXDBInt(BqlField = typeof (POReceiptLine.expenseAcctID))]
  public virtual int? ReceiptExpenseAcctID
  {
    get => this._ReceiptExpenseAcctID;
    set => this._ReceiptExpenseAcctID = value;
  }

  [PXDBInt(BqlField = typeof (POReceiptLine.expenseSubID))]
  public virtual int? ReceiptExpenseSubID
  {
    get => this._ReceiptExpenseSubID;
    set => this._ReceiptExpenseSubID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (POReceiptLine.tranDesc))]
  [PXUIField(DisplayName = "Transaction Descr.")]
  public virtual string ReciptTranDesc
  {
    get => this._ReciptTranDesc;
    set => this._ReciptTranDesc = value;
  }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (POReceipt.invoiceNbr))]
  [PXUIField(DisplayName = "Vendor Ref.")]
  public virtual string ReceiptVendorRefNbr
  {
    get => this._ReceiptVendorRefNbr;
    set => this._ReceiptVendorRefNbr = value;
  }

  [PXDBInt(BqlField = typeof (POOrderReceiptLink.payToVendorID))]
  public virtual int? PayToVendorID { get; set; }

  [ProjectBase(BqlField = typeof (POReceipt.projectID))]
  public virtual int? ProjectID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LinkLineReceipt.selected>
  {
  }

  public abstract class pOAccrualType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LinkLineReceipt.pOAccrualType>
  {
  }

  public abstract class pOAccrualRefNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LinkLineReceipt.pOAccrualRefNoteID>
  {
  }

  public abstract class pOAccrualLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LinkLineReceipt.pOAccrualLineNbr>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineReceipt.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineReceipt.orderNbr>
  {
  }

  public abstract class orderLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineReceipt.orderLineNbr>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineReceipt.inventoryID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineReceipt.vendorID>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LinkLineReceipt.vendorLocationID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineReceipt.uOM>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineReceipt.receiptType>
  {
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineReceipt.receiptNbr>
  {
  }

  public abstract class receiptLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineReceipt.receiptLineNbr>
  {
  }

  public abstract class receiptSortOrder : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LinkLineReceipt.receiptSortOrder>
  {
  }

  public abstract class receiptQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  LinkLineReceipt.receiptQty>
  {
  }

  public abstract class receiptCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LinkLineReceipt.receiptCuryID>
  {
  }

  public abstract class receiptUnbilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LinkLineReceipt.receiptUnbilledQty>
  {
  }

  public abstract class receiptBaseUnbilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LinkLineReceipt.receiptBaseUnbilledQty>
  {
  }

  public abstract class receiptSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineReceipt.receiptSiteID>
  {
  }

  public abstract class receiptSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LinkLineReceipt.receiptSubItemID>
  {
  }

  public abstract class pOAccrualAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LinkLineReceipt.pOAccrualAcctID>
  {
  }

  public abstract class pOAccrualSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineReceipt.pOAccrualSubID>
  {
  }

  public abstract class receiptExpenseAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LinkLineReceipt.receiptExpenseAcctID>
  {
  }

  public abstract class receiptExpenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LinkLineReceipt.receiptExpenseSubID>
  {
  }

  public abstract class reciptTranDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LinkLineReceipt.reciptTranDesc>
  {
  }

  public abstract class receiptVendorRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LinkLineReceipt.receiptVendorRefNbr>
  {
  }

  public abstract class payToVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineReceipt.payToVendorID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineReceipt.projectID>
  {
  }
}
