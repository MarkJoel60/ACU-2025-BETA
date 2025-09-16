// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.IScheduleAdjustmentRule
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;

#nullable disable
namespace PX.Data.Process;

/// <summary>
/// Common interface for all schedule adjustment rules
/// (schedule adjustment rule is a class that can adjust next run date based on some rule,
/// for example, run schedule monthly/weekly/first Sunday of a month/etc).
/// </summary>
[PXInternalUseOnly]
public interface IScheduleAdjustmentRule
{
  string TypeID { get; }

  void AdjustNextDate(AUSchedule schedule);

  void AdjustNextDate(AUSchedule schedule, System.DateTime startedAt);
}
