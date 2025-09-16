// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentBillAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using System;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class ComplianceDocumentBillAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatedSubscriber,
  IPXRowSelectedSubscriber
{
  public void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ComplianceDocument row))
      return;
    Decimal? billAmount = ComplianceDocumentBillAttribute.GetBillAmount(row.BillID, sender.Graph);
    sender.SetValue<ComplianceDocument.billAmount>((object) row, (object) billAmount);
  }

  private static Decimal? GetBillAmount(Guid? refNoteId, PXGraph senderGraph)
  {
    if (!refNoteId.HasValue)
      return new Decimal?();
    ComplianceDocumentReference documentReference = ComplianceDocumentReferenceRetriever.GetComplianceDocumentReference(senderGraph, refNoteId);
    return ComplianceDocumentBillAttribute.GetBill(senderGraph, documentReference)?.OrigDocAmt;
  }

  private static APInvoice GetBill(PXGraph senderGraph, ComplianceDocumentReference reference)
  {
    return ((PXSelectBase<APInvoice>) new PXSelect<APInvoice, Where<APInvoice.docType, Equal<Required<APInvoice.docType>>, And<APInvoice.refNbr, Equal<Required<APInvoice.refNbr>>>>>(senderGraph)).SelectSingle(new object[2]
    {
      (object) reference.Type,
      (object) reference.ReferenceNumber
    });
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ComplianceDocument row))
      return;
    PXUIFieldAttribute.SetEnabled<ComplianceDocument.billID>(sender, (object) row, !row.IsCreatedAutomatically.GetValueOrDefault());
  }
}
