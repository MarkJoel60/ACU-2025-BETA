// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMUnitSelectorAttrubute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.PM;

public class PMUnitSelectorAttrubute : PXCustomSelectorAttribute
{
  protected Type inventory;

  public PMUnitSelectorAttrubute(Type inventory, Type searchType)
    : base(searchType)
  {
    this.inventory = inventory;
  }

  protected virtual IEnumerable GetRecords()
  {
    int? nullable1 = (int?) this._Graph.Caches[((PXSelectorAttribute) this)._CacheType].GetValue(PXView.Currents == null || PXView.Currents.Length == 0 ? this._Graph.Caches[((PXSelectorAttribute) this)._CacheType].Current : PXView.Currents[0], this.inventory.Name);
    if (nullable1.HasValue)
    {
      int? nullable2 = nullable1;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(nullable2.GetValueOrDefault() == emptyInventoryId & nullable2.HasValue))
        return (IEnumerable) ((PXSelectBase<INUnit>) new PXSelectGroupBy<INUnit, Where<INUnit.unitType, Equal<INUnitType.inventoryItem>, And<INUnit.inventoryID, Equal<Required<INUnit.inventoryID>>>>, Aggregate<GroupBy<INUnit.fromUnit>>>(this._Graph)).Select(new object[1]
        {
          (object) nullable1
        });
    }
    return (IEnumerable) ((PXSelectBase<INUnit>) new PXSelectGroupBy<INUnit, Where<INUnit.unitType, Equal<INUnitType.global>>, Aggregate<GroupBy<INUnit.fromUnit>>>(this._Graph)).Select(Array.Empty<object>());
  }
}
