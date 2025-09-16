// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMSubAuditReportRetainageNotLinkedToSubcontract
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
/// A projection over the <see cref="T:PX.Objects.AP.APRegister" /> class joined with the <see cref="T:PX.Objects.AP.APTran" /> class.
/// The projection is used in Subcontract Audit Report.
/// </summary>
[PXCacheName("PM Subcontract Audit Report Retainage Not Linked to Subcontract")]
[PXProjection(typeof (SelectFromBase<APRegister, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<APTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APRegister.refNbr, Equal<APTran.refNbr>>>>>.And<BqlOperand<APRegister.docType, IBqlString>.IsEqual<APTran.tranType>>>>>.Where<BqlOperand<APRegister.isRetainageDocument, IBqlBool>.IsEqual<True>>.Aggregate<To<GroupBy<APRegister.origRefNbr>, GroupBy<APRegister.origDocType>, GroupBy<APTran.tranType>, GroupBy<APTran.refNbr>>>.Having<BqlAggregatedOperand<Max<APTran.pONbr>, IBqlString>.IsNull>))]
[Serializable]
public class PMSubAuditReportRetainageNotLinkedToSubcontract : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.AP.APRegister.OrigRefNbr" />
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (APRegister.origRefNbr))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.AP.APRegister.OrigDocType" />
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (APRegister.origDocType))]
  public virtual string DocType { get; set; }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMSubAuditReportRetainageNotLinkedToSubcontract.refNbr>
  {
  }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMSubAuditReportRetainageNotLinkedToSubcontract.docType>
  {
  }
}
