// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ManageSalesAllocationsExt.InventoryLinkFilterExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ManageSalesAllocationsExt;

public class InventoryLinkFilterExt : 
  InventoryLinkFilterExtensionBase<ManageSalesAllocations, SalesAllocationsFilter, SalesAllocationsFilter.inventoryID>
{
  [PXMergeAttributes]
  [StockItem(IsKey = true)]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noSales>>), "The inventory item is {0}.", new Type[] {typeof (PX.Objects.IN.InventoryItem.itemStatus)}, ShowWarning = true)]
  protected override void _(
    PX.Data.Events.CacheAttached<InventoryLinkFilter.inventoryID> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SalesAllocationsFilter> e)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SalesAllocationsFilter>>) e).Cache.ObjectsEqual<SalesAllocationsFilter.inventoryID>((object) e.Row, (object) e.OldRow))
      return;
    ((PXSelectBase) this.SelectedItems).Cache.Clear();
    ((PXSelectBase) this.SelectedItems).Cache.ClearQueryCache();
  }

  [PXOverride]
  public PXSelectBase<SalesAllocation> CreateBaseQuery(
    SalesAllocationsFilter filter,
    List<object> parameters,
    Func<SalesAllocationsFilter, List<object>, PXSelectBase<SalesAllocation>> base_CreateBaseQuery)
  {
    PXSelectBase<SalesAllocation> baseQuery = base_CreateBaseQuery(filter, parameters);
    int?[] array = this.GetSelectedEntities(filter).ToArray<int?>();
    if (array.Length != 0)
    {
      baseQuery.WhereAnd<Where<BqlOperand<SalesAllocation.inventoryID, IBqlInt>.IsIn<P.AsInt>>>();
      parameters.Add((object) array);
    }
    return baseQuery;
  }

  public class descr : 
    InventoryLinkFilterExtensionBase<ManageSalesAllocations, SalesAllocationsFilter, SalesAllocationsFilter.inventoryID>.AttachedInventoryDescription<InventoryLinkFilterExt.descr>
  {
  }
}
