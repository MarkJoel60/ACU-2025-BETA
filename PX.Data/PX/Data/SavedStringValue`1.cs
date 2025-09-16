// Decompiled with JetBrains decompiler
// Type: PX.Data.SavedStringValue`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>Includes in a BQL command a previously saved string value. The class can be used if current cannot be accessed easily. For example, the class can be used
/// in some attributes. The value must be saved in the corresponded slot using PXContext.SetSlot function.</summary>
/// <typeparam name="Field">The field whose current value is recovered.</typeparam>
/// <example>
/// The code below shows the definition of a DAC field.
/// <code title="" description="" lang="CS">
/// [PXDBCalced(
///     typeof(Left&lt;INSubItem.subItemCD, CurrentValue&lt;Dimension.segments&gt;&gt;),
///     typeof(string))]
/// public override string SubItemCD { get; set; }</code></example>
public sealed class SavedStringValue<Field> : IBqlOperand, IBqlCreator, IBqlVerifier where Field : IBqlField
{
  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) PXContext.GetSlot<string>(typeof (Field).FullName);
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
    object slot = (object) PXContext.GetSlot<string>(typeof (Field).FullName);
    string name = typeof (Field).Name;
    object obj = slot;
    PXCommandPreparingEventArgs.FieldDescription fieldDescription;
    ref PXCommandPreparingEventArgs.FieldDescription local = ref fieldDescription;
    cach.RaiseCommandPreparing(name, (object) null, obj, PXDBOperation.Select, (System.Type) null, out local);
    if (fieldDescription == null || fieldDescription.Expr == null)
      return false;
    exp = (SQLExpression) new SQLConst(slot);
    return true;
  }
}
