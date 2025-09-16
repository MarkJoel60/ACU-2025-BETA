// Decompiled with JetBrains decompiler
// Type: PX.Data.WhereBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class WhereBase<UnaryOperator, NextOperator> : 
  IBqlWhere,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier,
  IBqlPredicateChain
  where UnaryOperator : IBqlUnary, new()
  where NextOperator : IBqlBinary, new()
{
  private IBqlUnary _operator;
  private IBqlBinary _next;

  protected WhereBase()
  {
  }

  protected WhereBase(IBqlUnary un, IBqlBinary next)
  {
    this._operator = un;
    this._next = next;
  }

  protected IBqlUnary ensureOperator()
  {
    IBqlUnary bqlUnary = this._operator;
    if (bqlUnary != null)
      return bqlUnary;
    return !(typeof (UnaryOperator) == typeof (BqlNone)) ? (this._operator = (IBqlUnary) new UnaryOperator()) : (IBqlUnary) null;
  }

  protected IBqlBinary ensureNext()
  {
    IBqlBinary next = this._next;
    if (next != null)
      return next;
    return !(typeof (NextOperator) == typeof (BqlNone)) ? (this._next = (IBqlBinary) new NextOperator()) : (IBqlBinary) null;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this.ensureOperator() != null)
      this._operator.Verify(cache, item, pars, ref result, ref value);
    if (this.ensureNext() == null)
      return;
    this._next.Verify(cache, item, pars, ref result, ref value);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    RuntimeHelpers.EnsureSufficientExecutionStack();
    bool flag = true;
    bool[] flagArray = info.Parameters == null ? new bool[0] : info.Parameters.ConvertAll<bool>((Converter<IBqlParameter, bool>) (_ => _.NullAllowed)).ToArray();
    if (this.ensureOperator() != null)
      flag &= this._operator.AppendExpression(ref exp, graph, info, selection);
    if (this.ensureNext() != null)
    {
      if (exp == null && info.BuildExpression)
        exp = SQLExpression.None();
      flag &= this._next.AppendExpression(ref exp, graph, info, selection);
    }
    if (this.UseParenthesis())
      exp?.Embrace();
    for (int index = 0; index < flagArray.Length; ++index)
      info.Parameters[index].NullAllowed = flagArray[index];
    return flag;
  }

  public override string ToString()
  {
    string str1 = this._operator == null ? typeof (UnaryOperator).ToString() : this._operator.ToString();
    if (this._next == null && typeof (NextOperator) == typeof (BqlNone))
      return !this.UseParenthesis() ? str1 : $"({str1})";
    string str2 = this._next == null ? typeof (NextOperator).ToString() : this._next.ToString();
    if (!this.UseParenthesis())
      return $"{str1} {str2}";
    return $"({str1} {str2})";
  }

  public IBqlUnary GetContainedPredicate() => this.ensureOperator();

  public bool ContainsOperandWithComparison() => false;

  public IBqlBinary GetNextPredicate() => this.ensureNext();

  public abstract bool UseParenthesis();
}
