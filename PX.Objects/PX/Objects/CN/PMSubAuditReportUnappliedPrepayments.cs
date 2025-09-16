// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMSubAuditReportUnappliedPrepayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.CN;

/// <summary>
/// A projection over the <see cref="T:PX.Objects.AP.APRegister" /> class joined with the <see cref="T:PX.Objects.AP.APTran" /> class
/// and <see cref="T:PX.Objects.AP.APAdjust" /> class.
/// The projection is used in Subcontract Audit Report.
/// </summary>
[PXCacheName("PM Subcontract Audit Report Unapplied Prepayments")]
[PXProjection(typeof (SelectFromBase<APRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<APTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APRegister.refNbr, Equal<APTran.refNbr>>>>>.And<BqlOperand<APRegister.docType, IBqlString>.IsEqual<APTran.tranType>>>>, FbqlJoins.Inner<APAdjust>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APRegister.refNbr, Equal<APAdjust.adjdRefNbr>>>>>.And<BqlOperand<APRegister.docType, IBqlString>.IsEqual<APAdjust.adjdDocType>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APAdjust.released, Equal<True>>>>>.And<BqlOperand<APTran.tranType, IBqlString>.IsEqual<APDocType.prepayment>>>.AggregateTo<GroupBy<APTran.pONbr>, GroupBy<APTran.refNbr>, GroupBy<APTran.projectID>, GroupBy<APTran.vendorID>, GroupBy<APAdjust.adjdDocDate>, Sum<APTran.curyTranAmt>>))]
[Serializable]
public class PMSubAuditReportUnappliedPrepayments : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.AP.APTran.PONbr" />
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (APTran.pONbr))]
  public virtual 
  #nullable disable
  string PONbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.APTran.RefNbr" />
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (APTran.refNbr))]
  public virtual string RefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.APTran.ProjectID" />
  [PXDBInt(BqlField = typeof (APTran.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.APTran.VendorID" />
  [PXDBInt(BqlField = typeof (APTran.vendorID))]
  public virtual int? VendorID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.APRegister.DocDesc" />
  [PXDBString(60, BqlField = typeof (APRegister.docDesc))]
  public virtual string Description { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.APTran.TranType" />
  [PXDBString(10, BqlField = typeof (APTran.tranType))]
  public virtual string TranType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.APAdjust.AdjdDocDate" />
  [PXDBDate(BqlField = typeof (APAdjust.adjdDocDate))]
  public virtual DateTime? Date { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.APTran.CuryTranAmt" />
  [PXDBDecimal(BqlField = typeof (APTran.curyTranAmt))]
  public virtual Decimal? CuryAmount { get; set; }

  public abstract class pONbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMSubAuditReportUnappliedPrepayments.pONbr>
  {
  }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMSubAuditReportUnappliedPrepayments.refNbr>
  {
  }

  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMSubAuditReportUnappliedPrepayments.projectID>
  {
  }

  public abstract class vendorID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMSubAuditReportUnappliedPrepayments.vendorID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMSubAuditReportUnappliedPrepayments.description>
  {
  }

  public abstract class tranType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMSubAuditReportUnappliedPrepayments.tranType>
  {
  }

  public abstract class date : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMSubAuditReportUnappliedPrepayments.date>
  {
  }

  public abstract class curyAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMSubAuditReportUnappliedPrepayments.curyAmount>
  {
  }
}
