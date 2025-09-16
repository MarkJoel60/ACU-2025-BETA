// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.OpportunityMaintExt.MatrixEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.IN.Matrix.GraphExtensions;
using PX.Objects.IN.Matrix.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.OpportunityMaintExt;

public class MatrixEntryExt : SmartPanelExt<OpportunityMaint, CROpportunity>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.matrixItem>();

  protected override IEnumerable<IMatrixItemLine> GetLines(int? siteID, int? inventoryID)
  {
    return (IEnumerable<IMatrixItemLine>) ((IEnumerable<CROpportunityProducts>) ((PXSelectBase<CROpportunityProducts>) this.Base.Products).SelectMain(Array.Empty<object>())).Where<CROpportunityProducts>((Func<CROpportunityProducts, bool>) (l =>
    {
      int? inventoryId = l.InventoryID;
      int? nullable1 = inventoryID;
      if (!(inventoryId.GetValueOrDefault() == nullable1.GetValueOrDefault() & inventoryId.HasValue == nullable1.HasValue))
        return false;
      int? siteId = l.SiteID;
      int? nullable2 = siteID;
      return siteId.GetValueOrDefault() == nullable2.GetValueOrDefault() & siteId.HasValue == nullable2.HasValue || !siteID.HasValue;
    }));
  }

  protected override IEnumerable<IMatrixItemLine> GetLines(
    int? siteID,
    int? inventoryID,
    string taxCategoryID,
    string uom)
  {
    return (IEnumerable<IMatrixItemLine>) ((IEnumerable<CROpportunityProducts>) ((PXSelectBase<CROpportunityProducts>) this.Base.Products).SelectMain(Array.Empty<object>())).Where<CROpportunityProducts>((Func<CROpportunityProducts, bool>) (l =>
    {
      int? inventoryId = l.InventoryID;
      int? nullable1 = inventoryID;
      if (inventoryId.GetValueOrDefault() == nullable1.GetValueOrDefault() & inventoryId.HasValue == nullable1.HasValue)
      {
        int? siteId = l.SiteID;
        int? nullable2 = siteID;
        if ((siteId.GetValueOrDefault() == nullable2.GetValueOrDefault() & siteId.HasValue == nullable2.HasValue || !siteID.HasValue) && (l.TaxCategoryID == taxCategoryID || taxCategoryID == null))
          return l.UOM == uom;
      }
      return false;
    }));
  }

  protected override void UpdateLine(IMatrixItemLine line)
  {
    ((PXSelectBase<CROpportunityProducts>) this.Base.Products).Update((CROpportunityProducts) line);
  }

  protected override void CreateNewLine(int? siteID, int? inventoryID, Decimal qty)
  {
    this.CreateNewLine(siteID, inventoryID, (string) null, qty, (string) null);
  }

  protected override void CreateNewLine(
    int? siteID,
    int? inventoryID,
    string taxCategoryID,
    Decimal qty,
    string uom)
  {
    CROpportunityProducts copy1 = PXCache<CROpportunityProducts>.CreateCopy(((PXSelectBase<CROpportunityProducts>) this.Base.Products).Insert(new CROpportunityProducts()));
    copy1.SiteID = siteID;
    copy1.InventoryID = inventoryID;
    CROpportunityProducts copy2 = PXCache<CROpportunityProducts>.CreateCopy(((PXSelectBase<CROpportunityProducts>) this.Base.Products).Update(copy1));
    copy2.Qty = new Decimal?(qty);
    if (uom != null)
      copy2.UOM = uom;
    CROpportunityProducts opportunityProducts = ((PXSelectBase<CROpportunityProducts>) this.Base.Products).Update(copy2);
    if (string.IsNullOrEmpty(taxCategoryID))
      return;
    ((PXSelectBase) this.Base.Products).Cache.SetValueExt<CROpportunityProducts.taxCategoryID>((object) opportunityProducts, (object) taxCategoryID);
    ((PXSelectBase<CROpportunityProducts>) this.Base.Products).Update(opportunityProducts);
  }

  protected override bool IsDocumentOpen() => ((PXSelectBase) this.Base.Products).Cache.AllowInsert;

  protected override void DeductAllocated(SiteStatusByCostCenter allocated, IMatrixItemLine line)
  {
  }

  protected override string GetAvailabilityMessage(
    int? siteID,
    PX.Objects.IN.InventoryItem item,
    SiteStatusByCostCenter availability,
    string uom)
  {
    return (string) null;
  }

  protected override int? GetQtyPrecision()
  {
    object obj = (object) null;
    ((PXSelectBase) this.Base.Products).Cache.RaiseFieldSelecting<CROpportunityProducts.quantity>((object) null, ref obj, true);
    return obj is PXDecimalState pxDecimalState ? new int?(((PXFieldState) pxDecimalState).Precision) : new int?();
  }

  protected override bool IsItemStatusDisabled(PX.Objects.IN.InventoryItem item)
  {
    return base.IsItemStatusDisabled(item) || item?.ItemStatus == "NS";
  }

  protected override int? GetDefaultBranch()
  {
    return ((PXSelectBase<CROpportunity>) this.Base.Opportunity).Current?.BranchID;
  }
}
