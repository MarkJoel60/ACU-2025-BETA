// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies.ComplianceDocumentEntityStrategy
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies;

public abstract class ComplianceDocumentEntityStrategy
{
  public Type EntityType { get; protected set; }

  public Type FilterExpression { get; protected set; }

  public Type TypeField { get; protected set; }

  public abstract Guid? GetNoteId(PXGraph graph, string clDisplayName);
}
