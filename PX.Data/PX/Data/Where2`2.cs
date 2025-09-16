// Decompiled with JetBrains decompiler
// Type: PX.Data.Where2`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// Specifies a complex conditional expression for a BQL command that
/// typically joins two groups of conditions.
/// </summary>
/// <typeparam name="UnaryOperator">The first conditional expression.</typeparam>
/// <typeparam name="NextOperator">The logical operator appending the second
/// conditional expression.</typeparam>
/// <example><para>A filtering expression of the form ((C1 and C2) or (C3 and C4)), where C with a number denotes a single condition, is implemented by the BQL code of the following form:</para>
/// <code title="Example" lang="CS">
/// Where2&lt;Where&lt;C1,
///            And&lt;C2&gt;&gt;,
///     Or&lt;Where&lt;C3,
///            And&lt;C4&gt;&gt;&gt;&gt;</code>
/// <code title="Example2" description="A full expression of this type may look something like this:" groupname="Example" lang="CS">
/// Where2&lt;Where&lt;Table.field2, Greater&lt;Table.field1&gt;
///            And&lt;Table.field3, Between&lt;Table.field1, Table.field2&gt;&gt;&gt;,
///     Or&lt;Where&lt;Table.field3, IsNull,
///            And&lt;Table.field1, Equal&lt;Table.field2&gt;&gt;&gt;&gt;&gt;</code>
/// <code title="Example3" description="This is translated into:" groupname="Example2" lang="SQL">
/// WHERE ( Table.Field2 &gt; Table.Field1
///         AND Table.Field3 BETWEEN Table.Field1 AND Table.Field2 )
///    OR ( Table.Field3 IS NULL
///         AND Table.Field1 = Table.Field2 )</code>
/// </example>
public sealed class Where2<UnaryOperator, NextOperator> : WhereBase<UnaryOperator, NextOperator>
  where UnaryOperator : IBqlUnary, new()
  where NextOperator : IBqlBinary, new()
{
  /// <exclude />
  public Where2()
  {
  }

  /// <exclude />
  public Where2(IBqlUnary un, IBqlBinary next)
    : base(un, next)
  {
  }

  /// <exclude />
  public override bool UseParenthesis() => true;
}
