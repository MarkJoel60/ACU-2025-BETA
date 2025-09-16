// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Inspecting.ITableReferenceInspector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Inspecting;

/// <summary>
/// Performs inspection of <see cref="T:PX.Data.IBqlTable" /> referential relationship.
/// </summary>
[PXInternalUseOnly]
public interface ITableReferenceInspector
{
  /// <summary>
  /// Indicates whether the process of inspecting <see cref="T:PX.Data.ReferentialIntegrity.Reference" />s is already completed.
  /// </summary>
  Task AllReferencesAreInspected { get; }

  /// <summary>
  /// Gets information about referential relationship between all <see cref="T:PX.Data.IBqlTable" />s
  /// that are included in referential integrity check.
  /// </summary>
  IReadOnlyDictionary<System.Type, ReferencesInspectionResult> GetReferencesOfAllDacs();

  /// <summary>
  /// Gets information about referential relationship between the given <see cref="T:PX.Data.IBqlTable" />
  /// that is included in referential integrity check.
  /// </summary>
  /// <param name="bqlTable">Inspecting <see cref="T:PX.Data.IBqlTable" /></param>
  ReferencesInspectionResult GetReferencesOf(System.Type bqlTable);

  /// <summary>
  /// Gets information about referential relationship between the given <see cref="T:PX.Data.IBqlTable" />
  /// and other <see cref="T:PX.Data.IBqlTable" />s that are included in referential integrity check.
  /// </summary>
  ReferencesInspectionResult GetReferencesOf<TTable>() where TTable : IBqlTable;
}
