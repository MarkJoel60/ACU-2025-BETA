// Decompiled with JetBrains decompiler
// Type: PX.Data.Case`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Specifies a single condition to evaluate in the <tt>Switch</tt> clause
/// and the expression to return if the condition is satisfied, and allows
/// attaching more <tt>Case</tt> clauses.
/// </summary>
/// <typeparam name="Where_">The conditional expression, the <tt>Where</tt>
/// class.</typeparam>
/// <typeparam name="Operand">The value specified as field, constant, or
/// function.</typeparam>
/// <typeparam name="NextCase">The next <tt>Case</tt> clause.</typeparam>
/// <seealso cref="T:PX.Data.Switch`1" />
/// <seealso cref="T:PX.Data.Switch`2" />
/// <example>
/// The code below shows the usage of <tt>Switch</tt> and <tt>Case</tt>
/// classes in <tt>OrderBy</tt>. Because of the expression with
/// <tt>Switch</tt>, the rows with the <tt>FABookBalance.UpdateGL</tt> field equal
/// to <tt>false</tt> will be placed before the rows with <tt>FABookBalance.UpdateGL</tt>
/// equal to <tt>true</tt>.
/// <code>
/// [PXDefault(typeof(
///     Search2&lt;FinPeriod.finPeriodID,
///         InnerJoin&lt;FABookBalance, On&lt;FABookBalance.deprFromPeriod, LessEqual&lt;FinPeriod.finPeriodID&gt;&gt;&gt;,
///         Where&lt;FABookBalance.assetID, Equal&lt;Current&lt;FixedAsset.assetID&gt;&gt;,
///             And&lt;FinPeriod.endDate, Greater&lt;FinPeriod.startDate&gt;&gt;&gt;,
///         OrderBy&lt;Asc&lt;Switch&lt;Case&lt;Where&lt;FABookBalance.updateGL, Equal&lt;True&gt;&gt;, int1&gt;, int0&gt;,
///             Desc&lt;FinPeriod.finPeriodID&gt;&gt;&gt;&gt;))]
/// public virtual string PeriodID
/// { ... }
/// </code>
/// </example>
public class Case<Where_, Operand, NextCase> : Case2<Where_, Operand, NextCase>
  where Where_ : IBqlWhere, new()
  where Operand : IBqlOperand
  where NextCase : IBqlCase, new()
{
  /// <exclude />
  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this.FieldDefaulting(cache, item);
    base.Verify(cache, item, pars, ref result, ref value);
  }
}
