// Decompiled with JetBrains decompiler
// Type: PX.SM.PXPerformanceInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;

#nullable disable
namespace PX.SM;

public class PXPerformanceInfo : 
  IEnumerable<KeyValuePair<string, double>>,
  IEnumerable,
  IEnumerable<KeyValuePair<string, string>>
{
  public string UserId;
  public string CompanyName;
  public string TenantId;
  public string ScreenId;
  public string InternalScreenId;
  public string RequestType;
  public string PrimaryViewKeys;
  public string CommandTarget;
  public string CommandName;
  public int SqlCounter;
  public int SqlRows;
  public int ExceptionCounter;
  public int TraceCounter;
  public int ProcessingCounter;
  public Stopwatch SqlTimer = new Stopwatch();
  public Stopwatch Timer = new Stopwatch();
  public Stopwatch SessionLoadTimer = new Stopwatch();
  public Stopwatch SessionSaveTimer = new Stopwatch();
  public Stopwatch TmRowSelected = new Stopwatch();
  public Stopwatch TmRowSelecting = new Stopwatch();
  public Stopwatch TmFieldDefaulting = new Stopwatch();
  public Stopwatch TmFormulaCalculations = new Stopwatch();
  public Stopwatch TmInsert = new Stopwatch();
  public Stopwatch TmUpdate = new Stopwatch();
  public Stopwatch TmDelete = new Stopwatch();
  public Stopwatch TmGetSlot = new Stopwatch();
  public Stopwatch TmPrefetch = new Stopwatch();
  public Stopwatch TmScreenInfo = new Stopwatch();
  public int SessionLockCount;
  public double SessionLoadSize;
  public double SessionSaveSize;
  public double ReportsSessionSize;
  public System.DateTime StartTime = System.DateTime.UtcNow;
  public System.DateTime StartTimeLocal = System.DateTime.Now;
  public TimeSpan ThreadTime;
  public long MemBefore;
  public long MemDelta;
  public double MemWorkingSet;
  public object Tag;
  public bool IsExpensive;
  public bool IsImportant;
  public bool? IsNewUI;
  public int? nCaches;
  public int? ScriptTimeMs;
  public string ClientScriptKey;
  public string ID = PXPerformanceInfo.GetSampleId();
  [NonSerialized]
  private readonly StringTable traceTable = new StringTable();
  public List<PXProfilerSqlSample> SqlSamples;
  public List<PXProfilerSqlSample> QueryCacheSamples;
  [NonSerialized]
  public StringTable sqlTable = new StringTable();
  public List<PXProfilerTraceItem> TraceItems;
  public Queue<PXProfilerTraceItem> TraceItemsInProgress = new Queue<PXProfilerTraceItem>();
  public int SelectCount;
  public Stopwatch SelectTimer = new Stopwatch();
  [NonSerialized]
  internal Thread WorkingThread;
  private static readonly int MinLevel = PXPerformanceMonitor.LogLevels["Information"];

  public void SetPeakMemory()
  {
    long num = GC.GetTotalMemory(false) - this.MemBefore;
    if (num < this.MemDelta)
      return;
    this.MemDelta = num;
  }

  private static string GetSampleId()
  {
    Guid? nullable = PXContext.GetSlot<Guid?>("PerformanceMonitorSampleId");
    if (nullable.HasValue)
      return PXPerformanceMonitor.GuidToStr(nullable.Value);
    nullable = new Guid?(SequentialGuid.Generate());
    PXContext.SetSlot<Guid?>("PerformanceMonitorSampleId", nullable);
    return PXPerformanceMonitor.GuidToStr(nullable.Value);
  }

  public PXProfilerSqlSample AddSqlSample(string cmdText, string parameterDeclaration)
  {
    return this.AddSqlSample(cmdText, parameterDeclaration, false);
  }

  public PXProfilerSqlSample AddSqlSample(
    string cmdText,
    string parameterDeclaration,
    bool queryCache)
  {
    if (!PXPerformanceMonitor.SqlProfilerEnabled && !PXPerformanceMonitor.LogExpensiveRequests && !PXPerformanceMonitor.LogImportantExceptions)
      return (PXProfilerSqlSample) null;
    if (SuppressPerformanceInfoScope.IsScoped)
      return (PXProfilerSqlSample) null;
    if (queryCache && !PXPerformanceMonitor.SqlProfilerIncludeQueryCache)
      return (PXProfilerSqlSample) null;
    if (this.SqlSamples == null)
      this.SqlSamples = new List<PXProfilerSqlSample>();
    if (this.QueryCacheSamples == null)
      this.QueryCacheSamples = new List<PXProfilerSqlSample>();
    string s = (string) null;
    if (PXPerformanceMonitor.SqlProfilerStackTraceEnabled)
      s = PXStackTrace.RemoveSystemMethods(PXStackTrace.GetStackTrace());
    cmdText = this.sqlTable.Add(cmdText);
    string str = this.sqlTable.Add(s);
    if (!PXPerformanceMonitor.SaveSqlToDb && (cmdText.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase) || cmdText.StartsWith("INSERT", StringComparison.OrdinalIgnoreCase)))
      parameterDeclaration = string.Empty;
    PXProfilerSqlSample profilerSqlSample = new PXProfilerSqlSample()
    {
      Text = cmdText,
      Params = parameterDeclaration,
      StartTime = new double?(this.Timer.Elapsed.TotalMilliseconds),
      StackTrace = str,
      SqlSampleDateTime = new System.DateTime?(System.DateTime.Now),
      QueryCache = queryCache,
      BqlHash = PerformanceMonitorSqlSampleScope.BqlHash,
      BqlHashViewName = PerformanceMonitorSqlSampleScope.BqlHashViewName
    };
    profilerSqlSample.SqlTextId = System.Math.Abs(PXReflectionSerializer.GetStableHash(profilerSqlSample.Text));
    if (!string.IsNullOrEmpty(profilerSqlSample.StackTrace))
      profilerSqlSample.TraceTextId = System.Math.Abs(PXReflectionSerializer.GetStableHash(profilerSqlSample.StackTrace));
    if (!queryCache)
      this.SqlSamples.Add(profilerSqlSample);
    else
      this.QueryCacheSamples.Add(profilerSqlSample);
    return profilerSqlSample;
  }

  public static PXProfilerSqlSample FindLastSample(
    List<PXProfilerSqlSample> samples,
    IDataReader reader)
  {
    if (samples == null)
      return (PXProfilerSqlSample) null;
    int num = 0;
    if (samples.Count > 20)
      num = samples.Count - 20;
    for (int index = samples.Count - 1; index >= num; --index)
    {
      if (samples[index].Reader == reader)
        return samples[index];
    }
    return (PXProfilerSqlSample) null;
  }

  public PXProfilerTraceItem AddTrace(
    string source,
    string traceType,
    string message,
    string stackTrace)
  {
    return this.AddTrace(source, traceType, message, stackTrace, (string) null, (string) null, (string) null, (string) null);
  }

  public PXProfilerTraceItem AddTrace(
    string source,
    string traceType,
    string message,
    string stackTrace,
    string exceptionType,
    string eventDetails,
    string messageTemplate)
  {
    return this.AddTrace(source, traceType, message, stackTrace, exceptionType, eventDetails, messageTemplate, (string) null);
  }

  public PXProfilerTraceItem AddTrace(
    string source,
    string traceType,
    string message,
    string stackTrace,
    string exceptionType,
    string eventDetails,
    string messageTemplate,
    string dacDescriptorInfo)
  {
    if (traceType.Equals("FirstChanceException"))
      ++this.ExceptionCounter;
    else
      ++this.TraceCounter;
    bool flag1 = traceType.Equals("FirstChanceException") || PXPerformanceMonitor.LogLevels.ContainsKey(traceType) && PXPerformanceMonitor.LogLevels[traceType] <= PXPerformanceInfo.MinLevel;
    if (!flag1 && !PXPerformanceMonitor.TraceEnabled && !PXPerformanceMonitor.TraceExceptionsEnabled && !PXPerformanceMonitor.LogImportantExceptions)
      return (PXProfilerTraceItem) null;
    if (SuppressPerformanceInfoScope.IsScoped)
      return (PXProfilerTraceItem) null;
    if (traceType.Equals("FirstChanceException") && !flag1 && !PXPerformanceMonitor.TraceExceptionsEnabled && !PXPerformanceMonitor.LogImportantExceptions)
      return (PXProfilerTraceItem) null;
    if (this.TraceItems == null)
      this.TraceItems = new List<PXProfilerTraceItem>();
    bool flag2 = false;
    string traceCategory = PXPerformanceMonitor.GetTraceCategory(messageTemplate);
    if (PXPerformanceMonitor.FilterTraceItem(source, traceType, traceCategory))
      flag2 = true;
    if (!flag1 && !flag2)
      return (PXProfilerTraceItem) null;
    source = this.traceTable.Add(source);
    traceType = this.traceTable.Add(traceType);
    message = this.traceTable.Add(message);
    stackTrace = this.traceTable.Add(stackTrace);
    exceptionType = this.traceTable.Add(exceptionType);
    dacDescriptorInfo = this.traceTable.Add(dacDescriptorInfo);
    PXProfilerTraceItem profilerTraceItem = new PXProfilerTraceItem()
    {
      Source = source,
      EventType = traceType,
      Message = message,
      Category = traceCategory,
      StartTime = new double?(this.Timer.Elapsed.TotalMilliseconds),
      EventDateTime = System.DateTime.Now,
      StackTrace = stackTrace,
      ExceptionType = exceptionType,
      DacDescriptorInfo = dacDescriptorInfo,
      EventDetails = eventDetails
    };
    if (traceType.Equals("FirstChanceException"))
      profilerTraceItem.Category = (string) null;
    profilerTraceItem.MessageId = System.Math.Abs(PXReflectionSerializer.GetStableHash(profilerTraceItem.Message));
    if (!string.IsNullOrEmpty(profilerTraceItem.StackTrace))
      profilerTraceItem.TraceId = new int?(System.Math.Abs(PXReflectionSerializer.GetStableHash(profilerTraceItem.StackTrace)));
    if (flag1)
    {
      if ((long) this.TraceItemsInProgress.Count > (long) (PXPerformanceMonitor.MaxTraceItemsInProgressSize - 1U))
        this.TraceItemsInProgress.Dequeue();
      this.TraceItemsInProgress.Enqueue(profilerTraceItem);
    }
    if (flag2)
      this.TraceItems.Add(profilerTraceItem);
    return profilerTraceItem;
  }

  IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
  {
    return ((IEnumerable<KeyValuePair<string, string>>) new KeyValuePair<string, string>[0]).AsEnumerable<KeyValuePair<string, string>>().GetEnumerator();
  }

  IEnumerator<KeyValuePair<string, double>> IEnumerable<KeyValuePair<string, double>>.GetEnumerator()
  {
    if (this.SessionLoadSize > 0.0)
      yield return new KeyValuePair<string, double>("SessionLoadSize", this.SessionLoadSize);
    if (this.SessionSaveSize > 0.0)
      yield return new KeyValuePair<string, double>("SessionSaveSize", this.SessionSaveSize);
    if (this.ReportsSessionSize > 0.0)
      yield return new KeyValuePair<string, double>("ReportsSessionSize", this.ReportsSessionSize);
    if (PXPerformanceMonitor.IsTimersScopesEnabled)
    {
      yield return new KeyValuePair<string, double>("TmRowSelected", (double) this.TmRowSelected.ElapsedMilliseconds);
      yield return new KeyValuePair<string, double>("TmRowSelecting", (double) this.TmRowSelecting.ElapsedMilliseconds);
      yield return new KeyValuePair<string, double>("TmFieldDefaulting", (double) this.TmFieldDefaulting.ElapsedMilliseconds);
      yield return new KeyValuePair<string, double>("TmFormulaCalculations", (double) this.TmFormulaCalculations.ElapsedMilliseconds);
      yield return new KeyValuePair<string, double>("TmInsert", (double) this.TmInsert.ElapsedMilliseconds);
      yield return new KeyValuePair<string, double>("TmUpdate", (double) this.TmUpdate.ElapsedMilliseconds);
      yield return new KeyValuePair<string, double>("TmDelete", (double) this.TmDelete.ElapsedMilliseconds);
      yield return new KeyValuePair<string, double>("TmGetSlot", (double) this.TmGetSlot.ElapsedMilliseconds);
      yield return new KeyValuePair<string, double>("TmPrefetch", (double) this.TmPrefetch.ElapsedMilliseconds);
      yield return new KeyValuePair<string, double>("TmScreenInfo", (double) this.TmScreenInfo.ElapsedMilliseconds);
    }
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    PXPerformanceInfo pxPerformanceInfo = this;
    foreach (KeyValuePair<string, double> keyValuePair in (IEnumerable<KeyValuePair<string, double>>) pxPerformanceInfo)
      yield return (object) keyValuePair;
    foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) pxPerformanceInfo)
      yield return (object) keyValuePair;
  }
}
