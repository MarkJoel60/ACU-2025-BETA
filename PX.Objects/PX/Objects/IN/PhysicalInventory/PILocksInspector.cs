// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PhysicalInventory.PILocksInspector
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.PhysicalInventory;

public class PILocksInspector
{
  protected int _siteId;

  protected virtual bool CacheLocks => true;

  public PILocksInspector(int siteId) => this._siteId = siteId;

  public virtual bool IsInventoryLocationIncludedInPI(
    int? inventoryID,
    int? locationID,
    string piID)
  {
    return inventoryID.HasValue && locationID.HasValue && this.GetLocks().Any<PILocks>((Func<PILocks, bool>) (l => l.PIID == piID && l.HasCollision(inventoryID.Value, locationID.Value)));
  }

  public virtual bool IsInventoryLocationLocked(
    int? inventoryID,
    int? locationID,
    string excludePIID,
    out string PIID)
  {
    PIID = (string) null;
    if (!inventoryID.HasValue || !locationID.HasValue)
      return false;
    PILocks piLocks = this.GetLocks().FirstOrDefault<PILocks>((Func<PILocks, bool>) (l => l.IsActive && l.PIID != excludePIID && l.HasCollision(inventoryID.Value, locationID.Value)));
    PIID = piLocks?.PIID;
    return piLocks != null;
  }

  public virtual bool IsInventoryLocationLocked(
    int? inventoryID,
    int? locationID,
    string excludePIID)
  {
    return this.IsInventoryLocationLocked(inventoryID, locationID, excludePIID, out string _);
  }

  protected virtual List<PILocks> GetLocks()
  {
    return this.CacheLocks ? PILocksInspector.GetSitePILocksFromSlot(this._siteId, new Func<List<PILocks>>(this.LoadExistingLocks)).Locks : this.LoadExistingLocks();
  }

  private static PILocksInspector.SitePILocks GetSitePILocksFromSlot(
    int siteID,
    Func<List<PILocks>> getPILocksFunc)
  {
    return PXDatabase.GetSlot<PILocksInspector.SitePILocks, Func<List<PILocks>>>($"{typeof (PILocksInspector.SitePILocks).FullName}~{siteID}", getPILocksFunc, PILocksInspector.GetTablesToWatch());
  }

  private static Type[] GetTablesToWatch()
  {
    return new Type[2]
    {
      typeof (INPIStatusItem),
      typeof (INPIStatusLoc)
    };
  }

  protected virtual List<PILocks> LoadExistingLocks()
  {
    PXGraph pxGraph = new PXGraph();
    PXSelectReadonly<INPIStatusItem, Where<INPIStatusItem.siteID, Equal<Required<INPIStatusItem.siteID>>>, OrderBy<Asc<INPIStatusItem.pIID>>> pxSelectReadonly1 = new PXSelectReadonly<INPIStatusItem, Where<INPIStatusItem.siteID, Equal<Required<INPIStatusItem.siteID>>>, OrderBy<Asc<INPIStatusItem.pIID>>>(pxGraph);
    Dictionary<string, List<INPIStatusItem>> piIdDictionary1;
    using (new PXFieldScope(((PXSelectBase) pxSelectReadonly1).View, new Type[5]
    {
      typeof (INPIStatusItem.recordID),
      typeof (INPIStatusItem.inventoryID),
      typeof (INPIStatusItem.pIID),
      typeof (INPIStatusItem.excluded),
      typeof (INPIStatusItem.active)
    }))
      piIdDictionary1 = this.ToPiIdDictionary<INPIStatusItem>(GraphHelper.RowCast<INPIStatusItem>((IEnumerable) ((PXSelectBase<INPIStatusItem>) pxSelectReadonly1).Select(new object[1]
      {
        (object) this._siteId
      })));
    PXSelectReadonly<INPIStatusLoc, Where<INPIStatusLoc.siteID, Equal<Required<INPIStatusLoc.siteID>>>, OrderBy<Asc<INPIStatusLoc.pIID>>> pxSelectReadonly2 = new PXSelectReadonly<INPIStatusLoc, Where<INPIStatusLoc.siteID, Equal<Required<INPIStatusLoc.siteID>>>, OrderBy<Asc<INPIStatusLoc.pIID>>>(pxGraph);
    Dictionary<string, List<INPIStatusLoc>> piIdDictionary2;
    using (new PXFieldScope(((PXSelectBase) pxSelectReadonly2).View, new Type[5]
    {
      typeof (INPIStatusLoc.recordID),
      typeof (INPIStatusLoc.locationID),
      typeof (INPIStatusLoc.pIID),
      typeof (INPIStatusLoc.excluded),
      typeof (INPIStatusLoc.active)
    }))
      piIdDictionary2 = this.ToPiIdDictionary<INPIStatusLoc>(GraphHelper.RowCast<INPIStatusLoc>((IEnumerable) ((PXSelectBase<INPIStatusLoc>) pxSelectReadonly2).Select(new object[1]
      {
        (object) this._siteId
      })));
    if (piIdDictionary1.Count != piIdDictionary2.Count)
      PXTrace.WriteError("Inconsistent DB data: The system cannot find the pair record (lock) in one of the following tables: INPIStatusItem, INPIStatusLoc.");
    List<PILocks> piLocksList = new List<PILocks>();
    foreach (KeyValuePair<string, List<INPIStatusItem>> keyValuePair in piIdDictionary1)
    {
      string key = keyValuePair.Key;
      List<INPIStatusLoc> locationLocks = piIdDictionary2[key];
      piLocksList.Add(new PILocks(key, (IEnumerable<INPIStatusItem>) keyValuePair.Value, (IEnumerable<INPIStatusLoc>) locationLocks));
    }
    return piLocksList;
  }

  protected Dictionary<string, List<TLock>> ToPiIdDictionary<TLock>(IEnumerable<TLock> sortedLocks) where TLock : class, IBqlTable, IPILock, new()
  {
    string key = (string) null;
    Dictionary<string, List<TLock>> piIdDictionary = new Dictionary<string, List<TLock>>();
    foreach (TLock sortedLock in sortedLocks)
    {
      string piid = sortedLock.PIID;
      if (key != piid)
      {
        piIdDictionary.Add(piid, new List<TLock>());
        key = piid;
      }
      piIdDictionary[key].Add(sortedLock);
    }
    return piIdDictionary;
  }

  private class SitePILocks : IPrefetchable<Func<List<PILocks>>>, IPXCompanyDependent
  {
    public List<PILocks> Locks { get; private set; }

    public void Prefetch(Func<List<PILocks>> getPILocksFunc) => this.Locks = getPILocksFunc();
  }
}
