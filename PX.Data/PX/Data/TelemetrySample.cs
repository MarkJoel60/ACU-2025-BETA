// Decompiled with JetBrains decompiler
// Type: PX.Data.TelemetrySample
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog.Core;
using Serilog.Events;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public class TelemetrySample : ILogEventEnricher
{
  public Dictionary<string, string> Props = new Dictionary<string, string>();
  public Dictionary<string, double> Metrics = new Dictionary<string, double>();

  public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
  {
    foreach (KeyValuePair<string, double> metric in this.Metrics)
      logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(metric.Key, (object) metric.Value, false));
    foreach (KeyValuePair<string, string> prop in this.Props)
    {
      if (prop.Value != null)
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(prop.Key, (object) prop.Value, false));
    }
  }
}
