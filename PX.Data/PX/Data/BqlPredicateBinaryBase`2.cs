// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlPredicateBinaryBase`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class BqlPredicateBinaryBase<Operator, NextOperator> : 
  IBqlBinary,
  IBqlCreator,
  IBqlVerifier,
  IBqlPredicateChain
  where Operator : IBqlUnary, new()
  where NextOperator : IBqlBinary, new()
{
  private IBqlUnary _operator;
  private IBqlBinary _next;

  protected IBqlUnary ensureOperator()
  {
    if (this._operator == null)
      this._operator = (IBqlUnary) new Operator();
    return this._operator;
  }

  protected IBqlBinary ensureNext()
  {
    IBqlBinary next = this._next;
    if (next != null)
      return next;
    return !(typeof (NextOperator) == typeof (BqlNone)) ? (this._next = (IBqlBinary) new NextOperator()) : (IBqlBinary) null;
  }

  protected abstract string SqlOperator { get; }

  protected abstract bool BypassedValue { get; }

  protected BqlPredicateBinaryBase()
  {
  }

  protected BqlPredicateBinaryBase(IBqlUnary op) => this._operator = op;

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    bool? nullable1 = result;
    bool? nullable2 = result;
    bool bypassedValue = this.BypassedValue;
    bool flag1 = nullable2.GetValueOrDefault() == bypassedValue & nullable2.HasValue || this.BypassedValue && !result.HasValue;
    this.ensureOperator();
    this._operator.Verify(cache, item, pars, ref result, ref value);
    if (this.ensureNext() != null)
      this._next.Verify(cache, item, pars, ref result, ref value);
    if (!this.IsConjunction())
    {
      bool? nullable3 = nullable1;
      bool flag2 = true;
      if (nullable3.GetValueOrDefault() == flag2 & nullable3.HasValue)
      {
        result = new bool?(true);
      }
      else
      {
        if (!flag1)
          return;
        result = new bool?();
        value = (object) null;
      }
    }
    else
    {
      if (!flag1)
        return;
      result = new bool?(this.BypassedValue);
    }
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    SQLExpression exp1 = (SQLExpression) null;
    bool flag2 = flag1 & this.ensureOperator().AppendExpression(ref exp1, graph, info, selection);
    switch (this.SqlOperator)
    {
      case "AND":
        if (info.BuildExpression)
          exp = exp.And(exp1);
        if (this.ensureNext() != null)
        {
          flag2 &= this._next.AppendExpression(ref exp, graph, info, selection);
          break;
        }
        break;
      case "OR":
        if (this.ensureNext() != null)
        {
          if (exp1 == null && info.BuildExpression)
            exp1 = SQLExpression.None();
          flag2 &= this._next.AppendExpression(ref exp1, graph, info, selection);
        }
        if (info.BuildExpression)
        {
          exp = exp.Or(exp1);
          break;
        }
        break;
    }
    if (info.Parameters != null && this.BypassedValue)
    {
      foreach (IBqlParameter parameter in info.Parameters)
        parameter.NullAllowed = true;
    }
    return flag2;
  }

  public IBqlUnary GetUnary()
  {
    return !(typeof (NextOperator) == typeof (BqlNone)) ? (IBqlUnary) new Where2Np<Operator, NextOperator>() : this.GetContainedPredicate();
  }

  public override string ToString()
  {
    string str1 = this._operator == null ? typeof (Operator).ToString() : this._operator.ToString();
    if (this._next == null && typeof (NextOperator) == typeof (BqlNone))
      return $" {this.SqlOperator}{str1}";
    string str2 = this._next == null ? typeof (NextOperator).ToString() : this._next.ToString();
    return $" {this.SqlOperator}{str1} {str2}";
  }

  public bool UseParenthesis() => false;

  public bool IsConjunction() => !this.BypassedValue;

  public IBqlBinary GetNextPredicate() => this.ensureNext();

  public bool ContainsOperandWithComparison() => false;

  public IBqlUnary GetContainedPredicate() => this.ensureOperator();
}
