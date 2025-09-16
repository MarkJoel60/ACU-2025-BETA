// Decompiled with JetBrains decompiler
// Type: PX.Data.Switch`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Evaluates conditions and returns one of multiple possible values
/// or the default value if none of the conditions is satisfied.
/// Equivalent to SQL CASE-ELSE expression. Pairs condition-value
/// are specified via the <tt>Case</tt> clause.
/// </summary>
/// <typeparam name="Case">Pairs condition-value specified via the
/// <tt>Case</tt> class.</typeparam>
/// <typeparam name="Default">The default value as field, constant,
/// or function.</typeparam>
/// <seealso cref="T:PX.Data.Case`2"></seealso>
/// <seealso cref="T:PX.Data.Case`3"></seealso>
/// <example><para>The code below shows the definition of a DAC field and the SQL expression inserted into the query to calculate the field value.</para>
/// <code title="Example" lang="CS">
/// [PXDBCalced(
///     typeof(Switch&lt;
///         Case&lt;Where&lt;APInvoice.paySel, Equal&lt;boolTrue&gt;&gt;, APInvoice.payDate&gt;,
///         APInvoice.dueDate&gt;),
///     typeof(DateTime))]
/// public virtual DateTime? EstPayDate { ... }</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// CASE
///     WHEN APInvoice.PaySel = 1 THEN APInvoice.PayDate
///     ELSE 0
/// END</code>
/// </example>
public sealed class Switch<Case, Default> : 
  BqlFunction,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier,
  ISwitch,
  IBqlAggregateOperand
  where Case : IBqlCase, new()
  where Default : IBqlOperand
{
  private IBqlCase _case;
  private IBqlCreator _default;

  /// <exclude />
  public System.Type OuterField { get; set; }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    SQLSwitch sqlSwitch = (SQLSwitch) null;
    if (info.BuildExpression)
      sqlSwitch = new SQLSwitch();
    if (this._case == null)
      this._case = (IBqlCase) new Case();
    SQLExpression exp1 = (SQLExpression) null;
    bool flag2 = flag1 & this._case.AppendExpression(ref exp1, graph, info, selection);
    sqlSwitch?.Case((SQLSwitch) exp1);
    SQLExpression exp2 = (SQLExpression) null;
    bool flag3 = flag2 & this.GetOperandExpression<Default>(ref exp2, ref this._default, graph, info, selection);
    sqlSwitch?.Default(exp2);
    exp = (SQLExpression) sqlSwitch;
    if (info.Parameters != null)
    {
      foreach (IBqlParameter parameter in info.Parameters)
        parameter.NullAllowed = true;
    }
    return flag3;
  }

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this._case == null)
      this._case = (IBqlCase) new Case();
    this._case.Verify(cache, item, pars, ref result, ref value);
    bool? nullable = result;
    bool flag = true;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      return;
    if (this.OuterField == typeof (Default))
      value = PXCache.NotSetValue;
    else
      BqlFunction.getValue<Default>(ref this._default, cache, item, pars, ref result, out value);
  }
}
