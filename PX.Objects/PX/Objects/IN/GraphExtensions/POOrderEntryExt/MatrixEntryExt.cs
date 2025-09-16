// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.POOrderEntryExt.MatrixEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.GraphExtensions;
using PX.Objects.IN.Matrix.Interfaces;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.POOrderEntryExt;

public class MatrixEntryExt : SmartPanelExt<POOrderEntry, PX.Objects.PO.POOrder>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.matrixItem>();

  protected override IEnumerable<IMatrixItemLine> GetLines(int? siteID, int? inventoryID)
  {
    return (IEnumerable<IMatrixItemLine>) ((IEnumerable<PX.Objects.PO.POLine>) ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).SelectMain(Array.Empty<object>())).Where<PX.Objects.PO.POLine>((Func<PX.Objects.PO.POLine, bool>) (l =>
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
    return (IEnumerable<IMatrixItemLine>) ((IEnumerable<PX.Objects.PO.POLine>) ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).SelectMain(Array.Empty<object>())).Where<PX.Objects.PO.POLine>((Func<PX.Objects.PO.POLine, bool>) (l =>
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
    ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Update((PX.Objects.PO.POLine) line);
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
    PX.Objects.PO.POLine copy1 = PXCache<PX.Objects.PO.POLine>.CreateCopy(((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Insert(new PX.Objects.PO.POLine()));
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExt<PX.Objects.PO.POLine.inventoryID>((object) copy1, (object) inventoryID);
    PX.Objects.PO.POLine copy2 = PXCache<PX.Objects.PO.POLine>.CreateCopy(((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Update(copy1));
    if (siteID.HasValue)
    {
      copy2.SiteID = siteID;
      copy2 = PXCache<PX.Objects.PO.POLine>.CreateCopy(((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Update(copy2));
    }
    if (uom != null)
      copy2.UOM = uom;
    copy2.OrderQty = new Decimal?(qty);
    PX.Objects.PO.POLine poLine = ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Update(copy2);
    if (string.IsNullOrEmpty(taxCategoryID))
      return;
    ((PXSelectBase) this.Base.Transactions).Cache.SetValueExt<PX.Objects.PO.POLine.taxCategoryID>((object) poLine, (object) taxCategoryID);
    ((PXSelectBase<PX.Objects.PO.POLine>) this.Base.Transactions).Update(poLine);
  }

  protected override bool IsDocumentOpen()
  {
    return ((PXSelectBase) this.Base.Transactions).Cache.AllowInsert;
  }

  protected override void DeductAllocated(PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter allocated, IMatrixItemLine line)
  {
    PX.Objects.PO.POLine data = (PX.Objects.PO.POLine) line;
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    AvailabilitySigns availabilitySigns = ((PXGraph) this.Base).FindImplementation<IItemPlanHandler<PX.Objects.PO.POLine>>().GetAvailabilitySigns<PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter>(data);
    Decimal? nullable;
    if (Sign.op_Inequality(availabilitySigns.SignQtyAvail, Sign.Zero))
    {
      Decimal num3 = num1;
      Sign signQtyAvail = availabilitySigns.SignQtyAvail;
      nullable = data.BaseOrderQty;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      Decimal num4 = Sign.op_Multiply(signQtyAvail, valueOrDefault);
      num1 = num3 - num4;
    }
    if (Sign.op_Inequality(availabilitySigns.SignQtyHardAvail, Sign.Zero))
    {
      Decimal num5 = num2;
      Sign signQtyHardAvail = availabilitySigns.SignQtyHardAvail;
      nullable = data.BaseOrderQty;
      Decimal valueOrDefault = nullable.GetValueOrDefault();
      Decimal num6 = Sign.op_Multiply(signQtyHardAvail, valueOrDefault);
      num2 = num5 - num6;
    }
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter1 = allocated;
    nullable = statusByCostCenter1.QtyAvail;
    Decimal num7 = num1;
    statusByCostCenter1.QtyAvail = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num7) : new Decimal?();
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter statusByCostCenter2 = allocated;
    nullable = statusByCostCenter2.QtyHardAvail;
    Decimal num8 = num2;
    statusByCostCenter2.QtyHardAvail = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + num8) : new Decimal?();
  }

  protected override string GetAvailabilityMessage(
    int? siteID,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter allocated,
    string uom)
  {
    return PXMessages.LocalizeFormatNoPrefix("On Hand {1} {0}, Available {2} {0}, Available for Shipping {3} {0}, Purchase Orders {4} {0}", new object[6]
    {
      (object) uom,
      (object) this.FormatQty(allocated.QtyOnHand),
      (object) this.FormatQty(allocated.QtyAvail),
      (object) this.FormatQty(allocated.QtyHardAvail),
      (object) this.FormatQty(allocated.QtyActual),
      (object) this.FormatQty(allocated.QtyPOOrders)
    });
  }

  protected override int? GetQtyPrecision()
  {
    object obj = (object) null;
    ((PXSelectBase) this.Base.Transactions).Cache.RaiseFieldSelecting<PX.Objects.PO.POOrder.orderQty>((object) null, ref obj, true);
    return obj is PXDecimalState pxDecimalState ? new int?(((PXFieldState) pxDecimalState).Precision) : new int?();
  }

  protected override bool IsItemStatusDisabled(PX.Objects.IN.InventoryItem item)
  {
    return base.IsItemStatusDisabled(item) || item?.ItemStatus == "NP";
  }

  protected override int? GetDefaultBranch()
  {
    return ((PXSelectBase<PX.Objects.PO.POOrder>) this.Base.Document).Current?.BranchID;
  }

  protected override string GetDefaultUOM(int? inventoryID)
  {
    return PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID)?.PurchaseUnit;
  }
}
