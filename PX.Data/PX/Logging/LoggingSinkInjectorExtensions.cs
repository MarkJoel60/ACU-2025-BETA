// Decompiled with JetBrains decompiler
// Type: PX.Logging.LoggingSinkInjectorExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Hosting;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Logging;

internal static class LoggingSinkInjectorExtensions
{
  internal static LoggerBuilder AddSinkInjectors(
    this LoggerBuilder loggerBuilder,
    IModuleAssembler moduleAssembler)
  {
    return loggerBuilder.AddSinkInjectors(new Func<IEnumerable<ILoggingSinkInjector>>(moduleAssembler.ScanAssembliesAndResolveAssignableTo<ILoggingSinkInjector>));
  }

  internal static LoggerBuilder AddSinkInjectors(
    this LoggerBuilder loggerBuilder,
    Func<IEnumerable<ILoggingSinkInjector>> loggingSinkInjectorProvider)
  {
    return loggerBuilder.AddSinkInjectors((Func<IEnumerable<SubLogger>>) (() => loggingSinkInjectorProvider().Select<ILoggingSinkInjector, SubLogger>((Func<ILoggingSinkInjector, SubLogger>) (injector =>
    {
      IForwardingLevelSwitch levelSwitch = ((LogEventLevel) 0).ToForwardingLevelSwitch();
      ILogEventSink sink = injector.CreateLoggingSink(new Action<LogEventLevel>(((ISettableLevelSwitch) levelSwitch).SetMinimumLevel));
      return new SubLogger(injector.GetType().AssemblyQualifiedName, (IInformingLevelSwitch) levelSwitch, (Action<LoggerSinkConfiguration>) (sinkConfiguration => sinkConfiguration.Sink(sink, (ILevelSwitch) levelSwitch)));
    }))));
  }

  internal static LoggerBuilder AddSinkInjectors(
    this LoggerBuilder loggerBuilder,
    Func<IEnumerable<SubLogger>> loggingSinkInjectorProvider)
  {
    return loggerBuilder.AddSubLoggerProvider(loggingSinkInjectorProvider, "Exception while resolving Logging Sink Injectors");
  }
}
