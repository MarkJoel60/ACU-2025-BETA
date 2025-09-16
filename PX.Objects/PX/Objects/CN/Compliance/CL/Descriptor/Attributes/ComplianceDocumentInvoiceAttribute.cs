// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentInvoiceAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using System;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class ComplianceDocumentInvoiceAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatedSubscriber
{
  public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ComplianceDocument row))
      return;
    Decimal? invoiceAmount = ComplianceDocumentInvoiceAttribute.GetInvoiceAmount(row.InvoiceID, sender.Graph);
    sender.SetValue<ComplianceDocument.invoiceAmount>((object) row, (object) invoiceAmount);
  }

  private static Decimal? GetInvoiceAmount(Guid? refNoteId, PXGraph senderGraph)
  {
    if (!refNoteId.HasValue)
      return new Decimal?();
    ComplianceDocumentReference documentReference = ComplianceDocumentReferenceRetriever.GetComplianceDocumentReference(senderGraph, refNoteId);
    return ComplianceDocumentInvoiceAttribute.GetInvoice(senderGraph, documentReference)?.OrigDocAmt;
  }

  private static ARInvoice GetInvoice(PXGraph senderGraph, ComplianceDocumentReference reference)
  {
    return ((PXSelectBase<ARInvoice>) new PXSelect<ARInvoice, Where<ARInvoice.docType, Equal<Required<ARInvoice.docType>>, And<ARInvoice.refNbr, Equal<Required<ARInvoice.refNbr>>>>>(senderGraph)).SelectSingle(new object[2]
    {
      (object) reference.Type,
      (object) reference.ReferenceNumber
    });
  }
}
