// Decompiled with JetBrains decompiler
// Type: PX.Data.Case2`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class Case2<Where_, Operand, NextCase> : Case<Where_, Operand>
  where Where_ : IBqlWhere, new()
  where Operand : IBqlOperand
  where NextCase : IBqlCase, new()
{
  private IBqlCase _nextcase;

  /// <exclude />
  public override bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    int num1 = 1 & (base.AppendExpression(ref exp, graph, info, selection) ? 1 : 0);
    this.Ensure<IBqlCase, NextCase>(ref this._nextcase);
    SQLExpression exp1 = (SQLExpression) null;
    int num2 = this._nextcase.AppendExpression(ref exp1, graph, info, selection) ? 1 : 0;
    int num3 = num1 & num2;
    if (!info.BuildExpression)
      return num3 != 0;
    SQLSwitch sqlSwitch = (SQLSwitch) exp;
    if (sqlSwitch == null)
      return num3 != 0;
    sqlSwitch.Case((SQLSwitch) exp1);
    return num3 != 0;
  }

  /// <exclude />
  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    base.Verify(cache, item, pars, ref result, ref value);
    bool? nullable = result;
    bool flag = true;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      return;
    this.Ensure<IBqlCase, NextCase>(ref this._nextcase);
    this._nextcase.Verify(cache, item, pars, ref result, ref value);
  }
}
