// Decompiled with JetBrains decompiler
// Type: PX.Data.IIf`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class IIf<Operator, Operand, OperandElse> : IBqlOperand, IBqlCreator, IBqlVerifier
  where Operator : IBqlUnary, new()
  where Operand : IBqlOperand
  where OperandElse : IBqlOperand
{
  private readonly IBqlCreator _formula = (IBqlCreator) new Switch<Case<Where<Operator>, Operand>, OperandElse>();

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this._formula.AppendExpression(ref exp, graph, info, selection);
  }

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this._formula.Verify(cache, item, pars, ref result, ref value);
  }
}
