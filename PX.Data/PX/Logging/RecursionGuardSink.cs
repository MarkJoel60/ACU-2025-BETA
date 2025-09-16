// Decompiled with JetBrains decompiler
// Type: PX.Logging.RecursionGuardSink
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog.Core;
using Serilog.Events;
using System;
using System.Threading;

#nullable disable
namespace PX.Logging;

internal class RecursionGuardSink : ILogEventSink, IDisposable
{
  private readonly Logger _logger;
  private readonly ILogEventSink _sink;
  private readonly AsyncLocal<bool> _recursionGuard = new AsyncLocal<bool>()
  {
    Value = false
  };

  public RecursionGuardSink(Logger logger)
  {
    this._logger = logger;
    this._sink = (ILogEventSink) this._logger;
  }

  void IDisposable.Dispose() => this._logger.Dispose();

  public void Emit(LogEvent logEvent)
  {
    if (this._recursionGuard.Value)
    {
      SelfLog.WriteLine("Recursion detected at RecursionGuardSink");
    }
    else
    {
      this._recursionGuard.Value = true;
      try
      {
        this._sink.Emit(logEvent);
      }
      finally
      {
        this._recursionGuard.Value = false;
      }
    }
  }
}
