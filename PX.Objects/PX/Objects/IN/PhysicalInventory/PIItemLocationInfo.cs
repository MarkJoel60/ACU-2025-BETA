// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.PhysicalInventory.PIItemLocationInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN.PhysicalInventory;

public class PIItemLocationInfo : IEquatable<PIItemLocationInfo>
{
  public int InventoryID { get; set; }

  public int? SubItemID { get; set; }

  public int LocationID { get; set; }

  public PXResult QueryResult { get; set; }

  public string InventoryCD { get; set; }

  public string SubItemCD { get; set; }

  public string LocationCD { get; set; }

  public string LotSerialNbr { get; set; }

  public string Description { get; set; }

  public bool Equals(PIItemLocationInfo other)
  {
    if (other == null || this.InventoryID != other.InventoryID)
      return false;
    int? subItemId1 = this.SubItemID;
    int? subItemId2 = other.SubItemID;
    return subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue && this.LocationID == other.LocationID;
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return !(obj.GetType() != this.GetType()) && this.Equals(obj as PIItemLocationInfo);
  }

  public override int GetHashCode()
  {
    int num = (17 * 23 + this.InventoryID) * 23;
    int? subItemId = this.SubItemID;
    return (subItemId.HasValue ? new int?(num + subItemId.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 23 + this.LocationID;
  }

  public static PIItemLocationInfo Create(PXResult result)
  {
    InventoryItem inventoryItem = result.GetItem<InventoryItem>();
    INSubItem inSubItem = result.GetItem<INSubItem>();
    INLocation inLocation = result.GetItem<INLocation>();
    INLotSerialStatus inLotSerialStatus = result.GetItem<INLotSerialStatus>();
    return new PIItemLocationInfo()
    {
      InventoryID = inventoryItem.InventoryID.Value,
      SubItemID = (int?) inSubItem?.SubItemID,
      LocationID = inLocation.LocationID.Value,
      QueryResult = result,
      InventoryCD = inventoryItem.InventoryCD,
      SubItemCD = inSubItem?.SubItemCD,
      LocationCD = inLocation.LocationCD,
      LotSerialNbr = inLotSerialStatus?.LotSerialNbr,
      Description = inventoryItem.Descr
    };
  }
}
