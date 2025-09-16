// Decompiled with JetBrains decompiler
// Type: PX.Data.Not2`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Adds logical "not" to a unary operator and continues the conditonal
/// expression. In the resulting SQL the group is
/// preceded with NOT and surrounded by brackets.
/// </summary>
/// <typeparam name="Operator">The unary operator, <tt>Where</tt>, <tt>Where2</tt>,
/// or <tt>Match</tt> class.</typeparam>
/// <typeparam name="NextOperator">The next conditional expression, <tt>And</tt>,
/// <tt>And2</tt>, <tt>Or</tt>, or <tt>Or2</tt> class.</typeparam>
public sealed class Not2<Operator, NextOperator> : IBqlUnary, IBqlCreator, IBqlVerifier
  where Operator : IBqlUnary, new()
  where NextOperator : IBqlBinary, new()
{
  private IBqlCreator _operator;
  private IBqlCreator _next;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this._operator == null)
      this._operator = (IBqlCreator) new Operator();
    this._operator.Verify(cache, item, pars, ref result, ref value);
    bool? nullable = result;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
    {
      result = new bool?(true);
    }
    else
    {
      nullable = result;
      bool flag2 = true;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
        result = new bool?(false);
    }
    if (this._next == null)
      this._next = (IBqlCreator) new NextOperator();
    this._next.Verify(cache, item, pars, ref result, ref value);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (this._operator == null)
      this._operator = (IBqlCreator) new Operator();
    SQLExpression exp1 = (SQLExpression) null;
    int num1 = 1 & (this._operator.AppendExpression(ref exp1, graph, info, selection) ? 1 : 0);
    exp1?.Embrace();
    if (this._next == null)
      this._next = (IBqlCreator) new NextOperator();
    int num2 = this._next.AppendExpression(ref exp1, graph, info, selection) ? 1 : 0;
    int num3 = num1 & num2;
    if (!info.BuildExpression)
      return num3 != 0;
    exp = SQLExpression.Not(exp1);
    return num3 != 0;
  }
}
