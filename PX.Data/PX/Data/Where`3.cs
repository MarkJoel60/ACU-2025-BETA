// Decompiled with JetBrains decompiler
// Type: PX.Data.Where`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Specifies a particular condition in the two first type parameters and
/// attaches one more logical operator (<tt>And</tt> or <tt>Or</tt>).
/// </summary>
/// <typeparam name="Operand">The compared operand.</typeparam>
/// <typeparam name="Comparison">The comparison operator.</typeparam>
/// <typeparam name="NextOperator">The next conditional expression.</typeparam>
/// <example><para>An example of a data view and the corresponding SQL query are given below.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Table1.field1, Greater&lt;Table1.field2&gt;,
///         And&lt;Table1.field3, IsNull&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE Table1.Field1 &gt; Table1.Field2
///     AND Table.Field3 IS NULL</code>
/// </example>
public sealed class Where<Operand, Comparison, NextOperator> : 
  WhereBase<Operand, Comparison, NextOperator>
  where Operand : IBqlOperand
  where Comparison : IBqlComparison, new()
  where NextOperator : IBqlBinary, new()
{
  /// <exclude />
  public override bool UseParenthesis() => true;
}
