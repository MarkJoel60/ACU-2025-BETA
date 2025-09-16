// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DraftScheduleMaintASC606MultiplebaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.DR;

public class DraftScheduleMaintASC606MultiplebaseCurrencies : 
  PXGraphExtension<DraftScheduleMaintASC606, DraftScheduleMaint>
{
  public static bool IsActive()
  {
    return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() && PXAccess.FeatureInstalled<FeaturesSet.aSC606>();
  }

  protected virtual void _(
    Events.FieldUpdated<DRScheduleMultipleBaseCurrencies.baseCuryIDASC606> e)
  {
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<DRScheduleMultipleBaseCurrencies.baseCuryIDASC606>>) e).Cache.SetValueExt<DRSchedule.baseCuryID>(e.Row, e.NewValue);
  }

  protected virtual void _(Events.FieldUpdated<DRSchedule.baseCuryID> e)
  {
    object valuePending = ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<DRSchedule.baseCuryID>>) e).Cache.GetValuePending<DRScheduleMultipleBaseCurrencies.baseCuryIDASC606>(e.Row);
    if (valuePending != PXCache.NotSetValue && valuePending != null)
      return;
    ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<DRSchedule.baseCuryID>>) e).Cache.SetValue<DRScheduleMultipleBaseCurrencies.baseCuryIDASC606>(e.Row, e.NewValue);
  }

  protected virtual void _(Events.RowSelected<DRSchedule> e)
  {
    if (e.Row.BaseCuryID == null)
    {
      Exception exception = (Exception) new PXSetPropertyException("'{0}' may not be empty.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<DRScheduleMultipleBaseCurrencies.baseCuryIDASC606>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRSchedule>>) e).Cache)
      });
      ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRSchedule>>) e).Cache.RaiseExceptionHandling<DRScheduleMultipleBaseCurrencies.baseCuryIDASC606>((object) e.Row, (object) null, exception);
    }
    else
      ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRSchedule>>) e).Cache.RaiseExceptionHandling<DRScheduleMultipleBaseCurrencies.baseCuryIDASC606>((object) e.Row, (object) null, (Exception) null);
  }
}
