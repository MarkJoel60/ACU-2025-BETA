// Decompiled with JetBrains decompiler
// Type: PX.Logging.Enrichers.HttpSessionIdEnricher
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog.Core;
using Serilog.Events;
using System;
using System.Web;

#nullable disable
namespace PX.Logging.Enrichers;

/// <summary>Enrich log events with the HttpSessionId property.</summary>
internal class HttpSessionIdEnricher : ILogEventEnricher
{
  /// <summary>The property name added to enriched log events.</summary>
  public const string HttpSessionIdPropertyName = "HttpSessionId";

  /// <summary>
  /// Enrich the log event with the current ASP.NET session id, if sessions are enabled.</summary>
  /// <param name="logEvent">The log event to enrich.</param>
  /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
    if (logEvent == null)
      throw new ArgumentNullException(nameof (logEvent));
    if (HttpContext.Current == null || HttpContext.Current.Session == null)
      return;
    LogEventProperty logEventProperty = new LogEventProperty("HttpSessionId", (LogEventPropertyValue) new ScalarValue((object) HttpContext.Current.Session.SessionID));
    logEvent.AddPropertyIfAbsent(logEventProperty);
  }
}
