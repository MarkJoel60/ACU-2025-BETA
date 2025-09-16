// Decompiled with JetBrains decompiler
// Type: PX.Data.Round`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Returns a numeric value rounded to the specified precision.
/// Equivalent to SQL function ROUND.
/// </summary>
/// <typeparam name="Operand1">The numeric expression, given by a field, constant, or function.</typeparam>
/// <typeparam name="Operand2">The precision, given by a field, constant, or function.</typeparam>
/// <example><para>The code below shows the definition of a DAC field that is calculated at run time by a formula.</para>
/// <code title="Example" lang="CS">
/// [PXFormula(typeof(
///     Sub&lt;Mult&lt;SOLine.openQty, SOLine.curyUnitPrice&gt;,
///         Round&lt;Div&lt;Mult&lt;Mult&lt;SOLine.openQty, SOLine.curyUnitPrice&gt;, SOLine.discPct&gt;, decimal100&gt;, SOLine.curyInfoID&gt;&gt;))]
/// [PXDefault(TypeCode.Decimal, "0.0")]
/// public virtual Decimal? CuryOpenAmt { get; set; }</code>
/// </example>
public sealed class Round<Operand1, Operand2> : 
  BqlFunction,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier,
  IBqlAggregateOperand
  where Operand1 : IBqlOperand
  where Operand2 : IBqlOperand
{
  private IBqlCreator _operand1;
  private IBqlCreator _operand2;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) null;
    object obj1;
    object obj2;
    if (!BqlFunction.getValue<Operand1>(ref this._operand1, cache, item, pars, ref result, out obj1) || obj1 == null || !BqlFunction.getValue<Operand2>(ref this._operand2, cache, item, pars, ref result, out obj2) || obj2 == null)
      return;
    switch (System.Type.GetTypeCode(obj1.GetType()))
    {
      case TypeCode.Int16:
        value = (object) Convert.ToInt16(obj1);
        break;
      case TypeCode.Int32:
        value = (object) Convert.ToInt32(obj1);
        break;
      case TypeCode.Int64:
        value = (object) Convert.ToInt64(obj1);
        break;
      case TypeCode.Double:
        value = (object) System.Math.Round(Convert.ToDouble(obj1), (int) Convert.ToInt16(obj2), MidpointRounding.AwayFromZero);
        break;
      case TypeCode.Decimal:
        value = (object) System.Math.Round(Convert.ToDecimal(obj1), (int) Convert.ToInt16(obj2), MidpointRounding.AwayFromZero);
        break;
    }
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    SQLExpression exp1 = (SQLExpression) null;
    int num1 = 1 & (this.GetOperandExpression<Operand1>(ref exp1, ref this._operand1, graph, info, selection) ? 1 : 0);
    SQLExpression exp2 = (SQLExpression) null;
    int num2 = this.GetOperandExpression<Operand2>(ref exp2, ref this._operand2, graph, info, selection) ? 1 : 0;
    int num3 = num1 & num2;
    if (!info.BuildExpression)
      return num3 != 0;
    exp = (exp1 ?? SQLExpression.None()).Round(exp2);
    return num3 != 0;
  }
}
