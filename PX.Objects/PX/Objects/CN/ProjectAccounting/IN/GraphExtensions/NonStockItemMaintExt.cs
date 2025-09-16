// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.IN.GraphExtensions.NonStockItemMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.ProjectAccounting.AP.CacheExtensions;
using PX.Objects.CS;
using PX.Objects.IN;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.IN.GraphExtensions;

public class NonStockItemMaintExt : PXGraphExtension<NonStockItemMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  protected void _(PX.Data.Events.RowPersisting<PX.Objects.IN.InventoryItem> args)
  {
    if (args.Operation != 3)
      return;
    PX.Objects.IN.InventoryItem row = args.Row;
    if (row == null)
      return;
    this.DeleteVendorDefaultInventory(row.InventoryID);
  }

  private void DeleteVendorDefaultInventory(int? inventoryId)
  {
    List<PX.Objects.AP.Vendor> list = this.GetVendors(inventoryId).ToList<PX.Objects.AP.Vendor>();
    PXCache<PX.Objects.AP.Vendor> pxCache = GraphHelper.Caches<PX.Objects.AP.Vendor>((PXGraph) this.Base);
    foreach (PX.Objects.AP.Vendor vendor in list)
    {
      this.UpdateVendorDefaultInventoryId(vendor);
      pxCache.Update(vendor);
    }
    ((PXCache) pxCache).Persist((PXDBOperation) 1);
  }

  private void UpdateVendorDefaultInventoryId(PX.Objects.AP.Vendor vendor)
  {
    PXCache<PX.Objects.AP.Vendor>.GetExtension<VendorExt>(vendor).VendorDefaultInventoryId = new int?();
  }

  private IEnumerable<PX.Objects.AP.Vendor> GetVendors(int? inventoryId)
  {
    return ((PXSelectBase<PX.Objects.AP.Vendor>) new PXSelect<PX.Objects.AP.Vendor, Where<VendorExt.vendorDefaultInventoryId, Equal<Required<VendorExt.vendorDefaultInventoryId>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) inventoryId
    }).FirstTableItems;
  }
}
