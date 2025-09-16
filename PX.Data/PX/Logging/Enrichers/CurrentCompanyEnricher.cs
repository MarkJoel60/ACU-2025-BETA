// Decompiled with JetBrains decompiler
// Type: PX.Logging.Enrichers.CurrentCompanyEnricher
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Logging.Sinks.SystemEventsDbSink;
using Serilog.Core;
using Serilog.Events;

#nullable disable
namespace PX.Logging.Enrichers;

internal class CurrentCompanyEnricher : ILogEventEnricher
{
  internal const string TenantName = "CurrentCompany";

  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
    if (logEvent != null)
    {
      bool? nullable = logEvent.Properties?.ContainsKey("CurrentCompany");
      bool flag = true;
      if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        return;
    }
    string contextCompanyName = SystemEventHelper.GetContextCompanyName();
    if (contextCompanyName == null)
      return;
    logEvent.AddPropertyIfAbsent(new LogEventProperty("CurrentCompany", (LogEventPropertyValue) new ScalarValue((object) contextCompanyName)));
  }
}
