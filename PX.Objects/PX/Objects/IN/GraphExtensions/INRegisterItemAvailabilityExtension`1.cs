// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INRegisterItemAvailabilityExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Exceptions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class INRegisterItemAvailabilityExtension<TRegisterGraph> : 
  ItemAvailabilityExtension<TRegisterGraph, INTran, INTranSplit>
  where TRegisterGraph : INRegisterEntryBase
{
  protected override INTranSplit EnsureSplit(ILSMaster row)
  {
    return ((PXGraph) (object) this.Base).FindImplementation<INRegisterLineSplittingExtension<TRegisterGraph>>().EnsureSplit(row);
  }

  protected override Decimal GetUnitRate(INTran line)
  {
    return this.GetUnitRate<INTran.inventoryID, INTran.uOM>(line);
  }

  protected override string GetStatus(INTran line)
  {
    string status = string.Empty;
    INRegister current = (INRegister) ((PXGraph) (object) this.Base).Caches[typeof (INRegister)].Current;
    bool? nullable;
    int num1;
    if (line == null)
    {
      num1 = 1;
    }
    else
    {
      nullable = line.Released;
      num1 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    int num2;
    if (num1 != 0)
    {
      if (!(line.OrigModule != "IN"))
      {
        nullable = ((PXSelectBase<INSetup>) this.Base.insetup).Current.AllocateDocumentsOnHold;
        if (!nullable.GetValueOrDefault())
        {
          nullable = current.Hold;
          bool flag = false;
          num2 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
        }
        else
          num2 = 1;
      }
      else
        num2 = 1;
    }
    else
      num2 = 0;
    bool excludeCurrent = num2 != 0;
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

  protected override IEnumerable<PXExceptionInfo> GetCheckErrors(
    ILSMaster row,
    IStatus availability)
  {
    foreach (PXExceptionInfo checkError in base.GetCheckErrors(row, availability))
      yield return checkError;
    foreach (PXExceptionInfo checkError in this.GetCheckErrorsQtyOnHand(row, availability))
      yield return checkError;
  }

  protected virtual IEnumerable<PXExceptionInfo> GetCheckErrorsQtyOnHand(
    ILSMaster row,
    IStatus availability)
  {
    INRegisterItemAvailabilityExtension<TRegisterGraph> availabilityExtension = this;
    if (!availabilityExtension.IsAvailableOnHandQty(row, availability))
    {
      string messageQtyOnHand = availabilityExtension.GetErrorMessageQtyOnHand(availabilityExtension.GetStatusLevel(availability));
      if (messageQtyOnHand != null)
        yield return new PXExceptionInfo((PXErrorLevel) 3, messageQtyOnHand, Array.Empty<object>());
    }
  }

  protected virtual bool IsAvailableOnHandQty(ILSMaster row, IStatus availability)
  {
    short? invtMult = row.InvtMult;
    if ((invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?()).GetValueOrDefault() == -1)
    {
      Decimal? nullable = row.BaseQty;
      Decimal num1 = 0M;
      if (nullable.GetValueOrDefault() > num1 & nullable.HasValue && availability != null)
      {
        Decimal? qtyOnHand = availability.QtyOnHand;
        Decimal? qty = row.Qty;
        nullable = qtyOnHand.HasValue & qty.HasValue ? new Decimal?(qtyOnHand.GetValueOrDefault() - qty.GetValueOrDefault()) : new Decimal?();
        Decimal num2 = 0M;
        if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
        {
          INRegister current = this.Base.INRegisterDataMember.Current;
          int num3;
          if (current == null)
          {
            num3 = 0;
          }
          else
          {
            bool? released = current.Released;
            bool flag = false;
            num3 = released.GetValueOrDefault() == flag & released.HasValue ? 1 : 0;
          }
          if (num3 != 0)
            return false;
        }
      }
    }
    return true;
  }

  protected override void RaiseQtyExceptionHandling(
    INTran line,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.LineCache).RaiseExceptionHandling<INTran.qty>((object) line, (object) newValue, (Exception) new PXSetPropertyException(ei.MessageFormat, ei.ErrorLevel ?? (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.LineCache).GetStateExt<INTran.inventoryID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<INTran.subItemID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<INTran.siteID>((object) line),
      ((PXCache) this.LineCache).GetStateExt<INTran.locationID>((object) line),
      ((PXCache) this.LineCache).GetValue<INTran.lotSerialNbr>((object) line)
    }));
  }

  protected override void RaiseQtyExceptionHandling(
    INTranSplit split,
    PXExceptionInfo ei,
    Decimal? newValue)
  {
    ((PXCache) this.SplitCache).RaiseExceptionHandling<INTranSplit.qty>((object) split, (object) newValue, (Exception) new PXSetPropertyException(ei.MessageFormat, ei.ErrorLevel ?? (PXErrorLevel) 2, new object[5]
    {
      ((PXCache) this.SplitCache).GetStateExt<INTranSplit.inventoryID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<INTranSplit.subItemID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<INTranSplit.siteID>((object) split),
      ((PXCache) this.SplitCache).GetStateExt<INTranSplit.locationID>((object) split),
      ((PXCache) this.SplitCache).GetValue<INTranSplit.lotSerialNbr>((object) split)
    }));
  }
}
