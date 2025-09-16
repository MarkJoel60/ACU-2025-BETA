// Decompiled with JetBrains decompiler
// Type: PX.Data.Sum`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.SQLTree;

#nullable disable
namespace PX.Data;

/// <summary>
/// Returns the sum of all <tt>Field</tt> values in a group.
/// Equivalent to SQL function SUM.
/// </summary>
/// <typeparam name="Field">The field the function is applied to.</typeparam>
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
public sealed class Sum<Field> : 
  AggregatedFnBase<Field, BqlNone>,
  IBqlSimpleAggregator,
  IBqlFunction,
  IBqlCreator,
  IBqlVerifier,
  IBqlFunctionExt
  where Field : IBqlField
{
  /// <exclude />
  public override string GetFunction() => "SUM";

  public SQLExpression.Operation Operation => SQLExpression.Operation.SUM;
}
