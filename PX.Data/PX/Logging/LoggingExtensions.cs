// Decompiled with JetBrains decompiler
// Type: PX.Logging.LoggingExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using AutofacSerilogIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PX.Data;
using PX.Hosting;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

#nullable enable
namespace PX.Logging;

/// <exclude />
internal static class LoggingExtensions
{
  private const 
  #nullable disable
  string LegacySectionKey = "appSettings";
  private const string SerilogSectionKey = "serilog";
  private const string LicensingSectionKey = "licensing";

  internal static void AddPropertyIfAbsent(
    this LogEvent logEvent,
    string name,
    Func<LogEventPropertyValue> valueFactory)
  {
    if (logEvent == null)
      throw new ArgumentNullException(nameof (logEvent));
    if (!LogEventProperty.IsValidName(name))
      throw new ArgumentException("Invalid property name", nameof (name));
    if (valueFactory == null)
      throw new ArgumentNullException(nameof (valueFactory));
    if (logEvent.Properties.ContainsKey(name))
      return;
    logEvent.AddOrUpdateProperty(new LogEventProperty(name, valueFactory()));
  }

  internal static void AddLogger(this ILoggingBuilder loggingBuilder, ILogger logger)
  {
    ServiceCollectionServiceExtensions.AddSingleton(SerilogLoggingBuilderExtensions.AddSerilog(loggingBuilder, logger, false).AddSerilogFilter((IEnumerable<string>) new string[2]
    {
      "Microsoft.AspNetCore",
      typeof (HttpClient).FullName + "."
    }, (LogLevel) 3).AddSerilogFilter((IEnumerable<string>) new string[2]
    {
      "Microsoft.AspNetCore.SignalR",
      "Microsoft.AspNetCore.Http.Connections"
    }, (LogLevel) 1).Services, typeof (ILogger<>), typeof (LoggingExtensions.LoggerWrapper<>));
  }

  private static ILoggingBuilder AddSerilogFilter(
    this ILoggingBuilder loggingBuilder,
    IEnumerable<string> categories,
    LogLevel level)
  {
    return categories.Aggregate<string, ILoggingBuilder>(loggingBuilder, (Func<ILoggingBuilder, string, ILoggingBuilder>) ((lb, category) => FilterLoggingBuilderExtensions.AddFilter<SerilogLoggerProvider>(lb, category, level)));
  }

  internal static void RegisterLoggerAndEnsureDisposal(
    this ContainerBuilder builder,
    ILogger logger)
  {
    SerilogContainerBuilderExtensions.RegisterLogger(builder, logger, false, true, false);
  }

  internal static LoggerConfiguration Logger(
    this LoggerSinkConfiguration writeTo,
    Serilog.Core.Logger logger,
    bool dispose)
  {
    return !dispose ? writeTo.Logger((ILogger) logger, false, (LogEventLevel) 0, (LoggingLevelSwitch) null) : writeTo.Logger((System.Action<LoggerConfiguration>) (configuration => configuration.MinimumLevel.Verbose().WriteTo.Sink((ILogEventSink) logger, (LogEventLevel) 0, (LoggingLevelSwitch) null)), (LogEventLevel) 0, (LoggingLevelSwitch) null);
  }

  internal static IHostBuilder UseLogging(this IHostBuilder builder)
  {
    return builder.ConfigureServices((Action<HostBuilderContext, IServiceCollection>) ((context, services) =>
    {
      IModuleAssembler moduleAssembler = ModuleAssemblerExtensions.GetModuleAssembler(context);
      ILogger logger = new LoggerBuilder().AddSerilog((IConfiguration) context.Configuration.GetSection(ConfigurationPath.Combine(new string[2]
      {
        "appSettings",
        "serilog"
      }))).AddLicensing((IConfiguration) context.Configuration.GetSection(ConfigurationPath.Combine(new string[3]
      {
        "appSettings",
        "licensing",
        "serilog"
      }))).AddPXTrace((IConfiguration) context.Configuration.GetSection("pxtrace")).AddSystemLogging(context.Configuration, moduleAssembler).AddSinkInjectors(moduleAssembler).Build();
      HostBuilderContextExtensions.SetProperty<ILogger>(context, logger);
      PXTrace.Logger = logger.ForContext("Via", (object) "PXTrace.Logger", false);
      Log.Logger = logger.ForContext("Via", (object) (typeof (Log).FullName + ".Logger"), false);
      LoggingServiceCollectionExtensions.AddLogging(services, (System.Action<ILoggingBuilder>) (loggingBuilder => loggingBuilder.AddLogger(logger)));
    })).ConfigureContainer<ContainerBuilder>((Action<HostBuilderContext, ContainerBuilder>) ((context, containerBuilder) =>
    {
      containerBuilder.RegisterLoggerAndEnsureDisposal(context.GetLogger());
      containerBuilder.RegisterLicensingLog();
    }));
  }

  internal static ILogger GetLogger(this HostBuilderContext context)
  {
    return HostBuilderContextExtensions.GetProperty<ILogger>(context);
  }

  internal static IEnumerable<KeyValuePair<string, string>> SerilogAppSettingsProcessor(
    IEnumerable<KeyValuePair<string, string>> data)
  {
    foreach (KeyValuePair<string, string> keyValuePair in data)
    {
      if (keyValuePair.Key.StartsWith("serilog:", StringComparison.OrdinalIgnoreCase) || keyValuePair.Key.StartsWith("licensing:serilog:", StringComparison.OrdinalIgnoreCase))
        yield return new KeyValuePair<string, string>(ConfigurationPath.Combine(new string[2]
        {
          "appSettings",
          keyValuePair.Key
        }), Environment.ExpandEnvironmentVariables(keyValuePair.Value));
      else
        yield return keyValuePair;
    }
  }

  private class LoggerWrapper<T> : ILogger<T>, ILogger
  {
    private readonly ILogger _logger;

    public LoggerWrapper(ILoggerFactory factory)
    {
      this._logger = factory != null ? factory.CreateLogger(typeof (T).FullName) : throw new ArgumentNullException(nameof (factory));
    }

    IDisposable ILogger.BeginScope<TState>(TState state) => this._logger.BeginScope<TState>(state);

    bool ILogger.IsEnabled(LogLevel logLevel) => this._logger.IsEnabled(logLevel);

    void ILogger.Log<TState>(
      LogLevel logLevel,
      EventId eventId,
      TState state,
      Exception exception,
      Func<TState, Exception, string> formatter)
    {
      this._logger.Log<TState>(logLevel, eventId, state, exception, formatter);
    }
  }
}
