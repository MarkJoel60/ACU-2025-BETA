// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PhysicalInventory.PICollision
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.PhysicalInventory;

public class PICollision
{
  private const int c_itemsLoggingLimit = 50;

  public ICollection<int> InventoryItemIds { get; set; }

  public ICollection<int> LocationIds { get; set; }

  public string PIID { get; set; }

  public bool AllInventory { get; set; }

  public bool AllLocations { get; set; }

  public void Notify(PXGraph graph, string siteCD)
  {
    if (this.AllInventory && this.AllLocations)
      PXTrace.WriteError(PXMessages.Localize("The system cannot run the current PI in the {1} warehouse because the full PI {0} is in progress."), new object[2]
      {
        (object) this.PIID,
        (object) siteCD
      });
    else if (this.AllLocations)
      PXTrace.WriteError(PXMessages.Localize("The system cannot run the current PI in warehouse {1} because the PI has intersecting entities with PI {0}, which is in progress. Intersecting inventory items: {2}."), new object[3]
      {
        (object) this.PIID,
        (object) siteCD,
        (object) string.Join(", ", this.ReadIdsToString(graph, this.InventoryItemIds, new Func<PXGraph, IEnumerable<int>, IEnumerable<string>>(this.ReadInventoryCds)))
      });
    else if (this.AllInventory)
      PXTrace.WriteError(PXMessages.Localize("The system cannot run the current PI in warehouse {1} because the PI has intersecting entities with PI {0}, which is in progress. Intersecting locations: {2}."), new object[3]
      {
        (object) this.PIID,
        (object) siteCD,
        (object) string.Join(", ", this.ReadIdsToString(graph, this.LocationIds, new Func<PXGraph, IEnumerable<int>, IEnumerable<string>>(this.ReadLocationCds)))
      });
    else
      PXTrace.WriteError(PXMessages.Localize("The system cannot run the current PI in warehouse {1} because the PI has intersecting entities with PI {0}, which is in progress. Intersecting inventory items: {2}; intersecting locations: {3}."), new object[4]
      {
        (object) this.PIID,
        (object) siteCD,
        (object) string.Join(", ", this.ReadIdsToString(graph, this.InventoryItemIds, new Func<PXGraph, IEnumerable<int>, IEnumerable<string>>(this.ReadInventoryCds))),
        (object) string.Join(", ", this.ReadIdsToString(graph, this.LocationIds, new Func<PXGraph, IEnumerable<int>, IEnumerable<string>>(this.ReadLocationCds)))
      });
  }

  protected IEnumerable<string> ReadInventoryCds(PXGraph graph, IEnumerable<int> ids)
  {
    PXSelect<InventoryItem, Where<InventoryItem.inventoryID, In<Required<InventoryItem.inventoryID>>>> pxSelect = new PXSelect<InventoryItem, Where<InventoryItem.inventoryID, In<Required<InventoryItem.inventoryID>>>>(graph);
    using (new PXFieldScope(((PXSelectBase) pxSelect).View, new Type[3]
    {
      typeof (InventoryItem.inventoryID),
      typeof (InventoryItem.inventoryCD),
      typeof (InventoryItem.stkItem)
    }))
      return GraphHelper.RowCast<InventoryItem>((IEnumerable) ((PXSelectBase<InventoryItem>) pxSelect).Select(new object[1]
      {
        (object) ids.ToArray<int>()
      })).Select<InventoryItem, string>((Func<InventoryItem, string>) (i => i.InventoryCD));
  }

  protected IEnumerable<string> ReadLocationCds(PXGraph graph, IEnumerable<int> ids)
  {
    PXSelect<INLocation, Where<INLocation.locationID, In<Required<INLocation.locationID>>>> pxSelect = new PXSelect<INLocation, Where<INLocation.locationID, In<Required<INLocation.locationID>>>>(graph);
    using (new PXFieldScope(((PXSelectBase) pxSelect).View, new Type[3]
    {
      typeof (INLocation.locationID),
      typeof (INLocation.siteID),
      typeof (INLocation.locationCD)
    }))
      return GraphHelper.RowCast<INLocation>((IEnumerable) ((PXSelectBase<INLocation>) pxSelect).Select(new object[1]
      {
        (object) ids.ToArray<int>()
      })).Select<INLocation, string>((Func<INLocation, string>) (i => i.LocationCD));
  }

  protected string ReadIdsToString(
    PXGraph graph,
    ICollection<int> ids,
    Func<PXGraph, IEnumerable<int>, IEnumerable<string>> readFunc)
  {
    string str = string.Join(", ", readFunc(graph, ids.Take<int>(50)));
    return ids.Count > 50 ? str + $", and {ids.Count - 50} more..." : str;
  }
}
