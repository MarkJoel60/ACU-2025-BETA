// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PhysicalInventory.PILocks
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.PhysicalInventory;

public class PILocks
{
  protected string _piId;
  protected bool _isActive;
  protected bool _fullItemsLock;
  protected HashSet<int> _itemsCollection;
  protected bool _fullLocationsLock;
  protected HashSet<int> _locationsCollection;

  public string PIID => this._piId;

  public bool IsActive => this._isActive;

  public PILocks(
    string piId,
    IEnumerable<INPIStatusItem> itemLocks,
    IEnumerable<INPIStatusLoc> locationLocks)
  {
    this._piId = piId;
    bool isActive1;
    this._itemsCollection = this.ParseLocks<INPIStatusItem>(itemLocks, (Func<INPIStatusItem, int?>) (r => r.InventoryID), out this._fullItemsLock, out isActive1);
    bool isActive2;
    this._locationsCollection = this.ParseLocks<INPIStatusLoc>(locationLocks, (Func<INPIStatusLoc, int?>) (r => r.LocationID), out this._fullLocationsLock, out isActive2);
    this._isActive = isActive1 | isActive2;
  }

  public virtual bool HasCollision(int inventoryID, int locationID)
  {
    return (this._fullItemsLock ^ this._itemsCollection.Contains(inventoryID)) & (this._fullLocationsLock ^ this._locationsCollection.Contains(locationID));
  }

  public virtual PICollision Intersect(
    bool fullItemsLock,
    ICollection<int> inventoryItems,
    bool fullLocationsLock,
    ICollection<int> locations)
  {
    bool fullIntersection1;
    List<int> intersection1 = this.GetIntersection(this._fullItemsLock, (ICollection<int>) this._itemsCollection, fullItemsLock, inventoryItems, out fullIntersection1);
    if (!fullIntersection1 && !intersection1.Any<int>())
      return (PICollision) null;
    bool fullIntersection2;
    List<int> intersection2 = this.GetIntersection(this._fullLocationsLock, (ICollection<int>) this._locationsCollection, fullLocationsLock, locations, out fullIntersection2);
    if (!fullIntersection2 && !intersection2.Any<int>())
      return (PICollision) null;
    return new PICollision()
    {
      PIID = this._piId,
      AllInventory = fullIntersection1,
      AllLocations = fullIntersection2,
      InventoryItemIds = (ICollection<int>) intersection1,
      LocationIds = (ICollection<int>) intersection2
    };
  }

  protected List<int> GetIntersection(
    bool fullLock1,
    ICollection<int> collection1,
    bool fullLock2,
    ICollection<int> collection2,
    out bool fullIntersection)
  {
    fullIntersection = false;
    if (fullLock1 == fullLock2)
    {
      if (!fullLock1)
        return collection1.Where<int>((Func<int, bool>) (id => collection2.Contains(id))).ToList<int>();
      fullIntersection = true;
      return new List<int>(0);
    }
    ICollection<int> source = fullLock1 ? collection2 : collection1;
    ICollection<int> excludeCollection = fullLock1 ? collection1 : collection2;
    Func<int, bool> predicate = (Func<int, bool>) (id => !excludeCollection.Contains(id));
    return source.Where<int>(predicate).ToList<int>();
  }

  protected HashSet<int> ParseLocks<TLock>(
    IEnumerable<TLock> items,
    Func<TLock, int?> idSelector,
    out bool fullLock,
    out bool isActive)
    where TLock : IPILock
  {
    bool emptyLockExists = items.Any<TLock>((Func<TLock, bool>) (i => !idSelector(i).HasValue));
    fullLock = emptyLockExists;
    isActive = items.Any<TLock>((Func<TLock, bool>) (i => i.Active.GetValueOrDefault()));
    return items.Where<TLock>((Func<TLock, bool>) (i =>
    {
      bool? excluded = i.Excluded;
      bool flag = emptyLockExists;
      return excluded.GetValueOrDefault() == flag & excluded.HasValue && idSelector(i).HasValue;
    })).Select<TLock, int>((Func<TLock, int>) (i => idSelector(i).Value)).ToHashSet<int>();
  }
}
