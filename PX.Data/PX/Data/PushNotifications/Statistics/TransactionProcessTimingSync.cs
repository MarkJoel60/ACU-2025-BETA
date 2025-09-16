// Decompiled with JetBrains decompiler
// Type: PX.Data.PushNotifications.Statistics.TransactionProcessTimingSync
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Telemetry;
using PX.PushNotifications;
using PX.SM;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.PushNotifications.Statistics;

internal class TransactionProcessTimingSync : ILogEventSink, IDisposable
{
  private readonly IQueueStatisticAggregator _statisticsAggregator;
  private readonly IQueueDispatcherWithStatusInfo _dispatcher;
  private readonly string _queueType;
  private TimeSpan _elapsed;
  private Guid? _transactionId;
  private readonly List<(string sourceName, TimeSpan elapsed)> _processedSources = new List<(string, TimeSpan)>();
  private readonly LinkedList<LogEvent> _logList = new LinkedList<LogEvent>();
  private readonly ITelemetryInfo _telemetryInfo;

  public TransactionProcessTimingSync(
    IQueueStatisticAggregator statisticsAggregator,
    IQueueDispatcherWithStatusInfo dispatcher,
    string queueType,
    ITelemetryInfoFactory telemetryInfoFactory)
  {
    this._statisticsAggregator = statisticsAggregator;
    this._dispatcher = dispatcher;
    this._queueType = queueType;
    this._telemetryInfo = telemetryInfoFactory.Create(PXPerformanceMonitor.CreateAndSetupPerformanceInfo("Temporal", "Multiple Screens"));
  }

  public void Emit(LogEvent logEvent)
  {
    this.ExtractAndWriteElapsed(logEvent);
    this.ExtractAndWriteTransactionId(logEvent);
    this.ExtractAndWriteProcessesSource(logEvent);
    while (this._logList.Count > this._dispatcher.MaxLogCount)
      this._logList.RemoveFirst();
    this._logList.AddLast(logEvent);
  }

  private void ExtractAndWriteProcessesSource(LogEvent logEvent)
  {
    LogEventPropertyValue eventPropertyValue1;
    if (!logEvent.Properties.TryGetValue("SourceName", out eventPropertyValue1))
      return;
    string str = (eventPropertyValue1 is ScalarValue scalarValue1 ? scalarValue1.Value : (object) null) as string;
    LogEventPropertyValue eventPropertyValue2;
    if (!logEvent.Properties.TryGetValue("Elapsed", out eventPropertyValue2) || !(eventPropertyValue2 is ScalarValue scalarValue2) || !(scalarValue2.Value is double num))
      return;
    TimeSpan timeSpan = TimeSpan.FromMilliseconds(num);
    this._processedSources.Add((str, timeSpan));
  }

  private void ExtractAndWriteTransactionId(LogEvent logEvent)
  {
    LogEventPropertyValue eventPropertyValue;
    if (!logEvent.Properties.TryGetValue("ProcessingTransaction", out eventPropertyValue))
      return;
    this._transactionId = (eventPropertyValue is ScalarValue scalarValue ? scalarValue.Value : (object) null) as Guid?;
  }

  private void ExtractAndWriteElapsed(LogEvent logEvent)
  {
    LogEventPropertyValue eventPropertyValue;
    if (!logEvent.Properties.TryGetValue("Elapsed", out eventPropertyValue) || !(eventPropertyValue is ScalarValue scalarValue) || !(scalarValue.Value is double num))
      return;
    this._elapsed = TimeSpan.FromMilliseconds(num);
  }

  public void Dispose()
  {
    try
    {
      bool flag = this._elapsed > TimeSpan.FromMilliseconds((double) this._dispatcher.SlowTransactionThreshold);
      TransactionLogInfo transactionLogInfo = new TransactionLogInfo(this._logList, this._elapsed, System.DateTime.UtcNow, this._processedSources);
      QueueOutMessageMetadata data = new QueueOutMessageMetadata()
      {
        elapsed = this._elapsed,
        IsThresholdExceeded = flag,
        DbTransactionId = this._transactionId ?? Guid.Empty,
        LogInfo = transactionLogInfo,
        QueueType = this._queueType,
        TelemetryInfo = this._telemetryInfo
      };
      PXPerformanceInfo performanceInfo = this._telemetryInfo.PerformanceInfo;
      performanceInfo.Timer.Stop();
      performanceInfo.ThreadTime = PXPerformanceMonitor.CurrentThreadTime() - performanceInfo.ThreadTime;
      long totalMemory = GC.GetTotalMemory(false);
      performanceInfo.MemDelta = totalMemory - performanceInfo.MemBefore;
      PXPerformanceMonitor.RemoveSampleInProgress(performanceInfo);
      PXContext.SetSlot<PXPerformanceInfo>((PXPerformanceInfo) null);
      this._statisticsAggregator.SendOutMessageMetadata(data);
      this._statisticsAggregator.ReportSlowLog(data);
    }
    catch
    {
    }
  }
}
