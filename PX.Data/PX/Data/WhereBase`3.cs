// Decompiled with JetBrains decompiler
// Type: PX.Data.WhereBase`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class WhereBase<Operand, Comparison, NextOperator> : 
  IBqlWhere,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier,
  IBqlPredicateChain
  where Operand : IBqlOperand
  where Comparison : IBqlComparison, new()
  where NextOperator : IBqlBinary, new()
{
  private IBqlCreator _operand;
  private Comparison _comparison;
  private NextOperator _next;

  protected IBqlCreator ensureOperand()
  {
    return this._operand ?? (this._operand = this._operand.createOperand<Operand>());
  }

  protected IBqlComparison ensureComparison()
  {
    if ((object) this._comparison != null)
      return (IBqlComparison) this._comparison;
    return !(typeof (Comparison) == typeof (BqlNone)) ? (IBqlComparison) (this._comparison = new Comparison()) : (IBqlComparison) null;
  }

  protected IBqlBinary ensureNext()
  {
    if ((object) this._next != null)
      return (IBqlBinary) this._next;
    return !(typeof (NextOperator) == typeof (BqlNone)) ? (IBqlBinary) (this._next = new NextOperator()) : (IBqlBinary) null;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    int num = !BqlFunction.getValue<Operand>(ref this._operand, cache, item, pars, ref result, out value) ? 1 : 0;
    this.ensureComparison();
    this._comparison.Verify(cache, item, pars, ref result, ref value);
    if (this.ensureNext() != null)
      this._next.Verify(cache, item, pars, ref result, ref value);
    if (num == 0)
      return;
    result = new bool?();
    value = (object) null;
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    bool[] flagArray = info.Parameters == null ? new bool[0] : info.Parameters.ConvertAll<bool>((Converter<IBqlParameter, bool>) (_ => _.NullAllowed)).ToArray();
    SQLExpression exp1 = (SQLExpression) null;
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
    if (info.BuildExpression)
      exp = exp1 ?? SQLExpression.None();
    this.ensureComparison();
    bool flag2 = flag1 & this._comparison.AppendExpression(ref exp, graph, info, selection);
    if (this.ensureNext() != null)
      flag2 &= this._next.AppendExpression(ref exp, graph, info, selection);
    if (this.UseParenthesis())
      exp?.Embrace();
    for (int index = 0; index < flagArray.Length; ++index)
      info.Parameters[index].NullAllowed = flagArray[index];
    return flag2;
  }

  public IBqlUnary GetContainedPredicate()
  {
    return !(typeof (NextOperator) == typeof (BqlNone)) ? (IBqlUnary) new WhereNp<Operand, Comparison>() : (IBqlUnary) this;
  }

  public override string ToString()
  {
    string str1 = this._operand == null ? typeof (Operand).ToString() : this._operand.ToString();
    string str2 = (object) this._comparison == null ? typeof (Comparison).ToString() : this._comparison.ToString();
    if ((object) this._next == null && typeof (NextOperator) == typeof (BqlNone))
    {
      if (!this.UseParenthesis())
        return $"{str1} {str2}";
      return $"({str1} {str2})";
    }
    string str3 = (object) this._next == null ? typeof (NextOperator).ToString() : this._next.ToString();
    return !this.UseParenthesis() ? $"{str1} {str2} {str3}" : $"({str1} {str2} {str3})";
  }

  public bool ContainsOperandWithComparison() => true;

  public IBqlBinary GetNextPredicate() => this.ensureNext();

  public abstract bool UseParenthesis();
}
