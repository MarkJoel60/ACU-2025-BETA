// Decompiled with JetBrains decompiler
// Type: PX.Data.LicensingLog
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Microsoft.Extensions.Configuration;
using PX.Data.Update;
using PX.Licensing;
using PX.Logging;
using PX.Logging.Enrichers;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

#nullable enable
namespace PX.Data;

internal static class LicensingLog
{
  private static 
  #nullable disable
  ILogger _logger = Logger.None;

  internal static LoggerBuilder AddLicensing(
    this LoggerBuilder loggerBuilder,
    IConfiguration configuration)
  {
    return loggerBuilder.AddInnerLoggerBuiltHandler((System.Action<ILogger>) (logger => LicensingLog.Initialize(configuration, logger.ForContext("Via", (object) nameof (LicensingLog), false))));
  }

  internal static void RegisterLicensingLog(this ContainerBuilder builder)
  {
    builder.RegisterBuildCallback((System.Action<ILifetimeScope>) (container => container.Disposer.AddInstanceForDisposal(Disposable.Create((System.Action) (() =>
    {
      if (!(LicensingLog._logger is IDisposable logger2))
        return;
      logger2.Dispose();
    })))));
  }

  internal static ILogger CreateLogger(IConfiguration section, ILogger inner)
  {
    using (SelfLog.MarkLoggerStartup())
    {
      try
      {
        LoggerConfiguration loggerConfiguration = new LoggerConfiguration();
        LogEventLevel? nullable = ConfigurationBinder.GetValue<LogEventLevel?>(section, "minimum-level");
        if (nullable.HasValue)
        {
          loggerConfiguration.ReadFrom.KeyValuePairs(ConfigurationExtensions.AsEnumerable(section, true).Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (pair => pair.Value != null)));
          loggerConfiguration.WriteTo.Logger(inner, false, (LogEventLevel) 0, (LoggingLevelSwitch) null);
        }
        else
        {
          nullable = new LogEventLevel?((LogEventLevel) 3);
          loggerConfiguration.MinimumLevel.Is(nullable.GetValueOrDefault()).WriteTo.Logger(inner, false, (LogEventLevel) 0, (LoggingLevelSwitch) null);
        }
        Logger logger = loggerConfiguration.Enrich.With<HttpRequestIdEnricher>().Enrich.With<HttpSessionIdEnricher>().Enrich.With<CurrentCompanyEnricher>().Enrich.FromLogContext().Destructure.ByTransforming<PXLicense>((Func<PXLicense, object>) (l => l.GetData())).CreateLogger();
        inner.ForContext(typeof (LicensingLog)).Information<string, LogEventLevel?>("{LoggingPipeline} initialized with minimum level {MinimumLoggingLevel}", "Licensing logging pipeline", nullable);
        return (ILogger) logger;
      }
      catch (Exception ex) when (SelfLog.WriteAndRethrow(ex, "Exception while initializing Serilog for licensing"))
      {
        throw;
      }
    }
  }

  private static void Initialize(IConfiguration section, ILogger inner)
  {
    LicensingLog._logger = LicensingLog.CreateLogger(section, inner);
    PXLicenseHelper.InitializeLogging(LicensingLog._logger);
    CachedLicenseDefinition.InitializeLogging(LicensingLog._logger);
    PXLicensingServer.InitializeLogging(LicensingLog._logger);
    LicensingManager.InitializeLogging(LicensingLog._logger);
    PXLicenseHelper.GetBaseInstallationId(LicensingLog.ForClassContext(LicensingLog._logger, typeof (LicensingLog)), (LogEventLevel) 2);
  }

  internal static ILogger ForClassContext(System.Type type)
  {
    return LicensingLog.ForClassContext(LicensingLog._logger, type);
  }

  internal static ILogger ForClassContext(ILogger logger, System.Type type)
  {
    return logger.ForContext(type).ForContext("ClassContext", (object) type.Name, false);
  }
}
