// Decompiled with JetBrains decompiler
// Type: Serilog.LoggerExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Logging.Enrichers;
using Serilog.Core;
using System.Runtime.CompilerServices;

#nullable disable
namespace Serilog;

public static class LoggerExtensions
{
  public static ILogger WithStack(this ILogger logger)
  {
    return logger.ForContext((ILogEventEnricher) new StackTraceEnricher(false));
  }

  internal static ILogger WithSourceLocation(
    this ILogger logger,
    [CallerMemberName] string caller = null,
    [CallerFilePath] string path = null,
    [CallerLineNumber] int line = 0)
  {
    return logger.ForContext((ILogEventEnricher) new SourceLocationEnricher(caller, path, line));
  }
}
