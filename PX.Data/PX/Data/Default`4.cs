// Decompiled with JetBrains decompiler
// Type: PX.Data.Default`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Raises the <tt>FieldDefaulting</tt> event for the field to which the
/// <tt>PXFormula</tt> attribute is attached once the specified fields change.
/// </summary>
/// <typeparam name="V1">A data field.</typeparam>
/// <typeparam name="V2">A data field.</typeparam>
/// <typeparam name="V3">A data field.</typeparam>
/// <typeparam name="V4">A data field.</typeparam>
/// <example><para>The code below shows the usage of Default&lt;,,,&gt; in the definition of a DAC field.</para>
/// <code title="Example" lang="CS">
/// [PXDefault]
/// [PXFormula(typeof(Default&lt;inventoryID, contractID, taskID, customerLocationID&gt;))]
/// public virtual Int32? ExpenseSubID { get; set; }</code>
/// </example>
public sealed class Default<V1, V2, V3, V4> : Default<V1, V2, V3>
  where V1 : IBqlField
  where V2 : IBqlField
  where V3 : IBqlField
  where V4 : IBqlField
{
  /// <exclude />
  public override bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    int num = base.AppendExpression(ref exp, graph, info, selection) ? 1 : 0;
    List<System.Type> fields = info.Fields;
    if (fields == null)
      return num != 0;
    // ISSUE: explicit non-virtual call
    __nonvirtual (fields.Add(typeof (V4)));
    return num != 0;
  }
}
