// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Services.WorkTimeCalculation.DateTimeInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Objects.CS.Services.WorkTimeCalculation;

[PXInternalUseOnly]
public readonly struct DateTimeInfo(PXTimeZoneInfo timeZoneInfo, DateTime dateTime) : 
  IEquatable<DateTimeInfo>
{
  public PXTimeZoneInfo TimeZoneInfo { get; } = timeZoneInfo ?? throw new ArgumentNullException(nameof (timeZoneInfo));

  public DateTime DateTime { get; } = dateTime;

  public static DateTimeInfo FromLocalTimeZone(DateTime dateTime)
  {
    return new DateTimeInfo(LocaleInfo.GetTimeZone(), dateTime);
  }

  public static DateTimeInfo FromUtcTimeZone(DateTime dateTime)
  {
    return new DateTimeInfo(PXTimeZoneInfo.Invariant, dateTime);
  }

  public DateTimeInfo ToTimeZone(PXTimeZoneInfo timeZone)
  {
    if (this.TimeZoneInfo.Equals((object) timeZone))
      return this;
    DateTime dateTime = PXTimeZoneInfo.ConvertTimeFromUtc(PXTimeZoneInfo.ConvertTimeToUtc(this.DateTime, this.TimeZoneInfo), timeZone);
    return new DateTimeInfo(timeZone, dateTime);
  }

  public DateTimeInfo ToUtc() => this.ToTimeZone(PXTimeZoneInfo.Invariant);

  public bool Equals(DateTimeInfo other)
  {
    return this.TimeZoneInfo.Equals((object) other.TimeZoneInfo) && this.DateTime.Equals(other.DateTime);
  }

  public override bool Equals(object obj) => obj is DateTimeInfo other && this.Equals(other);

  public override int GetHashCode()
  {
    return this.TimeZoneInfo.GetHashCode() * 397 ^ this.DateTime.GetHashCode();
  }
}
