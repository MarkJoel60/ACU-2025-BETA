// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Merging.IReferenceMerger
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.ReferentialIntegrity.Inspecting;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Merging;

/// <summary>
/// Performs merging of similar <see cref="T:PX.Data.ReferentialIntegrity.Reference" />s by weighting
/// their corresponding <see cref="T:PX.Data.IBqlTable" />s and deciding which of them is more primary.
/// </summary>
[PXInternalUseOnly]
public interface IReferenceMerger
{
  IReadOnlyDictionary<System.Type, MergedReferencesInspectionResult> MergeReferences(
    IReadOnlyDictionary<System.Type, ReferencesInspectionResult> collectedReferences);

  /// <summary>
  /// Returns the "primary" <see cref="T:PX.Data.IBqlTable" /> type suggested by the merging algorithm for the
  /// original <see cref="T:PX.Data.IBqlTable" /> type as a replacement in the references.
  /// </summary>
  /// <param name="originalType">The original type of <see cref="T:PX.Data.IBqlTable" />.</param>
  /// <returns>The "primary" <see cref="T:PX.Data.IBqlTable" /> type suggested by the merging algorithm.</returns>
  System.Type GetSuggestedType(System.Type originalType);
}
