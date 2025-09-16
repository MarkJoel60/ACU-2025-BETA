// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// The interface that provides instance-level access to static primary key (PK) API of a certain table.
/// </summary>
public interface IPrimaryKey
{
  /// <summary>
  /// Finds an entity by its keys with a possibility to alter the fetch process.
  /// </summary>
  IBqlTable Find(PXGraph graph, PKFindOptions options, params object[] keys);

  /// <summary>
  /// Finds an entity by the item's keys with a possibility to alter the fetch process.
  /// </summary>
  IBqlTable Find(PXGraph graph, IBqlTable item, PKFindOptions options = PKFindOptions.None);

  /// <summary>Stores the item in the query cache results.</summary>
  void StoreResult(PXGraph graph, IBqlTable item, bool forDirtySelect = false);
}
