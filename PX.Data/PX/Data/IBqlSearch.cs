// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlSearch
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <summary>A BQL search command.</summary>
public interface IBqlSearch
{
  /// <summary>Returns the field inside the BQL search command.</summary>
  /// <returns>An <see cref="T:PX.Data.IBqlField">IBqlField</see> type.</returns>
  System.Type GetField();

  /// <summary>Returns the name of the field inside the BQL search command.</summary>
  /// <param name="graph">A <see cref="T:PX.Data.PXGraph">PXGraph</see> instance.</param>
  /// <returns>A string with the name of the field.</returns>
  string GetFieldName(PXGraph graph);

  /// <summary>Returns the BQL search command in the internal SQL tree format.</summary>
  /// <param name="graph">A <see cref="T:PX.Data.PXGraph">PXGraph</see> instance.</param>
  /// <param name="info">The information about the BQL search command.</param>
  /// <returns>The query in internal SQL tree format.</returns>
  Query GetQueryInternal(PXGraph graph, BqlCommandInfo info, BqlCommand.Selection selection);

  /// <summary>Returns an SQL tree expression of the field inside the BQL search command.</summary>
  /// <param name="graph">A <see cref="T:PX.Data.PXGraph">PXGraph</see> instance.</param>
  /// <returns>An SQL tree expression.</returns>
  SQLExpression GetFieldExpression(PXGraph graph);

  /// <summary>Returns an SQL tree expression of the where inside the BQL search command.</summary>
  /// <param name="graph">A <see cref="T:PX.Data.PXGraph">PXGraph</see> instance.</param>
  /// <returns>An SQL tree expression.</returns>
  SQLExpression GetWhereExpression(PXGraph graph);
}
