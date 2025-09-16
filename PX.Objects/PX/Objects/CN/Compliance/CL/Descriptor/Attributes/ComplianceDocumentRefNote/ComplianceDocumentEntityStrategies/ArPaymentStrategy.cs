// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies.ArPaymentStrategy
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

public class ArPaymentStrategy : ComplianceDocumentEntityStrategy
{
  public ArPaymentStrategy()
  {
    this.EntityType = typeof (ARPayment);
    this.FilterExpression = typeof (Where<ARPayment.docType, Equal<ARDocType.payment>, Or<ARPayment.docType, Equal<ARDocType.creditMemo>, Or<ARPayment.docType, Equal<ARDocType.prepayment>, Or<ARPayment.docType, Equal<ARDocType.refund>, Or<ARPayment.docType, Equal<ARDocType.voidPayment>, Or<ARPayment.docType, Equal<ARDocType.smallBalanceWO>, Or<ARPayment.docType, Equal<ARDocType.voidRefund>>>>>>>>);
    this.TypeField = typeof (ARPayment.docType);
  }

  public override Guid? GetNoteId(PXGraph graph, string clDisplayName)
  {
    DocumentKey documentKey = ComplianceReferenceTypeHelper.ConvertToDocumentKey<ARPayment>(clDisplayName);
    return ((PXSelectBase<ARPayment>) new PXSelect<ARPayment, Where<ARPayment.docType, Equal<Required<ARPayment.docType>>, And<ARPayment.refNbr, Equal<Required<ARPayment.refNbr>>>>>(graph)).Select(new object[2]
    {
      (object) documentKey.DocType,
      (object) documentKey.RefNbr
    }).FirstTableItems.ToList<ARPayment>().SingleOrDefault<ARPayment>()?.NoteID;
  }
}
