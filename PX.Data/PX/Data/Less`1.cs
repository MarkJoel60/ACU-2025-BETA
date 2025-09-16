// Decompiled with JetBrains decompiler
// Type: PX.Data.Less`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Checks if the preceding operand is less than <tt>Operand</tt>.
/// </summary>
/// <typeparam name="Operand">The operand to compare to.</typeparam>
/// <example><para>The code below creates a dynamic data view that selects financial periods satifying the condition. The corresponding SQL code is also shown.</para>
/// 	<code title="Example" lang="CS">
/// PXSelectBase selectperiod = new PXSelect&lt;FinPeriod,
///     Where&lt;FinPeriod.startDate, Less&lt;FinPeriod.endDate&gt;,
///         And&lt;FinPeriod.finYear, Equal&lt;Required&lt;FinPeriod.finYear&gt;&gt;&gt;&gt;,
///     OrderBy&lt;Desc&lt;FinPeriod.periodNbr&gt;&gt;&gt;(this);</code>
/// 	<code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM FinPeriod
/// WHERE ( FinPeriod.StartDate &lt; FinPeriod.EndDate
///     AND FinPeriod.FinYear = [value] )
/// ORDER BY FinPeriod.PeriodNbr DESC</code>
/// </example>
public class Less<Operand> : ComparisonBase<Operand> where Operand : IBqlOperand
{
  protected override bool? verifyCore(object val, object value)
  {
    return new bool?(this.collationComparer.Compare(val, value) < 0);
  }

  protected override bool isBypass(object val) => !(val is IComparable);

  /// <exclude />
  public Less()
    : base("<", true)
  {
  }
}
