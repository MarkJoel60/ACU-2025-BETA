// Decompiled with JetBrains decompiler
// Type: PX.Data.GroupBy`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Adds grouping by the field specified in <tt>Field</tt> and continues the aggregation clause.
/// Equivalent to SQL operator GROUP BY.
/// </summary>
/// <typeparam name="Field">The field by which the data records are grouped.</typeparam>
/// <typeparam name="NextAggregate">The next <tt>GroupBy</tt> clause or
/// aggregate function.</typeparam>
/// <example><para>The following BQL statement groups Table records by the Table1.field1 field and calculates sums of the Table1.field2 field in each group. The corresponding SQL query is given below.</para>
/// <code title="Example" lang="CS">
/// PXSelectGroupBy&lt;Table1,
///     Aggregate&lt;GroupBy&lt;Table1.field1, Sum&lt;Table1.field2&gt;&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT Table1.Field1, SUM(Table1.Field2),
///        [MAX(Table1.Field) or NULL for other fields]
/// FROM Table1
/// GROUP BY Table1.Field1</code>
/// </example>
public sealed class GroupBy<Field, NextAggregate> : GroupByBase<Field, NextAggregate>
  where Field : IBqlOperand
  where NextAggregate : IBqlFunction, new()
{
}
