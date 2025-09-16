// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTelemetryLogger
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog;
using Serilog.Core;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class PXTelemetryLogger
{
  internal static readonly string LogEventPropertyName = "PXET";
  internal static readonly object LogEventPropertyScalarValue = (object) true;

  public static void LogMessage(string message, string eventName)
  {
    PXTelemetryLogger.TelemetryLogger(eventName, message).WithStack().Information("");
  }

  public static ILogger TelemetryLogger(string eventGroupName, string eventID)
  {
    return PXTrace.Logger.ForTelemetry(eventGroupName, eventID);
  }

  public static void WriteToTelemetry(
    string eventGroupName,
    string eventID,
    TelemetrySample info,
    bool writeStack = false)
  {
    ILogger logger = PXTelemetryLogger.TelemetryLogger(eventGroupName, eventID);
    if (writeStack)
      logger = logger.WithStack();
    logger.ForContext((ILogEventEnricher) info).Information("");
  }

  internal static void WriteToTelemetryRequests(this ILogger logger, TelemetrySample info)
  {
    logger.ForContext("DependencyName", (object) string.Empty, false).ForContext(PXTelemetryLogger.LogEventPropertyName, PXTelemetryLogger.LogEventPropertyScalarValue, false).ForContext((ILogEventEnricher) info).Information("");
  }

  public static ILogger ForTelemetry(this ILogger logger, string eventGroupName, string eventID)
  {
    return logger.ForContext("EventID", (object) eventID, false).ForContext("SourceContext", (object) eventGroupName, false).ForContext(PXTelemetryLogger.LogEventPropertyName, PXTelemetryLogger.LogEventPropertyScalarValue, false);
  }

  public static ILogger WithEventID(this ILogger logger, string eventGroupName, string eventID)
  {
    return logger.ForContext("EventID", (object) eventID, false).ForContext("SourceContext", (object) eventGroupName, false);
  }
}
