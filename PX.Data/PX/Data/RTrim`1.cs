// Decompiled with JetBrains decompiler
// Type: PX.Data.RTrim`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class RTrim<Operand> : BqlFunction, IBqlOperand, IBqlCreator, IBqlVerifier where Operand : IBqlOperand
{
  private IBqlCreator _operand;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) null;
    object obj;
    if (!BqlFunction.getValue<Operand>(ref this._operand, cache, item, pars, ref result, out obj) || obj == null)
      return;
    value = (object) obj.ToString().TrimEnd();
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    SQLExpression exp1 = (SQLExpression) null;
    int num = 1 & (this.GetOperandExpression<Operand>(ref exp1, ref this._operand, graph, info, selection) ? 1 : 0);
    if (!info.BuildExpression)
      return num != 0;
    exp = (exp1 ?? SQLExpression.None()).RTrim();
    return num != 0;
  }
}
