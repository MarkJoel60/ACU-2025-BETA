// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APAdjustedBalanceAtDate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXProjection(typeof (SelectFromBase<APRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Cross<DateInfo>>, FbqlJoins.Left<APAdjustReport>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.docType, Equal<APAdjustReport.adjdDocType>>>>, PX.Data.And<BqlOperand<APRegister.refNbr, IBqlString>.IsEqual<APAdjustReport.adjdRefNbr>>>, PX.Data.And<BqlOperand<APAdjustReport.adjgDocDate, IBqlDateTime>.IsLessEqual<DateInfo.date>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjdDocType, NotEqual<APAdjustReport.adjgDocType>>>>>.Or<BqlOperand<APAdjustReport.adjdRefNbr, IBqlString>.IsNotEqual<APAdjustReport.adjgRefNbr>>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.released, Equal<PX.Data.True>>>>>.Or<BqlOperand<APAdjustReport.released, IBqlBool>.IsNull>>.AggregateTo<GroupBy<APRegister.docType>, GroupBy<APRegister.refNbr>, GroupBy<DateInfo.date>, Sum<APAdjustReport.lineTotalAdjusted>, Sum<APAdjustReport.curyLineTotalAdjusted>>))]
[PXCacheName("APAdjustedBalanceAtDate")]
[Serializable]
public class APAdjustedBalanceAtDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APRegister.docType))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (APRegister.refNbr))]
  public virtual string RefNbr { get; set; }

  [PXDBDate(IsKey = true, BqlField = typeof (DateInfo.date))]
  public virtual System.DateTime? SubmissionDate { get; set; }

  [PXDBCalced(typeof (Sub<Mult<Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.check>>>>>.And<BqlOperand<APAdjustReport.adjdDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>, decimal1, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.voidCheck>>>>>.And<BqlOperand<APAdjustReport.adjdDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>, decimal1, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjdDocType, Equal<APDocType.prepayment>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.check>>>>, PX.Data.Or<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsEqual<APDocType.voidCheck>>>>.Or<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsEqual<APDocType.prepayment>>>>, decimal_1, Case<PX.Data.Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj>>, decimal1, Case<PX.Data.Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.prepayment>>, decimal_1, Case<PX.Data.Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.quickCheck, APDocType.voidQuickCheck>>, decimal0>>>>>>>, Add<APAdjustReport.adjAmt, Add<APAdjustReport.adjDiscAmt, APAdjustReport.adjWhTaxAmt>>>, APAdjustReport.rGOLAmt>), typeof (Decimal))]
  [PXDecimal(4, BqlTable = typeof (APAdjustReport))]
  public virtual Decimal? LineTotal { get; set; }

  [PXDBCalced(typeof (Mult<Switch<Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.check>>>>>.And<BqlOperand<APAdjustReport.adjdDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>, decimal1, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.voidCheck>>>>>.And<BqlOperand<APAdjustReport.adjdDocType, IBqlString>.IsEqual<APDocType.debitAdj>>>, decimal1, Case<PX.Data.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjdDocType, Equal<APDocType.prepayment>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APAdjustReport.adjgDocType, Equal<APDocType.check>>>>, PX.Data.Or<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsEqual<APDocType.voidCheck>>>>.Or<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsEqual<APDocType.prepayment>>>>, decimal_1, Case<PX.Data.Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.refund, APDocType.voidRefund, APDocType.invoice, APDocType.creditAdj>>, decimal1, Case<PX.Data.Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.debitAdj, APDocType.check, APDocType.voidCheck, APDocType.prepayment>>, decimal_1, Case<PX.Data.Where<BqlOperand<APAdjustReport.adjgDocType, IBqlString>.IsIn<APDocType.quickCheck, APDocType.voidQuickCheck>>, decimal0>>>>>>>, Add<APAdjustReport.curyAdjdAmt, Add<APAdjustReport.curyAdjdDiscAmt, APAdjustReport.curyAdjdWhTaxAmt>>>), typeof (Decimal))]
  [PXDecimal(4, BqlTable = typeof (APAdjustReport))]
  public virtual Decimal? CuryLineTotal { get; set; }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjustedBalanceAtDate.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APAdjustedBalanceAtDate.refNbr>
  {
  }

  public abstract class submissionDate : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APAdjustedBalanceAtDate.submissionDate>
  {
  }

  public abstract class lineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APAdjustedBalanceAtDate.lineTotal>
  {
  }
}
