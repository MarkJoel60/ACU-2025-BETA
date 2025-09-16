// Decompiled with JetBrains decompiler
// Type: PX.Data.OrderBy`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <summary>
/// The clause for specifying how to order the result set of a BQL statetement, equivalent to the SQL clause ORDER BY.
/// </summary>
/// <typeparam name="List">The fields to sort by, <tt>Asc</tt> or <tt>Desc</tt>
/// class.</typeparam>
/// <example><para>An example of a data view with OrderBy and the corresponding SQL query are given below.</para>
/// <code title="]" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Table1.field1, IsNotNull&gt;,
///     OrderBy&lt;Asc&lt;Table1.field1&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE Table1.Field1 IS NOT NULL
/// ORDER BY Table1.Field1</code>
/// </example>
public sealed class OrderBy<List> : IBqlOrderBy, IBqlCreator, IBqlVerifier where List : IBqlSortColumn, new()
{
  private IBqlSortColumn _list;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    System.Collections.Generic.List<object> pars,
    ref bool? result,
    ref object value)
  {
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (this._list == null)
      this._list = (IBqlSortColumn) new List();
    return this._list.AppendExpression(ref exp, graph, info, selection);
  }

  /// <exclude />
  public void AppendQuery(
    Query query,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (this._list == null)
      this._list = (IBqlSortColumn) new List();
    this._list.AppendQuery(query, graph, info, selection);
  }
}
