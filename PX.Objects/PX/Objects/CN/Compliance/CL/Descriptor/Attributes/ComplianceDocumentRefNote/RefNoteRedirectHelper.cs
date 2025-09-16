// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.RefNoteRedirectHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CN.Compliance.CL.DAC;
using PX.Objects.CN.Compliance.CL.Services;
using PX.Objects.PM;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote;

public class RefNoteRedirectHelper
{
  private readonly IList<RefNoteBasedRedirectionInstruction> redirectionInstructions = (IList<RefNoteBasedRedirectionInstruction>) new List<RefNoteBasedRedirectionInstruction>()
  {
    new RefNoteBasedRedirectionInstruction(typeof (PX.Objects.AP.APInvoice), typeof (APInvoiceEntry), typeof (PX.Objects.AP.APInvoice.docType), typeof (PX.Objects.AP.APInvoice.refNbr)),
    new RefNoteBasedRedirectionInstruction(typeof (ARInvoice), typeof (ARInvoiceEntry), typeof (ARInvoice.docType), typeof (ARInvoice.refNbr)),
    new RefNoteBasedRedirectionInstruction(typeof (APPayment), typeof (APPaymentEntry), typeof (APPayment.docType), typeof (APPayment.refNbr)),
    new RefNoteBasedRedirectionInstruction(typeof (ARPayment), typeof (ARPaymentEntry), typeof (ARPayment.docType), typeof (ARPayment.refNbr)),
    new RefNoteBasedRedirectionInstruction(typeof (PX.Objects.PO.POOrder), typeof (POOrderEntry), typeof (PX.Objects.PO.POOrder.orderType), typeof (PX.Objects.PO.POOrder.orderNbr)),
    new RefNoteBasedRedirectionInstruction(typeof (PMRegister), typeof (RegisterEntry), typeof (PMRegister.module), typeof (PMRegister.refNbr))
  };

  public void Redirect(Type itemType, Guid referenceId)
  {
    RefNoteBasedRedirectionInstruction redirectionInstruction = this.redirectionInstructions.Single<RefNoteBasedRedirectionInstruction>((Func<RefNoteBasedRedirectionInstruction, bool>) (x => x.EntityType == itemType));
    PXGraph instance = PXGraph.CreateInstance(redirectionInstruction.GraphType);
    if (instance != null)
    {
      ComplianceDocumentReference documentReference = ComplianceDocumentReferenceRetriever.GetComplianceDocumentReference(instance, new Guid?(referenceId));
      object entity = RefNoteRedirectHelper.GetEntity(instance, redirectionInstruction, (object) documentReference.Type, (object) documentReference.ReferenceNumber);
      instance.Caches[redirectionInstruction.EntityType].Current = entity;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException(instance, string.Empty);
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
  }

  /// <summary>
  /// Dynamically builds a query in the following format
  /// PXSelect&lt;TEntity, Where&lt;TRefNoteField, Equal&lt;Required&lt;TRefNoteField&gt;&gt;&gt;&gt; and selects
  /// single record.
  /// </summary>
  private static object GetEntity(
    PXGraph graph,
    RefNoteBasedRedirectionInstruction redirectionInstruction,
    params object[] bqlParameters)
  {
    BqlCommand instance = BqlCommand.CreateInstance(new Type[12]
    {
      typeof (Select<,>),
      redirectionInstruction.EntityType,
      typeof (Where<,,>),
      redirectionInstruction.ReferenceTypeField,
      typeof (Equal<>),
      typeof (Required<>),
      redirectionInstruction.ReferenceTypeField,
      typeof (And<,>),
      redirectionInstruction.ReferenceNumberField,
      typeof (Equal<>),
      typeof (Required<>),
      redirectionInstruction.ReferenceNumberField
    });
    return new PXView(graph, true, instance).SelectSingle(bqlParameters);
  }
}
