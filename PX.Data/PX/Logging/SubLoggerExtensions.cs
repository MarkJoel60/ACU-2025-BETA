// Decompiled with JetBrains decompiler
// Type: PX.Logging.SubLoggerExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog;
using System.Collections.Generic;

#nullable disable
namespace PX.Logging;

internal static class SubLoggerExtensions
{
  internal static (LoggerConfiguration configuration, IInformingLevelSwitch levelSwitch, IReadOnlyDictionary<string, object> levels) Aggregate(
    this IEnumerable<SubLogger> subLoggers)
  {
    LoggerConfiguration loggerConfiguration = new LoggerConfiguration();
    Dictionary<string, object> dictionary = new Dictionary<string, object>();
    List<IInformingLevelSwitch> levelSwitches = new List<IInformingLevelSwitch>();
    foreach (SubLogger subLogger in subLoggers)
    {
      subLogger.Configurator(loggerConfiguration.WriteTo);
      levelSwitches.Add(subLogger.LevelSwitch);
      foreach (KeyValuePair<string, object> level in (IEnumerable<KeyValuePair<string, object>>) subLogger.Levels)
        dictionary.Add(level.Key, level.Value);
    }
    return (loggerConfiguration, levelSwitches.Aggregate(), (IReadOnlyDictionary<string, object>) dictionary);
  }
}
