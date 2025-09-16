// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Inspecting.ITableReferenceCollector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Inspecting;

[PXInternalUseOnly]
public interface ITableReferenceCollector
{
  /// <summary>
  /// Indicates whether the process of collecting <see cref="T:PX.Data.ReferentialIntegrity.Reference" />s is already completed.
  /// </summary>
  Task AllReferencesAreCollected { get; }

  /// <summary>
  /// Tries to collect the reference. Returns <see langword="false" /> if collection phase has already been completed.
  /// </summary>
  /// <param name="reference">The reference to be collected</param>
  bool TryCollectReference(Reference reference);

  /// <summary>
  /// Signals that all references have been collected, so no further calls to <see cref="!:CollectReference" /> are accepted.
  /// </summary>
  void CollectionCompleted();
}
