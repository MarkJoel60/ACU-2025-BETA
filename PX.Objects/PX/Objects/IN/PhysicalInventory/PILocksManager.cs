// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PhysicalInventory.PILocksManager
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.PhysicalInventory;

public class PILocksManager : PILocksInspector
{
  protected PXGraph _graph;
  protected PXSelectBase<INPIStatusItem> _inPIStatusItem;
  protected PXSelectBase<INPIStatusLoc> _inPIStatusLocation;
  protected string _piId;

  protected override bool CacheLocks => false;

  public PILocksManager(
    PXGraph graph,
    PXSelectBase<INPIStatusItem> inPIStatusItem,
    PXSelectBase<INPIStatusLoc> inPIStatusLocation,
    int siteId,
    string piId)
    : base(siteId)
  {
    this._graph = graph;
    this._inPIStatusItem = inPIStatusItem;
    this._inPIStatusLocation = inPIStatusLocation;
    this._piId = piId;
  }

  public virtual void Lock(
    bool fullItemsLock,
    ICollection<int> inventoryItemIds,
    bool fullLocationsLock,
    ICollection<int> locationIds,
    string siteCD)
  {
    List<PICollision> collisions = this.GetCollisions(this.GetLocks(), fullItemsLock, inventoryItemIds, fullLocationsLock, locationIds);
    if (collisions.Any<PICollision>())
    {
      foreach (PICollision piCollision in collisions)
        piCollision.Notify(this._graph, siteCD);
      throw new PXException("The system cannot run the PI because it has intersecting entities with PI {0}, which is in progress in warehouse {1}. See Trace for details.", new object[2]
      {
        (object) string.Join(", ", collisions.Select<PICollision, string>((Func<PICollision, string>) (c => c.PIID))),
        (object) siteCD
      });
    }
    if (fullItemsLock)
      this.InsertInventoryLock(new int?());
    foreach (int inventoryItemId in (IEnumerable<int>) inventoryItemIds)
      this.InsertInventoryLock(new int?(inventoryItemId), fullItemsLock);
    if (fullLocationsLock)
      this.InsertLocationLock(new int?());
    foreach (int locationId in (IEnumerable<int>) locationIds)
      this.InsertLocationLock(new int?(locationId), fullLocationsLock);
  }

  public virtual void UnlockInventory(bool deleteLock = true)
  {
    ((PXSelectBase) this._inPIStatusItem).Cache.Clear();
    foreach (PXResult<INPIStatusItem> pxResult in PXSelectBase<INPIStatusItem, PXSelect<INPIStatusItem, Where<INPIStatusItem.pIID, Equal<Required<INPIStatusItem.pIID>>>>.Config>.Select(this._graph, new object[1]
    {
      (object) this._piId
    }))
    {
      if (deleteLock)
      {
        this._inPIStatusItem.Delete(PXResult<INPIStatusItem>.op_Implicit(pxResult));
      }
      else
      {
        INPIStatusItem copy = PXCache<INPIStatusItem>.CreateCopy(PXResult<INPIStatusItem>.op_Implicit(pxResult));
        copy.Active = new bool?(false);
        this._inPIStatusItem.Update(copy);
      }
    }
    ((PXSelectBase) this._inPIStatusLocation).Cache.Clear();
    foreach (PXResult<INPIStatusLoc> pxResult in PXSelectBase<INPIStatusLoc, PXSelect<INPIStatusLoc, Where<INPIStatusLoc.pIID, Equal<Required<INPIStatusLoc.pIID>>>>.Config>.Select(this._graph, new object[1]
    {
      (object) this._piId
    }))
    {
      if (deleteLock)
      {
        this._inPIStatusLocation.Delete(PXResult<INPIStatusLoc>.op_Implicit(pxResult));
      }
      else
      {
        INPIStatusLoc copy = PXCache<INPIStatusLoc>.CreateCopy(PXResult<INPIStatusLoc>.op_Implicit(pxResult));
        copy.Active = new bool?(false);
        this._inPIStatusLocation.Update(copy);
      }
    }
  }

  protected virtual void InsertInventoryLock(int? inventoryId, bool excluded = false)
  {
    if (!inventoryId.HasValue && excluded)
      throw new PXArgumentException(nameof (excluded));
    this._inPIStatusItem.Insert(new INPIStatusItem()
    {
      SiteID = new int?(this._siteId),
      PIID = this._piId,
      InventoryID = inventoryId,
      Excluded = new bool?(excluded)
    });
  }

  protected virtual void InsertLocationLock(int? locationId, bool excluded = false)
  {
    if (!locationId.HasValue && excluded)
      throw new PXArgumentException(nameof (excluded));
    this._inPIStatusLocation.Insert(new INPIStatusLoc()
    {
      SiteID = new int?(this._siteId),
      PIID = this._piId,
      LocationID = locationId,
      Excluded = new bool?(excluded)
    });
  }

  protected virtual List<PICollision> GetCollisions(
    List<PILocks> existingLocks,
    bool fullItemsLock,
    ICollection<int> inventoryItemIds,
    bool fullLocationsLock,
    ICollection<int> locationIds)
  {
    List<PICollision> collisions = new List<PICollision>();
    foreach (PILocks existingLock in existingLocks)
    {
      PICollision piCollision = existingLock.Intersect(fullItemsLock, inventoryItemIds, fullLocationsLock, locationIds);
      if (piCollision != null)
        collisions.Add(piCollision);
    }
    return collisions;
  }
}
