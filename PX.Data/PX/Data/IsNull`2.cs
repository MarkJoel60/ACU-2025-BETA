// Decompiled with JetBrains decompiler
// Type: PX.Data.IsNull`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Returns <tt>Operand1</tt> if it is not null, or <tt>Operand2</tt> otherwise.
/// Equivalent to SQL function ISNULL.
/// </summary>
/// <typeparam name="Operand1">A field, constant, or function.</typeparam>
/// <typeparam name="Operand2">A field, constant, or function.</typeparam>
/// <remarks>You can use fluent BQL <see cref="T:PX.Data.BQL.BqlOperand`2.IfNullThen`1" /> instead.</remarks>
/// <example><para>The code below shows a part of a DAC field definition. The formula here sets the CuryRUTROTUndistributedAmt field at run time to the difference of the ARInvoice.CuryRUTROTTotalAmt and ARInvoice.CuryRUTROTTotalAmt values and to decimal zero if any of these values is null.</para>
/// <code title="Example" lang="CS">
/// [PXFormula(typeof(
///     IsNull&lt;Sub&lt;ARInvoice.curyRUTROTTotalAmt,
///                ARInvoice.curyRUTROTDistributedAmt&gt;,
///            decimal0&gt;))]
/// public virtual decimal? CuryRUTROTUndistributedAmt { get; set; }</code>
/// </example>
public sealed class IsNull<Operand1, Operand2> : 
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
    if (!BqlFunction.getValue<Operand1>(ref this._operand1, cache, item, pars, ref result, out value) || value != null)
      return;
    BqlFunction.getValue<Operand2>(ref this._operand2, cache, item, pars, ref result, out value);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    SQLExpression exp1 = (SQLExpression) null;
    bool flag2 = flag1 & this.GetOperandExpression<Operand1>(ref exp1, ref this._operand1, graph, info, selection);
    SQLExpression exp2 = (SQLExpression) null;
    bool flag3 = flag2 & this.GetOperandExpression<Operand2>(ref exp2, ref this._operand2, graph, info, selection);
    if (info.BuildExpression)
      exp = (exp1 ?? SQLExpression.None()).Coalesce(exp2);
    if (info.Parameters != null)
    {
      foreach (IBqlParameter parameter in info.Parameters)
        parameter.NullAllowed = true;
    }
    return flag3;
  }
}
