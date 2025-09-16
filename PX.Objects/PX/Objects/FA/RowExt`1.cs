// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.RowExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FA;

public sealed class RowExt<Field> : IBqlOperand, IBqlCreator, IBqlVerifier where Field : IBqlField
{
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    info.Fields?.Add(typeof (Field));
    return true;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (item is BqlFormula.ItemContainer itemContainer)
      itemContainer.InvolvedFields.Add(typeof (Field));
    object valueExt = cache.GetValueExt(BqlFormula.ItemContainer.Unwrap(item), typeof (Field).Name);
    value = valueExt is PXFieldState ? ((PXFieldState) valueExt).Value : valueExt;
  }
}
