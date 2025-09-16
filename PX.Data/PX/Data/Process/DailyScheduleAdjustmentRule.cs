// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.DailyScheduleAdjustmentRule
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;

#nullable disable
namespace PX.Data.Process;

internal class DailyScheduleAdjustmentRule : ScheduleAdjustmentRuleBase
{
  public override string TypeID => "D";

  protected override void AdjustNextDate(AUSchedule schedule, bool findNextDay)
  {
    if (!findNextDay)
      return;
    this.AdjustNextDate(schedule);
  }

  public override void AdjustNextDate(AUSchedule schedule)
  {
    int num = schedule.DailyFrequency.GetValueOrDefault() > (short) 0 ? (int) schedule.DailyFrequency.GetValueOrDefault() : 1;
    schedule.NextRunDate = new System.DateTime?(schedule.NextRunDate.Value.AddDays((double) num));
  }
}
