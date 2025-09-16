// Decompiled with JetBrains decompiler
// Type: PX.Data.Default`1
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
/// <tt>PXFormula</tt> attribute is attached once the specified field changes.
/// </summary>
/// <typeparam name="V1">A data field.</typeparam>
/// <example><para>The code below shows the usage of Default&lt;&gt; in the definition of a DAC field.</para>
/// <code title="Example" lang="CS">
/// [PXFormula(typeof(Default&lt;NotificationSource.setupID&gt;))]
/// public virtual string Format { get; set; }</code>
/// </example>
public class Default<V1> : Default, IBqlCreator, IBqlVerifier where V1 : IBqlField
{
  /// <exclude />
  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    info.Fields?.Add(typeof (V1));
    return true;
  }

  /// <exclude />
  public virtual void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) null;
  }
}
