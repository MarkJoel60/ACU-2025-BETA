// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUnitXRefAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

public class INUnitXRefAttribute(Type inventoryIDField) : INUnitAttribute(inventoryIDField)
{
  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    string uom = (string) e.NewValue;
    if (string.IsNullOrEmpty(uom) || sender.Graph.Caches[typeof (INUnit)].Cached.Cast<INUnit>().Any<INUnit>((Func<INUnit, bool>) (u => object.Equals((object) u.FromUnit, (object) uom))))
      return;
    if (PXSelectBase<INUnit, PXSelectReadonly<INUnit, Where<INUnit.fromUnit, Equal<Required<INUnit.fromUnit>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
    {
      (object) uom
    }).Count > 0)
      return;
    base.FieldVerifying(sender, e);
  }
}
