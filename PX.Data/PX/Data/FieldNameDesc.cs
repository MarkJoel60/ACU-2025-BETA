// Decompiled with JetBrains decompiler
// Type: PX.Data.FieldNameDesc
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class FieldNameDesc : IBqlSortColumn, IBqlCreator, IBqlVerifier
{
  private FieldNameParam _parameter;

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this._parameter == null)
      this._parameter = new FieldNameParam();
    this._parameter.Verify(cache, item, pars, ref result, ref value);
  }

  public void AppendQuery(
    Query query,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    info.SortColumns?.Add((IBqlSortColumn) this);
    if (this._parameter == null)
      this._parameter = new FieldNameParam();
    SQLExpression exp = (SQLExpression) null;
    this._parameter.AppendExpression(ref exp, graph, info, selection);
    if (exp == null || !info.BuildExpression)
      return;
    query.OrderDesc(exp);
  }

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

  public System.Type GetReferencedType() => (System.Type) null;

  public bool IsDescending => true;
}
