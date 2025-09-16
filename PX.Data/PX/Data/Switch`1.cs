// Decompiled with JetBrains decompiler
// Type: PX.Data.Switch`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Evaluates conditions and returns one of multiple possible values.
/// Equivalent to SQL CASE expression without the ELSE expression.
/// The condition-value pairs are specified via the <tt>Case</tt> clause.
/// </summary>
/// <typeparam name="Case">Pairs condition-value specified via the
/// <tt>Case</tt> class.</typeparam>
/// <remarks>
/// <para>The <tt>Switch</tt> clause can be used as <tt>Operand</tt> in
/// classes such as <tt>Where</tt> ad <tt>OrderBy</tt>.</para>
/// <para>You can use fluent BQL <see cref="T:PX.Data.BQL.BqlOperand`2.When`1.Else`1" />
/// or <see cref="T:PX.Data.BQL.BqlOperand`2.When`1.ElseNull" /> instead.</para>
/// </remarks>
/// <seealso cref="T:PX.Data.Case`2"></seealso>
/// <seealso cref="T:PX.Data.Case`3"></seealso>
/// <example><para>The code below shows the definition of a DAC field calculated at run time.</para>
/// <code title="Example" lang="CS">
/// [PXFormula(typeof(Switch&lt;
///     Case&lt;Where&lt;EPActivity.priority, Equal&lt;int0&gt;&gt;, EPActivity.priorityIcon.low,
///     Case&lt;Where&lt;EPActivity.priority, Equal&lt;int2&gt;&gt;, EPActivity.priorityIcon.high&gt;&gt;&gt;))]
/// public virtual String PriorityIcon { get; set; }</code>
/// <code title="Example2" description="This is translated into:" groupname="Example" lang="SQL">
/// CASE
///     WHEN Table.Field1 &lt; Table.Field2 THEN Table.Field3
///     WHEN Table.Field1 = Table.Field2 THEN Table.Field4
///     WHEN Table.Field1 &gt; Table.Field2 THEN Table.Field5
/// END</code>
/// </example>
public sealed class Switch<Case> : IBqlOperand, IBqlCreator, IBqlVerifier, IBqlAggregateOperand where Case : IBqlCase, new()
{
  private IBqlCase _case;

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
    if (this._case == null)
      this._case = (IBqlCase) new Case();
    SQLExpression exp1 = (SQLExpression) null;
    bool flag2 = flag1 & this._case.AppendExpression(ref exp1, graph, info, selection);
    sqlSwitch?.Case((SQLSwitch) exp1);
    exp = (SQLExpression) sqlSwitch;
    if (info.Parameters != null)
    {
      foreach (IBqlParameter parameter in info.Parameters)
        parameter.NullAllowed = true;
    }
    return flag2;
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
    value = (object) null;
  }
}
