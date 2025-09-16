// Decompiled with JetBrains decompiler
// Type: PX.Data.Archiving.ArchiveProcess
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Archiving.DAC;
using PX.Data.Automation.Services;
using PX.Data.BQL;
using PX.Data.PushNotifications;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

#nullable enable
namespace PX.Data.Archiving;

public class ArchiveProcess : PXGraph<
#nullable disable
ArchiveProcess>
{
  public const string AllToArchiveField = "AllToArchive";
  public const string ToArchiveSuffix = "ToArchive";
  public PXCancel<ArchiveProcess.Filter> Cancel;
  public PXFilter<ArchiveProcess.Filter> Header;
  public PXFilteredProcessing<ArchiveProcess.DateToArchive, ArchiveProcess.Filter> DatesToArchive;

  [InjectDependency]
  private INavigationService NavigationService { get; set; }

  public ArchiveProcess()
  {
    ArchiveProcess.Filter filter = this.Header.Current;
    this.DatesToArchive.SetSelected<ArchiveProcess.DateToArchive.selected>();
    this.DatesToArchive.SetProcessDelegate((PXProcessingBase<ArchiveProcess.DateToArchive>.ProcessListDelegate) (list => ArchiveProcess.Process(list, filter)));
    if (PXLongOperation.GetStatus(this.UID) == PXLongRunStatus.InProcess || PXContext.GetSlot<AUSchedule>() != null)
      return;
    this.AppendDocumentCounterFields();
  }

  protected virtual void AppendDocumentCounterFields()
  {
    ArchiveInfoHelper slot1 = ArchiveInfoHelper.Instance;
    this.DatesToArchive.Cache.Fields.Add("AllToArchive");
    this.FieldSelecting.AddHandler(typeof (ArchiveProcess.DateToArchive), "AllToArchive", new PXFieldSelecting(AllToArchiveFieldSelecting));
    foreach (ArchivalPolicy policy1 in slot1.GetPolicies())
    {
      ArchivalPolicy policy = policy1;
      string fieldName = policy.TableName + "ToArchive";
      this.DatesToArchive.Cache.Fields.Add(fieldName);
      this.FieldSelecting.AddHandler(typeof (ArchiveProcess.DateToArchive), fieldName, (PXFieldSelecting) ((c, e) => DocumentToArchiveFieldSelecting(c, e, fieldName, policy.TableName, policy.TypeName)));
      PXNamedAction.AddAction((PXGraph) this, typeof (ArchiveProcess.DateToArchive), fieldName, (string) null, (PXButtonDelegate) (adapter => DrillDown(adapter, fieldName, policy)), (PXEventSubscriberAttribute) new PXButtonAttribute()
      {
        CommitChanges = true,
        DisplayOnMainToolbar = false
      }, (PXEventSubscriberAttribute) new PXUIFieldAttribute()
      {
        Visible = false
      });
    }

    static IEnumerable DrillDown(PXAdapter a, string fieldName, ArchivalPolicy policy)
    {
      PXCache<ArchiveProcess.DateToArchive> pxCache = a.View.Graph.Caches<ArchiveProcess.DateToArchive>();
      ArchiveProcess.DateToArchive current = (ArchiveProcess.DateToArchive) pxCache.Current;
      if ((int) ((PXFieldState) pxCache.GetValueExt((object) current, fieldName)).Value != 0 && a.View.Graph is ArchiveProcess graph && graph.NavigationService != null)
      {
        ArchiveInfoHelper.TableInfo type = ArchiveInfoHelper.GetType((PXGraph) graph, policy.TypeName);
        if (type.ListScreenID == null)
          throw new PXException("The {0} entity list cannot be opened because the entity does not have a data-entry form or that form does not have a GI set as a data-entry point.", new object[1]
          {
            (object) type.CacheName
          });
        graph.NavigationService.NavigateTo(type.ListScreenID, (IReadOnlyDictionary<string, object>) new Dictionary<string, object>()
        {
          [$"{policy.TableName}_{type.GotReadyToArchiveField.Name}"] = (object) current.Date.Value.AddDays((double) -policy.DelayInDays)
        }, PXBaseRedirectException.WindowMode.NewWindow);
      }
      return a.Get();
    }

    static PXFieldState MakeIntState(int value, string fieldName, string displayName)
    {
      PXFieldState instance = PXIntState.CreateInstance((object) value, fieldName, new bool?(false), new int?(), new int?(), new int?(), (int[]) null, (string[]) null, typeof (int), new int?(), (string[]) null);
      instance.Enabled = false;
      instance.Visibility = PXUIVisibility.Dynamic;
      instance.DisplayName = PXMessages.LocalizeNoPrefix(displayName);
      return instance;
    }

    void AllToArchiveFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      int num1;
      if (e.Row is ArchiveProcess.DateToArchive row1)
      {
        System.DateTime? date1 = row1.Date;
        if (date1.HasValue)
        {
          ArchiveInfoHelper archiveInfoHelper = slot1;
          PXGraph graph = sender.Graph;
          date1 = row1.Date;
          System.DateTime date2 = date1.Value;
          num1 = archiveInfoHelper.GetAllCount(graph, date2);
          goto label_4;
        }
      }
      num1 = 0;
label_4:
      int num2 = num1;
      e.ReturnState = (object) MakeIntState(num2, "AllToArchive", "All Documents");
      e.ReturnValue = (object) num2;
    }
    ArchiveInfoHelper slot2;

    void DocumentToArchiveFieldSelecting(
      PXCache sender,
      PXFieldSelectingEventArgs e,
      string fieldName,
      string tableName,
      string dacFullName)
    {
      int num1;
      if (e.Row is ArchiveProcess.DateToArchive row2)
      {
        System.DateTime? date1 = row2.Date;
        if (date1.HasValue)
        {
          ArchiveInfoHelper archiveInfoHelper = slot2;
          PXGraph graph = sender.Graph;
          date1 = row2.Date;
          System.DateTime date2 = date1.Value;
          string dacFullName1 = dacFullName;
          num1 = archiveInfoHelper.GetCount(graph, date2, dacFullName1);
          goto label_4;
        }
      }
      num1 = 0;
label_4:
      int num2 = num1;
      e.ReturnState = (object) MakeIntState(num2, fieldName, slot2.GetCacheName(sender.Graph, dacFullName));
      e.ReturnValue = (object) num2;
    }
  }

  public virtual IEnumerable datesToArchive()
  {
    PXDelegateResult archive = new PXDelegateResult()
    {
      IsResultTruncated = true,
      IsResultFiltered = true,
      IsResultSorted = true
    };
    IEnumerable<ArchiveProcess.DateToArchive> dateToArchives;
    if (PXView.MaximumRows == 1 && PXView.SortColumns != null && PXView.Searches != null)
    {
      int index = Array.FindIndex<string>(PXView.SortColumns, (Predicate<string>) (s => s.Equals("date", StringComparison.OrdinalIgnoreCase)));
      if (index >= 0 && PXView.Searches[index] is System.DateTime search)
      {
        dateToArchives = ArchiveInfoHelper.Instance.GetArchivableDates((PXGraph) this, new System.DateTime?(search), new System.DateTime?(search)).Select<System.DateTime, ArchiveProcess.DateToArchive>((Func<System.DateTime, ArchiveProcess.DateToArchive>) (d => new ArchiveProcess.DateToArchive()
        {
          Date = new System.DateTime?(d)
        }));
        goto label_6;
      }
    }
    if (PXView.StartRow == 0 && PXView.MaximumRows == 0)
    {
      dateToArchives = ArchiveInfoHelper.Instance.GetArchivableDates((PXGraph) this).Select<System.DateTime, ArchiveProcess.DateToArchive>((Func<System.DateTime, ArchiveProcess.DateToArchive>) (d => new ArchiveProcess.DateToArchive()
      {
        Date = new System.DateTime?(d),
        EndDate = this.Accessinfo.BusinessDate
      })).Take<ArchiveProcess.DateToArchive>(1);
    }
    else
    {
      IEnumerable<ArchiveProcess.DateToArchive> source = ArchiveInfoHelper.Instance.GetArchivableDates((PXGraph) this).Select<System.DateTime, ArchiveProcess.DateToArchive>((Func<System.DateTime, ArchiveProcess.DateToArchive>) (d => new ArchiveProcess.DateToArchive()
      {
        Date = new System.DateTime?(d)
      }));
      dateToArchives = PXView.StartRow < 0 ? source.Reverse<ArchiveProcess.DateToArchive>().Skip<ArchiveProcess.DateToArchive>(System.Math.Abs(PXView.MaximumRows + PXView.StartRow)).Take<ArchiveProcess.DateToArchive>(PXView.MaximumRows).Reverse<ArchiveProcess.DateToArchive>() : source.Skip<ArchiveProcess.DateToArchive>(PXView.StartRow).Take<ArchiveProcess.DateToArchive>(PXView.MaximumRows);
    }
label_6:
    foreach (ArchiveProcess.DateToArchive dateToArchive1 in dateToArchives)
    {
      if (this.DatesToArchive.Cache.Locate((object) dateToArchive1) is ArchiveProcess.DateToArchive dateToArchive2)
        dateToArchive1.Selected = dateToArchive2.Selected;
      else
        this.DatesToArchive.Cache.SetStatus((object) dateToArchive1, PXEntryStatus.Held);
      archive.Add((object) dateToArchive1);
    }
    return (IEnumerable) archive;
  }

  protected virtual void _(Events.RowSelected<ArchiveProcess.Filter> e)
  {
    ArchiveProcess.Filter row = e.Row;
    int num1;
    if (row == null)
    {
      num1 = 0;
    }
    else
    {
      int? durationLimitInHours = row.ArchivingProcessDurationLimitInHours;
      int num2 = 8;
      num1 = durationLimitInHours.GetValueOrDefault() <= num2 & durationLimitInHours.HasValue ? 1 : 0;
    }
    if (num1 != 0)
    {
      this.DatesToArchive.SetProcessAllEnabled(true);
      this.DatesToArchive.SetProcessEnabled(true);
    }
    else
    {
      this.DatesToArchive.SetProcessAllEnabled(false);
      this.DatesToArchive.SetProcessEnabled(false);
    }
  }

  private static void Process(List<ArchiveProcess.DateToArchive> list, ArchiveProcess.Filter filter)
  {
    int? durationLimitInHours = filter.ArchivingProcessDurationLimitInHours;
    int num = 8;
    if (durationLimitInHours.GetValueOrDefault() > num & durationLimitInHours.HasValue)
      throw new PXException("The duration of the archiving process temporarily cannot exceed 8 hours. This value will be increased in future versions.");
    ArchiveInfoHelper instance = ArchiveInfoHelper.Instance;
    try
    {
      using (new SuppressPushNotificationsScope())
      {
        using (new SuppressPerformanceInfoScope())
        {
          using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
          {
            cancellationTokenSource.CancelAfter(TimeSpan.FromHours((double) (filter.ArchivingProcessDurationLimitInHours ?? 1)));
            ArchiveProcess.ProcessArchive(instance, (IEnumerable<ArchiveProcess.DateToArchive>) list, cancellationTokenSource.Token);
          }
        }
      }
    }
    finally
    {
      instance.ClearData();
    }
  }

  private static void ProcessArchive(
    ArchiveInfoHelper archiveInfo,
    IEnumerable<ArchiveProcess.DateToArchive> datesToArchive,
    CancellationToken cancelToken)
  {
    System.DateTime executionDate = PXTimeZoneInfo.UtcNow.With<System.DateTime, System.DateTime>((Func<System.DateTime, System.DateTime>) (n => new System.DateTime(n.Year, n.Month, n.Day, n.Hour, n.Minute, n.Second)));
    Lazy<ArchiveProcess.InspectorGraph> lazyInstance = PXGraph.CreateLazyInstance<ArchiveProcess.InspectorGraph>();
    Dictionary<string, ArchiveProcess.DocumentArchiver> dictionary = new Dictionary<string, ArchiveProcess.DocumentArchiver>();
    bool flag = true;
    if (datesToArchive.Count<ArchiveProcess.DateToArchive>() == 1)
    {
      ArchiveProcess.DateToArchive dateToArchive = datesToArchive.First<ArchiveProcess.DateToArchive>();
      if (dateToArchive != null && dateToArchive.Date.HasValue && dateToArchive.EndDate.HasValue)
      {
        datesToArchive = ArchiveProcess.ExpandPeriod(dateToArchive.Date.Value, dateToArchive.EndDate.Value).Select<System.DateTime, ArchiveProcess.DateToArchive>((Func<System.DateTime, ArchiveProcess.DateToArchive>) (d => new ArchiveProcess.DateToArchive()
        {
          Date = new System.DateTime?(d)
        }));
        flag = false;
      }
    }
    foreach (ArchiveProcess.DateToArchive currentItem in datesToArchive)
    {
      if (cancelToken.IsCancellationRequested)
        break;
      if (flag)
        PXProcessing.SetCurrentItem((object) currentItem);
      int num1 = 0;
      int num2 = 0;
      foreach (ArchivalPolicy policy in archiveInfo.GetPolicies())
      {
        if (!cancelToken.IsCancellationRequested)
        {
          using (ArchiveProcess.InspectorGraph.LogScope logScope = lazyInstance.Value.GetLogScope(policy, currentItem.Date.Value, executionDate))
          {
            ArchiveProcess.DocumentArchiver documentArchiver;
            if (!dictionary.TryGetValue(policy.TypeName, out documentArchiver))
            {
              documentArchiver = new ArchiveProcess.DocumentArchiver(policy.TypeName, archiveInfo, lazyInstance.Value);
              dictionary.Add(policy.TypeName, documentArchiver);
            }
            int num3 = 0;
            int num4 = 0;
            foreach (IBqlTable selectRecord in documentArchiver.SelectRecords(currentItem.Date.Value))
            {
              if (!cancelToken.IsCancellationRequested)
              {
                ++num4;
                try
                {
                  logScope.EnsureLog();
                  documentArchiver.Archive(selectRecord, currentItem.Date.Value, executionDate);
                }
                catch (Exception ex)
                {
                  ++num3;
                  PXTrace.WriteError(ex);
                  documentArchiver.LogArchiveFail(selectRecord);
                }
              }
              else
                break;
            }
            num2 += num4;
            num1 += num3;
            if (num4 > 0)
              documentArchiver.Clear();
          }
        }
        else
          break;
      }
      if (flag)
      {
        if (num1 == 0)
          PXProcessing.SetProcessed();
        else
          PXProcessing.SetWarning(PXMessages.LocalizeFormatNoPrefix("{0} out of {1} documents could not be archived. For details, see the trace log: Click Tools > Trace on the form title bar.", (object) num1, (object) num2));
      }
    }
  }

  private static IEnumerable<System.DateTime> ExpandPeriod(System.DateTime from, System.DateTime to)
  {
    for (System.DateTime current = from; current <= to; current = current.AddDays(1.0))
      yield return current;
  }

  [PXHidden]
  public class Filter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt(MinValue = 1, MaxValue = 24)]
    [PXDefault(typeof (Search<ArchivalSetup.archivingProcessDurationLimitInHours>))]
    [PXUIField(DisplayName = "Archiving Process Duration (Hours)")]
    [PXUIVerify(typeof (BqlOperand<ArchiveProcess.Filter.archivingProcessDurationLimitInHours, IBqlInt>.IsLessEqual<ArchivalSetup.archivingProcessDurationLimitInHours.maxValue>), PXErrorLevel.Error, "The duration of the archiving process temporarily cannot exceed 8 hours. This value will be increased in future versions.", new System.Type[] {})]
    public virtual int? ArchivingProcessDurationLimitInHours { get; set; }

    public abstract class archivingProcessDurationLimitInHours : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchiveProcess.Filter.archivingProcessDurationLimitInHours>
    {
    }
  }

  [PXHidden]
  public class DateToArchive : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXBool]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected { get; set; }

    [PXDate(IsKey = true)]
    [PXUIField(DisplayName = "Ready-to-Archive Date")]
    public virtual System.DateTime? Date { get; set; }

    [PXDate]
    public virtual System.DateTime? EndDate { get; set; }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ArchiveProcess.DateToArchive.selected>
    {
    }

    public abstract class date : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ArchiveProcess.DateToArchive.date>
    {
    }

    public abstract class endDate : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ArchiveProcess.DateToArchive.endDate>
    {
    }
  }

  private class DocumentArchiver
  {
    private readonly string _entityFullName;
    private readonly ArchiveInfoHelper _archiveSlot;
    private readonly ArchiveInfoHelper.TableInfo _tableInfo;
    private readonly PXGraph _graph;
    private readonly PXCache _cache;
    private readonly EntityHelper _entityHelper;
    private readonly PXAction _archiveAction;

    public DocumentArchiver(
      string entityFullName,
      ArchiveInfoHelper archiveSlot,
      ArchiveProcess.InspectorGraph inspectorGraph)
    {
      this._entityFullName = entityFullName;
      this._archiveSlot = archiveSlot;
      this._tableInfo = ArchiveInfoHelper.GetType((PXGraph) inspectorGraph, entityFullName);
      System.Type graphType = this._tableInfo.PrimaryGraph;
      if ((object) graphType == null)
        graphType = typeof (ArchiveProcess.GenericArchiveGraph<>).MakeGenericType(this._tableInfo.Table);
      this._graph = PXGraph.CreateInstance(graphType);
      this._cache = this._graph.Caches[this._tableInfo.Table];
      this._entityHelper = new EntityHelper(this._graph);
      this._archiveAction = this._graph.Actions.Values.Cast<PXAction>().FirstOrDefault<PXAction>((Func<PXAction, bool>) (a =>
      {
        PXSpecialButtonType? specialType = a.GetSpecialType();
        PXSpecialButtonType specialButtonType = PXSpecialButtonType.Archive;
        return specialType.GetValueOrDefault() == specialButtonType & specialType.HasValue;
      }));
      if (this._archiveAction != null)
        return;
      this._archiveAction = (PXAction) Activator.CreateInstance(typeof (PXArchive<>).MakeGenericType(this._tableInfo.Table), (object) this._graph, (object) "Archive");
    }

    public IEnumerable<IBqlTable> SelectRecords(System.DateTime dateToArchive)
    {
      return this._archiveSlot.SelectRecords(this._graph, this._entityFullName, dateToArchive);
    }

    public void Archive(IBqlTable rec, System.DateTime dateToArchive, System.DateTime executionDate)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this._graph.Clear(PXClearOption.ClearAll);
        this._cache.Current = (object) rec;
        this._archiveAction.Press();
        this._cache.Current = (object) null;
        this._graph.EnsureCachePersistence<ArchiveProcess.ArchivedDocumentBatchByDateDelta>();
        PXCache<ArchiveProcess.ArchivedDocumentBatchByDateDelta> pxCache = this._graph.Caches<ArchiveProcess.ArchivedDocumentBatchByDateDelta>();
        pxCache.Insert(new ArchiveProcess.ArchivedDocumentBatchByDateDelta()
        {
          DateToArchive = new System.DateTime?(dateToArchive),
          ExecutionDate = new System.DateTime?(executionDate),
          TableName = this._tableInfo.Table.Name,
          ArchivedRowsCount = new int?(1),
          ExecutionTimeInSeconds = new int?(0)
        });
        pxCache.Persist(PXDBOperation.Insert);
        transactionScope.Complete(this._graph);
        pxCache.Persisted(false, (Exception) null);
      }
    }

    public void LogArchiveFail(IBqlTable rec)
    {
      string str = ((IEnumerable<string>) this._entityHelper.GetFriendlyEntityName(rec).Split('+', '.')).Last<string>();
      PXTrace.WriteError(PXMessages.LocalizeFormatNoPrefixNLA("The {0} document with the {1} type cannot be archived. See the error below:", (object) this._entityHelper.GetEntityKeysDescription(rec), (object) str));
    }

    public void Clear() => this._graph.Clear();
  }

  private class GenericArchiveGraph<TEntity> : 
    PXGraph<ArchiveProcess.GenericArchiveGraph<TEntity>, TEntity>
    where TEntity : class, IBqlTable, new()
  {
    public PXSelect<TEntity> Primary;
  }

  private class InspectorGraph : PXGraph<ArchiveProcess.InspectorGraph>
  {
    public PXSelect<ArchivedDocumentBatchByDate> Log;
    public PXSelect<ArchiveProcess.ArchivedDocumentBatchByDateDelta> LogDelta;

    public ArchiveProcess.InspectorGraph.LogScope GetLogScope(
      ArchivalPolicy policy,
      System.DateTime dateToArchive,
      System.DateTime executionDate)
    {
      return new ArchiveProcess.InspectorGraph.LogScope(this, policy, dateToArchive, executionDate);
    }

    internal class LogScope : IDisposable
    {
      private readonly ArchiveProcess.InspectorGraph _inspectorGraph;
      private readonly Stopwatch _stopwatch;
      private readonly ArchivalPolicy _policy;
      private readonly System.DateTime _dateToArchive;
      private readonly System.DateTime _executionDate;
      private bool _initialized;

      public LogScope(
        ArchiveProcess.InspectorGraph inspectorGraph,
        ArchivalPolicy policy,
        System.DateTime dateToArchive,
        System.DateTime executionDate)
      {
        this._inspectorGraph = inspectorGraph;
        this._policy = policy;
        this._dateToArchive = dateToArchive;
        this._executionDate = executionDate;
        this._stopwatch = Stopwatch.StartNew();
      }

      public void EnsureLog()
      {
        if (this._initialized)
          return;
        this._inspectorGraph.Log.Insert(new ArchivedDocumentBatchByDate()
        {
          DateToArchive = new System.DateTime?(this._dateToArchive),
          ExecutionDate = new System.DateTime?(this._executionDate),
          TableName = this._policy.TableName,
          TypeName = this._policy.TypeName,
          ExecutionTimeInSeconds = new int?(0),
          ArchivedRowsCount = new int?(0)
        });
        this._inspectorGraph.Persist();
        this._initialized = true;
      }

      public void Dispose()
      {
        this._stopwatch.Stop();
        if (!this._initialized)
          return;
        this._inspectorGraph.LogDelta.Insert(new ArchiveProcess.ArchivedDocumentBatchByDateDelta()
        {
          DateToArchive = new System.DateTime?(this._dateToArchive),
          ExecutionDate = new System.DateTime?(this._executionDate),
          TableName = this._policy.TableName,
          ArchivedRowsCount = new int?(0),
          ExecutionTimeInSeconds = new int?((int) this._stopwatch.Elapsed.TotalSeconds)
        });
        this._inspectorGraph.Persist();
      }
    }
  }

  [PXHidden]
  [ArchiveProcess.ArchivedDocumentBatchByDateDelta.Accumulator(BqlTable = typeof (ArchivedDocumentBatchByDate))]
  public class ArchivedDocumentBatchByDateDelta : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBDate(IsKey = true)]
    public virtual System.DateTime? DateToArchive { get; set; }

    [PXDBDate(IsKey = true, PreserveTime = true, UseTimeZone = false)]
    public virtual System.DateTime? ExecutionDate { get; set; }

    [PXDBString(1024 /*0x0400*/, IsUnicode = true, IsKey = true, InputMask = "")]
    public virtual string TableName { get; set; }

    [PXDBInt]
    public virtual int? ExecutionTimeInSeconds { get; set; }

    [PXDBInt]
    public virtual int? ArchivedRowsCount { get; set; }

    public abstract class dateToArchive : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchiveProcess.ArchivedDocumentBatchByDateDelta.dateToArchive>
    {
    }

    public abstract class executionDate : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchiveProcess.ArchivedDocumentBatchByDateDelta.executionDate>
    {
    }

    public abstract class tableName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ArchiveProcess.ArchivedDocumentBatchByDateDelta.tableName>
    {
    }

    public abstract class executionTimeInSeconds : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchiveProcess.ArchivedDocumentBatchByDateDelta.executionTimeInSeconds>
    {
    }

    public abstract class archivedRowsCount : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ArchiveProcess.ArchivedDocumentBatchByDateDelta.archivedRowsCount>
    {
    }

    public class AccumulatorAttribute : PXAccumulatorAttribute
    {
      public AccumulatorAttribute() => this._SingleRecord = true;

      protected override bool PrepareInsert(
        PXCache sender,
        object row,
        PXAccumulatorCollection columns)
      {
        if (!base.PrepareInsert(sender, row, columns))
          return false;
        ArchiveProcess.ArchivedDocumentBatchByDateDelta batchByDateDelta = (ArchiveProcess.ArchivedDocumentBatchByDateDelta) row;
        columns.UpdateOnly = true;
        columns.Update<ArchiveProcess.ArchivedDocumentBatchByDateDelta.executionTimeInSeconds>((object) batchByDateDelta.ExecutionTimeInSeconds, PXDataFieldAssign.AssignBehavior.Summarize);
        columns.Update<ArchiveProcess.ArchivedDocumentBatchByDateDelta.archivedRowsCount>((object) batchByDateDelta.ArchivedRowsCount, PXDataFieldAssign.AssignBehavior.Summarize);
        return true;
      }
    }
  }

  [PXLocalizable]
  public static class Msg
  {
    public const string AllDocsToArchiveDisplayName = "All Documents";
    public const string NotAllDocumentsWereArchived = "{0} out of {1} documents could not be archived. For details, see the trace log: Click Tools > Trace on the form title bar.";
    public const string CantArchiveDocument = "The {0} document with the {1} type cannot be archived. See the error below:";
    public const string CantDrillDown = "The {0} entity list cannot be opened because the entity does not have a data-entry form or that form does not have a GI set as a data-entry point.";
  }
}
