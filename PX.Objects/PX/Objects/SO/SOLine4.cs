// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLine4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXProjection(typeof (Select<SOLine, Where<SOLine.lineType, NotEqual<SOLineType.miscCharge>>>), Persistent = true)]
[Serializable]
public class SOLine4 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected string _Operation;
  protected string _ShipComplete;
  protected int? _InventoryID;
  protected string _UOM;
  protected Decimal? _OrderQty;
  protected Decimal? _BaseShippedQty;
  protected Decimal? _ShippedQty;
  protected Decimal? _UnbilledQty;
  protected Decimal? _BaseUnbilledQty;
  protected Decimal? _OpenQty;
  protected Decimal? _BaseOpenQty;
  protected Decimal? _CompleteQtyMin;
  protected Decimal? _CompleteQtyMax;
  protected long? _CuryInfoID;
  protected Decimal? _CuryUnitPrice;
  protected Decimal? _UnitPrice;
  protected Decimal? _DiscPct;
  protected Decimal? _CuryOpenAmt;
  protected Decimal? _OpenAmt;
  protected Decimal? _CuryUnbilledAmt;
  protected Decimal? _UnbilledAmt;
  protected Decimal? _GroupDiscountRate;
  protected Decimal? _DocumentDiscountRate;
  protected string _TaxCategoryID;
  protected bool? _OpenLine;

  [PXDBInt(BqlField = typeof (SOLine.branchID))]
  public virtual int? BranchID { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (SOLine.orderType))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (SOLine.orderNbr))]
  [PXParent(typeof (Select<SOOrder, Where<SOOrder.orderType, Equal<Current<SOLine4.orderType>>, And<SOOrder.orderNbr, Equal<Current<SOLine4.orderNbr>>>>>))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (SOLine.lineNbr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(BqlField = typeof (SOLine.sortOrder))]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBString(1, IsFixed = true, InputMask = ">a", BqlField = typeof (SOLine.operation))]
  [PXUIField(DisplayName = "Operation")]
  [SOOperation.List]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBShort(BqlField = typeof (SOLine.lineSign))]
  [PXDefault]
  public virtual short? LineSign { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (SOLine.shipComplete))]
  public virtual string ShipComplete
  {
    get => this._ShipComplete;
    set => this._ShipComplete = value;
  }

  [PXDBInt(BqlField = typeof (SOLine.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.SiteID" />
  [PXParent(typeof (Select<SOOrderSite, Where<SOOrderSite.orderType, Equal<Current<SOLine4.orderType>>, And<SOOrderSite.orderNbr, Equal<Current<SOLine4.orderNbr>>, And<SOOrderSite.siteID, Equal<Current2<SOLine4.siteID>>>>>>), LeaveChildren = true, ParentCreate = true)]
  [PXUnboundFormula(typeof (IIf<Where<BqlOperand<SOLine4.openLine, IBqlBool>.IsEqual<True>>, int1, int0>), typeof (SumCalc<SOOrderSite.openLineCntr>), SkipZeroUpdates = false, ValidateAggregateCalculation = true)]
  [PXDBInt(BqlField = typeof (SOLine.siteID))]
  public virtual int? SiteID { get; set; }

  [INUnit(typeof (SOLine4.inventoryID), BqlField = typeof (SOLine.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBBaseQuantity(typeof (SOLine4.uOM), typeof (SOLine4.orderQty), BqlField = typeof (SOLine.baseOrderQty))]
  [PXDefault]
  public virtual Decimal? BaseOrderQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (SOLine.orderQty))]
  [PXDefault]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBBaseQuantity(typeof (SOLine4.uOM), typeof (SOLine4.shippedQty), BqlField = typeof (SOLine.baseShippedQty))]
  public virtual Decimal? BaseShippedQty
  {
    get => this._BaseShippedQty;
    set => this._BaseShippedQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.shippedQty))]
  [PXDefault]
  public virtual Decimal? ShippedQty
  {
    get => this._ShippedQty;
    set => this._ShippedQty = value;
  }

  [PXDBQuantity(typeof (SOLine4.uOM), typeof (SOLine4.baseUnbilledQty), BqlField = typeof (SOLine.unbilledQty))]
  [PXUnboundFormula(typeof (BqlOperand<SOLine4.unbilledQty, IBqlDecimal>.Multiply<SOLine4.lineSign>), typeof (SumCalc<SOOrder.unbilledOrderQty>))]
  [PXDefault]
  public virtual Decimal? UnbilledQty
  {
    get => this._UnbilledQty;
    set => this._UnbilledQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.baseUnbilledQty))]
  public virtual Decimal? BaseUnbilledQty
  {
    get => this._BaseUnbilledQty;
    set => this._BaseUnbilledQty = value;
  }

  [PXDBQuantity(typeof (SOLine4.uOM), typeof (SOLine4.baseOpenQty), BqlField = typeof (SOLine.openQty))]
  [PXFormula(typeof (Switch<Case<Where<SOLine4.lineSign, Equal<shortMinus1>>, Minimum<Sub<SOLine4.orderQty, SOLine4.shippedQty>, decimal0>>, Maximum<Sub<SOLine4.orderQty, SOLine4.shippedQty>, decimal0>>))]
  [PXUnboundFormula(typeof (BqlOperand<SOLine4.openQty, IBqlDecimal>.Multiply<SOLine4.lineSign>), typeof (SumCalc<SOOrder.openOrderQty>))]
  [PXDefault]
  public virtual Decimal? OpenQty
  {
    get => this._OpenQty;
    set => this._OpenQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.baseOpenQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Open Qty.")]
  public virtual Decimal? BaseOpenQty
  {
    get => this._BaseOpenQty;
    set => this._BaseOpenQty = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0, BqlField = typeof (SOLine.completeQtyMin))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CompleteQtyMin
  {
    get => this._CompleteQtyMin;
    set => this._CompleteQtyMin = value;
  }

  [PXDBDecimal(2, MinValue = 100.0, MaxValue = 999.0, BqlField = typeof (SOLine.completeQtyMax))]
  [PXDefault(TypeCode.Decimal, "100.0")]
  public virtual Decimal? CompleteQtyMax
  {
    get => this._CompleteQtyMax;
    set => this._CompleteQtyMax = value;
  }

  [PXDBBool(BqlField = typeof (SOLine.completed))]
  public virtual bool? Completed { get; set; }

  [PXDBLong(BqlField = typeof (SOLine.curyInfoID))]
  [CurrencyInfo]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.curyUnitPrice))]
  [PXDefault]
  public virtual Decimal? CuryUnitPrice
  {
    get => this._CuryUnitPrice;
    set => this._CuryUnitPrice = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.unitPrice))]
  [PXDefault]
  public virtual Decimal? UnitPrice
  {
    get => this._UnitPrice;
    set => this._UnitPrice = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.discPct))]
  [PXDefault]
  public virtual Decimal? DiscPct
  {
    get => this._DiscPct;
    set => this._DiscPct = value;
  }

  [PXDBCurrency(typeof (SOLine4.curyInfoID), typeof (SOLine4.openAmt), BqlField = typeof (SOLine.curyOpenAmt))]
  [PXFormula(typeof (Mult<Mult<SOLine4.openQty, SOLine4.curyUnitPrice>, Sub<decimal1, Div<SOLine4.discPct, decimal100>>>))]
  [PXUIField(DisplayName = "Open Amount")]
  [PXDefault]
  public virtual Decimal? CuryOpenAmt
  {
    get => this._CuryOpenAmt;
    set => this._CuryOpenAmt = value;
  }

  [PXDBDecimal(4, BqlField = typeof (SOLine.openAmt))]
  [PXDefault]
  public virtual Decimal? OpenAmt
  {
    get => this._OpenAmt;
    set => this._OpenAmt = value;
  }

  [PXDBCurrency(typeof (SOLine4.curyInfoID), typeof (SOLine4.unbilledAmt), BqlField = typeof (SOLine.curyUnbilledAmt))]
  [PXFormula(typeof (Mult<Mult<SOLine4.unbilledQty, SOLine4.curyUnitPrice>, Sub<decimal1, Div<SOLine4.discPct, decimal100>>>))]
  [PXDefault]
  public virtual Decimal? CuryUnbilledAmt
  {
    get => this._CuryUnbilledAmt;
    set => this._CuryUnbilledAmt = value;
  }

  [PXDBDecimal(4, BqlField = typeof (SOLine.unbilledAmt))]
  [PXDefault]
  public virtual Decimal? UnbilledAmt
  {
    get => this._UnbilledAmt;
    set => this._UnbilledAmt = value;
  }

  [PXDBDecimal(18, BqlField = typeof (SOLine.groupDiscountRate))]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? GroupDiscountRate
  {
    get => this._GroupDiscountRate;
    set => this._GroupDiscountRate = value;
  }

  [PXDBDecimal(18, BqlField = typeof (SOLine.documentDiscountRate))]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? DocumentDiscountRate
  {
    get => this._DocumentDiscountRate;
    set => this._DocumentDiscountRate = value;
  }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (SOLine.taxCategoryID))]
  [SOOpenTax4(typeof (SOOrder), typeof (SOTax), typeof (SOTaxTran), Inventory = typeof (SOLine4.inventoryID), UOM = typeof (SOLine4.uOM), LineQty = typeof (SOLine4.openQty))]
  [SOUnbilledTax4(typeof (SOOrder), typeof (SOTax), typeof (SOTaxTran), Inventory = typeof (SOLine4.inventoryID), UOM = typeof (SOLine4.uOM), LineQty = typeof (SOLine4.unbilledQty))]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  [PXDBDate(BqlField = typeof (SOLine.shipDate))]
  public virtual DateTime? ShipDate { get; set; }

  [PXDBDecimal(4, BqlField = typeof (SOLine.lineAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? LineAmt { get; set; }

  [PXDBInt(BqlField = typeof (SOLine.salesAcctID))]
  public virtual int? SalesAcctID { get; set; }

  [PXDBInt(BqlField = typeof (SOLine.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(BqlField = typeof (SOLine.taskID))]
  public virtual int? TaskID { get; set; }

  [PXDBBool(BqlField = typeof (SOLine.openLine))]
  public virtual bool? OpenLine
  {
    get => this._OpenLine;
    set => this._OpenLine = value;
  }

  [PXDBBool(BqlField = typeof (SOLine.pOCreate))]
  public virtual bool? POCreate { get; set; }

  [PXDBString(BqlField = typeof (SOLine.pOSource))]
  public virtual string POSource { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BlanketType" />
  [PXDBString(2, IsFixed = true, BqlField = typeof (SOLine.blanketType))]
  public virtual string BlanketType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BlanketNbr" />
  [PXDBString(15, IsUnicode = true, BqlField = typeof (SOLine.blanketNbr))]
  public virtual string BlanketNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BlanketLineNbr" />
  [PXDBInt(BqlField = typeof (SOLine.blanketLineNbr))]
  public virtual int? BlanketLineNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.BlanketSplitLineNbr" />
  [PXDBInt(BqlField = typeof (SOLine.blanketSplitLineNbr))]
  public virtual int? BlanketSplitLineNbr { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine4.branchID>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine4.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine4.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine4.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine4.sortOrder>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine4.operation>
  {
  }

  public abstract class lineSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOLine4.lineSign>
  {
  }

  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine4.shipComplete>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine4.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine4.siteID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine4.uOM>
  {
  }

  public abstract class baseOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.baseOrderQty>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.orderQty>
  {
  }

  public abstract class baseShippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.baseShippedQty>
  {
  }

  public abstract class shippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.shippedQty>
  {
  }

  public abstract class unbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.unbilledQty>
  {
  }

  public abstract class baseUnbilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine4.baseUnbilledQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.openQty>
  {
  }

  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.baseOpenQty>
  {
  }

  public abstract class completeQtyMin : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.completeQtyMin>
  {
  }

  public abstract class completeQtyMax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.completeQtyMax>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine4.completed>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOLine4.curyInfoID>
  {
  }

  public abstract class curyUnitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.curyUnitPrice>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.unitPrice>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.discPct>
  {
  }

  public abstract class curyOpenAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.curyOpenAmt>
  {
  }

  public abstract class openAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.openAmt>
  {
  }

  public abstract class curyUnbilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine4.curyUnbilledAmt>
  {
  }

  public abstract class unbilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.unbilledAmt>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine4.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine4.documentDiscountRate>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine4.taxCategoryID>
  {
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLine4.shipDate>
  {
  }

  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine4.lineAmt>
  {
  }

  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine4.salesAcctID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine4.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine4.taskID>
  {
  }

  public abstract class openLine : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine4.openLine>
  {
  }

  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine4.pOCreate>
  {
  }

  public abstract class pOSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine4.pOSource>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOLine4.Tstamp>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine4.BlanketType" />
  public abstract class blanketType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine4.blanketType>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine4.BlanketNbr" />
  public abstract class blanketNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine4.blanketNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine4.BlanketLineNbr" />
  public abstract class blanketLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine4.blanketLineNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine4.BlanketSplitLineNbr" />
  public abstract class blanketSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOLine4.blanketSplitLineNbr>
  {
  }
}
