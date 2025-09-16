// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSelectAsyncResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Database.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace PX.Data;

/// <exclude />
internal sealed class PXSelectAsyncResult : PXSelectResultBase, IAsyncEnumerable<PXDataRecord>
{
  private readonly Func<(DbCommand, int, TimeSpan)> _commandFactory;
  private readonly CancellationToken _token;
  private readonly PXDatabaseProvider _provider;
  private readonly StringTable _stringTable;
  internal readonly Stopwatch _Timer = new Stopwatch();
  private static readonly bool StringInternEnabled = WebConfig.GetBool(nameof (StringInternEnabled), true);

  public PXSelectAsyncResult(
    PXDatabaseProvider provider,
    Func<DbCommand> commandFactory,
    CancellationToken token)
    : this(provider, (Func<(DbCommand, int, TimeSpan)>) (() => (commandFactory(), 0, TimeSpan.Zero)), token)
  {
  }

  public PXSelectAsyncResult(
    PXDatabaseProvider provider,
    Func<(DbCommand, int, TimeSpan)> commandFactory,
    CancellationToken token)
  {
    this._commandFactory = commandFactory;
    this._token = token;
    this._provider = provider;
    if (!PXSelectAsyncResult.StringInternEnabled)
      return;
    this._stringTable = new StringTable();
  }

  /// <inheritdoc />
  public IAsyncEnumerator<PXDataRecord> GetAsyncEnumerator(CancellationToken cancellationToken = default (CancellationToken))
  {
    return (IAsyncEnumerator<PXDataRecord>) new PXSelectAsyncResult.PXSelectResultAsyncEnumerator(this, cancellationToken == new CancellationToken() ? this._token : cancellationToken);
  }

  private sealed class PXSelectResultAsyncEnumerator : 
    PXSelectResultBase.PXSelectResultEnumeratorBase,
    IAsyncEnumerator<PXDataRecord>,
    IAsyncDisposable
  {
    private readonly PXContextChecker _contextChecker = new PXContextChecker(nameof (PXSelectResultAsyncEnumerator));
    private readonly PXSelectAsyncResult _result;
    private readonly CancellationToken _cancellationToken;
    private DbCommand _dbCommand;
    private DbDataReader _reader;
    private int _rowCountLimit;
    private List<System.Action> _postponedRowSelecting = new List<System.Action>();

    private void ExecuteRowSelecting()
    {
      for (int index = 0; index < this._postponedRowSelecting.Count; ++index)
        this._postponedRowSelecting[index]();
      this._postponedRowSelecting.Clear();
    }

    public PXSelectResultAsyncEnumerator(
      PXSelectAsyncResult result,
      CancellationToken cancellationToken)
    {
      this._result = result;
      this._cancellationToken = cancellationToken;
    }

    /// <inheritdoc />
    public PXDataRecord Current
    {
      get
      {
        if (this._reader == null)
          return (PXDataRecord) null;
        this._contextChecker.CheckContext();
        PXDataRecord record = this._result._provider.CreateRecord((IDataReader) this._reader, (IDbCommand) null, this._result._stringTable);
        record.SetRowSelecting(this._postponedRowSelecting);
        return record;
      }
    }

    /// <inheritdoc />
    public async ValueTask<bool> MoveNextAsync()
    {
      PXSelectAsyncResult.PXSelectResultAsyncEnumerator resultAsyncEnumerator = this;
      PXCancellationToken.CheckCancellation();
      resultAsyncEnumerator._cancellationToken.ThrowIfCancellationRequested();
      resultAsyncEnumerator._contextChecker.CheckContext();
      if (resultAsyncEnumerator._dbCommand == null)
      {
        (DbCommand dbCommand, int num, TimeSpan _) = resultAsyncEnumerator._result._commandFactory();
        resultAsyncEnumerator._dbCommand = dbCommand;
        resultAsyncEnumerator._rowCountLimit = num;
      }
      if (resultAsyncEnumerator._reader != null && resultAsyncEnumerator._reader.IsClosed)
      {
        resultAsyncEnumerator._reader.Dispose();
        resultAsyncEnumerator._reader = (DbDataReader) null;
        resultAsyncEnumerator.ExecuteRowSelecting();
      }
      if (resultAsyncEnumerator._reader == null)
      {
        if (resultAsyncEnumerator._result._provider is PXDatabaseDummyProvider)
          return false;
        if (resultAsyncEnumerator._dbCommand.Connection.State == ConnectionState.Closed)
          resultAsyncEnumerator._result._provider.OpenConnection((IDbConnection) resultAsyncEnumerator._dbCommand.Connection);
        DbDataReader dbDataReader = await resultAsyncEnumerator._result._provider.ExecuteReaderAsync(resultAsyncEnumerator._dbCommand, resultAsyncEnumerator._cancellationToken);
        resultAsyncEnumerator._reader = dbDataReader;
        if (PXPerformanceMonitor.SqlProfilerEnabled && PXPerformanceMonitor.IsEnabled)
        {
          PXPerformanceInfo currentSample = PXPerformanceMonitor.CurrentSample;
          if (currentSample != null)
            resultAsyncEnumerator._sample = PXPerformanceInfo.FindLastSample(currentSample.SqlSamples, (IDataReader) resultAsyncEnumerator._reader);
        }
      }
      bool hasRow;
      try
      {
        hasRow = await resultAsyncEnumerator._reader.ReadAsync(resultAsyncEnumerator._cancellationToken);
      }
      catch (object ex)
      {
        await Task.Yield();
        throw;
      }
      if (hasRow)
      {
        ++resultAsyncEnumerator.ReadRowCount;
        if (resultAsyncEnumerator._rowCountLimit > 0 && resultAsyncEnumerator.ReadRowCount >= resultAsyncEnumerator._rowCountLimit)
          PXSqlLimits.LimitExceeded((IDbCommand) resultAsyncEnumerator._dbCommand);
      }
      return hasRow;
    }

    /// <inheritdoc />
    public ValueTask DisposeAsync()
    {
      this.PerformanceMonitorSampleDispose((IDataReader) this._reader);
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
        this._reader = (DbDataReader) null;
      }
      if (this._dbCommand != null)
      {
        try
        {
          this._result._provider.LeaveConnection((IDbConnection) this._dbCommand.Connection);
        }
        catch (Exception ex)
        {
          PXTrace.Logger.Error(ex, ex.Message);
        }
        try
        {
          this._dbCommand.Dispose();
        }
        catch (Exception ex)
        {
          PXTrace.Logger.Error(ex, ex.Message);
        }
        this._dbCommand = (DbCommand) null;
      }
      this.ExecuteRowSelecting();
      this._contextChecker.Dispose();
      return new ValueTask();
    }
  }
}
