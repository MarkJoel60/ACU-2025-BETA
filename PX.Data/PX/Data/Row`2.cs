// Decompiled with JetBrains decompiler
// Type: PX.Data.Row`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
/// <summary>
/// Returns the value of the specified field and creates an additional
/// dependency for the formula (on the provided dependency field).
/// Each time the dependency field is updated, the formula is recalculated.
/// The formula also depends on all other fields referenced in the formula.
/// </summary>
/// <typeparam name="Field">The field whose value is returned.</typeparam>
/// <typeparam name="DependentField">The dependent field.</typeparam>
/// <example>
/// <code>
/// [PXFormula(
///     typeof(Mult&lt;Row&lt;POLine.baseOrderQty, POLine.orderQty&gt;, POLine.unitWeight&gt;),
///     typeof(SumCalc&lt;POOrder.orderWeight&gt;))]
/// public virtual Decimal? ExtWeight { get; set; }
/// </code>
/// </example>
[Obsolete("Use Row<TField>.WithDependency<TDependentField> class instead")]
public sealed class Row<Field, DependentField> : IBqlOperand, IBqlCreator, IBqlVerifier
  where Field : IBqlField
  where DependentField : IBqlField
{
  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    info.Fields?.Add(typeof (DependentField));
    return true;
  }

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (item is BqlFormula.ItemContainer itemContainer)
      itemContainer.InvolvedFields.Add(typeof (DependentField));
    value = cache.GetValue(BqlFormula.ItemContainer.Unwrap(item), typeof (Field).Name);
  }
}
