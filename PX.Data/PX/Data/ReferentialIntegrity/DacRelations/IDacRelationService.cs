// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.DacRelations.IDacRelationService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.ReferentialIntegrity.DacRelations;

/// <summary>Provides information about relations between DACs.</summary>
[PXInternalUseOnly]
public interface IDacRelationService
{
  /// <summary>
  /// Returns relation information for a particular DAC type specified by <paramref name="table" />.
  /// </summary>
  /// <param name="table">DAC type</param>
  /// <returns>The information about all incoming and outgoing relations for a particular DAC.</returns>
  TableRelations GetTableRelations(System.Type table);
}
