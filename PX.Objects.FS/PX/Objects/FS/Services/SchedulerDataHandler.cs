// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.Services.SchedulerDataHandler
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Common;
using PX.Objects.FS.Interfaces;
using System;

#nullable disable
namespace PX.Objects.FS.Services;

internal class SchedulerDataHandler : ISchedulerDataHandler
{
  public void StoreDatesFilter(SchedulerDatesFilter filter, string screenId)
  {
    string sessionKey = SchedulerDataHandler.GetSessionKey(screenId);
    PXContext.SessionTyped<SchedulerSession>().PeriodInfos[sessionKey] = filter;
  }

  public SchedulerDatesFilter LoadDatesFilter(string screenId)
  {
    string sessionKey = SchedulerDataHandler.GetSessionKey(screenId);
    SchedulerDatesFilter periodInfo = PXContext.SessionTyped<SchedulerSession>().PeriodInfos[sessionKey];
    DateTime dateTime = PXContext.GetBusinessDate() ?? PXTimeZoneInfo.Today;
    SchedulerDatesFilter schedulerDatesFilter;
    if (periodInfo == null || !periodInfo.PeriodKind.HasValue)
    {
      schedulerDatesFilter = new SchedulerDatesFilter();
      schedulerDatesFilter.FilterBusinessHours = new bool?(true);
      schedulerDatesFilter.PeriodKind = new int?(1);
      schedulerDatesFilter.DateBegin = new DateTime?(dateTime);
      schedulerDatesFilter.DateEnd = new DateTime?(dateTime.AddDays(1.0));
      schedulerDatesFilter.DateSelected = new DateTime?(dateTime);
    }
    else
      schedulerDatesFilter = periodInfo;
    return schedulerDatesFilter;
  }

  private static string GetSessionKey(string screenId) => "_scheduler";
}
