// Decompiled with JetBrains decompiler
// Type: PX.Logging.Enrichers.HttpRequestIdEnricher
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

/// <summary>Adds a HttpRequestId GUID to log events.</summary>
public class HttpRequestIdEnricher : ILogEventEnricher
{
  /// <summary>The property name added to enriched log events.</summary>
  public const string HttpRequestIdPropertyName = "HttpRequestId";

  /// <summary>
  /// Enriches the log event with an id assigned to the currently-executing HTTP request, if any.
  /// </summary>
  /// <param name="logEvent">The log event to enrich.</param>
  /// <param name="propertyFactory">Factory for creating new properties to add to the event.</param>
  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
    if (logEvent == null)
      throw new ArgumentNullException(nameof (logEvent));
    HttpContext current = HttpContext.Current;
    string str = current != null ? current.EnsureRequestTraceIdentifier() : (string) null;
    if (str == null)
      return;
    logEvent.AddPropertyIfAbsent(new LogEventProperty("HttpRequestId", (LogEventPropertyValue) new ScalarValue((object) str)));
  }
}
