// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt.INKitItemAvailabilityExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Exceptions;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt;

public class INKitItemAvailabilityExtension : 
  ItemAvailabilityExtension<KitAssemblyEntry, INKitRegister, INKitTranSplit>
{
  protected override INKitTranSplit EnsureSplit(ILSMaster row)
  {
    return ((PXGraph) this.Base).FindImplementation<INKitLineSplittingExtension>().EnsureSplit(row);
  }

  protected override Decimal GetUnitRate(INKitRegister line)
  {
    return this.GetUnitRate<INKitRegister.kitInventoryID, INKitRegister.uOM>(line);
  }

  protected override string GetStatus(INKitRegister line)
  {
    string status = string.Empty;
    IStatus availability = this.FetchWithLineUOM(line, line == null || !line.Released.GetValueOrDefault(), line.CostCenterID);
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

  protected override void RaiseQtyExceptionHandling(
    INKitRegister line,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.LineCache).RaiseExceptionHandling<INKitRegister.qty>((object) line, (object) null, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.LineCache).GetStateExt<INKitRegister.kitInventoryID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<INKitRegister.subItemID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<INKitRegister.siteID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<INKitRegister.locationID>((object) line),
      ((PXCache) this.LineCache).GetValue<INKitRegister.lotSerialNbr>((object) line)
    }));
  }

  protected override void RaiseQtyExceptionHandling(
    INKitTranSplit split,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.SplitCache).RaiseExceptionHandling<INKitTranSplit.qty>((object) split, (object) null, (Exception) new PXSetPropertyException(ei.MessageFormat, (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.SplitCache).GetStateExt<INKitTranSplit.inventoryID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<INKitTranSplit.subItemID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<INKitTranSplit.siteID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<INKitTranSplit.locationID>((object) split),
      ((PXCache) this.SplitCache).GetValue<INKitTranSplit.lotSerialNbr>((object) split)
    }));
  }

  protected override void AddStatusField()
  {
  }
}
