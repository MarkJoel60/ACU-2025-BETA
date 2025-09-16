// Decompiled with JetBrains decompiler
// Type: PX.Data.CurrentValue`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Equivalent to the <tt>Current</tt> parameter, but is used in the
/// <tt>PXProjection</tt> attribute.
/// </summary>
/// <typeparam name="Field">The field whose current value is inserted.</typeparam>
/// <example>
/// The code below shows the definition of a DAC field.
/// <code>
/// [PXDBCalced(
///     typeof(Left&lt;INSubItem.subItemCD, CurrentValue&lt;Dimension.segments&gt;&gt;),
///     typeof(string))]
/// public override string SubItemCD { get; set; }
/// </code>
/// </example>
public sealed class CurrentValue<Field> : IBqlOperand, IBqlCreator, IBqlVerifier where Field : IBqlField
{
  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (typeof (Field) != cache.GetItemType())
      cache = cache.Graph.Caches[BqlCommand.GetItemType(typeof (Field))];
    value = cache.GetValue<Field>(cache.Current);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (graph == null || !info.BuildExpression)
      return true;
    PXMutableCollection.AddMutableItem((IBqlCreator) this);
    PXCache cach = graph.Caches[BqlCommand.GetItemType(typeof (Field))];
    object v = cach.GetValue<Field>(cach.Current);
    PXCommandPreparingEventArgs.FieldDescription description;
    cach.RaiseCommandPreparing(typeof (Field).Name, (object) null, v, PXDBOperation.Select, (System.Type) null, out description);
    exp = (SQLExpression) new SQLConst(v);
    if (description != null)
      (exp as SQLConst).SetDBType(description.DataType);
    return true;
  }
}
