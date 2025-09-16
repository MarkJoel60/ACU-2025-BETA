// Decompiled with JetBrains decompiler
// Type: PX.Logging.LevelSwitchExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Logging;

internal static class LevelSwitchExtensions
{
  private static readonly IInformingLevelSwitch Maximized = ((LogEventLevel) 5).ToLevelSwitch();

  internal static LoggerConfiguration ControlledBy(
    this LoggerMinimumLevelConfiguration configuration,
    ILevelSwitch levelSwitch)
  {
    return levelSwitch.ApplyTo(configuration);
  }

  private static LogEventLevel Min(LogEventLevel level1, LogEventLevel level2)
  {
    return level1 >= level2 ? level2 : level1;
  }

  /// <summary>
  /// Will create a never-changing level switch (used for unification)
  /// </summary>
  internal static IInformingLevelSwitch ToLevelSwitch(this LogEventLevel level)
  {
    return (IInformingLevelSwitch) new LevelSwitchExtensions.ConstantLevelSwitch(level);
  }

  internal static IForwardingLevelSwitch ToForwardingLevelSwitch(this LogEventLevel level)
  {
    return (IForwardingLevelSwitch) new LevelSwitchExtensions.ForwardingLevelSwitch(level);
  }

  internal static LoggerConfiguration Sink(
    this LoggerSinkConfiguration writeTo,
    ILogEventSink sink,
    ILevelSwitch levelSwitch)
  {
    return levelSwitch.Apply<LoggerConfiguration>((Func<LoggingLevelSwitch, LoggerConfiguration>) (s => writeTo.Sink(sink, (LogEventLevel) 0, s)));
  }

  internal static LoggerConfiguration Configure(
    this LoggerSinkConfiguration writeTo,
    Func<LoggerSinkConfiguration, LoggingLevelSwitch, LoggerConfiguration> configurator,
    ILevelSwitch levelSwitch)
  {
    return levelSwitch.Apply<LoggerConfiguration>((Func<LoggingLevelSwitch, LoggerConfiguration>) (s => configurator(writeTo, s)));
  }

  /// <summary>
  /// Will maintain minimum level that is the lowest between <paramref name="threshold" /> and <paramref name="levelSwitch" /> (i.e., no higher than the <paramref name="threshold" />)
  /// </summary>
  internal static IInformingLevelSwitch Threshold(
    this IInformingLevelSwitch levelSwitch,
    LogEventLevel threshold)
  {
    return levelSwitch is LevelSwitchExtensions.ConstantLevelSwitch ? LevelSwitchExtensions.Min(threshold, levelSwitch.MinimumLevel).ToLevelSwitch() : (IInformingLevelSwitch) new LevelSwitchExtensions.ThresholdLevelSwitch(threshold, levelSwitch);
  }

  /// <summary>
  /// Will maintain minimum level between two given level switches
  /// </summary>
  internal static IInformingLevelSwitch CombineWith(
    this IInformingLevelSwitch levelSwitch,
    IInformingLevelSwitch other)
  {
    if (levelSwitch == null)
      return other ?? LevelSwitchExtensions.Maximized;
    if (other == null)
      return levelSwitch;
    if (levelSwitch is LevelSwitchExtensions.ConstantLevelSwitch)
      return other.Threshold(levelSwitch.MinimumLevel);
    return other is LevelSwitchExtensions.ConstantLevelSwitch ? levelSwitch.Threshold(other.MinimumLevel) : (IInformingLevelSwitch) new LevelSwitchExtensions.PairLevelSwitch(levelSwitch, other);
  }

  internal static IInformingLevelSwitch Aggregate(
    this IReadOnlyList<IInformingLevelSwitch> levelSwitches)
  {
    switch (levelSwitches.Count)
    {
      case 0:
        return LevelSwitchExtensions.Maximized;
      case 1:
        return levelSwitches[0];
      case 2:
        return levelSwitches[0].CombineWith(levelSwitches[1]);
      default:
        return (IInformingLevelSwitch) new LevelSwitchExtensions.AggregatingLevelSwitch((IReadOnlyCollection<IInformingLevelSwitch>) levelSwitches);
    }
  }

  private class ConstantLevelSwitch : IInformingLevelSwitch, ILevelSwitch
  {
    public ConstantLevelSwitch(LogEventLevel level) => this.MinimumLevel = level;

    public ISettableLevelSwitch Parent { get; set; }

    public LogEventLevel MinimumLevel { get; }

    public T Apply<T>(Func<LoggingLevelSwitch, T> func)
    {
      return func(new LoggingLevelSwitch(this.MinimumLevel));
    }

