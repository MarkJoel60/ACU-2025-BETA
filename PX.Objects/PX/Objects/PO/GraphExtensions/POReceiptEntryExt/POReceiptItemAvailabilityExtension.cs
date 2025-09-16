// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.POReceiptItemAvailabilityExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Exceptions;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class POReceiptItemAvailabilityExtension : 
  ItemAvailabilityExtension<POReceiptEntry, PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLineSplit>
{
  protected override PX.Objects.PO.POReceiptLineSplit EnsureSplit(ILSMaster row)
  {
    return ((PXGraph) this.Base).FindImplementation<POReceiptLineSplittingExtension>().EnsureSplit(row);
  }

  protected override Decimal GetUnitRate(PX.Objects.PO.POReceiptLine line)
  {
    return this.GetUnitRate<PX.Objects.PO.POReceiptLine.inventoryID, PX.Objects.PO.POReceiptLine.uOM>(line);
  }

  protected override string GetStatus(PX.Objects.PO.POReceiptLine line)
  {
    string status = string.Empty;
    PX.Objects.PO.POReceipt poReceipt = PXParentAttribute.SelectParent<PX.Objects.PO.POReceipt>((PXCache) this.LineCache, (object) line);
    bool excludeCurrent = poReceipt == null || !poReceipt.Released.GetValueOrDefault();
    IStatus availability = this.FetchWithLineUOM(line, excludeCurrent, line.CostCenterID);
    if (availability != null)
    {
      status = this.FormatStatus(availability, line.UOM);
      this.Check((ILSMaster) line, availability);
    }
    return status;
  }

  protected override void ExcludeCurrent(
    ILSDetail currentSplit,
    IStatus allocated,
    AvailabilitySigns signs)
  {
    base.ExcludeCurrent(currentSplit, allocated, signs);
    PX.Objects.PO.POReceiptLineSplit assignedSplit = (PX.Objects.PO.POReceiptLineSplit) currentSplit;
    foreach (PX.Objects.PO.Unassigned.POReceiptLineSplit selectUnassignedSplit in this.SelectUnassignedSplits(assignedSplit))
    {
      if (currentSplit.LocationID.HasValue)
      {
        int? locationId1 = currentSplit.LocationID;
        int? locationId2 = selectUnassignedSplit.LocationID;
        if (!(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue))
          continue;
      }
      if (currentSplit.LotSerialNbr == null || string.IsNullOrEmpty(selectUnassignedSplit.LotSerialNbr) || string.Equals(currentSplit.LotSerialNbr, selectUnassignedSplit.LotSerialNbr, StringComparison.InvariantCultureIgnoreCase))
        base.ExcludeCurrent((ILSDetail) selectUnassignedSplit, allocated, signs);
    }
    PX.Objects.PO.POReceiptLine poReceiptLine = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base.transactions).Locate(new PX.Objects.PO.POReceiptLine()
    {
      ReceiptNbr = assignedSplit.ReceiptNbr,
      ReceiptType = assignedSplit.ReceiptType,
      LineNbr = assignedSplit.LineNbr
    }) ?? PX.Objects.PO.POReceiptLine.PK.Find((PXGraph) this.Base, assignedSplit.ReceiptType, assignedSplit.ReceiptNbr, assignedSplit.LineNbr, (PKFindOptions) 1);
    if (!poReceiptLine.IsCorrection.GetValueOrDefault())
      return;
    PXResultset<PX.Objects.PO.POReceiptLineSplit> pxResultset = PXSelectBase<PX.Objects.PO.POReceiptLineSplit, PXViewOf<PX.Objects.PO.POReceiptLineSplit>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLineSplit.receiptNbr, Equal<BqlField<PX.Objects.PO.POReceiptLine.origReceiptNbr, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLineSplit.receiptType, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origReceiptType, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLineSplit.lineNbr, IBqlInt>.IsEqual<BqlField<PX.Objects.PO.POReceiptLine.origReceiptLineNbr, IBqlInt>.FromCurrent>>>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new PX.Objects.PO.POReceiptLine[1]
    {
      poReceiptLine
    }, Array.Empty<object>());
    Sign signQtyAvail1 = signs.SignQtyAvail;
    Decimal signQtyAvail2 = (Decimal) (int) -((Sign) ref signQtyAvail1).Value;
    Sign sign = signs.SignQtyHardAvail;
    Decimal signQtyHardAvail = (Decimal) (int) -((Sign) ref sign).Value;
    sign = signs.SignQtyActual;
    Decimal signQtyActual = (Decimal) (int) -((Sign) ref sign).Value;
    AvailabilitySigns signs1 = new AvailabilitySigns(signQtyAvail2, signQtyHardAvail, signQtyActual);
    foreach (PXResult<PX.Objects.PO.POReceiptLineSplit> pxResult in pxResultset)
    {
      PX.Objects.PO.POReceiptLineSplit currentSplit1 = PXResult<PX.Objects.PO.POReceiptLineSplit>.op_Implicit(pxResult);
      int? nullable1 = ((ILSMaster) currentSplit).SiteID;
      int? nullable2;
      if (nullable1.HasValue)
      {
        nullable1 = ((ILSMaster) currentSplit).SiteID;
        nullable2 = currentSplit1.SiteID;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          continue;
      }
      nullable2 = currentSplit.LocationID;
      if (nullable2.HasValue)
      {
        nullable2 = currentSplit.LocationID;
        nullable1 = currentSplit1.LocationID;
        if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
          continue;
      }
      if (currentSplit.LotSerialNbr == null || string.IsNullOrEmpty(currentSplit1.LotSerialNbr) || string.Equals(currentSplit.LotSerialNbr, currentSplit1.LotSerialNbr, StringComparison.InvariantCultureIgnoreCase))
        base.ExcludeCurrent((ILSDetail) currentSplit1, allocated, signs1);
    }
  }

  private string FormatStatus(IStatus availability, string uom)
  {
    return PXMessages.LocalizeFormatNoPrefixNLA("On Hand {1} {0}, Available {2} {0}, Available for Shipping {3} {0}", new object[5]
    {
      (object) uom,
      (object) this.FormatQty(availability.QtyOnHand),
      (object) this.FormatQty(availability.QtyAvail),
      (object) this.FormatQty(availability.QtyHardAvail),
      (object) this.FormatQty(availability.QtyActual)
    });
  }

  protected override void Optimize()
  {
    base.Optimize();
    PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSiteStatusByCostCenter>.On<PX.Objects.PO.POReceiptLine.FK.SiteStatusByCostCenter>>, FbqlJoins.Left<INLocationStatusByCostCenter>.On<PX.Objects.PO.POReceiptLine.FK.LocationStatusByCostCenter>>, FbqlJoins.Left<INLotSerialStatusByCostCenter>.On<PX.Objects.PO.POReceiptLine.FK.LotSerialStatusByCostCenter>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>>.ReadOnly readOnly = new PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSiteStatusByCostCenter>.On<PX.Objects.PO.POReceiptLine.FK.SiteStatusByCostCenter>>, FbqlJoins.Left<INLocationStatusByCostCenter>.On<PX.Objects.PO.POReceiptLine.FK.LocationStatusByCostCenter>>, FbqlJoins.Left<INLotSerialStatusByCostCenter>.On<PX.Objects.PO.POReceiptLine.FK.LotSerialStatusByCostCenter>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, Equal<BqlField<PX.Objects.PO.POReceipt.receiptType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.IsEqual<BqlField<PX.Objects.PO.POReceipt.receiptNbr, IBqlString>.FromCurrent>>>>.ReadOnly((PXGraph) this.Base);
    using (new PXFieldScope(((PXSelectBase) readOnly).View, new Type[3]
    {
      typeof (INSiteStatusByCostCenter),
      typeof (INLocationStatusByCostCenter),
      typeof (INLotSerialStatusByCostCenter)
    }))
    {
      foreach (PXResult<PX.Objects.PO.POReceiptLine, INSiteStatusByCostCenter, INLocationStatusByCostCenter, INLotSerialStatusByCostCenter> pxResult in ((PXSelectBase<PX.Objects.PO.POReceiptLine>) readOnly).Select(Array.Empty<object>()))
      {
        PX.Objects.PO.POReceiptLine poReceiptLine;
        INSiteStatusByCostCenter statusByCostCenter1;
        INLocationStatusByCostCenter statusByCostCenter2;
        INLotSerialStatusByCostCenter statusByCostCenter3;
        pxResult.Deconstruct(ref poReceiptLine, ref statusByCostCenter1, ref statusByCostCenter2, ref statusByCostCenter3);
        INSiteStatusByCostCenter statusByCostCenter4 = statusByCostCenter1;
        INLocationStatusByCostCenter statusByCostCenter5 = statusByCostCenter2;
        INLotSerialStatusByCostCenter statusByCostCenter6 = statusByCostCenter3;
        PrimaryKeyOf<INSiteStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>>.StoreResult((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>) statusByCostCenter4, false);
        if (statusByCostCenter5.LocationID.HasValue)
          PrimaryKeyOf<INLocationStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INLocationStatusByCostCenter.inventoryID, INLocationStatusByCostCenter.subItemID, INLocationStatusByCostCenter.siteID, INLocationStatusByCostCenter.locationID, INLocationStatusByCostCenter.costCenterID>>.StoreResult((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<INLocationStatusByCostCenter.inventoryID, INLocationStatusByCostCenter.subItemID, INLocationStatusByCostCenter.siteID, INLocationStatusByCostCenter.locationID, INLocationStatusByCostCenter.costCenterID>) statusByCostCenter5, false);
        if (statusByCostCenter6 != null && statusByCostCenter6.LotSerialNbr != null)
          PrimaryKeyOf<INLotSerialStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INLotSerialStatusByCostCenter.inventoryID, INLotSerialStatusByCostCenter.subItemID, INLotSerialStatusByCostCenter.siteID, INLotSerialStatusByCostCenter.locationID, INLotSerialStatusByCostCenter.lotSerialNbr, INLotSerialStatusByCostCenter.costCenterID>>.StoreResult((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<INLotSerialStatusByCostCenter.inventoryID, INLotSerialStatusByCostCenter.subItemID, INLotSerialStatusByCostCenter.siteID, INLotSerialStatusByCostCenter.locationID, INLotSerialStatusByCostCenter.lotSerialNbr, INLotSerialStatusByCostCenter.costCenterID>) statusByCostCenter6, false);
      }
    }
  }

  protected override void RaiseQtyExceptionHandling(
    PX.Objects.PO.POReceiptLine line,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.LineCache).RaiseExceptionHandling<PX.Objects.PO.POReceiptLine.receiptQty>((object) line, (object) newValue, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.LineCache).GetValueExt<PX.Objects.PO.POReceiptLine.inventoryID>((object) line),
      ((PXCache) this.LineCache).GetValueExt<PX.Objects.PO.POReceiptLine.subItemID>((object) line),
      ((PXCache) this.LineCache).GetValueExt<PX.Objects.PO.POReceiptLine.siteID>((object) line),
      ((PXCache) this.LineCache).GetValueExt<PX.Objects.PO.POReceiptLine.locationID>((object) line),
      ((PXCache) this.LineCache).GetValue<PX.Objects.PO.POReceiptLine.lotSerialNbr>((object) line)
    }));
  }

  protected override void RaiseQtyExceptionHandling(
    PX.Objects.PO.POReceiptLineSplit split,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.SplitCache).RaiseExceptionHandling<PX.Objects.PO.POReceiptLineSplit.qty>((object) split, (object) newValue, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.SplitCache).GetValueExt<PX.Objects.PO.POReceiptLineSplit.inventoryID>((object) split),
      ((PXCache) this.SplitCache).GetValueExt<PX.Objects.PO.POReceiptLineSplit.subItemID>((object) split),
      ((PXCache) this.SplitCache).GetValueExt<PX.Objects.PO.POReceiptLineSplit.siteID>((object) split),
      ((PXCache) this.SplitCache).GetValueExt<PX.Objects.PO.POReceiptLineSplit.locationID>((object) split),
      ((PXCache) this.SplitCache).GetValue<PX.Objects.PO.POReceiptLineSplit.lotSerialNbr>((object) split)
    }));
  }

  protected virtual List<PX.Objects.PO.Unassigned.POReceiptLineSplit> SelectUnassignedSplits(
    PX.Objects.PO.POReceiptLineSplit assignedSplit)
  {
    PX.Objects.PO.Unassigned.POReceiptLineSplit receiptLineSplit = new PX.Objects.PO.Unassigned.POReceiptLineSplit()
    {
      ReceiptType = assignedSplit.ReceiptType,
      ReceiptNbr = assignedSplit.ReceiptNbr,
      LineNbr = assignedSplit.LineNbr,
      SplitLineNbr = assignedSplit.SplitLineNbr
    };
    return PXParentAttribute.SelectSiblings((PXCache) GraphHelper.Caches<PX.Objects.PO.Unassigned.POReceiptLineSplit>((PXGraph) this.Base), (object) receiptLineSplit, this.IsOptimizationEnabled ? typeof (PX.Objects.PO.POReceipt) : typeof (PX.Objects.PO.POReceiptLine)).Cast<PX.Objects.PO.Unassigned.POReceiptLineSplit>().Where<PX.Objects.PO.Unassigned.POReceiptLineSplit>((Func<PX.Objects.PO.Unassigned.POReceiptLineSplit, bool>) (us =>
    {
      int? inventoryId1 = us.InventoryID;
      int? inventoryId2 = assignedSplit.InventoryID;
      if (!(inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue))
        return false;
      int? lineNbr1 = us.LineNbr;
      int? lineNbr2 = assignedSplit.LineNbr;
      return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
    })).ToList<PX.Objects.PO.Unassigned.POReceiptLineSplit>();
  }
}
