// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOLine2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXProjection(typeof (Select2<SOLine, InnerJoin<SOOrderType, On<SOLine.FK.OrderType>, InnerJoin<SOOrderTypeOperation, On<SOLine.FK.OrderTypeOperation>>>, Where<SOLine.lineType, NotEqual<SOLineType.miscCharge>>>), new Type[] {typeof (SOLine)})]
[Serializable]
public class SOLine2 : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _Behavior;
  protected string _OrderNbr;
  protected int? _LineNbr;
  protected int? _SortOrder;
  protected string _LineType;
  protected string _Operation;
  protected string _ShipComplete;
  protected bool? _Completed;
  protected int? _InventoryID;
  protected int? _SubItemID;
  protected int? _SiteID;
  protected int? _SalesAcctID;
  protected int? _SalesSubID;
  protected string _TranDesc;
  protected string _UOM;
  protected Decimal? _OrderQty;
  protected Decimal? _BaseShippedQty;
  protected Decimal? _ShippedQty;
  protected Decimal? _BilledQty;
  protected Decimal? _BaseBilledQty;
  protected Decimal? _UnbilledQty;
  protected Decimal? _BaseUnbilledQty;
  protected Decimal? _OpenQty;
  protected Decimal? _BaseOpenQty;
  protected Decimal? _CompleteQtyMin;
  protected Decimal? _CompleteQtyMax;
  protected DateTime? _ShipDate;
  protected long? _CuryInfoID;
  protected Decimal? _CuryUnitPrice;
  protected Decimal? _DiscPct;
  protected Decimal? _CuryBilledAmt;
  protected Decimal? _BilledAmt;
  protected Decimal? _CuryOpenAmt;
  protected Decimal? _OpenAmt;
  protected Decimal? _CuryUnbilledAmt;
  protected Decimal? _UnbilledAmt;
  protected Decimal? _GroupDiscountRate;
  protected Decimal? _DocumentDiscountRate;
  protected string _TaxCategoryID;
  protected string _PlanType;
  protected string _POSource;
  protected Decimal? _CuryExtPrice;

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (SOLine.orderType))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.Behavior" />
  [PXDBString(2, IsFixed = true, InputMask = ">aa", BqlField = typeof (SOLine.behavior))]
  public virtual string Behavior
  {
    get => this._Behavior;
    set => this._Behavior = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "", BqlField = typeof (SOLine.orderNbr))]
  [PXParent(typeof (Select<SOOrder, Where<SOOrder.orderType, Equal<Current<SOLine2.orderType>>, And<SOOrder.orderNbr, Equal<Current<SOLine2.orderNbr>>>>>))]
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

  [PXDBString(2, IsFixed = true, BqlField = typeof (SOLine.lineType))]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
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

  [PXDBBool(BqlField = typeof (SOLine.completed))]
  public virtual bool? Completed
  {
    get => this._Completed;
    set => this._Completed = value;
  }

  [PXDBInt(BqlField = typeof (SOLine.inventoryID))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(BqlField = typeof (SOLine.subItemID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDBInt(BqlField = typeof (SOLine.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBInt(BqlField = typeof (SOLine.salesAcctID))]
  public virtual int? SalesAcctID
  {
    get => this._SalesAcctID;
    set => this._SalesAcctID = value;
  }

  [PXDBInt(BqlField = typeof (SOLine.salesSubID))]
  public virtual int? SalesSubID
  {
    get => this._SalesSubID;
    set => this._SalesSubID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (SOLine.tranDesc))]
  public virtual string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  [INUnit(typeof (SOLine2.inventoryID), BqlField = typeof (SOLine.uOM))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.orderQty))]
  [PXDefault]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.baseOrderQty))]
  [PXDefault]
  public virtual Decimal? BaseOrderQty { get; set; }

  [PXDBBaseQtyWithOrigQty(typeof (SOLine2.uOM), typeof (SOLine2.shippedQty), typeof (SOLine2.uOM), typeof (SOLine2.baseOrderQty), typeof (SOLine2.orderQty), BqlField = typeof (SOLine.baseShippedQty))]
  [PXDefault]
  public virtual Decimal? BaseShippedQty
  {
    get => this._BaseShippedQty;
    set => this._BaseShippedQty = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (SOLine2.baseShippedQty), typeof (Decimal), Persistent = true)]
  public virtual Decimal? OriginalBaseShippedQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (SOLine.shippedQty))]
  [PXDefault]
  public virtual Decimal? ShippedQty
  {
    get => this._ShippedQty;
    set => this._ShippedQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.billedQty))]
  [PXDefault]
  public virtual Decimal? BilledQty
  {
    get => this._BilledQty;
    set => this._BilledQty = value;
  }

  [PXDBBaseQuantity(typeof (SOLine2.uOM), typeof (SOLine2.billedQty), BqlField = typeof (SOLine.baseBilledQty))]
  [PXDefault]
  public virtual Decimal? BaseBilledQty
  {
    get => this._BaseBilledQty;
    set => this._BaseBilledQty = value;
  }

  [PXDBQuantity(BqlField = typeof (SOLine.unbilledQty))]
  [PXUnboundFormula(typeof (BqlOperand<SOLine2.unbilledQty, IBqlDecimal>.Multiply<SOLine2.lineSign>), typeof (SumCalc<SOOrder.unbilledOrderQty>))]
  [PXDefault]
  public virtual Decimal? UnbilledQty
  {
    get => this._UnbilledQty;
    set => this._UnbilledQty = value;
  }

  [PXDBBaseQuantity(typeof (SOLine2.uOM), typeof (SOLine2.unbilledQty), BqlField = typeof (SOLine.baseUnbilledQty))]
  [PXDefault]
  public virtual Decimal? BaseUnbilledQty
  {
    get => this._BaseUnbilledQty;
    set => this._BaseUnbilledQty = value;
  }

  [PXDBDecimal(6, BqlField = typeof (SOLine.openQty))]
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

  [PXDBDate(BqlField = typeof (SOLine.shipDate))]
  public virtual DateTime? ShipDate
  {
    get => this._ShipDate;
    set => this._ShipDate = value;
  }

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

  [PXDBPriceCostCalced(typeof (Switch<Case<Where<SOLine.orderQty, Equal<decimal0>>, SOLine.unitPrice>, Div<SOLine.extPrice, SOLine.orderQty>>), typeof (Decimal), CastToScale = 9, CastToPrecision = 25)]
  public virtual Decimal? ActualUnitPrice { get; set; }

  [PXDBDecimal(6, BqlField = typeof (SOLine.unitCost))]
  [PXDefault]
  public virtual Decimal? UnitCost { get; set; }

  [PXDBDecimal(6, BqlField = typeof (SOLine.discPct))]
  [PXDefault]
  public virtual Decimal? DiscPct
  {
    get => this._DiscPct;
    set => this._DiscPct = value;
  }

  [PXFormula(typeof (Mult<Mult<SOLine2.billedQty, SOLine2.curyUnitPrice>, Sub<decimal1, Div<SOLine2.discPct, decimal100>>>))]
  [PXDBCurrency(typeof (SOLine2.curyInfoID), typeof (SOLine2.billedAmt), BqlField = typeof (SOLine.curyBilledAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryBilledAmt
  {
    get => this._CuryBilledAmt;
    set => this._CuryBilledAmt = value;
  }

  [PXDBDecimal(4, BqlField = typeof (SOLine.billedAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BilledAmt
  {
    get => this._BilledAmt;
    set => this._BilledAmt = value;
  }

  [PXDBCurrency(typeof (SOLine2.curyInfoID), typeof (SOLine2.openAmt), BqlField = typeof (SOLine.curyOpenAmt))]
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

  [PXDBCurrency(typeof (SOLine2.curyInfoID), typeof (SOLine2.unbilledAmt), BqlField = typeof (SOLine.curyUnbilledAmt))]
  [PXFormula(typeof (Mult<Mult<SOLine2.unbilledQty, SOLine2.curyUnitPrice>, Sub<decimal1, Div<SOLine2.discPct, decimal100>>>))]
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

  [PXDBBool(BqlField = typeof (SOLine.disableAutomaticTaxCalculation))]
  public virtual bool? DisableAutomaticTaxCalculation { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (SOLine.taxZoneID))]
  public virtual string TaxZoneID { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (SOLine.taxCategoryID))]
  [SOUnbilledTax2(typeof (SOOrder), typeof (SOTax), typeof (SOTaxTran), Inventory = typeof (SOLine2.inventoryID), UOM = typeof (SOLine2.uOM), LineQty = typeof (SOLine2.unbilledQty))]
  [SOOpenTax2(typeof (SOOrder), typeof (SOTax), typeof (SOTaxTran), Inventory = typeof (SOLine2.inventoryID), UOM = typeof (SOLine2.uOM), LineQty = typeof (SOLine2.openQty))]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (SOOrderTypeOperation.orderPlanType))]
  public virtual string PlanType
  {
    get => this._PlanType;
    set => this._PlanType = value;
  }

  [PXDBString(BqlField = typeof (SOLine.pOSource))]
  public virtual string POSource
  {
    get => this._POSource;
    set => this._POSource = value;
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryDiscAmt" />
  [PXDBCurrency(typeof (SOLine2.curyInfoID), typeof (SOLine2.discAmt), BqlField = typeof (SOLine.curyDiscAmt))]
  [PXUIField(DisplayName = "Discount Amount")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDiscAmt { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.DiscAmt" />
  [PXDBDecimal(4, BqlField = typeof (SOLine.discAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.CuryExtPrice" />
  [PXDBCurrency(typeof (SOLine2.curyInfoID), typeof (SOLine2.extPrice), BqlField = typeof (SOLine.curyExtPrice))]
  [PXUIField(DisplayName = "Ext. Price")]
  [PXFormula(typeof (Mult<SOLine2.orderQty, SOLine2.curyUnitPrice>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryExtPrice { get; set; }

  /// <inheritdoc cref="P:PX.Objects.SO.SOLine.ExtPrice" />
  [PXDBDecimal(4, BqlField = typeof (SOLine.extPrice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtPrice { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (SOLine.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (SOLine.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (SOLine.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine2.orderType>
  {
  }

  public abstract class behavior : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine2.behavior>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine2.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine2.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine2.sortOrder>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine2.lineType>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine2.operation>
  {
  }

  public abstract class lineSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOLine2.lineSign>
  {
  }

  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine2.shipComplete>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOLine2.completed>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine2.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine2.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine2.siteID>
  {
  }

  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine2.salesAcctID>
  {
  }

  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOLine2.salesSubID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine2.tranDesc>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine2.uOM>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.orderQty>
  {
  }

  public abstract class baseOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.baseOrderQty>
  {
  }

  public abstract class baseShippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.baseShippedQty>
  {
  }

  public abstract class originalBaseShippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine2.originalBaseShippedQty>
  {
  }

  public abstract class shippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.shippedQty>
  {
  }

  public abstract class billedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.billedQty>
  {
  }

  public abstract class baseBilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.baseBilledQty>
  {
  }

  public abstract class unbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.unbilledQty>
  {
  }

  public abstract class baseUnbilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine2.baseUnbilledQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.openQty>
  {
  }

  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.baseOpenQty>
  {
  }

  public abstract class completeQtyMin : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.completeQtyMin>
  {
  }

  public abstract class completeQtyMax : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.completeQtyMax>
  {
  }

  public abstract class shipDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  SOLine2.shipDate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOLine2.curyInfoID>
  {
  }

  public abstract class curyUnitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.curyUnitPrice>
  {
  }

  public abstract class actualUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine2.actualUnitPrice>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.unitCost>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.discPct>
  {
  }

  public abstract class curyBilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.curyBilledAmt>
  {
  }

  public abstract class billedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.billedAmt>
  {
  }

  public abstract class curyOpenAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.curyOpenAmt>
  {
  }

  public abstract class openAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.openAmt>
  {
  }

  public abstract class curyUnbilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine2.curyUnbilledAmt>
  {
  }

  public abstract class unbilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.unbilledAmt>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine2.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOLine2.documentDiscountRate>
  {
  }

  public abstract class disableAutomaticTaxCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOLine2.disableAutomaticTaxCalculation>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine2.taxZoneID>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine2.taxCategoryID>
  {
  }

  public abstract class planType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine2.planType>
  {
  }

  public abstract class pOSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOLine2.pOSource>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.discAmt>
  {
  }

  public abstract class curyExtPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.curyExtPrice>
  {
  }

  public abstract class extPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  SOLine2.extPrice>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOLine2.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOLine2.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOLine2.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOLine2.Tstamp>
  {
  }
}
