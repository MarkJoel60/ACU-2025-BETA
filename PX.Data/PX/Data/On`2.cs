// Decompiled with JetBrains decompiler
// Type: PX.Data.On`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Specifies a single joining condition for a JOIN clause. Corresponds to SQL keyword ON.
/// </summary>
/// <typeparam name="Operand">The compared operand.</typeparam>
/// <typeparam name="Comparison">The comparison operator.</typeparam>
/// <example><para>Below is an example of a data view with On and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelectJoin&lt;Table1,
///     InnerJoin&lt;Table2, On&lt;Table2.field2, Equal&lt;Table1.field1&gt;&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// INNER JOIN Table2 ON Table2.Field2 = Table1.Field1</code>
/// </example>
public sealed class On<Operand, Comparison> : OnBase
  where Operand : IBqlOperand
  where Comparison : IBqlComparison, new()
{
  private IBqlCreator _operand;
  private IBqlComparison _comparison;

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
    if (!flag)
      return;
    result = new bool?();
    value = (object) null;
  }

  public override bool AppendJoinExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag = true;
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
      flag &= this._operand.AppendExpression(ref exp1, graph, info, selection);
    }
    if (info.BuildExpression)
      exp = exp1 ?? SQLExpression.None();
    if (this._comparison == null)
      this._comparison = (IBqlComparison) new Comparison();
    return flag & this._comparison.AppendExpression(ref exp, graph, info, selection);
  }

  /// <exclude />
  public override IBqlUnary GetMatchingWhere() => (IBqlUnary) new Where<Operand, Comparison>();
}
