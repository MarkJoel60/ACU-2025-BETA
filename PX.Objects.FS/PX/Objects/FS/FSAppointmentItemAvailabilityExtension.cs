// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentItemAvailabilityExtension
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.Common.Exceptions;
using PX.Objects.IN;
using PX.Objects.SO.GraphExtensions;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSAppointmentItemAvailabilityExtension : 
  SOBaseItemAvailabilityExtension<AppointmentEntry, FSAppointmentDet, FSApptLineSplit>
{
  public override void Initialize()
  {
    base.Initialize();
    ((PXGraph) this.Base).Caches.AddCacheMapping(typeof (INLocationStatus), typeof (INLocationStatus));
    ((PXGraph) this.Base).Caches.AddCacheMapping(typeof (PX.Objects.CR.LocationStatus), typeof (PX.Objects.CR.LocationStatus));
  }

  protected override FSApptLineSplit EnsureSplit(ILSMaster row)
  {
    return ((PXGraph) this.Base).FindImplementation<FSAppointmentLineSplittingExtension>().EnsureSplit(row);
  }

  protected override Decimal GetUnitRate(FSAppointmentDet line)
  {
    return this.GetUnitRate<FSAppointmentDet.inventoryID, FSAppointmentDet.uOM>(line);
  }

  protected override string GetStatus(FSAppointmentDet line)
  {
    string status = string.Empty;
    IStatus availability = this.FetchWithLineUOM(line, line?.Status != "CP", (int?) line?.CostCenterID);
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

  protected void ExcludeCurrent(
    ILSDetail currentSplit,
    IStatus allocated,
    Decimal signQtyAvail,
    Decimal signQtyHardAvail,
    Decimal signQtyActual)
  {
    IStatus status = allocated;
    Decimal? qtyAvail = status.QtyAvail;
    Decimal valueOrDefault = currentSplit.BaseQty.GetValueOrDefault();
    status.QtyAvail = qtyAvail.HasValue ? new Decimal?(qtyAvail.GetValueOrDefault() + valueOrDefault) : new Decimal?();
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
      case FSAppointmentDet fsAppointmentDet:
        this.MemoCheck(fsAppointmentDet);
        FSApptLineSplit split1 = this.EnsureSplit((ILSMaster) fsAppointmentDet);
        this.MemoCheck(fsAppointmentDet, split1, false);
        if (split1.LotSerialNbr != null)
          break;
        row.LotSerialNbr = (string) null;
        break;
      case FSApptLineSplit split2:
        FSAppointmentDet line = PXParentAttribute.SelectParent<FSAppointmentDet>((PXCache) this.SplitCache, (object) split2);
        this.MemoCheck(line);
        this.MemoCheck(line, split2, true);
        break;
    }
  }

  public virtual bool MemoCheck(FSAppointmentDet line) => this.MemoCheckQty(line);

  protected virtual bool MemoCheckQty(FSAppointmentDet row) => true;

  protected virtual bool MemoCheck(
    FSAppointmentDet line,
    FSApptLineSplit split,
    bool triggeredBySplit)
  {
    return true;
  }

  protected override void RaiseQtyExceptionHandling(
    FSAppointmentDet line,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.LineCache).RaiseExceptionHandling<FSAppointmentDet.effTranQty>((object) line, (object) newValue, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[6]
    {
      ((PXCache) this.LineCache).GetStateExt<FSAppointmentDet.inventoryID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<FSAppointmentDet.subItemID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<FSAppointmentDet.siteID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<FSAppointmentDet.locationID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<FSApptLineSplit.costCenterID>((object) line),
      ((PXCache) this.LineCache).GetValue<FSAppointmentDet.lotSerialNbr>((object) line)
    }));
  }

  protected override void RaiseQtyExceptionHandling(
    FSApptLineSplit split,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.SplitCache).RaiseExceptionHandling<FSApptLineSplit.qty>((object) split, (object) newValue, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[6]
    {
      ((PXCache) this.SplitCache).GetStateExt<FSApptLineSplit.inventoryID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<FSApptLineSplit.subItemID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<FSApptLineSplit.siteID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<FSApptLineSplit.locationID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<FSApptLineSplit.costCenterID>((object) split),
      ((PXCache) this.SplitCache).GetValue<FSApptLineSplit.lotSerialNbr>((object) split)
    }));
  }
}
