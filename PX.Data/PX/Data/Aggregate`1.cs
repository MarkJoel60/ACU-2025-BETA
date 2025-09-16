// Decompiled with JetBrains decompiler
// Type: PX.Data.Aggregate`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// A wrapper clause for the <tt>GroupBy</tt> clauses and aggregation functions.
/// </summary>
/// <typeparam name="TFunctions">The <tt>GroupBy</tt> class or aggregation function.</typeparam>
/// <example><para>The following BQL statement groups the records of the Table table by the Table.field1 field and calculates the sums of the Table.field2 field in each group.</para>
/// <code title="Example" lang="CS">
/// PXSelectGroupBy&lt;Table,
///     Aggregate&lt;GroupBy&lt;Table.field1, Sum&lt;Table.field2&gt;&gt;&gt;&gt;</code>
/// <code title="Example2" description="This is translated into the following SQL code." groupname="Example" lang="SQL">
/// SELECT Table.Field1, SUM(Table.Field2),
///        [MAX(Table.Field) or NULL for other fields]
/// FROM Table
/// GROUP BY Table.Field1</code>
/// </example>
public sealed class Aggregate<TFunctions> : AggregateBase<TFunctions, BqlNone> where TFunctions : IBqlFunction, new()
{
}
