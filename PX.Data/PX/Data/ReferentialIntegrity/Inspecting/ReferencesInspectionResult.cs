// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Inspecting.ReferencesInspectionResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Inspecting;

/// <summary>
/// Represents a result of inspecting raw <see cref="T:PX.Data.ReferentialIntegrity.Reference" />s of some <see cref="T:PX.Data.IBqlTable" />
/// </summary>
[ImmutableObject(true)]
[PXInternalUseOnly]
public class ReferencesInspectionResult
{
  private static readonly Reference[] EmptyArray = new Reference[0];

  internal ReferencesInspectionResult(
    System.Type inspectingTable,
    IEnumerable<Reference> outgoingReferences,
    IEnumerable<Reference> incomingReferences)
  {
    this.InspectingTable = inspectingTable;
    this.OutgoingReferences = outgoingReferences == null || !outgoingReferences.Any<Reference>() ? (IReadOnlyCollection<Reference>) ReferencesInspectionResult.EmptyArray : (IReadOnlyCollection<Reference>) outgoingReferences.ToArray<Reference>();
    this.IncomingReferences = incomingReferences == null || !incomingReferences.Any<Reference>() ? (IReadOnlyCollection<Reference>) ReferencesInspectionResult.EmptyArray : (IReadOnlyCollection<Reference>) incomingReferences.ToArray<Reference>();
  }

  public System.Type InspectingTable { get; }

  public IReadOnlyCollection<Reference> OutgoingReferences { get; }

  public IReadOnlyCollection<Reference> IncomingReferences { get; }
}
