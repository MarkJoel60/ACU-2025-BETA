// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.Relational.BC.SharedChildOverrideGraphExtBC`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR.Extensions.Relational.BC;

[PXInternalUseOnly]
public abstract class SharedChildOverrideGraphExtBC<Extension, Graph, FieldFrom, FieldTo> : 
  PXGraphExtension<Extension, Graph>
  where Extension : PXGraphExtension<Graph>
  where Graph : PXGraph
  where FieldFrom : class, IBqlField
  where FieldTo : class, IBqlField
{
  protected void _(Events.FieldUpdating<FieldFrom> e)
  {
    bool? newValue1 = ((Events.FieldUpdatingBase<Events.FieldUpdating<FieldFrom>>) e).NewValue as bool?;
    string newValue2 = ((Events.FieldUpdatingBase<Events.FieldUpdating<FieldFrom>>) e).NewValue as string;
    if (newValue1.HasValue)
    {
      PXCache cache = ((Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<FieldFrom>>) e).Cache;
      object row = e.Row;
      bool? nullable = newValue1;
      // ISSUE: variable of a boxed type
      __Boxed<bool?> local = (ValueType) (nullable.HasValue ? new bool?(!nullable.GetValueOrDefault()) : new bool?());
      cache.SetValueExt<FieldTo>(row, (object) local);
    }
    else
    {
      bool result;
      if (newValue2 == null || !bool.TryParse(newValue2, out result))
        return;
      ((Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<FieldFrom>>) e).Cache.SetValueExt<FieldTo>(e.Row, (object) !result);
    }
  }
}
