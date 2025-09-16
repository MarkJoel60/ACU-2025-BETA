// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlJoin
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using PX.DbServices.QueryObjectModel;

#nullable disable
namespace PX.Data;

/// <summary>A BQL <tt>Join</tt> clause.</summary>
public interface IBqlJoin : IBqlVerifier
{
  /// <summary>Appends the SQL tree query that corresponds to the BQL <tt>Join</tt> clause to a BQL statement.</summary>
  /// <param name="query">The SQL tree query to be appended.</param>
  /// <param name="graph">A graph instance.</param>
  /// <param name="info">The information about the BQL command.</param>
  void AppendQuery(
    Query query,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection,
    IBqlFunction[] aggregates,
    System.Type outerTable = null);

  /// <summary>Returns the next BQL <tt>Join</tt> statement.</summary>
  /// <returns>An <see cref="T:PX.Data.IBqlJoin">IBqlJoin</see> type.</returns>
  IBqlJoin getNextJoin();

  /// <summary>Returns the joined table.</summary>
  /// <returns>An <see cref="T:PX.Data.IBqlTable">IBqlTable</see> type.</returns>
  System.Type getJoinedTable();

  /// <summary>Returns the BQL <tt>Join</tt> condition.</summary>
  /// <returns>A <see cref="T:PX.Data.IBqlUnary">IBqlUnary</see> type.</returns>
  IBqlUnary getJoinCondition();

  /// <summary>Returns the yaql type that corresponds to the BQL <tt>Join</tt> type.</summary>
  YaqlJoinType getJoinType();
}
