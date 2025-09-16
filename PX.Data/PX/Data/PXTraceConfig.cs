// Decompiled with JetBrains decompiler
// Type: PX.Data.PXTraceConfig
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

public class PXTraceConfig
{
  public const string ConfigurationPrefix = "pxtrace";
  public const string MaxTraceCountConfigKey = "maxTraceCount";
  public const uint MaxTraceCountDefault = 100;
  public const string TracePageUrl = "/Scripts/Trace/index.html";
  private uint maxTraceCount = 100;

  public uint MaxTraceCount
  {
    get => this.maxTraceCount;
    set
    {
      int num = (int) PXTraceConfig.MaxTraceCountThrowIfInvalid(value);
      this.maxTraceCount = value;
    }
  }

  private static uint MaxTraceCountThrowIfInvalid(uint val)
  {
    return val != 0U ? val : throw new ArgumentOutOfRangeException(nameof (val));
  }

  public static uint MaxTraceCountFromConfigValue(string cfgValue)
  {
    return string.IsNullOrWhiteSpace(cfgValue) ? 100U : PXTraceConfig.MaxTraceCountThrowIfInvalid(uint.Parse(cfgValue));
  }
}
