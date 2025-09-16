// Decompiled with JetBrains decompiler
// Type: PX.Data.ExternalValue`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class ExternalValue<Expression> : IBqlOperand, IBqlCreator, IBqlVerifier where Expression : IBqlOperand
{
  private IBqlCreator _expression;

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (typeof (IBqlField).IsAssignableFrom(typeof (Expression)))
    {
      info.Fields?.Add(typeof (Expression));
    }
    else
    {
      if (this._expression == null)
        this._expression = this._expression.createOperand<Expression>();
      SQLExpression exp1 = SQLExpression.None();
      this._expression.AppendExpression(ref exp1, graph, info, selection);
    }
    return true;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    bool flag = true;
    System.Type type = (System.Type) null;
    if (item is BqlFormula.ItemContainer itemContainer)
    {
      itemContainer.InvolvedFields.Add(typeof (Expression));
      flag = itemContainer.IsExternalCall;
      type = itemContainer.DependentField;
    }
    if (typeof (IBqlField).IsAssignableFrom(typeof (Expression)))
    {
      value = flag && type?.Name == typeof (Expression).Name || type?.Name != typeof (Expression).Name ? cache.GetValue(BqlFormula.ItemContainer.Unwrap(item), typeof (Expression).Name) : PXCache.NotSetValue;
    }
    else
    {
      if (this._expression == null)
        this._expression = this._expression.createOperand<Expression>();
      this._expression.Verify(cache, item, pars, ref result, ref value);
      List<System.Type> typeList = new List<System.Type>();
      SQLExpression sqlExpression = SQLExpression.None();
      IBqlCreator expression = this._expression;
      ref SQLExpression local = ref sqlExpression;
      PXGraph graph = cache.Graph;
      BqlCommandInfo info = new BqlCommandInfo(false);
      info.Fields = typeList;
      info.BuildExpression = false;
      BqlCommand.Selection selection = new BqlCommand.Selection();
      expression.AppendExpression(ref local, graph, info, selection);
      value = flag && typeList.Contains(type) || !typeList.Contains(type) ? value : PXCache.NotSetValue;
    }
  }
}
