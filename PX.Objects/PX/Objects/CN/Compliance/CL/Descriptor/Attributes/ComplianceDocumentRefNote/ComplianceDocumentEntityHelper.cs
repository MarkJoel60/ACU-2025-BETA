// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote;

public class ComplianceDocumentEntityHelper
{
  private static readonly ComplianceDocumentEntityStrategy[] Strategies = new ComplianceDocumentEntityStrategy[6]
  {
    (ComplianceDocumentEntityStrategy) new ApInvoiceStrategy(),
    (ComplianceDocumentEntityStrategy) new ApPaymentStrategy(),
    (ComplianceDocumentEntityStrategy) new ArInvoiceStrategy(),
    (ComplianceDocumentEntityStrategy) new ArPaymentStrategy(),
    (ComplianceDocumentEntityStrategy) new PoOrderStrategy(),
    (ComplianceDocumentEntityStrategy) new PmRegisterStrategy()
  };
  private readonly Type itemType;
  private readonly ComplianceDocumentEntityStrategy complianceDocumentEntityStrategy;

  public ComplianceDocumentEntityHelper(Type itemType)
  {
    this.itemType = itemType;
    this.complianceDocumentEntityStrategy = ((IEnumerable<ComplianceDocumentEntityStrategy>) ComplianceDocumentEntityHelper.Strategies).Single<ComplianceDocumentEntityStrategy>((Func<ComplianceDocumentEntityStrategy, bool>) (x => x.EntityType == itemType));
  }

  public bool IsStrategyExist => this.complianceDocumentEntityStrategy != null;

  public PXView CreateView(PXGraph graph)
  {
    BqlCommand bqlCommand = BqlCommand.CreateInstance(new Type[2]
    {
      typeof (Select<>),
      this.itemType
    });
    if (this.IsStrategyExist && this.complianceDocumentEntityStrategy.FilterExpression != (Type) null)
      bqlCommand = bqlCommand.WhereNew(this.complianceDocumentEntityStrategy.FilterExpression);
    return new PXView(graph, true, bqlCommand);
  }

  public Guid? GetNoteId(PXGraph graph, string clDisplayName)
  {
    return this.complianceDocumentEntityStrategy.GetNoteId(graph, clDisplayName);
  }
}
