// Decompiled with JetBrains decompiler
// Type: PX.Common.SystemTimeRegion
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;

#nullable disable
namespace PX.Common;

public sealed class SystemTimeRegion : ITimeRegion
{
  private readonly TimeZoneInfo \u0002;

  public SystemTimeRegion(TimeZoneInfo zone) => this.\u0002 = zone;

  public bool SupportsDaylightSavingTime
  {
    get => this.\u0002 != null && this.\u0002.SupportsDaylightSavingTime;
  }

  public TimeZoneInfo.AdjustmentRule GetAdjustmentRule(int year)
  {
    if (this.\u0002 != null)
    {
      foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in this.\u0002.GetAdjustmentRules())
      {
        if (adjustmentRule.DateStart.Year <= year && adjustmentRule.DateEnd.Year >= year)
          return adjustmentRule;
      }
    }
    return (TimeZoneInfo.AdjustmentRule) null;
  }

  public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules() => this.\u0002?.GetAdjustmentRules();

  public bool IsAmbiguousTime(DateTime dateTime)
  {
    return this.SupportsDaylightSavingTime && this.\u0002.IsAmbiguousTime(dateTime);
  }
}
