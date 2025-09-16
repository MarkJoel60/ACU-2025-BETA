// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.IScheduledJobHandler
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System.Collections.Generic;

#nullable enable
namespace PX.Data.Process;

internal interface IScheduledJobHandler
{
  bool IsActive { get; }

  void ProcessSchedule(
    PXGraph screen,
    List<PXFilterRow> filters,
    string viewName,
    AUSchedule schedule,
    string? screenId);

  PXCache? GetProcessingEntityCache(AUSchedule schedule, AUScheduleHistory historyItem);

  void ModifyAUScheduleScreen(AUScheduleMaint screen, PXCache scheduleCache, AUSchedule schedule);

  string Type { get; }

  string Description { get; }
}
