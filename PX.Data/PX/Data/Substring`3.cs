// Decompiled with JetBrains decompiler
// Type: PX.Data.Substring`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Returns the <tt>Length</tt> characters from the <tt>Source</tt> string starting
/// from the <tt>StartIndex</tt> index (the first character has index 1).
/// Equivalent to SQL function SUBSTRING.
/// </summary>
/// <typeparam name="Source">A field, constant, or function.</typeparam>
/// <typeparam name="StartIndex">A field, constant, or function.</typeparam>
/// <typeparam name="Length">A field, constant, or function.</typeparam>
/// <example>
/// The code below shows the definiton of the <tt>FinYear</tt> field and
/// the SQL expression that is inserted into the query to select the value for it.
/// <code>
/// [PXDBCalced(typeof(Substring&lt;AcctHist.finPeriodID, CS.int1, CS.int4&gt;), typeof(string))]
/// public override string FinYear { ... }
/// </code>
/// <code lang="SQL">
/// SUBSTRING(AcctHist.FinPeriodID, 1, 4)
/// </code>
/// </example>
public sealed class Substring<Source, StartIndex, Length> : 
  BqlFunction,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier
  where Source : IBqlOperand
  where StartIndex : IBqlOperand
  where Length : IBqlOperand
{
  private IBqlCreator _source;
  private IBqlCreator _startIndex;
  private IBqlCreator _length;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    object obj;
    object length;
    if (!BqlFunction.getValue<Source>(ref this._source, cache, item, pars, ref result, out value) || value == null || !BqlFunction.getValue<StartIndex>(ref this._startIndex, cache, item, pars, ref result, out obj) || obj == null || !BqlFunction.getValue<Length>(ref this._length, cache, item, pars, ref result, out length) || length == null)
      return;
    value = (object) ((string) value).Substring(System.Math.Max(0, (int) obj - 1), (int) length);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    SQLExpression exp1 = (SQLExpression) null;
    int num1 = 1 & (this.GetOperandExpression<Source>(ref exp1, ref this._source, graph, info, selection) ? 1 : 0);
    SQLExpression exp2 = (SQLExpression) null;
    int num2 = this.GetOperandExpression<StartIndex>(ref exp2, ref this._startIndex, graph, info, selection) ? 1 : 0;
    int num3 = num1 & num2;
    SQLExpression exp3 = (SQLExpression) null;
    int num4 = this.GetOperandExpression<Length>(ref exp3, ref this._length, graph, info, selection) ? 1 : 0;
    int num5 = num3 & num4;
    if (!info.BuildExpression)
      return num5 != 0;
    exp = (exp1 ?? SQLExpression.None()).Substr(exp2, exp3);
    return num5 != 0;
  }
}
