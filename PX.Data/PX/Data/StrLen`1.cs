// Decompiled with JetBrains decompiler
// Type: PX.Data.StrLen`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Returns the number of characters in the <tt>Source</tt> string.
/// </summary>
/// <typeparam name="Source">A field, constant, or function.</typeparam>
/// <example><para>The code below shows the definiton of the FinYear field and the SQL expression that is inserted into the query to select the value for it.</para>
/// <code title="Example" lang="CS">
/// [PXDBCalced(typeof(StrLen&lt;AcctHist.finPeriodID), typeof(int))]
/// public override int FinYearLength { ... }</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// LEN(AcctHist.FinPeriodID)</code>
/// </example>
public sealed class StrLen<Source> : 
  BqlFunction,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier,
  IBqlAggregateOperand
  where Source : IBqlOperand
{
  private IBqlCreator _source;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (!BqlFunction.getValue<Source>(ref this._source, cache, item, pars, ref result, out value) || value == null)
      return;
    value = (object) ((string) value).Length;
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    SQLExpression exp1 = (SQLExpression) null;
    int num = 1 & (this.GetOperandExpression<Source>(ref exp1, ref this._source, graph, info, selection) ? 1 : 0);
    if (!info.BuildExpression)
      return num != 0;
    exp = (exp1 ?? SQLExpression.None()).Length();
    return num != 0;
  }
}
