// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Services.ComplianceDocumentReferenceRetriever
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.Common;
using PX.Objects.Common.Abstractions;
using System;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Services;

public static class ComplianceDocumentReferenceRetriever
{
  public static ComplianceDocumentReference GetComplianceDocumentReference(
    PXGraph graph,
    Guid? referenceId)
  {
    return ((PXSelectBase<ComplianceDocumentReference>) new PXSelect<ComplianceDocumentReference, Where<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<Required<ComplianceDocumentReference.complianceDocumentReferenceId>>>>(graph)).SelectSingle(new object[1]
    {
      (object) referenceId
    });
  }

  public static ComplianceDocumentReference GetComplianceDocumentReference(
    PXGraph graph,
    IDocumentKey documentKey)
  {
    return ((PXSelectBase<ComplianceDocumentReference>) new PXSelect<ComplianceDocumentReference, Where<ComplianceDocumentReference.type, Equal<Required<ComplianceDocumentReference.type>>, And<ComplianceDocumentReference.referenceNumber, Equal<Required<ComplianceDocumentReference.referenceNumber>>>>>(graph)).SelectSingle(new object[2]
    {
      (object) documentKey.DocType,
      (object) documentKey.RefNbr
    });
  }

  public static ComplianceDocumentReference GetComplianceDocumentReference(
    PXGraph graph,
    IDocumentAdjustment adjust)
  {
    return ((PXSelectBase<ComplianceDocumentReference>) new PXSelect<ComplianceDocumentReference, Where<ComplianceDocumentReference.type, Equal<Required<ComplianceDocumentReference.type>>, And<ComplianceDocumentReference.referenceNumber, Equal<Required<ComplianceDocumentReference.referenceNumber>>>>>(graph)).SelectSingle(new object[2]
    {
      (object) adjust.AdjgDocType,
      (object) adjust.AdjgRefNbr
    });
  }

  public static ComplianceDocumentReference GetComplianceDocumentReference(
    PXGraph graph,
    APAdjust adjustment)
  {
    return ((PXSelectBase<ComplianceDocumentReference>) new PXSelect<ComplianceDocumentReference, Where<ComplianceDocumentReference.type, Equal<Required<ComplianceDocumentReference.type>>, And<ComplianceDocumentReference.referenceNumber, Equal<Required<ComplianceDocumentReference.referenceNumber>>>>>(graph)).SelectSingle(new object[2]
    {
      (object) adjustment.DisplayDocType,
      (object) adjustment.DisplayRefNbr
    });
  }

  public static ComplianceDocumentReference GetComplianceDocumentReference(
    PXGraph graph,
    APTran transaction)
  {
    return ((PXSelectBase<ComplianceDocumentReference>) new PXSelect<ComplianceDocumentReference, Where<ComplianceDocumentReference.type, Equal<Required<ComplianceDocumentReference.type>>, And<ComplianceDocumentReference.referenceNumber, Equal<Required<ComplianceDocumentReference.referenceNumber>>>>>(graph)).SelectSingle(new object[2]
    {
      (object) transaction.POOrderType,
      (object) transaction.PONbr
    });
  }

  public static Guid? GetComplianceDocumentReferenceId(PXGraph graph, IDocumentKey documentKey)
  {
    return ComplianceDocumentReferenceRetriever.GetComplianceDocumentReference(graph, documentKey)?.ComplianceDocumentReferenceId;
  }

  public static Guid? GetComplianceDocumentReferenceId(PXGraph graph, IDocumentAdjustment arAdjust)
  {
    return ComplianceDocumentReferenceRetriever.GetComplianceDocumentReference(graph, arAdjust)?.ComplianceDocumentReferenceId;
  }

  public static Guid? GetComplianceDocumentReferenceId(PXGraph graph, APInvoice apInvoice)
  {
    return ComplianceDocumentReferenceRetriever.GetComplianceDocumentReference(graph, (IDocumentKey) apInvoice)?.ComplianceDocumentReferenceId;
  }

  public static Guid? GetComplianceDocumentReferenceId(PXGraph graph, APTran transaction)
  {
    return ComplianceDocumentReferenceRetriever.GetComplianceDocumentReference(graph, transaction)?.ComplianceDocumentReferenceId;
  }

  public static Guid? GetComplianceDocumentReferenceId(PXGraph graph, APAdjust adjustment)
  {
    return ComplianceDocumentReferenceRetriever.GetComplianceDocumentReference(graph, adjustment)?.ComplianceDocumentReferenceId;
  }
}
