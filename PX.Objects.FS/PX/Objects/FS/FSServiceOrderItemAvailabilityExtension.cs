// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceOrderItemAvailabilityExtension
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Exceptions;
using PX.Objects.IN;
using PX.Objects.SO.GraphExtensions;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSServiceOrderItemAvailabilityExtension : 
  SOBaseItemAvailabilityExtension<ServiceOrderEntry, FSSODet, FSSODetSplit>
{
  protected override FSSODetSplit EnsureSplit(ILSMaster row)
  {
    return ((PXGraph) this.Base).FindImplementation<FSServiceOrderLineSplittingExtension>().EnsureSplit(row);
  }

  protected override Decimal GetUnitRate(FSSODet line)
  {
    return this.GetUnitRate<FSSODet.inventoryID, FSSODet.uOM>(line);
  }

  protected override string GetStatus(FSSODet line)
  {
    string status = string.Empty;
    IStatus availability = this.FetchWithLineUOM(line, line == null || !line.Completed.GetValueOrDefault(), (int?) line?.CostCenterID);
    if (availability != null)
    {
      status = this.FormatStatus(availability, line.UOM);
      this.Check((ILSMaster) line, availability);
    }
    return status;
  }

  private string FormatStatus(IStatus availability, string uom)
  {
    return PXMessages.LocalizeFormatNoPrefix("On Hand {1} {0}, Available {2} {0}, Available for Shipping {3} {0}", new object[4]
    {
      (object) uom,
      (object) this.FormatQty(availability.QtyOnHand),
      (object) this.FormatQty(availability.QtyAvail),
      (object) this.FormatQty(availability.QtyHardAvail)
    });
  }

  protected override IStatus Fetch(ILSDetail split, bool excludeCurrent, int? costCenterID)
  {
    return base.Fetch(split, excludeCurrent, costCenterID);
  }

  public override void Check(ILSMaster row, int? costCenterID)
  {
    base.Check(row, costCenterID);
    this.MemoCheck(row);
  }

  protected virtual void MemoCheck(ILSMaster row)
  {
    switch (row)
    {
      case FSSODet fssoDet:
        this.MemoCheck(fssoDet);
        FSSODetSplit split1 = this.EnsureSplit((ILSMaster) fssoDet);
        this.MemoCheck(fssoDet, split1, false);
        if (split1.LotSerialNbr != null)
          break;
        row.LotSerialNbr = (string) null;
        break;
      case FSSODetSplit split2:
        FSSODet line = PXParentAttribute.SelectParent<FSSODet>((PXCache) this.SplitCache, (object) split2);
        this.MemoCheck(line);
        this.MemoCheck(line, split2, true);
        break;
    }
  }

  public virtual bool MemoCheck(FSSODet line) => this.MemoCheckQty(line);

  protected virtual bool MemoCheckQty(FSSODet row) => true;

  protected virtual bool MemoCheck(FSSODet line, FSSODetSplit split, bool triggeredBySplit) => true;

  protected override int DetailsCountToEnableOptimization => 50;

  protected override void Optimize()
  {
    base.Optimize();
    foreach (PXResult<FSSODet, INUnit, INSiteStatusByCostCenter> pxResult in PXSelectBase<FSSODet, PXViewOf<FSSODet>.BasedOn<SelectFromBase<FSSODet, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INUnit>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INUnit.inventoryID, Equal<FSSODet.inventoryID>>>>>.And<BqlOperand<INUnit.fromUnit, IBqlString>.IsEqual<FSSODet.uOM>>>>, FbqlJoins.Inner<INSiteStatusByCostCenter>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<FSSODet.inventoryID, Equal<INSiteStatusByCostCenter.inventoryID>>>>, And<BqlOperand<FSSODet.subItemID, IBqlInt>.IsEqual<INSiteStatusByCostCenter.subItemID>>>, And<BqlOperand<FSSODet.siteID, IBqlInt>.IsEqual<INSiteStatusByCostCenter.siteID>>>>.And<BqlOperand<INSiteStatusByCostCenter.costCenterID, IBqlInt>.IsEqual<PX.Objects.IN.CostCenter.freeStock>>>>>.Where<KeysRelation<CompositeKey<Field<FSSODet.srvOrdType>.IsRelatedTo<FSServiceOrder.srvOrdType>, Field<FSSODet.refNbr>.IsRelatedTo<FSServiceOrder.refNbr>>.WithTablesOf<FSServiceOrder, FSSODet>, FSServiceOrder, FSSODet>.SameAsCurrent>>.ReadOnly.Config>.Select((PXGraph) this.Base, Array.Empty<object>()))
    {
      PrimaryKeyOf<INUnit>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INUnit.unitType, INUnit.inventoryID, INUnit.fromUnit>>.StoreResult((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<INUnit.unitType, INUnit.inventoryID, INUnit.fromUnit>) PXResult<FSSODet, INUnit, INSiteStatusByCostCenter>.op_Implicit(pxResult), false);
      PrimaryKeyOf<INSiteStatusByCostCenter>.MultiBy<TypeArrayOf<IBqlField>.IFilledWith<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>>.StoreResult((PXGraph) this.Base, (TypeArrayOf<IBqlField>.IFilledWith<INSiteStatusByCostCenter.inventoryID, INSiteStatusByCostCenter.subItemID, INSiteStatusByCostCenter.siteID, INSiteStatusByCostCenter.costCenterID>) PXResult<FSSODet, INUnit, INSiteStatusByCostCenter>.op_Implicit(pxResult), false);
    }
  }

  protected override void RaiseQtyExceptionHandling(
    FSSODet line,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.LineCache).RaiseExceptionHandling<FSSODet.estimatedQty>((object) line, (object) newValue, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.LineCache).GetStateExt<FSSODet.inventoryID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<FSSODet.subItemID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<FSSODet.siteID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<FSSODet.siteLocationID>((object) line),
      ((PXCache) this.LineCache).GetValue<FSSODet.lotSerialNbr>((object) line)
    }));
  }

  protected override void RaiseQtyExceptionHandling(
    FSSODetSplit split,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.SplitCache).RaiseExceptionHandling<FSSODetSplit.qty>((object) split, (object) newValue, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.SplitCache).GetStateExt<FSSODetSplit.inventoryID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<FSSODetSplit.subItemID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<FSSODetSplit.siteID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<FSSODetSplit.locationID>((object) split),
      ((PXCache) this.SplitCache).GetValue<FSSODetSplit.lotSerialNbr>((object) split)
    }));
  }

  protected override bool IsAvailableQty(ILSMaster row, IStatus availability)
  {
    short? invtMult = row.InvtMult;
    if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1)
    {
      Decimal? nullable = row.BaseQty;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() > num1 & nullable.HasValue && availability != null)
      {
        Decimal? qtyAvail = availability.QtyAvail;
        Decimal? qtyNotAvail = availability.QtyNotAvail;
        nullable = qtyAvail.HasValue & qtyNotAvail.HasValue ? new Decimal?(qtyAvail.GetValueOrDefault() + qtyNotAvail.GetValueOrDefault()) : new Decimal?();
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
          return false;
      }
    }
    return true;
  }
}
