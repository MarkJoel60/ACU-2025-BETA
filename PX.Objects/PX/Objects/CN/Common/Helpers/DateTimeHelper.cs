// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Helpers.DateTimeHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using System;

#nullable disable
namespace PX.Objects.CN.Common.Helpers;

public static class DateTimeHelper
{
  public static DateTime CalculateBusinessDate(
    DateTime originalBusinessDate,
    int timeSpan,
    string calendarId)
  {
    int businessDaysDifference = DateTimeHelper.GetBusinessDaysDifference(originalBusinessDate, timeSpan, calendarId);
    return originalBusinessDate.AddDays((double) businessDaysDifference);
  }

  private static int GetBusinessDaysDifference(
    DateTime originalBusinessDate,
    int timeFrame,
    string calendarId)
  {
    int businessDaysDifference = 1;
    PXGraph instance = PXGraph.CreateInstance<PXGraph>();
    while (true)
    {
      DateTime date = originalBusinessDate.AddDays((double) businessDaysDifference);
      if (!CalendarHelper.IsWorkDay(instance, calendarId, date) || --timeFrame >= 1)
        ++businessDaysDifference;
      else
        break;
    }
    return businessDaysDifference;
  }
}
