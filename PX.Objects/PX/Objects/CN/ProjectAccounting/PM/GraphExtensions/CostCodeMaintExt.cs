// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.GraphExtensions.CostCodeMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.ProjectAccounting.AP.CacheExtensions;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.PM.GraphExtensions;

public class CostCodeMaintExt : PXGraphExtension<CostCodeMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  protected void _(PX.Data.Events.RowPersisting<PMCostCode> args)
  {
    if (((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMCostCode>>) args).Cache == null)
      return;
    this.UpdateVendorsIfRequired(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMCostCode>>) args).Cache.Deleted);
  }

  private void UpdateVendorsIfRequired(IEnumerable costCodes)
  {
    int?[] array = costCodes.Cast<PMCostCode>().Select<PMCostCode, int?>((Func<PMCostCode, int?>) (c => c.CostCodeID)).ToArray<int?>();
    if (!((IEnumerable<int?>) array).Any<int?>())
      return;
    List<PX.Objects.AP.Vendor> list = this.GetVendors(array).ToList<PX.Objects.AP.Vendor>();
    PXCache<PX.Objects.AP.Vendor> pxCache = GraphHelper.Caches<PX.Objects.AP.Vendor>((PXGraph) this.Base);
    foreach (PX.Objects.AP.Vendor vendor in list)
    {
      this.UpdateVendorDefaultCostCodeId(vendor);
      pxCache.Update(vendor);
    }
    ((PXCache) pxCache).Persist((PXDBOperation) 1);
  }

  private void UpdateVendorDefaultCostCodeId(PX.Objects.AP.Vendor vendor)
  {
    PXCache<PX.Objects.AP.Vendor>.GetExtension<VendorExt>(vendor).VendorDefaultCostCodeId = new int?();
  }

  private IEnumerable<PX.Objects.AP.Vendor> GetVendors(int?[] costCodeIds)
  {
    return ((PXSelectBase<PX.Objects.AP.Vendor>) new PXSelect<PX.Objects.AP.Vendor, Where<VendorExt.vendorDefaultCostCodeId, In<Required<VendorExt.vendorDefaultCostCodeId>>>>((PXGraph) this.Base)).Select(new object[1]
    {
      (object) costCodeIds
    }).FirstTableItems;
  }
}
