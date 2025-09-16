// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies.ApInvoiceStrategy
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.Common.Abstractions;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies;

public class ApInvoiceStrategy : ComplianceDocumentEntityStrategy
{
  public ApInvoiceStrategy()
  {
    this.EntityType = typeof (APInvoice);
    this.FilterExpression = typeof (Where<APInvoice.docType, Equal<APDocType.invoice>, Or<APInvoice.docType, Equal<APDocType.creditAdj>, Or<APInvoice.docType, Equal<APDocType.debitAdj>, Or<APInvoice.docType, Equal<APDocType.prepayment>>>>>);
    this.TypeField = typeof (APInvoice.docType);
  }

  public override Guid? GetNoteId(PXGraph graph, string clDisplayName)
  {
    DocumentKey documentKey = ComplianceReferenceTypeHelper.ConvertToDocumentKey<APInvoice>(clDisplayName);
    return ((PXSelectBase<APInvoice>) new PXSelect<APInvoice, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>(graph)).Select(new object[2]
    {
      (object) documentKey.DocType,
      (object) documentKey.RefNbr
    }).FirstTableItems.ToList<APInvoice>().SingleOrDefault<APInvoice>()?.NoteID;
  }
}
