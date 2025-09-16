// Decompiled with JetBrains decompiler
// Type: PX.Data.And`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Appends a unary operator to a conditional expression via logical "and".
/// </summary>
/// <typeparam name="Operator">The unary conditional expression, <tt>Not</tt>,
/// <tt>Where</tt>, <tt>Where2</tt>, or <tt>Match</tt></typeparam>
/// <example><para>The code below shows a data view and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Table1.field1, IsNotNull,
///         And&lt;Not&lt;Table1.field2, Equal&lt;Zero&gt;&gt;&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE ( Table1.Field1 IS NOT NULL
///     AND NOT ( Table1.Field2 = 0 ) )</code>
/// </example>
public sealed class And<Operator> : BqlPredicateBinaryBase<Operator, BqlNone> where Operator : IBqlUnary, new()
{
  protected override string SqlOperator => "AND";

  protected override bool BypassedValue => false;

  /// <exclude />
  public And(IBqlUnary op)
    : base(op)
  {
  }

  /// <exclude />
  public And()
    : base((IBqlUnary) null)
  {
  }
}
