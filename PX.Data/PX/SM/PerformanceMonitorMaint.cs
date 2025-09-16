// Decompiled with JetBrains decompiler
// Type: PX.SM.PerformanceMonitorMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using PX.Api;
using PX.Archiver;
using PX.BulkInsert;
using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Process.Automation;
using PX.DbServices.Commands;
using PX.DbServices.Commands.Data;
using PX.DbServices.Points;
using PX.DbServices.Points.DbmsBase;
using PX.DbServices.QueryObjectModel;
using PX.Web.UI;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Hosting;

#nullable enable
namespace PX.SM;

[PXDisableWorkflow]
[Serializable]
public class PerformanceMonitorMaint : PXGraph<
#nullable disable
PerformanceMonitorMaint>
{
  public PXFilter<PerformanceMonitorMaint.SMPerformanceFilterRow> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectOrderBy<SMPerformanceInfo, OrderBy<Desc<SMPerformanceInfo.requestStartTime>>> Samples;
  public PXSelectReadonly<SMPerformanceInfoSQLText> SqlText;
  public PXSelectReadonly<SMPerformanceInfoStackTrace> StackTrace;
  public PXSelect<SMPerformanceInfoSQL> SqlSamples;
  public PXSelectReadonly<SMPerformanceInfoSQLWithTables, Where<SMPerformanceInfoSQLWithTables.parentId, Equal<Current<SMPerformanceInfo.recordId>>>> Sql;
  public PXSelectReadonly<SMPerformanceInfoSQLWithTables> SqlImport;
  public PXSelectReadonly2<SMPerformanceInfoSQLWithTables, InnerJoin<SMPerformanceInfo, On<SMPerformanceInfo.recordId, Equal<SMPerformanceInfoSQLWithTables.parentId>>>, Where<SMPerformanceInfo.userId, Equal<Required<SMPerformanceInfo.userId>>>> SqlImportUser;
  public PXSelectReadonly2<SMPerformanceInfoSQLWithTables, InnerJoin<SMPerformanceInfo, On<SMPerformanceInfo.recordId, Equal<SMPerformanceInfoSQLWithTables.parentId>>>, Where<SMPerformanceInfo.recordId, Equal<Required<SMPerformanceInfo.recordId>>>> SqlImportRecord;
  public PXSelect<SMPerformanceInfoTraceWithMessages, Where<SMPerformanceInfoTraceWithMessages.parentId, Equal<Current<SMPerformanceInfo.recordId>>, And<SMPerformanceInfoTraceEvents.traceType, NotEqual<PXPerformanceMonitor.traceEventType>>>> TraceEvents;
  public PXFilter<SMPerformanceInfoTraceWithMessages> TraceEventWithMessage;
  public PXSelect<SMPerformanceInfoTraceWithMessages, Where<SMPerformanceInfoTraceWithMessages.parentId, Equal<Current<SMPerformanceInfo.recordId>>, And<SMPerformanceInfoTraceEvents.traceType, Equal<PXPerformanceMonitor.traceEventType>>>> TraceExceptions;
  public PXSelectReadonly<SMPerformanceInfoTraceMessages> TraceEventMessage;
  public PXSelectReadonly<SMPerformanceInfoTraceWithMessages, Where<SMPerformanceInfoTraceWithMessages.parentId, Equal<Current<SMPerformanceInfo.recordId>>>> TraceMessage;
  public PXSelectReadonly<SMPerformanceInfoTraceWithMessages> TraceMessageImport;
  public PXSelectReadonly2<SMPerformanceInfoTraceWithMessages, InnerJoin<SMPerformanceInfo, On<SMPerformanceInfo.recordId, Equal<SMPerformanceInfoTraceWithMessages.parentId>>>, Where<SMPerformanceInfo.userId, Equal<Required<SMPerformanceInfo.userId>>>> TraceMessageImportUser;
  public PXSelectReadonly2<SMPerformanceInfoTraceWithMessages, InnerJoin<SMPerformanceInfo, On<SMPerformanceInfo.recordId, Equal<SMPerformanceInfoTraceWithMessages.parentId>>>, Where<SMPerformanceInfo.recordId, Equal<Required<SMPerformanceInfo.recordId>>>> TraceMessageImportRecord;
  public PXSelectReadonly3<SMPerformanceInfoSQLSummary, OrderBy<Desc<SMPerformanceInfoSQLSummary.totalSQLTime>>> SqlSummary;
  public PXFilter<SMPerformanceInfoSQLSummary> SqlSummaryFilter;
  public PXSelectReadonly2<SMPerformanceInfoSQLWithTables, InnerJoin<SMPerformanceInfo, On<SMPerformanceInfo.recordId, Equal<SMPerformanceInfoSQLWithTables.parentId>>>, Where<SMPerformanceInfoSQL.queryCache, Equal<PX.Data.Zero>, And<SMPerformanceInfoSQL.sqlId, Equal<Current<SMPerformanceInfoSQLSummary.recordId>>>>> SqlSummaryRows;
  public PXFilter<SMPerformanceInfoSQLWithTables> SqlSummaryRowsPreview;
  public PXSelectReadonly<SMPerformanceInfoExceptionSummary> TraceExceptionsSummary;
  public PXSelectReadonly2<SMPerformanceInfoTraceWithMessages, InnerJoin<SMPerformanceInfo, On<SMPerformanceInfo.recordId, Equal<SMPerformanceInfoTraceWithMessages.parentId>>>, Where<SMPerformanceInfoTraceEvents.traceMessageId, Equal<Current<SMPerformanceInfoExceptionSummary.traceMessageId>>, And<SMPerformanceInfoTraceEvents.traceType, Equal<PXPerformanceMonitor.traceEventType>>>> TraceExceptionDetails;
  public PXSelectReadonly2<SMPerformanceInfoTraceWithMessages, LeftJoin<SMPerformanceInfo, On<SMPerformanceInfo.recordId, Equal<SMPerformanceInfoTraceWithMessages.parentId>>>, Where<SMPerformanceInfoTraceEvents.traceType, NotEqual<PXPerformanceMonitor.traceEventType>>, OrderBy<Desc<SMPerformanceInfoTraceEvents.eventDateTime>>> TraceEventsLog;
  public PXFilter<SMPerformanceInfoTraceWithMessages> TraceEventsLogDetails;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionStartMonitor;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionStopMonitor;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionFlushSamples;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionClearSamples;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionOpenUrl;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionViewScreen;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionSelectRows;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionPinRows;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionSaveRows;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionViewSql;
  public PXAction<SMPerformanceInfo> ActionViewExceptions;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionViewExceptionDetails;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionViewTrace;
  public PXAction<SMPerformanceInfoSQLSummary> ActionViewSqlSummaryRows;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionViewEventDetails;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionDownloadJson;
  public PXAction<PerformanceMonitorMaint.SMPerformanceFilterRow> ActionImportLogs;
  public PXSelect<PerformanceMonitorMaint.SMPerformanceFilterRow> UploadDialogPanel;
  private static string Pinned = Sprite.Ac.GetFullUrl("pin");
  private static string Unpinned = Sprite.Control.GetFullUrl("Empty");
  private static string Headers;
  private static HashSet<int> sqlTextsIds = new HashSet<int>();
  private static Dictionary<int, string> sqlTextsTables = new Dictionary<int, string>();
  private static HashSet<int> sqlTracesIds = new HashSet<int>();
  private static HashSet<int> traceMessagesIds = new HashSet<int>();
  private static int maxCacheSize = 100000;
  private static System.DateTime _lastCheckTime = System.DateTime.Now;
  private static object _lastCheckTimeLock = new object();
  private static readonly Regex InsertOrUpdatedStatement = new Regex("(INSERT\\s+\\w+)|(UPDATE\\s+\\w+)");

  public PerformanceMonitorMaint()
  {
    this.Samples.Cache.AllowInsert = false;
    this.Samples.Cache.AllowDelete = false;
    foreach (string field in (List<string>) this.Samples.Cache.Fields)
      PXUIFieldAttribute.SetEnabled(this.Samples.Cache, field, false);
    PXUIFieldAttribute.SetEnabled<SMPerformanceInfo.isChecked>(this.Samples.Cache, (object) null, true);
    this.Samples.Cache.AllowUpdate = true;
    this.Samples.AllowUpdate = true;
    this.Sql.Cache.AllowInsert = false;
    this.Sql.Cache.AllowUpdate = false;
    this.Sql.Cache.AllowDelete = false;
    this.Sql.View.IsReadOnly = true;
    this.TraceEvents.Cache.AllowInsert = false;
    this.TraceEvents.Cache.AllowUpdate = false;
    this.TraceEvents.Cache.AllowDelete = false;
    this.TraceEvents.View.IsReadOnly = true;
    this.SqlSummaryFilter.View.IsReadOnly = true;
    this.SqlSummaryRowsPreview.View.IsReadOnly = true;
    PXContext.SetSlot<bool>("PerformanceMonitorLogExpensiveRequests", PXPerformanceMonitor.LogExpensiveRequests);
    PXContext.SetSlot<bool>("PerformanceMonitorLogImportantExceptions", PXPerformanceMonitor.LogImportantExceptions);
    this.EnsureProfilerDataSizeIsWithInTheLimitAndUpdateStateWithControls();
  }

  public override void InitCacheMapping(Dictionary<System.Type, System.Type> map)
  {
    base.InitCacheMapping(map);
    this.Caches.AddCacheMapping(typeof (SMPerformanceInfoTraceEvents), typeof (SMPerformanceInfoTraceEvents));
  }

  protected virtual IEnumerable sqlSummary()
  {
    List<SMPerformanceInfoSQLSummary> performanceInfoSqlSummaryList = new List<SMPerformanceInfoSQLSummary>();
    foreach ((SMPerformanceInfoSQL performanceInfoSql, SMPerformanceInfoSQLText performanceInfoSqlText, int? nullable) in PXSelectBase<SMPerformanceInfoSQL, PXSelectJoinGroupBy<SMPerformanceInfoSQL, InnerJoin<SMPerformanceInfoSQLText, On<SMPerformanceInfoSQL.sqlId, Equal<SMPerformanceInfoSQLText.recordId>>>, Where<SMPerformanceInfoSQL.queryCache, Equal<PX.Data.Zero>>, Aggregate<GroupBy<SMPerformanceInfoSQL.sqlId, Sum<SMPerformanceInfoSQL.sqlTimeMs, Sum<SMPerformanceInfoSQL.nRows, Count>>>>>.Config>.Select((PXGraph) this).AsEnumerable<PXResult<SMPerformanceInfoSQL>>().Select<PXResult<SMPerformanceInfoSQL>, (SMPerformanceInfoSQL, SMPerformanceInfoSQLText, int?)>((Func<PXResult<SMPerformanceInfoSQL>, (SMPerformanceInfoSQL, SMPerformanceInfoSQLText, int?)>) (r => (r.GetItem<SMPerformanceInfoSQL>(), r.GetItem<SMPerformanceInfoSQLText>(), r.RowCount))))
    {
      SMPerformanceInfoSQLSummary performanceInfoSqlSummary = new SMPerformanceInfoSQLSummary()
      {
        RecordId = performanceInfoSql.SqlId,
        TableList = performanceInfoSqlText.TableList,
        SQLText = performanceInfoSqlText.SQLText,
        QueryHash = performanceInfoSqlText.SQLHash,
        TotalExecutions = nullable,
        TotalRows = performanceInfoSql.NRows,
        TotalSQLTime = performanceInfoSql.SqlTimeMs
      };
      if (this.SqlSummary.Cache.Locate((object) performanceInfoSqlSummary) == null)
      {
        performanceInfoSqlSummary = (SMPerformanceInfoSQLSummary) this.SqlSummary.Cache.Insert((object) performanceInfoSqlSummary);
        this.SqlSummary.Cache.SetStatus((object) performanceInfoSqlSummary, PXEntryStatus.Notchanged);
      }
      performanceInfoSqlSummaryList.Add(performanceInfoSqlSummary);
    }
    this.SqlSummary.Cache.IsDirty = false;
    return (IEnumerable) performanceInfoSqlSummaryList;
  }

  protected virtual IEnumerable traceExceptionsSummary()
  {
    List<SMPerformanceInfoExceptionSummary> exceptionSummaryList = new List<SMPerformanceInfoExceptionSummary>();
    foreach ((SMPerformanceInfoTraceEvents performanceInfoTraceEvents, SMPerformanceInfoTraceMessages infoTraceMessages, SMPerformanceInfo smPerformanceInfo1, int? nullable) in PXSelectBase<SMPerformanceInfoTraceEvents, PXSelectJoinGroupBy<SMPerformanceInfoTraceEvents, InnerJoin<SMPerformanceInfo, On<SMPerformanceInfoTraceEvents.parentId, Equal<SMPerformanceInfo.recordId>>, InnerJoin<SMPerformanceInfoTraceMessages, On<SMPerformanceInfoTraceEvents.traceMessageId, Equal<SMPerformanceInfoTraceMessages.recordId>>>>, Where<SMPerformanceInfoTraceEvents.traceType, Equal<PXPerformanceMonitor.traceEventType>>, Aggregate<GroupBy<SMPerformanceInfo.tenantId, GroupBy<SMPerformanceInfoTraceEvents.traceMessageId, Count>>>>.Config>.Select((PXGraph) this).AsEnumerable<PXResult<SMPerformanceInfoTraceEvents>>().Select<PXResult<SMPerformanceInfoTraceEvents>, (SMPerformanceInfoTraceEvents, SMPerformanceInfoTraceMessages, SMPerformanceInfo, int?)>((Func<PXResult<SMPerformanceInfoTraceEvents>, (SMPerformanceInfoTraceEvents, SMPerformanceInfoTraceMessages, SMPerformanceInfo, int?)>) (r => (r.GetItem<SMPerformanceInfoTraceEvents>(), r.GetItem<SMPerformanceInfoTraceMessages>(), r.GetItem<SMPerformanceInfo>(), r.RowCount))))
    {
      PXResultset<SMPerformanceInfoTraceEvents> pxResultset = PXSelectBase<SMPerformanceInfoTraceEvents, PXSelectReadonly2<SMPerformanceInfoTraceEvents, LeftJoin<SMPerformanceInfoStackTrace, On<SMPerformanceInfoTraceEvents.stackTraceId, Equal<SMPerformanceInfoStackTrace.recordId>>, LeftJoin<SMPerformanceInfo, On<SMPerformanceInfoTraceEvents.parentId, Equal<SMPerformanceInfo.recordId>>>>, Where<SMPerformanceInfoTraceEvents.traceMessageId, Equal<Required<SMPerformanceInfoTraceEvents.traceMessageId>>, And<SMPerformanceInfoTraceEvents.traceType, Equal<PXPerformanceMonitor.traceEventType>>>, OrderBy<Desc<SMPerformanceInfo.requestStartTime>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) performanceInfoTraceEvents.TraceMessageId);
      SMPerformanceInfo smPerformanceInfo2 = pxResultset[0].GetItem<SMPerformanceInfo>();
      SMPerformanceInfoStackTrace performanceInfoStackTrace = pxResultset[0].GetItem<SMPerformanceInfoStackTrace>();
      SMPerformanceInfoExceptionSummary exceptionSummary = new SMPerformanceInfoExceptionSummary()
      {
        TraceMessageId = performanceInfoTraceEvents.TraceMessageId,
        Tenant = smPerformanceInfo1.TenantId,
        ExceptionMessage = infoTraceMessages.MessageText,
        ExceptionType = performanceInfoTraceEvents.ExceptionType,
        LastCommandName = smPerformanceInfo2.CommandName,
        LastCommandTarget = smPerformanceInfo2.CommandTarget,
        LastOccured = smPerformanceInfo2.RequestStartTime,
        LastStackTrace = performanceInfoStackTrace.StackTrace,
        LastUrl = smPerformanceInfo2.InternalScreenId,
        Count = nullable
      };
      if (this.TraceExceptionsSummary.Cache.Locate((object) exceptionSummary) == null)
      {
        exceptionSummary = (SMPerformanceInfoExceptionSummary) this.TraceExceptionsSummary.Cache.Insert((object) exceptionSummary);
        this.TraceExceptionsSummary.Cache.SetStatus((object) exceptionSummary, PXEntryStatus.Notchanged);
      }
      exceptionSummaryList.Add(exceptionSummary);
    }
    this.TraceExceptionsSummary.Cache.IsDirty = false;
    return (IEnumerable) exceptionSummaryList;
  }

  [PXButton]
  [PXUIField(DisplayName = "Start")]
  protected void actionStartMonitor()
  {
    PXPerformanceMonitor.ExpirationTime = System.DateTime.Now.AddDays(1.0);
    PXPerformanceMonitor.IsEnabled = true;
  }

  [PXButton]
  [PXUIField(DisplayName = "Stop")]
  protected void actionStopMonitor()
  {
    PXPerformanceMonitor.IsEnabled = false;
    this.actionFlushSamples();
    ++PXPerformanceMonitor.FlushVersion;
  }

  [PXButton]
  [PXUIField(DisplayName = "Refresh Results")]
  protected void actionFlushSamples()
  {
    this.SaveSamples(PXPerformanceMonitor.RemoveSamples());
    PXPerformanceMonitor.TraceItems = new ConcurrentQueue<PXProfilerTraceItem>();
    ++PXPerformanceMonitor.FlushVersion;
    this.Caches[typeof (SMPerformanceInfoSQL)].ClearQueryCache();
    this.Caches[typeof (SMPerformanceInfoTraceEvents)].ClearQueryCache();
    this.Clear();
  }

  [PXButton(Tooltip = "Remove requests that are not pinned and all related information")]
  [PXUIField(DisplayName = "Clear Log")]
  protected void actionClearSamples()
  {
    PerformanceMonitorMaint.sqlTextsIds = new HashSet<int>();
    PerformanceMonitorMaint.sqlTextsTables = new Dictionary<int, string>();
    PerformanceMonitorMaint.sqlTracesIds = new HashSet<int>();
    PerformanceMonitorMaint.traceMessagesIds = new HashSet<int>();
    bool flag = false;
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle<SMPerformanceInfo>(new PXDataField("RecordId"), (PXDataField) new PXDataFieldValue("IsChecked", PXDbType.Bit, new int?(1), (object) 1)))
    {
      if (pxDataRecord != null)
        flag = true;
    }
    if (!flag)
    {
      this.ClearLog(typeof (SMPerformanceInfoStackTrace));
      this.ClearLog(typeof (SMPerformanceInfoTraceMessages));
      this.ClearLog(typeof (SMPerformanceInfoTraceEvents));
      this.ClearLog(typeof (SMPerformanceInfoSQLText));
      this.ClearLog(typeof (SMPerformanceInfoSQL));
      this.ClearLog(typeof (SMPerformanceInfo));
    }
    else
    {
      switch (PXLongOperation.GetStatus((object) this))
      {
        case PXLongRunStatus.NotExists:
        case PXLongRunStatus.Aborted:
          PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() =>
          {
            List<CommandBase> commandBaseList = new List<CommandBase>();
            PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
            CmdDelete cmdDelete = new CmdDelete(YaqlSchemaTable.op_Implicit("SMPerformanceInfo"), (List<YaqlJoin>) null)
            {
              Condition = Yaql.or(Yaql.eq<int>((YaqlScalar) Yaql.column<SMPerformanceInfo.isChecked>((string) null), 0), Yaql.isNull((YaqlScalar) Yaql.column<SMPerformanceInfo.isChecked>((string) null)))
            };
            commandBaseList.Add((CommandBase) cmdDelete);
            commandBaseList.AddRange((IEnumerable<CommandBase>) PerformanceMonitorMaint.GetCleanupCommands());
            using (new PXCommandScope(PXDatabase.Provider.DefaultQueryTimeout * 20))
              dbServicesPoint.executeCommands((IEnumerable<CommandBase>) commandBaseList, new ExecutionContext((IExecutionObserver) null), false);
          }));
          break;
      }
    }
    PXPerformanceMonitor.TraceItems = new ConcurrentQueue<PXProfilerTraceItem>();
    this.Samples.Cache.ClearQueryCache();
    this.Caches[typeof (SMPerformanceInfoSQL)].ClearQueryCache();
    this.Caches[typeof (SMPerformanceInfoTraceEvents)].ClearQueryCache();
    this.Clear();
    this.EnsureProfilerDataSizeIsWithInTheLimitAndUpdateStateWithControls();
  }

  private static List<CommandBase> GetCleanupCommands()
  {
    List<CommandBase> cleanupCommands = new List<CommandBase>();
    int profilerCleanupLogSize = WebConfig.ProfilerCleanupLogSize;
    int cleanupRepeatCount = WebConfig.ProfilerCleanupRepeatCount;
    YaqlVectorQuery yaqlVectorQuery1 = new YaqlVectorQuery((YaqlTable) Yaql.schemaTable("SMPerformanceInfo", (string) null), (List<YaqlJoin>) null)
    {
      Column = YaqlScalarAlilased.op_Implicit(Yaql.column<SMPerformanceInfo.recordId>((string) null))
    };
    CmdRepeatableDelete repeatableDelete1 = new CmdRepeatableDelete(YaqlSchemaTable.op_Implicit("SMPerformanceInfoSQL"), (List<YaqlJoin>) null);
    ((CmdDelete) repeatableDelete1).Condition = Yaql.isNotIn((YaqlScalar) Yaql.column<SMPerformanceInfoSQL.parentId>((string) null), yaqlVectorQuery1);
    ((CmdDelete) repeatableDelete1).LimitRows = profilerCleanupLogSize;
    repeatableDelete1.RepeatCount = cleanupRepeatCount;
    cleanupCommands.Add((CommandBase) repeatableDelete1);
    YaqlVectorQuery yaqlVectorQuery2 = new YaqlVectorQuery((YaqlTable) Yaql.schemaTable("SMPerformanceInfoSQL", (string) null), (List<YaqlJoin>) null)
    {
      Column = YaqlScalarAlilased.op_Implicit(Yaql.column<SMPerformanceInfoSQL.sqlId>((string) null))
    };
    CmdRepeatableDelete repeatableDelete2 = new CmdRepeatableDelete(YaqlSchemaTable.op_Implicit("SMPerformanceInfoSQLText"), (List<YaqlJoin>) null);
    ((CmdDelete) repeatableDelete2).Condition = Yaql.isNotIn((YaqlScalar) Yaql.column<SMPerformanceInfoSQLText.recordId>((string) null), yaqlVectorQuery2);
    ((CmdDelete) repeatableDelete2).LimitRows = profilerCleanupLogSize;
    repeatableDelete2.RepeatCount = cleanupRepeatCount;
    cleanupCommands.Add((CommandBase) repeatableDelete2);
    YaqlVectorQuery yaqlVectorQuery3 = new YaqlVectorQuery((YaqlTable) Yaql.schemaTable("SMPerformanceInfo", (string) null), (List<YaqlJoin>) null)
    {
      Column = YaqlScalarAlilased.op_Implicit(Yaql.column<SMPerformanceInfo.recordId>((string) null))
    };
    CmdRepeatableDelete repeatableDelete3 = new CmdRepeatableDelete(YaqlSchemaTable.op_Implicit("SMPerformanceInfoTraceEvents"), (List<YaqlJoin>) null);
    ((CmdDelete) repeatableDelete3).Condition = Yaql.isNotIn((YaqlScalar) Yaql.column<SMPerformanceInfoTraceEvents.parentId>((string) null), yaqlVectorQuery3);
    ((CmdDelete) repeatableDelete3).LimitRows = profilerCleanupLogSize;
    repeatableDelete3.RepeatCount = cleanupRepeatCount;
    cleanupCommands.Add((CommandBase) repeatableDelete3);
    YaqlVectorQuery yaqlVectorQuery4 = new YaqlVectorQuery((YaqlTable) Yaql.schemaTable("SMPerformanceInfoTraceEvents", (string) null), (List<YaqlJoin>) null)
    {
      Column = YaqlScalarAlilased.op_Implicit(Yaql.column<SMPerformanceInfoTraceEvents.traceMessageId>((string) null))
    };
    CmdRepeatableDelete repeatableDelete4 = new CmdRepeatableDelete(YaqlSchemaTable.op_Implicit("SMPerformanceInfoTraceMessages"), (List<YaqlJoin>) null);
    ((CmdDelete) repeatableDelete4).Condition = Yaql.isNotIn((YaqlScalar) Yaql.column<SMPerformanceInfoTraceMessages.recordId>((string) null), yaqlVectorQuery4);
    ((CmdDelete) repeatableDelete4).LimitRows = profilerCleanupLogSize;
    repeatableDelete4.RepeatCount = cleanupRepeatCount;
    cleanupCommands.Add((CommandBase) repeatableDelete4);
    YaqlVectorQuery yaqlVectorQuery5 = new YaqlVectorQuery((YaqlTable) Yaql.schemaTable("SMPerformanceInfoSQL", (string) null), (List<YaqlJoin>) null)
    {
      Column = YaqlScalarAlilased.op_Implicit(Yaql.column<SMPerformanceInfoSQL.stackTraceId>((string) null))
    };
    YaqlVectorQuery yaqlVectorQuery6 = new YaqlVectorQuery((YaqlTable) Yaql.schemaTable("SMPerformanceInfoTraceEvents", (string) null), (List<YaqlJoin>) null)
    {
      Column = YaqlScalarAlilased.op_Implicit(Yaql.column<SMPerformanceInfoTraceEvents.stackTraceId>((string) null))
    };
    CmdRepeatableDelete repeatableDelete5 = new CmdRepeatableDelete(YaqlSchemaTable.op_Implicit("SMPerformanceInfoStackTrace"), (List<YaqlJoin>) null);
    ((CmdDelete) repeatableDelete5).Condition = Yaql.not(Yaql.or(Yaql.isIn((YaqlScalar) Yaql.column<SMPerformanceInfoStackTrace.recordId>((string) null), yaqlVectorQuery5), Yaql.isIn((YaqlScalar) Yaql.column<SMPerformanceInfoStackTrace.recordId>((string) null), yaqlVectorQuery6)));
    ((CmdDelete) repeatableDelete5).LimitRows = profilerCleanupLogSize;
    repeatableDelete5.RepeatCount = cleanupRepeatCount;
    cleanupCommands.Add((CommandBase) repeatableDelete5);
    return cleanupCommands;
  }

  private void CleanupLogs()
  {
    SMPerformanceInfo smPerformanceInfo1 = (SMPerformanceInfo) PXSelectBase<SMPerformanceInfo, PXSelectReadonly3<SMPerformanceInfo, OrderBy<Desc<SMPerformanceInfo.recordId>>>.Config>.SelectWindowed((PXGraph) this, WebConfig.ProfilerCleanupLogSize, 1);
    SMPerformanceInfo smPerformanceInfo2 = (SMPerformanceInfo) PXSelectBase<SMPerformanceInfo, PXSelectReadonly<SMPerformanceInfo, Where<SMPerformanceInfo.requestStartTime, Less<Required<SMPerformanceInfo.requestStartTime>>>, OrderBy<Desc<SMPerformanceInfo.recordId>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) System.DateTime.UtcNow.AddDays((double) (-1 * WebConfig.ProfilerCleanupLogDays)));
    SMPerformanceInfo smPerformanceInfo3 = smPerformanceInfo1;
    if (smPerformanceInfo1 == null)
      smPerformanceInfo3 = smPerformanceInfo2;
    else if (smPerformanceInfo2 != null)
    {
      int? recordId1 = smPerformanceInfo2.RecordId;
      int? recordId2 = smPerformanceInfo3.RecordId;
      if (recordId1.GetValueOrDefault() > recordId2.GetValueOrDefault() & recordId1.HasValue & recordId2.HasValue)
        smPerformanceInfo3 = smPerformanceInfo2;
    }
    if (smPerformanceInfo3 == null || !smPerformanceInfo3.RequestStartTime.HasValue)
      return;
    PerformanceMonitorMaint.sqlTextsIds = new HashSet<int>();
    PerformanceMonitorMaint.sqlTextsTables = new Dictionary<int, string>();
    PerformanceMonitorMaint.sqlTracesIds = new HashSet<int>();
    PerformanceMonitorMaint.traceMessagesIds = new HashSet<int>();
    List<CommandBase> commandBaseList = new List<CommandBase>();
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    CmdDelete cmdDelete = new CmdDelete(YaqlSchemaTable.op_Implicit("SMPerformanceInfo"), (List<YaqlJoin>) null)
    {
      Condition = Yaql.and(Yaql.lt<int?>((YaqlScalar) Yaql.column<SMPerformanceInfo.recordId>((string) null), smPerformanceInfo3.RecordId), Yaql.or(Yaql.eq<int>((YaqlScalar) Yaql.column<SMPerformanceInfo.isChecked>((string) null), 0), Yaql.isNull((YaqlScalar) Yaql.column<SMPerformanceInfo.isChecked>((string) null))))
    };
    commandBaseList.Add((CommandBase) cmdDelete);
    commandBaseList.AddRange((IEnumerable<CommandBase>) PerformanceMonitorMaint.GetCleanupCommands());
    using (new PXCommandScope(PXDatabase.Provider.DefaultQueryTimeout * 20))
      dbServicesPoint.executeCommands((IEnumerable<CommandBase>) commandBaseList, new ExecutionContext((IExecutionObserver) null), false);
  }

  [PXButton]
  [PXUIField(Visible = false)]
  protected void actionOpenUrl()
  {
    SMPerformanceInfo current = this.Samples.Current;
    if (current != null)
      throw new PXRedirectToUrlException(current.ScreenId, PXBaseRedirectException.WindowMode.New, "");
  }

  [PXButton]
  [PXUIField(DisplayName = "Open Screen")]
  protected void actionViewScreen()
  {
    SMPerformanceInfo current = this.Samples.Current;
    if (current != null && !string.IsNullOrEmpty(current.InternalScreenId))
    {
      string url = "~/Main?ScreenId=" + current.InternalScreenId;
      if (!string.IsNullOrEmpty(current.TenantId))
        url = $"{url}&CompanyID={current.TenantId}";
      throw new PXRedirectToUrlException(url, PXBaseRedirectException.WindowMode.New, true, "");
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "Select")]
  protected void actionSelectRows()
  {
    ((PerformanceMonitorMaint.SMPerformanceFilterRow) this.Filter.Cache.Current).SelectRows = new bool?(true);
    this.Samples.Cache.AllowUpdate = true;
    this.Samples.AllowUpdate = true;
    PXUIFieldAttribute.SetEnabled<SMPerformanceInfo.isChecked>(this.Samples.Cache, (object) null, true);
    this.ActionSaveRows.SetVisible(true);
    this.ActionSelectRows.SetVisible(false);
  }

  [PXButton]
  [PXUIField(DisplayName = "Pin/Unpin")]
  protected void actionPinRows()
  {
    SMPerformanceInfo current = this.Samples.Current;
    if (current == null)
      return;
    current.IsChecked = new bool?(!current.IsChecked.GetValueOrDefault());
    this.Samples.Cache.MarkUpdated((object) current);
    this.Samples.Cache.Persist(PXDBOperation.Update);
  }

  [PXButton]
  [PXUIField(DisplayName = "Save", Visible = false)]
  protected void actionSaveRows()
  {
    ((PerformanceMonitorMaint.SMPerformanceFilterRow) this.Filter.Cache.Current).SelectRows = new bool?(false);
    this.Samples.Cache.Persist(PXDBOperation.Update);
    this.Samples.Cache.AllowUpdate = false;
    this.Samples.AllowUpdate = false;
    PXUIFieldAttribute.SetEnabled<SMPerformanceInfo.isChecked>(this.Samples.Cache, (object) null, false);
    this.ActionSelectRows.SetVisible(true);
    this.ActionSaveRows.SetVisible(false);
  }

  [PXButton]
  [PXUIField(DisplayName = "View Sql")]
  protected virtual IEnumerable actionViewSql(PXAdapter adapter)
  {
    if (this.Samples.Current != null)
    {
      int num = (int) this.Sql.AskExt();
    }
    return adapter.Get();
  }

  [PXButton]
  protected virtual IEnumerable actionViewExceptions(PXAdapter adapter)
  {
    if (this.Samples.Current != null)
    {
      int num = (int) this.TraceExceptions.AskExt();
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "View Exception Details")]
  protected virtual IEnumerable actionViewExceptionDetails(PXAdapter adapter)
  {
    if (this.Samples.Current != null)
    {
      int num = (int) this.TraceExceptionDetails.AskExt();
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "View Event Log")]
  protected virtual IEnumerable actionViewTrace(PXAdapter adapter)
  {
    if (this.Samples.Current != null)
    {
      int num = (int) this.TraceEvents.AskExt();
    }
    return adapter.Get();
  }

  [PXButton]
  protected virtual IEnumerable actionViewSqlSummaryRows(PXAdapter adapter)
  {
    if (this.SqlSummary.Current != null)
    {
      int num = (int) this.SqlSummary.AskExt();
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "View Event Details")]
  protected virtual IEnumerable actionViewEventDetails(PXAdapter adapter)
  {
    this.TraceEventsLogDetails.Current = this.TraceEventsLog.Current;
    if (this.TraceEventsLog.Current != null)
    {
      int num = (int) this.TraceEventsLog.AskExt();
    }
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Export")]
  protected void actionDownloadJson()
  {
    throw new PXRedirectToUrlException("~/apiweb/telemetry/profilerData", "")
    {
      TargetFrame = "Profiler"
    };
  }

  [PXButton]
  [PXUIField(DisplayName = "Import")]
  protected IEnumerable actionImportLogs(PXAdapter adapter)
  {
    if (this.UploadDialogPanel.AskExt() == WebDialogResult.OK)
      PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() => this.ImportLogs((PXContext.SessionTyped<PXSessionStatePXData>().FileInfo.Pop<FileInfo>("UploadedPerformanceLogsKey") ?? throw new PXException("The file is not found, or you don't have enough rights to see the file.")).BinData)));
    return adapter.Get();
  }

  protected void _(
    Events.RowSelected<PerformanceMonitorMaint.SMPerformanceFilterRow> e)
  {
    if (e.Row == null)
      return;
    this.ActionStopMonitor.SetEnabled(PXPerformanceMonitor.IsEnabled);
    this.ActionStartMonitor.SetEnabled(!PXPerformanceMonitor.IsEnabled);
    this.ActionSelectRows.SetVisible(false);
    if (PXPerformanceMonitor.TraceEnabled && !string.IsNullOrEmpty(PXPerformanceMonitor.LogLevelFilter) && PXPerformanceMonitor.LogLevels[PXPerformanceMonitor.LogLevelFilter] > PXPerformanceMonitor.LogLevels["Warning"] && string.IsNullOrEmpty(PXPerformanceMonitor.LogCategoryFilter))
      PXUIFieldAttribute.SetError(this.Filter.Cache, (object) e.Row, typeof (PerformanceMonitorMaint.SMPerformanceFilterRow.logCategoryFilter).Name, "You need to select at least one category below to be able to turn on logs with a level less than Warning.", (string) null, true, PXErrorLevel.Warning);
    else
      PXUIFieldAttribute.SetError(this.Filter.Cache, (object) e.Row, typeof (PerformanceMonitorMaint.SMPerformanceFilterRow.logCategoryFilter).Name, (string) null, (string) null, true, PXErrorLevel.Undefined);
    int num = PXPerformanceMonitor.SaveRequestsToDb ? 1 : 0;
    bool? nullable = new bool?(PXPerformanceMonitor.SaveSqlToDb);
    bool flag = true;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      PXUIFieldAttribute.SetError(this.Filter.Cache, (object) e.Row, typeof (PerformanceMonitorMaint.SMPerformanceFilterRow.sqlProfiler).Name, "Note that logging SQL requests can cause performance degradation; therefore, you should turn on SQL logging only for troubleshooting purposes.", (string) null, true, PXErrorLevel.Warning);
    else
      PXUIFieldAttribute.SetError(this.Filter.Cache, (object) e.Row, typeof (PerformanceMonitorMaint.SMPerformanceFilterRow.sqlProfiler).Name, (string) null, (string) null, true, PXErrorLevel.Undefined);
    if (PXPerformanceMonitor.TraceEnabled && !string.IsNullOrEmpty(e.Row.LogLevelFilter))
      PXUIFieldAttribute.SetError(this.Filter.Cache, (object) e.Row, typeof (PerformanceMonitorMaint.SMPerformanceFilterRow.traceEnabled).Name, "Note that logging events can cause performance degradation; therefore, you should turn on event logging only for troubleshooting purposes.", (string) null, true, PXErrorLevel.Warning);
    else
      PXUIFieldAttribute.SetError(this.Filter.Cache, (object) e.Row, typeof (PerformanceMonitorMaint.SMPerformanceFilterRow.traceEnabled).Name, (string) null, (string) null, true, PXErrorLevel.Undefined);
    this.UpdateProfilerControlsState(e.Row, !PXPerformanceMonitor.IsProfilerDataSizeOverLimits);
  }

  public void SMPerformanceFilterRow_ProfilerEnabled_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is PerformanceMonitorMaint.SMPerformanceFilterRow row) || e.NewValue == null)
      return;
    bool? profilerEnabled = row.ProfilerEnabled;
    bool flag = true;
    if (!(profilerEnabled.GetValueOrDefault() == flag & profilerEnabled.HasValue) || (bool) e.NewValue)
      return;
    PXPerformanceMonitor._UserProfilerName = (string) null;
    PXPerformanceMonitor._UserProfilerDate = System.DateTime.Now;
    row.SqlProfiler = new bool?(false);
    object newValue = (object) false;
    this.Caches[typeof (PerformanceMonitorMaint.SMPerformanceFilterRow)].RaiseFieldUpdating<PerformanceMonitorMaint.SMPerformanceFilterRow.sqlProfiler>((object) row, ref newValue);
    row.TraceExceptionsEnabled = new bool?(false);
    row.TraceEnabled = new bool?(false);
    PXContext.SetSlot<bool>("PerformanceMonitorSkipLogLevelFilter", true);
    e.Cancel = true;
  }

  public void SMPerformanceFilterRow_SqlProfiler_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (sender.Current != e.Row || !(e.Row is PerformanceMonitorMaint.SMPerformanceFilterRow row) || e.NewValue == null || !(bool) e.NewValue || PXPerformanceMonitor.SaveSqlToDb)
      return;
    row.ProfilerEnabled = new bool?(true);
    object newValue = (object) true;
    this.Caches[typeof (PerformanceMonitorMaint.SMPerformanceFilterRow)].RaiseFieldUpdating("ProfilerEnabled", (object) row, ref newValue);
    PXContext.SetSlot<bool>("PerformanceMonitorSkipLogLevelFilter", true);
    e.Cancel = true;
  }

  public void SMPerformanceFilterRow_TraceEnabled_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is PerformanceMonitorMaint.SMPerformanceFilterRow row) || e.NewValue == null)
      return;
    bool? traceEnabled = row.TraceEnabled;
    bool flag = false;
    if (!(traceEnabled.GetValueOrDefault() == flag & traceEnabled.HasValue) || !(bool) e.NewValue)
      return;
    row.ProfilerEnabled = new bool?(true);
    object newValue = (object) true;
    this.Caches[typeof (PerformanceMonitorMaint.SMPerformanceFilterRow)].RaiseFieldUpdating("ProfilerEnabled", (object) row, ref newValue);
    PXContext.SetSlot<bool>("PerformanceMonitorSkipLogLevelFilter", true);
    e.Cancel = true;
  }

  public void SMPerformanceFilterRow_TraceExceptionsEnabled_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is PerformanceMonitorMaint.SMPerformanceFilterRow row) || e.NewValue == null)
      return;
    bool? exceptionsEnabled = row.TraceExceptionsEnabled;
    bool flag = false;
    if (!(exceptionsEnabled.GetValueOrDefault() == flag & exceptionsEnabled.HasValue) || !(bool) e.NewValue)
      return;
    row.ProfilerEnabled = new bool?(true);
    object newValue = (object) true;
    this.Caches[typeof (PerformanceMonitorMaint.SMPerformanceFilterRow)].RaiseFieldUpdating("ProfilerEnabled", (object) row, ref newValue);
    PXContext.SetSlot<bool>("PerformanceMonitorSkipLogLevelFilter", true);
    e.Cancel = true;
  }

  public void SMPerformanceInfo_IsChecked_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SMPerformanceInfo row))
      return;
    this.Samples.Cache.Persist((object) row, PXDBOperation.Update);
  }

  public void SMPerformanceInfo_IsPinned_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is SMPerformanceInfo row))
      return;
    string str1 = PerformanceMonitorMaint.Unpinned;
    bool? nullable = row.IsChecked;
    bool flag = true;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      str1 = PerformanceMonitorMaint.Pinned;
    PXFieldSelectingEventArgs selectingEventArgs = e;
    string str2 = str1;
    int? length = new int?(-1);
    nullable = new bool?();
    bool? isUnicode = nullable;
    bool? isKey = new bool?(false);
    int? required = new int?();
    nullable = new bool?();
    bool? exclusiveValues = nullable;
    string headerImage = str1;
    PXFieldState instance = PXImageState.CreateInstance((object) str2, length, isUnicode, "IsPinned", isKey, required, (string) null, (string[]) null, (string[]) null, exclusiveValues, (string) null, headerImage);
    selectingEventArgs.ReturnState = (object) instance;
    e.ReturnValue = (object) str1;
  }

  public void SMPerformanceInfoSQLWithTables_SQLWithStackTrace_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    if (!(e.Row is SMPerformanceInfoSQLWithTables row))
      return;
    string str = row.SQLWithParams;
    if (PXPerformanceMonitor.FormatSQLEnabled)
      str = SqlFormatter.FormatSQL(str, true);
    if (!PXPerformanceMonitor.SqlProfilerShowStackTrace || string.IsNullOrEmpty(row.StackTrace))
      e.ReturnValue = (object) str;
    else
      e.ReturnValue = (object) $"{str}\n\n/* Stack Trace:\n\n{row.StackTrace} */";
  }

  private void ClearLog(System.Type table)
  {
    using (new PXCommandScope(PXDatabase.Provider.DefaultQueryTimeout * 20))
      PXDatabase.Provider.Truncate(table);
  }

  protected virtual string GetWebsiteHeaders()
  {
    if (PerformanceMonitorMaint.Headers == null)
    {
      string path = HostingEnvironment.MapPath("~/App_Data/WebsiteHeaders.txt");
      if (File.Exists(path))
        PerformanceMonitorMaint.Headers = File.ReadAllText(path);
      if (string.IsNullOrEmpty(PerformanceMonitorMaint.Headers))
        PerformanceMonitorMaint.Headers = WebConfig.GetString("ClusterNodeId", string.Empty);
      if (string.IsNullOrEmpty(PerformanceMonitorMaint.Headers))
        PerformanceMonitorMaint.Headers = WebsiteID.Key;
      if (PerformanceMonitorMaint.Headers == null)
        PerformanceMonitorMaint.Headers = "";
    }
    return PerformanceMonitorMaint.Headers == "" ? (string) null : PerformanceMonitorMaint.Headers;
  }

  private static IEnumerable<List<object[]>> SplitRows(List<object[]> data)
  {
    int maxRecordsCount = 900;
    List<object[]> objArrayList = new List<object[]>(maxRecordsCount);
    foreach (object[] objArray in data)
    {
      objArrayList.Add(objArray);
      if (objArrayList.Count >= maxRecordsCount)
      {
        yield return objArrayList;
        objArrayList = new List<object[]>();
      }
    }
    if (objArrayList.Count > 0)
      yield return objArrayList;
  }

  private void BulkInsert<TTable>(PointDbmsBase point, List<object[]> data, bool ignoreDups = true)
  {
    if (data == null || data.Count == 0)
      return;
    DbmsTableAdapter table = point.GetTable(typeof (TTable).Name, FileMode.Open) as DbmsTableAdapter;
    table.UseNativeImport = false;
    foreach (List<object[]> splitRow in PerformanceMonitorMaint.SplitRows(data))
      table.WriteRows((IEnumerable<object[]>) splitRow, false, (System.Action<int>) null, ignoreDups, true);
  }

  private static bool CheckExistingRecords(int newRecord, HashSet<int> existingRecords)
  {
    if (existingRecords.Contains(newRecord))
      return false;
    existingRecords.Add(newRecord);
    return true;
  }

  internal static SMPerformanceInfo ConvertSample(PXPerformanceInfo sample)
  {
    double totalMilliseconds1 = sample.ThreadTime.TotalMilliseconds;
    double totalMilliseconds2 = sample.Timer.Elapsed.TotalMilliseconds;
    double totalMilliseconds3 = sample.SqlTimer.Elapsed.TotalMilliseconds;
    SMPerformanceInfo smPerformanceInfo = new SMPerformanceInfo()
    {
      CommandName = sample.CommandName,
      CommandTarget = sample.CommandTarget,
      RequestStartTime = new System.DateTime?(sample.StartTimeLocal),
      RequestTimeMs = new double?(totalMilliseconds2),
      ScreenId = sample.ScreenId,
      InternalScreenId = sample.InternalScreenId,
      SqlCounter = new int?(sample.SqlCounter),
      SqlRows = new int?(sample.SqlRows),
      RequestType = sample.RequestType,
      ExceptionCounter = new int?(sample.ExceptionCounter),
      EventCounter = new int?(sample.TraceCounter),
      SqlTimeMs = new double?(totalMilliseconds3),
      SelectCounter = new int?(sample.SelectCount),
      SelectTimeMs = new double?(sample.SelectTimer.Elapsed.TotalMilliseconds),
      UserId = sample.UserId,
      TenantId = sample.TenantId,
      RequestCpuTimeMs = new double?(totalMilliseconds1),
      MemBefore = new long?(sample.MemBefore),
      MemDelta = new long?(sample.MemDelta),
      MemBeforeMb = new double?((double) sample.MemBefore / 1000000.0),
      MemDeltaMb = new double?((double) sample.MemDelta / 1000000.0),
      MemoryWorkingSet = new double?(sample.MemWorkingSet),
      ScriptTimeMs = sample.ScriptTimeMs,
      InstallationId = PXLicenseHelper.InstallationID,
      SessionLoadTimeMs = new double?(sample.SessionLoadTimer.Elapsed.TotalMilliseconds),
      SessionSaveTimeMs = new double?(sample.SessionSaveTimer.Elapsed.TotalMilliseconds),
      WaitTime = new double?(totalMilliseconds2 > totalMilliseconds1 + totalMilliseconds3 ? totalMilliseconds2 - totalMilliseconds1 - totalMilliseconds3 : 0.0),
      LoggedSqlCounter = new int?(0),
      LoggedEventCounter = new int?(0),
      LoggedExceptionCounter = new int?(0)
    };
    if (sample.ProcessingCounter > 0)
      smPerformanceInfo.ProcessingItems = new int?(sample.ProcessingCounter);
    return smPerformanceInfo;
  }

  internal static SMPerformanceInfoSQLWithTables ConvertSqlSample(PXProfilerSqlSample sqlSample)
  {
    SMPerformanceInfoSQLWithTables infoSqlWithTables = new SMPerformanceInfoSQLWithTables();
    infoSqlWithTables.NRows = sqlSample.RowCount;
    infoSqlWithTables.QueryCache = new bool?(sqlSample.QueryCache);
    infoSqlWithTables.RequestDateTime = sqlSample.SqlSampleDateTime;
    infoSqlWithTables.RequestStartTime = sqlSample.StartTime;
    infoSqlWithTables.SqlId = new int?(sqlSample.SqlTextId);
    infoSqlWithTables.SQLParams = sqlSample.Params;
    infoSqlWithTables.SQLText = sqlSample.Text;
    infoSqlWithTables.SqlTimeMs = new double?(sqlSample.SqlTimer.Elapsed.TotalMilliseconds);
    infoSqlWithTables.StackTrace = sqlSample.StackTrace;
    infoSqlWithTables.StackTraceId = new int?(sqlSample.TraceTextId);
    infoSqlWithTables.TableList = sqlSample.Tables;
    return infoSqlWithTables;
  }

  internal static SMPerformanceInfoTraceWithMessages ConvertTraceitem(PXProfilerTraceItem traceItem)
  {
    SMPerformanceInfoTraceWithMessages traceWithMessages = new SMPerformanceInfoTraceWithMessages();
    traceWithMessages.RequestStartTime = traceItem.StartTime;
    traceWithMessages.Source = traceItem.Source;
    traceWithMessages.TraceType = traceItem.EventType;
    traceWithMessages.MessageText = traceItem.Message;
    traceWithMessages.StackTrace = traceItem.StackTrace;
    traceWithMessages.ExceptionType = traceItem.ExceptionType;
    traceWithMessages.EventDateTime = new System.DateTime?(traceItem.EventDateTime);
    traceWithMessages.EventDetails = traceItem.EventDetails;
    traceWithMessages.StackTraceId = traceItem.TraceId;
    traceWithMessages.TraceMessageId = new int?(traceItem.MessageId);
    return traceWithMessages;
  }

  public virtual void SaveSamples(IEnumerable<PXPerformanceInfo> saved)
  {
    if (!saved.Any<PXPerformanceInfo>())
    {
      PXPerformanceMonitor.TraceItems = new ConcurrentQueue<PXProfilerTraceItem>();
    }
    else
    {
      List<object[]> data1 = new List<object[]>();
      List<object[]> data2 = new List<object[]>();
      List<object[]> data3 = new List<object[]>();
      List<object[]> data4 = new List<object[]>();
      List<object[]> data5 = new List<object[]>();
      Dictionary<PXPerformanceInfo, SMPerformanceInfo> source = new Dictionary<PXPerformanceInfo, SMPerformanceInfo>();
      foreach (PXPerformanceInfo pxPerformanceInfo in saved)
      {
        if (PXPerformanceMonitor.FilterRequestItem(pxPerformanceInfo))
        {
          SMPerformanceInfo smPerformanceInfo = this.Samples.Insert(PerformanceMonitorMaint.ConvertSample(pxPerformanceInfo));
          smPerformanceInfo.Headers = this.GetWebsiteHeaders();
          source.Add(pxPerformanceInfo, smPerformanceInfo);
        }
      }
      if (!source.Any<KeyValuePair<PXPerformanceInfo, SMPerformanceInfo>>())
      {
        PXPerformanceMonitor.TraceItems = new ConcurrentQueue<PXProfilerTraceItem>();
      }
      else
      {
        foreach (KeyValuePair<PXPerformanceInfo, SMPerformanceInfo> keyValuePair in source)
        {
          if (keyValuePair.Key.SqlSamples != null || keyValuePair.Key.QueryCacheSamples != null)
          {
            foreach (PXProfilerSqlSample sqlSample in keyValuePair.Key.SqlSamples.Concat<PXProfilerSqlSample>((IEnumerable<PXProfilerSqlSample>) keyValuePair.Key.QueryCacheSamples))
            {
              if (PXPerformanceMonitor.FilterSqlItem(keyValuePair.Key, sqlSample))
              {
                if (!sqlSample.QueryCache)
                {
                  SMPerformanceInfo smPerformanceInfo = keyValuePair.Value;
                  int? loggedSqlCounter = smPerformanceInfo.LoggedSqlCounter;
                  smPerformanceInfo.LoggedSqlCounter = loggedSqlCounter.HasValue ? new int?(loggedSqlCounter.GetValueOrDefault() + 1) : new int?();
                }
                int sqlTextId = sqlSample.SqlTextId;
                string str1;
                if (PerformanceMonitorMaint.CheckExistingRecords(sqlTextId, PerformanceMonitorMaint.sqlTextsIds))
                {
                  string text = sqlSample.Text;
                  str1 = ((IEnumerable<string>) PXDatabase.ExtractTablesFromSqlWithCase(sqlSample.Text)).JoinToString<string>(",");
                  PerformanceMonitorMaint.sqlTextsTables.Add(sqlTextId, str1);
                  if (Str.IsNullOrEmpty(str1))
                  {
                    if (text.StartsWith("SELECT "))
                      str1 = text.Substring(7);
                    else if (text.StartsWith("INSERT ") || text.StartsWith("UPDATE "))
                    {
                      Match match = PerformanceMonitorMaint.InsertOrUpdatedStatement.Match(text);
                      if (match.Success)
                        str1 = match.ToString();
                    }
                  }
                  string str2 = sqlSample.BqlHash ?? PXReflectionSerializer.GetStableHash(text).ToString("X");
                  data1.Add(new object[5]
                  {
                    (object) sqlTextId,
                    (object) text,
                    (object) str2,
                    (object) str1,
                    (object) 0
                  });
                }
                else
                  str1 = PerformanceMonitorMaint.sqlTextsTables[sqlTextId];
                sqlSample.Tables = str1;
                int? nullable = new int?(sqlSample.TraceTextId);
                if (nullable.HasValue && PerformanceMonitorMaint.CheckExistingRecords(nullable.Value, PerformanceMonitorMaint.sqlTracesIds))
                  data2.Add(new object[2]
                  {
                    (object) nullable.Value,
                    (object) sqlSample.StackTrace
                  });
              }
            }
            try
            {
              StringBuilder stringBuilder = new StringBuilder();
              SMPerformanceInfo smPerformanceInfo = keyValuePair.Value;
              Dictionary<string, PerformanceMonitorMaint.SqlDigestRow> dictionary = new Dictionary<string, PerformanceMonitorMaint.SqlDigestRow>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
              foreach (PXProfilerSqlSample sqlSample in keyValuePair.Key.SqlSamples)
              {
                string tables = sqlSample.Tables;
                string str = "UNK";
                if (sqlSample.Text.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                  str = "SELECT";
                if (sqlSample.Text.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase))
                  str = "UPDATE";
                if (sqlSample.Text.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
                  str = "INSERT";
                if (sqlSample.Text.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase))
                  str = "DELETE";
                string key = $"{str} {tables}";
                PerformanceMonitorMaint.SqlDigestRow sqlDigestRow;
                if (!dictionary.TryGetValue(key, out sqlDigestRow))
                {
                  sqlDigestRow = new PerformanceMonitorMaint.SqlDigestRow();
                  dictionary.Add(key, sqlDigestRow);
                }
                ++sqlDigestRow.Count;
                sqlDigestRow.Rows += sqlSample.RowCount.GetValueOrDefault();
                sqlDigestRow.SqlTime += sqlSample.SqlTimer.ElapsedMilliseconds;
                sqlDigestRow.Key = key;
                sqlDigestRow.BqlHashViewName = sqlSample.BqlHashViewName;
              }
              double? sqlTimeMs = smPerformanceInfo.SqlTimeMs;
              double num1 = 10.0;
              double? nullable1 = sqlTimeMs.HasValue ? new double?(sqlTimeMs.GetValueOrDefault() / num1) : new double?();
              double num2 = 1.0;
              double? ms = nullable1.HasValue ? new double?(nullable1.GetValueOrDefault() + num2) : new double?();
              int? nullable2 = smPerformanceInfo.SqlRows;
              nullable1 = nullable2.HasValue ? new double?((double) nullable2.GetValueOrDefault() / 10.0) : new double?();
              double num3 = 1.0;
              double? rows = nullable1.HasValue ? new double?(nullable1.GetValueOrDefault() + num3) : new double?();
              nullable2 = smPerformanceInfo.SqlCounter;
              nullable1 = nullable2.HasValue ? new double?((double) nullable2.GetValueOrDefault() / 10.0) : new double?();
              double num4 = 1.0;
              double? cnt = nullable1.HasValue ? new double?(nullable1.GetValueOrDefault() + num4) : new double?();
              foreach (PerformanceMonitorMaint.SqlDigestRow sqlDigestRow in dictionary.Values.Where<PerformanceMonitorMaint.SqlDigestRow>((Func<PerformanceMonitorMaint.SqlDigestRow, bool>) (_ =>
              {
                double rows1 = (double) _.Rows;
                double? nullable3 = rows;
                double valueOrDefault1 = nullable3.GetValueOrDefault();
                if (!(rows1 > valueOrDefault1 & nullable3.HasValue))
                {
                  double count = (double) _.Count;
                  double? nullable4 = cnt;
                  double valueOrDefault2 = nullable4.GetValueOrDefault();
                  if (!(count > valueOrDefault2 & nullable4.HasValue))
                  {
                    double sqlTime = (double) _.SqlTime;
                    double? nullable5 = ms;
                    double valueOrDefault3 = nullable5.GetValueOrDefault();
                    return sqlTime > valueOrDefault3 & nullable5.HasValue;
                  }
                }
                return true;
              })).OrderByDescending<PerformanceMonitorMaint.SqlDigestRow, long>((Func<PerformanceMonitorMaint.SqlDigestRow, long>) (_ => _.SqlTime)).ToList<PerformanceMonitorMaint.SqlDigestRow>())
              {
                string str = (string) null;
                if (sqlDigestRow.BqlHashViewName != null)
                  str = ", ViewName: " + sqlDigestRow.BqlHashViewName;
                stringBuilder.AppendLine($"{sqlDigestRow.SqlTime} ms, {sqlDigestRow.Count}#, {sqlDigestRow.Rows} rows, key: {sqlDigestRow.Key}{str}");
              }
              smPerformanceInfo.SqlDigest = StringExtensions.Truncate(stringBuilder.ToString(), 1024 /*0x0400*/);
            }
            catch
            {
            }
          }
          if (keyValuePair.Key.TraceItems != null)
          {
            foreach (PXProfilerTraceItem traceItem in keyValuePair.Key.TraceItems)
            {
              if (PXPerformanceMonitor.FilterTraceItem(traceItem.Source, traceItem.EventType, traceItem.Category, keyValuePair.Key))
              {
                if (traceItem.EventType.Equals("FirstChanceException"))
                {
                  SMPerformanceInfo smPerformanceInfo = keyValuePair.Value;
                  int? exceptionCounter = smPerformanceInfo.LoggedExceptionCounter;
                  smPerformanceInfo.LoggedExceptionCounter = exceptionCounter.HasValue ? new int?(exceptionCounter.GetValueOrDefault() + 1) : new int?();
                }
                else
                {
                  SMPerformanceInfo smPerformanceInfo = keyValuePair.Value;
                  int? loggedEventCounter = smPerformanceInfo.LoggedEventCounter;
                  smPerformanceInfo.LoggedEventCounter = loggedEventCounter.HasValue ? new int?(loggedEventCounter.GetValueOrDefault() + 1) : new int?();
                }
              }
              if (PerformanceMonitorMaint.CheckExistingRecords(traceItem.MessageId, PerformanceMonitorMaint.traceMessagesIds))
                data3.Add(new object[2]
                {
                  (object) traceItem.MessageId,
                  (object) traceItem.Message
                });
              if (traceItem.TraceId.HasValue && PerformanceMonitorMaint.CheckExistingRecords(traceItem.TraceId.Value, PerformanceMonitorMaint.sqlTracesIds))
                data2.Add(new object[2]
                {
                  (object) traceItem.TraceId.Value,
                  (object) traceItem.StackTrace
                });
            }
          }
        }
        using (new PXConnectionStringScope(WebConfig.ProfilerConnectionString))
          this.Actions.PressSave();
        foreach (KeyValuePair<PXPerformanceInfo, SMPerformanceInfo> keyValuePair in source)
        {
          if (keyValuePair.Key.SqlSamples != null || keyValuePair.Key.QueryCacheSamples != null)
          {
            foreach (PXProfilerSqlSample sqlSample in keyValuePair.Key.SqlSamples.Concat<PXProfilerSqlSample>((IEnumerable<PXProfilerSqlSample>) keyValuePair.Key.QueryCacheSamples))
            {
              if (PXPerformanceMonitor.FilterSqlItem(keyValuePair.Key, sqlSample))
                data4.Add(new object[10]
                {
                  (object) keyValuePair.Value.RecordId,
                  (object) 0,
                  (object) sqlSample.StartTime,
                  (object) sqlSample.SqlTimer.Elapsed.TotalMilliseconds,
                  (object) sqlSample.RowCount,
                  (object) sqlSample.SqlTextId,
                  (object) sqlSample.Params,
                  (object) sqlSample.TraceTextId,
                  (object) sqlSample.SqlSampleDateTime,
                  (object) sqlSample.QueryCache
                });
            }
          }
          if (keyValuePair.Key.TraceItems != null)
          {
            foreach (PXProfilerTraceItem traceItem in keyValuePair.Key.TraceItems)
            {
              if (PXPerformanceMonitor.FilterTraceItem(traceItem.Source, traceItem.EventType, traceItem.Category, keyValuePair.Key))
                data5.Add(new object[10]
                {
                  (object) keyValuePair.Value.RecordId,
                  (object) 0,
                  (object) traceItem.StartTime,
                  (object) traceItem.MessageId,
                  (object) traceItem.Source,
                  (object) traceItem.EventType,
                  (object) traceItem.TraceId,
                  (object) traceItem.EventDateTime,
                  (object) traceItem.ExceptionType,
                  (object) traceItem.EventDetails
                });
            }
          }
        }
        ConcurrentQueue<PXProfilerTraceItem> traceItems1 = PXPerformanceMonitor.TraceItems;
        ConcurrentQueue<PXProfilerTraceItem> traceItems2 = PXPerformanceMonitor.TraceItems;
        PXPerformanceMonitor.TraceItems = new ConcurrentQueue<PXProfilerTraceItem>();
        foreach (PXProfilerTraceItem profilerTraceItem in traceItems2)
        {
          if (PXPerformanceMonitor.FilterTraceItem(profilerTraceItem.Source, profilerTraceItem.EventType, profilerTraceItem.Category))
          {
            if (PerformanceMonitorMaint.CheckExistingRecords(profilerTraceItem.MessageId, PerformanceMonitorMaint.traceMessagesIds))
              data3.Add(new object[2]
              {
                (object) profilerTraceItem.MessageId,
                (object) profilerTraceItem.Message
              });
            if (profilerTraceItem.TraceId.HasValue && PerformanceMonitorMaint.CheckExistingRecords(profilerTraceItem.TraceId.Value, PerformanceMonitorMaint.sqlTracesIds))
              data2.Add(new object[2]
              {
                (object) profilerTraceItem.TraceId.Value,
                (object) profilerTraceItem.StackTrace
              });
            data5.Add(new object[10]
            {
              (object) 0,
              (object) 0,
              (object) profilerTraceItem.StartTime,
              (object) profilerTraceItem.MessageId,
              (object) profilerTraceItem.Source,
              (object) profilerTraceItem.EventType,
              (object) profilerTraceItem.TraceId,
              (object) profilerTraceItem.EventDateTime,
              null,
              (object) profilerTraceItem.EventDetails
            });
          }
        }
        if (PerformanceMonitorMaint.sqlTextsIds.Count > PerformanceMonitorMaint.maxCacheSize)
        {
          PerformanceMonitorMaint.sqlTextsIds = new HashSet<int>();
          PerformanceMonitorMaint.sqlTextsTables = new Dictionary<int, string>();
        }
        if (PerformanceMonitorMaint.sqlTracesIds.Count > PerformanceMonitorMaint.maxCacheSize)
          PerformanceMonitorMaint.sqlTracesIds = new HashSet<int>();
        if (PerformanceMonitorMaint.traceMessagesIds.Count > PerformanceMonitorMaint.maxCacheSize)
          PerformanceMonitorMaint.traceMessagesIds = new HashSet<int>();
        PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
        this.BulkInsert<SMPerformanceInfoSQL>(dbServicesPoint, data4, false);
        this.BulkInsert<SMPerformanceInfoSQLText>(dbServicesPoint, data1);
        this.BulkInsert<SMPerformanceInfoStackTrace>(dbServicesPoint, data2);
        this.BulkInsert<SMPerformanceInfoTraceEvents>(dbServicesPoint, data5, false);
        this.BulkInsert<SMPerformanceInfoTraceMessages>(dbServicesPoint, data3);
        bool flag = false;
        System.DateTime now = System.DateTime.Now;
        TimeSpan timeSpan = now.Subtract(PerformanceMonitorMaint._lastCheckTime);
        if (timeSpan.TotalMinutes >= 60.0)
        {
          lock (PerformanceMonitorMaint._lastCheckTimeLock)
          {
            now = System.DateTime.Now;
            timeSpan = now.Subtract(PerformanceMonitorMaint._lastCheckTime);
            if (timeSpan.TotalMinutes >= 60.0)
            {
              PerformanceMonitorMaint._lastCheckTime = System.DateTime.Now;
              flag = true;
            }
          }
        }
        if (flag)
        {
          try
          {
            this.CleanupLogs();
          }
          catch
          {
          }
        }
        this.EnsureProfilerDataSizeIsWithInTheLimitAndUpdateStateWithControls();
      }
    }
  }

  private void EnsureProfilerDataSizeIsWithInTheLimitAndUpdateStateWithControls()
  {
    PXPerformanceMonitor.EnsureProfilerDataSizeIsWithInTheLimit();
    this.UpdateProfilerControlsState((PerformanceMonitorMaint.SMPerformanceFilterRow) null, !PXPerformanceMonitor.IsProfilerDataSizeOverLimits);
  }

  private void UpdateProfilerControlsState(
    PerformanceMonitorMaint.SMPerformanceFilterRow row,
    bool isEnabled)
  {
    PXCache cache = this.Filter.Cache;
    PXUIFieldAttribute.SetEnabled<PerformanceMonitorMaint.SMPerformanceFilterRow.profilerEnabled>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<PerformanceMonitorMaint.SMPerformanceFilterRow.sqlProfiler>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<PerformanceMonitorMaint.SMPerformanceFilterRow.traceEnabled>(cache, (object) row, isEnabled);
    PXUIFieldAttribute.SetEnabled<PerformanceMonitorMaint.SMPerformanceFilterRow.traceExceptionsEnabled>(cache, (object) row, isEnabled);
    if (isEnabled)
      PXUIFieldAttribute.SetError(cache, (object) row, typeof (PerformanceMonitorMaint.SMPerformanceFilterRow.profilerEnabled).Name, (string) null, (string) null, true, PXErrorLevel.Undefined);
    else
      PXUIFieldAttribute.SetWarning<PerformanceMonitorMaint.SMPerformanceFilterRow.profilerEnabled>(cache, (object) row, $"The data size limit ({SpaceUsageMaint.FormatSize((double) WebConfig.ProfilerDataSizeLimit)}) for the request profiler has been exceeded. The profiler has been disabled. Clean up collected data to enable the profiler again.");
  }

  private void ImportLogs(byte[] content)
  {
    List<SMPerformanceInfo> smPerformanceInfoList = (List<SMPerformanceInfo>) null;
    List<SMPerformanceInfoSQLWithTables> infoSqlWithTablesList = (List<SMPerformanceInfoSQLWithTables>) null;
    List<SMPerformanceInfoTraceWithMessages> traceWithMessagesList = (List<SMPerformanceInfoTraceWithMessages>) null;
    using (MemoryStream memoryStream = new MemoryStream(content, false))
    {
      using (ZipArchiveWrapper zipArchiveWrapper = new ZipArchiveWrapper((Stream) memoryStream))
      {
        using (Stream stream = zipArchiveWrapper.GetStream("SMPerformanceInfo.log"))
          smPerformanceInfoList = this.DeserializeLargeJsonArray<SMPerformanceInfo>(stream).ToList<SMPerformanceInfo>();
      }
    }
    if (smPerformanceInfoList.Count == 0)
      return;
    using (MemoryStream memoryStream = new MemoryStream(content, false))
    {
      using (ZipArchiveWrapper zipArchiveWrapper = new ZipArchiveWrapper((Stream) memoryStream))
      {
        using (Stream stream = zipArchiveWrapper.GetStream("SMPerformanceInfoSQL.log"))
          infoSqlWithTablesList = this.DeserializeLargeJsonArray<SMPerformanceInfoSQLWithTables>(stream).ToList<SMPerformanceInfoSQLWithTables>();
      }
    }
    using (MemoryStream memoryStream = new MemoryStream(content, false))
    {
      using (ZipArchiveWrapper zipArchiveWrapper = new ZipArchiveWrapper((Stream) memoryStream))
      {
        using (Stream stream = zipArchiveWrapper.GetStream("SMPerformanceInfoTrace.log"))
          traceWithMessagesList = this.DeserializeLargeJsonArray<SMPerformanceInfoTraceWithMessages>(stream).ToList<SMPerformanceInfoTraceWithMessages>();
      }
    }
    List<object[]> data1 = new List<object[]>();
    List<object[]> data2 = new List<object[]>();
    List<object[]> data3 = new List<object[]>();
    List<object[]> data4 = new List<object[]>();
    List<object[]> data5 = new List<object[]>();
    HashSet<int?> nullableSet1 = new HashSet<int?>();
    HashSet<int?> nullableSet2 = new HashSet<int?>();
    HashSet<int?> nullableSet3 = new HashSet<int?>();
    Dictionary<int?, SMPerformanceInfo> source = new Dictionary<int?, SMPerformanceInfo>();
    PerformanceMonitorMaint instance = PXGraph.CreateInstance<PerformanceMonitorMaint>();
    foreach (SMPerformanceInfo smPerformanceInfo1 in smPerformanceInfoList)
    {
      int? recordId = smPerformanceInfo1.RecordId;
      smPerformanceInfo1.RecordId = new int?();
      smPerformanceInfo1.NoteID = new Guid?();
      smPerformanceInfo1.IsChecked = new bool?();
      SMPerformanceInfo smPerformanceInfo2 = instance.Samples.Insert(smPerformanceInfo1);
      source.Add(recordId, smPerformanceInfo2);
    }
    if (!source.Any<KeyValuePair<int?, SMPerformanceInfo>>())
      return;
    using (new PXConnectionStringScope(WebConfig.ProfilerConnectionString))
      instance.Actions.PressSave();
    foreach (SMPerformanceInfoSQLWithTables infoSqlWithTables in infoSqlWithTablesList)
    {
      SMPerformanceInfoSQL performanceInfoSql = (SMPerformanceInfoSQL) infoSqlWithTables;
      if (source.ContainsKey(performanceInfoSql.ParentId))
      {
        data4.Add(new object[10]
        {
          (object) source[performanceInfoSql.ParentId].RecordId,
          (object) 0,
          (object) performanceInfoSql.RequestStartTime,
          (object) performanceInfoSql.SqlTimeMs,
          (object) performanceInfoSql.NRows,
          (object) performanceInfoSql.SqlId,
          (object) performanceInfoSql.SQLParams,
          (object) performanceInfoSql.StackTraceId,
          (object) performanceInfoSql.RequestDateTime,
          (object) performanceInfoSql.QueryCache
        });
        if (!nullableSet1.Contains(performanceInfoSql.SqlId))
        {
          nullableSet1.Add(performanceInfoSql.SqlId);
          data1.Add(new object[5]
          {
            (object) performanceInfoSql.SqlId,
            (object) infoSqlWithTables.SQLText,
            (object) infoSqlWithTables.SQLHash,
            (object) infoSqlWithTables.TableList,
            (object) 0
          });
        }
        if (performanceInfoSql.StackTraceId.HasValue && !nullableSet2.Contains(performanceInfoSql.StackTraceId))
        {
          nullableSet2.Add(performanceInfoSql.StackTraceId);
          data2.Add(new object[2]
          {
            (object) performanceInfoSql.StackTraceId,
            (object) infoSqlWithTables.StackTrace
          });
        }
      }
    }
    foreach (SMPerformanceInfoTraceWithMessages traceWithMessages in traceWithMessagesList)
    {
      SMPerformanceInfoTraceEvents performanceInfoTraceEvents = (SMPerformanceInfoTraceEvents) traceWithMessages;
      data5.Add(new object[10]
      {
        (object) (source.ContainsKey(performanceInfoTraceEvents.ParentId) ? source[performanceInfoTraceEvents.ParentId].RecordId : new int?(0)),
        (object) 0,
        (object) performanceInfoTraceEvents.RequestStartTime,
        (object) performanceInfoTraceEvents.TraceMessageId,
        (object) performanceInfoTraceEvents.Source,
        (object) performanceInfoTraceEvents.TraceType,
        (object) performanceInfoTraceEvents.StackTraceId,
        (object) performanceInfoTraceEvents.EventDateTime,
        (object) performanceInfoTraceEvents.ExceptionType,
        (object) performanceInfoTraceEvents.EventDetails
      });
      if (!nullableSet3.Contains(performanceInfoTraceEvents.TraceMessageId))
      {
        nullableSet3.Add(performanceInfoTraceEvents.TraceMessageId);
        data3.Add(new object[2]
        {
          (object) performanceInfoTraceEvents.TraceMessageId,
          (object) traceWithMessages.MessageText
        });
      }
      if (performanceInfoTraceEvents.StackTraceId.HasValue && !nullableSet2.Contains(performanceInfoTraceEvents.StackTraceId))
      {
        nullableSet2.Add(performanceInfoTraceEvents.StackTraceId);
        data2.Add(new object[2]
        {
          (object) performanceInfoTraceEvents.StackTraceId,
          (object) traceWithMessages.StackTrace
        });
      }
    }
    PointDbmsBase dbServicesPoint = PXDatabase.Provider.CreateDbServicesPoint();
    this.BulkInsert<SMPerformanceInfoSQL>(dbServicesPoint, data4, false);
    this.BulkInsert<SMPerformanceInfoSQLText>(dbServicesPoint, data1);
    this.BulkInsert<SMPerformanceInfoStackTrace>(dbServicesPoint, data2);
    this.BulkInsert<SMPerformanceInfoTraceEvents>(dbServicesPoint, data5, false);
    this.BulkInsert<SMPerformanceInfoTraceMessages>(dbServicesPoint, data3);
  }

  private IEnumerable<T> DeserializeLargeJsonArray<T>(Stream jsonStream)
  {
    using (StreamReader streamReader = new StreamReader(jsonStream))
    {
      JsonTextReader jsonTextReader = new JsonTextReader((TextReader) streamReader);
      ((JsonReader) jsonTextReader).SupportMultipleContent = true;
      using (JsonTextReader jsonReader = jsonTextReader)
      {
        JsonSerializer serializer = new JsonSerializer();
        while (((JsonReader) jsonReader).Read())
        {
          if (((JsonReader) jsonReader).TokenType == 1)
            yield return serializer.Deserialize<T>((JsonReader) jsonReader);
        }
      }
    }
  }

  public override bool ProviderInsert(System.Type table, params PXDataFieldAssign[] pars)
  {
    try
    {
      return base.ProviderInsert(table, pars);
    }
    catch (PXDatabaseException ex)
    {
      if (ex.ErrorCode == PXDbExceptions.PrimaryKeyConstraintViolation && (table == typeof (SMPerformanceInfoSQLText) || table == typeof (SMPerformanceInfoStackTrace) || table == typeof (SMPerformanceInfoTraceMessages)))
        return true;
      throw;
    }
  }

  public override IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    using (new PXConnectionStringScope(WebConfig.ProfilerConnectionString))
      return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  private class SqlDigestRow
  {
    public int Count;
    public int Rows;
    public string Key;
    public long SqlTime;
    public string BqlHashViewName;
  }

  [Serializable]
  public class SMPerformanceSettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBIdentity(IsKey = true)]
    public int? RecordId { get; set; }

    [PXDBString(255 /*0xFF*/)]
    public string ScreenId { get; set; }

    [PXDBString(50)]
    public string UserId { get; set; }

    [PXDBBool]
    public bool? SqlProfiler { get; set; }

    [PXDBBool]
    public bool? SqlProfilerStackTrace { get; set; }

    [PXDBBool]
    public bool? SqlProfilerShowStackTrace { get; set; }

    [PXDBBool]
    public bool? SqlProfilerIncludeQueryCache { get; set; }

    [PXDBBool]
    public bool? LogExpensiveRequests { get; set; }

    [PXDBBool]
    public bool? LogImportantExceptions { get; set; }

    [PXDBBool]
    public bool? TraceEnabled { get; set; }

    [PXDBBool]
    public bool? TraceExceptionsEnabled { get; set; }

    [PXDBBool]
    public bool? ProfilerEnabled { get; set; }

    [PXDBInt]
    public int? TimeLimit { get; set; }

    [PXDBInt]
    public int? SqlCounterLimit { get; set; }

    [PXDBInt]
    public int? SqlTimeLimit { get; set; }

    [PXDBInt]
    public int? SqlRowCountLimit { get; set; }

    [PXDBString]
    public string SqlMethod { get; set; }

    [PXDBString]
    public string LogLevel { get; set; }

    [PXDBString]
    public string LogCategory { get; set; }

    [PXDBBool]
    public bool? SaveRequestsToDb { get; set; }

    [PXDBBool]
    public bool? SaveSqlToDb { get; set; }

    [PXDBString]
    public string UserProfilerName { get; set; }

    [PXDBDate(PreserveTime = true, UseSmallDateTime = false, UseTimeZone = false, DisplayMask = "g")]
    public System.DateTime? UserProfilerDate { get; set; }

    [PXDBInt]
    public int? FlushVersion { get; set; }

    public abstract class recordId : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.recordId>
    {
    }

    public abstract class screenId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.screenId>
    {
    }

    public abstract class userId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.userId>
    {
    }

    public abstract class sqlProfiler : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.sqlProfiler>
    {
    }

    public abstract class sqlProfilerStackTrace : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.sqlProfilerStackTrace>
    {
    }

    public abstract class sqlProfilerShowStackTrace : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.sqlProfilerShowStackTrace>
    {
    }

    public abstract class sqlProfilerIncludeQueryCache : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.sqlProfilerIncludeQueryCache>
    {
    }

    public abstract class logExpensiveRequests : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.logExpensiveRequests>
    {
    }

    public abstract class logImportantExceptions : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.logImportantExceptions>
    {
    }

    public abstract class traceEnabled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.traceEnabled>
    {
    }

    public abstract class traceExceptionsEnabled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.traceExceptionsEnabled>
    {
    }

    public abstract class profilerEnabled : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.profilerEnabled>
    {
    }

    public abstract class timeLimit : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.timeLimit>
    {
    }

    public abstract class sqlCounterLimit : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.sqlCounterLimit>
    {
    }

    public abstract class sqlTimeLimit : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.sqlTimeLimit>
    {
    }

    public abstract class sqlRowCountLimit : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.sqlRowCountLimit>
    {
    }

    public abstract class sqlMethod : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.sqlMethod>
    {
    }

    public abstract class logLevel : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.logLevel>
    {
    }

    public abstract class logCategory : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.logCategory>
    {
    }

    public abstract class saveRequestsToDb : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.saveRequestsToDb>
    {
    }

    public abstract class saveSqlToDb : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.saveSqlToDb>
    {
    }

    public abstract class userProfilerName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.userProfilerName>
    {
    }

    public abstract class userProfilerDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.userProfilerDate>
    {
    }

    public abstract class flushVersion : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceSettings.flushVersion>
    {
    }
  }

  [Serializable]
  public class SMPerformanceFilterRow : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString]
    [PXUIField(DisplayName = "URL")]
    public string ScreenId
    {
      get => PXPerformanceMonitor.FilterScreenId;
      set => PXPerformanceMonitor.FilterScreenId = value;
    }

    [PXString]
    [PXUIField(DisplayName = "CommandTarget")]
    public string CommandTarget { get; set; }

    [PXString]
    [PXUIField(DisplayName = "CommandName")]
    public string CommandName { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Username")]
    public string UserId
    {
      get => PXPerformanceMonitor.FilterUserName;
      set => PXPerformanceMonitor.FilterUserName = value;
    }

    [PXDouble]
    [PXUIField(DisplayName = "RequestTimeMs")]
    public double? RequestTimeMs { get; set; }

    [PXDouble]
    [PXUIField(DisplayName = "Request CPU, ms")]
    public double? RequestCpuTimeMs { get; set; }

    [PXDouble]
    [PXUIField(DisplayName = "SqlTimeMs")]
    public double? SqlTimeMs { get; set; }

    [PXUIField(DisplayName = "Log SQL (Apply Filter)")]
    [PXBool]
    public bool? SqlProfiler
    {
      get => new bool?(PXPerformanceMonitor.SaveSqlToDb);
      set
      {
        if (!value.HasValue)
          return;
        PXPerformanceMonitor.SaveSqlToDb = value.Value;
      }
    }

    [PXUIField(DisplayName = "Log SQL Requests Stack Trace")]
    [PXBool]
    public bool? SqlProfilerStackTrace
    {
      get => new bool?(PXPerformanceMonitor.SqlProfilerStackTraceEnabled);
      set
      {
        if (!value.HasValue)
          return;
        PXPerformanceMonitor.SqlProfilerStackTraceEnabled = value.Value;
      }
    }

    [PXUIField(DisplayName = "Show Stack Trace")]
    [PXBool]
    public bool? SqlProfilerShowStackTrace
    {
      get => new bool?(PXPerformanceMonitor.SqlProfilerShowStackTrace);
      set
      {
        if (!value.HasValue)
          return;
        PXPerformanceMonitor.SqlProfilerShowStackTrace = value.Value;
      }
    }

    [PXUIField(DisplayName = "Log Events (Apply Filter)")]
    [PXBool]
    public bool? TraceEnabled
    {
      get => new bool?(PXPerformanceMonitor.TraceEnabled);
      set
      {
        if (!value.HasValue)
          return;
        PXPerformanceMonitor.TraceEnabled = value.Value;
      }
    }

    [PXUIField(DisplayName = "Log Exceptions")]
    [PXBool]
    public bool? TraceExceptionsEnabled
    {
      get => new bool?(PXPerformanceMonitor.TraceExceptionsEnabled);
      set
      {
        if (!value.HasValue)
          return;
        PXPerformanceMonitor.TraceExceptionsEnabled = value.Value;
      }
    }

    [PXUIField(DisplayName = "Log Requests (Apply Filter)")]
    [PXBool]
    public bool? ProfilerEnabled
    {
      get
      {
        PXPerformanceMonitor.ExpirationTime = System.DateTime.Now.AddDays(1.0);
        return new bool?(PXPerformanceMonitor.SaveRequestsToDb);
      }
      set
      {
        if (!value.HasValue)
          return;
        PXPerformanceMonitor.SaveRequestsToDb = value.Value;
      }
    }

    [PXBool]
    public bool? SaveRequestsToDb
    {
      get
      {
        PXPerformanceMonitor.ExpirationTime = System.DateTime.Now.AddDays(1.0);
        return new bool?(PXPerformanceMonitor.SaveRequestsToDb);
      }
      set
      {
        if (!value.HasValue)
          return;
        PXPerformanceMonitor.SaveRequestsToDb = value.Value;
      }
    }

    [PXInt]
    [PXUIField(DisplayName = "Server Time Threshold")]
    public int? TimeLimit
    {
      get => PXPerformanceMonitor.FilterTimeLimit;
      set => PXPerformanceMonitor.FilterTimeLimit = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "SQL Count Threshold")]
    public int? SqlCounterLimit
    {
      get => PXPerformanceMonitor.FilterSqlCount;
      set => PXPerformanceMonitor.FilterSqlCount = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "SQL Time Threshold")]
    public int? SqlTimeLimit
    {
      get => PXPerformanceMonitor.FilterSqlTime;
      set => PXPerformanceMonitor.FilterSqlTime = value;
    }

    [PXInt]
    [PXUIField(DisplayName = "Row Count Threshold")]
    public int? SqlRowCounterLimit
    {
      get => PXPerformanceMonitor.FilterSqlRowCount;
      set => PXPerformanceMonitor.FilterSqlRowCount = value;
    }

    [PXString]
    [PXUIField(DisplayName = "Executed by Method")]
    public string SqlMethodFilter
    {
      get => PXPerformanceMonitor.SqlMethodFilter;
      set => PXPerformanceMonitor.SqlMethodFilter = value;
    }

    [PXString]
    [FilterTraceTypeList]
    [PXUIField(DisplayName = "Log Level")]
    public string LogLevelFilter
    {
      get => PXPerformanceMonitor.LogLevelFilter;
      set => PXPerformanceMonitor.LogLevelFilter = value;
    }

    [PXString]
    [FilterTraceCategoryList(MultiSelect = true)]
    [PXUIField(DisplayName = "Category")]
    public string LogCategoryFilter
    {
      get => PXPerformanceMonitor.LogCategoryFilter;
      set => PXPerformanceMonitor.LogCategoryFilter = value;
    }

    [PXUIField(DisplayName = "Include Cached SQL Results")]
    [PXBool]
    public bool? SqlProfilerIncludeQueryCache
    {
      get => new bool?(PXPerformanceMonitor.SqlProfilerIncludeQueryCache);
      set
      {
        if (!value.HasValue)
          return;
        PXPerformanceMonitor.SqlProfilerIncludeQueryCache = value.Value;
      }
    }

    [Obsolete("This property is no longer used. See AC-330011 for details", true)]
    [PXUIField(DisplayName = "Log Expensive Requests")]
    [PXBool]
    public bool? LogExpensiveRequests
    {
      get => new bool?(PXPerformanceMonitor.LogExpensiveRequests);
      set
      {
      }
    }

    [Obsolete("This property is no longer used. See AC-330011 for details", true)]
    [PXUIField(DisplayName = "Log Important Exceptions")]
    [PXBool]
    public bool? LogImportantExceptions
    {
      get => new bool?(PXPerformanceMonitor.LogImportantExceptions);
      set
      {
      }
    }

    [PXUIField(DisplayName = "Threads", Enabled = false)]
    [PXString]
    public string CurrentThreads { get; set; }

    [PXBool]
    public bool? SelectRows { get; set; }

    public abstract class screenId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceFilterRow.screenId>
    {
    }

    public abstract class commandTarget : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceFilterRow.commandTarget>
    {
    }

    public abstract class commandName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceFilterRow.commandName>
    {
    }

    public abstract class userId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceFilterRow.userId>
    {
    }

    public abstract class sqlProfiler : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceFilterRow.sqlProfiler>
    {
    }

    public abstract class traceEnabled : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceFilterRow.traceEnabled>
    {
    }

    public abstract class traceExceptionsEnabled : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceFilterRow.traceExceptionsEnabled>
    {
    }

    public abstract class profilerEnabled : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceFilterRow.profilerEnabled>
    {
    }

    public abstract class logLevelFilter : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceFilterRow.logLevelFilter>
    {
    }

    public abstract class logCategoryFilter : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceFilterRow.logCategoryFilter>
    {
    }

    public abstract class sqlProfilerIncludeQueryCache : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PerformanceMonitorMaint.SMPerformanceFilterRow.sqlProfilerIncludeQueryCache>
    {
    }
  }
}
