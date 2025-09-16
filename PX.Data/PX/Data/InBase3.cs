// Decompiled with JetBrains decompiler
// Type: PX.Data.InBase3
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
public abstract class InBase3 : IBqlComparison, IBqlCreator, IBqlVerifier
{
  protected System.Type[] _types;
  protected IBqlCreator[] _operands;

  [Obsolete]
  protected abstract string SqlOperator { get; }

  protected abstract bool IsNegative { get; }

  protected InBase3(params System.Type[] types)
  {
    this._types = types;
    this._operands = new IBqlCreator[types.Length];
  }

  /// <exclude />
  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag = true;
    SQLExpression exp1 = (SQLExpression) null;
    for (int index = 0; index < this._types.Length; ++index)
    {
      System.Type type = this._types[index];
      SQLExpression exp2 = (SQLExpression) null;
      if (!typeof (IBqlCreator).IsAssignableFrom(type))
      {
        if (info.BuildExpression)
          exp2 = BqlCommand.GetSingleExpression(type, graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
        info.Fields?.Add(type);
      }
      else
      {
        if (this._operands[index] == null)
          this._operands[index] = this._operands[index].createOperand(type);
        flag &= this._operands[index].AppendExpression(ref exp2, graph, info, selection);
        if (exp2 is ISQLDBTypedExpression sqldbTypedExpression)
          sqldbTypedExpression.SetDBType(exp.GetDBType());
      }
      if (info.BuildExpression)
        exp1 = exp1 != null ? exp1.Seq(exp2) : exp2;
    }
    if (info.BuildExpression)
      exp = this.IsNegative ? exp.NotIn(exp1) : exp.In(exp1);
    if (info.Parameters != null)
    {
      foreach (IBqlParameter parameter in info.Parameters)
        parameter.NullAllowed = true;
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
    object val = value;
    object obj = value;
    bool? nullable1 = result;
    int index = 0;
    this.Verify1(this._types[index], ref this._operands[index], cache, item, pars, ref result, ref value, val);
    while (++index < this._types.Length)
    {
      bool? nullable2 = result;
      if (0 == (nullable2.GetValueOrDefault() ? 1 : 0) & nullable2.HasValue)
      {
        value = obj;
        result = nullable1;
        this.Verify1(this._types[index], ref this._operands[index], cache, item, pars, ref result, ref value, val);
      }
      else
        break;
    }
    if (!this.IsNegative)
      return;
    ref bool? local = ref result;
    bool? nullable3 = result;
    bool? nullable4 = nullable3.HasValue ? new bool?(!nullable3.GetValueOrDefault()) : new bool?();
    local = nullable4;
  }

  protected void Verify1(
    System.Type tOperand,
    ref IBqlCreator _operand,
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value,
    object val)
  {
    bool flag = false;
    if (typeof (IBqlField).IsAssignableFrom(tOperand))
    {
      if (cache.GetItemType() == BqlCommand.GetItemType(tOperand) || BqlCommand.GetItemType(tOperand).IsAssignableFrom(cache.GetItemType()))
      {
        value = cache.GetValue(item, tOperand.Name);
        flag = true;
      }
    }
    else
    {
      if (_operand == null)
        _operand = _operand.createOperand(tOperand);
      _operand.Verify(cache, item, pars, ref result, ref value);
    }
    if (!flag)
    {
      result = new bool?(object.Equals(val, value));
    }
    else
    {
      result = new bool?();
      value = (object) null;
    }
  }
}