    public LoggerConfiguration ApplyTo(LoggerMinimumLevelConfiguration configuration)
    {
      return configuration.Is(this.MinimumLevel);
    }
  }

  private class ForwardingLevelSwitch : 
    IForwardingLevelSwitch,
    ISettableLevelSwitch,
    ILevelSwitch,
    IInformingLevelSwitch
  {
    private static readonly ISettableLevelSwitch DefaultParent = (ISettableLevelSwitch) new LevelSwitchExtensions.ForwardingLevelSwitch.Stub();
    private readonly LoggingLevelSwitch _switch;

    public ForwardingLevelSwitch(LogEventLevel initialMinimumLevel = 2)
    {
      this._switch = new LoggingLevelSwitch(initialMinimumLevel);
      this.Parent = LevelSwitchExtensions.ForwardingLevelSwitch.DefaultParent;
    }

    public ISettableLevelSwitch Parent { get; set; }

    public LogEventLevel MinimumLevel => this._switch.MinimumLevel;

    public virtual void SetMinimumLevel(LogEventLevel level)
    {
      this._switch.MinimumLevel = level;
      this.Parent.SetMinimumLevel(level);
    }

    public T Apply<T>(Func<LoggingLevelSwitch, T> func) => func(this._switch);

    public LoggerConfiguration ApplyTo(LoggerMinimumLevelConfiguration configuration)
    {
      return this.Apply<LoggerConfiguration>(new Func<LoggingLevelSwitch, LoggerConfiguration>(configuration.ControlledBy));
    }

    private sealed class Stub : ISettableLevelSwitch, ILevelSwitch
    {
      public LogEventLevel MinimumLevel => (LogEventLevel) 2;

      public T Apply<T>(Func<LoggingLevelSwitch, T> func)
      {
        return func(new LoggingLevelSwitch(this.MinimumLevel));
      }

      public LoggerConfiguration ApplyTo(LoggerMinimumLevelConfiguration configuration)
      {
        return configuration.Is(this.MinimumLevel);
      }

      public void SetMinimumLevel(LogEventLevel level)
      {
      }
    }
  }

  private sealed class ThresholdLevelSwitch : LevelSwitchExtensions.ForwardingLevelSwitch
  {
    private readonly LogEventLevel _threshold;

    public ThresholdLevelSwitch(LogEventLevel threshold, IInformingLevelSwitch levelSwitch)
      : base()
    {
      this._threshold = threshold;
      levelSwitch.Parent = (ISettableLevelSwitch) this;
      this.SetMinimumLevel(levelSwitch.MinimumLevel);
    }

    public override void SetMinimumLevel(LogEventLevel level)
    {
      base.SetMinimumLevel(LevelSwitchExtensions.Min(level, this._threshold));
    }
  }

  private sealed class PairLevelSwitch : LevelSwitchExtensions.ForwardingLevelSwitch
  {
    private readonly IInformingLevelSwitch _switch1;
    private readonly IInformingLevelSwitch _switch2;

    public PairLevelSwitch(IInformingLevelSwitch switch1, IInformingLevelSwitch switch2)
      : base()
    {
      this._switch1 = switch1;
      this._switch2 = switch2;
      this._switch1.Parent = (ISettableLevelSwitch) this;
      this._switch2.Parent = (ISettableLevelSwitch) this;
      this.SetMinimumLevel(this._switch1.MinimumLevel);
    }

    public override void SetMinimumLevel(LogEventLevel level)
    {
      base.SetMinimumLevel(LevelSwitchExtensions.Min(this._switch1.MinimumLevel, this._switch2.MinimumLevel));
    }
  }

  private sealed class AggregatingLevelSwitch : LevelSwitchExtensions.ForwardingLevelSwitch
  {
    private readonly IReadOnlyCollection<IInformingLevelSwitch> _children;

    public AggregatingLevelSwitch(
      IReadOnlyCollection<IInformingLevelSwitch> children)
      : base()
    {
      this._children = children;
      foreach (IInformingLevelSwitch child in (IEnumerable<IInformingLevelSwitch>) this._children)
        child.Parent = (ISettableLevelSwitch) this;
      this.SetMinimumLevel();
    }

    public override void SetMinimumLevel(LogEventLevel level) => this.SetMinimumLevel();

    private void SetMinimumLevel()
    {
      base.SetMinimumLevel(this._children.Min<IInformingLevelSwitch, LogEventLevel>((Func<IInformingLevelSwitch, LogEventLevel>) (l => l.MinimumLevel)));
    }
  }
}
