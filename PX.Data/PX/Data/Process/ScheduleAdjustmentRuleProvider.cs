// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.ScheduleAdjustmentRuleProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Process;

internal class ScheduleAdjustmentRuleProvider : IScheduleAdjustmentRuleProvider
{
  private readonly IEnumerable<IScheduleAdjustmentRule> _rules;

  public ScheduleAdjustmentRuleProvider(IEnumerable<IScheduleAdjustmentRule> rules)
  {
    this._rules = rules ?? throw new ArgumentNullException(nameof (rules));
  }

  public IScheduleAdjustmentRule GetRule(AUSchedule schedule)
  {
    return this._rules.FirstOrDefault<IScheduleAdjustmentRule>((Func<IScheduleAdjustmentRule, bool>) (x =>
    {
      string typeId = x.TypeID;
      return typeId != null && typeId.Equals(schedule?.ScheduleType);
    }));
  }
}
