// Decompiled with JetBrains decompiler
// Type: Serilog.LoggerEnrichmentConfigurationExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Logging.Enrichers;
using Serilog.Configuration;

#nullable disable
namespace Serilog;

internal static class LoggerEnrichmentConfigurationExtensions
{
  internal static LoggerConfiguration WithStackTrace(
    this LoggerEnrichmentConfiguration enrichmentConfiguration,
    bool onlyWhenNoException)
  {
    return StackTraceEnricher.WithStackTrace(enrichmentConfiguration, onlyWhenNoException);
  }
}
