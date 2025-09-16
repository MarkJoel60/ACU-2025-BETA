// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.ScheduledJobBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Data.Process;

internal abstract class ScheduledJobBase<T> where T : IItemWithRefId
{
  private readonly IScheduleAdjustmentRuleProvider _adjustmentRuleProvider;

  public ScheduledJobBase(
    IScheduleAdjustmentRuleProvider adjustmentRuleProvider)
  {
    this._adjustmentRuleProvider = adjustmentRuleProvider;
  }

  public void ProcessSchedule(
    PXGraph screenGraph,
    List<PXFilterRow> filters,
    string viewName,
    AUSchedule schedule,
    string? screenId)
  {
    using (PXPerformanceMonitor.CreatePerformanceMonitorScope("Scheduler " + schedule.Action, screenId))
    {
      List<T> listToProcess = this.GetListToProcess(screenGraph, filters, viewName, schedule, screenId);
      List<(T, PXProcessingMessage)> eventResults = new List<(T, PXProcessingMessage)>(listToProcess.Count);
      PXProcessing<ScheduledJobBase<T>.DummyDac>.ProcessScheduled<T>(this.GetProcessDelegate(screenGraph, filters, viewName, schedule, screenId, eventResults), listToProcess.ToList<T>(), schedule, screenId, (Func<System.DateTime, PXCache, Exception, List<T>, ProcessingResult>) ((startDate, histCache, ex, list) => ScheduledJobBase<T>.WriteEventDefinitionProcessingHistory(startDate, histCache, ex, schedule, eventResults)), screenGraph, this._adjustmentRuleProvider);
    }
  }

  private static ProcessingResult WriteEventDefinitionProcessingHistory(
    System.DateTime startDate,
    PXCache histCache,
    Exception? exception,
    AUSchedule schedule,
    List<(T EventDefinition, PXProcessingMessage? processingResult)> eventResults)
  {
    ProcessingResult processingResult = new ProcessingResult();
    foreach ((T EventDefinition, PXProcessingMessage processingMessage) in eventResults)
    {
      PXErrorLevel errorLevel = PXProcessing<ScheduledJobBase<T>.DummyDac>.InsertHistoryRecord(schedule, histCache, processingMessage, exception, startDate, EventDefinition.GetRefId());
      processingResult.AddToResult(errorLevel);
    }
    return processingResult;
  }

  protected abstract List<T> GetListToProcess(
    PXGraph screen,
    List<PXFilterRow> filters,
    string viewName,
    AUSchedule schedule,
    string? screenId);

  protected abstract System.Action<List<T>> GetProcessDelegate(
    PXGraph screenGraph,
    List<PXFilterRow> filters,
    string viewName,
    AUSchedule schedule,
    string? screenId,
    List<(T Row, PXProcessingMessage? processingResult)> eventResults);

  internal class DummyDac : IBqlTable, IBqlTableSystemDataStorage
  {
    public ref PXBqlTableSystemData GetBqlTableSystemData() => throw new NotImplementedException();
  }
}
