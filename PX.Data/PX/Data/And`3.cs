// Decompiled with JetBrains decompiler
// Type: PX.Data.And`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Appends a single condition to a conditional expression via logical "and"
/// and continues the chain of conditions.
/// </summary>
/// <typeparam name="Operand">The compared operand.</typeparam>
/// <typeparam name="Comparison">The comparison operator.</typeparam>
/// <typeparam name="NextOperator">The next conditional expression, <tt>And</tt>,
/// <tt>And2</tt>, <tt>Or</tt>, or <tt>Or2</tt> class.</typeparam>
/// <example><para>The code below shows a data view and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Table1.field1, IsNotNull,
///         And&lt;Table1.field2, Greater&lt;Table1.field1&gt;,
///         Or&lt;Table1.field1, IsNull&gt;&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE ( Table1.Field1 IS NOT NULL
///     AND Table1.Field2 &gt; Table1.Field1
///     OR Table1.Field1 IS NULL )</code>
/// </example>
public class And<Operand, Comparison, NextOperator> : 
  IBqlBinary,
  IBqlCreator,
  IBqlVerifier,
  IBqlPredicateChain
  where Operand : IBqlOperand
  where Comparison : IBqlComparison, new()
  where NextOperator : IBqlBinary, new()
{
  private IBqlCreator _operand;
  private IBqlComparison _comparison;
  private IBqlBinary _next;

  protected void ensureOperand()
  {
    if (this._operand != null)
      return;
    this._operand = this._operand.createOperand<Operand>();
  }

  protected IBqlComparison ensureComparison()
  {
    IBqlComparison comparison = this._comparison;
    if (comparison != null)
      return comparison;
    return !(typeof (Comparison) == typeof (BqlNone)) ? (this._comparison = (IBqlComparison) new Comparison()) : (IBqlComparison) null;
  }

  protected IBqlBinary ensureNext()
  {
    IBqlBinary next = this._next;
    if (next != null)
      return next;
    return !(typeof (NextOperator) == typeof (BqlNone)) ? (this._next = (IBqlBinary) new NextOperator()) : (IBqlBinary) null;
  }

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    bool? nullable1 = result;
    bool flag1 = !BqlFunction.getValue<Operand>(ref this._operand, cache, item, pars, ref result, out value);
    this.ensureComparison();
    this._comparison.Verify(cache, item, pars, ref result, ref value);
    bool? nullable2 = nullable1;
    bool flag2 = false;
    if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
      result = new bool?(false);
    if (this.ensureNext() != null)
    {
      this._next.Verify(cache, item, pars, ref result, ref value);
      if (!this._next.IsConjunction())
      {
        bool? nullable3 = result;
        bool flag3 = false;
        if (!(nullable3.GetValueOrDefault() == flag3 & nullable3.HasValue))
          return;
      }
    }
    if (!flag1)
      return;
    bool? nullable4 = nullable1;
    bool flag4 = false;
    if (nullable4.GetValueOrDefault() == flag4 & nullable4.HasValue)
    {
      result = new bool?(false);
    }
    else
    {
      result = new bool?();
      value = (object) null;
    }
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    SQLExpression exp1 = SQLExpression.None();
    if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Operand)))
    {
      if (info.BuildExpression)
        exp1 = BqlCommand.GetSingleExpression(typeof (Operand), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
      info.Fields?.Add(typeof (Operand));
    }
    else
    {
      this.ensureOperand();
      flag1 &= this._operand.AppendExpression(ref exp1, graph, info, selection);
    }
    this.ensureComparison();
    if (exp1 == null && info.BuildExpression)
      exp1 = SQLExpression.None();
    bool flag2 = flag1 & this._comparison.AppendExpression(ref exp1, graph, info, selection);
    if (info.BuildExpression)
      exp = exp.And(exp1);
    if (this.ensureNext() != null)
      flag2 &= this._next.AppendExpression(ref exp, graph, info, selection);
    return flag2;
  }

  /// <exclude />
  public IBqlUnary GetUnary()
  {
    return !(typeof (NextOperator) == typeof (BqlNone)) ? (IBqlUnary) new WhereNp<Operand, Comparison, NextOperator>() : this.GetContainedPredicate();
  }

  /// <exclude />
  public IBqlUnary GetContainedPredicate() => (IBqlUnary) new WhereNp<Operand, Comparison>();

  /// <exclude />
  public bool UseParenthesis() => false;

  /// <exclude />
  public bool IsConjunction() => true;

  /// <exclude />
  public IBqlBinary GetNextPredicate() => this.ensureNext();

  /// <exclude />
  public bool ContainsOperandWithComparison() => true;
}
