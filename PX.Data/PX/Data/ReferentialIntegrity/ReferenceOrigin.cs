// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.ReferenceOrigin
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.ReferentialIntegrity;

/// <summary>
/// Indicates how referential relationship between <see cref="T:PX.Data.IBqlTable" />s is achieved
/// </summary>
[PXInternalUseOnly]
public enum ReferenceOrigin
{
  /// <summary>
  /// Referential relationship between <see cref="T:PX.Data.IBqlTable" />s is achieved by a <a href="https://wiki.acumatica.com/display/TND/Primary+and+Foreign+Key+API">Foreign Key API</a>.
  /// </summary>
  ForeignKeyApi,
  /// <summary>
  /// Referential relationship between <see cref="T:PX.Data.IBqlTable" />s is achieved by a <see cref="T:PX.Data.PXParentAttribute" />.
  /// </summary>
  ParentAttribute,
  /// <summary>
  /// Referential relationship between <see cref="T:PX.Data.IBqlTable" />s is achieved by a <see cref="T:PX.Data.ReferentialIntegrity.Attributes.PXForeignReferenceAttribute" />.
  /// </summary>
  DeclareReferenceAttribute,
  /// <summary>
  /// Referential relationship between <see cref="T:PX.Data.IBqlTable" />s is achieved by a <see cref="T:PX.Data.PXSelectorAttribute" />.
  /// </summary>
  SelectorAttribute,
  /// <summary>
  /// Referential relationship between <see cref="T:PX.Data.IBqlTable" />s is achieved by a <see cref="T:PX.Data.IBqlWhere" /> declared as a part of a BQL view inside a graph.
  /// </summary>
  WhereInCustomSelect,
  /// <summary>
  /// Referential relationship between <see cref="T:PX.Data.IBqlTable" />s is achieved by a <see cref="T:PX.Data.IBqlJoin" /> declared as a part of a BQL view inside a graph.
  /// </summary>
  JoinInCustomSelect,
}
