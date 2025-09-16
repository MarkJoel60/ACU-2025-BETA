// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAdjustReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("Applications")]
[Serializable]
public class ARAdjustReport : ARAdjust
{
  [PXDecimal(4)]
  [PXDBCalced(typeof (Sub<Mult<Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.payment>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.prepayment>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.voidPayment>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO>>, decimal1, Case<Where<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO>>, decimal_1, Case<Where<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsIn<ARDocType.cashSale, ARDocType.cashReturn>>, decimal0>>>>>>>, Add<ARAdjust.adjAmt, Add<ARAdjust.adjDiscAmt, ARAdjust.adjWOAmt>>>, ARAdjust.rGOLAmt>), typeof (Decimal))]
  public virtual Decimal? LineTotalAdjusted { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Mult<Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.payment>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.prepayment>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.voidPayment>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO>>, decimal1, Case<Where<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO>>, decimal_1, Case<Where<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsIn<ARDocType.cashSale, ARDocType.cashReturn>>, decimal0>>>>>>>, Add<ARAdjust.curyAdjdAmt, Add<ARAdjust.curyAdjdDiscAmt, ARAdjust.curyAdjdWOAmt>>>), typeof (Decimal))]
  public virtual Decimal? CuryLineTotalAdjusted { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Mult<Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.payment>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.prepayment>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.voidPayment>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO>>, decimal1, Case<Where<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO>>, decimal_1, Case<Where<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsIn<ARDocType.cashSale, ARDocType.cashReturn>>, decimal0>>>>>>>, ARAdjust.adjAmt>), typeof (Decimal))]
  public virtual Decimal? LineTotalAdjusting { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Mult<Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.payment>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.prepayment>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust.adjgDocType, Equal<ARDocType.voidPayment>>>>>.And<BqlOperand<ARAdjust.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO>>, decimal1, Case<Where<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO>>, decimal_1, Case<Where<BqlOperand<ARAdjust.adjgDocType, IBqlString>.IsIn<ARDocType.cashSale, ARDocType.cashReturn>>, decimal0>>>>>>>, ARAdjust.curyAdjgAmt>), typeof (Decimal))]
  public virtual Decimal? CuryLineTotalAdjusting { get; set; }

  public new abstract class adjdDocType : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    ARAdjustReport.adjdDocType>
  {
  }

  public new abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjustReport.adjdRefNbr>
  {
  }

  public new abstract class adjgDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARAdjustReport.adjgDocDate>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARAdjustReport.released>
  {
  }

  public new abstract class adjgDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjustReport.adjgDocType>
  {
  }

  public new abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjustReport.adjgRefNbr>
  {
  }

  public new abstract class curyAdjdAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjustReport.curyAdjdAmt>
  {
  }

  public new abstract class curyAdjdDiscAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjustReport.curyAdjdDiscAmt>
  {
  }

  public new abstract class curyAdjdWOAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjustReport.curyAdjdWOAmt>
  {
  }

  public new abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjustReport.rGOLAmt>
  {
  }

  public new abstract class adjAmt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjustReport.adjAmt>
  {
  }

  public new abstract class adjDiscAmt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjustReport.adjDiscAmt>
  {
  }

  public new abstract class adjWOAmt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjustReport.adjWOAmt>
  {
  }

  public new abstract class curyAdjgAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARAdjustReport.curyAdjgAmt>
  {
  }

  public abstract class lineTotalAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjustReport.lineTotalAdjusted>
  {
  }

  public abstract class curyLineTotalAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjustReport.curyLineTotalAdjusted>
  {
  }

  public abstract class lineTotalAdjusting : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjustReport.lineTotalAdjusting>
  {
  }

  public abstract class curyLineTotalAdjusting : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjustReport.curyLineTotalAdjusting>
  {
  }
}
