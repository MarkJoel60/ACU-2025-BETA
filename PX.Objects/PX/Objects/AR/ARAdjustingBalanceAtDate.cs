// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAdjustingBalanceAtDate
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
namespace PX.Objects.AR;

[PXProjection(typeof (SelectFromBase<ARRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Cross<DateInfo>>, FbqlJoins.Left<ARAdjustReport>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<ARAdjustReport.adjgDocType>>>>, And<BqlOperand<ARRegister.refNbr, IBqlString>.IsEqual<ARAdjustReport.adjgRefNbr>>>>.And<BqlOperand<ARAdjustReport.adjgDocDate, IBqlDateTime>.IsLessEqual<DateInfo.date>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjustReport.released, Equal<True>>>>>.Or<BqlOperand<ARAdjustReport.released, IBqlBool>.IsNull>>.AggregateTo<GroupBy<ARRegister.docType>, GroupBy<ARRegister.refNbr>, GroupBy<DateInfo.date>, Sum<ARAdjustReport.lineTotalAdjusting>, Sum<ARAdjustReport.curyLineTotalAdjusting>>))]
[PXCacheName("ARAdjustingBalanceAtDate")]
[Serializable]
public class ARAdjustingBalanceAtDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (ARRegister.docType))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (ARRegister.refNbr))]
  public virtual string RefNbr { get; set; }

  [PXDBDate(IsKey = true, BqlField = typeof (DateInfo.date))]
  public virtual DateTime? SubmissionDate { get; set; }

  [PXDBCalced(typeof (Mult<Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjustReport.adjgDocType, Equal<ARDocType.payment>>>>>.And<BqlOperand<ARAdjustReport.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjustReport.adjgDocType, Equal<ARDocType.prepayment>>>>>.And<BqlOperand<ARAdjustReport.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjustReport.adjgDocType, Equal<ARDocType.voidPayment>>>>>.And<BqlOperand<ARAdjustReport.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlOperand<ARAdjustReport.adjgDocType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO>>, decimal1, Case<Where<BqlOperand<ARAdjustReport.adjgDocType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO>>, decimal_1, Case<Where<BqlOperand<ARAdjustReport.adjgDocType, IBqlString>.IsIn<ARDocType.cashSale, ARDocType.cashReturn>>, decimal0>>>>>>>, ARAdjustReport.adjAmt>), typeof (Decimal))]
  [PXDecimal(4, BqlTable = typeof (ARAdjustReport))]
  public virtual Decimal? LineTotal { get; set; }

  [PXDBCalced(typeof (Mult<Switch<Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjustReport.adjgDocType, Equal<ARDocType.payment>>>>>.And<BqlOperand<ARAdjustReport.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjustReport.adjgDocType, Equal<ARDocType.prepayment>>>>>.And<BqlOperand<ARAdjustReport.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjustReport.adjgDocType, Equal<ARDocType.voidPayment>>>>>.And<BqlOperand<ARAdjustReport.adjdDocType, IBqlString>.IsEqual<ARDocType.creditMemo>>>, decimal1, Case<Where<BqlOperand<ARAdjustReport.adjgDocType, IBqlString>.IsIn<ARDocType.refund, ARDocType.voidRefund, ARDocType.invoice, ARDocType.debitMemo, ARDocType.finCharge, ARDocType.smallCreditWO>>, decimal1, Case<Where<BqlOperand<ARAdjustReport.adjgDocType, IBqlString>.IsIn<ARDocType.creditMemo, ARDocType.payment, ARDocType.prepayment, ARDocType.voidPayment, ARDocType.smallBalanceWO>>, decimal_1, Case<Where<BqlOperand<ARAdjustReport.adjgDocType, IBqlString>.IsIn<ARDocType.cashSale, ARDocType.cashReturn>>, decimal0>>>>>>>, ARAdjustReport.curyAdjgAmt>), typeof (Decimal))]
  [PXDecimal(4, BqlTable = typeof (ARAdjustReport))]
  public virtual Decimal? CuryLineTotal { get; set; }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjustingBalanceAtDate.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARAdjustingBalanceAtDate.refNbr>
  {
  }

  public abstract class submissionDate : 
    BqlType<IBqlDateTime, DateTime>.Field<ARAdjustingBalanceAtDate.submissionDate>
  {
  }

  public abstract class lineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARAdjustingBalanceAtDate.lineTotal>
  {
  }
}
