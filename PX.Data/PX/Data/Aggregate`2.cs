// Decompiled with JetBrains decompiler
// Type: PX.Data.Aggregate`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// A wrapper clause for the <tt>GroupBy</tt> clauses and aggregation functions.
/// It also supports the result filtering by using the <see cref="T:PX.Data.Having`1" /> clause.
/// </summary>
/// <typeparam name="TFunctions">The <tt>GroupBy</tt> class or aggregation function.</typeparam>
/// <typeparam name="THaving">The filtering condition for the result.</typeparam>
/// <example>
/// <code lang="CS">PXSelectGroupBy&lt;Detail,
///     Aggregate&lt;GroupBy&lt;Detail.docType, GroupBy&lt;Detail.documentID, Sum&lt;Detail.qty&gt;&gt;&gt;,
///         Having&lt;Detail.qty.Summarized.IsGreaterEqual&lt;Zero&gt;&gt;&gt;&gt;</code>
/// The following BQL statement groups the records of the <tt>Detail</tt> table
/// by the <tt>Detail.docType</tt> and <tt>Detail.documentID</tt> fields,
/// calculates the sums of the <tt>Detail.qty</tt> field in each group,
/// and displays only those groups for which these sums are positive.
/// Note that the <tt>Having</tt> clause requires parameters in FBQL format.</example>
public sealed class Aggregate<TFunctions, THaving> : AggregateBase<TFunctions, THaving>
  where TFunctions : IBqlFunction, new()
  where THaving : IBqlHaving, new()
{
}
