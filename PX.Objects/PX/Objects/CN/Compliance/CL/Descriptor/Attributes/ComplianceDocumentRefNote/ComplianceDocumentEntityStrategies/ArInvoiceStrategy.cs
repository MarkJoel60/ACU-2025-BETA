// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies.ArInvoiceStrategy
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.Common.Abstractions;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies;

public class ArInvoiceStrategy : ComplianceDocumentEntityStrategy
{
  public ArInvoiceStrategy()
  {
    this.EntityType = typeof (ARInvoice);
    this.FilterExpression = typeof (Where<ARInvoice.docType, Equal<ARDocType.invoice>, Or<ARInvoice.docType, Equal<ARDocType.debitMemo>, Or<ARInvoice.docType, Equal<ARDocType.creditMemo>, Or<ARInvoice.docType, Equal<ARDocType.finCharge>, Or<ARInvoice.docType, Equal<ARDocType.smallCreditWO>>>>>>);
    this.TypeField = typeof (ARInvoice.docType);
  }

  public override Guid? GetNoteId(PXGraph graph, string clDisplayName)
  {
    DocumentKey documentKey = ComplianceReferenceTypeHelper.ConvertToDocumentKey<ARInvoice>(clDisplayName);
    return ((PXSelectBase<ARInvoice>) new PXSelect<ARInvoice, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>(graph)).Select(new object[2]
    {
      (object) documentKey.DocType,
      (object) documentKey.RefNbr
    }).FirstTableItems.ToList<ARInvoice>().SingleOrDefault<ARInvoice>()?.NoteID;
  }
}
