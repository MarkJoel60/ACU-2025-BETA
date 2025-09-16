// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.POLine3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXProjection(typeof (Select<PX.Objects.PO.POLine>), Persistent = true)]
[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1. Use SupplyPOLine instead.")]
[Serializable]
public class POLine3 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected string _VendorRefNbr;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected string _LineType;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected long? _PlanID;
  protected int? _VendorID;
  protected DateTime? _OrderDate;
  protected DateTime? _PromisedDate;
  protected bool? _Cancelled;
  protected int? _SiteID;
  protected string _UOM;
  protected Decimal? _OrderQty;
  protected Decimal? _BaseOrderQty;
  protected Decimal? _OpenQty;
  protected Decimal? _BaseOpenQty;
  protected Decimal? _ReceivedQty;
  protected Decimal? _BaseReceivedQty;
  protected string _TranDesc;
  protected string _ReceiptStatus;
  protected string _SOOrderType;
  protected string _SOOrderNbr;
  protected int? _SOOrderLineNbr;
  protected Decimal? _DemandQty;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.PO.POLine.orderType))]
  [PXDefault]
  [PXUIField(DisplayName = "PO Type", Enabled = false)]
  [POOrderType.List]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.PO.POLine.orderNbr))]
  [PXDefault]
  [PXUIField(DisplayName = "PO Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.PO.POOrder.orderNbr, Where<PX.Objects.PO.POOrder.orderType, Equal<Current<POLine3.orderType>>>>), DescriptionField = typeof (PX.Objects.PO.POOrder.orderDesc))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXString(40)]
  [PXUIField(DisplayName = "Vendor Ref.", Enabled = false)]
  public virtual string VendorRefNbr
  {
    get => this._VendorRefNbr;
    set => this._VendorRefNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.PO.POLine.lineNbr))]
  [PXDefault]
  [PXUIField(DisplayName = "PO Line Nbr.", Visible = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POLine.sortOrder))]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.PO.POLine.lineType))]
  [POLineType.List]
  [PXUIField(DisplayName = "Line Type", Enabled = false)]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [Inventory(Filterable = true, BqlField = typeof (PX.Objects.PO.POLine.inventoryID), Enabled = false)]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(BqlField = typeof (PX.Objects.PO.POLine.subItemID), Enabled = false)]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBLong(BqlField = typeof (PX.Objects.PO.POLine.planID))]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual long? PlanID
  {
    get => this._PlanID;
    set => this._PlanID = value;
  }

  [Vendor(typeof (Search<BAccountR.bAccountID, Where<PX.Objects.AP.Vendor.type, NotEqual<BAccountType.employeeType>>>), BqlField = typeof (PX.Objects.PO.POLine.vendorID), Enabled = false)]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBDate(BqlField = typeof (PX.Objects.PO.POLine.orderDate))]
  [PXUIField(DisplayName = "Order Date", Enabled = false)]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [PXDBDate(BqlField = typeof (PX.Objects.PO.POLine.promisedDate))]
  [PXUIField(DisplayName = "Promised", Enabled = false)]
  public virtual DateTime? PromisedDate
  {
    get => this._PromisedDate;
    set => this._PromisedDate = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POLine.cancelled))]
  public virtual bool? Cancelled
  {
    get => this._Cancelled;
    set => this._Cancelled = value;
  }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POLine.completed))]
  public virtual bool? Completed { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.PO.POLine.closed))]
  public virtual bool? Closed { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.PO.POLine.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(6, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POLine.uOM))]
  [PXUIField(DisplayName = "UOM", Enabled = false)]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.POLine.orderQty))]
  [PXUIField(DisplayName = "Order Qty.", Enabled = false)]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.POLine.baseOrderQty))]
  public virtual Decimal? BaseOrderQty
  {
    get => this._BaseOrderQty;
    set => this._BaseOrderQty = value;
  }

  [PXDBQuantity(BqlField = typeof (PX.Objects.PO.POLine.openQty))]
  [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.PO.POLine.baseOpenQty))]
  public virtual Decimal? BaseOpenQty
  {
    get => this._BaseOpenQty;
    set => this._BaseOpenQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.PO.POLine.receivedQty))]
  public virtual Decimal? ReceivedQty
  {
    get => this._ReceivedQty;
    set => this._ReceivedQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.PO.POLine.baseReceivedQty))]
  public virtual Decimal? BaseReceivedQty
  {
    get => this._BaseReceivedQty;
    set => this._BaseReceivedQty = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.PO.POLine.tranDesc))]
  [PXUIField(DisplayName = "Line Description", Enabled = false)]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.PO.POLine.receiptStatus))]
  public virtual string ReceiptStatus
  {
    get => this._ReceiptStatus;
    set => this._ReceiptStatus = value;
  }

  [PXString(2, IsFixed = true)]
  public virtual string SOOrderType
  {
    get => this._SOOrderType;
    set => this._SOOrderType = value;
  }

  [PXString(15, IsUnicode = true)]
  public virtual string SOOrderNbr
  {
    get => this._SOOrderNbr;
    set => this._SOOrderNbr = value;
  }

  [PXInt]
  public virtual int? SOOrderLineNbr
  {
    get => this._SOOrderLineNbr;
    set => this._SOOrderLineNbr = value;
  }

  [PXBool]
  public virtual bool? LinkedToCurrentSOLine { get; set; }

  [PXDecimal(6)]
  public virtual Decimal? DemandQty
  {
    get => this._DemandQty;
    set => this._DemandQty = value;
  }

  [PXDBTimestamp(BqlField = typeof (PX.Objects.PO.POLine.Tstamp))]
  public virtual byte[] tstamp { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (PX.Objects.PO.POLine.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PX.Objects.PO.POLine.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (PX.Objects.PO.POLine.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine3.selected>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine3.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine3.orderNbr>
  {
  }

  public abstract class vendorRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine3.vendorRefNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine3.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine3.sortOrder>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine3.lineType>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine3.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine3.subItemID>
  {
  }

  public abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POLine3.planID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine3.vendorID>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLine3.orderDate>
  {
  }

  public abstract class promisedDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POLine3.promisedDate>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine3.cancelled>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine3.completed>
  {
  }

  public abstract class closed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POLine3.closed>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine3.siteID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine3.uOM>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine3.orderQty>
  {
  }

  public abstract class baseOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine3.baseOrderQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine3.openQty>
  {
  }

  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine3.baseOpenQty>
  {
  }

  public abstract class receivedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine3.receivedQty>
  {
  }

  public abstract class baseReceivedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POLine3.baseReceivedQty>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine3.tranDesc>
  {
  }

  public abstract class receiptStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine3.receiptStatus>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine3.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POLine3.sOOrderNbr>
  {
  }

  public abstract class sOOrderLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POLine3.sOOrderLineNbr>
  {
  }

  public abstract class linkedToCurrentSOLine : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POLine3.linkedToCurrentSOLine>
  {
  }

  public abstract class demandQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POLine3.demandQty>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POLine3.Tstamp>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POLine3.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POLine3.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POLine3.lastModifiedDateTime>
  {
  }
}
