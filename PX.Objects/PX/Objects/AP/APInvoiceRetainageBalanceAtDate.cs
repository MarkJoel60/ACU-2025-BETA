// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APInvoiceRetainageBalanceAtDate
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

[PXProjection(typeof (SelectFromBase<APRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Cross<DateInfo>>, FbqlJoins.Left<APRegisterReport>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APRegister.docType, Equal<APRegisterReport.origDocType>>>>, PX.Data.And<BqlOperand<APRegister.refNbr, IBqlString>.IsEqual<APRegisterReport.origRefNbr>>>, PX.Data.And<BqlOperand<APRegisterReport.docDate, IBqlDateTime>.IsLessEqual<DateInfo.date>>>, PX.Data.And<BqlOperand<APRegisterReport.released, IBqlBool>.IsEqual<PX.Data.True>>>>.And<BqlOperand<APRegisterReport.isRetainageDocument, IBqlBool>.IsEqual<PX.Data.True>>>>>.AggregateTo<GroupBy<APRegister.docType>, GroupBy<APRegister.refNbr>, GroupBy<DateInfo.date>, Sum<APRegisterReport.signReleasedRetainage>>))]
[PXCacheName("APInvoiceRetainageBalanceAtDate")]
[Serializable]
public class APInvoiceRetainageBalanceAtDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (APRegister.docType))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (APRegister.refNbr))]
  public virtual string RefNbr { get; set; }

  [PXDBDate(IsKey = true, BqlField = typeof (DateInfo.date))]
  public virtual System.DateTime? SubmissionDate { get; set; }

  [PXDBCalced(typeof (Mult<APRegisterReport.signAmount, Case<PX.Data.Where<BqlOperand<APRegisterReport.isRetainageDocument, IBqlBool>.IsEqual<PX.Data.True>>, APRegisterReport.origDocAmt, Case<PX.Data.Where<BqlOperand<APRegisterReport.isRetainageDocument, IBqlBool>.IsNotEqual<PX.Data.True>>, Mult<decimal_1, APRegisterReport.retainageTotal>>>>), typeof (Decimal))]
  [PXDecimal(4, BqlTable = typeof (APRegisterReport))]
  public virtual Decimal? LineTotal { get; set; }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceRetainageBalanceAtDate.docType>
  {
  }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APInvoiceRetainageBalanceAtDate.refNbr>
  {
  }

  public abstract class submissionDate : 
    BqlType<IBqlDateTime, System.DateTime>.Field<APInvoiceRetainageBalanceAtDate.submissionDate>
  {
  }

  public abstract class lineTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APInvoiceRetainageBalanceAtDate.lineTotal>
  {
  }
}
