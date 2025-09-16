// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.TransferProcessMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.FA;

public class TransferProcessMultipleBaseCurrencies : PXGraphExtension<TransferProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected virtual void _(
    Events.FieldUpdating<TransferProcess.TransferFilter, TransferProcess.TransferFilter.branchTo> e)
  {
    if (e.Row == null)
      return;
    TransferProcess.TransferFilter row = e.Row;
    if (((Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TransferProcess.TransferFilter, TransferProcess.TransferFilter.branchTo>>) e).Cache.GetValuePending<TransferProcess.TransferFilter.branchFrom>((object) row) == PXCache.NotSetValue)
      return;
    object branchId = (object) PXAccess.GetBranchID((string) ((Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TransferProcess.TransferFilter, TransferProcess.TransferFilter.branchTo>>) e).Cache.GetValuePending<TransferProcess.TransferFilter.branchFrom>((object) row));
    object obj = ((Events.FieldUpdatingBase<Events.FieldUpdating<TransferProcess.TransferFilter, TransferProcess.TransferFilter.branchTo>>) e).NewValue is int? ? ((Events.FieldUpdatingBase<Events.FieldUpdating<TransferProcess.TransferFilter, TransferProcess.TransferFilter.branchTo>>) e).NewValue : (object) PXAccess.GetBranchID((string) ((Events.FieldUpdatingBase<Events.FieldUpdating<TransferProcess.TransferFilter, TransferProcess.TransferFilter.branchTo>>) e).NewValue);
    if (obj != null && !(PXAccess.GetBranch((int?) branchId)?.BaseCuryID == PXAccess.GetBranch((int?) obj)?.BaseCuryID))
      return;
    try
    {
      ((Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TransferProcess.TransferFilter, TransferProcess.TransferFilter.branchTo>>) e).Cache.SetValue<TransferProcess.TransferFilter.branchTo>((object) row, obj);
      ((Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TransferProcess.TransferFilter, TransferProcess.TransferFilter.branchTo>>) e).Cache.SetValueExt<TransferProcess.TransferFilter.branchFrom>((object) row, branchId);
      ((Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TransferProcess.TransferFilter, TransferProcess.TransferFilter.branchTo>>) e).Cache.RaiseExceptionHandling<TransferProcess.TransferFilter.branchFrom>((object) row, branchId, (Exception) null);
      ((Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TransferProcess.TransferFilter, TransferProcess.TransferFilter.branchTo>>) e).Cache.RaiseFieldVerifying<TransferProcess.TransferFilter.branchFrom>((object) row, ref branchId);
    }
    catch (PXSetPropertyException ex)
    {
      ((Events.Event<PXFieldUpdatingEventArgs, Events.FieldUpdating<TransferProcess.TransferFilter, TransferProcess.TransferFilter.branchTo>>) e).Cache.RaiseExceptionHandling<TransferProcess.TransferFilter.branchFrom>((object) row, branchId, (Exception) ex);
    }
  }
}
