// Decompiled with JetBrains decompiler
// Type: SerilogTimings.Extensions.SerilogTimingsExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using Serilog;
using Serilog.Events;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace SerilogTimings.Extensions;

[PXInternalUseOnly]
public static class SerilogTimingsExtensions
{
  /// <summary>
  /// Begin a new timed operation leveled at <see cref="F:Serilog.Events.LogEventLevel.Verbose" />.
  /// The return value must be disposed to complete the operation.
  /// </summary>
  /// <param name="messageTemplate">A log message describing the operation, in message template format.</param>
  /// <param name="args">Arguments to the log message. These will be stored and captured only when the
  /// operation completes, so do not pass arguments that are mutated during the operation.</param>
  /// <seealso cref="M:SerilogTimings.Extensions.LoggerOperationExtensions.OperationAt(Serilog.ILogger,Serilog.Events.LogEventLevel,System.Nullable{Serilog.Events.LogEventLevel})" />
  /// <seealso cref="M:SerilogTimings.Configuration.LevelledOperation.Time(System.String,System.Object[])" />
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDisposable TimeOperationVerbose(
    this ILogger logger,
    string messageTemplate,
    params object[] args)
  {
    return LoggerOperationExtensions.OperationAt(logger, (LogEventLevel) 0, new LogEventLevel?()).Time(messageTemplate, args);
  }

  /// <summary>
  /// Begin a new timed operation leveled at  at <see cref="F:Serilog.Events.LogEventLevel.Verbose" />.
  /// The return value must be completed using <see cref="M:SerilogTimings.Operation.Complete" />,
  /// or disposed to record abandonment.
  /// </summary>
  /// <param name="messageTemplate">A log message describing the operation, in message template format.</param>
  /// <param name="args">Arguments to the log message. These will be stored and captured only when the
  /// operation completes, so do not pass arguments that are mutated during the operation.</param>
  /// <returns>An <see cref="T:SerilogTimings.Operation" /> object.</returns>
  /// <seealso cref="M:SerilogTimings.Extensions.LoggerOperationExtensions.OperationAt(Serilog.ILogger,Serilog.Events.LogEventLevel,System.Nullable{Serilog.Events.LogEventLevel})" />
  /// <seealso cref="M:SerilogTimings.Configuration.LevelledOperation.Begin(System.String,System.Object[])" />
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Operation BeginOperationVerbose(
    this ILogger logger,
    string messageTemplate,
    params object[] args)
  {
    return LoggerOperationExtensions.OperationAt(logger, (LogEventLevel) 0, new LogEventLevel?()).Begin(messageTemplate, args);
  }
}
