// Decompiled with JetBrains decompiler
// Type: PX.Data.Validate`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// Raises the <tt>FieldVerifying</tt> event for the field to which the
/// <tt>PXFormula</tt> attribute is attached once the specified field changes.
/// </summary>
/// <typeparam name="V1">A data field.</typeparam>
/// <example>
/// The code below enables validation of the <tt>UnitRate</tt> field
/// on changes of the <tt>INUnit.UnitMultDiv</tt>.
/// <code>
/// [PXDBDecimal(6)]
/// [PXDefault(TypeCode.Decimal,"1.0")]
/// [PXUIField(DisplayName="Conversion Factor", Visibility=PXUIVisibility.Visible)]
/// [PXFormula(typeof(Validate&lt;INUnit.unitMultDiv&gt;))]
/// public virtual Decimal? UnitRate { get; set; }
/// </code>
/// </example>
public class Validate<V1> : Validate, IBqlCreator, IBqlVerifier
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
