// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.GraphExtensions.RQRequisitionEntryExt.InventoryFullTextSearchAddItemExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.GraphExtensions;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.RQ.GraphExtensions.RQRequisitionEntryExt;

public class InventoryFullTextSearchAddItemExt : 
  InventoryFullTextSearchAddItemExtBase<RQRequisitionEntry, InventoryFullTextSearchExt, RQRequisitionLine, RQRequisitionLine.inventoryID, RQRequisitionSiteStatusLookupExt, RQRequisition, RQSiteStatusSelected, POSiteStatusFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.inventoryFullTextSearch>();

  protected override bool OrderByHasDefaultValue(string[] sortcolumns, bool[] descendings)
  {
    return sortcolumns != null && sortcolumns.Length == 3 && "InventoryID".Equals(sortcolumns[0], StringComparison.OrdinalIgnoreCase) && "SiteCD".Equals(sortcolumns[1], StringComparison.OrdinalIgnoreCase) && "SubItemCD".Equals(sortcolumns[2], StringComparison.OrdinalIgnoreCase) && ((IEnumerable<bool>) descendings).All<bool>((Func<bool, bool>) (d => !d));
  }
}
