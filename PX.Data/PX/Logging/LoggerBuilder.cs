// Decompiled with JetBrains decompiler
// Type: PX.Logging.LoggerBuilder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using PX.Logging.Enrichers;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#nullable enable
namespace PX.Logging;

internal class LoggerBuilder
{
  internal const 
  #nullable disable
  string PipelineInitializationMessageTemplate = "{LoggingPipeline} initialized with minimum level {MinimumLoggingLevel}";
  private IConfiguration _legacyAppSettings = (IConfiguration) new ConfigurationBuilder().Build();
  private readonly List<Func<IReadOnlyCollection<SubLogger>>> _subLoggerProviders = new List<Func<IReadOnlyCollection<SubLogger>>>();
  private readonly List<Action<ILogger>> _innerLoggerBuiltHandlers = new List<Action<ILogger>>();
  private readonly List<Func<SubLogger>> _backEndProviders = new List<Func<SubLogger>>();

  internal LoggerBuilder AddSerilog(IConfiguration configuration)
  {
    this._legacyAppSettings = configuration;
    SelfLog.Initialize(configuration);
    return this;
  }

  internal LoggerBuilder AddSubLoggerProvider(
    Func<IEnumerable<SubLogger>> provider,
    string errorMessage)
  {
    this._subLoggerProviders.Add(LoggerBuilder.Wrap(provider, errorMessage));
    return this;
  }

  internal LoggerBuilder AddInnerLoggerBuiltHandler(Action<ILogger> handler)
  {
    this._innerLoggerBuiltHandlers.Add(handler);
    return this;
  }

  internal LoggerBuilder AddBackEndProvider(Func<SubLogger> provider)
  {
    this._backEndProviders.Add(provider);
    return this;
  }

  private static Func<IReadOnlyCollection<SubLogger>> Wrap(
    Func<IEnumerable<SubLogger>> provider,
    string errorMessage)
  {
    return (Func<IReadOnlyCollection<SubLogger>>) (() =>
    {
      using (SelfLog.MarkLoggerStartup())
      {
        try
        {
          return (IReadOnlyCollection<SubLogger>) provider().ToArray<SubLogger>();
        }
        catch (Exception ex) when (
        {
          // ISSUE: unable to correctly present filter
          string format = errorMessage;
          if (SelfLog.WriteAndRethrow(ex, format))
          {
            SuccessfulFiltering;
          }
          else
            throw;
        }
        )
        {
          throw;
        }
      }
    });
  }

  /// <remarks>
  /// This method runs - should be run - once and only once, as early as possible in the application lifecycle
  /// Any exception leaving this method will (should) stop the application initialization.
  /// </remarks>
  internal ILogger Build() => this.BuildInternal().Item1;

  internal ILevelSwitch BuildLevelSwitch() => this.BuildInternal().Item2;

  private (ILogger, ILevelSwitch) BuildInternal()
  {
    StackTraceEnricher.Initialize(this._legacyAppSettings);
    List<SubLogger> list = this._subLoggerProviders.SelectMany<Func<IReadOnlyCollection<SubLogger>>, SubLogger>((Func<Func<IReadOnlyCollection<SubLogger>>, IEnumerable<SubLogger>>) (provider => (IEnumerable<SubLogger>) provider())).ToList<SubLogger>();
    (Logger logger, IInformingLevelSwitch levelSwitch, IReadOnlyDictionary<string, object> levels) = list.Count != 0 ? LoggerBuilder.CreateInnerLogger(this.AppendSerilogFromConfiguration((IEnumerable<SubLogger>) list)) : throw new ArgumentOutOfRangeException("subLoggers", "Should have at least one sublogger");
    foreach (Action<ILogger> loggerBuiltHandler in this._innerLoggerBuiltHandlers)
      loggerBuiltHandler((ILogger) logger);
    (ILogger, IInformingLevelSwitch) outerLogger = LoggerBuilder.CreateOuterLogger(this._backEndProviders.Select<Func<SubLogger>, SubLogger>((Func<Func<SubLogger>, SubLogger>) (provider => provider())).Prepend<SubLogger>(new SubLogger("Serilog", levelSwitch, (Action<LoggerSinkConfiguration>) (sinkConfiguration => sinkConfiguration.Logger(logger, true)), levels)));
    return (outerLogger.Item1, (ILevelSwitch) outerLogger.Item2);
  }

