// Decompiled with JetBrains decompiler
// Type: PX.Data.NullIf`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Returns null if <tt>Operand1</tt> equals <tt>Operand2</tt> and
/// returns <tt>Operand1</tt> if the two expression are not equal.
/// Equivalent to SQL function NULLIF.
/// </summary>
/// <typeparam name="Operand1">A field, constant, or function.</typeparam>
/// <typeparam name="Operand2">A field, constant, or function.</typeparam>
/// <remarks>You can use fluent BQL <see cref="T:PX.Data.BQL.BqlOperand`2.NullIf`1" /> instead.</remarks>
/// <example><para>The code below shows part of the definition of a DAC field. The NullIf&lt;,&gt; class is used in the formula to prevent deletion by zero. If the APTran.qty equals zero, the formula returns null, so the CuryDiscCost value is null.</para>
/// <code title="Example" lang="CS">
/// [PXFormula(typeof(
///     Div&lt;Sub&lt;APTran.curyTranAmt, APTran.curyDiscAmt&gt;,
///         NullIf&lt;APTran.qty, decimal0&gt;&gt;))]
/// [PXUIField(DisplayName = "Disc. Unit Cost", Enabled = false, Visible = false)]
/// public virtual Decimal? CuryDiscCost { get; set; }</code>
/// </example>
public sealed class NullIf<Operand1, Operand2> : BqlFunction, IBqlOperand, IBqlCreator, IBqlVerifier
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
    if (!BqlFunction.getValue<Operand1>(ref this._operand1, cache, item, pars, ref result, out value) || value == null)
      return;
    object objB;
    BqlFunction.getValue<Operand2>(ref this._operand2, cache, item, pars, ref result, out objB);
    value = object.Equals(value, objB) ? (object) null : value;
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
    exp = (exp1 ?? SQLExpression.None()).NullIf(exp2);
    return num3 != 0;
  }
}
