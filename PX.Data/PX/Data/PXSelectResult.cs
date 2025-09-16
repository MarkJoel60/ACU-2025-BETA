// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Database.Common;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace PX.Data;

/// <exclude />
internal sealed class PXSelectResult : PXSelectResultBase, IEnumerable<PXDataRecord>, IEnumerable
{
  private PXDatabaseProvider _provider;
  private StringTable _stringTable;
  internal Stopwatch _Timer = new Stopwatch();
  private static readonly bool StringInternEnabled = WebConfig.GetBool(nameof (StringInternEnabled), true);
  private readonly Func<(IDbCommand, int, TimeSpan)> _commandFactory;

  public PXSelectResult(PXDatabaseProvider provider, Func<IDbCommand> commandFactory)
    : this(provider, (Func<(IDbCommand, int, TimeSpan)>) (() => (commandFactory(), 0, TimeSpan.MaxValue)))
  {
  }

  public PXSelectResult(
    PXDatabaseProvider provider,
    Func<(IDbCommand, int, TimeSpan)> commandFactory)
  {
    this._commandFactory = commandFactory;
    this._provider = provider;
    if (!PXSelectResult.StringInternEnabled)
      return;
    this._stringTable = new StringTable();
  }

  IEnumerator<PXDataRecord> IEnumerable<PXDataRecord>.GetEnumerator()
  {
    return (IEnumerator<PXDataRecord>) new PXSelectResult.PXSelectResultEnumerator(this);
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) new PXSelectResult.PXSelectResultEnumerator(this);
  }

  private sealed class PXSelectResultEnumerator : 
    PXSelectResultBase.PXSelectResultEnumeratorBase,
    IEnumerator<PXDataRecord>,
    IDisposable,
    IEnumerator
  {
    private PXSelectResult _result;
    private IDbCommand _command;
    private IDataReader _reader;
    internal int _rowCountLimit;
    internal TimeSpan? _sqlTimeLimit;
    private List<System.Action> _postponedRowSelecting = new List<System.Action>();

    public PXSelectResultEnumerator(PXSelectResult result) => this._result = result;

    void IDisposable.Dispose()
    {
      this.PerformanceMonitorSampleDispose(this._reader);
      if (this._reader != null)
      {
        try
        {
          this._reader.Dispose();
        }
        catch (Exception ex)
        {
          PXTrace.Logger.Error(ex, ex.Message);
        }
        this._reader = (IDataReader) null;
      }
      if (this._command != null)
      {
        try
        {
          this._result._provider.LeaveConnection(this._command.Connection);
        }
        catch (Exception ex)
        {
          PXTrace.Logger.Error(ex, ex.Message);
        }
        try
        {
          this._command.Dispose();
        }
        catch (Exception ex)
        {
          PXTrace.Logger.Error(ex, ex.Message);
        }
        this._command = (IDbCommand) null;
      }
      this.ExecuteRowSelecting();
    }

    PXDataRecord IEnumerator<PXDataRecord>.Current
    {
      get
      {
        if (this._reader == null)
          return (PXDataRecord) null;
        PXDataRecord record = this._result._provider.CreateRecord(this._reader, (IDbCommand) null, this._result._stringTable);
        record.SetRowSelecting(this._postponedRowSelecting);
        return record;
      }
    }

    object IEnumerator.Current
    {
      get
      {
        if (this._reader == null)
          return (object) null;
        TimeSpan? sqlTimeLimit = this._sqlTimeLimit;
        TimeSpan elapsed = this._result._Timer.Elapsed;
        if ((sqlTimeLimit.HasValue ? (sqlTimeLimit.GetValueOrDefault() > elapsed ? 1 : 0) : 0) != 0)
          PXSqlLimits.ThrowSqlTimeout(this._command);
        PXDataRecord record = this._result._provider.CreateRecord(this._reader, this._command, this._result._stringTable);
        record.SetRowSelecting(this._postponedRowSelecting);
        return (object) record;
      }
    }

    bool IEnumerator.MoveNext()
    {
      PXCancellationToken.CheckCancellation();
      if (this._command == null)
      {
        (IDbCommand, int, TimeSpan) valueTuple = this._result._commandFactory();
        TimeSpan? nullable = new TimeSpan?(valueTuple.Item3);
        this._command = valueTuple.Item1;
        this._rowCountLimit = valueTuple.Item2;
        this._sqlTimeLimit = nullable;
      }
      if (this._reader != null && this._reader.IsClosed)
      {
        this._reader.Dispose();
        this._reader = (IDataReader) null;
        this.ExecuteRowSelecting();
      }
      if (this._reader == null)
      {
        if (this._result._provider is PXDatabaseDummyProvider)
          return false;
        if (this._command.Connection.State == ConnectionState.Closed)
          this._result._provider.OpenConnection(this._command.Connection);
        try
        {
          this._reader = this._result._provider.ExecuteReader(this._command);
          if (PXPerformanceMonitor.SqlProfilerEnabled)
          {
            if (PXPerformanceMonitor.IsEnabled)
            {
              PXPerformanceInfo currentSample = PXPerformanceMonitor.CurrentSample;
              if (currentSample != null)
                this._sample = PXPerformanceInfo.FindLastSample(currentSample.SqlSamples, this._reader);
            }
          }
        }
        catch (Exception ex)
        {
          Trace.WriteLine(ex.Message);
          throw;
        }
      }
      int num = this._reader.Read() ? 1 : 0;
      if (num == 0)
        return num != 0;
      ++this.ReadRowCount;
      if (this._rowCountLimit <= 0)
        return num != 0;
      if (this.ReadRowCount < this._rowCountLimit)
        return num != 0;
      PXSqlLimits.LimitExceeded(this._command);
      return num != 0;
    }

    private void ExecuteRowSelecting()
    {
      for (int index = 0; index < this._postponedRowSelecting.Count; ++index)
        this._postponedRowSelecting[index]();
      this._postponedRowSelecting.Clear();
    }

    void IEnumerator.Reset()
    {
      if (this._reader != null)
        this._reader.Dispose();
      if (this._command == null)
        return;
      if (this._command.Connection.State == ConnectionState.Closed)
        this._result._provider.OpenConnection(this._command.Connection);
      this._reader = this._result._provider.ExecuteReader(this._command);
      if (!PXPerformanceMonitor.SqlProfilerEnabled || !PXPerformanceMonitor.IsEnabled)
        return;
      PXPerformanceInfo currentSample = PXPerformanceMonitor.CurrentSample;
      if (currentSample == null)
        return;
      this._sample = PXPerformanceInfo.FindLastSample(currentSample.SqlSamples, this._reader);
    }
  }
}
