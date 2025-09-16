// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.POOrderSiteStatusLookupExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.Extensions.AddItemLookup;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

public class POOrderSiteStatusLookupExt : 
  AlternateIDLookupExt<POOrderEntry, PX.Objects.PO.POOrder, PX.Objects.PO.POLine, POSiteStatusSelected, POSiteStatusFilter, POSiteStatusSelected.purchaseUnit>
{
  protected override PX.Objects.PO.POLine CreateNewLine(POSiteStatusSelected line)
  {
    PX.Objects.PO.POLine newLine = new PX.Objects.PO.POLine();
    newLine.SiteID = line.SiteID;
    switch (((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.OrderType)
    {
      case "DP":
        newLine.LineType = "GP";
        break;
      case "PD":
        newLine.LineType = "PG";
        break;
      default:
        newLine.LineType = "GI";
        break;
    }
    newLine.InventoryID = line.InventoryID;
    newLine.SubItemID = line.SubItemID;
    newLine.UOM = line.PurchaseUnit;
    newLine.OrderQty = line.QtySelected;
    newLine.AlternateID = line.AlternateID;
    return ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Insert(this.InitNewLine(line, newLine));
  }

  protected virtual PX.Objects.PO.POLine InitNewLine(POSiteStatusSelected line, PX.Objects.PO.POLine newLine)
  {
    return newLine;
  }

  protected override Dictionary<string, int> GetAlternateTypePriority()
  {
    return new Dictionary<string, int>()
    {
      {
        "0VPN",
        0
      },
      {
        "GLBL",
        10
      },
      {
        "BAR",
        20
      },
      {
        "GIN",
        30
      },
      {
        "0CPN",
        40
      }
    };
  }

  protected override bool ScreenSpecificFilter(INItemXRef xRef)
  {
    if (xRef.AlternateType != "0VPN")
      return true;
    int? baccountId = xRef.BAccountID;
    int? vendorId = (int?) ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current?.VendorID;
    return baccountId.GetValueOrDefault() == vendorId.GetValueOrDefault() & baccountId.HasValue == vendorId.HasValue;
  }

  protected virtual void _(PX.Data.Events.RowInserted<POSiteStatusFilter> e)
  {
    if (e.Row == null || ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current == null)
      return;
    INSite inSite = INSite.PK.Find((PXGraph) this.Base, (int?) PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXViewOf<PX.Objects.CR.Location>.BasedOn<SelectFromBase<PX.Objects.CR.Location, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Location.locationID, Equal<BqlField<PX.Objects.PO.POOrder.vendorLocationID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.CR.Location.bAccountID, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POOrder.vendorID, IBqlInt>.FromCurrent>>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, Array.Empty<object>()))?.VSiteID);
    if (inSite != null && (PXAccess.FeatureInstalled<FeaturesSet.interBranch>() || PXAccess.IsSameParentOrganization(inSite.BranchID, ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.BranchID)))
      e.Row.SiteID = inSite.SiteID;
    e.Row.VendorID = ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current.VendorID;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.PO.POOrder> e)
  {
    if (e.Row == null)
      return;
    int? nullable = e.Row.VendorID;
    int num;
    if (nullable.HasValue)
    {
      nullable = e.Row.VendorLocationID;
      num = nullable.HasValue ? 1 : 0;
    }
    else
      num = 0;
    ((PXAction) this.showItems).SetEnabled(num != 0);
  }

  protected override PXView CreateItemInfoView()
  {
    PXCache pxCache = (PXCache) GraphHelper.Caches<POSiteStatusFilter>((PXGraph) this.Base);
    return pxCache.Current != null && ((INSiteStatusFilter) pxCache.Current).OnlyAvailable.GetValueOrDefault() ? (PXView) new AddItemLookupBaseExt<POOrderEntry, PX.Objects.PO.POOrder, POSiteStatusSelected, POSiteStatusFilter>.LookupView((PXGraph) this.Base, ((BqlCommand) new SelectFrom<POSiteStatusSelected, TypeArrayOf<IFbqlJoin>.Empty>.InnerJoin<POVendorInventoryOnly>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POVendorInventoryOnly.inventoryID, Equal<POSiteStatusSelected.inventoryID>>>>, And<BqlOperand<POVendorInventoryOnly.vendorID, IBqlInt>.IsEqual<BqlField<POSiteStatusFilter.vendorID, IBqlInt>.FromCurrent>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POVendorInventoryOnly.subItemID, Equal<POSiteStatusSelected.subItemID>>>>>.Or<BqlOperand<POSiteStatusSelected.subItemID, IBqlInt>.IsNull>>>()).WhereAnd(this.CreateWhere())) : base.CreateItemInfoView();
  }
}
