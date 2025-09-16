// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLinqBqlSortColumn
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.SQLTree;

internal class SQLinqBqlSortColumn : IBqlSortColumn, IBqlCreator, IBqlVerifier
{
  private readonly OrderSegment _orderSegment;

  public SQLinqBqlSortColumn(OrderSegment orderSegment) => this._orderSegment = orderSegment;

  public bool IsDescending => !this._orderSegment.ascending_;

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    Query query = new Query();
    this.AppendQuery(query, graph, info, selection);
    return query.IsOK();
  }

  public void AppendQuery(
    Query query,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    info.SortColumns?.Add((IBqlSortColumn) this);
    query?.AddOrderSegment(this._orderSegment.Duplicate());
  }

  public System.Type GetReferencedType() => (System.Type) null;

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
  }
}
