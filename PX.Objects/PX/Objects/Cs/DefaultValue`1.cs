// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DefaultValue`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS;

public class DefaultValue<Field> : IBqlCreator, IBqlVerifier, IBqlOperand where Field : IBqlField
{
  protected object _DefaultValue = PXCache.NotSetValue;

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (this._DefaultValue == PXCache.NotSetValue && graph != null)
    {
      PXCache cach = graph.Caches[BqlCommand.GetItemType(typeof (Field))];
      cach.RaiseFieldDefaulting<Field>((object) null, ref this._DefaultValue);
      PXDefaultAttribute.SetDefault<Field>(cach, (object) null);
    }
    return true;
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this._DefaultValue == PXCache.NotSetValue)
    {
      PXCache cach = cache.Graph.Caches[BqlCommand.GetItemType(typeof (Field))];
      cach.RaiseFieldDefaulting<Field>((object) null, ref this._DefaultValue);
      PXDefaultAttribute.SetDefault<Field>(cach, (object) null);
    }
    if (this._DefaultValue == PXCache.NotSetValue)
      return;
    value = this._DefaultValue;
  }
}
