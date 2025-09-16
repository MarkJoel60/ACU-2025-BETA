// Decompiled with JetBrains decompiler
// Type: PX.Logging.SubLogger
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog.Configuration;
using Serilog.Events;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Logging;

internal sealed class SubLogger
{
  internal SubLogger(
    string name,
    LogEventLevel level,
    Action<LoggerSinkConfiguration> configurator)
    : this(level.ToLevelSwitch(), configurator, (IReadOnlyDictionary<string, object>) new Dictionary<string, object>()
    {
      {
        name,
        (object) level
      }
    })
  {
  }

  internal SubLogger(
    string name,
    IInformingLevelSwitch levelSwitch,
    Action<LoggerSinkConfiguration> configurator)
    : this(levelSwitch, configurator, (IReadOnlyDictionary<string, object>) new Dictionary<string, object>()
    {
      {
        name,
        (object) levelSwitch.MinimumLevel
      }
    })
  {
  }

  internal SubLogger(
    string name,
    IInformingLevelSwitch levelSwitch,
    Action<LoggerSinkConfiguration> configurator,
    IReadOnlyDictionary<string, object> levels)
    : this(levelSwitch, configurator, (IReadOnlyDictionary<string, object>) new Dictionary<string, object>()
    {
      {
        name,
        (object) levels
      }
    })
  {
  }

  private SubLogger(
    IInformingLevelSwitch levelSwitch,
    Action<LoggerSinkConfiguration> configurator,
    IReadOnlyDictionary<string, object> levels)
  {
    this.LevelSwitch = levelSwitch;
    this.Configurator = configurator;
    this.Levels = levels;
  }

  internal IInformingLevelSwitch LevelSwitch { get; }

  internal Action<LoggerSinkConfiguration> Configurator { get; }

  internal IReadOnlyDictionary<string, object> Levels { get; }
}
