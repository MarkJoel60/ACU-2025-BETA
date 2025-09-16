// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMSubAuditReportRetainageNotLinkedToSubcontractGrouped
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.CN;

/// <summary>
/// A projection over the <see cref="T:PX.Objects.CN.PMSubAuditReportRetainageNotLinkedToSubcontract" /> class but grouped.
/// The projection is used in Subcontract Audit Report.
/// </summary>
[PXCacheName("PM Subcontract Audit Report Retainage Not Linked to Subcontract Grouped")]
[PXProjection(typeof (SelectFromBase<PMSubAuditReportRetainageNotLinkedToSubcontract, TypeArrayOf<IFbqlJoin>.Empty>.AggregateTo<GroupBy<PMSubAuditReportRetainageNotLinkedToSubcontract.refNbr>, GroupBy<PMSubAuditReportRetainageNotLinkedToSubcontract.docType>>))]
[Serializable]
public class PMSubAuditReportRetainageNotLinkedToSubcontractGrouped : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.CN.PMSubAuditReportRetainageNotLinkedToSubcontract.RefNbr" />
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PMSubAuditReportRetainageNotLinkedToSubcontract.refNbr))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.CN.PMSubAuditReportRetainageNotLinkedToSubcontract.DocType" />
  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (PMSubAuditReportRetainageNotLinkedToSubcontract.docType))]
  public virtual string DocType { get; set; }

  public abstract class refNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMSubAuditReportRetainageNotLinkedToSubcontractGrouped.refNbr>
  {
  }

  public abstract class docType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMSubAuditReportRetainageNotLinkedToSubcontractGrouped.docType>
  {
  }
}
