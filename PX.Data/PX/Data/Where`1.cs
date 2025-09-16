// Decompiled with JetBrains decompiler
// Type: PX.Data.Where`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Specifies an unary operator as the conditional expression for a BQL
/// command.
/// </summary>
/// <typeparam name="UnaryOperator">The conditional expression, <tt>Not</tt>
/// or <tt>Match</tt> class.</typeparam>
/// <example><para>The code below shows a data view and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Not&lt;Table1.field1, IsNotNull,
///               And&lt;Table1.field2, LessEqual&lt;Table1.field1&gt;&gt;&gt;&gt;&gt;
///     records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE NOT ( Table1.Field1 IS NOT NULL
///             AND Table1.Field2 &lt;= Table1.Field1 )</code>
/// </example>
public sealed class Where<UnaryOperator> : WhereBase<UnaryOperator, BqlNone> where UnaryOperator : IBqlUnary, new()
{
  /// <exclude />
  public Where()
  {
  }

  /// <exclude />
  public Where(IBqlUnary un)
    : base(un, (IBqlBinary) null)
  {
  }

  /// <exclude />
  public override bool UseParenthesis() => true;
}
