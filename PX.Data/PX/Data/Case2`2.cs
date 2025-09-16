// Decompiled with JetBrains decompiler
// Type: PX.Data.Case2`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class Case2<Where_, Operand> : BqlFunction, IBqlCase, IBqlCreator, IBqlVerifier, IBqlOperand
  where Where_ : IBqlWhere, new()
  where Operand : IBqlOperand
{
  private IBqlWhere _where;
  private IBqlCreator _operand;

  /// <exclude />
  protected void Ensure<I, T>(ref I variable) where T : I, new()
  {
    if ((object) variable != null)
      return;
    variable = (I) new T();
  }

  protected void FieldDefaulting(PXCache cache, object item)
  {
    this.Ensure<IBqlWhere, Where_>(ref this._where);
    List<System.Type> source = new List<System.Type>();
    SQLExpression sqlExpression = SQLExpression.None();
    IBqlWhere where = this._where;
    ref SQLExpression local = ref sqlExpression;
    BqlCommandInfo info = new BqlCommandInfo(false);
    info.Fields = source;
    info.BuildExpression = false;
    BqlCommand.Selection selection = new BqlCommand.Selection();
    where.AppendExpression(ref local, (PXGraph) null, info, selection);
    object pure_item = BqlFormula.ItemContainer.Unwrap(item);
    foreach (System.Type type in source.Where<System.Type>((Func<System.Type, bool>) (t => cache.GetValue(pure_item, t.Name) == null)))
    {
      object newValue;
      if (cache.RaiseFieldDefaulting(type.Name, pure_item, out newValue))
        cache.RaiseFieldUpdating(type.Name, pure_item, ref newValue);
      cache.SetValue(pure_item, type.Name, newValue);
    }
  }

  /// <exclude />
  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    SQLSwitch sqlSwitch = (SQLSwitch) null;
    if (info.BuildExpression)
      sqlSwitch = new SQLSwitch();
    this.Ensure<IBqlWhere, Where_>(ref this._where);
    SQLExpression exp1 = (SQLExpression) null;
    int num1 = 1 & (this._where.AppendExpression(ref exp1, graph, info, selection) ? 1 : 0);
    SQLExpression exp2 = (SQLExpression) null;
    int num2 = this.GetOperandExpression<Operand>(ref exp2, ref this._operand, graph, info, selection) ? 1 : 0;
    int num3 = num1 & num2;
    if (!info.BuildExpression)
      return num3 != 0;
    exp = (SQLExpression) sqlSwitch.Case(exp1, exp2);
    return num3 != 0;
  }

  /// <exclude />
  public virtual void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this.Ensure<IBqlWhere, Where_>(ref this._where);
    ((IBqlUnary) this._where).Verify(cache, item, pars, ref result, ref value);
    value = (object) null;
    bool? nullable = result;
    bool flag = true;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      bool? result1 = new bool?();
      BqlFunction.getValue<Operand>(ref this._operand, cache, item, pars, ref result1, out value);
    }
    else
    {
      if (!this.tryCreateOperand<Operand>(ref this._operand))
        return;
      List<IBqlParameter> bqlParameterList = new List<IBqlParameter>();
      SQLExpression sqlExpression = SQLExpression.None();
      IBqlCreator operand = this._operand;
      ref SQLExpression local = ref sqlExpression;
      BqlCommandInfo info = new BqlCommandInfo(false);
      info.Parameters = bqlParameterList;
      info.BuildExpression = false;
      BqlCommand.Selection selection = new BqlCommand.Selection();
      operand.AppendExpression(ref local, (PXGraph) null, info, selection);
      if (bqlParameterList.Count <= 0)
        return;
      pars.RemoveRange(0, bqlParameterList.Count);
    }
  }
}
