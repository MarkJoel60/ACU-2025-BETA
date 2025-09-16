// Decompiled with JetBrains decompiler
// Type: PX.Data.Parent`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Returns the value of the specified field from the parent data record.
/// The parent data record is defined by the <see cref="T:PX.Data.PXParentAttribute">PXParet</see>
/// attribute.
/// </summary>
/// <typeparam name="Field">The field of the parent data record.</typeparam>
/// <example>
/// <code>
/// [PXUnboundFormula(
///     typeof(Switch&lt;
///         Case&lt;Where&lt;SOLine.operation, Equal&lt;Parent&lt;SOOrder.defaultOperation&gt;&gt;,
///                  And&lt;SOLine.lineType, NotEqual&lt;SOLineType.miscCharge&gt;&gt;&gt;,
///              SOLine.orderQty&gt;,
///         decimal0&gt;),
///     typeof(SumCalc&lt;SOOrder.orderQty&gt;))]
/// public virtual decimal? OrderQty { get; set; }
/// </code>
/// </example>
public sealed class Parent<Field> : IBqlCreator, IBqlVerifier, IBqlOperand where Field : IBqlOperand
{
  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
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
    if (typeof (IBqlField).IsAssignableFrom(typeof (Field)) && typeof (Field).IsNested)
    {
      System.Type itemType = BqlCommand.GetItemType(typeof (Field));
      PXCache cach = cache.Graph.Caches[itemType];
      object data = PXParentAttribute.SelectParent(cache, BqlFormula.ItemContainer.Unwrap(item), itemType);
      value = cach.GetValue(data, typeof (Field).Name);
    }
    else
      value = (object) null;
  }
}
