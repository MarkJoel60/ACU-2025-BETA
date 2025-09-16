// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APAdjustReport
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
namespace PX.Objects.AP;

[PXHidden]
[Serializable]
public class APAdjustReport : APAdjust
{
  [PXDecimal(4)]
  [PXDBCalced(typeof (Sub<Mult<Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.check>>>>>.And<BqlOperand<APAdjustReport.adjdDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.voidCheck>>>>>.And<BqlOperand<APAdjustReport.adjdDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjdDocType, Equal<APDocType.prepayment>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.check>>>>, PX.Data.Or<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsEqual<APDocType.voidCheck>>>>.Or<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsEqual<APDocType.prepayment>>>>, decimal_1, Case<Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj>>, decimal1, Case<Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.prepayment>>, decimal_1, Case<Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.quickCheck, APDocType.voidQuickCheck>>, decimal0>>>>>>>, Add<APAdjustReport.adjAmt, Add<APAdjustReport.adjDiscAmt, APAdjustReport.adjWhTaxAmt>>>, APAdjustReport.rGOLAmt>), typeof (Decimal))]
  public virtual Decimal? LineTotalAdjusted { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Mult<Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.check>>>>>.And<BqlOperand<APAdjustReport.adjdDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.voidCheck>>>>>.And<BqlOperand<APAdjustReport.adjdDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjdDocType, Equal<APDocType.prepayment>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.check>>>>, PX.Data.Or<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsEqual<APDocType.voidCheck>>>>.Or<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsEqual<APDocType.prepayment>>>>, decimal_1, Case<Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj>>, decimal1, Case<Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.prepayment>>, decimal_1, Case<Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.quickCheck, APDocType.voidQuickCheck>>, decimal0>>>>>>>, Add<APAdjustReport.curyAdjdAmt, Add<APAdjustReport.curyAdjdDiscAmt, APAdjustReport.curyAdjdWhTaxAmt>>>), typeof (Decimal))]
  public virtual Decimal? CuryLineTotalAdjusted { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Mult<Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.check>>>>>.And<BqlOperand<APAdjustReport.adjdDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.voidCheck>>>>>.And<BqlOperand<APAdjustReport.adjdDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjdDocType, Equal<APDocType.prepayment>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.check>>>>, PX.Data.Or<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsEqual<APDocType.voidCheck>>>>.Or<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsEqual<APDocType.prepayment>>>>, decimal_1, Case<Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj>>, decimal1, Case<Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.prepayment>>, decimal_1, Case<Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.quickCheck, APDocType.voidQuickCheck>>, decimal0>>>>>>>, APAdjustReport.adjAmt>), typeof (Decimal))]
  public virtual Decimal? LineTotalAdjusting { get; set; }

  [PXDecimal(4)]
  [PXDBCalced(typeof (Mult<Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.check>>>>>.And<BqlOperand<APAdjustReport.adjdDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.voidCheck>>>>>.And<BqlOperand<APAdjustReport.adjdDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjdDocType, Equal<APDocType.prepayment>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.check>>>>, PX.Data.Or<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsEqual<APDocType.voidCheck>>>>.Or<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsEqual<APDocType.prepayment>>>>, decimal_1, Case<Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj>>, decimal1, Case<Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.prepayment>>, decimal_1, Case<Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.quickCheck, APDocType.voidQuickCheck>>, decimal0>>>>>>>, APAdjustReport.curyAdjgAmt>), typeof (Decimal))]
  public virtual Decimal? CuryLineTotalAdjusting { get; set; }

  public new abstract class adjdDocType : 
    BqlType<IBqlString, string>.Field<
    #nullable disable
    APAdjustReport.adjdDocType>
  {
  }

  public new abstract class adjdRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjustReport.adjdRefNbr>
  {
  }

  public new abstract class adjgDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APAdjustReport.adjgDocDate>
  {
  }

  public new abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APAdjustReport.released>
  {
  }

  public new abstract class adjgDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjustReport.adjgDocType>
  {
  }

  public new abstract class adjgRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjustReport.adjgRefNbr>
  {
  }

  public new abstract class curyAdjdAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjustReport.curyAdjdAmt>
  {
  }

  public new abstract class curyAdjdDiscAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjustReport.curyAdjdDiscAmt>
  {
  }

  public abstract class curyAdjdWOAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjustReport.curyAdjdWOAmt>
  {
  }

  public new abstract class rGOLAmt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjustReport.rGOLAmt>
  {
  }

  public new abstract class adjAmt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjustReport.adjAmt>
  {
  }

  public new abstract class adjDiscAmt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjustReport.adjDiscAmt>
  {
  }

  public abstract class adjWOAmt : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjustReport.adjWOAmt>
  {
  }

  public new abstract class curyAdjgAmt : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APAdjustReport.curyAdjgAmt>
  {
  }

  public new abstract class adjWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjustReport.adjWhTaxAmt>
  {
  }

  public new abstract class curyAdjdWhTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjustReport.curyAdjdWhTaxAmt>
  {
  }

  public abstract class lineTotalAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjustReport.lineTotalAdjusted>
  {
  }

  public abstract class curyLineTotalAdjusted : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjustReport.curyLineTotalAdjusted>
  {
  }

  public abstract class lineTotalAdjusting : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjustReport.lineTotalAdjusting>
  {
  }

  public abstract class curyLineTotalAdjusting : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjustReport.curyLineTotalAdjusting>
  {
  }
}
