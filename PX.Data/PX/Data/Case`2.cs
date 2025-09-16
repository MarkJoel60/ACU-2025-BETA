// Decompiled with JetBrains decompiler
// Type: PX.Data.Case`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Specifies a condition that should be evaluated in the <tt>Switch</tt> clause, and
/// the expression, which should be returned if the condition is satisfied.
/// </summary>
/// <typeparam name="Where_">The conditional expression, the <tt>Where</tt>
/// class.</typeparam>
/// <typeparam name="Operand">The value specified as field, constant, or
/// function.</typeparam>
/// <remarks>
/// <para>The condition is set by the <tt>Where</tt> clause. In the translation to SQL,
/// <tt>Case</tt> is replaced with <tt>WHEN [conditions] THEN [expression]</tt>.</para>
/// <para>You can use fluent BQL <see cref="T:PX.Data.BQL.BqlOperand`2.When`1" /> instead.</para>
/// </remarks>
/// <seealso cref="T:PX.Data.Switch`1"></seealso>
/// <seealso cref="T:PX.Data.Switch`2"></seealso>
/// <example><para>The code below shows the definition of a DAC field calculated at run time.</para>
/// <code title="Example" lang="CS">
/// [PXDBInt]
/// [PXUIField(DisplayName = "Time")]
/// [PXFormula(typeof(
///     Switch&lt;
///         Case&lt;Where&lt;EPActivity.trackTime, NotEqual&lt;True&gt;&gt;, int0&gt;,
///         EPActivity.timeSpent&gt;))]
/// public virtual Int32? TimeSpent { ... }</code>
/// </example>
public class Case<Where_, Operand> : Case2<Where_, Operand>
  where Where_ : IBqlWhere, new()
  where Operand : IBqlOperand
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
