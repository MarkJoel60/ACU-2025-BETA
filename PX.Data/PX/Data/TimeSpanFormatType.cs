// Decompiled with JetBrains decompiler
// Type: PX.Data.TimeSpanFormatType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Defines data format types for the
/// <see cref="T:PX.Data.PXDBTimeSpanLongAttribute">PXDBTimeSpanLong</see>
/// and <see cref="T:PX.Data.PXTimeSpanLongAttribute">PXTimeSpanLong</see>
/// attributes.</summary>
public enum TimeSpanFormatType
{
  /// <summary>
  /// Time span in format "dddhhmm", where "ddd" represents days, "hh" represents
  /// hours (with leading zeros), "mm" represents minutes (with leading zeros).
  /// </summary>
  DaysHoursMinites,
  /// <summary>
  /// Time span in format "dddhhmm", where "ddd" represents days, "hh" represents
  /// hours (with leading zeros), "mm" represents minutes (with leading zeros).
  /// </summary>
  DaysHoursMinitesCompact,
  /// <summary>
  /// Time span in format "HHHHmm", where "HHHH" represents
  /// hours (recalculated to include days), "mm" represents minutes (with leading zeros).
  /// </summary>
  LongHoursMinutes,
  /// <summary>
  /// Time span in format "hhmm", where "hh" represents
  /// hours, "mm" represents minutes (with leading zeros).
  /// </summary>
  ShortHoursMinutes,
  /// <summary>
  /// Time span in format "hhmm", where "hh" represents
  /// hours (with leading zeros), "mm" represents minutes (with leading zeros).
  /// </summary>
  ShortHoursMinutesCompact,
}
