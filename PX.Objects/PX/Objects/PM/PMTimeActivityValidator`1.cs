// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTimeActivityValidator`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using System;

#nullable disable
namespace PX.Objects.PM;

public abstract class PMTimeActivityValidator<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMTimeActivity.projectTaskID> e)
  {
    PMTimeActivity row = e.Row as PMTimeActivity;
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTimeActivity.projectTaskID>, object, object>) e).NewValue == null || !(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTimeActivity.projectTaskID>, object, object>) e).NewValue is int))
      return;
    PMTask pmTask = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask>.Config>.Search<PMTask.projectID, PMTask.taskID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMTimeActivity.projectTaskID>>) e).Cache.Graph, (object) row.ProjectID, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMTimeActivity.projectTaskID>, object, object>) e).NewValue, Array.Empty<object>()));
    bool? nullable;
    if (pmTask != null)
    {
      nullable = pmTask.IsCancelled;
      if (nullable.GetValueOrDefault())
      {
        PXSetPropertyException<PMTimeActivity.projectTaskID> propertyException = new PXSetPropertyException<PMTimeActivity.projectTaskID>("Task is Canceled and cannot be used for data entry.");
        ((PXSetPropertyException) propertyException).ErrorValue = (object) pmTask.TaskCD;
        throw propertyException;
      }
    }
    if (pmTask == null)
      return;
    nullable = pmTask.IsCompleted;
    if (nullable.GetValueOrDefault())
    {
      PXSetPropertyException<PMTimeActivity.projectTaskID> propertyException = new PXSetPropertyException<PMTimeActivity.projectTaskID>("Task is Completed and cannot be used for data entry.");
      ((PXSetPropertyException) propertyException).ErrorValue = (object) pmTask.TaskCD;
      throw propertyException;
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMTimeActivity> e)
  {
    if (e.Row == null)
      return;
    PMTimeActivity row = e.Row;
    int? nullable = row.ProjectID;
    if (!nullable.HasValue)
      return;
    nullable = row.ProjectTaskID;
    if (!nullable.HasValue)
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMTimeActivity>>) e).Cache.VerifyFieldAndRaiseException<PMTimeActivity.projectTaskID>((object) e.Row);
  }
}
