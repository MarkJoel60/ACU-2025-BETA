// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentCheckAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CN.Compliance.CL.DAC;
using System;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class ComplianceDocumentCheckAttribute : PXEventSubscriberAttribute, IPXFieldUpdatedSubscriber
{
  public void FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs args)
  {
    if (!(args.Row is ComplianceDocument row))
      return;
    this.UpdateCheckNumber(cache, row);
  }

  private void UpdateCheckNumber(PXCache cache, ComplianceDocument document)
  {
    string extRefNbr = document.ApCheckID.HasValue ? this.GetPayment(document.ApCheckID, cache.Graph)?.ExtRefNbr : (string) null;
    cache.SetValue<ComplianceDocument.checkNumber>((object) document, (object) extRefNbr);
  }

  private APPayment GetPayment(Guid? checkId, PXGraph graph)
  {
    ComplianceDocumentReference documentReference = this.GetComplianceDocumentReference(checkId, graph);
    if (documentReference == null)
      return (APPayment) null;
    return ((PXSelectBase<APPayment>) new PXSelect<APPayment, Where<APPayment.refNbr, Equal<Required<APPayment.refNbr>>, And<APPayment.docType, Equal<Required<APPayment.docType>>>>>(graph)).SelectSingle(new object[2]
    {
      (object) documentReference.ReferenceNumber,
      (object) documentReference.Type
    });
  }

  private ComplianceDocumentReference GetComplianceDocumentReference(Guid? checkId, PXGraph graph)
  {
    return ((PXSelectBase<ComplianceDocumentReference>) new PXSelect<ComplianceDocumentReference, Where<ComplianceDocumentReference.complianceDocumentReferenceId, Equal<Required<ComplianceDocumentReference.complianceDocumentReferenceId>>>>(graph)).SelectSingle(new object[1]
    {
      (object) checkId
    });
  }
}
