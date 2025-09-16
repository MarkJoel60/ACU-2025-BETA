// Decompiled with JetBrains decompiler
// Type: PX.Data.Having`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// The class that is used to filter the results of an aggregated query.
/// The class corresponds to the <tt>HAVING</tt> SQL clause.
/// </summary>
/// <typeparam name="TCondition">The filtering condition for the aggregated result.</typeparam>
/// <example>
/// <code lang="CS">PXSelectGroupBy&lt;Detail,
///     Aggregate&lt;GroupBy&lt;Detail.docType, GroupBy&lt;Detail.documentID, Sum&lt;Detail.qty&gt;&gt;&gt;,
///         Having&lt;Detail.qty.Summarized.IsGreaterEqual&lt;Zero&gt;&gt;&gt;&gt;</code>
/// This BQL statement groups records of the <tt>Detail</tt> table
/// by the <tt>Detail.docType</tt> and <tt>Detail.documentID</tt> fields,
/// calculates the sums of the <tt>Detail.qty</tt> field in each group,
/// and displays only those groups that have these sums positive.
/// Note that the <tt>Having</tt> clause requires parameters in FBQL format.</example>
public sealed class Having<TCondition> : IBqlHaving, IBqlCreator, IBqlVerifier where TCondition : IBqlHavingCondition, new()
{
  public IBqlUnary Condition { get; } = (IBqlUnary) new HavingConditionWrapper<TCondition>();

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this.Condition.Verify(cache, item, pars, ref result, ref value);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this.Condition.AppendExpression(ref exp, graph, info, selection);
  }
}
