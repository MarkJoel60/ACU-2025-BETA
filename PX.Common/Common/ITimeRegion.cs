// Decompiled with JetBrains decompiler
// Type: PX.Common.ITimeRegion
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;

#nullable disable
namespace PX.Common;

public interface ITimeRegion
{
  bool SupportsDaylightSavingTime { get; }

  TimeZoneInfo.AdjustmentRule GetAdjustmentRule(int year);

  TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules();

  bool IsAmbiguousTime(DateTime dateTime);
}
