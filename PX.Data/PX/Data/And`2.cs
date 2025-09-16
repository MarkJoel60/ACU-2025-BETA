// Decompiled with JetBrains decompiler
// Type: PX.Data.And`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Appends a single condition to a conditional expression via logical "and".
/// </summary>
/// <typeparam name="Operand">The compared operand.</typeparam>
/// <typeparam name="Comparison">The comparison operator.</typeparam>
/// <example><para>The following expression selects all records that are not equal to NULL from Table1 -&gt; Filed1 along with the records from Table -&gt; Field2 that are greater than those fetched from the Field1. The following expression will be translated into a SQL query, an example of which is given below.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Table1.field1, IsNotNull,
///         And&lt;Table1.field2, Greater&lt;Table1.field1&gt;&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE ( Table1.Field1 IS NOT NULL
///     AND Table1.Field2 &gt; Table1.Field1)</code>
/// </example>
public sealed class And<Operand, Comparison> : And<Operand, Comparison, BqlNone>
  where Operand : IBqlOperand
  where Comparison : IBqlComparison, new()
{
}
