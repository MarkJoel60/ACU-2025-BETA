// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ScheduleRunBase`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Encapsulates the common logic for generating transactions according to
/// a schedule. Module-specific schedule running graphs derive from it.
/// </summary>
/// <typeparam name="TGraph">The specific graph type.</typeparam>
/// <typeparam name="TMaintenanceGraph">The type of the graph used to create and edit schedules for relevant entity type.</typeparam>
/// <typeparam name="TProcessGraph">The type of the graph used to run schedules for relevant entity type.</typeparam>
public class ScheduleRunBase<TGraph, TMaintenanceGraph, TProcessGraph> : PXGraph<TGraph>
  where TGraph : PXGraph
  where TMaintenanceGraph : ScheduleMaintBase<TMaintenanceGraph, TProcessGraph>, new()
  where TProcessGraph : PXGraph<TProcessGraph>, IScheduleProcessing, new()
{
  public PXFilter<ScheduleRun.Parameters> Filter;
  public PXCancel<ScheduleRun.Parameters> Cancel;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<Schedule, ScheduleRun.Parameters, Where<Schedule.active, Equal<True>, And<Schedule.nextRunDate, LessEqual<Current<ScheduleRun.Parameters.executionDate>>>>> Schedule_List;

  protected virtual bool checkAnyScheduleDetails => true;

  protected virtual IEnumerable schedule_List()
  {
    ScheduleRunBase<TGraph, TMaintenanceGraph, TProcessGraph> scheduleRunBase = this;
    IEnumerable<Schedule> schedules = GraphHelper.RowCast<Schedule>(GraphHelper.QuickSelect(new PXView((PXGraph) scheduleRunBase, false, ((PXSelectBase) scheduleRunBase.Schedule_List).View.BqlSelect)));
    if (scheduleRunBase.checkAnyScheduleDetails)
    {
      ScheduleMaintBase<TMaintenanceGraph, TProcessGraph> maintenanceGraph = (ScheduleMaintBase<TMaintenanceGraph, TProcessGraph>) PXGraph.CreateInstance<TMaintenanceGraph>();
      foreach (Schedule schedule in schedules)
      {
        ((PXSelectBase<Schedule>) maintenanceGraph.Schedule_Header).Current = schedule;
        if (maintenanceGraph.AnyScheduleDetails())
          yield return (object) schedule;
      }
      maintenanceGraph = (ScheduleMaintBase<TMaintenanceGraph, TProcessGraph>) null;
    }
    else
    {
      foreach (object obj in schedules)
        yield return obj;
    }
  }

  public ScheduleRunBase()
  {
    ((PXProcessing<Schedule>) this.Schedule_List).SetProcessCaption("Run");
    ((PXProcessing<Schedule>) this.Schedule_List).SetProcessAllCaption("Run All");
  }

  public virtual void Clear(PXClearOption option)
  {
    if (((Dictionary<Type, PXCache>) ((PXGraph) this).Caches).ContainsKey(typeof (Schedule)))
      ((PXGraph) this).Caches[typeof (Schedule)].ClearQueryCache();
    ((PXGraph) this).Clear(option);
  }

  protected IEnumerable ViewScheduleAction(PXAdapter adapter)
  {
    if (((PXSelectBase<Schedule>) this.Schedule_List).Current == null)
      return adapter.Get();
    PXRedirectHelper.TryRedirect(((PXSelectBase) this.Schedule_List).Cache, (object) ((PXSelectBase<Schedule>) this.Schedule_List).Current, "Schedule", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXUIField]
  protected virtual void Schedule_ScheduleID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void Schedule_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    Schedule row = (Schedule) e.Row;
    if (row == null || PXLongOperation.Exists(((PXGraph) this).UID))
      return;
    if (!row.NoRunLimit.GetValueOrDefault())
    {
      short? nullable1 = row.RunCntr;
      int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      nullable1 = row.RunLimit;
      int? nullable3 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
      if (nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue)
      {
        cache.RaiseExceptionHandling<Schedule.scheduleID>(e.Row, (object) row.ScheduleID, (Exception) new PXSetPropertyException("The task reached the configured limit and will not be processed. Please change the task limit or deactivate it.", (PXErrorLevel) 5));
        return;
      }
    }
    DateTime? nextRunDate = row.NextRunDate;
    DateTime? businessDate = ((PXGraph) this).Accessinfo.BusinessDate;
    if ((nextRunDate.HasValue & businessDate.HasValue ? (nextRunDate.GetValueOrDefault() > businessDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    cache.RaiseExceptionHandling<Schedule.scheduleID>(e.Row, (object) row.ScheduleID, (Exception) new PXSetPropertyException("The next generation date for this task is greater than the current business date.", (PXErrorLevel) 3));
  }

  protected virtual void Parameters_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ScheduleRun.Parameters row = e.Row as ScheduleRun.Parameters;
    PXUIFieldAttribute.SetRequired<ScheduleRun.Parameters.executionDate>(sender, row.LimitTypeSel == "D");
    sender.ClearFieldErrors<ScheduleRun.Parameters.executionDate>((object) row);
    if (row.LimitTypeSel == "D" && !row.ExecutionDate.HasValue)
      sender.DisplayFieldError<ScheduleRun.Parameters.executionDate>((object) row, "The Execution Date box cannot be empty if the Stop on Execution Date option is selected.");
    ScheduleRunBase.SetProcessDelegate<TProcessGraph>((PXGraph) this, (ScheduleRun.Parameters) e.Row, (PXProcessing<Schedule>) this.Schedule_List);
  }

  [Obsolete("This handler is not used anymore and will be removed in Acumatica 8.0")]
  protected virtual void Parameters_StartDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
  }
}
