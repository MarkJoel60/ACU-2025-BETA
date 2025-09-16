// Decompiled with JetBrains decompiler
// Type: PX.Data.Not`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Adds logical "not" to a conditional expression of a BQL statement. In the resulting SQL,
/// the group is preceded with NOT and surrounded by brackets.
/// </summary>
/// <typeparam name="Operand">The compared operand.</typeparam>
/// <typeparam name="Comparison">The comparison operator.</typeparam>
/// <typeparam name="NextOperator">The next conditional expression, <tt>And</tt>,
/// <tt>And2</tt>, <tt>Or</tt>, or <tt>Or2</tt> class.</typeparam>
/// <example>
/// The code below shows the defition of a data view and the
/// corresponding SQL query.
/// <code>
/// public PXSelect&lt;Table1,
///     Where&lt;Table1.field1, IsNotNull,
///         And&lt;Not&lt;Table1.field2, Equal&lt;Zero&gt;,
///             Or&lt;Table1.field2, Greater&lt;Table1.field1&gt;&gt;&gt;&gt;&gt;&gt; records;
/// </code>
/// <code lang="SQL">
/// SELECT * FROM Table1
/// WHERE ( Table1.Field1 IS NOT NULL
///     AND NOT (Table1.Field2 = 0 OR Table1.Field2 &gt; Table1.Field1) )
/// </code>
/// </example>
public sealed class Not<Operand, Comparison, NextOperator> : IBqlUnary, IBqlCreator, IBqlVerifier
  where Operand : IBqlOperand
  where Comparison : IBqlComparison, new()
  where NextOperator : IBqlBinary, new()
{
  private IBqlCreator _operand;
  private IBqlComparison _comparison;
  private IBqlBinary _next;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    int num = !BqlFunction.getValue<Operand>(ref this._operand, cache, item, pars, ref result, out value) ? 1 : 0;
    if (this._comparison == null)
      this._comparison = (IBqlComparison) new Comparison();
    this._comparison.Verify(cache, item, pars, ref result, ref value);
    if (result.HasValue)
    {
      ref bool? local = ref result;
      bool? nullable1 = result;
      bool? nullable2 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
      local = nullable2;
    }
    if (this._next == null)
      this._next = (IBqlBinary) new NextOperator();
    this._next.Verify(cache, item, pars, ref result, ref value);
    if (num == 0)
      return;
    result = new bool?();
    value = (object) null;
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    if (info.Fields is BqlCommand.EqualityList fields)
      fields.NonStrict = true;
    SQLExpression exp1 = SQLExpression.None();
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
    if (this._comparison == null)
      this._comparison = (IBqlComparison) new Comparison();
    bool flag2 = flag1 & this._comparison.AppendExpression(ref exp1, graph, info, selection);
    if (this._next == null)
      this._next = (IBqlBinary) new NextOperator();
    bool flag3 = flag2 & this._next.AppendExpression(ref exp1, graph, info, selection);
    if (info.BuildExpression)
      exp = SQLExpression.Not(exp1);
    return flag3;
  }
}
