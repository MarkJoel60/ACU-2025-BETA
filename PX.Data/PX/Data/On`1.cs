// Decompiled with JetBrains decompiler
// Type: PX.Data.On`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Specifies the joining condition for a JOIN clause. Corresponds to SQL keyword ON.
/// </summary>
/// <typeparam name="Operator">The conditional expression, the <tt>Not</tt>,
/// <tt>Where</tt>, or <tt>Where2</tt> class.</typeparam>
/// <example><para>Below is an example of a data view with On and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelectJoin&lt;Table1,
///     InnerJoin&lt;Table2, On&lt;Not&lt;Table2.field2, Equal&lt;Table1.field1&gt;&gt;&gt;&gt;&gt;
///     records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// INNER JOIN Table2 ON NOT (Table2.Field2 = Table1.Field1)</code>
/// </example>
public sealed class On<Operator> : OnBase where Operator : IBqlUnary, new()
{
  private IBqlUnary _operator;

  /// <exclude />
  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this._operator == null)
      this._operator = (IBqlUnary) new Operator();
    this._operator.Verify(cache, item, pars, ref result, ref value);
  }

  /// <exclude />
  public override IBqlUnary GetMatchingWhere() => (IBqlUnary) new Where<Operator>();

  public override bool AppendJoinExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (this._operator == null)
      this._operator = (IBqlUnary) new Operator();
    return (1 & (this._operator.AppendExpression(ref exp, graph, info, selection) ? 1 : 0)) != 0;
  }
}
