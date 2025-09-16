// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXCacheName("Request Line")]
[Serializable]
public class RQRequestLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected 
  #nullable disable
  string _OrderNbr;
  protected int? _LineNbr;
  protected string _DepartmentID;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected string _Description;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected string _VendorName;
  protected string _VendorRefNbr;
  protected string _VendorDescription;
  protected string _UOM;
  protected string _AlternateID;
  protected Decimal? _OrderQty;
  protected Decimal? _BaseOrderQty;
  protected Decimal? _OriginQty;
  protected Decimal? _BaseOriginQty;
  protected Decimal? _IssuedQty;
  protected Decimal? _BaseIssuedQty;
  protected Decimal? _ReqQty;
  protected Decimal? _BaseReqQty;
  protected Decimal? _OpenQty;
  protected Decimal? _BaseOpenQty;
  protected string _IssueStatus;
  protected long? _CuryInfoID;
  protected Decimal? _CuryEstUnitCost;
  protected Decimal? _EstUnitCost;
  protected Decimal? _CuryEstExtCost;
  protected Decimal? _EstExtCost;
  protected int? _ExpenseAcctID;
  protected int? _ExpenseSubID;
  protected Guid? _NoteID;
  protected DateTime? _RequestedDate;
  protected DateTime? _PromisedDate;
  protected bool? _Approved;
  protected bool? _Cancelled;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected bool? _Updatable = new bool?(false);

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [Branch(typeof (RQRequest.branchID), null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (RQRequest.orderNbr), DefaultForUpdate = false)]
  [PXParent(typeof (RQRequestLine.FK.Request))]
  [PXUIField]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  [PXLineNbr(typeof (RQRequest.lineCntr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (RQRequest.departmentID))]
  public virtual string DepartmentID
  {
    get => this._DepartmentID;
    set => this._DepartmentID = value;
  }

  [RQRequestInventoryItem(typeof (RQRequest.reqClassID), Filterable = true)]
  [PXForeignReference(typeof (RQRequestLine.FK.InventoryItem))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<RQRequestLine.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [SubItem(typeof (RQRequestLine.inventoryID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.descr, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<RQRequestLine.inventoryID>>>>))]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [VendorNonEmployeeActive]
  [PXDefault(typeof (Search2<PX.Objects.AP.Vendor.bAccountID, LeftJoin<InventoryItemCurySettings, On<InventoryItemCurySettings.preferredVendorID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<InventoryItemCurySettings.inventoryID, Equal<Current<RQRequestLine.inventoryID>>, And<InventoryItemCurySettings.curyID, EqualBaseCuryID<Current<RQRequest.branchID>>>>>>, Where2<Where<Current<RQRequest.vendorID>, IsNotNull, And<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQRequest.vendorID>>>>, Or<Where<Current<RQRequest.vendorID>, IsNull, And<InventoryItemCurySettings.preferredVendorID, IsNotNull>>>>>))]
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

  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Vendor Ref.")]
  public virtual string VendorRefNbr
  {
    get => this._VendorRefNbr;
    set => this._VendorRefNbr = value;
  }

  [PXDBString(100, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Vendor Description")]
  public virtual string VendorDescription
  {
    get => this._VendorDescription;
    set => this._VendorDescription = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<RQRequestLine.inventoryID>>>>))]
  [INUnit(typeof (RQRequestLine.inventoryID), DisplayName = "UOM")]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [AlternativeItem(INPrimaryAlternateType.VPN, typeof (RQRequestLine.vendorID), typeof (RQRequestLine.inventoryID), typeof (RQRequestLine.subItemID), typeof (RQRequestLine.uOM))]
  public virtual string AlternateID
  {
    get => this._AlternateID;
    set => this._AlternateID = value;
  }

  [PXDBQuantity(typeof (RQRequestLine.uOM), typeof (RQRequestLine.baseOrderQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOrderQty
  {
    get => this._BaseOrderQty;
    set => this._BaseOrderQty = value;
  }

  [PXDBQuantity(typeof (RQRequestLine.uOM), typeof (RQRequestLine.baseOriginQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Original Qty.", Enabled = false)]
  [Obsolete("Not used anywhere. Consider removing on refactoring.")]
  public virtual Decimal? OriginQty
  {
    get => this._OriginQty;
    set => this._OriginQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [Obsolete("Not used anywhere. Consider removing on refactoring.")]
  public virtual Decimal? BaseOriginQty
  {
    get => this._BaseOriginQty;
    set => this._BaseOriginQty = value;
  }

  [PXDBQuantity(typeof (RQRequestLine.uOM), typeof (RQRequestLine.baseIssuedQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Issued Qty.", Enabled = false)]
  [Obsolete("Not used anywhere. Consider removing on refactoring.")]
  public virtual Decimal? IssuedQty
  {
    get => this._IssuedQty;
    set => this._IssuedQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [Obsolete("Not used anywhere. Consider removing on refactoring.")]
  public virtual Decimal? BaseIssuedQty
  {
    get => this._BaseIssuedQty;
    set => this._BaseIssuedQty = value;
  }

  [PXDBQuantity(typeof (RQRequestLine.uOM), typeof (RQRequestLine.baseReqQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Requisition Qty.", Enabled = false)]
  public virtual Decimal? ReqQty
  {
    get => this._ReqQty;
    set => this._ReqQty = value;
  }

  [PXDBDecimal(6)]
  public virtual Decimal? BaseReqQty
  {
    get => this._BaseReqQty;
    set => this._BaseReqQty = value;
  }

  [PXDBQuantity(typeof (RQRequestLine.uOM), typeof (RQRequestLine.baseOpenQty), HandleEmptyKey = true)]
  [PXFormula(null, typeof (SumCalc<RQRequest.openOrderQty>), ValidateAggregateCalculation = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  [PXDBDecimal(6)]
  public virtual Decimal? BaseOpenQty
  {
    get => this._BaseOpenQty;
    set => this._BaseOpenQty = value;
  }

  [PXString]
  [PXUIField]
  [RQRequestIssueType.List]
  public string IssueStatus
  {
    get => this._IssueStatus;
    set => this._IssueStatus = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (RQRequest.curyInfoID))]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (RQRequestLine.curyInfoID), typeof (RQRequestLine.estUnitCost))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryEstUnitCost
  {
    get => this._CuryEstUnitCost;
    set => this._CuryEstUnitCost = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Search<INItemCost.lastCost, Where<INItemCost.inventoryID, Equal<Current<RQRequestLine.inventoryID>>, And<INItemCost.curyID, EqualBaseCuryID<Current2<RQRequest.branchID>>>>>))]
  [PXUIField(DisplayName = "Est. Unit Cost")]
  public virtual Decimal? EstUnitCost
  {
    get => this._EstUnitCost;
    set => this._EstUnitCost = value;
  }

  [PXDBCurrency(typeof (RQRequestLine.curyInfoID), typeof (RQRequestLine.estExtCost))]
  [PXUIField]
  [PXFormula(typeof (Mult<RQRequestLine.orderQty, RQRequestLine.curyEstUnitCost>))]
  [PXFormula(null, typeof (SumCalc<RQRequest.curyEstExtCostTotal>), ValidateAggregateCalculation = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryEstExtCost
  {
    get => this._CuryEstExtCost;
    set => this._CuryEstExtCost = value;
  }

  [PXUIField(DisplayName = "Est. Ext. Cost")]
  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? EstExtCost
  {
    get => this._EstExtCost;
    set => this._EstExtCost = value;
  }

  [Account]
  [PXDefault]
  public virtual int? ExpenseAcctID
  {
    get => this._ExpenseAcctID;
    set => this._ExpenseAcctID = value;
  }

  [SubAccount(typeof (RQRequestLine.expenseAcctID), typeof (RQRequestLine.branchID), false)]
  [PXDefault]
  public virtual int? ExpenseSubID
  {
    get => this._ExpenseSubID;
    set => this._ExpenseSubID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Cost", Visible = false)]
  public virtual bool? ManualPrice { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Required Date")]
  public virtual DateTime? RequestedDate
  {
    get => this._RequestedDate;
    set => this._RequestedDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Promised Date")]
  public virtual DateTime? PromisedDate
  {
    get => this._PromisedDate;
    set => this._PromisedDate = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Approved
  {
    get => this._Approved;
    set => this._Approved = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? Cancelled
  {
    get => this._Cancelled;
    set => this._Cancelled = value;
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

  [PXBool]
  [PXDefault(false)]
  public virtual bool? Updatable
  {
    get => this._Updatable;
    set => this._Updatable = value;
  }

  public class PK : PrimaryKeyOf<RQRequestLine>.By<RQRequestLine.orderNbr, RQRequestLine.lineNbr>
  {
    public static RQRequestLine Find(
      PXGraph graph,
      string orderNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (RQRequestLine) PrimaryKeyOf<RQRequestLine>.By<RQRequestLine.orderNbr, RQRequestLine.lineNbr>.FindBy(graph, (object) orderNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Request : 
      PrimaryKeyOf<RQRequest>.By<RQRequest.orderNbr>.ForeignKeyOf<RQRequestLine>.By<RQRequestLine.orderNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<RQRequestLine>.By<RQRequestLine.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<RQRequestLine>.By<RQRequestLine.subItemID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestLine.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLine.branchID>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLine.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLine.lineNbr>
  {
  }

  public abstract class departmentID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLine.departmentID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLine.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLine.subItemID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLine.description>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLine.vendorID>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequestLine.vendorLocationID>
  {
  }

  public abstract class vendorName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLine.vendorName>
  {
  }

  public abstract class vendorRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLine.vendorRefNbr>
  {
  }

  public abstract class vendorDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestLine.vendorDescription>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLine.uOM>
  {
  }

  public abstract class alternateID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLine.alternateID>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestLine.orderQty>
  {
  }

  public abstract class baseOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLine.baseOrderQty>
  {
  }

  [Obsolete("Not used anywhere. Consider removing on refactoring.")]
  public abstract class originQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestLine.originQty>
  {
  }

  [Obsolete("Not used anywhere. Consider removing on refactoring.")]
  public abstract class baseOriginQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLine.baseOriginQty>
  {
  }

  [Obsolete("Not used anywhere. Consider removing on refactoring.")]
  public abstract class issuedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestLine.issuedQty>
  {
  }

  [Obsolete("Not used anywhere. Consider removing on refactoring.")]
  public abstract class baseIssuedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLine.baseIssuedQty>
  {
  }

  public abstract class reqQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestLine.reqQty>
  {
  }

  public abstract class baseReqQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestLine.baseReqQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestLine.openQty>
  {
  }

  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestLine.baseOpenQty>
  {
  }

  public abstract class issueStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLine.issueStatus>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  RQRequestLine.curyInfoID>
  {
  }

  public abstract class curyEstUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLine.curyEstUnitCost>
  {
  }

  public abstract class estUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestLine.estUnitCost>
  {
  }

  public abstract class curyEstExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestLine.curyEstExtCost>
  {
  }

  public abstract class estExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestLine.estExtCost>
  {
  }

  public abstract class expenseAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLine.expenseAcctID>
  {
  }

  public abstract class expenseSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLine.expenseSubID>
  {
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestLine.manualPrice>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequestLine.noteID>
  {
  }

  public abstract class requestedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestLine.requestedDate>
  {
  }

  public abstract class promisedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestLine.promisedDate>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestLine.approved>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestLine.cancelled>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RQRequestLine.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequestLine.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestLine.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestLine.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RQRequestLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestLine.lastModifiedDateTime>
  {
  }

  public abstract class updatable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestLine.updatable>
  {
  }
}
