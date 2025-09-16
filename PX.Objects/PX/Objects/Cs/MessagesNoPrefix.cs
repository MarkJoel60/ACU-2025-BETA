// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.MessagesNoPrefix
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.CS;

[PXLocalizable]
public static class MessagesNoPrefix
{
  public const string SalesTerritoryInactive = "The sales territory is inactive.";
  public const string EndTimeCannotBeEarlier = "The end time cannot be earlier than the sum of the start time and break time.";
  public const string Disabled = "Disabled";
  public const string WorkTimeMask = "###d ##h ##m";
  public const string CalendarIsNotFound = "The calendar {0} is not found.";
  public const string CalendarSlotWasNotInitialized = "The calendar slot was not initialized.";
  public const string CalendarDoesNotHaveWorkingDays = "The calendar {0} does not have working days.";
  public const string CalendarMustHaveWorkingDays = "The calendar {0} should have the Day of Week check box selected for at least one weekday.";
  public const string WorkdayTimeMustBeBetweenZeroAndOneDay = "The Workday Hours should be greater than 00:00 and less than or equal to 24:00.";
}
