// Decompiled with JetBrains decompiler
// Type: PX.Data.ColumnsNotInAggregatesCollector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// The visitor of the SQL expressions tree used to collect <see cref="T:PX.Data.SQLTree.Column" /> SQL expressions from a query/subquery that can be wrapped in an aggregate SQL function.
/// This is used for example for results fields in GI with grouping.
/// </summary>
internal class ColumnsNotInAggregatesCollector : SQLTreeTraversalVisitor<List<Column>>
{
  protected override List<Column> DefaultResult => new List<Column>();

  public override List<Column> Visit(SQLExpression sqlExpression)
  {
    return sqlExpression.IsAggregate() ? this.DefaultResult : base.Visit(sqlExpression);
  }

  public override List<Column> Visit(Column column)
  {
    return new List<Column>() { column };
  }

  protected override List<Column> CombineResult(
    List<Column> collectedColumns,
    List<Column> newColumns)
  {
    if (collectedColumns == null || collectedColumns.Count == 0)
      return newColumns;
    if (newColumns == null || newColumns.Count == 0)
      return collectedColumns;
    foreach (Column newColumn in newColumns)
      collectedColumns.Add(newColumn);
    return collectedColumns;
  }
}