  private IEnumerable<SubLogger> AppendSerilogFromConfiguration(IEnumerable<SubLogger> subLoggers)
  {
    LoggerBuilder loggerBuilder = this;
    foreach (SubLogger subLogger in subLoggers)
      yield return subLogger;
    LogEventLevel? nullable = ConfigurationBinder.GetValue<LogEventLevel?>(loggerBuilder._legacyAppSettings, "minimum-level");
    if (nullable.HasValue)
    {
      LogEventLevel minimumLevel = nullable.GetValueOrDefault();
      // ISSUE: reference to a compiler-generated method
      yield return new SubLogger("web.config/appSettings", minimumLevel, new Action<LoggerSinkConfiguration>(loggerBuilder.\u003CAppendSerilogFromConfiguration\u003Eb__13_0));
    }
  }

  private static (Logger logger, IInformingLevelSwitch levelSwitch, IReadOnlyDictionary<string, object> levels) CreateInnerLogger(
    IEnumerable<SubLogger> subLoggers)
  {
    using (SelfLog.MarkLoggerStartup())
    {
      try
      {
        (LoggerConfiguration configuration, IInformingLevelSwitch levelSwitch, IReadOnlyDictionary<string, object> levels) = subLoggers.Aggregate();
        Logger logger = new LoggerConfiguration().MinimumLevel.ControlledBy((ILevelSwitch) levelSwitch).WriteTo.Sink((ILogEventSink) new RecursionGuardSink(configuration.Enrich.FromLogContext().Enrich.With<UserEnricher>().Enrich.With<CurrentCompanyEnricher>().Enrich.With<HttpRequestIdEnricher>().Enrich.With<ScreenIdEnricher>().Enrich.WithStackTrace(false).CreateLogger()), (LogEventLevel) 0, (LoggingLevelSwitch) null).CreateLogger();
        logger.ForContext("Downstream", (object) levels, false).ForContext(typeof (LoggerBuilder)).Information<string, LogEventLevel>("{LoggingPipeline} initialized with minimum level {MinimumLoggingLevel}", "Inner logging pipeline", levelSwitch.MinimumLevel);
        return (logger, levelSwitch, levels);
      }
      catch (Exception ex) when (SelfLog.WriteAndRethrow(ex, "Exception while initializing inner Serilog logger"))
      {
        throw;
      }
    }
  }

  private static (ILogger, IInformingLevelSwitch) CreateOuterLogger(
    IEnumerable<SubLogger> subLoggers)
  {
    using (SelfLog.MarkLoggerStartup())
    {
      try
      {
        (LoggerConfiguration configuration, IInformingLevelSwitch levelSwitch, IReadOnlyDictionary<string, object> levels) = subLoggers.Aggregate();
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        Logger logger = configuration.MinimumLevel.ControlledBy((ILevelSwitch) levelSwitch).Enrich.WithStackTrace(true).Enrich.With(new ILogEventEnricher[1]
        {
          (ILogEventEnricher) new AspNetCallbackEnricher(LoggerBuilder.\u003C\u003EO.\u003C0\u003E__RequestAccessor ?? (LoggerBuilder.\u003C\u003EO.\u003C0\u003E__RequestAccessor = new Func<HttpRequestBase>(AspNetCallbackEnricher.RequestAccessor)))
        }).CreateLogger();
        logger.ForContext("Downstream", (object) levels, false).ForContext(typeof (LoggerBuilder)).Information<string, LogEventLevel>("{LoggingPipeline} initialized with minimum level {MinimumLoggingLevel}", "Outer logging pipeline", levelSwitch.MinimumLevel);
        return ((ILogger) logger, levelSwitch);
      }
      catch (Exception ex) when (SelfLog.WriteAndRethrow(ex, "Exception while initializing outer Serilog logger"))
      {
        throw;
      }
    }
  }
}
