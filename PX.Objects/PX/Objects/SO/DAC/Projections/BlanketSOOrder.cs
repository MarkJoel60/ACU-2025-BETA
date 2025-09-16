// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Projections.BlanketSOOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Projections;

/// <exclude />
[PXCacheName("Blanket Sales Order")]
[PXProjection(typeof (Select<PX.Objects.SO.SOOrder, Where<PX.Objects.SO.SOOrder.behavior, Equal<SOBehavior.bL>>>), Persistent = true)]
public class BlanketSOOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOOrder.orderType))]
  [PXDefault]
  public virtual 
  #nullable disable
  string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PX.Objects.SO.SOOrder.orderNbr))]
  [PXDefault]
  public virtual string OrderNbr { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOOrder.hold))]
  [PXDefault]
  public virtual bool? Hold { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOOrder.approved))]
  [PXDefault]
  public virtual bool? Approved { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOOrder.completed))]
  [PXDefault]
  public virtual bool? Completed { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL", BqlField = typeof (PX.Objects.SO.SOOrder.curyID))]
  [PXDefault]
  public virtual string CuryID { get; set; }

  [PXDBLong(BqlField = typeof (PX.Objects.SO.SOOrder.curyInfoID))]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOOrder.orderDate))]
  [PXDefault]
  public virtual DateTime? OrderDate { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (PX.Objects.SO.SOOrder.taxCalcMode))]
  [PXDefault]
  public virtual string TaxCalcMode { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOOrder.expireDate))]
  public virtual DateTime? ExpireDate { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOOrder.isExpired))]
  [PXDefault]
  public virtual bool? IsExpired { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.SO.SOOrder.qtyOnOrders))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QtyOnOrders { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.SO.SOOrder.blanketOpenQty))]
  [PXDefault]
  public virtual Decimal? BlanketOpenQty { get; set; }

  [PXDBCurrency(typeof (BlanketSOOrder.curyInfoID), typeof (BlanketSOOrder.openOrderTotal), BqlField = typeof (PX.Objects.SO.SOOrder.curyOpenOrderTotal))]
  [PXDefault]
  public virtual Decimal? CuryOpenOrderTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.SO.SOOrder.openOrderTotal))]
  [PXDefault]
  public virtual Decimal? OpenOrderTotal { get; set; }

  [PXDBCurrency(typeof (BlanketSOOrder.curyInfoID), typeof (BlanketSOOrder.openLineTotal), BqlField = typeof (PX.Objects.SO.SOOrder.curyOpenLineTotal))]
  [PXDefault]
  public virtual Decimal? CuryOpenLineTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.SO.SOOrder.openLineTotal))]
  [PXDefault]
  public virtual Decimal? OpenLineTotal { get; set; }

  [PXDBCurrency(typeof (BlanketSOOrder.curyInfoID), typeof (BlanketSOOrder.openTaxTotal), BqlField = typeof (PX.Objects.SO.SOOrder.curyOpenTaxTotal))]
  [PXDefault]
  public virtual Decimal? CuryOpenTaxTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.SO.SOOrder.openTaxTotal))]
  [PXDefault]
  public virtual Decimal? OpenTaxTotal { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.SO.SOOrder.openOrderQty))]
  [PXDefault]
  public virtual Decimal? OpenOrderQty { get; set; }

  [PXDBQuantity(BqlField = typeof (PX.Objects.SO.SOOrder.unbilledOrderQty))]
  [PXDefault]
  public virtual Decimal? UnbilledOrderQty { get; set; }

  [PXDBCurrency(typeof (BlanketSOOrder.curyInfoID), typeof (BlanketSOOrder.unbilledOrderTotal), BqlField = typeof (PX.Objects.SO.SOOrder.curyUnbilledOrderTotal))]
  [PXDefault]
  public virtual Decimal? CuryUnbilledOrderTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.SO.SOOrder.unbilledOrderTotal))]
  [PXDefault]
  public virtual Decimal? UnbilledOrderTotal { get; set; }

  [PXDBCurrency(typeof (BlanketSOOrder.curyInfoID), typeof (BlanketSOOrder.unbilledLineTotal), BqlField = typeof (PX.Objects.SO.SOOrder.curyUnbilledLineTotal))]
  [PXDefault]
  public virtual Decimal? CuryUnbilledLineTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.SO.SOOrder.unbilledLineTotal))]
  [PXDefault]
  public virtual Decimal? UnbilledLineTotal { get; set; }

  [PXDBCurrency(typeof (BlanketSOOrder.curyInfoID), typeof (BlanketSOOrder.unbilledTaxTotal), BqlField = typeof (PX.Objects.SO.SOOrder.curyUnbilledTaxTotal))]
  [PXDefault]
  public virtual Decimal? CuryUnbilledTaxTotal { get; set; }

  [PXDBDecimal(4, BqlField = typeof (PX.Objects.SO.SOOrder.unbilledTaxTotal))]
  [PXDefault]
  public virtual Decimal? UnbilledTaxTotal { get; set; }

  [PXDBDate(BqlField = typeof (PX.Objects.SO.SOOrder.minSchedOrderDate))]
  public virtual DateTime? MinSchedOrderDate { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOOrder.openLineCntr))]
  [PXDefault]
  public virtual int? OpenLineCntr { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOOrder.openSiteCntr))]
  public virtual int? OpenSiteCntr { get; set; }

  [PXInt]
  [PXDBCalced(typeof (PX.Objects.SO.SOOrder.openLineCntr), typeof (int))]
  [PXDefault]
  public virtual int? OrigOpenLineCntr { get; set; }

  [PXDBCurrency(typeof (BlanketSOOrder.curyInfoID), typeof (BlanketSOOrder.unreleasedPaymentAmt), BqlField = typeof (PX.Objects.SO.SOOrder.curyUnreleasedPaymentAmt))]
  [PXDefault]
  public virtual Decimal? CuryUnreleasedPaymentAmt { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (PX.Objects.SO.SOOrder.unreleasedPaymentAmt))]
  [PXDefault]
  public virtual Decimal? UnreleasedPaymentAmt { get; set; }

  [PXDBCurrency(typeof (BlanketSOOrder.curyInfoID), typeof (BlanketSOOrder.cCAuthorizedAmt), BqlField = typeof (PX.Objects.SO.SOOrder.curyCCAuthorizedAmt))]
  [PXDefault]
  public virtual Decimal? CuryCCAuthorizedAmt { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (PX.Objects.SO.SOOrder.cCAuthorizedAmt))]
  [PXDefault]
  public virtual Decimal? CCAuthorizedAmt { get; set; }

  [PXDBCurrency(typeof (BlanketSOOrder.curyInfoID), typeof (BlanketSOOrder.paidAmt), BqlField = typeof (PX.Objects.SO.SOOrder.curyPaidAmt))]
  [PXDefault]
  public virtual Decimal? CuryPaidAmt { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (PX.Objects.SO.SOOrder.paidAmt))]
  [PXDefault]
  public virtual Decimal? PaidAmt { get; set; }

  [PXDBCurrency(typeof (BlanketSOOrder.curyInfoID), typeof (BlanketSOOrder.paymentTotal), BqlField = typeof (PX.Objects.SO.SOOrder.curyPaymentTotal))]
  [PXDefault]
  public virtual Decimal? CuryPaymentTotal { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.SO.SOOrder.isOpenTaxValid))]
  [PXDefault(false)]
  public virtual bool? IsOpenTaxValid { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (PX.Objects.SO.SOOrder.paymentTotal))]
  [PXDefault]
  public virtual Decimal? PaymentTotal { get; set; }

  [PXDBCurrency(typeof (BlanketSOOrder.curyInfoID), typeof (BlanketSOOrder.paymentOverall), BqlField = typeof (PX.Objects.SO.SOOrder.curyPaymentOverall))]
  [PXDefault]
  public virtual Decimal? CuryPaymentOverall { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (PX.Objects.SO.SOOrder.paymentOverall))]
  [PXDefault]
  public virtual Decimal? PaymentOverall { get; set; }

  [PXDBCurrency(typeof (BlanketSOOrder.curyInfoID), typeof (BlanketSOOrder.transferredToChildrenPaymentTotal), BqlField = typeof (PX.Objects.SO.SOOrder.curyTransferredToChildrenPaymentTotal))]
  [PXDefault]
  public virtual Decimal? CuryTransferredToChildrenPaymentTotal { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (BlanketSOOrder.transferredToChildrenPaymentTotal))]
  [PXDefault]
  public virtual Decimal? TransferredToChildrenPaymentTotal { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOOrder.shipmentCntr))]
  [PXDefault]
  public virtual int? ShipmentCntr { get; set; }

  [PXBool]
  public virtual bool? ShipmentCntrUpdated { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.SO.SOOrder.childLineCntr))]
  [PXDefault]
  public virtual int? ChildLineCntr { get; set; }

  [PXDBGuid(false, BqlField = typeof (PX.Objects.SO.SOOrder.noteID))]
  [PXDefault]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (PX.Objects.SO.SOOrder.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (PX.Objects.SO.SOOrder.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (PX.Objects.SO.SOOrder.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<BlanketSOOrder>.By<BlanketSOOrder.orderType, BlanketSOOrder.orderNbr>
  {
    public static BlanketSOOrder Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      PKFindOptions options = 0)
    {
      return (BlanketSOOrder) PrimaryKeyOf<BlanketSOOrder>.By<BlanketSOOrder.orderType, BlanketSOOrder.orderNbr>.FindBy(graph, (object) orderType, (object) orderNbr, options);
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOOrder.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOOrder.orderNbr>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOOrder.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOOrder.approved>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOOrder.completed>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOOrder.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  BlanketSOOrder.curyInfoID>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  BlanketSOOrder.orderDate>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOOrder.taxCalcMode>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  BlanketSOOrder.expireDate>
  {
  }

  public abstract class isExpired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOOrder.isExpired>
  {
  }

  public abstract class qtyOnOrders : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOOrder.qtyOnOrders>
  {
  }

  public abstract class blanketOpenQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.blanketOpenQty>
  {
  }

  public abstract class curyOpenOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.curyOpenOrderTotal>
  {
  }

  public abstract class openOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.openOrderTotal>
  {
  }

  public abstract class curyOpenLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.curyOpenLineTotal>
  {
  }

  public abstract class openLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.openLineTotal>
  {
  }

  public abstract class curyOpenTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.curyOpenTaxTotal>
  {
  }

  public abstract class openTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.openTaxTotal>
  {
  }

  public abstract class openOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.openOrderQty>
  {
  }

  public abstract class unbilledOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.unbilledOrderQty>
  {
  }

  public abstract class curyUnbilledOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.curyUnbilledOrderTotal>
  {
  }

  public abstract class unbilledOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.unbilledOrderTotal>
  {
  }

  public abstract class curyUnbilledLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.curyUnbilledLineTotal>
  {
  }

  public abstract class unbilledLineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.unbilledLineTotal>
  {
  }

  public abstract class curyUnbilledTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.curyUnbilledTaxTotal>
  {
  }

  public abstract class unbilledTaxTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.unbilledTaxTotal>
  {
  }

  public abstract class minSchedOrderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSOOrder.minSchedOrderDate>
  {
  }

  public abstract class openLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOOrder.openLineCntr>
  {
  }

  public abstract class openSiteCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOOrder.openSiteCntr>
  {
  }

  public abstract class origOpenLineCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BlanketSOOrder.origOpenLineCntr>
  {
  }

  public abstract class curyUnreleasedPaymentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.curyUnreleasedPaymentAmt>
  {
  }

  public abstract class unreleasedPaymentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.unreleasedPaymentAmt>
  {
  }

  public abstract class curyCCAuthorizedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.curyCCAuthorizedAmt>
  {
  }

  public abstract class cCAuthorizedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.cCAuthorizedAmt>
  {
  }

  public abstract class curyPaidAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOOrder.curyPaidAmt>
  {
  }

  public abstract class paidAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOOrder.paidAmt>
  {
  }

  public abstract class curyPaymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.curyPaymentTotal>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.DAC.Projections.BlanketSOOrder.IsOpenTaxValid" />
  public abstract class isOpenTaxValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOOrder.isOpenTaxValid>
  {
  }

  public abstract class paymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.paymentTotal>
  {
  }

  public abstract class curyPaymentOverall : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.curyPaymentOverall>
  {
  }

  public abstract class paymentOverall : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.paymentOverall>
  {
  }

  public abstract class curyTransferredToChildrenPaymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.curyTransferredToChildrenPaymentTotal>
  {
  }

  public abstract class transferredToChildrenPaymentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOOrder.transferredToChildrenPaymentTotal>
  {
  }

  public abstract class shipmentCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOOrder.shipmentCntr>
  {
  }

  public abstract class shipmentCntrUpdated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BlanketSOOrder.shipmentCntrUpdated>
  {
  }

  public abstract class childLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOOrder.childLineCntr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BlanketSOOrder.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  BlanketSOOrder.Tstamp>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    BlanketSOOrder.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOOrder.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSOOrder.lastModifiedDateTime>
  {
  }
}
