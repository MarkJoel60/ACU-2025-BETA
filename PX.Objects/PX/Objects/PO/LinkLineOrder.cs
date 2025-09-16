// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LinkLineOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (Select2<POLine, InnerJoin<POOrder, On<POOrder.orderType, Equal<POLine.orderType>, And<POOrder.orderNbr, Equal<POLine.orderNbr>>>>, Where<POLine.closed, NotEqual<True>, And<POOrder.status, In3<POOrderStatus.open, POOrderStatus.completed>, And<POLine.pOAccrualType, Equal<PX.Objects.PO.POAccrualType.order>, And<NotExists<Select2<POOrderReceipt, InnerJoin<POReceipt, On<POOrderReceipt.FK.Receipt>>, Where<POOrderReceipt.pOType, Equal<POLine.orderType>, And<POOrderReceipt.pONbr, Equal<POLine.orderNbr>, And<POReceipt.status, Equal<POReceiptStatus.underCorrection>>>>>>>>>>>), Persistent = false)]
[Serializable]
public class LinkLineOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  protected Decimal? _OrderBaseQty;
  protected Decimal? _OrderQty;
  protected Decimal? _OrderAmount;
  protected Decimal? _OrderCuryAmount;
  protected string _OrderCuryID;
  protected int? _OrderSiteID;
  protected int? _OrderSubItemID;
  protected int? _OrderExpenseAcctID;
  protected int? _OrderExpenseSubID;
  protected string _OrderTranDesc;
  protected string _VendorRefNbr;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (POLine.orderType))]
  [POOrderType.List]
  [PXUIField(DisplayName = "Type")]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, BqlField = typeof (POLine.orderNbr))]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PXSelector(typeof (Search<POOrder.orderNbr, Where<POOrder.orderType, Equal<Current<LinkLineOrder.orderType>>>>))]
  [PXUIVerify]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXUIField(DisplayName = "PO Line", Visible = false)]
  [PXDBInt(IsKey = true, BqlField = typeof (POLine.lineNbr))]
  public virtual int? OrderLineNbr
  {
    get => this._OrderLineNbr;
    set => this._OrderLineNbr = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (POLine.pOAccrualType))]
  public virtual string POAccrualType { get; set; }

  [PXDBGuid(false, BqlField = typeof (POLine.orderNoteID))]
  public virtual Guid? OrderNoteID { get; set; }

  [POLineInventoryItem(Filterable = true, BqlField = typeof (POLine.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(BqlField = typeof (POOrder.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt(BqlField = typeof (POOrder.vendorLocationID))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [INUnit(typeof (POLine.inventoryID), DisplayName = "UOM", BqlField = typeof (POLine.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBDecimal(6, BqlField = typeof (POLine.baseOrderQty))]
  public virtual Decimal? OrderBaseQty
  {
    get => this._OrderBaseQty;
    set => this._OrderBaseQty = value;
  }

  [PXDBQuantity(BqlField = typeof (POLine.orderQty))]
  [PXUIField]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBDecimal(BqlField = typeof (POLine.lineAmt))]
  public virtual Decimal? OrderAmount
  {
    get => this._OrderAmount;
    set => this._OrderAmount = value;
  }

  [PXDBDecimal(BqlField = typeof (POLine.curyLineAmt))]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? OrderCuryAmount
  {
    get => this._OrderCuryAmount;
    set => this._OrderCuryAmount = value;
  }

  [PXUIField(DisplayName = "Currency")]
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (POOrder.curyID))]
  public virtual string OrderCuryID
  {
    get => this._OrderCuryID;
    set => this._OrderCuryID = value;
  }

  [Site(BqlField = typeof (POLine.siteID))]
  public virtual int? OrderSiteID
  {
    get => this._OrderSiteID;
    set => this._OrderSiteID = value;
  }

  [SubItem(typeof (LinkLineOrder.inventoryID), BqlField = typeof (POLine.subItemID))]
  public virtual int? OrderSubItemID
  {
    get => this._OrderSubItemID;
    set => this._OrderSubItemID = value;
  }

  [PXDBInt(BqlField = typeof (POLine.expenseAcctID))]
  public virtual int? OrderExpenseAcctID
  {
    get => this._OrderExpenseAcctID;
    set => this._OrderExpenseAcctID = value;
  }

  [PXDBInt(BqlField = typeof (POLine.expenseSubID))]
  public virtual int? OrderExpenseSubID
  {
    get => this._OrderExpenseSubID;
    set => this._OrderExpenseSubID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (POLine.tranDesc))]
  [PXUIField(DisplayName = "Transaction Descr.")]
  public virtual string OrderTranDesc
  {
    get => this._OrderTranDesc;
    set => this._OrderTranDesc = value;
  }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (POOrder.vendorRefNbr))]
  [PXUIField(DisplayName = "Vendor Ref.")]
  public virtual string VendorRefNbr
  {
    get => this._VendorRefNbr;
    set => this._VendorRefNbr = value;
  }

  [PXDBInt(BqlField = typeof (POOrder.payToVendorID))]
  public virtual int? PayToVendorID { get; set; }

  [ProjectBase(BqlField = typeof (POOrder.projectID))]
  public virtual int? ProjectID { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  LinkLineOrder.selected>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineOrder.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineOrder.orderNbr>
  {
  }

  public abstract class orderLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineOrder.orderLineNbr>
  {
  }

  public abstract class pOAccrualType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LinkLineOrder.pOAccrualType>
  {
  }

  public abstract class orderNoteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LinkLineOrder.orderNoteID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineOrder.inventoryID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineOrder.vendorID>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LinkLineOrder.vendorLocationID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineOrder.uOM>
  {
  }

  public abstract class orderBaseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LinkLineOrder.orderBaseQty>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  LinkLineOrder.orderQty>
  {
  }

  public abstract class orderAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  LinkLineOrder.orderAmount>
  {
  }

  public abstract class orderCuryAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    LinkLineOrder.orderCuryAmount>
  {
  }

  public abstract class orderCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineOrder.orderCuryID>
  {
  }

  public abstract class orderSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineOrder.orderSiteID>
  {
  }

  public abstract class orderSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineOrder.orderSubItemID>
  {
  }

  public abstract class orderExpenseAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LinkLineOrder.orderExpenseAcctID>
  {
  }

  public abstract class orderExpenseSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LinkLineOrder.orderExpenseSubID>
  {
  }

  public abstract class orderTranDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LinkLineOrder.orderTranDesc>
  {
  }

  public abstract class vendorRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LinkLineOrder.vendorRefNbr>
  {
  }

  public abstract class payToVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineOrder.payToVendorID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LinkLineOrder.projectID>
  {
  }
}
