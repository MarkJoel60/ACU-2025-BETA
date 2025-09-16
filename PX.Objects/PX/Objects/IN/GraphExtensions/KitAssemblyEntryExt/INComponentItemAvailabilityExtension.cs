// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt.INComponentItemAvailabilityExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Exceptions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt;

public class INComponentItemAvailabilityExtension : 
  ItemAvailabilityExtension<KitAssemblyEntry, INComponentTran, INComponentTranSplit>
{
  protected override INComponentTranSplit EnsureSplit(ILSMaster row)
  {
    return ((PXGraph) this.Base).FindImplementation<INComponentLineSplittingExtension>().EnsureSplit(row);
  }

  protected override Decimal GetUnitRate(INComponentTran line)
  {
    return this.GetUnitRate<INComponentTran.inventoryID, INComponentTran.uOM>(line);
  }

  public override void Initialize()
  {
    base.Initialize();
    ((RowUpdatedEvents) ((PXGraph) this.Base).RowUpdated).AddAbstractHandler<INComponentTran>(new Action<AbstractEvents.IRowUpdated<INComponentTran>>(this.EventHandler));
  }

  protected override string GetStatus(INComponentTran line)
  {
    string status = string.Empty;
    INKitRegister current = ((PXSelectBase<INKitRegister>) this.Base.Document).Current;
    int num;
    if ((line != null ? (!line.Released.GetValueOrDefault() ? 1 : 0) : 1) != 0)
    {
      if (!(line.OrigModule != "IN"))
      {
        bool? nullable = ((PXSelectBase<INSetup>) this.Base.Setup).Current.AllocateDocumentsOnHold;
        if (!nullable.GetValueOrDefault())
        {
          nullable = current.Hold;
          bool flag = false;
          num = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
        }
        else
          num = 1;
      }
      else
        num = 1;
    }
    else
      num = 0;
    bool excludeCurrent = num != 0;
    IStatus availability = this.FetchWithLineUOM(line, excludeCurrent, line.CostCenterID);
    if (availability != null)
    {
      status = this.FormatStatus(availability, line.UOM);
      this.Check((ILSMaster) line, availability);
    }
    return status;
  }

  private string FormatStatus(IStatus availability, string uom)
  {
    return PXMessages.LocalizeFormatNoPrefixNLA("On Hand {1} {0}, Available {2} {0}, Available for Shipping {3} {0}, Available for Issue {4} {0}", new object[5]
    {
      (object) uom,
      (object) this.FormatQty(availability.QtyOnHand),
      (object) this.FormatQty(availability.QtyAvail),
      (object) this.FormatQty(availability.QtyHardAvail),
      (object) this.FormatQty(availability.QtyActual)
    });
  }

  protected virtual void EventHandler(AbstractEvents.IRowUpdated<INComponentTran> e)
  {
    if (e.Row == null || PXLongOperation.Exists(((PXGraph) this.Base).UID))
      return;
    IStatus availability = this.FetchWithLineUOM(e.Row, true, e.Row.CostCenterID);
    if (availability == null)
      return;
    this.Check((ILSMaster) e.Row, availability);
  }

  protected override IEnumerable<PXExceptionInfo> GetCheckErrors(
    ILSMaster row,
    IStatus availability)
  {
    INComponentItemAvailabilityExtension availabilityExtension = this;
    // ISSUE: reference to a compiler-generated method
    foreach (PXExceptionInfo checkError in availabilityExtension.\u003C\u003En__0(row, availability))
      yield return checkError;
    short? invtMult = row.InvtMult;
    if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1)
    {
      Decimal? nullable = row.BaseQty;
      Decimal num = 0M;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue && availability != null)
      {
        nullable = availability.QtyAvail;
        Decimal? qty = row.Qty;
        if (nullable.GetValueOrDefault() < qty.GetValueOrDefault() & nullable.HasValue & qty.HasValue)
        {
          string errorMessageQtyAvail = availabilityExtension.GetErrorMessageQtyAvail(availabilityExtension.GetStatusLevel(availability));
          if (errorMessageQtyAvail != null)
            yield return new PXExceptionInfo(errorMessageQtyAvail, Array.Empty<object>());
        }
      }
    }
  }

  protected override void RaiseQtyExceptionHandling(
    INComponentTran line,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.LineCache).RaiseExceptionHandling<INComponentTran.qty>((object) line, (object) null, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.LineCache).GetStateExt<INComponentTran.inventoryID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<INComponentTran.subItemID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<INComponentTran.siteID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<INComponentTran.locationID>((object) line),
      ((PXCache) this.LineCache).GetValue<INTran.lotSerialNbr>((object) line)
    }));
  }

  protected override void RaiseQtyExceptionHandling(
    INComponentTranSplit split,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.SplitCache).RaiseExceptionHandling<INComponentTranSplit.qty>((object) split, (object) null, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.SplitCache).GetStateExt<INComponentTranSplit.inventoryID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<INComponentTranSplit.subItemID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<INComponentTranSplit.siteID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<INComponentTranSplit.locationID>((object) split),
      ((PXCache) this.SplitCache).GetValue<INComponentTranSplit.lotSerialNbr>((object) split)
    }));
  }
}
