// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.CardsSynchronization.CCSyncFilteredProcessing`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.CardsSynchronization;

/// <summary>
/// Class allows to add schedule history row during credit cards synchronization.
/// These rows are inserted into AUScheduleHistory table. This class adds hook methods on button "Add Schedule"
/// </summary>
public class CCSyncFilteredProcessing<Table, FilterTable, Where, OrderBy> : 
  PXFilteredProcessing<Table, FilterTable, Where, OrderBy>
  where Table : class, IBqlTable, new()
  where FilterTable : class, IBqlTable, new()
  where Where : IBqlWhere, new()
  where OrderBy : IBqlOrderBy, new()
{
  private Action beforeScheduleAdd;
  private Action afterScheduleAdd;
  private Action beforeScheduleProcessAll;

  public CCSyncFilteredProcessing(PXGraph graph)
    : base(graph)
  {
  }

  public CCSyncFilteredProcessing(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
  }

  [PXButton(ImageKey = "AddNew", DisplayOnMainToolbar = false)]
  [PXUIField]
  protected virtual IEnumerable _ScheduleAdd_(PXAdapter adapter)
  {
    Action beforeScheduleAdd = this.beforeScheduleAdd;
    if (beforeScheduleAdd != null)
      beforeScheduleAdd();
    IEnumerable enumerable = ((PXFilteredProcessingBase<Table, FilterTable>) this)._ScheduleAdd_(adapter);
    Action afterScheduleAdd = this.afterScheduleAdd;
    if (afterScheduleAdd == null)
      return enumerable;
    afterScheduleAdd();
    return enumerable;
  }

  public void SetBeforeScheduleProcessAllAction(Action beforeAction)
  {
    this.beforeScheduleProcessAll = beforeAction;
  }

  public void SetBeforeScheduleAddAction(Action beforeScheduleAdd)
  {
    this.beforeScheduleAdd = beforeScheduleAdd;
  }

  public void SetAfterScheduleAddAction(Action afterScheduleAdd)
  {
    this.afterScheduleAdd = afterScheduleAdd;
  }

  protected virtual bool startPendingProcess(List<Table> items)
  {
    AUSchedule slot = PXContext.GetSlot<AUSchedule>();
    if (slot == null)
      return ((PXFilteredProcessingBase<Table, FilterTable>) this).startPendingProcess(items);
    ((PXProcessingBase<Table>) this)._OuterView.Cache.IsDirty = false;
    List<Table> list = ((PXProcessingBase<Table>) this).GetSelectedItems(((PXProcessingBase<Table>) this)._OuterView.Cache, (IEnumerable) items);
    PXContext.SetSlot<AUSchedule>((AUSchedule) null);
    AUSchedule scheduleparam = slot;
    if (((PXProcessingBase<Table>) this)._IsInstance)
      this.ProcessSyncCC(((PXProcessingBase<Table>) this)._ProcessDelegate, list, scheduleparam, CancellationToken.None);
    else
      ((PXSelectBase) this)._Graph.LongOperationManager.StartOperation((Action<CancellationToken>) (cancellationToken => this.ProcessSyncCC(((PXProcessingBase<Table>) this)._ProcessDelegate, list, scheduleparam, cancellationToken)));
    return true;
  }

  protected void ProcessSyncCC(
    Action<List<Table>, CancellationToken> processor,
    List<Table> list,
    AUSchedule schedule,
    CancellationToken cancellationToken)
  {
    Action scheduleProcessAll = this.beforeScheduleProcessAll;
    if (scheduleProcessAll != null)
      scheduleProcessAll();
    PXLongOperation.SetCustomInfo((object) new List<SyncCCProcessingInfoEntry>(), "PXCCProcessingState");
    list.Clear();
    ((PXProcessingBase<Table>) this)._InProc = new PXResultset<Table>();
    ((PXProcessing<Table>) this)._ProcessScheduled(processor, list, schedule, cancellationToken);
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (AUScheduleHistory)];
    if (PXLongOperation.GetCustomInfoForCurrentThread("PXCCProcessingState") is List<SyncCCProcessingInfoEntry> forCurrentThread)
    {
      foreach (SyncCCProcessingInfoEntry processingInfoEntry in forCurrentThread)
      {
        AUScheduleHistory auScheduleHistory = new AUScheduleHistory();
        auScheduleHistory.ExecutionResult = processingInfoEntry.ProcessingMessage.Message;
        auScheduleHistory.ErrorLevel = new short?((short) processingInfoEntry.ProcessingMessage.ErrorLevel);
        auScheduleHistory.ScheduleID = schedule.ScheduleID;
        auScheduleHistory.ScreenID = schedule.ScreenID;
        DateTime dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(PXTimeZoneInfo.UtcNow, PXTimeZoneInfo.FindSystemTimeZoneById(schedule.TimeZoneID));
        auScheduleHistory.ExecutionDate = new DateTime?(dateTime);
        auScheduleHistory.RefNoteID = new Guid?(processingInfoEntry.NoteId);
        cach.Insert((object) auScheduleHistory);
      }
    }
    cach.Persist((PXDBOperation) 2);
  }
}
