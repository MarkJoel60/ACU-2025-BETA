// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.SOOrderEntryExt.MatrixEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.GraphExtensions;
using PX.Objects.IN.Matrix.Interfaces;
using PX.Objects.SO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.SOOrderEntryExt;

public class MatrixEntryExt : SmartPanelExt<SOOrderEntry, PX.Objects.SO.SOOrder>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.matrixItem>();

  protected override IEnumerable<IMatrixItemLine> GetLines(int? siteID, int? inventoryID)
  {
    return (IEnumerable<IMatrixItemLine>) ((IEnumerable<PX.Objects.SO.SOLine>) ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).SelectMain(Array.Empty<object>())).Where<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (l =>
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
    return (IEnumerable<IMatrixItemLine>) ((IEnumerable<PX.Objects.SO.SOLine>) ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).SelectMain(Array.Empty<object>())).Where<PX.Objects.SO.SOLine>((Func<PX.Objects.SO.SOLine, bool>) (l =>
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
    ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update((PX.Objects.SO.SOLine) line);
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
    PX.Objects.SO.SOLine copy1 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Insert(new PX.Objects.SO.SOLine()));
    copy1.SiteID = siteID;
    copy1.InventoryID = inventoryID;
    PX.Objects.SO.SOLine copy2 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(copy1));
    if (!copy2.RequireLocation.GetValueOrDefault())
      copy2.LocationID = new int?();
    PX.Objects.SO.SOLine copy3 = PXCache<PX.Objects.SO.SOLine>.CreateCopy(((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(copy2));
    copy3.Qty = new Decimal?(qty);
    if (uom != null)
      copy3.UOM = uom;
    PX.Objects.SO.SOLine soLine = ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(copy3);
    if (string.IsNullOrEmpty(taxCategoryID))
      return;
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExt<PX.Objects.SO.SOLine.taxCategoryID>((object) soLine, (object) taxCategoryID);
    ((PXSelectBase<PX.Objects.SO.SOLine>) this.Base.Transactions).Update(soLine);
  }

  protected override bool IsDocumentOpen()
  {
    return ((PXSelectBase) this.Base.Transactions).Cache.AllowInsert;
  }

  protected override void DeductAllocated(PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter allocated, IMatrixItemLine line)
  {
    PX.Objects.SO.SOLine soLine = (PX.Objects.SO.SOLine) line;
    int? costCenterId = soLine.CostCenterID;
    int num = 0;
    if (!(costCenterId.GetValueOrDefault() == num & costCenterId.HasValue))
      return;
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter1 = allocated;
    Decimal? nullable1 = statusByCostCenter1.QtyAvail;
    Decimal? nullable2 = soLine.LineQtyAvail;
    statusByCostCenter1.QtyAvail = nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter2 = allocated;
    nullable2 = statusByCostCenter2.QtyHardAvail;
    nullable1 = soLine.LineQtyHardAvail;
    statusByCostCenter2.QtyHardAvail = nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
  }

  protected override string GetAvailabilityMessage(
    int? siteID,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter availability,
    string uom)
  {
    if (this.Base.LineSplittingAllocatedExt.IsAllocationEntryEnabled)
    {
      Decimal? nullable = this.GetLines(siteID, item.InventoryID).Sum<IMatrixItemLine>((Func<IMatrixItemLine, Decimal?>) (l => ((PX.Objects.SO.SOLine) l).LineQtyHardAvail));
      if (uom != item.BaseUnit)
        nullable = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) this.Matrix).Cache, item.InventoryID, uom, nullable.GetValueOrDefault(), INPrecision.QUANTITY));
      return PXMessages.LocalizeFormatNoPrefix("On Hand {1} {0}, Available {2} {0}, Available for Shipping {3} {0}, Allocated {4} {0}", new object[5]
      {
        (object) uom,
        (object) this.FormatQty(availability.QtyOnHand),
        (object) this.FormatQty(availability.QtyAvail),
        (object) this.FormatQty(availability.QtyHardAvail),
        (object) this.FormatQty(nullable)
      });
    }
    return PXMessages.LocalizeFormatNoPrefix("On Hand {1} {0}, Available {2} {0}, Available for Shipping {3} {0}", new object[4]
    {
      (object) uom,
      (object) this.FormatQty(availability.QtyOnHand),
      (object) this.FormatQty(availability.QtyAvail),
      (object) this.FormatQty(availability.QtyHardAvail)
    });
  }

  protected override int? GetQtyPrecision()
  {
    object obj = (object) null;
    ((PXSelectBase) this.Base.Transactions).Cache.RaiseFieldSelecting<PX.Objects.SO.SOOrder.orderQty>((object) null, ref obj, true);
    return obj is PXDecimalState pxDecimalState ? new int?(((PXFieldState) pxDecimalState).Precision) : new int?();
  }

  protected override bool IsItemStatusDisabled(PX.Objects.IN.InventoryItem item)
  {
    return base.IsItemStatusDisabled(item) || item?.ItemStatus == "NS";
  }

  protected override int? GetDefaultBranch()
  {
    return ((PXSelectBase<PX.Objects.SO.SOOrder>) this.Base.Document).Current?.BranchID;
  }
}
