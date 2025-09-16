// Decompiled with JetBrains decompiler
// Type: PX.Data.BqlFormula`2
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
[Obsolete("Use BqlFormulaEvaluator<> instead.")]
public abstract class BqlFormula<Operand1, Operand2> : BqlFormula<Operand1>
  where Operand1 : IBqlOperand
  where Operand2 : IBqlOperand
{
  protected IBqlCreator _operand2;

  /// <exclude />
  public override bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return base.AppendExpression(ref exp, graph, info, selection) & this.GetOperandExpression<Operand2>(ref exp, ref this._operand2, graph, info, selection);
  }
}
