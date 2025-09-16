// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlFormulaEvaluator`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// An abstract class that is used to derive custom BQL functions.
/// </summary>
/// <typeparam name="Operand1">A field, constant, or function.</typeparam>
/// <typeparam name="Operand2">A field, constant, or function.</typeparam>
/// <typeparam name="Operand3">A field, constant, or function.</typeparam>
/// <typeparam name="Operand4">A field, constant, or function.</typeparam>
public abstract class BqlFormulaEvaluator<Operand1, Operand2, Operand3, Operand4> : 
  BqlFormulaEvaluator<Operand1, Operand2, Operand3>
  where Operand1 : IBqlOperand
  where Operand2 : IBqlOperand
  where Operand3 : IBqlOperand
  where Operand4 : IBqlOperand
{
  protected IBqlCreator _operand4;

  /// <exclude />
  public override bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return base.AppendExpression(ref exp, graph, info, selection) & this.GetOperandExpression<Operand4>(ref exp, ref this._operand4, graph, info, selection);
  }

  /// <exclude />
  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = this.Evaluate(cache, BqlFormula.ItemContainer.Unwrap(item), new Dictionary<System.Type, object>()
    {
      [typeof (Operand1)] = this.Calculate<Operand1>(cache, item),
      [typeof (Operand2)] = this.Calculate<Operand2>(cache, item),
      [typeof (Operand3)] = this.Calculate<Operand3>(cache, item),
      [typeof (Operand4)] = this.Calculate<Operand4>(cache, item)
    });
  }
}
