// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Services.WorkTimeCalculation.CalendarExceptionInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.CS.Services.WorkTimeCalculation;

[PXInternalUseOnly]
public readonly struct CalendarExceptionInfo
{
  public CalendarExceptionInfo(DateTime date, TimeRange timeRange, bool isWorkingDay)
  {
    this.Date = date.Date;
    this.TimeRange = timeRange;
    this.IsWorkingDay = isWorkingDay;
  }

  public DateTime Date { get; }

  public bool IsWorkingDay { get; }

  public TimeRange TimeRange { get; }
}
