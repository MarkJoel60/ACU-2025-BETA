// Decompiled with JetBrains decompiler
// Type: PX.Data.Avg`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Returns the average of the values of <tt>Field</tt> in a group and
/// continues the aggregation clause with <tt>NextAggregate</tt>.
/// Equivalent to SQL function AVG.
/// </summary>
/// <typeparam name="Field">The field the function is applied to.</typeparam>
/// <typeparam name="NextAggregate">The next <tt>GroupBy</tt> clause or
/// aggregate function.</typeparam>
/// <example><para>The following BQL statement groups Table records by the Table1.field2 field and calculates average values of the Table1.field1 field in each group. The corresponding SQL query is given below.</para>
/// <code title="Example" lang="CS">
/// PXSelectGroupBy&lt;Table1,
///     Aggregate&lt;Avg&lt;Table1.field1, GroupBy&lt;Table1.field2&gt;&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT AVG(Table1.Field1), Table1.Field2,
///        [MAX(Table1.Field) or NULL for other fields]
/// FROM Table1
/// GROUP BY Table1.Field2</code>
/// </example>
public sealed class Avg<Field, NextAggregate> : AggregatedFnBase<Field, NextAggregate>
  where Field : IBqlField
  where NextAggregate : IBqlFunction, new()
{
  /// <exclude />
  public override string GetFunction() => "AVG";
}
