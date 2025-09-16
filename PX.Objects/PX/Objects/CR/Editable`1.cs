// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Editable`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class Editable<Field> : IBqlCreator, IBqlVerifier, IBqlOperand where Field : IBqlField
{
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return false;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    PXCache cach = cache.Graph.Caches[BqlCommand.GetItemType(typeof (Field))];
    object obj = (object) null;
    if (cach.GetItemType().IsAssignableFrom(cache.GetItemType()))
      obj = BqlFormula.ItemContainer.Unwrap(item);
    PXFieldState stateExt = cach.GetStateExt<Field>(obj) as PXFieldState;
    value = (object) (bool) (stateExt == null ? 1 : (!stateExt.Enabled || !stateExt.Visible ? 0 : (!stateExt.IsReadOnly ? 1 : 0)));
  }
}
