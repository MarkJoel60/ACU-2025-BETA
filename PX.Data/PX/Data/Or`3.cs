// Decompiled with JetBrains decompiler
// Type: PX.Data.Or`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Appends a single condition to a conditional expression via logical "or"
/// and continues the chain of conditions.
/// </summary>
/// <typeparam name="Operand">The compared operand.</typeparam>
/// <typeparam name="Comparison">The comparison operator.</typeparam>
/// <typeparam name="NextOperator">The next conditional expression, <tt>And</tt>,
/// <tt>And2</tt>, <tt>Or</tt>, or <tt>Or2</tt> class.</typeparam>
/// <example><para>The code below shows a data view and the corresponding SQL query.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;Table1,
///     Where&lt;Table1.field1, IsNull,
///         Or&lt;Table1.field1, Equal&lt;Zero&gt;,
///         Or&lt;Table1.field1, Greater&lt;Table1.field2&gt;&gt;&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM Table1
/// WHERE ( Table1.Field1 IS NULL
///     OR Table1.Field1 = 0
///     OR Table1.Field1 &gt; Table1.Field2 )</code>
/// </example>
public class Or<Operand, Comparison, NextOperator> : 
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
    if (this.ensureNext() != null)
      this._next.Verify(cache, item, pars, ref result, ref value);
    bool? nullable2 = nullable1;
    bool flag2 = true;
    if (nullable2.GetValueOrDefault() == flag2 & nullable2.HasValue)
    {
      result = new bool?(true);
    }
    else
    {
      if (!flag1)
      {
        if (nullable1.HasValue)
          return;
        nullable2 = result;
        bool flag3 = false;
        if (!(nullable2.GetValueOrDefault() == flag3 & nullable2.HasValue))
          return;
      }
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
    if (info.Fields is BqlCommand.EqualityList fields)
      fields.NonStrict = true;
    SQLExpression exp1 = (SQLExpression) null;
    if (info.BuildExpression)
      exp1 = SQLExpression.None();
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
    if (this.ensureNext() != null)
      flag2 &= this._next.AppendExpression(ref exp1, graph, info, selection);
    if (info.BuildExpression)
      exp = exp.Or(exp1);
    if (info.Parameters != null)
    {
      foreach (IBqlParameter parameter in info.Parameters)
        parameter.NullAllowed = true;
    }
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
  public bool IsConjunction() => false;

  /// <exclude />
  public IBqlBinary GetNextPredicate() => this.ensureNext();

  /// <exclude />
  public bool ContainsOperandWithComparison() => true;
}
