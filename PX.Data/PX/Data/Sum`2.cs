// Decompiled with JetBrains decompiler
// Type: PX.Data.Sum`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Returns the sum of all <tt>Field</tt> values in a group and continues the aggregation
/// clause with <tt>NextAggregate</tt>. Equivalent to SQL function SUM.
/// </summary>
/// <typeparam name="Field">The field the function is applied to.</typeparam>
/// <typeparam name="NextAggregate">The next <tt>GroupBy</tt> clause or
/// aggregate function.</typeparam>
/// <example><para>The code below shows a data view that groups data records by one column.</para>
/// <code title="Example" lang="CS">
/// PXSelectGroupBy&lt;Table1,
///     Aggregate&lt;Sum&lt;Table1.field1,
///               Sum&lt;Table1.field2,
///               GroupBy&lt;Table1.field3&gt;&gt;&gt;&gt;&gt; records;</code>
/// <code title="" description="" lang="SQL">
/// SELECT SUM(Table1.Field1), SUM(Table1.Field2), Table.Field3,
///        [MAX(Table1.Field) or NULL for other fields]
/// FROM Table1
/// GROUP BY Table.Field3</code>
/// </example>
public sealed class Sum<Field, NextAggregate> : AggregatedFnBase<Field, NextAggregate>
  where Field : IBqlField
  where NextAggregate : IBqlFunction, new()
{
  /// <exclude />
  public override string GetFunction() => "SUM";
}
