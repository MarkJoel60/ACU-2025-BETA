// Decompiled with JetBrains decompiler
// Type: PX.Logging.TraceProviders.TraceLevelFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace PX.Logging.TraceProviders;

internal class TraceLevelFilter
{
  private static readonly Dictionary<string, SourceLevels> KnownLevels = new Dictionary<string, SourceLevels>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
  {
    {
      "Verbose",
      SourceLevels.All
    },
    {
      "Debug",
      SourceLevels.Information
    },
    {
      "Information",
      SourceLevels.Information
    },
    {
      "Warning",
      SourceLevels.Warning
    },
    {
      "Error",
      SourceLevels.Error
    },
    {
      "Fatal",
      SourceLevels.Critical
    }
  };
  private readonly SourceLevels _minimumLevel;

  public TraceLevelFilter(string minimumLevel, SourceLevels defaultMinimumLevel)
  {
    if (TraceLevelFilter.KnownLevels.TryGetValue(minimumLevel ?? "", out this._minimumLevel))
      return;
    this._minimumLevel = defaultMinimumLevel;
  }

  internal bool ShouldTrace(TraceEventType eventType)
  {
    return (eventType & (TraceEventType) this._minimumLevel) != 0;
  }

  internal SourceLevels MinimumLevel => this._minimumLevel;
}
