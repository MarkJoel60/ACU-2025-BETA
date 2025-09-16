// Decompiled with JetBrains decompiler
// Type: PX.Data.Or`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Appends a single condition to a conditional expression via logical "or".
/// </summary>
/// <typeparam name="Operand">The compared operand.</typeparam>
/// <typeparam name="Comparison">The comparison operator.</typeparam>
/// <example><para>The code below shows a data view and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Table1.field1, IsNull,
///         Or&lt;Table1.field1, Equal&lt;Zero&gt;&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE ( Table1.Field1 IS NULL OR Table1.Field1 = 0 )</code>
/// </example>
public sealed class Or<Operand, Comparison> : Or<Operand, Comparison, BqlNone>
  where Operand : IBqlOperand
  where Comparison : IBqlComparison, new()
{
}
