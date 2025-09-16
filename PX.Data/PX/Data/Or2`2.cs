// Decompiled with JetBrains decompiler
// Type: PX.Data.Or2`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Appends a unary operator to a conditional expression via logical "or" and continues the chain of conditions.</summary>
/// <typeparam name="Operator">The unary conditional expression <tt>Not</tt>,
/// <tt>Where</tt>, <tt>Where2</tt>, or <tt>Match</tt></typeparam>
/// <typeparam name="NextOperator">The next conditional expression, <tt>And</tt>,
/// <tt>And2</tt>, <tt>Or</tt>, or <tt>Or2</tt> class.</typeparam>
/// <example><para>The code below shows a data view and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Table1.field1, IsNull,
///         Or2&lt;Where&lt;Table1.field1, Equal&lt;Zero&gt;,
///             And&lt;Table1.field2, IsNotNull&gt;&gt;,
///         Or&lt;Table1.field1, Greater&lt;Zero&gt;&gt;&gt;&gt;&gt; records;</code>
/// <code title="" description="" lang="SQL">
/// SELECT * FROM Table1
/// WHERE ( Table1.Field1 IS NULL
///     OR ( Table1.Field1 = 0 AND Table1.Field2 IS NOT NULL )
///     OR Table1.Field1 &gt; 0 )</code>
/// </example>
public sealed class Or2<Operator, NextOperator> : BqlPredicateBinaryBase<Operator, NextOperator>
  where Operator : IBqlUnary, new()
  where NextOperator : IBqlBinary, new()
{
  protected override string SqlOperator => "OR";

  protected override bool BypassedValue => true;
}
