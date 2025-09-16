// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.IPrimaryKeyOf`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// The interface that provides typed instance-level access to static primary key (PK) API of a certain table.
/// </summary>
public interface IPrimaryKeyOf<TTable> : IPrimaryKey where TTable : IBqlTable
{
  /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.PKFindOptions,System.Object[])" />
  TTable Find(PXGraph graph, PKFindOptions options, params object[] keys);

  /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.Find(PX.Data.PXGraph,PX.Data.IBqlTable,PX.Data.PKFindOptions)" />
  TTable Find(PXGraph graph, TTable item, PKFindOptions options = PKFindOptions.None);

  /// <inheritdoc cref="M:PX.Data.ReferentialIntegrity.Attributes.IPrimaryKey.StoreResult(PX.Data.PXGraph,PX.Data.IBqlTable,System.Boolean)" />
  void StoreResult(PXGraph graph, TTable item, bool forDirtySelect = false);
}
