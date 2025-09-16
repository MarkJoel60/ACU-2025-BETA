// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies.ApPaymentStrategy
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

public class ApPaymentStrategy : ComplianceDocumentEntityStrategy
{
  public ApPaymentStrategy()
  {
    this.EntityType = typeof (APPayment);
    this.FilterExpression = typeof (Where<APPayment.docType, Equal<APDocType.check>, Or<APPayment.docType, Equal<APDocType.debitAdj>, Or<APPayment.docType, Equal<APDocType.prepayment>, Or<APPayment.docType, Equal<APDocType.refund>, Or<APPayment.docType, Equal<APDocType.voidCheck>, Or<APPayment.docType, Equal<APDocType.voidRefund>>>>>>>);
    this.TypeField = typeof (APPayment.docType);
  }

  public override Guid? GetNoteId(PXGraph graph, string clDisplayName)
  {
    DocumentKey documentKey = ComplianceReferenceTypeHelper.ConvertToDocumentKey<APPayment>(clDisplayName);
    return ((PXSelectBase<APPayment>) new PXSelect<APPayment, Where<APPayment.docType, Equal<Required<APPayment.docType>>, And<APPayment.refNbr, Equal<Required<APPayment.refNbr>>>>>(graph)).Select(new object[2]
    {
      (object) documentKey.DocType,
      (object) documentKey.RefNbr
    }).FirstTableItems.ToList<APPayment>().SingleOrDefault<APPayment>()?.NoteID;
  }
}
