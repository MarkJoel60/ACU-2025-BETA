// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.DateInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;
using System.Diagnostics;

#nullable disable
namespace PX.Objects.CR;

[DebuggerDisplay("WorkingDay={IsWorkingDay} StartTime={StartTime.ToShortTimeString()} EndTime={EndTime.ToShortTimeString()}")]
public struct DateInfo
{
  private bool isWorkingDay;
  private DateTime startTime;
  private DateTime endTime;

  public DateInfo(bool? isWorkingDay, DateTime? startTime, DateTime? endTime)
  {
    this.isWorkingDay = isWorkingDay.GetValueOrDefault();
    this.startTime = this.isWorkingDay ? startTime.Value : new DateTime(2008, 1, 1, 0, 0, 0);
    this.endTime = this.isWorkingDay ? endTime.Value : new DateTime(2008, 1, 1, 0, 0, 0);
  }

  public bool IsWorkingDay => this.isWorkingDay;

  public TimeSpan StartTime => this.startTime.TimeOfDay;

  public TimeSpan EndTime => this.endTime.TimeOfDay;
}
