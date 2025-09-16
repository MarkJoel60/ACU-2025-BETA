// Decompiled with JetBrains decompiler
// Type: PX.Telemetry.SerilogSink.ITelemetrySinkConfiguration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;

#nullable disable
namespace PX.Telemetry.SerilogSink;

internal interface ITelemetrySinkConfiguration
{
  Func<LoggerSinkConfiguration, LoggingLevelSwitch, LoggerConfiguration> CreateSinkConfigurator(
    Action<LogEventLevel> informer);
}
