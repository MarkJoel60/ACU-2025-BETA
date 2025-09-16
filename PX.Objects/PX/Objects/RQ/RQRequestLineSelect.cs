// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestLineSelect
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.RQ;

[OwnedEscalatedFilter.Projection(typeof (RQRequestLineFilter), typeof (RQRequestLine), typeof (InnerJoin<RQRequest, On<RQRequest.orderNbr, Equal<RQRequestLine.orderNbr>, And<RQRequest.status, Equal<RQRequestStatus.open>, And<RQRequestLine.openQty, Greater<decimal0>>>>>), null, typeof (RQRequest.workgroupID), typeof (RQRequest.ownerID), typeof (RQRequest.orderDate), typeof (Where<CurrentValue<RQRequestLineFilter.filterSet>, Equal<False>>))]
public class RQRequestLineSelect : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _OrderNbr;
  protected int? _LineNbr;
  protected string _CuryID;
  protected Decimal? _SelectQty;
  protected Decimal? _BaseSelectQty;
  protected Decimal? _OpenQty;
  protected Decimal? _BaseOpenQty;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected string _Description;
  protected string _UOM;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected string _VendorName;
  protected string _VendorRefNbr;
  protected string _VendorDescription;
  protected string _AlternateID;
  protected DateTime? _RequestedDate;
  protected DateTime? _PromisedDate;
  protected Decimal? _ReqQty;
  protected Decimal? _BaseReqQty;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (RQRequestLine.orderNbr))]
  [PXUIField]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (RQRequestLine.lineNbr))]
  [PXUIField]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (RQRequest.curyID))]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXQuantity(typeof (RQRequestLineSelect.uOM), typeof (RQRequestLineSelect.baseSelectQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? SelectQty
  {
    get => this._SelectQty;
    set => this._SelectQty = value;
  }

  [PXDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseSelectQty
  {
    get => this._BaseSelectQty;
    set => this._BaseSelectQty = value;
  }

  [PXDBQuantity(typeof (RQRequestLine.uOM), typeof (RQRequestLine.baseOpenQty), HandleEmptyKey = true, BqlField = typeof (RQRequestLine.openQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (RQRequestLine.baseOpenQty))]
  public virtual Decimal? BaseOpenQty
  {
    get => this._BaseOpenQty;
    set => this._BaseOpenQty = value;
  }

  [Inventory(Filterable = true, BqlField = typeof (RQRequestLine.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(typeof (RQRequestLine.inventoryID), BqlField = typeof (RQRequestLine.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (RQRequestLine.description))]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<RQRequestLineSelect.inventoryID>>>>))]
  [INUnit(typeof (RQRequestLineSelect.inventoryID), DisplayName = "UOM", BqlField = typeof (RQRequestLine.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [VendorNonEmployeeActive]
  [PXDefault(typeof (Search2<PX.Objects.AP.Vendor.bAccountID, LeftJoin<InventoryItemCurySettings, On<InventoryItemCurySettings.preferredVendorID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<InventoryItemCurySettings.inventoryID, Equal<Current<RQRequestLine.inventoryID>>, And<InventoryItemCurySettings.curyID, EqualBaseCuryID<Current<RQRequisition.branchID>>>>>>, Where2<Where<Current<RQRequest.vendorID>, IsNotNull, And<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQRequest.vendorID>>>>, Or<Where<Current<RQRequest.vendorID>, IsNull, And<InventoryItemCurySettings.preferredVendorID, IsNotNull>>>>>))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequestLine.vendorID>>>))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.defLocationID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQRequestLine.vendorID>>>>))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [PXString(50, IsUnicode = true)]
  [PXDBScalar(typeof (Search<PX.Objects.AP.Vendor.acctName, Where<PX.Objects.AP.Vendor.bAccountID, Equal<RQRequestLine.vendorID>>>))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.acctName, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQRequestLine.vendorID>>>>))]
  [PXUIField(DisplayName = "Vendor Name", Enabled = false)]
  public virtual string VendorName
  {
    get => this._VendorName;
    set => this._VendorName = value;
  }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (RQRequestLine.vendorRefNbr))]
  [PXDefault]
  [PXUIField(DisplayName = "Vendor Ref.")]
  public virtual string VendorRefNbr
  {
    get => this._VendorRefNbr;
    set => this._VendorRefNbr = value;
  }

  [PXDBString(100, IsUnicode = true, BqlField = typeof (RQRequestLine.vendorDescription))]
  [PXDefault]
  [PXUIField(DisplayName = "Vendor Description")]
  public virtual string VendorDescription
  {
    get => this._VendorDescription;
    set => this._VendorDescription = value;
  }

  [PXDBString(50, IsUnicode = true, InputMask = "", BqlField = typeof (RQRequestLine.alternateID))]
  [PXUIField(DisplayName = "Alternate ID")]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBDate(BqlField = typeof (RQRequestLine.requestedDate))]
  [PXUIField(DisplayName = "Required Date")]
  public virtual DateTime? RequestedDate
  {
    get => this._RequestedDate;
    set => this._RequestedDate = value;
  }

  [PXDBDate(BqlField = typeof (RQRequestLine.promisedDate))]
  [PXUIField(DisplayName = "Promised Date")]
  public virtual DateTime? PromisedDate
  {
    get => this._PromisedDate;
    set => this._PromisedDate = value;
  }

  [PXDBQuantity(typeof (RQRequestLine.uOM), typeof (RQRequestLine.baseReqQty), HandleEmptyKey = true, BqlField = typeof (RQRequestLine.reqQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Requisition Qty.", Enabled = false)]
  public virtual Decimal? ReqQty
  {
    get => this._ReqQty;
    set => this._ReqQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (RQRequestLine.baseReqQty))]
  public virtual Decimal? BaseReqQty
  {
    get => this._BaseReqQty;
    set => this._BaseReqQty = value;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestLineSelect.selected>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLineSelect.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLineSelect.lineNbr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLineSelect.curyID>
  {
  }

  public abstract class selectQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLineSelect.selectQty>
  {
  }

  public abstract class baseSelectQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLineSelect.baseSelectQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestLineSelect.openQty>
  {
  }

  public abstract class baseOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLineSelect.baseOpenQty>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLineSelect.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLineSelect.subItemID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestLineSelect.description>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLineSelect.uOM>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLineSelect.vendorID>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequestLineSelect.vendorLocationID>
  {
  }

  public abstract class vendorName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestLineSelect.vendorName>
  {
  }

  public abstract class vendorRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestLineSelect.vendorRefNbr>
  {
  }

  public abstract class vendorDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestLineSelect.vendorDescription>
  {
  }

  public abstract class alternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestLineSelect.alternateID>
  {
  }

  public abstract class requestedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestLineSelect.requestedDate>
  {
  }

  public abstract class promisedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestLineSelect.promisedDate>
  {
  }

  public abstract class reqQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestLineSelect.reqQty>
  {
  }

  public abstract class baseReqQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLineSelect.baseReqQty>
  {
  }
}
