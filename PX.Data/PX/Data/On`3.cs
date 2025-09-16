// Decompiled with JetBrains decompiler
// Type: PX.Data.On`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Specifies a single joining condition for a JOIN clause and allows
/// continuing the chain of conditions using a logical operator.
/// Corresponds to SQL keyword ON.
/// </summary>
/// <typeparam name="Operand">The compared operand.</typeparam>
/// <typeparam name="Comparison">The comparison operator.</typeparam>
/// <typeparam name="NextOperator">The next conditional expression.</typeparam>
/// <example><para>Below is an example of a data view with On and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelectJoin&lt;Table1,
///     InnerJoin&lt;Table2,
///         On&lt;Table2.field1, Equal&lt;Table1.field2&gt;,
///         And&lt;Table2.field3, Equal&lt;Table1.field4&gt;&gt;&gt;&gt;&gt;
///     records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// INNER JOIN Table2 ON
///     Table2.Field1 = Table1.Field2
///     AND Table2.Field3 = Table1.Field4</code>
/// </example>
public sealed class On<Operand, Comparison, NextOperator> : OnBase
  where Operand : IBqlOperand
  where Comparison : IBqlComparison, new()
  where NextOperator : IBqlBinary, new()
{
  private IBqlCreator _operand;
  private IBqlComparison _comparison;
  private IBqlBinary _next;

  /// <exclude />
  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    bool flag = false;
    if (typeof (IBqlField).IsAssignableFrom(typeof (Operand)))
    {
      flag = !(cache.GetItemType() == BqlCommand.GetItemType(typeof (Operand))) && !BqlCommand.GetItemType(typeof (Operand)).IsAssignableFrom(cache.GetItemType());
      value = flag ? (object) null : cache.GetValue(item, typeof (Operand).Name);
    }
    else
    {
      if (this._operand == null)
        this._operand = this._operand.createOperand<Operand>();
      this._operand.Verify(cache, item, pars, ref result, ref value);
    }
    if (this._comparison == null)
      this._comparison = (IBqlComparison) new Comparison();
    this._comparison.Verify(cache, item, pars, ref result, ref value);
    if (this._next == null)
      this._next = (IBqlBinary) new NextOperator();
    this._next.Verify(cache, item, pars, ref result, ref value);
    if (!flag)
      return;
    result = new bool?();
    value = (object) null;
  }

  /// <exclude />
  public override IBqlUnary GetMatchingWhere()
  {
    return (IBqlUnary) new Where<Operand, Comparison, NextOperator>();
  }

  public override bool AppendJoinExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    SQLExpression exp1 = (SQLExpression) null;
    if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Operand)))
    {
      if (info.BuildExpression)
        exp1 = BqlCommand.GetSingleExpression(typeof (Operand), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
      info.Fields?.Add(typeof (Operand));
    }
    else
    {
      if (this._operand == null)
        this._operand = this._operand.createOperand<Operand>();
      flag1 &= this._operand.AppendExpression(ref exp1, graph, info, selection);
    }
    if (info.BuildExpression)
      exp = exp1 ?? SQLExpression.None();
    if (this._comparison == null)
      this._comparison = (IBqlComparison) new Comparison();
    bool flag2 = flag1 & this._comparison.AppendExpression(ref exp, graph, info, selection);
    if (this._next == null)
      this._next = (IBqlBinary) new NextOperator();
    return flag2 & this._next.AppendExpression(ref exp, graph, info, selection);
  }
}
