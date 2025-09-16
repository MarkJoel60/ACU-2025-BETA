// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlFormula`7
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// An abstract class that is used to derive custom BQL functions.
/// </summary>
/// <typeparam name="Operand1">A field, constant, or function.</typeparam>
/// <typeparam name="Operand2">A field, constant, or function.</typeparam>
/// <typeparam name="Operand3">A field, constant, or function.</typeparam>
/// <typeparam name="Operand4">A field, constant, or function.</typeparam>
/// <typeparam name="Operand5">A field, constant, or function.</typeparam>
/// <typeparam name="Operand6">A field, constant, or function.</typeparam>
/// <typeparam name="Operand7">A field, constant, or function.</typeparam>
[Obsolete("Use BqlFormulaEvaluator<> instead.")]
public abstract class BqlFormula<Operand1, Operand2, Operand3, Operand4, Operand5, Operand6, Operand7> : 
  BqlFormula<Operand1, Operand2, Operand3, Operand4, Operand5, Operand6>
  where Operand1 : IBqlOperand
  where Operand2 : IBqlOperand
  where Operand3 : IBqlOperand
  where Operand4 : IBqlOperand
  where Operand5 : IBqlOperand
  where Operand6 : IBqlOperand
  where Operand7 : IBqlOperand
{
  protected IBqlCreator _operand7;

  /// <exclude />
  public override bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return base.AppendExpression(ref exp, graph, info, selection) & this.GetOperandExpression<Operand7>(ref exp, ref this._operand7, graph, info, selection);
  }
}
