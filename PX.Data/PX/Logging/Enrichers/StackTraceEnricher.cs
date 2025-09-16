// Decompiled with JetBrains decompiler
// Type: PX.Logging.Enrichers.StackTraceEnricher
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using PX.Common;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Logging.Enrichers;

internal class StackTraceEnricher : ILogEventEnricher
{
  internal const string StackTracePropertyName = "StackTrace";
  private readonly bool _onlyWhenNoException;
  private static bool _gatherStackTrace;
  private static bool _gatherFileInfo;

  public StackTraceEnricher(bool onlyWhenNoException)
  {
    this._onlyWhenNoException = onlyWhenNoException;
  }

  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
    if (logEvent == null)
      throw new ArgumentNullException(nameof (logEvent));
    if (this._onlyWhenNoException && logEvent.Exception != null)
      return;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    logEvent.AddPropertyIfAbsent("StackTrace", StackTraceEnricher.\u003C\u003EO.\u003C0\u003E__Factory ?? (StackTraceEnricher.\u003C\u003EO.\u003C0\u003E__Factory = new Func<LogEventPropertyValue>(StackTraceEnricher.Factory)));
  }

  internal static void Initialize(IConfiguration settings)
  {
    StackTraceEnricher._gatherStackTrace = false;
    StackTraceEnricher._gatherFileInfo = false;
    try
    {
      if (!ConfigurationExtensions.AsEnumerable((IConfiguration) settings.GetSection("using")).Any<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Value == "PX.Data")))
        return;
      StackTraceEnricher._gatherStackTrace = ReadOptionalBoolParameter("always");
      StackTraceEnricher._gatherFileInfo = ReadOptionalBoolParameter("withFileInfo");
    }
    catch (Exception ex) when (SelfLog.WriteAndRethrow(ex, "Error while getting stack trace capture configuration"))
    {
    }

    bool ReadOptionalBoolParameter(string name)
    {
      string setting = settings["enrich:WithStackTrace." + name];
      bool result;
      return !string.IsNullOrWhiteSpace(setting) && bool.TryParse(setting, out result) && result;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static StackTrace GetStackTrace(int skipFrames)
  {
    return PXStackTrace.GetStackTrace(skipFrames, StackTraceEnricher._gatherFileInfo);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static string GetStackTraceIfEnabled(int skipFrames)
  {
    return !StackTraceEnricher._gatherStackTrace ? (string) null : StackTraceEnricher.GetStackTrace(skipFrames).ToString();
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  internal static LoggerConfiguration WithStackTrace(
    LoggerEnrichmentConfiguration enrichmentConfiguration,
    bool onlyWhenNoException)
  {
    if (!StackTraceEnricher._gatherStackTrace)
      return enrichmentConfiguration.With(Array.Empty<ILogEventEnricher>());
    return enrichmentConfiguration.With(new ILogEventEnricher[1]
    {
      (ILogEventEnricher) new StackTraceEnricher(onlyWhenNoException)
    });
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static LogEventPropertyValue Factory()
  {
    return (LogEventPropertyValue) new ScalarValue((object) StackTraceEnricher.GetStackTrace(4).ToString());
  }

  internal static string GetStackTrace(LogEvent logEvent)
  {
    LogEventPropertyValue eventPropertyValue;
    return !logEvent.Properties.TryGetValue("StackTrace", out eventPropertyValue) || !(eventPropertyValue is ScalarValue scalarValue) || !(scalarValue.Value is string str) || string.IsNullOrWhiteSpace(str) ? (string) null : str;
  }
}
