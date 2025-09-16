// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.InventoryFullTextSearchAddItemExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN.GraphExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

[PXProtectedAccess(null)]
public abstract class InventoryFullTextSearchAddItemExt : 
  InventoryFullTextSearchAddItemExtBase<SOInvoiceEntry, InventoryFullTextSearchExt, ARTran, ARTran.inventoryID, SOInvoiceSiteStatusLookupExt, PX.Objects.AR.ARInvoice, SOInvoiceSiteStatusSelected, SOSiteStatusFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.inventoryFullTextSearch>();

  protected override bool OrderByHasDefaultValue(string[] sortcolumns, bool[] descendings)
  {
    return sortcolumns != null && sortcolumns.Length == 3 && "InventoryID".Equals(sortcolumns[0], StringComparison.OrdinalIgnoreCase) && "SiteCD".Equals(sortcolumns[1], StringComparison.OrdinalIgnoreCase) && "SubItemCD".Equals(sortcolumns[2], StringComparison.OrdinalIgnoreCase) && ((IEnumerable<bool>) descendings).All<bool>((Func<bool, bool>) (d => !d));
  }
}
