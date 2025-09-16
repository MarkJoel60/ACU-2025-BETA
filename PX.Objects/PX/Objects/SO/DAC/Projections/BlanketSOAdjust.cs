// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Projections.BlanketSOAdjust
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Projections;

[PXCacheName("Blanket SO Adjustment")]
[PXProjection(typeof (Select<SOAdjust>), Persistent = true)]
public class BlanketSOAdjust : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const int AdjgRefNbrLength = 15;

  [PXDBIdentity(IsKey = true, BqlField = typeof (SOAdjust.recordID))]
  public virtual int? RecordID { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, InputMask = "", BqlField = typeof (SOAdjust.adjgDocType))]
  [ARPaymentType.List]
  [PXDefault]
  public virtual 
  #nullable disable
  string AdjgDocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, BqlField = typeof (SOAdjust.adjgRefNbr))]
  [PXParent(typeof (Select<ARPaymentTotals, Where<ARPaymentTotals.docType, Equal<Current<BlanketSOAdjust.adjgDocType>>, And<ARPaymentTotals.refNbr, Equal<Current<BlanketSOAdjust.adjgRefNbr>>>>>), ParentCreate = true)]
  public virtual string AdjgRefNbr { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (SOAdjust.adjdOrderType))]
  public virtual string AdjdOrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (SOAdjust.adjdOrderNbr))]
  [PXDefault]
  [PXParent(typeof (BlanketSOAdjust.FK.BlanketOrder))]
  public virtual string AdjdOrderNbr { get; set; }

  [PXDBDecimal(4, BqlField = typeof (SOAdjust.curyAdjgAmt))]
  [PXFormula(typeof (Maximum<Sub<BlanketSOAdjust.curyAdjgOrigBlanketAmt, BlanketSOAdjust.curyAdjgTransferredToChildrenAmt>, decimal0>))]
  public virtual Decimal? CuryAdjgAmt { get; set; }

  [PXFormula(typeof (Maximum<Sub<BlanketSOAdjust.adjOrigBlanketAmt, BlanketSOAdjust.adjTransferredToChildrenAmt>, decimal0>))]
  [PXDBDecimal(4, BqlField = typeof (SOAdjust.adjAmt))]
  [PXDefault]
  public virtual Decimal? AdjAmt { get; set; }

  [PXFormula(typeof (Maximum<Sub<BlanketSOAdjust.curyAdjdOrigBlanketAmt, BlanketSOAdjust.curyAdjdTransferredToChildrenAmt>, decimal0>))]
  [PXDBDecimal(4, BqlField = typeof (SOAdjust.curyAdjdAmt))]
  [PXUnboundFormula(typeof (Switch<Case<Where<BlanketSOAdjust.voided, Equal<False>, And<BlanketSOAdjust.paymentReleased, Equal<False>, And<BlanketSOAdjust.isCCAuthorized, Equal<False>>>>, BlanketSOAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<BlanketSOOrder.curyUnreleasedPaymentAmt>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<BlanketSOAdjust.voided, Equal<False>, And<BlanketSOAdjust.paymentReleased, Equal<False>, And<BlanketSOAdjust.isCCAuthorized, Equal<True>>>>, BlanketSOAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<BlanketSOOrder.curyCCAuthorizedAmt>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<BlanketSOAdjust.voided, Equal<False>, And<BlanketSOAdjust.paymentReleased, Equal<True>>>, BlanketSOAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<BlanketSOOrder.curyPaidAmt>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<BlanketSOAdjust.voided, Equal<False>>, BlanketSOAdjust.curyAdjdAmt>, decimal0>), typeof (SumCalc<BlanketSOOrder.curyPaymentTotal>), ForceAggregateRecalculation = true)]
  [PXUnboundFormula(typeof (IIf<Where<BlanketSOAdjust.voided, Equal<False>, And<Where<BlanketSOAdjust.isCCPayment, Equal<False>, Or<BlanketSOAdjust.isCCAuthorized, Equal<True>, Or<BlanketSOAdjust.isCCCaptured, Equal<True>, Or<BlanketSOAdjust.paymentReleased, Equal<True>>>>>>>, Add<BlanketSOAdjust.curyAdjdAmt, BlanketSOAdjust.curyAdjdBilledAmt>, decimal0>), typeof (SumCalc<BlanketSOOrder.curyPaymentOverall>), ForceAggregateRecalculation = true)]
  [CopyChildLink(typeof (ARPaymentTotals.orderCntr), typeof (BlanketSOAdjust.curyAdjdAmt), new Type[] {typeof (BlanketSOAdjust.adjdOrderType), typeof (BlanketSOAdjust.adjdOrderNbr)}, new Type[] {typeof (ARPaymentTotals.adjdOrderType), typeof (ARPaymentTotals.adjdOrderNbr)})]
  [PXDefault]
  public virtual Decimal? CuryAdjdAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (SOAdjust.curyAdjdBilledAmt))]
  [PXDefault]
  public virtual Decimal? CuryAdjdBilledAmt { get; set; }

  [PXDBBool(BqlField = typeof (SOAdjust.isCCPayment))]
  public virtual bool? IsCCPayment { get; set; }

  [PXDBBool(BqlField = typeof (SOAdjust.paymentReleased))]
  public virtual bool? PaymentReleased { get; set; }

  [PXDBBool(BqlField = typeof (SOAdjust.isCCAuthorized))]
  public virtual bool? IsCCAuthorized { get; set; }

  [PXDBBool(BqlField = typeof (SOAdjust.isCCCaptured))]
  public virtual bool? IsCCCaptured { get; set; }

  [PXDBBool(BqlField = typeof (SOAdjust.voided))]
  public virtual bool? Voided { get; set; }

  [PXDBDecimal(4, BqlField = typeof (SOAdjust.curyAdjgTransferredToChildrenAmt))]
  [PXDefault]
  public virtual Decimal? CuryAdjgTransferredToChildrenAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (SOAdjust.adjTransferredToChildrenAmt))]
  [PXDefault]
  public virtual Decimal? AdjTransferredToChildrenAmt { get; set; }

  [PXDBDecimal(4, BqlField = typeof (SOAdjust.curyAdjdTransferredToChildrenAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transferred to Child Orders", Enabled = false)]
  [PXUnboundFormula(typeof (IIf<Where<BlanketSOAdjust.voided, Equal<False>>, BlanketSOAdjust.curyAdjdTransferredToChildrenAmt, decimal0>), typeof (SumCalc<BlanketSOOrder.curyTransferredToChildrenPaymentTotal>), ForceAggregateRecalculation = true)]
  public virtual Decimal? CuryAdjdTransferredToChildrenAmt { get; set; }

  [PXDBCalced(typeof (Add<BlanketSOAdjust.curyAdjdAmt, BlanketSOAdjust.curyAdjdTransferredToChildrenAmt>), typeof (Decimal), Persistent = true)]
  [PXDecimal(4)]
  public virtual Decimal? CuryAdjdOrigBlanketAmt { get; set; }

  [PXDBCalced(typeof (Add<BlanketSOAdjust.adjAmt, BlanketSOAdjust.adjTransferredToChildrenAmt>), typeof (Decimal), Persistent = true)]
  [PXDecimal(4)]
  public virtual Decimal? AdjOrigBlanketAmt { get; set; }

  [PXDBCalced(typeof (Add<BlanketSOAdjust.curyAdjgAmt, BlanketSOAdjust.curyAdjgTransferredToChildrenAmt>), typeof (Decimal), Persistent = true)]
  [PXDecimal(4)]
  public virtual Decimal? CuryAdjgOrigBlanketAmt { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<BlanketSOAdjust>.By<BlanketSOAdjust.recordID, BlanketSOAdjust.adjdOrderType, BlanketSOAdjust.adjdOrderNbr, BlanketSOAdjust.adjgDocType, BlanketSOAdjust.adjgRefNbr>
  {
    public static BlanketSOAdjust Find(
      PXGraph graph,
      int recordID,
      string adjdOrderType,
      string adjdOrderNbr,
      string adjgDocType,
      string adjgRefNbr,
      PKFindOptions options = 0)
    {
      return (BlanketSOAdjust) PrimaryKeyOf<BlanketSOAdjust>.By<BlanketSOAdjust.recordID, BlanketSOAdjust.adjdOrderType, BlanketSOAdjust.adjdOrderNbr, BlanketSOAdjust.adjgDocType, BlanketSOAdjust.adjgRefNbr>.FindBy(graph, (object) recordID, (object) adjdOrderType, (object) adjdOrderNbr, (object) adjgDocType, (object) adjgRefNbr, options);
    }
  }

  public static class FK
  {
    public class BlanketOrder : 
      PrimaryKeyOf<BlanketSOOrder>.By<BlanketSOOrder.orderType, BlanketSOOrder.orderNbr>.ForeignKeyOf<BlanketSOAdjust>.By<BlanketSOAdjust.adjdOrderType, BlanketSOAdjust.adjdOrderNbr>
    {
    }
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOAdjust.recordID>
  {
  }

  public abstract class adjgDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOAdjust.adjgDocType>
  {
  }

  public abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOAdjust.adjgRefNbr>
  {
  }

  public abstract class adjdOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOAdjust.adjdOrderType>
  {
  }

  public abstract class adjdOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOAdjust.adjdOrderNbr>
  {
  }

  public abstract class curyAdjgAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOAdjust.curyAdjgAmt>
  {
  }

  public abstract class adjAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BlanketSOAdjust.adjAmt>
  {
  }

  public abstract class curyAdjdAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOAdjust.curyAdjdAmt>
  {
  }

  public abstract class curyAdjdBilledAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOAdjust.curyAdjdBilledAmt>
  {
  }

  public abstract class isCCPayment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOAdjust.isCCPayment>
  {
  }

  public abstract class paymentReleased : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BlanketSOAdjust.paymentReleased>
  {
  }

  public abstract class isCCAuthorized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BlanketSOAdjust.isCCAuthorized>
  {
  }

  public abstract class isCCCaptured : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOAdjust.isCCCaptured>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BlanketSOAdjust.voided>
  {
  }

  public abstract class curyAdjgTransferredToChildrenAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOAdjust.curyAdjgTransferredToChildrenAmt>
  {
  }

  public abstract class adjTransferredToChildrenAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOAdjust.adjTransferredToChildrenAmt>
  {
  }

  public abstract class curyAdjdTransferredToChildrenAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOAdjust.curyAdjdTransferredToChildrenAmt>
  {
  }

  public abstract class curyAdjdOrigBlanketAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOAdjust.curyAdjdOrigBlanketAmt>
  {
  }

  public abstract class adjOrigBlanketAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOAdjust.adjOrigBlanketAmt>
  {
  }

  public abstract class curyAdjgOrigBlanketAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BlanketSOAdjust.curyAdjgOrigBlanketAmt>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  BlanketSOAdjust.Tstamp>
  {
  }
}
