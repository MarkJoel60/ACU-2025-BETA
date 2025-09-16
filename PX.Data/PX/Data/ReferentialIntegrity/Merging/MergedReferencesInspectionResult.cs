// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Merging.MergedReferencesInspectionResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.ReferentialIntegrity.Inspecting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Merging;

/// <summary>
/// Represents a result of inspecting merged <see cref="T:PX.Data.ReferentialIntegrity.Reference" />s of some <see cref="T:PX.Data.IBqlTable" />
/// </summary>
[ImmutableObject(true)]
[PXInternalUseOnly]
public class MergedReferencesInspectionResult
{
  private static readonly MergedReference[] EmptyArray = new MergedReference[0];

  public MergedReferencesInspectionResult(
    System.Type inspectingTable,
    IEnumerable<MergedReference> outgoingMergedReferences,
    IEnumerable<MergedReference> incomingMergedReferences)
  {
    this.InspectingTable = inspectingTable;
    this.OutgoingMergedReferences = outgoingMergedReferences == null || !outgoingMergedReferences.Any<MergedReference>() ? (IReadOnlyCollection<MergedReference>) MergedReferencesInspectionResult.EmptyArray : (IReadOnlyCollection<MergedReference>) outgoingMergedReferences.ToArray<MergedReference>();
    this.IncomingMergedReferences = incomingMergedReferences == null || !incomingMergedReferences.Any<MergedReference>() ? (IReadOnlyCollection<MergedReference>) MergedReferencesInspectionResult.EmptyArray : (IReadOnlyCollection<MergedReference>) incomingMergedReferences.ToArray<MergedReference>();
    this.ReferencesInspectionResult = new ReferencesInspectionResult(inspectingTable, this.OutgoingMergedReferences.Select<MergedReference, Reference>((Func<MergedReference, Reference>) (r => r.Reference)), this.IncomingMergedReferences.Select<MergedReference, Reference>((Func<MergedReference, Reference>) (r => r.Reference)));
  }

  public ReferencesInspectionResult ReferencesInspectionResult { get; }

  public System.Type InspectingTable { get; }

  public IReadOnlyCollection<MergedReference> OutgoingMergedReferences { get; }

  public IReadOnlyCollection<MergedReference> IncomingMergedReferences { get; }
}
