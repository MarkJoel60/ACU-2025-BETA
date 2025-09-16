// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.PODefaultVendor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PO;

/// <summary>
/// This attribute defines, if the vendor and it's location specified
/// are the preffered Vendor for the inventory item. May be placed on field of boolean type,
/// to display this information dynamically
/// <example>
/// [PODefaultVendor(typeof(POVendorInventory.inventoryID), typeof(POVendorInventory.vendorID), typeof(POVendorInventory.vendorLocationID))]
/// </example>
/// </summary>
public class PODefaultVendor : PXEventSubscriberAttribute
{
  private Type inventoryID;
  private Type vendorID;
  private Type vendorLocationID;

  /// <summary>Ctor</summary>
  /// <param name="inventoryID">Must be IBqlField. Field which contains inventory id, for which Vendor/location is checked for beeng a preferred Vendor</param>
  /// <param name="vendorID">Must be IBqlField. Field which contains VendorID of the vendor checking for beeng a preferred Vendor</param>
  /// <param name="vendorLocationID">Must be IBqlField. Field which contains VendorLocationID of the vendor checking for beeng a preferred Vendor</param>
  public PODefaultVendor(Type inventoryID, Type vendorID, Type vendorLocationID)
  {
    this.inventoryID = inventoryID;
    this.vendorID = vendorID;
    this.vendorLocationID = vendorLocationID;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.RowSelectingEvents rowSelecting = sender.Graph.RowSelecting;
    Type itemType = sender.GetItemType();
    PODefaultVendor poDefaultVendor = this;
    // ISSUE: virtual method pointer
    PXRowSelecting pxRowSelecting = new PXRowSelecting((object) poDefaultVendor, __vmethodptr(poDefaultVendor, RowSelecting));
    rowSelecting.AddHandler(itemType, pxRowSelecting);
  }

  protected virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    object inventoryID = sender.GetValue(e.Row, this.inventoryID.Name);
    object obj = sender.GetValue(e.Row, this.vendorID.Name);
    object objB = sender.GetValue(e.Row, this.vendorLocationID.Name);
    if (inventoryID == null || obj == null)
      return;
    PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find(sender.Graph, (int?) obj);
    InventoryItemCurySettings itemCurySettings = (InventoryItemCurySettings) null;
    if (vendor != null && vendor.BaseCuryID != null)
      itemCurySettings = InventoryItemCurySettings.PK.Find(sender.Graph, (int?) inventoryID, vendor.BaseCuryID);
    else if (vendor != null)
      itemCurySettings = PXResultset<InventoryItemCurySettings>.op_Implicit(PXSelectBase<InventoryItemCurySettings, PXViewOf<InventoryItemCurySettings>.BasedOn<SelectFromBase<InventoryItemCurySettings, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<InventoryItemCurySettings.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.Select(sender.Graph, new object[1]
      {
        inventoryID
      }));
    sender.SetValue(e.Row, this._FieldName, (object) (bool) (itemCurySettings == null || !object.Equals((object) itemCurySettings.PreferredVendorID, obj) ? 0 : (object.Equals((object) itemCurySettings.PreferredVendorLocationID, objB) ? 1 : 0)));
  }
}
