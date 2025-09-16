// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARInvoiceRetainageBalanceAtDate
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

[PXProjection(typeof (SelectFromBase<ARRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Cross<DateInfo>>, FbqlJoins.Left<ARRegisterReport>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegister.docType, Equal<ARRegisterReport.origDocType>>>>, And<BqlOperand<ARRegister.refNbr, IBqlString>.IsEqual<ARRegisterReport.origRefNbr>>>, And<BqlOperand<ARRegisterReport.docDate, IBqlDateTime>.IsLessEqual<DateInfo.date>>>, And<BqlOperand<ARRegisterReport.released, IBqlBool>.IsEqual<True>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegisterReport.isRetainageDocument, Equal<True>>>>>.Or<BqlOperand<ARRegisterReport.isRetainageReversing, IBqlBool>.IsEqual<True>>>>>>.AggregateTo<GroupBy<ARRegister.docType>, GroupBy<ARRegister.refNbr>, GroupBy<DateInfo.date>, Sum<ARRegisterReport.signReleasedRetainage>>))]
[PXCacheName("ARInvoiceRetainageBalanceAtDate")]
[Serializable]
public class ARInvoiceRetainageBalanceAtDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (ARRegister.docType))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (ARRegister.refNbr))]
  public virtual string RefNbr { get; set; }

  [PXDBDate(IsKey = true, BqlField = typeof (DateInfo.date))]
  public virtual DateTime? SubmissionDate { get; set; }

  [PXDBCalced(typeof (Mult<ARRegisterReport.signAmount, Case<Where<BqlOperand<ARRegisterReport.isRetainageDocument, IBqlBool>.IsEqual<True>>, ARRegisterReport.origDocAmt, Case<Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegisterReport.isRetainageDocument, NotEqual<True>>>>>.And<BqlOperand<ARRegisterReport.isRetainageReversing, IBqlBool>.IsEqual<True>>>, Mult<decimal_1, ARRegisterReport.retainageTotal>>>>), typeof (Decimal))]
  [PXDecimal(4, BqlTable = typeof (ARRegisterReport))]
  public virtual Decimal? LineTotal { get; set; }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceRetainageBalanceAtDate.docType>
  {
  }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARInvoiceRetainageBalanceAtDate.refNbr>
  {
  }

  public abstract class submissionDate : 
    BqlType<IBqlDateTime, DateTime>.Field<ARInvoiceRetainageBalanceAtDate.submissionDate>
  {
  }

  public abstract class lineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARInvoiceRetainageBalanceAtDate.lineTotal>
  {
  }
}
