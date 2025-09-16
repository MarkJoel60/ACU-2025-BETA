// Decompiled with JetBrains decompiler
// Type: PX.Data.Sub`2
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
/// Returns the subtraction of <tt>Operand2</tt> from <tt>Operand1</tt>.
/// </summary>
/// <typeparam name="Operand1">A field, constant, or function.</typeparam>
/// <typeparam name="Operand2">A field, constant, or function.</typeparam>
/// <example><para>The code below shows the definitinon of the CuryUnappliedBal field in the ARPayment DAC. The field is set to the difference of CuryDocBal and CuryApplAmt fields.</para>
/// <code title="Example" lang="CS">
/// [PXCurrency(typeof(APPayment.curyInfoID), typeof(APPayment.unappliedBal))]
/// [PXUIField(DisplayName = "Unapplied Balance", Visibility = PXUIVisibility.Visible, Enabled = false)]
/// [PXFormula(typeof(Sub&lt;APPayment.curyDocBal, APPayment.curyApplAmt&gt;))]
/// public virtual Decimal? CuryUnappliedBal
/// {
/// ...
/// }</code>
/// </example>
public sealed class Sub<Operand1, Operand2> : 
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
    value = Sub<Operand1, Operand2>.calculateValue(obj1, obj2);
  }

  internal static object calculateValue(object value1, object value2)
  {
    switch (System.Type.GetTypeCode(value1.GetType()))
    {
      case TypeCode.Int16:
        return (object) ((int) Convert.ToInt16(value1) - (int) Convert.ToInt16(value2));
      case TypeCode.Int32:
        return (object) (Convert.ToInt32(value1) - Convert.ToInt32(value2));
      case TypeCode.Int64:
        return (object) (Convert.ToInt64(value1) - Convert.ToInt64(value2));
      case TypeCode.Double:
        return (object) (Convert.ToDouble(value1) - Convert.ToDouble(value2));
      case TypeCode.Decimal:
        return (object) (Convert.ToDecimal(value1) - Convert.ToDecimal(value2));
      case TypeCode.DateTime:
        return System.Type.GetTypeCode(value2.GetType()) == TypeCode.DateTime ? (object) (int) Convert.ToDateTime(value1).Subtract(Convert.ToDateTime(value2)).TotalMinutes : (object) (value2 is TimeSpan timeSpan ? Convert.ToDateTime(value1).Add(timeSpan) : Convert.ToDateTime(value1).AddDays((double) -Convert.ToInt32(value2)));
      default:
        return (object) null;
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
    exp = (exp1 - exp2).Embrace();
    return num3 != 0;
  }
}
