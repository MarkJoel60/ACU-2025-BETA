// Decompiled with JetBrains decompiler
// Type: PX.Logging.SystemLoggingExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using PX.Hosting;
using PX.Telemetry.SerilogSink;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Logging;

internal static class SystemLoggingExtensions
{
  internal static 
  #nullable disable
  LoggerBuilder AddSystemLogging(
    this LoggerBuilder loggerBuilder,
    Func<IEnumerable<SubLogger>> systemLoggingProvider)
  {
    return loggerBuilder.AddSubLoggerProvider(systemLoggingProvider, "Exception while getting system logging components");
  }

  internal static LoggerBuilder AddSystemLogging(
    this LoggerBuilder loggerBuilder,
    IConfiguration configuration,
    IModuleAssembler moduleAssembler)
  {
    return loggerBuilder.AddSystemLogging((Func<IEnumerable<SubLogger>>) (() => (IEnumerable<SubLogger>) new SubLogger[1]
    {
      SystemLoggingExtensions.CreateSystemSubLogger(configuration, moduleAssembler)
    }));
  }

  private static SubLogger CreateSystemSubLogger(
    IConfiguration configuration,
    IModuleAssembler moduleAssembler)
  {
    List<Action<LoggerSinkConfiguration>> actions = new List<Action<LoggerSinkConfiguration>>();
    List<IInformingLevelSwitch> levelSwitches = new List<IInformingLevelSwitch>();
    Dictionary<string, object> levels = new Dictionary<string, object>();
    foreach ((string str, IInformingLevelSwitch levelSwitch, Action<LoggerSinkConfiguration> action) in SystemLoggingExtensions.CreateComponents(configuration, moduleAssembler))
    {
      actions.Add(action);
      levelSwitches.Add(levelSwitch);
      levels.Add(str, (object) levelSwitch.MinimumLevel);
    }
    IInformingLevelSwitch masterLevelSwitch = levelSwitches.Aggregate();
    return new SubLogger("System", masterLevelSwitch, (Action<LoggerSinkConfiguration>) (sinkConfiguration => sinkConfiguration.Logger((Action<LoggerConfiguration>) (loggerConfiguration =>
    {
      loggerConfiguration.MinimumLevel.ControlledBy((ILevelSwitch) masterLevelSwitch);
      foreach (Action<LoggerSinkConfiguration> action in actions)
        action(loggerConfiguration.WriteTo);
    }), (LogEventLevel) 0, (LoggingLevelSwitch) null)), (IReadOnlyDictionary<string, object>) levels);
  }

  private static IEnumerable<(string name, IInformingLevelSwitch levelSwitch, Action<LoggerSinkConfiguration>)> CreateComponents(
    IConfiguration configuration,
    IModuleAssembler moduleAssembler)
  {
    IForwardingLevelSwitch telemetryLevelSwitch = ((LogEventLevel) 0).ToForwardingLevelSwitch();
    ITelemetrySinkConfiguration sinkConfiguration1 = moduleAssembler.GetTelemetrySinkConfiguration();
    Func<LoggerSinkConfiguration, LoggingLevelSwitch, LoggerConfiguration> telemetryConfigurator = sinkConfiguration1.CreateSinkConfigurator(new Action<LogEventLevel>(((ISettableLevelSwitch) telemetryLevelSwitch).SetMinimumLevel));
    yield return (sinkConfiguration1.GetType().FullName, (IInformingLevelSwitch) telemetryLevelSwitch, (Action<LoggerSinkConfiguration>) (sinkConfiguration => sinkConfiguration.Configure(telemetryConfigurator, (ILevelSwitch) telemetryLevelSwitch)));
    if (ConfigurationBinder.GetValue<bool>(configuration, "SystemEventsDbSink:Enabled", true))
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      yield return ("SystemEventsDbSink", ((LogEventLevel) 2).ToLevelSwitch(), (Action<LoggerSinkConfiguration>) (sinkConfiguration => sinkConfiguration.Conditional(SystemLoggingExtensions.\u003C\u003EO.\u003C0\u003E__IsValidSystemEvent ?? (SystemLoggingExtensions.\u003C\u003EO.\u003C0\u003E__IsValidSystemEvent = new Func<LogEvent, bool>(PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink.IsValidSystemEvent)), (Action<LoggerSinkConfiguration>) (conditionalSinkConfiguration => conditionalSinkConfiguration.Sink((ILogEventSink) new PX.Logging.Sinks.SystemEventsDbSink.SystemEventsDbSink(), (LogEventLevel) 2)))));
    }
  }

  private static ITelemetrySinkConfiguration GetTelemetrySinkConfiguration(
    this IModuleAssembler moduleAssembler)
  {
    ITelemetrySinkConfiguration[] array = moduleAssembler.ScanAssembliesAndResolveAssignableTo<ITelemetrySinkConfiguration>().ToArray<ITelemetrySinkConfiguration>();
    switch (array.Length)
    {
      case 0:
        throw new InvalidOperationException("No telemetry configurator found");
      case 1:
        return array[0];
      default:
        throw new InvalidOperationException("Multiple telemetry configurators found:\n" + string.Join("\n", ((IEnumerable<ITelemetrySinkConfiguration>) array).Select<ITelemetrySinkConfiguration, string>((Func<ITelemetrySinkConfiguration, string>) (c => c.GetType().AssemblyQualifiedName))));
    }
  }
}
