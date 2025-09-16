// Decompiled with JetBrains decompiler
// Type: PX.Data.GroupByBase`2
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
public abstract class GroupByBase<Field, NextAggregate> : IBqlFunction, IBqlCreator, IBqlVerifier
  where Field : IBqlOperand
  where NextAggregate : IBqlFunction, new()
{
  private IBqlCreator _operand;
  private IBqlFunction _next;

  private IBqlFunction ensureNext()
  {
    return !(typeof (NextAggregate) == typeof (BqlNone)) ? this._next ?? (this._next = (IBqlFunction) new NextAggregate()) : (IBqlFunction) null;
  }

  public void GetAggregates(List<IBqlFunction> fields)
  {
    fields.Add((IBqlFunction) this);
    if (this.ensureNext() == null)
      return;
    this._next.GetAggregates(fields);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag = true;
    if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Field)))
    {
      if (info.BuildExpression)
        exp = BqlCommand.GetSingleExpression(typeof (Field), graph, info.Tables, selection, BqlCommand.FieldPlace.GroupBy);
    }
    else
    {
      if (this._operand == null)
        this._operand = this._operand.createOperand<Field>();
      flag &= this._operand.AppendExpression(ref exp, graph, info, selection);
    }
    if (exp == null && info.BuildExpression)
      exp = SQLExpression.None();
    if (this.ensureNext() != null)
    {
      SQLExpression exp1 = (SQLExpression) null;
      flag &= this._next.AppendExpression(ref exp1, graph, info, selection);
      if (info.BuildExpression)
        exp = exp.Seq(exp1);
    }
    return flag;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this.ensureNext() == null)
      return;
    this._next.Verify(cache, item, pars, ref result, ref value);
  }

  public string GetFunction() => string.Empty;

  public System.Type GetField()
  {
    if (!typeof (IBqlCreator).IsAssignableFrom(typeof (Field)))
      return typeof (Field);
    if (!typeof (IBqlFunction).IsAssignableFrom(typeof (Field)))
      return (System.Type) null;
    if (this._operand == null)
      this._operand = (object) Activator.CreateInstance<Field>() as IBqlCreator;
    return ((IBqlFunction) this._operand).GetField();
  }

  public bool IsGroupBy() => true;
}
