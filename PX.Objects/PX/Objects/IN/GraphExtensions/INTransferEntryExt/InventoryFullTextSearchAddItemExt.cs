// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INTransferEntryExt.InventoryFullTextSearchAddItemExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INTransferEntryExt;

public class InventoryFullTextSearchAddItemExt : 
  InventoryFullTextSearchAddItemExtBase<INTransferEntry, InventoryFullTextSearchExt, INTran, INTran.inventoryID, INTransferEntry.SiteStatusLookup, INRegister, INTransferEntry.SiteStatusLookup.INSiteStatusSelected, INSiteStatusFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.inventoryFullTextSearch>();

  protected override bool OrderByHasDefaultValue(string[] sortcolumns, bool[] descendings)
  {
    return sortcolumns != null && sortcolumns.Length == 4 && "InventoryID".Equals(sortcolumns[0], StringComparison.OrdinalIgnoreCase) && "SiteID".Equals(sortcolumns[1], StringComparison.OrdinalIgnoreCase) && "LocationID".Equals(sortcolumns[2], StringComparison.OrdinalIgnoreCase) && "SubItemID".Equals(sortcolumns[3], StringComparison.OrdinalIgnoreCase) && ((IEnumerable<bool>) descendings).All<bool>((Func<bool, bool>) (d => !d));
  }
}
