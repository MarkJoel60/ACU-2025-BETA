// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies.PoOrderStrategy
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Abstractions;
using PX.Objects.PO;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies;

public class PoOrderStrategy : ComplianceDocumentEntityStrategy
{
  public PoOrderStrategy()
  {
    this.EntityType = typeof (POOrder);
    this.FilterExpression = typeof (Where<POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>);
    this.TypeField = typeof (POOrder.orderType);
  }

  public override Guid? GetNoteId(PXGraph graph, string clDisplayName)
  {
    DocumentKey documentKey = ComplianceReferenceTypeHelper.ConvertToDocumentKey<POOrder>(clDisplayName);
    return ((PXSelectBase<POOrder>) new PXSelect<POOrder, Where<POOrder.orderType, Equal<Required<POOrder.orderType>>, And<POOrder.orderNbr, Equal<Required<POOrder.orderNbr>>, And<POOrder.orderType, NotEqual<POOrderType.regularSubcontract>>>>>(graph)).Select(new object[2]
    {
      (object) documentKey.DocType,
      (object) documentKey.RefNbr
    }).FirstTableItems.ToList<POOrder>().SingleOrDefault<POOrder>()?.NoteID;
  }
}
