// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Projections.BlanketSOLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Projections;

/// <exclude />
[PXCacheName("Blanket SO Line")]
[PXProjection(typeof (Select<PX.Objects.SO.SOLine, Where<PX.Objects.SO.SOLine.behavior, Equal<SOBehavior.bL>>>), Persistent = true)]
public class BlanketSOLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SiteID;

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.branchID))]
  [PXDefault]
  public virtual int? BranchID { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.orderType))]
  [PXDefault]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.SO.SOLine.orderNbr))]
  [PXDefault]
  [PXParent(typeof (BlanketSOLine.FK.BlanketOrder))]
  public virtual string OrderNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (PX.Objects.SO.SOLine.lineNbr))]
  [PXDefault]
  public virtual int? LineNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.behavior))]
  [PXDefault]
  public virtual string Behavior { get; set; }

  [PXDBString(1, IsFixed = true, InputMask = ">a", BqlField = typeof (PX.Objects.SO.SOLine.operation))]
  [PXDefault]
  public virtual string Operation { get; set; }

  [PXDBShort(BqlField = typeof (PX.Objects.SO.SOLine.lineSign))]
  [PXDefault]
  public virtual short? LineSign { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.isStockItem))]
  public virtual bool? IsStockItem { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.subItemID))]
  [PXDefault]
  public virtual int? SubItemID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.siteID))]
  [PXParent(typeof (Select<BlanketSOOrderSite, Where<BlanketSOOrderSite.orderType, Equal<Current<BlanketSOLine.orderType>>, And<BlanketSOOrderSite.orderNbr, Equal<Current<BlanketSOLine.orderNbr>>, And<BlanketSOOrderSite.siteID, Equal<Current2<BlanketSOLine.siteID>>>>>>), LeaveChildren = true)]
  [PXUnboundFormula(typeof (IIf<Where<BqlOperand<BlanketSOLine.openLine, IBqlBool>.IsEqual<True>>, int1, int0>), typeof (SumCalc<BlanketSOOrderSite.openLineCntr>), ValidateAggregateCalculation = true)]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.tranDesc))]
  public virtual string TranDesc { get; set; }

  [INUnit(typeof (BlanketSOLine.inventoryID), BqlField = typeof (PX.Objects.SO.SOLine.uOM))]
  [PXDefault]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (BlanketSOLine.uOM), typeof (BlanketSOLine.baseOrderQty), BqlField = typeof (PX.Objects.SO.SOLine.orderQty))]
  [PXDefault]
  public virtual Decimal? OrderQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.SO.SOLine.baseOrderQty))]
  [PXDefault]
  public virtual Decimal? BaseOrderQty { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOLine.requestDate))]
  [PXDefault]
  public virtual DateTime? RequestDate { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.taxCategoryID))]
  public virtual string TaxCategoryID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.taskID))]
  public virtual int? TaskID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.shipComplete))]
  [PXDefault]
  public virtual string ShipComplete { get; set; }

  [PXDBLong(BqlField = typeof (PX.Objects.SO.SOLine.curyInfoID))]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (BlanketSOLine.curyInfoID), typeof (BlanketSOLine.unitPrice), BqlField = typeof (PX.Objects.SO.SOLine.curyUnitPrice))]
  [PXDefault]
  public virtual Decimal? CuryUnitPrice { get; set; }

  [PXDBPriceCost(BqlField = typeof (PX.Objects.SO.SOLine.unitPrice))]
  [PXDefault]
  public virtual Decimal? UnitPrice { get; set; }

  [PXDBCurrency(typeof (BlanketSOLine.curyInfoID), typeof (BlanketSOLine.extPrice), BqlField = typeof (PX.Objects.SO.SOLine.curyExtPrice))]
  [PXDefault]
  public virtual Decimal? CuryExtPrice { get; set; }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.SO.SOLine.extPrice))]
  [PXDefault]
  public virtual Decimal? ExtPrice { get; set; }

  [PXDBDecimal(6, MinValue = -100.0, MaxValue = 100.0, BqlField = typeof (PX.Objects.SO.SOLine.discPct))]
  [PXDefault]
  public virtual Decimal? DiscPct { get; set; }

  [PXDBCurrency(typeof (BlanketSOLine.curyInfoID), typeof (BlanketSOLine.discAmt), BqlField = typeof (PX.Objects.SO.SOLine.curyDiscAmt))]
  [PXDefault]
  public virtual Decimal? CuryDiscAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.SO.SOLine.discAmt))]
  [PXDefault]
  public virtual Decimal? DiscAmt { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.isFree))]
  [PXDefault]
  public virtual bool? IsFree { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.manualDisc))]
  [PXDefault(false)]
  public virtual bool? ManualDisc { get; set; }

  /// <summary>
  /// Indicates (if selected) that the automatic line discounts are not applied to this line.
  /// </summary>
  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.skipLineDiscounts))]
  [PXDefault(false)]
  public virtual bool? SkipLineDiscounts { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.automaticDiscountsDisabled))]
  [PXDefault(false)]
  public virtual bool? AutomaticDiscountsDisabled { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.discountID))]
  public virtual string DiscountID { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOLine.lineType))]
  [PXDefault]
  public virtual string LineType { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.completed))]
  [PXDefault]
  public virtual bool? Completed { get; set; }

  [PXDBCurrency(typeof (BlanketSOLine.curyInfoID), typeof (BlanketSOLine.lineAmt), BqlField = typeof (PX.Objects.SO.SOLine.curyLineAmt))]
  [PXDefault]
  public virtual Decimal? CuryLineAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.SO.SOLine.lineAmt))]
  [PXDefault]
  public virtual Decimal? LineAmt { get; set; }

  [PXDBDecimal(18, BqlField = typeof (PX.Objects.SO.SOLine.groupDiscountRate))]
  [PXDefault]
  public virtual Decimal? GroupDiscountRate { get; set; }

  [PXDBDecimal(18, BqlField = typeof (PX.Objects.SO.SOLine.documentDiscountRate))]
  [PXDefault]
  public virtual Decimal? DocumentDiscountRate { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.salesPersonID))]
  public virtual int? SalesPersonID { get; set; }

  /// <summary>
  /// The account associated with the sale of the line item.
  /// </summary>
  [PXDefault]
  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.salesAcctID))]
  public virtual int? SalesAcctID { get; set; }

  /// <summary>
  /// The subaccount associated with the sale of the line item.
  /// </summary>
  [PXDefault]
  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.salesSubID))]
  public virtual int? SalesSubID { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.pOCreate))]
  [PXDefault]
  public virtual bool? POCreate { get; set; }

  [PXDBString(BqlField = typeof (PX.Objects.SO.SOLine.pOSource))]
  [PXDefault]
  public virtual string POSource { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.pOCreated))]
  [PXDefault]
  public virtual bool? POCreated { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.vendorID))]
  public virtual int? VendorID { get; set; }

  [PXDBQuantity(typeof (BlanketSOLine.uOM), typeof (BlanketSOLine.baseQtyOnOrders), BqlField = typeof (PX.Objects.SO.SOLine.qtyOnOrders))]
  [PXDefault]
  [PXUnboundFormula(typeof (Switch<Case<Where<BlanketSOLine.lineType, NotEqual<SOLineType.miscCharge>>, BlanketSOLine.qtyOnOrders>, decimal0>), typeof (SumCalc<BlanketSOOrder.qtyOnOrders>))]
  public virtual Decimal? QtyOnOrders { get; set; }

  [PXDBDecimal(6, MinValue = 0.0, BqlField = typeof (PX.Objects.SO.SOLine.baseQtyOnOrders))]
  [PXDefault]
  public virtual Decimal? BaseQtyOnOrders { get; set; }

  [PXDBString(40, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.customerOrderNbr))]
  public virtual string CustomerOrderNbr { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOLine.schedOrderDate))]
  public virtual DateTime? SchedOrderDate { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOLine.schedShipDate))]
  public virtual DateTime? SchedShipDate { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.taxZoneID))]
  public virtual string TaxZoneID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.customerID))]
  [PXDefault]
  public virtual int? CustomerID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.customerLocationID))]
  [PXDefault]
  public virtual int? CustomerLocationID { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.shipVia))]
  public virtual string ShipVia { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.fOBPoint))]
  public virtual string FOBPoint { get; set; }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.shipTermsID))]
  public virtual string ShipTermsID { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.SO.SOLine.shipZoneID))]
  public virtual string ShipZoneID { get; set; }

  [PXQuantity]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.SO.SOLine.lineType, NotEqual<SOLineType.miscCharge>, And<PX.Objects.SO.SOLine.completed, Equal<False>>>, Sub<PX.Objects.SO.SOLine.orderQty, PX.Objects.SO.SOLine.qtyOnOrders>>, decimal0>), typeof (Decimal))]
  [PXFormula(typeof (Switch<Case<Where<BlanketSOLine.lineType, NotEqual<SOLineType.miscCharge>, And<BlanketSOLine.completed, Equal<False>>>, Sub<BlanketSOLine.orderQty, BlanketSOLine.qtyOnOrders>>, decimal0>), typeof (SumCalc<BlanketSOOrder.blanketOpenQty>))]
  [PXDefault]
  public virtual Decimal? BlanketOpenQty { get; set; }

  [PXFormula(null, typeof (SumCalc<BlanketSOOrder.childLineCntr>))]
  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.childLineCntr))]
  [PXDefault]
  public virtual int? ChildLineCntr { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOLine.openChildLineCntr))]
  [PXDefault]
  public virtual int? OpenChildLineCntr { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOLine.openLine))]
  [PXFormula(typeof (Switch<Case<Where<BlanketSOLine.completed, NotEqual<True>, And<BlanketSOLine.orderQty, Greater<decimal0>>>, True>, False>))]
  [DirtyFormula(typeof (Switch<Case<Where<BlanketSOLine.openLine, Equal<True>, And<Where<BlanketSOLine.isFree, NotEqual<True>, Or<BlanketSOLine.manualDisc, Equal<True>>>>>, int1>, int0>), typeof (SumCalc<BlanketSOOrder.openLineCntr>), true, SkipZeroUpdates = false)]
  public virtual bool? OpenLine { get; set; }

  [PXDBQuantity(typeof (BlanketSOLine.uOM), typeof (BlanketSOLine.baseShippedQty), BqlField = typeof (PX.Objects.SO.SOLine.shippedQty))]
  [PXDefault]
  public virtual Decimal? ShippedQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.SO.SOLine.baseShippedQty))]
  [PXDefault]
  public virtual Decimal? BaseShippedQty { get; set; }

  [PXDBCalced(typeof (Sub<PX.Objects.SO.SOLine.orderQty, PX.Objects.SO.SOLine.openQty>), typeof (Decimal))]
  [PXQuantity(typeof (BlanketSOLine.uOM), typeof (BlanketSOLine.baseClosedQty))]
  [PXDefault]
  public virtual Decimal? ClosedQty { get; set; }

  [PXDBCalced(typeof (Sub<PX.Objects.SO.SOLine.baseOrderQty, PX.Objects.SO.SOLine.baseOpenQty>), typeof (Decimal))]
  [PXQuantity]
  [PXDefault]
  public virtual Decimal? BaseClosedQty { get; set; }

  [PXDBQuantity(typeof (BlanketSOLine.uOM), typeof (BlanketSOLine.baseOpenQty), BqlField = typeof (PX.Objects.SO.SOLine.openQty))]
  [PXFormula(typeof (Switch<Case<Where<BlanketSOLine.completed, NotEqual<True>>, Sub<BlanketSOLine.orderQty, BlanketSOLine.closedQty>>, decimal0>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<BlanketSOLine.lineType, NotEqual<SOLineType.miscCharge>>, BqlOperand<BlanketSOLine.openQty, IBqlDecimal>.Multiply<BlanketSOLine.lineSign>>, decimal0>), typeof (SumCalc<BlanketSOOrder.openOrderQty>))]
  [PXDefault]
  public virtual Decimal? OpenQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.SO.SOLine.baseOpenQty))]
  [PXDefault]
  public virtual Decimal? BaseOpenQty { get; set; }

  [PXDBCurrency(typeof (BlanketSOLine.curyInfoID), typeof (BlanketSOLine.openAmt), BqlField = typeof (PX.Objects.SO.SOLine.curyOpenAmt))]
  [PXFormula(typeof (BqlFunction<ArrayedSwitch<TypeArrayOf<IBqlCase>.Append<TypeArrayOf<IBqlCase>.Empty, Case<Where<BqlOperand<BlanketSOLine.lineType, IBqlString>.IsNotEqual<SOLineType.miscCharge>>, BlanketSOLine.openQty>>, decimal0>, IBqlDecimal>.Multiply<BqlOperand<BlanketSOLine.curyLineAmt, IBqlDecimal>.Divide<BqlOperand<BlanketSOLine.orderQty, IBqlDecimal>.When<BqlOperand<BlanketSOLine.orderQty, IBqlDecimal>.IsNotEqual<decimal0>>.Else<decimal1>>>))]
  [PXDefault]
  public virtual Decimal? CuryOpenAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.SO.SOLine.openAmt))]
  [PXDefault]
  public virtual Decimal? OpenAmt { get; set; }

  [PXDecimal]
  [PXDBCalced(typeof (PX.Objects.SO.SOLine.curyOpenAmt), typeof (Decimal))]
  [PXDefault]
  public virtual Decimal? OrigCuryOpenAmt { get; set; }

  [PXDBDecimal(6, BqlField = typeof (PX.Objects.SO.SOLine.billedQty))]
  [PXDefault]
  public virtual Decimal? BilledQty { get; set; }

  [PXDBBaseQuantity(typeof (BlanketSOLine.uOM), typeof (BlanketSOLine.billedQty), BqlField = typeof (PX.Objects.SO.SOLine.baseBilledQty))]
  [PXDefault]
  public virtual Decimal? BaseBilledQty { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.SO.SOLine.unbilledQty))]
  [PXDefault]
  public virtual Decimal? UnbilledQty { get; set; }

  [PXDBBaseQuantity(typeof (BlanketSOLine.uOM), typeof (BlanketSOLine.unbilledQty), BqlField = typeof (PX.Objects.SO.SOLine.baseUnbilledQty))]
  [PXDefault]
  public virtual Decimal? BaseUnbilledQty { get; set; }

  [PXDBCurrency(typeof (BlanketSOLine.curyInfoID), typeof (BlanketSOLine.billedAmt), BqlField = typeof (PX.Objects.SO.SOLine.curyBilledAmt))]
  [PXFormula(typeof (Mult<Mult<BlanketSOLine.billedQty, BlanketSOLine.curyUnitPrice>, Sub<decimal1, Div<BlanketSOLine.discPct, decimal100>>>))]
  [PXDefault]
  public virtual Decimal? CuryBilledAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.SO.SOLine.billedAmt))]
  [PXDefault]
  public virtual Decimal? BilledAmt { get; set; }

  [PXDBCurrency(typeof (BlanketSOLine.curyInfoID), typeof (BlanketSOLine.unbilledAmt), BqlField = typeof (PX.Objects.SO.SOLine.curyUnbilledAmt))]
  [PXFormula(typeof (Mult<Mult<BlanketSOLine.unbilledQty, BlanketSOLine.curyUnitPrice>, Sub<decimal1, Div<BlanketSOLine.discPct, decimal100>>>))]
  [PXDefault]
  public virtual Decimal? CuryUnbilledAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.SO.SOLine.unbilledAmt))]
  [PXDefault]
  public virtual Decimal? UnbilledAmt { get; set; }

  [PXNote(BqlField = typeof (PX.Objects.SO.SOLine.noteID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (PX.Objects.SO.SOLine.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PX.Objects.SO.SOLine.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (PX.Objects.SO.SOLine.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<BlanketSOLine>.By<BlanketSOLine.orderType, BlanketSOLine.orderNbr, BlanketSOLine.lineNbr>
  {
    public static BlanketSOLine Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (BlanketSOLine) PrimaryKeyOf<BlanketSOLine>.By<BlanketSOLine.orderType, BlanketSOLine.orderNbr, BlanketSOLine.lineNbr>.FindBy(graph, (object) orderType, (object) orderNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class BlanketOrder : 
      PrimaryKeyOf<BlanketSOOrder>.By<BlanketSOOrder.orderType, BlanketSOOrder.orderNbr>.ForeignKeyOf<BlanketSOLine>.By<BlanketSOLine.orderType, BlanketSOLine.orderNbr>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.branchID>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.orderNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.lineNbr>
  {
  }

  public abstract class behavior : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.behavior>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.operation>
  {
  }

  public abstract class lineSign : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  BlanketSOLine.lineSign>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOLine.isStockItem>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.siteID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.tranDesc>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.uOM>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.orderQty>
  {
  }

  public abstract class baseOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.baseOrderQty>
  {
  }

  public abstract class requestDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSOLine.requestDate>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOLine.taxCategoryID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.costCodeID>
  {
  }

  public abstract class shipComplete : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.shipComplete>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  BlanketSOLine.curyInfoID>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.curyUnitPrice>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.unitPrice>
  {
  }

  public abstract class curyExtPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.curyExtPrice>
  {
  }

  public abstract class extPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.extPrice>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.discPct>
  {
  }

  public abstract class curyDiscAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.discAmt>
  {
  }

  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOLine.isFree>
  {
  }

  public abstract class manualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOLine.manualDisc>
  {
  }

  public abstract class skipLineDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BlanketSOLine.skipLineDiscounts>
  {
  }

  public abstract class automaticDiscountsDisabled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BlanketSOLine.automaticDiscountsDisabled>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.discountID>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.lineType>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOLine.completed>
  {
  }

  public abstract class curyLineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.curyLineAmt>
  {
  }

  public abstract class lineAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.lineAmt>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.documentDiscountRate>
  {
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.salesPersonID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.DAC.Projections.BlanketSOLine.SalesAcctID" />
  public abstract class salesAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.salesAcctID>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.DAC.Projections.BlanketSOLine.SalesSubID" />
  public abstract class salesSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.salesSubID>
  {
  }

  public abstract class pOCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOLine.pOCreate>
  {
  }

  public abstract class pOSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.pOSource>
  {
  }

  public abstract class pOCreated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOLine.pOCreated>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.vendorID>
  {
  }

  public abstract class qtyOnOrders : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.qtyOnOrders>
  {
  }

  public abstract class baseQtyOnOrders : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.baseQtyOnOrders>
  {
  }

  public abstract class customerOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOLine.customerOrderNbr>
  {
  }

  public abstract class schedOrderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSOLine.schedOrderDate>
  {
  }

  public abstract class schedShipDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSOLine.schedShipDate>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.taxZoneID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.customerID>
  {
  }

  public abstract class customerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BlanketSOLine.customerLocationID>
  {
  }

  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.shipVia>
  {
  }

  public abstract class fOBPoint : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.fOBPoint>
  {
  }

  public abstract class shipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.shipTermsID>
  {
  }

  public abstract class shipZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOLine.shipZoneID>
  {
  }

  public abstract class blanketOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.blanketOpenQty>
  {
  }

  public abstract class childLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOLine.childLineCntr>
  {
  }

  public abstract class openChildLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BlanketSOLine.openChildLineCntr>
  {
  }

  public abstract class openLine : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOLine.openLine>
  {
  }

  public abstract class shippedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.shippedQty>
  {
  }

  public abstract class baseShippedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.baseShippedQty>
  {
  }

  public abstract class closedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.closedQty>
  {
  }

  public abstract class baseClosedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.baseClosedQty>
  {
  }

  public abstract class openQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.openQty>
  {
  }

  public abstract class baseOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.baseOpenQty>
  {
  }

  public abstract class curyOpenAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.curyOpenAmt>
  {
  }

  public abstract class openAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.openAmt>
  {
  }

  public abstract class origCuryOpenAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.origCuryOpenAmt>
  {
  }

  public abstract class billedQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.billedQty>
  {
  }

  public abstract class baseBilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.baseBilledQty>
  {
  }

  public abstract class unbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.unbilledQty>
  {
  }

  public abstract class baseUnbilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.baseUnbilledQty>
  {
  }

  public abstract class curyBilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.curyBilledAmt>
  {
  }

  public abstract class billedAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.billedAmt>
  {
  }

  public abstract class curyUnbilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOLine.curyUnbilledAmt>
  {
  }

  public abstract class unbilledAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOLine.unbilledAmt>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BlanketSOLine.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  BlanketSOLine.Tstamp>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    BlanketSOLine.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOLine.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSOLine.lastModifiedDateTime>
  {
  }
}
