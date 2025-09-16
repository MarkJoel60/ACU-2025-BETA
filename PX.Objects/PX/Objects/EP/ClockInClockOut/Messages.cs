// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.Messages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.EP.ClockInClockOut;

[PXLocalizable]
public static class Messages
{
  public const string EPTimeLogTypeUsedInEpSetup = "The type cannot be deleted because it is specified as the Default Time Log Type on the Time & Expenses Preferences (EP101000) form.";
  public const string EPTimeLogTypeUsedInEpTimeLog = "The type cannot be deleted because there is at least one time entry with this type.";
  public const string EPTimeLogUsedInTimeActivity = "The time log cannot be deleted because it is associated with at least one time activity.";
  public const string EPTimeLogStartDateGreaterEndDate = "The start date cannot be later than the end date.";
  public const string EPTimeLogEndDateLessStartDate = "The end date cannot be earlier than the start date.";
  public const string EPTimeLogStartTimeGreaterEndTime = "The start time cannot be later than the end time.";
  public const string EPTimeLogEndTimeLessStartTime = "The end time cannot be earlier than the start time.";
  public const string EPTimeLogRelatedEntityDeleted = "The document related to the time log has been deleted.";
  public const string TimeLogCacheName = "Time Log";
  public const string TimeLogTypeCacheName = "Time Log Type";
  public const string ClockInTimerDataCacheName = "Clock In Timer";
  public const string TimeLogsViewName = "Time Logs";
  public const string AvailableTimersViewName = "Available Clock In Timers";
  public const string ActiveTimersViewName = "Active Clock In Timers";
}
