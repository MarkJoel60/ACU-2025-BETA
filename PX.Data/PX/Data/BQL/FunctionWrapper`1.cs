// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.FunctionWrapper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BQL;

/// <exclude />
public sealed class FunctionWrapper<TFunctionOrOperand> : IBqlOperand, IBqlCreator, IBqlVerifier where TFunctionOrOperand : IBqlCreator, new()
{
  private readonly TFunctionOrOperand _functionOrOperand = new TFunctionOrOperand();

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this._functionOrOperand.Verify(cache, item, pars, ref result, ref value);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (!(this._functionOrOperand is IBqlFunctionExt functionOrOperand))
      return this._functionOrOperand.AppendExpression(ref exp, graph, info, selection);
    System.Type field = functionOrOperand.GetField();
    if (functionOrOperand.Operation == SQLExpression.Operation.NONE)
    {
      if (info.BuildExpression)
        exp = BqlCommand.GetSingleExpression(field, graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
      return true;
    }
    if (functionOrOperand.Operation == SQLExpression.Operation.COUNT_DISTINCT)
      field = typeof (TFunctionOrOperand).GetGenericArguments()[0];
    SQLExpression expr = (SQLExpression) null;
    if (info.BuildExpression && functionOrOperand.Operation != SQLExpression.Operation.COUNT)
      expr = BqlCommand.GetSingleExpression(field, graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
    if (info.BuildExpression)
      exp = SQLExpression.Aggregate(functionOrOperand.Operation, expr);
    return true;
  }
}
